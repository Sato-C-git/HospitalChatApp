using HospitalChatApp.Shared.Models;
using MagicOnion;

namespace HospitalChatApp.Shared.Interfaces;

public interface IHospitalChatHub : IStreamingHub<IHospitalChatHub, IHospitalChatHubReceiver>
{
    Task LoginAsync(string loginId, string password);
    ValueTask JoinAsync(string roomName, string userName);
    ValueTask LeaveAsync();
    ValueTask SendMessageAsync(long roomId, long userId, string message);

    Task<Room[]> GetRoomsAsync(long userId);

    //ToDo:ユーザーの情報も返す
    Task<Message[]> GetMessagesAsync(long roomId);

    Task<Room[]> selectRoom(string roomName);

}