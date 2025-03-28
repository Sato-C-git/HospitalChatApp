using System.Collections;
using System.ComponentModel.Design;
using HospitalChatApp.Shared.Enums;
using MagicOnion;
using MessagePack;

namespace HospitalChatApp.Shared.Models;


/// <summary>
/// ユーザー情報
/// </summary>
[MessagePackObject]
public class User
{
    /// <summary>
    /// ユーザーID
    /// </summary>
    [Key(0)]
    public long UserId { get; set; }

    /// <summary>
    /// ログインID
    /// </summary>
    [Key(1)]
    public string LoginId { get; set; }

    /// <summary>
    /// ログインパスワード
    /// </summary>
    [Key(2)]
    public string Password { get; set; }

    /// <summary>
    /// 名前
    /// </summary>
    [Key(3)]
    public string UserName { get; set; }

    /// <summary>
    /// 生年月日
    /// </summary>
    [Key(4)]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// 性別
    /// </summary>
    [Key(5)]
    public SexType SexType { get; set; }

    /// <summary>
    /// 郵便番号
    /// </summary>
    [Key(6)]
    public int ZipCode { get; set; }

    /// <summary>
    /// 住所
    /// </summary>
    [Key(7)]
    public string Address { get; set; }

    /// <summary>
    /// 電話番号（固定）
    /// </summary>
    [Key(8)]
    public int Phone { get; set; }

    /// <summary>
    /// 電話番号（携帯）
    /// </summary>
    [Key(9)]
    public int Mobile { get; set; }

    /// <summary>
    /// emailアドレス
    /// </summary>
    [Key(10)]
    public string Email { get; set; }

    /// <summary>
    /// 職員番号
    /// </summary>
    [Key(11)]
    public string PersonalNumber { get; set; }

    /// <summary>
    /// 所属部署
    /// </summary>
    [Key(12)]
    public string DepartmentName { get; set; }

    /// <summary>
    /// 役職
    /// </summary>
    [Key(13)]
    public string PositionName { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    [Key(14)]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 最終更新日時
    /// </summary>
    [Key(15)]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 削除されたか
    /// </summary>
    [Key(16)]
    public bool Deleted { get; set; }
}