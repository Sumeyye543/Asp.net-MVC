using Banka.Models;
using Banka.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banka.Controllers
{
    public class MusteriController :Controller
    {
        BankaEntities1 n = new BankaEntities1();
        [HttpGet]


        [Authorize(Roles = "A")]
        public ActionResult MusteriEkle()
        {
            Musteri k = new Musteri();
            ViewBag.temsilci = n.Musteri_Temsilcisi.ToList();
            Session["durum"] = "ekle";
            return View(k);
        }

        [HttpPost]
        public ActionResult MusteriEkle(Musteri k)
        {
            try
            {
                int flag = 0;
                Kullanıcılar yeni = n.Kullanıcılar.FirstOrDefault(x => x.Tc == k.musteri_Tc);
                var yeni1 = new Kullanıcılar();
                ViewBag.temsilci = n.Musteri_Temsilcisi.ToList();
                ViewBag.musteri = n.Musteri.ToList();
                Musteri kat = n.Musteri.FirstOrDefault(x => x.musteri_Tc == k.musteri_Tc);
                if (Session["durum"].ToString() != "guncelle")
                {
                    foreach (var item in ViewBag.musteri)
                    {
                        if (item.musteri_Tc == k.musteri_Tc)
                        {
                            flag = 1;

                        }



                    }
                    if (flag == 0)
                    {
                        n.Musteri.Add(k);
                        yeni1.Tc = k.musteri_Tc;
                        yeni1.Adı_Soyadı = k.musteri_adi + " " + k.musteri_soyadi;
                        yeni1.sifre = k.musteri_sifre;
                        yeni1.tip = "M";
                        n.Kullanıcılar.Add(yeni1);
                        Session["durum"] = "ekle";
                    }
                    else if (flag == 1)
                    {
                        Session["msa"] = "Tc ile aynı Tc'ye ait kayıt bulunmaktadır.";
                        return Redirect("/Musteri/MusteriEkle");

                    }


                }
                else
                {
                   
                    yeni.Adı_Soyadı = k.musteri_adi + " " + k.musteri_soyadi;
                    yeni.sifre = k.musteri_sifre;
                    kat.musteri_Tc = k.musteri_Tc;
                    kat.musteri_Tc = k.musteri_Tc;
                    kat.musteri_adi = k.musteri_adi;
                    kat.musteri_soyadi = k.musteri_soyadi;
                    kat.musteri_tel = k.musteri_tel;
                    kat.musteri_eposta = k.musteri_eposta;
                    kat.musteri_adres = k.musteri_adres;
                    kat.musteri_sifre = k.musteri_sifre;
                    kat.temsilci_Tc = k.temsilci_Tc;
                }
                n.SaveChanges();

                return Redirect("/Admin/Index");
            }

            catch
            {
                Session["msa"] = "Bütün Alanları Doldurunuz";
                return Redirect("/Musteri/MusteriEkle");

            }
            
        }
        [HttpGet]
        [Authorize(Roles = "A")]
        public ActionResult MusteriSil()
        {
            List<Musteri> list = n.Musteri.ToList();


            return View(list);

        }
        [HttpPost]
        public void Sil(string id)
        {
            List<Hesaplar> b = n.Hesaplar.ToList();
            List<İslemler> c = n.İslemler.ToList();
            List<Makbuz> d = n.Makbuz.ToList();
            List<Kredi_Borclari> e = n.Kredi_Borclari.ToList();
            Musteri user = n.Musteri.FirstOrDefault(x => x.musteri_Tc == id);
            b = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + id + "'").ToList();
            c = n.İslemler.SqlQuery(sql: "Select * From İslemler where musteri_Tc='" + id + "'").ToList();
            d = n.Makbuz.SqlQuery(sql: "Select * From Makbuz INNER JOIN İslemler ON Makbuz.islem_id=İslemler.islem_id where musteri_Tc='" + id + "'").ToList();

            e = n.Kredi_Borclari.SqlQuery(sql: "Select * From Kredi_Borclari where musteri_Tc='" + id + "'").ToList();

            Kullanıcılar a = n.Kullanıcılar.FirstOrDefault(x => x.Tc == id);
            if (a != null)
            {
                n.Kullanıcılar.Remove(a);
            }
            if (b != null)
            {
                n.Hesaplar.RemoveRange(b);
                n.SaveChanges();
            }
            if (d != null)
            {

                n.Makbuz.RemoveRange(d);
                n.SaveChanges();
            }
            if (c != null)
            {

                n.İslemler.RemoveRange(c);
                n.SaveChanges();
            }
            if(e != null)
            {
                n.Kredi_Borclari.RemoveRange(e);
                n.SaveChanges();
            }


            n.Musteri.Remove(user);
            n.SaveChanges();


        }
        [Authorize(Roles = "A,T")]
        public ActionResult MusteriGuncelle()
        {
            List<Musteri> list = n.Musteri.ToList();


            return View(list);

        }

        public ActionResult Guncelle(string id)
        {

            Musteri user = n.Musteri.FirstOrDefault(x => x.musteri_Tc == id);
            ViewBag.temsilci = n.Musteri_Temsilcisi.ToList();
            Session["durum"] = "guncelle";
            return View("MusteriEkle", user);

        }
        public JsonResult GetChartData(string id)
        {
            try
            {

                ViewModel model = new ViewModel();
                model.KullaniciHesaplari = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + id + "'").ToList();
                decimal[] SeriesVal = new decimal[model.KullaniciHesaplari.Count];
                string[] LabelsVal = new string[model.KullaniciHesaplari.Count];

                int i = 0;
                foreach (var item in model.KullaniciHesaplari )
                {
                    SeriesVal[i] = Convert.ToDecimal(item.hesap_bakiye);
                    LabelsVal[i] = item.hesap_id.ToString();
                    i++;
                }



               
                return Json(new { success = true, series = SeriesVal, labels = LabelsVal, message = "success.!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
