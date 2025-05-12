using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KütüphaneOtomasyonu.Models;

namespace KütüphaneOtomasyonu.DataModel
{
    public class Kakitap
    {
        public List<Kitap> Kitaps { get; set; }
        public List<Kategori> Kategoris { get; set; }
        public string ISBN { get; set; }
        public Kitap SeciliKitap { get; set; }

        public List<Personel> Personels { get; set; }
        public string Ad { get; set; }
        public Personel secilipersonel { get; set; }

        public List<Ogrenci> Ogrencis { get; set; }
        public List<Bolum> Bolums { get; set; }
        public string OgrenciNo { get; set; }
        public Ogrenci seciliOgrenci { get; set; }

        public List<Odunc> Oduncs { get; set; }
        public string odID { get; set; }
        public Odunc seciliodunc { get; set; }
    }
}