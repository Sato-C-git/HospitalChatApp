namespace HospitalChatApp.Shared.Models;

/// <summary>
/// ファイルの管理
/// </summary>
public class File
{
    /// <summary>
    /// ファイルID
    /// </summary>
    public long FileId { get; set; }

    /// <summary>
    /// ファイル名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// ファイル形式
    /// </summary>
    public string FileExtension { get; set; }

    /// <summary>
    /// ファイルサイズ
    /// </summary>
    public int FileSize { get; set; }

    /// <summary>
    /// ファイルのアップロード日時
    /// </summary>
    public DateTime UploadedAt { get; set; }

    /// <summary>
    /// アップロードしたユーザーID
    /// </summary>
    public long UploadUserId { get; set; }

    /// <summary>
    /// 添付されたメッセージID
    /// </summary>
    public long MessageId { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 最終更新日時
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 削除されたか
    /// </summary>
    public bool Deleted { get; set; }
}