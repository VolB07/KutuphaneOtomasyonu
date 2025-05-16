using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KütüphaneOtomasyonu.Models;

namespace KütüphaneOtomasyonu.Controllers
{
    public class Login : Controller
    {
        KutuphaneOtomasyon3Entities4 db = new KutuphaneOtomasyon3Entities4();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            try
            {
                string Email = fc["username"];
                string Sifre = fc["password"];
                var kullanici = db.Personels.FirstOrDefault(p => p.Email == Email && p.Sifre == Sifre && p.AktifMi);


                if (kullanici != null)
                {
                    HttpCookie cookie = new HttpCookie("KullaniciEmail", kullanici.Email);
                    cookie.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Kitaplar","Home");
                }
                ViewBag.Hata = "Geçersiz Email veya Şifre";
                return View();
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}