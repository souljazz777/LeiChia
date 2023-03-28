using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Query;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using LeiChia.Data;
using LeiChia.Models;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpPost]
        //[ActionName("DefaultAction")]
        public IActionResult Create(int[] fruit)
        {
            var fruit1 = fruit[0];
            ViewData["message"] = fruit1;
            //ViewData["name"] = number;

            return RedirectToAction("Create", "Product");
        }
    }
}

