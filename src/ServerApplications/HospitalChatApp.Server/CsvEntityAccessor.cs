using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using HospitalChatApp.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using AttachedFile = HospitalChatApp.Shared.Models.AttachedFile;

namespace HospitalChatApp.Server;

public class CsvEntityAccessor : IEntityAccessor
{
    private string _chatUsersCsvPath;
    private string _chatFilesCsvPath;
    private string _chatRoomsCsvPath;
    private string _chatRoomMembersCsvPath;
    private string _chatMessagesCsvPath;
    private string _chatMessageReadStatusesCsvPath;
    private string _chatContactsCsvPath;

    //イニシャライズを作って初期に必要な処理を一括で行う。
    public async Task InitializeAsync()
    {
        //Resourcesの呼出し
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        var csvFilePath = config["ResourceFolderRelativePath"];

        //exeの入ったフォルダの場所のパスを取得して絶対パスに変更
        var exePath = Assembly.GetEntryAssembly()?.Location;
        var exeDirectory = Path.GetDirectoryName(exePath);

        var folderPath = Path.Combine(exeDirectory, csvFilePath);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        this._chatUsersCsvPath = Path.Combine(folderPath, "ChatUsers.csv");
        this._chatFilesCsvPath = Path.Combine(folderPath, "ChatAttachedFiles.csv");
        this._chatRoomsCsvPath = Path.Combine(folderPath, "ChatRooms.csv");
        this._chatRoomMembersCsvPath = Path.Combine(folderPath, "ChatRoomMembers.csv");
        this._chatMessagesCsvPath = Path.Combine(folderPath, "ChatMessages.csv");
        this._chatMessageReadStatusesCsvPath = Path.Combine(folderPath, "ChatMessagesReadStatuses.csv");
        this._chatContactsCsvPath = Path.Combine(folderPath, "ChatContacts.csv");

        await this.CreateCsvFileAsync(typeof(User), this._chatUsersCsvPath);
        await this.CreateCsvFileAsync(typeof(AttachedFile), _chatFilesCsvPath);
        await this.CreateCsvFileAsync(typeof(Room), _chatRoomsCsvPath);
        await this.CreateCsvFileAsync(typeof(RoomMember), _chatRoomMembersCsvPath);
        await this.CreateCsvFileAsync(typeof(Message), _chatMessagesCsvPath);
        await this.CreateCsvFileAsync(typeof(MessageReadStatus), _chatMessageReadStatusesCsvPath);
        await this.CreateCsvFileAsync(typeof(Contact), _chatContactsCsvPath);
    }

    private async Task CreateCsvFileAsync(Type entityType, string fullPath)
    {
        if (File.Exists(fullPath))
        {
            return;
        }
        // ファイルを作成
        await using var fileStream = File.Create(fullPath);
        await using var writer = new StreamWriter(fullPath);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        csv.WriteHeader(entityType);


    }



    //ロードのイメージ。引数にほしい情報の条件にあったものだけをもってくる
    public async Task<User[]> FetchUsersWhereAsync(Func<User, bool>? predicate = null)
    {
        //条件などにあうUserの中配列やリストにして外に出す.
        //csvファイルを読み込んで、返却値として返す。
        using var userReader = new StreamReader(_chatUsersCsvPath);
        using var userCsv = new CsvReader(userReader, CultureInfo.InvariantCulture);
        var users = userCsv.GetRecordsAsync<User>();
        return await (predicate == null ? users.ToArrayAsync() : users.Where(predicate).ToArrayAsync());
    }

    public async Task UpsertUsersAsync(IEnumerable<User> users)
    {
        //Insert処理しかないので、update処理を図を権分岐でデータがない(null)→Insert
        //exeの入ったフォルダの場所のパスを取得して絶対パスに変更
        //fetchを呼び出す
        List<User> existingUsers;
        using var userReader = new StreamReader(_chatUsersCsvPath);
        using (var userCsv = new CsvReader(userReader, CultureInfo.InvariantCulture))
        {
            existingUsers = await userCsv.GetRecordsAsync<User>().ToListAsync();
        }
        ;

        foreach (var user in users)
        {
            var index = existingUsers.FindIndex(u => u.UserId == user.UserId);
            if (index != -1)
            {
                //update data
                //existingUsersからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingUsers[index] = user;
            }
            else
            {
                //add new data
                existingUsers.Add(user);
            }
        }
        await using var writer = new StreamWriter(_chatUsersCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingUsers);


    }

