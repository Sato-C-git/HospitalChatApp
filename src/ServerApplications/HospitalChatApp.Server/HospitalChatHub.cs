using HospitalChatApp.Shared.Models;
using HospitalChatApp.Shared.Interfaces;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;

namespace HospitalChatApp.Server;

public class HospitalChatHub : StreamingHubBase<IHospitalChatHub, IHospitalChatHubReceiver>, IHospitalChatHub
{
    IGroup<IHospitalChatHubReceiver>? room;
    string userName = "unknown";


    public async Task<User?> LoginAsync(string loginId, string loginPassWord)
    {
        await Global.EntityAccessor.FetchUsersWhereAsync(user => user.LoginId == loginId && user.PassWord == loginPassWord);

    }

    public async ValueTask JoinAsync(string roomName, string userName)
    {
        this.room = await Group.AddAsync(roomName);
        this.userName = userName;
        room.All.OnJoin(userName);

        Client.OnMessage("System", $"Welcome, hello {userName}!");
        this.room.All.OnOnlyMessage("Hello, world!");
    }


    public async ValueTask LeaveAsync()
    {
        room.All.OnLeave(ConnectionId.ToString());
        await this.room.RemoveAsync(Context);
    }

    public async ValueTask SendMessageAsync(string message)
    {
        room.All.OnMessage(userName, message);
    }

    public async Task EchoAsync(string message)
    {
        this.Client.OnOnlyMessage("Echo: " + message);
    }
}