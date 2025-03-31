using MessagePack;

namespace HospitalChatApp.Shared.Models;

/// <summary>
/// 連絡先の管理
/// </summary>
[MessagePackObject]
public class Contact
{
    /// <summary>
    /// 連絡先ID
    /// </summary>
    [Key(0)]
    public long ContactId { get; set; }

    /// <summary>
    /// 連絡先を所有するユーザーID
    /// </summary>
    [Key(1)]
    public long UserId { get; set; }

    /// <summary>
    /// 連絡先のユーザーID
    /// </summary>
    [Key(2)]
    public long ContactUserId { get; set; }

    /// <summary>
    /// 表示名
    /// </summary>
    [Key(3)]
    public string DisplayName { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    [Key(4)]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 最終更新日時
    /// </summary>
    [Key(5)]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 削除されたか
    /// </summary>
    [Key(6)]
    public bool Deleted { get; set; }
}