using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Charity_Project.Database;
using System.IO;

namespace Charity_Project.Controllers
{
    public class StoriesController : Controller
    {
        private Charity_DBEntities db = new Charity_DBEntities();

        // GET: Stories
        public ActionResult Index()
        {

            var role = Session["role"];

            if (Session["Users"] != null)
            {

                return View(db.Stories_tbl.ToList());

            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
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

        // GET: Stories/Create
        public ActionResult Create()
        {
            if (Session["Users"] != null)
            {

                return View();

            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,VideoLink,ImageLink,Description")] Stories_tbl stories_tbl, HttpPostedFileBase image, HttpPostedFileBase image1)
        {

            var allowedVideoExtensions = new[] {
            ".mp4", ".MPA", ".avi", "AVI" , "mkv" , "3gp" , "flv"
        };

            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg" , "PNG" 
        };

            Random r = new Random();
            int id = r.Next(1000, 9999);

            var fileName = Path.GetFileName(image.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext = Path.GetExtension(image.FileName); //getting the extension(ex-.jpg)  

            var VideoName = Path.GetFileName(image1.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext1 = Path.GetExtension(image1.FileName); //getting the extension(ex-.jpg)  

            if (allowedExtensions.Contains(ext) || allowedVideoExtensions.Contains(ext1)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  

                string Videoname1 = Path.GetFileNameWithoutExtension(VideoName);

                string myfile = name + "_" + id + ext; //appending the name with id  
                string myfile1 = Videoname1 + "_" + id + ext1;

                // store the file inside ~/project folder(Img)  
                var path = Path.Combine(Server.MapPath("~/Img"), myfile);

                var path1 = Path.Combine(Server.MapPath("~/Videos"), myfile1);
                //tbl.Image_url = path;
                //obj.tbl_details.Add(tbl);
                //obj.SaveChanges();
                image.SaveAs(path);
                image1.SaveAs(path1);

                if (ModelState.IsValid)
                {
                    stories_tbl.ImageLink = myfile;
                    stories_tbl.VideoLink = myfile1;
                    db.Stories_tbl.Add(stories_tbl);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }
            else
            {
                ViewBag.message = "Please choose Correct file";
            }


            return View(stories_tbl);
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, string Title, string VideoLink, HttpPostedFileBase image, HttpPostedFileBase image1 ,string Description, string ImageLink)
        {
            try
            {


                Stories_tbl Stories_tbl = new Stories_tbl();

                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg" , "PNG"
        };

                var allowedVideoExtensions = new[] {
            ".mp4", ".MPA", ".avi", "AVI" , "mkv" , "3gp" , "flv"
        };

                Random r = new Random();
                int id = r.Next(1000, 9999);
                if (image != null || image1 != null)
                {



                    //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(image.FileName); //getting the extension(ex-.jpg)  
                 
                    var ext1 = Path.GetExtension(image1.FileName); //getting the extension(ex-.jpg)  

                    if (allowedExtensions.Contains(ext) || allowedVideoExtensions.Contains(ext1)) //check what type of extension  
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + id + ext; //appending the name with id  
                                                               // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Img"), myfile);
                        //tbl.Image_url = path;
                        //obj.tbl_details.Add(tbl);
                        //obj.SaveChanges();

                        var VideoName = Path.GetFileName(image1.FileName);
                     
                        string Videoname1 = Path.GetFileNameWithoutExtension(VideoName);
                        string myfile1 = Videoname1 + "_" + id + ext1;
                        var path1 = Path.Combine(Server.MapPath("~/Videos"), myfile1);


                        image.SaveAs(path);
                        image1.SaveAs(path1);

                        if (ModelState.IsValid)
                        {
                            Stories_tbl.Id = Id;
                            Stories_tbl.Title = Title;
                            Stories_tbl.Description = Description;
                            Stories_tbl.ImageLink = myfile;
                            Stories_tbl.VideoLink = myfile1;
                            db.Entry(Stories_tbl).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        Stories_tbl.Id = Id;
                        Stories_tbl.Title = Title;
                        Stories_tbl.Description = Description;
                        Stories_tbl.ImageLink = ImageLink;
                        db.Entry(Stories_tbl).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(Stories_tbl);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
        //public ActionResult Edit([Bind(Include = "Id,Title,VideoLink,ImageLink,Description")] Stories_tbl stories_tbl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(stories_tbl).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(stories_tbl);
        //}

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stories_tbl stories_tbl = db.Stories_tbl.Find(id);
            db.Stories_tbl.Remove(stories_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
