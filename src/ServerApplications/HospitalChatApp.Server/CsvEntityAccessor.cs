using System.Collections;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using HospitalChatApp.Shared.Models;
using File = HospitalChatApp.Shared.Models.File;

namespace HospitalChatApp.Server;

public class CsvEntityAccessor: IEntityAccessor
{
    public Task<User[]> FetchUsersWhereAsync(Func<User, bool>? predicate = null)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        var logfilePath = config["ResourceFolderPath"];
        Console.WriteLine($"ログのファイルパス：{logfilePath}");
        throw new NotImplementedException();
    }

    public async Task UpsertUsersAsync(IEnumerable<User> users)
    {

        var fileName = "ChatUsers.csv";
        var folderPath = "rsc";
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
        csv.WriteHeader<User>();
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

    public Task<File[]> FetchFilesWhereAsync(Func<File, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertFilesAsync(IEnumerable<File> files)
    {
        throw new NotImplementedException();
    }

    public Task<RoomParticipant[]> FetchRoomParticipantsWhereAsync(Func<RoomParticipant, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertRoomParticipantsAsync(IEnumerable<RoomParticipant> roomParticipants)
    {
        throw new NotImplementedException();
    }

    public Task<ContactInformation[]> FetchContactInformationWhereAsync(Func<ContactInformation, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertContactInformationAsync(IEnumerable<ContactInformation> contactInformation)
    {
        throw new NotImplementedException();
    }

    public Task<ReadStatus[]> FetchReadStatusesWhereAsync(Func<ReadStatus, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public Task UpsertReadStatusesAsync(IEnumerable<ReadStatus> readStatuses)
    {
        throw new NotImplementedException();
    }
}