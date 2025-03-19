namespace HospitalChatApp.Shared.Models;

/// <summary>
/// 既読の管理
/// </summary>
public class MessageReadStatus
{
    /// <summary>
    /// 既読ID
    /// </summary>
    public long ReadStatusId { get; set; }

    /// <summary>
    /// メッセージID
    /// </summary>
    public long MessageId { get; set; }

    /// <summary>
    /// ユーザーID
    /// </summary>
    public long UserId { get; set; }

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