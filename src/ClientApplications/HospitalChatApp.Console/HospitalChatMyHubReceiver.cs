using HospitalChatApp.Shared.Interfaces;
using HospitalChatApp.Shared.Models;

namespace HospitalChatApp.Console;

public class HospitalChatMyHubReceiver : IHospitalChatMyHubReceiver
{
    public Task<string> HelloAsync(string name, int age)
    {
        System.Console.WriteLine($"Hello from {name} ({age})");
        var result = System.Console.ReadLine() ?? "";
        return Task.FromResult(result);
    }


    public void OnMessage(string message)
    {
        System.Console.WriteLine($"OnMessage: {message}");
    }

    public Guid ContextId { get; set; } = Guid.NewGuid();
}