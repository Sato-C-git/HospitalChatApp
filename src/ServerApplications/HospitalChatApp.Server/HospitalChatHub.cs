using HospitalChatApp.Shared.Models;
using HospitalChatApp.Shared.Interfaces;
using MessagePack;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;
using MagicOnion.Serialization;
using System.Diagnostics.Contracts;

namespace HospitalChatApp.Server;

public class HospitalChatHub : StreamingHubBase<IHospitalChatHub, IHospitalChatHubReceiver>, IHospitalChatHub
{
    IGroup<IHospitalChatHubReceiver>? room;
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

        foreach (var user in longinIdStatus)
        {
            if (user.Password == loginPassword)
            {
                this.Client.OnLogin(user);
                return;
            }
        }
        this.Client.OnLoginFailed("Passwordが一致しません");
        return;
    }

    public async Task GetRoomsAsync(string roomName, string userId)
    {
        //var loginUser =
    }

    public async Task JoinRoomAsync(string roomNAme){}

    public async ValueTask JoinAsync(string roomName, string userName)
    {
        this.room = await Group.AddAsync(roomName);
        this.userName = userName;
        this.room.All.OnJoin(userName);

        this.Client.OnMessage("System", $"Welcome, hello {userName}!");
        this.room.All.OnOnlyMessage("Hello, world!");
    }


    public async ValueTask LeaveAsync()
    {
        this.room.All.OnLeave(ConnectionId.ToString());
        await this.room.RemoveAsync(Context);
    }

    public async ValueTask SendMessageAsync(string message)
    {
        this.room.All.OnMessage(userName, message);
    }

    public async Task EchoAsync(string message)
    {
        this.Client.OnOnlyMessage("Echo: " + message);
    }
}