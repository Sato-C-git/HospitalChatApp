namespace MagicOnionSample.Shared.Services;

public interface IChatHubReceiver
{
    void OnJoin(string userName);
    void OnLeave(string userName);
    void OnMessage(string userName, string message);
}