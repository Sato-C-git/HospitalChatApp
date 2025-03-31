

// See https://aka.ms/new-console-template for more information

using System.Text;
using Grpc.Net.Client;
using HospitalChatApp.Console;
using HospitalChatApp.Shared.Interfaces;
using MagicOnion.Client;


var channel = GrpcChannel.ForAddress("https://localhost:7192");
var receiver = new HospitalChatHubReceiver();
var client = await StreamingHubClient.ConnectAsync<IHospitalChatHub, IHospitalChatHubReceiver>(channel, receiver);

//ログインIDとPA巣ワード入力→サーバーで受け取ってサーバーのリソースと照合→結果はbool(true→ルーム一覧, false→

Console.WriteLine("IDを入力してください。");
var loginId = Console.ReadLine();
Console.WriteLine("パスワードを入力してください.");
var password = Console.ReadLine();
await client.LoginAsync(loginId, password);
var rooms = await client.GetRoomsAsync(Global.LoginUser.UserId);
var messages = await client.GetMessagesAsync(1);

var sb = new StringBuilder();
foreach (var message in messages)
{
    sb.AppendLine(message.Content);
}
Console.WriteLine(sb);



await client.SendMessageAsync("Hello, world!");

while (true)
{
    await Task.Delay(1000);
}