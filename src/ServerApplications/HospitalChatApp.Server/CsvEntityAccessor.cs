using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using HospitalChatApp.Shared.Models;
using AttachedFile = HospitalChatApp.Shared.Models.AttachedFile;

namespace HospitalChatApp.Server;

public class CsvEntityAccessor: IEntityAccessor
{
    private string _chatUsersCsvPath;

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
        await this.CreateCsvFileAsync(typeof(User), this._chatUsersCsvPath);
        await this.CreateCsvFileAsync(typeof(AttachedFile), Path.Combine(folderPath, "ChatAttachedFiles.csv"));
        await this.CreateCsvFileAsync(typeof(Room), Path.Combine(folderPath, "ChatRooms.csv"));
        await this.CreateCsvFileAsync(typeof(RoomMember), Path.Combine(folderPath, "ChatRoomMembers.csv"));
        await this.CreateCsvFileAsync(typeof(Message), Path.Combine(folderPath, "ChatMessages.csv"));
        await this.CreateCsvFileAsync(typeof(MessageReadStatus), Path.Combine(folderPath, "ChatMessagesReadStatuses.csv"));
        await this.CreateCsvFileAsync(typeof(Contact), Path.Combine(folderPath, "ChatContacts.csv"));
    }

    private async Task CreateCsvFileAsync(Type entityType, string fullPath)
    {
        if (!File.Exists(fullPath))
        {
            // ファイルを作成
            await using var fileStream = File.Create(fullPath);
        }
        await using var writer = new StreamWriter(fullPath);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        csv.WriteHeader(entityType);
    }


    //ロードのイメージ。引数にほしい情報の条件にあったものだけをもってくる
    public async Task<User[]> FetchUsersWhereAsync(Func<User, bool>? predicate = null)
    {
        var userInitialize = new CsvEntityAccessor();
        //条件などにあうUserの中配列やリストにして外に出す
        //csvファイルを読み込んで、返却値として返す。
        using var userReader = new StreamReader(_chatUsersCsvPath);
        using var userCsv = new CsvReader(userReader, CultureInfo.InvariantCulture);
        {
            var users = await Task.Run(() => userCsv.GetRecords<User>().ToList());
            return predicate == null ? users.ToArray() : users.Where(predicate).ToArray();
        }
    }

    public async Task UpsertUsersAsync(IEnumerable<User> users)
    {
        //Insert処理しかないので、update処理を図王権分岐で追加データの中身がnull→Insert
        //exeの入ったフォルダの場所のパスを取得して絶対パスに変更
        var exePath = Assembly.GetEntryAssembly()?.Location;
        if (exePath == null)
        {
            return;
        }
        var exeDirectory = Path.GetDirectoryName(exePath);
        if (exeDirectory == null)
        {
            return;
        }

        var fileName = "ChatUsers.csv";
        var folderPath = Path.Combine(exeDirectory,"Resources");


        var fullPath = Path.Combine(folderPath, fileName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        if (!System.IO.File.Exists(fullPath))
        {
            // ファイルを作成
            await using (System.IO.File.Create(fullPath))
            {
                Console.WriteLine("ファイルが存在しなかったため、新規作成しました。");
            }
        }
        await using var writer = new StreamWriter(fullPath, true);
        await using var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
        csv.WriteHeader<User>(); //
        //NextRecord:CSVデータの次の行に移動するために使用する。
        await csv.NextRecordAsync();
        await csv.WriteRecordsAsync(users);
    }

    public Task<Message[]> FetchMessagesWhereAsync(Func<Message, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertMessagesAsync(IEnumerable<Message> messages)
    {
        throw new NotImplementedException();
    }

    public Task<Room[]> FetchRoomsWhereAsync(Func<Room, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertRoomsAsync(IEnumerable<Room> rooms)
    {
        throw new NotImplementedException();
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