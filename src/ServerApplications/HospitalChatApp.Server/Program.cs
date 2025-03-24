using System.Xml.Serialization;using CsvHelper;
using HospitalChatApp.Server;
using HospitalChatApp.Shared.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", async () =>
{
    var users = new List<User>();
    users.Add(new()
    {
        UserId = 10000,
        UserName = "Sato",
    });
    users.Add(new ()
    {
        UserId = 376964,
        UserName = "Fuziwara"
    }
        );
    await Global.EntityAccessor.UpsertUsersAsync(users);
});

var accessor = new CsvEntityAccessor();
await accessor.InitializeAsync();
Global.EntityAccessor = accessor;
app.Run();



