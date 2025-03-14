using Cysharp.Runtime.Multicast;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;
using MagicOnionSample.Shared.Services;

namespace MagicOnionSample.Server;


/// <summary>
/// Unary Serviceの実装
/// </summary>
public class MyFirstService : ServiceBase<IMyFirstService>, IMyFirstService
{
    // `UnaryResult<T>` allows the method to be treated as `async` method.
    public async UnaryResult<int> SumAsync(int x, int y)
    { 
        Console.WriteLine($"Received:{x}, {y}");
        return x + y;
    }
}


/// <summary>
/// StreamingHub Serviceの実装
/// </summary>
public class ChatHub : StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
{
    public IGroup<IChatHubReceiver>? room;
    string userName = "unknown";

    public async ValueTask JoinAsync(string roomName, string userName)
    {
        this.room = await Group.AddAsync(roomName);
        this.userName = userName;
        room.All.OnJoin(userName);
        // Send a message "Hello, world!" to all clients in the room
        this.room.All.OnMessage(userName, "Hello, world!");

        //this.room.Only([connectionId1, connectionId2]).OnMessage(userName, "Hello, world! to specific clients");

        this.room.Except(ConnectionId).OnMessage(userName, "Hello, world! except me");

        this.room.Single(ConnectionId).OnMessage(userName, "Hello, world! to me");
        
        Client.OnMessage("System", $"Welcome, hello {userName}!");
    }

    public async ValueTask LeaveAsync()
    {
        room.All.OnLeave(ConnectionId.ToString());
        await this.room.RemoveAsync(Context);
    }

    public async ValueTask SendMessageAsync(string message)
    {
        room.All.OnMessage(userName, message);
    }

    public async Task EchoAsync(string message)
    {
        this.Client.OnMessage(userName, "Echo: " + message);
    }
}

public class GroupService(IMulticastGroupProvider groupProvider) : IDisposable
{
    // NOTE: You can also manage multiple groups using a dictionary, etc.
    private readonly IMulticastSyncGroup<Guid, IChatHubReceiver> _group
        = groupProvider.GetOrAddSynchronousGroup<Guid, IChatHubReceiver>("MyGroup");

    public void Dispose() => _group.Dispose();
}




