
using MagicOnionSample.Shared.Services;

namespace MagicOnionSampleStreamingHub.Client;


class ChatHubReceiver : IChatHubReceiver
    {
        public void OnJoin(string userName)
            => Console.WriteLine($"{userName} joined.");
        public void OnLeave(string userName)
            => Console.WriteLine($"{userName} left.");
        public void OnMessage(string userName, string message)
            => Console.WriteLine($"{userName}: {message}");
    }