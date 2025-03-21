using System.Xml.Serialization;
using HospitalChatApp.Server;
using HospitalChatApp.Shared.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", async () =>
{
    var accessor = new CsvEntityAccessor();
    var users = new List<User>();
    users.Add(new()
    {
        UserId = 10000,
        UserName = "Sato",
    });
    //await accessor.InitializeAsync();
    await accessor.FetchUsersWhereAsync();
    
});

app.Run();



