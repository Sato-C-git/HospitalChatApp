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
                //update user data
                //existingUserにuserの値を入れ替えたい
                //existingUSersからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingUsers[index] = user;
            }
            else
            {
                //add new user data
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
                //existingMssageからindexの場所を削除→同じ場所にインサート(indexで指定）
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
        List<Room> existingrooms
            ;
        using var roomReader = new StreamReader(_chatRoomsCsvPath);
        using (var roomCsv = new CsvReader(roomReader, CultureInfo.InvariantCulture))
        {
            existingrooms = await roomCsv.GetRecordsAsync<Room>().ToListAsync();
        }
        ;

        foreach (var room in rooms)
        {
            var index = existingrooms.FindIndex(r => r.RoomId ==room.RoomId);
            if (index != -1)
            {
                //existingroomからindexの場所を削除→同じ場所にインサート(indexで指定）
                existingrooms[index] = room;
            }
            else
            {
                //add new data
                existingrooms.Add(room);
            }
        }
        await using var writer = new StreamWriter(_chatRoomsCsvPath, false);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        await csv.WriteRecordsAsync(existingrooms);
    }

    public Task<AttachedFile[]> FetchAttachedFilesWhereAsync(Func<AttachedFile, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertAttachedFilesAsync(IEnumerable<AttachedFile> attachedFiles)
    {
        throw new NotImplementedException();
    }

    public Task<RoomMember[]> FetchRoomMembersWhereAsync(Func<RoomMember, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertRoomMembersAsync(IEnumerable<RoomMember> roomMembers)
    {
        throw new NotImplementedException();
    }

    public Task<Contact[]> FetchContactsWhereAsync(Func<Contact, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertContactsAsync(IEnumerable<Contact> contacts)
    {
        throw new NotImplementedException();
    }

    public Task<MessageReadStatus[]> FetchMessageReadStatusesWhereAsync(Func<MessageReadStatus, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertMessageReadStatusesAsync(IEnumerable<MessageReadStatus> messageReadStatuses)
    {
        throw new NotImplementedException();
    }
}