using System.ComponentModel.Design;
using HospitalChatApp.Shared.Enums;

namespace HospitalChatApp.Shared.Models;


/// <summary>
/// ユーザー情報
/// </summary>
public class User
{
    /// <summary>
    /// ユーザーID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// ログインID
    /// </summary>
    public string LoginId { get; set; }

    /// <summary>
    /// ログインパスワード
    /// </summary>
    public string PassWord { get; set; }

    /// <summary>
    /// 名前
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 生年月日
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// 性別
    /// </summary>
    public SexType SexType { get; set; }

    /// <summary>
    /// 郵便番号
    /// </summary>
    public int ZipCode { get; set; }

    /// <summary>
    /// 住所
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 電話番号（固定）
    /// </summary>
    public int Phone { get; set; }

    /// <summary>
    /// 電話番号（携帯）
    /// </summary>
    public int Mobile { get; set; }

    /// <summary>
    /// emailアドレス
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 職員番号
    /// </summary>
    public string PersonalNumber { get; set; }

    /// <summary>
    /// 所属部署
    /// </summary>
    public string DepartmentName { get; set; }

    /// <summary>
    /// 役職
    /// </summary>
    public string PositionName { get; set; }

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