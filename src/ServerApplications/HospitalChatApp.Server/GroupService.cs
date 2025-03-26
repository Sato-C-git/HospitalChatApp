using Cysharp.Runtime.Multicast;
using HospitalChatApp.Shared.Interfaces;

namespace HospitalChatApp.Server;

public class GroupService(IMulticastGroupProvider groupProvider) : IDisposable
{
    private readonly IMulticastSyncGroup<Guid, IHospitalChatHubReceiver> _group =
        groupProvider.GetOrAddSynchronousGroup<Guid, IHospitalChatHubReceiver>("HospitalGroup");

    public void Dispose() => _group.Dispose();
}