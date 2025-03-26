namespace HospitalChatApp.Shared.Interfaces;

public interface IHospitalChatMyHubReceiver
{
    // The Client results method is defined in the Receiver with a return type of Task or Task<T>
    Task<string> HelloAsync(string name, int age);

    // Regular broadcast method
    void OnMessage(string message);

    // guID
    Guid ContextId { get; set; }
 }