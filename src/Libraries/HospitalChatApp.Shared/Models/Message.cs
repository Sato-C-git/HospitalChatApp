using HospitalChatApp.Shared.Enums;

namespace HospitalChatApp.Shared.Models;

/// <summary>
/// メッセージの管理
/// </summary>
public class Message
{
    /// <summary>
    /// メッセージID
    /// </summary>
    public long MessageId { get; set; }

    /// <summary>
    /// ルームID
    /// </summary>
    public long RoomId { get; set; }

    /// <summary>
    /// 送信者ユーザーID
    /// </summary>
    public long SendUserId { get; set; }

    /// <summary>
    /// 送信日時
    /// </summary>
    public DateTime SendDateTime { get; set; }

    /// <summary>
    /// 編集されたか
    /// </summary>
    public bool Edited { get; set; }

    /// <summary>
    /// 固定されているか
    /// </summary>
    public bool Pinned { get; set; }

    /// <summary>
    /// 通知のフラグ
    /// </summary>
    public NotificationType NotificationType { get; set; }

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