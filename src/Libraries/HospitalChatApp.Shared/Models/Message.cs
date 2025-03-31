using HospitalChatApp.Shared.Enums;
using MessagePack;

namespace HospitalChatApp.Shared.Models;

/// <summary>
/// メッセージの管理
/// </summary>
[MessagePackObject]
public class Message
{
    /// <summary>
    /// メッセージID
    /// </summary>
    [Key(0)]
    public long MessageId { get; set; }

    /// <summary>
    /// ルームID
    /// </summary>
    [Key(1)]
    public long RoomId { get; set; }

    /// <summary>
    /// 送信者ユーザーID
    /// </summary>
    [Key(3)]
    public long SendUserId { get; set; }

    /// <summary>
    /// メッセージ内容
    /// </summary>
    [Key(2)]
    public string Content { get; set; }

    /// <summary>
    /// 送信日時
    /// </summary>
    [Key(4)]
    public DateTime SendDateTime { get; set; }

    /// <summary>
    /// 編集されたか
    /// </summary>
    [Key(5)]
    public bool Edited { get; set; }

    /// <summary>
    /// 固定されているか
    /// </summary>
    [Key(6)]
    public bool Pinned { get; set; }

    /// <summary>
    /// 通知のフラグ
    /// </summary>
    [Key(7)]
    public MessagePriority MessagePriority { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    [Key(8)]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 最終更新日時
    /// </summary>
    [Key(9)]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 削除されたか
    /// </summary>
    [Key(10)]
    public bool Deleted { get; set; }
}