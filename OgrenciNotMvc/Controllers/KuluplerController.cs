using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class KuluplerController : Controller
    {
        // GET: Kulupler
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var kulupler = db.TblKulupler.ToList();
            return View(kulupler);
        }
        [HttpGet]
        public ActionResult YeniKulup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKulup(TblKulupler p2)
        {
            db.TblKulupler.Add(p2);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var kulup = db.TblKulupler.Find(id);
            db.TblKulupler.Remove(kulup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KulupGetir(int id)
        {
            var kulup2 = db.TblKulupler.Find(id);
            return View("KulupGetir",kulup2);
        }
        public ActionResult Guncelle(TblKulupler P)
        {
            var klp = db.TblKulupler.Find(P.KulupId);
            klp.KulupAd = P.KulupAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}