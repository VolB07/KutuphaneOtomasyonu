using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using KütüphaneOtomasyonu.DataModel;
using KütüphaneOtomasyonu.Models;

namespace KütüphaneOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private KutuphaneOtomasyon3Entities2 _kutuphaneOtomasyo;
        KutuphaneOtomasyon3Entities2 db = new KutuphaneOtomasyon3Entities2();
        public HomeController()
        {
            _kutuphaneOtomasyo = new KutuphaneOtomasyon3Entities2();
        }
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public ActionResult Kitaplar()
        {
            Kakitap _kakitap = new Kakitap();
            var kitaplarr = (from Kitap in _kutuphaneOtomasyo.Kitaps select Kitap).ToList();
            var Kategoriler = (from Kategori in _kutuphaneOtomasyo.Kategoris select Kategori).ToList();

            _kakitap.Kitaps = kitaplarr;
            _kakitap.Kategoris = Kategoriler;
            return View(_kakitap);
        }
        [HttpPost]
        public ActionResult Kitaplar(FormCollection fc)
        {
            try
            {
                Kitap depo = new Kitap();
                Kategori kdepo = new Kategori();

                if (fc["KitapEkle"] == "Ekle")
                {
                    depo.ISBN = fc["ISBN"];
                    depo.KitapAdi = fc["KitapAdi"];
                    depo.YazarAd = fc["YazarAd"];
                    depo.YazarSoyad = fc["YazarSoyad"];
                    depo.YayinEviAdi = fc["YayinEviAdi"];
                    var Kisim = fc["KategoriAdi"];
                    var kat = db.Kategoris.FirstOrDefault(k => k.KategoriAdi == Kisim);
                    if (kat != null)
                    {
                        int katID = kat.KategoriID;
                        depo.KategoriID = katID;
                    }
                    depo.RafNumarasi = fc["RafNumarasi"];
                    depo.AdetSayisi = Convert.ToInt32(fc["AdetSayisi"]);
                    depo.SayfaSayisi = Convert.ToInt32(fc["SayfaSayisi"]);

                    depo.AktifMi = true;
                    depo.OlusturmaTarihi = DateTime.Now;
                    depo.GuncellemeTarihi = null;

                    db.Kitaps.Add(depo);
                    db.SaveChanges();
                    return RedirectToAction("Kitaplar");
                }

                if (fc["KategoriEkle"] == "Ekle")
                {
                    kdepo.KategoriAdi = fc["KategoriAdi"];
                    kdepo.Aciklama = fc["Aciklama"];
                    kdepo.OlusturmaTarihi = DateTime.Now;
                    db.Kategoris.Add(kdepo);
                    db.SaveChanges();
                    return RedirectToAction("Kitaplar");
                }

                if (fc["KitapSil"] == "Sil")
                {
                    var aranankitap = fc["ISBN"];
                    var kitap = db.Kitaps.FirstOrDefault(k => k.ISBN.ToString() == aranankitap);
                    if (kitap != null)
                    {
                        kitap.AktifMi = false;
                        kitap.GuncellemeTarihi = DateTime.Now;
                        db.SaveChanges();
                    }
                }

                if (fc["Buls"] == "Bul")
                {
                    var arananVeri = fc["ISBN"];
                    var kod = db.Kitaps.FirstOrDefault(k => k.ISBN.ToString() == arananVeri);

                    var model = new Kakitap
                    {
                        ISBN = arananVeri,
                        SeciliKitap = kod,
                        Kitaps = db.Kitaps.ToList(),
                        Kategoris = db.Kategoris.ToList()
                    };
                    return View(model);
                }

                if (fc["guncel"] == "dene")
                {

                    var aranankitap = fc["ISBN"];
                    var guncelle = db.Kitaps.FirstOrDefault(k => k.ISBN.ToString() == aranankitap);
                    if (guncelle != null)
                    {
                        guncelle.KitapAdi = fc["KitapAdi"];
                        guncelle.YazarAd = fc["YazarAd"];
                        guncelle.YazarSoyad = fc["YazarSoyad"];
                        guncelle.YayinEviAdi = fc["YayinEviAdi"];
                        var Kisim = fc["KategoriAdi"];
                        var kat = db.Kategoris.FirstOrDefault(k => k.KategoriAdi == Kisim);
                        if (kat != null)
                        {
                            guncelle.KategoriID = kat.KategoriID;
                        }
                        guncelle.RafNumarasi = fc["RafNumarasi"];
                        guncelle.AdetSayisi = Convert.ToInt32(fc["AdetSayisi"]);
                        guncelle.SayfaSayisi = Convert.ToInt32(fc["SayfaSayisi"]);

                        var tarih = DateTime.Now;
                        guncelle.GuncellemeTarihi = tarih;
                        db.SaveChanges();
                    }
                }

                Kakitap _kakitap = new Kakitap();
                var kitaplarr = (from Kitap in _kutuphaneOtomasyo.Kitaps select Kitap).ToList();
                var Kategoriler = (from Kategori in _kutuphaneOtomasyo.Kategoris select Kategori).ToList();
                _kakitap.Kitaps = kitaplarr;
                _kakitap.Kategoris = Kategoriler;
                return View(_kakitap);
            }
            catch (Exception)
            {
                ViewBag.HataMesaji = "Hatalı giriş! Lütfen bilgileri kontrol ediniz.";

                Kakitap _kakitap = new Kakitap();
                _kakitap.Kitaps = db.Kitaps.ToList();
                _kakitap.Kategoris = db.Kategoris.ToList();
                return View(_kakitap);
            }
        }

        public ActionResult Personeller()
        {
            Kakitap _kakitap = new Kakitap();
            var ppersonel = (from Personel in _kutuphaneOtomasyo.Personels select Personel).ToList();
            _kakitap.Personels = ppersonel;
            return View(_kakitap);
        }
        [HttpPost]
        public ActionResult Personeller(FormCollection fc)
        {
            try
            {
                Personel depo = new Personel();
                if (fc["PersonelEkle"] == "Ekle")
                {
                    depo.Ad = fc["pAD"];
                    depo.Soyad = fc["pSoyAd"];
                    depo.Email = fc["pEmail"];
                    string pasword = fc["pSifre"];
                    depo.Sifre = ComputeSha256Hash(pasword);

                    depo.AktifMi = true;
                    depo.AdminMi = false;

                    depo.OlusturmaTarihi = DateTime.Now;
                    depo.GuncellemeTarihi = null;

                    db.Personels.Add(depo);
                    db.SaveChanges();
                    return RedirectToAction("Personeller");
                }
                if (fc["PersonelSil"] == "Sil")
                {
                    var arananPersonel = fc["PersonelID"];
                    var per = db.Personels.FirstOrDefault(p => p.PersonelID.ToString() == arananPersonel);
                    per.AktifMi = false;
                    db.SaveChanges();
                }
                if (fc["perBul"] == "Bul")
                {
                    var arananVeri = fc["pAd"];
                    var kod = db.Personels.FirstOrDefault(k => k.Ad.ToString() == arananVeri);

                    var model = new Kakitap
                    {
                        Ad = arananVeri,
                        secilipersonel = kod,
                        Personels = db.Personels.ToList()
                    };
                    return View(model);
                }

                if (fc["pGuncelle"] == "Güncelle")
                {

                    var aranankitap = fc["perid"];
                    var guncelle = db.Personels.FirstOrDefault(k => k.PersonelID.ToString() == aranankitap);
                    if (guncelle != null)
                    {
                        guncelle.Ad = fc["pAD"];
                        guncelle.Soyad = fc["pSoyad"];
                        guncelle.Email = fc["pEmail"];
                        guncelle.Sifre = fc["pSifre"];

                        guncelle.GuncellemeTarihi = DateTime.Now;
                        db.SaveChanges();
                    }
                }
                Kakitap _kakitap = new Kakitap();
                var ppersonel = (from Personel in _kutuphaneOtomasyo.Personels select Personel).ToList();
                _kakitap.Personels = ppersonel;
                return View(_kakitap);
            }
            catch (Exception)
            {
                ViewBag.HataMesaji = "Hatalı giriş! Lütfen bilgileri kontrol ediniz.";

                Kakitap _kakitap = new Kakitap();
                _kakitap.Personels = db.Personels.ToList();
                return View(_kakitap);
            }
        }

        public ActionResult Ogrenciler()
        {
            Kakitap _kakitap = new Kakitap();
            var ogrencilerr = (from Ogrenci in _kutuphaneOtomasyo.Ogrencis select Ogrenci).ToList();
            var bolumm = (from Bolum in _kutuphaneOtomasyo.Bolums select Bolum).ToList();
            _kakitap.Ogrencis = ogrencilerr;
            _kakitap.Bolums = bolumm;
            return View(_kakitap);
        }
        [HttpPost]
        public ActionResult Ogrenciler(FormCollection fc)
        {
            try
            {
                Ogrenci depo = new Ogrenci();
                Bolum Kdepo = new Bolum();
                if (fc["OgrenciEkle"] == "Ekle")
                {
                    depo.Ad = fc["oAD"];
                    depo.Soyad = fc["oSoyad"];
                    depo.OgrenciNo = fc["oNO"];

                    var Kisim = fc["BolumAdi"];
                    var kat = db.Bolums.FirstOrDefault(k => k.BolumAdi == Kisim);
                    if (kat != null)
                    {
                        int katID = kat.BolumID;
                        depo.BolumID = katID;
                    }

                    depo.Email = fc["oEmail"];
                    depo.Telefon = fc["oTel"];
                    depo.SilindiMi = false;
                    depo.OlusturmaTarihi = DateTime.Now;

                    db.Ogrencis.Add(depo);
                    db.SaveChanges();
                    return RedirectToAction("Ogrenciler");
                }

                if (fc["BolumEkle"] == "Ekle")
                {
                    Kdepo.BolumAdi = fc["BolumAdi"];
                    Kdepo.OlusturmaTarihi = DateTime.Now;
                    Kdepo.AktifMi = true;
                    db.Bolums.Add(Kdepo);
                    db.SaveChanges();
                    return RedirectToAction("Ogrenciler");
                }
                if (fc["OgrBul"] == "Bul")
                {
                    var arananVeri = fc["oNo"];
                    var kod = db.Ogrencis.FirstOrDefault(k => k.OgrenciNo.ToString() == arananVeri);

                    var model = new Kakitap
                    {
                        OgrenciNo = arananVeri,
                        seciliOgrenci = kod,
                        Ogrencis = db.Ogrencis.ToList(),
                        Bolums = db.Bolums.ToList()
                    };
                    return View(model);
                }
                if (fc["pGuncelle"] == "Güncelle")
                {
                    var aranankitap = fc["OgrID"];
                    var guncelle = db.Ogrencis.FirstOrDefault(k => k.OgrenciNo.ToString() == aranankitap);
                    if (guncelle != null)
                    {
                        guncelle.Ad = fc["oAD"];
                        guncelle.Soyad = fc["oSoyad"];
                        guncelle.Email = fc["oEmail"];
                        guncelle.Telefon = fc["oTel"];
                        var Kisim = fc["BolumAdi"];
                        var kat = db.Bolums.FirstOrDefault(k => k.BolumAdi == Kisim);
                        if (kat != null)
                        {
                            guncelle.BolumID = kat.BolumID;
                        }
                        guncelle.GuncellemeTarihi = DateTime.Now;
                        db.SaveChanges();
                    }
                }
                if (fc["OgrSil"] == "Sil")
                {
                    var arananPersonel = fc["oID"];
                    var per = db.Ogrencis.FirstOrDefault(p => p.OgrenciID.ToString() == arananPersonel);
                    per.SilindiMi = true;
                    db.SaveChanges();
                }


                Kakitap _kakitap = new Kakitap();
                var ogrencilerr = (from Ogrenci in _kutuphaneOtomasyo.Ogrencis select Ogrenci).ToList();
                var bolumm = (from Bolum in _kutuphaneOtomasyo.Bolums select Bolum).ToList();
                _kakitap.Ogrencis = ogrencilerr;
                _kakitap.Bolums = bolumm;
                return View(_kakitap);
            }
            catch (Exception)
            {
                ViewBag.HataMesaji = "Hatalı giriş! Lütfen bilgileri kontrol ediniz.";

                Kakitap _kakitap = new Kakitap();
                _kakitap.Ogrencis = db.Ogrencis.ToList();
                _kakitap.Bolums = db.Bolums.ToList();
                return View(_kakitap);
            }
        }
        public ActionResult Oduncver()
        {
            Kakitap _kakitap = new Kakitap();
            var kitaplarr = (from Kitap in _kutuphaneOtomasyo.Kitaps select Kitap).ToList();
            var ogrencilerr = (from Ogrenci in _kutuphaneOtomasyo.Ogrencis select Ogrenci).ToList();
            var ppersonel = (from Personel in _kutuphaneOtomasyo.Personels select Personel).ToList();
            var oduncc = (from Odunc in _kutuphaneOtomasyo.Oduncs select Odunc).ToList();
            _kakitap.Personels = ppersonel;
            _kakitap.Ogrencis = ogrencilerr;
            _kakitap.Kitaps = kitaplarr;
            _kakitap.Oduncs = oduncc;
            return View(_kakitap);
        }
        [HttpPost]
        public ActionResult Oduncver(FormCollection fc)
        {
            Kakitap _kakitap = new Kakitap();
            var kitaplarr = (from Kitap in _kutuphaneOtomasyo.Kitaps select Kitap).ToList();
            var ogrencilerr = (from Ogrenci in _kutuphaneOtomasyo.Ogrencis select Ogrenci).ToList();
            var ppersonel = (from Personel in _kutuphaneOtomasyo.Personels select Personel).ToList();
            var oduncc = (from Odunc in _kutuphaneOtomasyo.Oduncs select Odunc).ToList();
            _kakitap.Personels = ppersonel;
            _kakitap.Ogrencis = ogrencilerr;
            _kakitap.Kitaps = kitaplarr;
            _kakitap.Oduncs = oduncc;

            Kitap K = new Kitap();
            Ogrenci O = new Ogrenci();
            Personel P = new Personel();
            Odunc Od = new Odunc();



            if (fc["Odver"] == "Ödünç ver")
            {
                var kitapp = fc["KitapAdi"];
                var kit = db.Kitaps.FirstOrDefault(k => k.KitapAdi == kitapp);
                if (kit != null)
                {
                    Od.KitapID = kit.KitapID;
                }
                var Ogrencii = fc["OgrenciAdi"];
                var Ogr = db.Ogrencis.FirstOrDefault(k => k.Ad == Ogrencii);
                if (Ogr != null)
                {
                    Od.OgrenciID = Ogr.OgrenciID;
                }
                var Personell = fc["PersonelAdi"];
                var per = db.Personels.FirstOrDefault(k => k.Ad == Personell);
                if (per != null)
                {
                    Od.PersonelID = per.PersonelID;
                }

                Od.OduncAlmaTarihi = DateTime.Now;
                Od.IadeTarihi = DateTime.Now.AddDays(15);


                Od.TeslimAlindiMi = false;

                Od.Aciklama = fc["Acikla"];

                Od.OlusturmaTarihi = DateTime.Now;
                Od.GuncellemeTarihi = null;
                db.Oduncs.Add(Od);
                db.SaveChanges();

                return RedirectToAction("Oduncver");
            }
            //if (fc["oduncsil"] == "Sil")
            //{
            //    var arananPersonel = fc["oduncID"];
            //    var per = db.Oduncs.FirstOrDefault(p => p.OduncID.ToString() == arananPersonel);
            //    per.Temizle = false;
            //    db.SaveChanges();
            //}
            if (fc["OduncBul"] == "Bul")
            {
                var arananOduncID = fc["oduncID"];
                var odunc = db.Oduncs.FirstOrDefault(k => k.OduncID.ToString() == arananOduncID);

                var model = new Kakitap
                {
                    odID = arananOduncID,
                    seciliodunc = odunc,
                    Oduncs = db.Oduncs.ToList(),
                    Kitaps = db.Kitaps.ToList(),
                    Ogrencis = db.Ogrencis.ToList(),
                    Personels = db.Personels.ToList()
                };
                return View(model);
            }
            if (fc["OduncGuncelle"] == "Güncelle")
            {
                var guncelleme = fc["OduncID"];
                var guncel = db.Oduncs.FirstOrDefault(k => k.OduncID.ToString() == guncelleme);
                if (guncel != null)
                {
                    var kit = fc["KitapAdi"];
                    var kat = db.Kitaps.FirstOrDefault(k => k.KitapAdi == kit);
                    if (kat != null)
                    {
                        guncel.KitapID = kat.KitapID;
                    }

                    var Ogr = fc["oAd"];
                    var ogrr = db.Ogrencis.FirstOrDefault(k => k.Ad.ToString() == Ogr);
                    if (ogrr != null)
                    {
                        guncel.OgrenciID = ogrr.OgrenciID;
                    }

                    var per = fc["PersonelAdi"];
                    var perr = db.Personels.FirstOrDefault(k => k.Ad.ToString() == per);
                    if (perr != null)
                    {
                        guncel.PersonelID = perr.PersonelID;
                    }

                    guncel.Aciklama = fc["Aciklama"];
                    guncel.GuncellemeTarihi = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Oduncver");
                }
            }
            if (fc["teslim"] == "Teslim Edildi")
            {
                var arananPersonel = fc["tesID"];
                var per = db.Oduncs.FirstOrDefault(p => p.OduncID.ToString() == arananPersonel);
                per.TeslimAlindiMi = true;
                db.SaveChanges();
                return RedirectToAction("Oduncver");
            }
            return View(_kakitap);
        }
         public ActionResult Hakkımızda()
         {
            return View();
         }
        public ActionResult NasılKullanılır()
        {
            return View();
        }
    }

}