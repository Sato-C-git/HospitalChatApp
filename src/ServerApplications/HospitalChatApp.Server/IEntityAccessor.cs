using System.ComponentModel.Design;
using HospitalChatApp.Shared.Models;
using AttachedFile = HospitalChatApp.Shared.Models.AttachedFile;

namespace HospitalChatApp.Server;

public interface IEntityAccessor
{
    Task<User[]> FetchUsersWhereAsync(Func<User,bool>? predicate= null);
    Task UpsertUsersAsync(IEnumerable<User> users);

    Task<Message[]> FetchMessagesWhereAsync(Func<Message, bool>? predicate =null);
    Task UpsertMessagesAsync(IEnumerable<Message> messages);

    Task<Room[]> FetchRoomsWhereAsync(Func<Room, bool>? predicate = null);
    Task UpsertRoomsAsync(IEnumerable<Room> rooms);

    Task<AttachedFile[]> FetchAttachedFilesWhereAsync(Func<AttachedFile, bool>? predicate=null);
    Task UpsertAttachedFilesAsync(IEnumerable<AttachedFile> attachedFiles);

    Task<RoomMember[]> FetchRoomMembersWhereAsync(Func<RoomMember, bool>? predicate=null);
    Task UpsertRoomMembersAsync(IEnumerable<RoomMember> roomMembers);

    Task<Contact[]> FetchContactsWhereAsync(Func<Contact, bool>? predicate=null);
    Task UpsertContactsAsync(IEnumerable<Contact> contacts);

    Task<MessageReadStatus[]> FetchMessageReadStatusesWhereAsync(Func<MessageReadStatus, bool>? predicate=null);
    Task UpsertMessageReadStatusesAsync(IEnumerable<MessageReadStatus> messageReadStatuses);


}