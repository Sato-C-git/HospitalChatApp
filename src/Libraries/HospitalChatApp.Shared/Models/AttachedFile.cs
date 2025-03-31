using MessagePack;

namespace HospitalChatApp.Shared.Models;

/// <summary>
/// ファイルの管理
/// </summary>
[MessagePackObject]
public class AttachedFile
{
    /// <summary>
    /// ファイルID
    /// </summary>
    [Key(0)]
    public long FileId { get; set; }

    /// <summary>
    /// ファイル名
    /// </summary>
    [Key(1)]
    public string FileName { get; set; }

    /// <summary>
    /// ファイル形式
    /// </summary>
    [Key(2)]
    public string FileExtension { get; set; }

    /// <summary>
    /// ファイルサイズ
    /// </summary>
    [Key(3)]
    public int FileSize { get; set; }

    /// <summary>
    /// ファイルのアップロード日時
    /// </summary>
    [Key(4)]
    public DateTime UploadedAt { get; set; }

    /// <summary>
    /// アップロードしたユーザーID
    /// </summary>
    [Key(5)]
    public long UploadUserId { get; set; }

    /// <summary>
    /// 添付されたメッセージID
    /// </summary>
    [Key(6)]
    public long MessageId { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    [Key(7)]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 最終更新日時
    /// </summary>
    [Key(8)]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 削除されたか
    /// </summary>
    [Key(9)]
    public bool Deleted { get; set; }
}