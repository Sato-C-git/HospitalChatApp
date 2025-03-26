using Cysharp.Runtime.Multicast;
using HospitalChatApp.Shared.Interfaces;

namespace HospitalChatApp.Server;
/// <summary>
/// グループチャットのユーザー管理
/// </summary>
/// <param name="groupProvider"></param>
public class GroupService(IMulticastGroupProvider groupProvider) : IDisposable
{
    private readonly IMulticastSyncGroup<Guid, IHospitalChatMyHubReceiver> _group =
        groupProvider.GetOrAddSynchronousGroup<Guid, IHospitalChatMyHubReceiver>("HospitalGroup");

    public void SendMessageToAll(string message) => _group.All.OnMessage(message);

    public void AddMember(IHospitalChatMyHubReceiver receiver) => _group.Add(receiver.ContextId, receiver);
    public void RemoveMember(IHospitalChatMyHubReceiver receiver) => _group.Remove(receiver.ContextId);


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