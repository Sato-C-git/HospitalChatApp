using MagicOnion;

namespace HospitalChatApp.Shared.Interfaces;

public interface IHospitalChatHub : IStreamingHub<IHospitalChatHub, IHospitalChatHubReceiver>
{
    ValueTask JoinAsync(string roomName, string userName);
    ValueTask LeaveAsync();
    ValueTask SendMessageAsync(string message);
}