

// See https://aka.ms/new-console-template for more information

using System.Text;
using Grpc.Net.Client;
using HospitalChatApp.Console;
using HospitalChatApp.Shared.Interfaces;
using MagicOnion.Client;


var channel = GrpcChannel.ForAddress("https://localhost:7192");
var receiver = new HospitalChatHubReceiver();
var client = await StreamingHubClient.ConnectAsync<IHospitalChatHub, IHospitalChatHubReceiver>(channel, receiver);


Console.WriteLine("IDを入力してください。");
var loginId = Console.ReadLine();
Console.WriteLine("パスワードを入力してください.");
var password = Console.ReadLine();
await client.LoginAsync(loginId, password);

var rooms = await client.GetRoomsAsync(Global.LoginUser.UserId);
var stringBuilder = new StringBuilder();
foreach (var room in rooms)
{
    stringBuilder.AppendLine($"{room.RoomId} : {room.RoomName}");

}
Console.WriteLine(stringBuilder);

Console.WriteLine("ルームを選択：");
var roomId = Console.ReadLine();
long.TryParse(roomId, out var roomIdLong);


var messages = await client.GetMessagesAsync(roomIdLong);

var sb = new StringBuilder();
foreach (var message in messages)
{
    sb.AppendLine(message.Content);
}
Console.WriteLine(sb);

Console.WriteLine("メッセージ入力：");
var content = Console.ReadLine();

await client.SendMessageAsync(roomIdLong, Global.LoginUser.UserId, content);

while (true)
{
    await Task.Delay(1000);
}