using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Author
        DBLibraryEntities db = new DBLibraryEntities();
        public ActionResult Index()
        {
            var yazarlar = db.Yazars.ToList();
            return View(yazarlar); // veritabanındai verileri liste olarak alıyor.
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(Yazar author)
        {
            db.Yazars.Add(author);
            var sonyazar = db.Yazars.ToList().OrderByDescending(x => x.ID).FirstOrDefault();
            author.ID = sonyazar.ID + 1; //veritabanında daha öncejki verinin idsini 1 artırıp kaydediyor.
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}