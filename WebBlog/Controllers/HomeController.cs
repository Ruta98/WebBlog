using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Models;


namespace WebBlog.Controllers
{
    public class ID
            {
public int Id { get; set; }
}

    public class resalt
    {
        public string State = "Ok";
    }
public class HomeController : Controller
    {
        DataSQL Db = new DataSQL();
        public IActionResult Index()
        {       
            return View(@"wwwroot/index.html");
        }

        [Route("Login")]
        [HttpPost]
        public JsonResult Login([FromBody] User parUser)
        {
            //r.DeleteArticle(Id.Id);///Convert.ToInt32(Id));
            var Res = -3;
            if (parUser != null )
            {
                Res = Db.IsLogin(parUser);                
            }
            return Json(Res);
        }

        [Route("AddUser")]
        [HttpPost]
        public JsonResult AddUser([FromBody] User parUser)//
        {
            var Res = Db.AddUser(parUser);
            return Json(Res);
        }

        [Route("ListArticle")]
        [HttpPost]
        public JsonResult ListArticle()//
        {
            var Res = Db.GetArticles();
            return Json(Res);
        }
        [Route("ListAddArticle")]
        [HttpPost]
        public JsonResult ListAddArticle([FromBody] Article parArticle)//
        {
            var Res = Db.GetArticles(parArticle.Id);
            return Json(Res);
        }

        [Route("AddArticle")]
        [HttpPost]
        public JsonResult AddArticle([FromBody] Article parArticle)//
        {
            var Res = Db.UpdateArticle(parArticle);
            return Json(new resalt { State = (Res ? "Ok" : "Error") }); 
        }

        
        [Route("UpdateArticle")]
        [HttpPost]
        public JsonResult UpdateArticle([FromBody] Article parArticle)//
        {
            var Res = Db.UpdateArticle(parArticle);
            return Json(new resalt { State=(Res? "Ok" : "Error") });
        }


        [Route("DeleteArticle")]
        [HttpPost]
        public JsonResult DeleteArticle([FromBody] ID Id)
        {
            bool Res = false;
            if (Id != null)
            {
                var r = new DataSQL();
                Res=r.DeleteArticle(Id.Id);

            }
            return Json(new resalt { State = (Res ? "Ok" : "Error") });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
