//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KütüphaneOtomasyonu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kitap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kitap()
        {
            this.Oduncs = new HashSet<Odunc>();
        }
    
        public int KitapID { get; set; }
        public string ISBN { get; set; }
        public string KitapAdi { get; set; }
        public string YazarAd { get; set; }
        public string YazarSoyad { get; set; }
        public string YayinEviAdi { get; set; }
        public int KategoriID { get; set; }
        public string RafNumarasi { get; set; }
        public int AdetSayisi { get; set; }
        public Nullable<bool> AktifMi { get; set; }
        public Nullable<int> SayfaSayisi { get; set; }
        public Nullable<System.DateTime> OlusturmaTarihi { get; set; }
        public Nullable<System.DateTime> GuncellemeTarihi { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Odunc> Oduncs { get; set; }
    }
}
