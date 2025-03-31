using System.ComponentModel.Design.Serialization;
using HospitalChatApp.Shared.Models;
using HospitalChatApp.Shared.Interfaces;
using MessagePack;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;
using MagicOnion.Serialization;
using System.Diagnostics.Contracts;
using Cysharp.Runtime.Multicast;

namespace HospitalChatApp.Server;

public class HospitalChatHub(IMulticastGroupProvider groupProvider) : StreamingHubBase<IHospitalChatHub, IHospitalChatHubReceiver>, IHospitalChatHub
{
    private GroupService group = new GroupService(groupProvider);

    string userName = "unknown";

    public async Task LoginAsync(string loginId, string loginPassword)
    {
        var longinIdStatus =  await Global.EntityAccessor.FetchUsersWhereAsync(user => user.LoginId == loginId);
        var length = longinIdStatus.Length;
        if (length == 0)
        {
            this.Client.OnLoginFailed("IDが見つかりません");
            return;
        }

        if (longinIdStatus.FirstOrDefault(user => IsPasswordValid(loginPassword, user)) is not { } loginUser )
        {
            this.Client.OnLoginFailed("Passwordが一致しません");
            return;
        }
        this.Client.OnLogin(loginUser);

        this.group.AddMember(this.Client, loginUser.UserId);
    }

    private bool IsPasswordValid(string loginPassword, User registeredUser)
    {
        return loginPassword == registeredUser.Password;
    }

    public async Task<Room[]> GetRoomsAsync(long userId)
    {
        var loginUserId = userId;
        var userRooms = await Global.EntityAccessor.FetchRoomMembersWhereAsync(roomMember => roomMember.UserId == loginUserId);

        if (userRooms.Length == 0)
        {
            this.Client.OnOnlyMessage("参加しているルームがありません。" );
        }

        //Console.WriteLine("ルーム一覧");
        List<Room> rooms = new();
        foreach (var userRoom in userRooms)
        {
            var room = await Global.EntityAccessor.FetchRoomsWhereAsync(r => r.RoomId == userRoom.RoomId);
            if (room.Length == 1)
            {
                rooms.AddRange(room);
            }
        }

        return rooms.ToArray();
    }

    public async Task<Message[]> GetMessagesAsync(long roomId)
    {
        var messages = await Global.EntityAccessor.FetchMessagesWhereAsync(m => m.RoomId == roomId);
        return messages;
    }



    public async ValueTask JoinAsync(string roomName, string userName)
    {
        //this.group = await Group.AddAsync(roomName);
        //this.userName = userName;
        //this.group.All.OnJoin(userName);

        //this.Client.OnMessage("System", $"Welcome, hello {userName}!");
        //this.group.All.OnOnlyMessage("Hello, world!");
    }


    public async ValueTask LeaveAsync()
    {
        //this.group.All.OnLeave(ConnectionId.ToString());
        //await this.group.RemoveAsync(Context);
    }

    public async ValueTask SendMessageAsync(long roomId, long userId, string message)
    {
        Message messageLog = new()
        {
            RoomId = roomId,
            SendUserId = userId,
            Content = message,
            SendDateTime = DateTime.Now,
            Edited = false,
            Pinned = false,
            MessagePriority = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Deleted = false
        };

        var roomMembers = await Global.EntityAccessor.FetchRoomMembersWhereAsync(r => r.RoomId == roomId);
        var roomUserIds = roomMembers.Select(r => r.UserId);
        this.group.SendMessageToOnly(message, roomUserIds);


        var messageLogs = new List<Message>();
        messageLogs.Add(messageLog);
        await Global.EntityAccessor.UpsertMessagesAsync(messageLogs);
    }


    public async Task EchoAsync(string message)
    {
        this.Client.OnOnlyMessage("Echo: " + message);
    }


    public async Task<Room[]> selectRoom(string roomName)
    {
        var rooms = await Global.EntityAccessor.FetchRoomsWhereAsync(r => r.RoomName == roomName);
        return rooms;
    }
}