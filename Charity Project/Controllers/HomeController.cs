using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Charity_Project.Database;

namespace Charity_Project.Controllers
{
    public class HomeController : Controller
    {
        Charity_DBEntities db = new Charity_DBEntities();
        public ActionResult Index()
        {
            return View(db.Stories_tbl.ToList());
        }

        public ActionResult CIndex()
        {
            return View();
        }

        public ActionResult SampleStory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stories_tbl stories_tbl = db.Stories_tbl.Find(id);
            if (stories_tbl == null)
            {
                return HttpNotFound();
            }
            return View(stories_tbl);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Users"] == null)
            {

                return View();

            }
            else
            {

                return RedirectToAction("Index", "Product");
            }
        }

        [HttpPost]
        public ActionResult Login(Login_tbl obj)
        {
            List<Login_tbl> li = new List<Login_tbl>();
            if (obj.Username != null && obj.Password != null)
            {


                if (BLL.LoginClass.userlogin(obj.Username, obj.Password))
                {

                    Session["Users"] = obj.Name;

                    var query = (from data in db.Login_tbl where data.Username == obj.Username select new { data.Name, data.Username , data.role});

                    foreach (var d in query)

                    {
                        Session["Users"] = d.Username.ToUpper();
                        Session["Name"] = d.Name;
                        Session["role"] = d.role;

                    }

                    //    TempData["Loggeduser"] = username;
                    //    TempData["Startname"] = DateTime.Now.ToString();
                    ViewBag.n = "Login Successfull...";

                    return RedirectToAction("Index", "Stories");

                    //  return PartialView();
                }
                else
                {

                    ViewBag.n = "Invalid Username or Password...";

                    return View();
                }
            }
            else
            {

                ViewBag.n = "Required All Feilds...";

                return View();
            }



        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}