using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Hosting;


namespace LeiChia.Models;

public class Fruit
{
    [Key]
    public int Id { get; set; }
    [DisplayName("價格")]
    public int Price { get; set; }
    //[StringLength(100)]
    [DisplayName("名稱")]
    public string Name { get; set; }

    [DisplayName("描述")]
    [Required(ErrorMessage = "描述是必填欄位")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "描述必須介於10至200個字元之間")]
    public string? Description { get; set; }

    [DisplayName("庫存量")]
    [Required(ErrorMessage = "庫存量是必填欄位")]
    [Range(0, 1000, ErrorMessage = "庫存量必須介於0至1000之間")]
    public int Stock { get; set; }

    [DisplayName("圖片")]
    [Required(ErrorMessage = "圖片是必填欄位")]
    [Url(ErrorMessage = "圖片必須是有效的URL")]
    public string? ImageUrl { get; set; }

    public int Quantity {get; set;}

    public ICollection<Detail> Detail { get; set; }
}

public class Member
{
    [Key]
    public int Id { get; set; }
    [StringLength(10, ErrorMessage = "請輸入正確的電話格式")]
    [DisplayName("會員電話")]
    //[Range(1,10)]
    public string Phone { get; set; } //string 改
    [Required]
    [DisplayName("會員密碼")]
    [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,30}$", ErrorMessage = "密碼格式有誤")]
    public string Password { get; set; }
    [Required]
    [DisplayName("會員信箱")]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
    public string Mail { get; set; }
    [Required]
    [DisplayName("會員姓名")]
    public string Name { get; set; }
    public ICollection<Order>? Order { get; set; }
}

public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string Address { get; set; }

    public Member Member { get; set; }
    public ICollection<Detail> Detail { get; set; }
}

public class Detail
{
    [Key]
    public int Id { get; set; }
    public int Total { get; set; }
    public int Quantity { get; set; }

    public Fruit Fruit { get; set; }
    public Order Order { get; set; }
}
