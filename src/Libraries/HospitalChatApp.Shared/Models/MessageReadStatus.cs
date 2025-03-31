using MessagePack;

namespace HospitalChatApp.Shared.Models;

/// <summary>
/// 既読の管理
/// </summary>
[MessagePackObject]
public class MessageReadStatus
{
    /// <summary>
    /// 既読ID
    /// </summary>
    [Key(0)]
    public long ReadStatusId { get; set; }

    /// <summary>
    /// メッセージID
    /// </summary>
    [Key(1)]
    public long MessageId { get; set; }

    /// <summary>
    /// ユーザーID
    /// </summary>
    [Key(2)]
    public long UserId { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    [Key(3)]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 最終更新日時
    /// </summary>
    [Key(4)]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 削除されたか
    /// </summary>
    [Key(5)]
    public bool Deleted { get; set; }

}