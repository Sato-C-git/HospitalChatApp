using HospitalChatApp.Shared.Models;

namespace HospitalChatApp.Console;

public class Global
{
    public static User LoginUser { get; set; } = new();
    public static Room EnterRoom { get; set; } = new();
}