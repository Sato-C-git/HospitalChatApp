using MagicOnion;
using MessagePack;
namespace HospitalChatApp.Shared.Models;

/// <summary>
/// ルーム参加者の管理
/// </summary>
[MessagePackObject]
public class RoomMember
{
    /// <summary>
    /// ルーム参加者の管理ID
    /// </summary>
    [Key(0)]
    public long RoomMemberId { get; set; }

    /// <summary>
    /// 参加者が参加しているルームID
    /// </summary>
    [Key(1)]
    public long RoomId { get; set; }

    /// <summary>
    /// 参加者のユーザーID
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