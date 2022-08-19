using Banka.Models;
using Banka.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banka.Controllers
{
    public class TemsilciController:Controller
    {
        BankaEntities1 n = new BankaEntities1();
        [HttpGet]
        [Authorize(Roles = "A")]
        
        public ActionResult TemsilciEkle()
        {
            Musteri_Temsilcisi k = new Musteri_Temsilcisi();
            ViewBag.temsilci = n.Musteri_Temsilcisi.ToList();
            Session["durum"] = "ekle";
            return View(k);
        }

        [HttpPost]
        public ActionResult Ekle(Musteri_Temsilcisi k)
        {
            try
            {
                int flag = 0;
                Kullanıcılar yeni = n.Kullanıcılar.FirstOrDefault(x => x.Tc == k.temsilci_Tc);
                var yeni1 = new Kullanıcılar();
                ViewBag.temsilci = n.Musteri_Temsilcisi.ToList();
                Musteri_Temsilcisi kat = n.Musteri_Temsilcisi.FirstOrDefault(x => x.temsilci_Tc == k.temsilci_Tc);

                if (Session["durum"].ToString() != "guncelle")
                {
                    foreach (var item in ViewBag.temsilci)
                    {
                        if (item.temsilci_Tc == k.temsilci_Tc)
                        {
                            flag = 1;

                        }



                    }
                    if (flag == 0)
                    {
                        n.Musteri_Temsilcisi.Add(k);
                        yeni1.Tc = k.temsilci_Tc;
                        yeni1.Adı_Soyadı = k.temsilci_adi + " " + k.temsilci_soyadi;
                        yeni1.sifre = k.temsilci_sifre;
                        yeni1.tip = "T";
                        n.Kullanıcılar.Add(yeni1);
                        Session["durum"] = "ekle";
                    }
                    else if (flag == 1)
                    {
                        Session["msg"] = "Tc ile aynı Tc'ye ait kayıt bulunmaktadır.";
                        return Redirect("/Temsilci/TemsilciEkle");

                    }


                }


                else
                {
                    yeni.Adı_Soyadı = k.temsilci_adi + " " + k.temsilci_soyadi;
                    yeni.sifre = k.temsilci_sifre;
                    kat.temsilci_Tc = k.temsilci_Tc;
                    kat.temsilci_adi = k.temsilci_adi;
                    kat.temsilci_soyadi = k.temsilci_soyadi;
                    kat.temsilci_tel = k.temsilci_tel;
                    kat.temsilci_eposta = k.temsilci_eposta;
                    kat.temsilci_adres = k.temsilci_adres;
                    kat.temsilci_sifre = k.temsilci_sifre;
                    kat.temsilci_Tc = k.temsilci_Tc;
                }

                n.SaveChanges();

                return Redirect("/Admin/Index");
            }
            catch
            {
                Session["msg"] = "Bütün Alanları Doldurunuz";
                return Redirect("/Temsilci/TemsilciEkle");
            }

        }
        [HttpGet]

        [MyAuthorization(Roles = "A")]
        public ActionResult TemsilciSil()
        {
            List<Musteri_Temsilcisi> list = n.Musteri_Temsilcisi.ToList();
            ViewBag.Musteri = n.Musteri.ToList();


            return View(list);

        }
        [HttpPost]
        public JsonResult Sil(string id)
        {
            try
            {
                Musteri_Temsilcisi user = n.Musteri_Temsilcisi.FirstOrDefault(x => x.temsilci_Tc == id);
                Musteri b = n.Musteri.FirstOrDefault(x => x.temsilci_Tc == id);

                Kullanıcılar a = n.Kullanıcılar.FirstOrDefault(x => x.Tc == user.temsilci_Tc);
                if (a != null)
                {
                    n.Kullanıcılar.Remove(a);
                }
                n.Musteri_Temsilcisi.Remove(user);
                n.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
           


        }
        public ActionResult TemsilciGuncelle()
        {
            List<Musteri_Temsilcisi> list = n.Musteri_Temsilcisi.ToList();


            return View(list);

        }

        public ActionResult Guncelle(string id)
        {

            Musteri_Temsilcisi user = n.Musteri_Temsilcisi.FirstOrDefault(x => x.temsilci_Tc == id);
            ViewBag.temsilci = n.Musteri_Temsilcisi.ToList();
            Session["durum"] = "guncelle";
            return View("TemsilciEkle", user);
        }














    }
}