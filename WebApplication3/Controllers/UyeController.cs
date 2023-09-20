using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Author
        DBLibraryEntities db = new DBLibraryEntities();
        public ActionResult Index()
        {
            var uyeler = db.Üye.ToList(); // veritabanındai verileri liste olarak alıyor.
            return View(uyeler);
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(Üye uye)
        {
            db.Üye.Add(uye);
            var sonuye = db.Üye.ToList().OrderByDescending(x => x.ID).FirstOrDefault();
            if (sonuye != null) //veritabanında daha önce veri olup olmadığını kontrol ediyor.
            {
                uye.ID = sonuye.ID + 1; //varsa son idnin üzerine 1 ekleyip kaydediyor.
            }
            else
            {

                uye.ID = 1; // yoksa doğrudan 1 veriyor.
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}