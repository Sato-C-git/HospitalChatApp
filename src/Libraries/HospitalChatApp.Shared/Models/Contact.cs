namespace HospitalChatApp.Shared.Models;

/// <summary>
/// 連絡先の管理
/// </summary>
public class Contact
{
    /// <summary>
    /// 連絡先ID
    /// </summary>
    public long ContactId { get; set; }

    /// <summary>
    /// 連絡先を所有するユーザーID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 連絡先のユーザーID
    /// </summary>
    public long ContactUserId { get; set; }

    /// <summary>
    /// 表示名
    /// </summary>
    public string DisplayName { get; set; }

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