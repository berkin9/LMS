using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models.Entity;

namespace WebApplication3.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBLibraryEntities db = new DBLibraryEntities();

        public ActionResult Index()
        {
            var oduncler = db.ÖDÜNÇ.ToList(); // veritabanındai verileri liste olarak alıyor.
            return View(oduncler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from x in db.Üye.ToList() select new SelectListItem { Text = x.AD + " " + x.SOYAD, Value = x.ID.ToString() }).ToList(); // tüm üylere listeleniyor
            List<SelectListItem> deger2 = (from y in db.Kitaps.Where(x => x.DURUM == true).ToList() select new SelectListItem { Text = y.AD, Value = y.ID.ToString() }).ToList(); // sadece müsait kitaplar listeeniyor.
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;


            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(ÖDÜNÇ p)
        {
            var d1 = db.Üye.Where(x => x.ID == p.Üye1.ID).FirstOrDefault();
            var d2 = db.Kitaps.Where(y => y.ID == p.Kitap.ID).FirstOrDefault();
            p.Üye1 = d1;
            p.Kitap = d2;
            db.ÖDÜNÇ.Add(p);
            var sonodunc = db.ÖDÜNÇ.ToList().OrderByDescending(x => x.ID).FirstOrDefault();
            if (sonodunc != null) //veritabanında daha önce veri olup olmadığını kontrol ediyor.
            {
                p.ID = sonodunc.ID + 1; //varsa son idnin üzerine 1 ekleyip kaydediyor.
            }
            else
            {
                p.ID = 1; // yoksa doğrudan 1 veriyor.
            }

            db.SaveChanges();

            
            return RedirectToAction("Index");
        }

    }
}