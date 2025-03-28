

using HospitalChatApp.Shared.Interfaces;
using HospitalChatApp.Shared.Models;

namespace HospitalChatApp.Console;

public class HospitalChatHubReceiver : IHospitalChatHubReceiver
{
    public void OnJoin(string userName)
        => System.Console.WriteLine($"{userName} joined.");
    public void OnLeave(string userName)
        => System.Console.WriteLine($"{userName} left.");
    public void OnMessage(string userName, string message)
        => System.Console.WriteLine($"{userName}: {message}");

    public void OnOnlyMessage(string message)
        => System.Console.WriteLine($"{message}");

    public void OnLogin(User user)
    {
        System.Console.WriteLine($"{user.UserName}");
    }

    public void OnLoginFailed(string message)
    {
        System.Console.WriteLine(message);
    }




}