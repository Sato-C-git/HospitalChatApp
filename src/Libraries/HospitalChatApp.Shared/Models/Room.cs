using HospitalChatApp.Shared.Enums;

namespace HospitalChatApp.Shared.Models;

/// <summary>
/// ルームの管理
/// </summary>
public class Room
{
    /// <summary>
    /// ルームID
    /// </summary>
    public long RoomId { get; set; }

    /// <summary>
    /// ルームの名前
    /// </summary>
    public string RoomName { get; set; }

    /// <summary>
    /// ルーム作成者ID
    /// </summary>
    public long CreatedByUserId { get; set; }

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