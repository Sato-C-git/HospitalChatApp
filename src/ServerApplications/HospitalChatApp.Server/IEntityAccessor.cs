using System.ComponentModel.Design;
using HospitalChatApp.Shared.Models;
using File = HospitalChatApp.Shared.Models.File;

namespace HospitalChatApp.Server;

public interface IEntityAccessor
{
    Task<User[]> FetchUsersWhereAsync(Func<User,bool>? predicate= null);
    Task UpsertUsersAsync(IEnumerable<User> users);

    Task<Message[]> FetchMessagesWhereAsync(Func<Message, bool>? predicate =null);
    Task UpsertMessagesAsync(IEnumerable<Message> messages);

    Task<Room[]> FetchRoomsWhereAsync(Func<Room, bool>? predicate = null);
    Task UpsertRoomsAsync(IEnumerable<Room> rooms);

    Task<File[]> FetchFilesWhereAsync(Func<File, bool>? predicate=null);
    Task UpsertFilesAsync(IEnumerable<File> files);

    Task<RoomParticipant[]> FetchRoomParticipantsWhereAsync(Func<RoomParticipant, bool>? predicate=null);
    Task UpsertRoomParticipantsAsync(IEnumerable<RoomParticipant> roomParticipants);

    Task<ContactInformation[]> FetchContactInformationWhereAsync(Func<ContactInformation, bool>? predicate=null);
    Task UpsertContactInformationAsync(IEnumerable<ContactInformation> contactInformation);

    Task<ReadStatus[]> FetchReadStatusesWhereAsync(Func<ReadStatus, bool>? predicate=null);
    Task UpsertReadStatusesAsync(IEnumerable<ReadStatus> readStatuses);


}