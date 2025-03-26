using MagicOnion;
using System.Text.RegularExpressions;

namespace HospitalChatApp.Shared.Interfaces;

public interface IHospitalChatMyHub : IStreamingHub<IHospitalChatMyHub, IHospitalChatMyHubReceiver>
{
}