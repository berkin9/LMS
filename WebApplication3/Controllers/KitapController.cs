using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models.Entity;
namespace WebApplication3.Controllers
{

    public class KitapController : Controller
    {
        // GET: Kitap
        DBLibraryEntities db = new DBLibraryEntities();//veritabanındaki bilgileri çekiyor.
        public ActionResult Index()
        {
            var kitaplar = db.Kitaps.ToList();
            return View(kitaplar);
        }

        [HttpGet]
        public ActionResult KitapEkle()
        {//Dropdown listesinde mevcut yazarları listeliyor ve seçilen yazarı tutuyor.

            List<SelectListItem> deger2 = (from i in db.Yazars.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.value2 = deger2;
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(Kitap book)
        {//seçilen yazar bilgisine göre eklenen yeni kitabı veritabanına kaydeden fonsiyondur.
            var author = db.Yazars.Where(authr => authr.ID == book.Yazar1.ID).FirstOrDefault();
            book.Yazar1 = author;
            book.DURUM = true;
            db.Kitaps.Add(book);
            var sonkitap = db.Kitaps.ToList().OrderByDescending(x =>x.ID).FirstOrDefault();
            if(sonkitap != null)
            {
                book.ID = sonkitap.ID + 1;//veritabanında daha öncejki verinin idsini 1 artırıp kaydediyor.

            }
            else
            {
                book.ID = 1;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}