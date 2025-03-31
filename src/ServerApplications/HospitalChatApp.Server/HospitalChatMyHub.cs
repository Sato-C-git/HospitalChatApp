using HospitalChatApp.Shared.Interfaces;
using MagicOnion.Server.Hubs;

namespace HospitalChatApp.Server;

public class HospitalChatMyHub(GroupService groupService) : StreamingHubBase<IHospitalChatMyHub, IHospitalChatMyHubReceiver>, IHospitalChatMyHub
{
    protected override ValueTask OnConnected()
    {
        //groupService.AddMember(Client);
        return default;
    }

    protected override ValueTask OnDisconnected()
    {
        //groupService.RemoveMember(Client);
        return default;
    }

    public void SendMessage(string message) => groupService.SendMessageToAll(message);
}