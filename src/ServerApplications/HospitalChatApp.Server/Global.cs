namespace HospitalChatApp.Server;

public static class Global
{
    public static IEntityAccessor EntityAccessor { get; set; }
    public static readonly string _chatUsersCsvFileName = "ChatUsers.csv";
}