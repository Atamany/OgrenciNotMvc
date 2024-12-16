using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var ogrenciler = db.TblOgrenciler.ToList();
            return View(ogrenciler);
        }

        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            List<SelectListItem> degerler = (from i in db.TblKulupler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text=i.KulupAd,
                                                 Value=i.KulupId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(TblOgrenciler p3)
        {
            var klp = db.TblKulupler.Where(m => m.KulupId == p3.TblKulupler.KulupId).FirstOrDefault();
            p3.TblKulupler = klp;
            db.TblOgrenciler.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}