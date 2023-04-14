using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Query;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using LeiChia.Data;
using LeiChia.Models;
using LeiChia.Helpers;
using System.Reflection.Metadata.Ecma335;

namespace FRUITSHOP.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<LeiChia.Models.Fruit> fruit = _db.Fruit;
            return View(fruit);
        }
        public IActionResult Cart()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                TempData["warning"] = "購物車是空的!";
                return RedirectToAction("Index", "Product");
            }
            return View(cart);
        }

        public IActionResult AddToCart(int id, int quantity)
        {
            //取得商品資料
            CartItem item = new CartItem
            {
                Fruit = _db.Fruit.Single(x => x.Id.Equals(id)),
                Amount = quantity,
                SubTotal = _db.Fruit.Single(m => m.Id == id).Price * quantity
            };
            
            //判斷 Session 內有無購物車
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                //如果沒有已存在購物車: 建立新的購物車
                cart = new List<CartItem>();
                cart.Add(item);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                //如果已存在購物車: 檢查有無相同的商品，有的話只調整數量
                int index = cart.FindIndex(m => m.Fruit.Id.Equals(id));
                if (index != -1)
                {
                    cart[index].Amount += item.Amount;
                    cart[index].SubTotal += item.SubTotal;
                }
                else
                {
                    cart.Add(item);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Cart", "Product");
        }
        public IActionResult RemoveItem(int id)
        {
            //向 Session 取得商品列表
            List<CartItem> cart = SessionHelper.
                GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            //用FindIndex查詢目標在List裡的位置
            int index = cart.FindIndex(m => m.Fruit.Id.Equals(id));
            cart.RemoveAt(index);

            if (cart.Count < 1)
            {
                SessionHelper.Remove(HttpContext.Session, "cart");
            }
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Cart", "Product");
        }
    }
}









