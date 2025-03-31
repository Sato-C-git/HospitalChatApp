using Cysharp.Runtime.Multicast;
using HospitalChatApp.Shared.Interfaces;

namespace HospitalChatApp.Server;
/// <summary>
/// グループチャットのユーザー管理
/// </summary>
/// <param name="groupProvider"></param>
public class GroupService(IMulticastGroupProvider groupProvider) : IDisposable
{
    private readonly IMulticastSyncGroup<long, IHospitalChatHubReceiver> _group =
        groupProvider.GetOrAddSynchronousGroup<long, IHospitalChatHubReceiver>("HospitalGroup");

    public void SendMessageToAll(string message) => _group.All.OnOnlyMessage(message);

    public void SendMessageToOnly(string message, IEnumerable<long> ids) => _group.Only(ids).OnOnlyMessage(message);

    public void AddMember(IHospitalChatHubReceiver receiver, long userId) => _group.Add(userId ,receiver);
    public void RemoveMember(long userId) => _group.Remove(userId);


    public void Dispose() => _group.Dispose();
}


public class MyBackgroundService(GroupService groupService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(60));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            groupService.SendMessageToAll("Send message periodically...");
        }
    }
}