    public async Task<Message[]> FetchMessagesWhereAsync(Func<Message, bool>? predicate = null)
    {
        using var messageReader = new StreamReader(_chatMessagesCsvPath);
        using var messageCsv = new CsvReader(messageReader, CultureInfo.InvariantCulture);
        var messages = messageCsv.GetRecordsAsync<Message>();
        return await(predicate == null ? messages.ToArrayAsync() : messages.Where(predicate).ToArrayAsync());
    }

    public async Task UpsertMessagesAsync(IEnumerable<Message> messages)
    {
        List<Message> existingMessages;
        using var messageReader = new StreamReader(_chatMessagesCsvPath);
        using (var messageCsv = new CsvReader(messageReader, CultureInfo.InvariantCulture))
        {
            existingMessages = await messageCsv.GetRecordsAsync<Message>().ToListAsync();
        }
        ;

        foreach (var message in messages)
        {
            var index = existingMessages.FindIndex(m => m.MessageId == message.MessageId);
            if (index != -1)
            {
                //existingMessageからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingMessages[index] = message;
            }
            else
            {
                //add new data
                existingMessages.Add(message);
            }
        }
        await using var writer = new StreamWriter(_chatMessagesCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingMessages);
    }

    public async Task<Room[]> FetchRoomsWhereAsync(Func<Room, bool>? predicate = null)
    {
        using var roomReader = new StreamReader(_chatRoomsCsvPath);
        using var roomCsv = new CsvReader(roomReader, CultureInfo.InvariantCulture);
        var rooms = roomCsv.GetRecordsAsync<Room>();
        return await(predicate == null ? rooms.ToArrayAsync() : rooms.Where(predicate).ToArrayAsync());
    }

    public async Task UpsertRoomsAsync(IEnumerable<Room> rooms)
    {
        List<Room> existingRooms
            ;
        using var roomReader = new StreamReader(_chatRoomsCsvPath);
        using (var roomCsv = new CsvReader(roomReader, CultureInfo.InvariantCulture))
        {
            existingRooms = await roomCsv.GetRecordsAsync<Room>().ToListAsync();
        }
        ;

        foreach (var room in rooms)
        {
            var index = existingRooms.FindIndex(r => r.RoomId ==room.RoomId);
            if (index != -1)
            {
                //existingRoomからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingRooms[index] = room;
            }
            else
            {
                //add new data
                existingRooms.Add(room);
            }
        }
        await using var writer = new StreamWriter(_chatRoomsCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingRooms);
    }

    public async Task<AttachedFile[]> FetchAttachedFilesWhereAsync(Func<AttachedFile, bool>? predicate = null)
    {
        using var attachedFileReader = new StreamReader(_chatFilesCsvPath);
        using var attachedFileCsv = new CsvReader(attachedFileReader, CultureInfo.InvariantCulture);
        var attachedFiles = attachedFileCsv.GetRecordsAsync<AttachedFile>();
        return await(predicate == null ? attachedFiles.ToArrayAsync() : attachedFiles.Where(predicate).ToArrayAsync());
    }

    public async Task UpsertAttachedFilesAsync(IEnumerable<AttachedFile> attachedFiles)
    {
        List<AttachedFile> existingAttachedFiles
            ;
        using var attachedFileReader = new StreamReader(_chatRoomsCsvPath);
        using (var attachedFIleCsv = new CsvReader(attachedFileReader, CultureInfo.InvariantCulture))
        {
            existingAttachedFiles = await attachedFIleCsv.GetRecordsAsync<AttachedFile>().ToListAsync();
        }
        ;

        foreach (var attachedFile in attachedFiles)
        {
            var index = existingAttachedFiles.FindIndex(f => f.FileId == attachedFile.FileId);
            if (index != -1)
            {
                //existingAttachedFileからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingAttachedFiles[index] = attachedFile;
            }
            else
            {
                //add new data
                existingAttachedFiles.Add(attachedFile);
            }
        }
        await using var writer = new StreamWriter(_chatFilesCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingAttachedFiles);
    }

