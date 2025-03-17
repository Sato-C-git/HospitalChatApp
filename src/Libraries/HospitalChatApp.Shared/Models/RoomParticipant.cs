namespace HospitalChatApp.Shared.Models;

/// <summary>
/// ルーム参加者の管理
/// </summary>
public class RoomParticipant
{
    /// <summary>
    /// ルーム参加者の管理ID
    /// </summary>
    public long RoomParticipantId { get; set; }

    /// <summary>
    /// 参加者が参加しているルームID
    /// </summary>
    public long RoomId { get; set; }

    /// <summary>
    /// 参加者のユーザーID
    /// </summary>
    public long UserID { get; set; }


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