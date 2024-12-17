using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using OgrenciNotMvc.Models;

namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var SinavNotlar = db.TblNotlar
                .Include(w => w.TblOgrenciler)
                .Include(w => w.TblDersler)
                .ToList();
            return View(SinavNotlar);
        }
        [HttpGet]
        public ActionResult YeniSinav()
        {
            ViewBag.dgr = db.TblOgrenciler.Select(o => new SelectListItem
            {
                Value = o.OgrenciId.ToString(), // Kaydedilecek değer
                Text = o.OgrenciId + " - " + o.OgrAd + " " + o.OgrSoyad // Görüntülenecek metin
            }).ToList();
            ViewBag.ders = db.TblDersler.Select(i => new SelectListItem
            {
                Value = i.DersId.ToString(), // Kaydedilecek değer
                Text = i.DersAd // Görüntülenecek metin
            }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult YeniSinav(TblNotlar tbn, int sinav1 = 0, int sinav2 = 0, int sinav3 = 0, int proje = 0)
        { 
            // Öğrenci ID'sini alıyoruz ve TblNotlar'a atıyoruz
            tbn.OgrId = tbn.OgrId; // Formdan alınan OgrId'yi doğrudan atıyoruz

            // Ders ID'sini alıyoruz ve TblNotlar'a atıyoruz
            tbn.DersId = tbn.DersId; // Formdan alınan DersId'yi doğrudan atıyoruz

            // Diğer işlemler (sınav, proje vs.)
            tbn.Sinav1 = (byte?)sinav1;
            tbn.Sinav2 = (byte?)sinav2;
            tbn.Sinav3 = (byte?)sinav3;
            tbn.Proje = (byte?)proje;

            // Ortalama hesaplaması
            tbn.Ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;

            // Durum hesaplaması
            tbn.Durum = tbn.Ortalama >= 50;

            // TblNotlar nesnesini veritabanına ekliyoruz
            db.TblNotlar.Add(tbn);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult NotGetir(int id)
        {
            var notlar = db.TblNotlar.Find(id);
            return View("NotGetir",notlar);
        }
        [HttpPost]
        public ActionResult NotGetir(TblNotlar p, int sinav1=0, int sinav2=0, int sinav3=0, int proje=0)
        {
            
                var snv = db.TblNotlar.Find(p.NotId);
            snv.OgrId = snv.OgrId; // Formdan alınan OgrId'yi doğrudan atıyoruz

            // Ders ID'sini alıyoruz ve TblNotlar'a atıyoruz
            snv.DersId = snv.DersId; // Formdan alınan DersId'yi doğrudan atıyoruz

            // Diğer işlemler (sınav, proje vs.)
            snv.Sinav1 = (byte?)sinav1;
            snv.Sinav2 = (byte?)sinav2;
            snv.Sinav3 = (byte?)sinav3;
            snv.Proje = (byte?)proje;

            // Ortalama hesaplaması
            snv.Ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;

            // Durum hesaplaması
            snv.Durum = snv.Ortalama >= 50;
            db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            
        }
    }
}