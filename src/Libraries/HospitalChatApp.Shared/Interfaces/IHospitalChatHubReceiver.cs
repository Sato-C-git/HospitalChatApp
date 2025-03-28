using HospitalChatApp.Shared.Models;

namespace HospitalChatApp.Shared.Interfaces;

public interface IHospitalChatHubReceiver
{
    void OnJoin(string userName);
    void OnLeave(string userName);
    void OnMessage(string userName, string message);

    void OnOnlyMessage(string message);

    void OnLogin(User user);

    void OnLoginFailed(string message);
}