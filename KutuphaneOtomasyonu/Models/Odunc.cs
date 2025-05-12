

namespace KütüphaneOtomasyonu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Odunc
    {
        public int OduncID { get; set; }
        public int KitapID { get; set; }
        public int OgrenciID { get; set; }
        public int PersonelID { get; set; }
        public System.DateTime OduncAlmaTarihi { get; set; }
        public Nullable<System.DateTime> IadeTarihi { get; set; }
        public Nullable<bool> TeslimAlindiMi { get; set; }
        public string Aciklama { get; set; }
        public Nullable<System.DateTime> OlusturmaTarihi { get; set; }
        public Nullable<System.DateTime> GuncellemeTarihi { get; set; }
    
        public virtual Kitap Kitap { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual Personel Personel { get; set; }
        [NotMapped]
        public int KalanGun
        {
            get
            {
                if (IadeTarihi.HasValue)
                {
                    return (IadeTarihi.Value - DateTime.Now).Days;
                }
                else
                {
                    return 0; 
                }
            }
        }


    }
}
