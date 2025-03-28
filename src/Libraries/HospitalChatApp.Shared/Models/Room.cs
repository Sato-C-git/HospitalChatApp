using HospitalChatApp.Shared.Enums;
using MagicOnion;
using MessagePack;

namespace HospitalChatApp.Shared.Models;

/// <summary>
/// ルームの管理
/// </summary>

[MessagePackObject]
public class Room
{
    /// <summary>
    /// ルームID
    /// </summary>
    [Key(0)]
    public long RoomId { get; set; }

    /// <summary>
    /// ルームの名前
    /// </summary>
    [Key(1)]
    public string RoomName { get; set; }

    /// <summary>
    /// ルーム作成者ID
    /// </summary>
    [Key(2)]
    public long CreatedByUserId { get; set; }

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