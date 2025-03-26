

// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using HospitalChatApp.Console;
using HospitalChatApp.Server;
using HospitalChatApp.Shared.Interfaces;
using MagicOnion.Client;

var channel = GrpcChannel.ForAddress("https://localhost:7192");
var receiver = new HospitalChatHubReceiver();
var client = await StreamingHubClient.ConnectAsync<IHospitalChatHub, IHospitalChatHubReceiver>(channel, receiver);

//ログインIDとPA巣ワード入力→サーバーで受け取ってサーバーのリソースと照合→結果はbool(true→ルーム一覧, false→
await client.JoinAsync("room", "user1");
await client.SendMessageAsync("Hello, world!");

while (true)
{
    await Task.Delay(1000);
}