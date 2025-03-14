using Grpc.Net.Client;
using MagicOnion.Client;
using MagicOnionSample.Shared.Services;
using MagicOnionSampleStreamingHub.Client;
using IChatHubReceiver = MagicOnionSample.Shared.Services.IChatHubReceiver;


var channel = GrpcChannel.ForAddress("https://localhost:5001");
var receiver = new ChatHubReceiver();
var client = await StreamingHubClient.ConnectAsync<IChatHub, IChatHubReceiver>(channel, receiver);

await client.JoinAsync("room", "user1");
await client.SendMessageAsync("Hello, world!");

while (true)
{
    await Task.Delay(1000);
}
