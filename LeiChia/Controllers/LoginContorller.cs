using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeiChia.Models;
using LeiChia.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Query;
using System.Text.Encodings.Web;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FRUITSHOP.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Member> objMemberList = _db.Member;
            return View(objMemberList);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Member obj)
        {
            if (ModelState.IsValid)
            {
                _db.Member.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Member obj)
        {
            var memberData = _db.Member.SingleOrDefault(u => u.Password == obj.Password);

            if (memberData == null)
            {
                NotFound();
                return View();
            }
            else if (obj.Password != memberData.Password)
            {
                NotFound();
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, obj.Phone),
                new Claim("FullName", memberData.Name),
                //new Claim("EmployeeId", memberData.Member_id.ToString()), 新增一個物件來判斷測試
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties
            {
                // 這裡可以自訂驗證選項...
                // 是否可自動更新 Cookie(時效?)
                //AllowRefresh = <bool>,
                // IsPersistent 設置 Persistent cookies，否則瀏覽器 session 關閉就失效
                IsPersistent = true,
                //AllowRefresh = true,
                // Persistent cookie 可進一步設置失效時間：
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //IssuedUtc = <DateTimeOffset>,
                //RedirectUri = <string>
            });

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Cart()
        {
            // 獲取cookie物件內資訊
            //var data = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //var employeeid = data.Where(u => u.Type == "EmployeeId").First().Value;
            //if (employeeid == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //return RedirectToAction("Index", "Product");
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Index", "Product");

        }
    }
}

