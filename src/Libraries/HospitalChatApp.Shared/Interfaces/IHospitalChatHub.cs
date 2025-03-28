﻿using HospitalChatApp.Shared.Models;
using MagicOnion;

namespace HospitalChatApp.Shared.Interfaces;

public interface IHospitalChatHub : IStreamingHub<IHospitalChatHub, IHospitalChatHubReceiver>
{
    Task LoginAsync(string loginId, string password);
    ValueTask JoinAsync(string roomName, string userName);
    ValueTask LeaveAsync();
    ValueTask SendMessageAsync(string message);

    Task GetRoomsAsync(string roomId, string roomName);

    Task JoinRoomAsync(string roomName);
}