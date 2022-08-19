using Banka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banka
{
    public class ViewModel
    {

        public List<Musteri> MusteriList { get; set; }
        public List<Musteri_Temsilcisi> MusteriTemsilcisiList { get; set; }
        public List<Hesaplar> HesaplarList { get; set; }
        public List<İslemler> IslemlerList { get; set; }
        public List<Makbuz> MakbuzList { get; set; }
        public List<Banka_Toplam> Banka_ToplamList { get; set; }
        public List<Hesaplar> KullaniciHesaplari { get; set; }
        public List<Musteri> KullaniciBilgileri { get; set; }
        public List<Musteri> TemsilciMusteri { get; set; }
        public List<Hesaplar> TemsilciHesaplari { get; set; }
        public List<Kur> KurList { get; set; }
    }
}