    public async Task<RoomMember[]> FetchRoomMembersWhereAsync(Func<RoomMember, bool>? predicate = null)
    {
        using var reader = new StreamReader(_chatRoomMembersCsvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var roomMembers = csv.GetRecordsAsync<RoomMember>();
        return await(predicate == null ? roomMembers.ToArrayAsync() : roomMembers.Where(predicate).ToArrayAsync());
    }

    public async Task UpsertRoomMembersAsync(IEnumerable<RoomMember> roomMembers)
    {
        List<RoomMember> existingRoomMembers;
        using var roomMemberReader = new StreamReader(_chatRoomMembersCsvPath);
        using (var roomMemberCsv = new CsvReader(roomMemberReader, CultureInfo.InvariantCulture))
        {
            existingRoomMembers = await roomMemberCsv.GetRecordsAsync<RoomMember>().ToListAsync();
        }
        ;

        foreach (var roomMember in roomMembers)
        {
            var index = existingRoomMembers.FindIndex(rm => rm.RoomMemberId == roomMember.RoomMemberId);
            if (index != -1)
            {
                //existingRoomMemberからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingRoomMembers[index] = roomMember;
            }
            else
            {
                //add new data
                existingRoomMembers.Add(roomMember);
            }
        }
        await using var writer = new StreamWriter(_chatRoomMembersCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingRoomMembers);
    }

    public async Task<Contact[]> FetchContactsWhereAsync(Func<Contact, bool>? predicate = null)
    {
        using var contactReader = new StreamReader(_chatContactsCsvPath);
        using var contactCsv = new CsvReader(contactReader, CultureInfo.InvariantCulture);
        var contacts = contactCsv.GetRecordsAsync<Contact>();
        return await(predicate == null ? contacts.ToArrayAsync() : contacts.Where(predicate).ToArrayAsync());
    }

    public async Task UpsertContactsAsync(IEnumerable<Contact> contacts)
    {
        List<Contact> existingContacts;
        using var reader = new StreamReader(_chatContactsCsvPath);
        using (var contactCsv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            existingContacts = await contactCsv.GetRecordsAsync<Contact>().ToListAsync();
        }
        ;

        foreach (var contact in contacts)
        {
            var index = existingContacts.FindIndex(c => c.ContactId == contact.ContactId);
            if (index != -1)
            {
                //existingRoomMemberからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingContacts[index] = contact;
            }
            else
            {
                //add new data
                existingContacts.Add(contact);
            }
        }
        await using var writer = new StreamWriter(_chatContactsCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingContacts);
    }

    public async Task<MessageReadStatus[]> FetchMessageReadStatusesWhereAsync(Func<MessageReadStatus, bool>? predicate = null)
    {
        using var statusReader = new StreamReader(_chatMessageReadStatusesCsvPath);
        using var readStatusCsv = new CsvReader(statusReader, CultureInfo.InvariantCulture);
        var readStatuses = readStatusCsv.GetRecordsAsync<MessageReadStatus>();
        return await(predicate == null ? readStatuses.ToArrayAsync() : readStatuses.Where(predicate).ToArrayAsync());
    }


    public async Task UpsertMessageReadStatusesAsync(IEnumerable<MessageReadStatus> messageReadStatuses)
    {
        List<MessageReadStatus> existingReadStatus;
        using var reader = new StreamReader(_chatMessageReadStatusesCsvPath);
        using (var readStatusCsv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            existingReadStatus = await readStatusCsv.GetRecordsAsync<MessageReadStatus>().ToListAsync();
        }
        ;

        foreach (var messageReadStatus in messageReadStatuses)
        {
            var index = existingReadStatus.FindIndex(s => s.ReadStatusId == messageReadStatus.ReadStatusId);
            if (index != -1)
            {
                //existingReadStatusからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingReadStatus[index] = messageReadStatus;
            }
            else
            {
                //add new data
                existingReadStatus.Add(messageReadStatus);
            }
        }
        await using var writer = new StreamWriter(_chatMessageReadStatusesCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingReadStatus);
    }
}