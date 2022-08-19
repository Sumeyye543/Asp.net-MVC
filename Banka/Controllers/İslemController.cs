using Banka.Models;
using Banka.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banka.Controllers
{
    public class İslemController:Controller
    {
        BankaEntities1 n = new BankaEntities1();


        [HttpGet]



        public ActionResult İslemEkle()
        {
            İslemler k = new İslemler();
            ViewBag.musteri = n.Musteri.SqlQuery(sql: "Select * From Musteri where temsilci_Tc='" + Session["Tc"] + "'");
            Session["durum"] = "ekle";

            return View(k);
        }

        [HttpPost]
        public ActionResult İslemEkle(İslemler k)
        {
            İslemler a = n.İslemler.FirstOrDefault(x => x.islem_id == k.islem_id);
            if (Session["durum"].ToString() == "ekle")
            {
                k.temsilci_Tc = Session["Tc"].ToString();
                n.İslemler.Add(k);
                n.SaveChanges();

            }
            else
            {
                a.islem_adi = k.islem_adi;
                a.islem_bakiye = k.islem_bakiye;
                a.islem_tarihi = k.islem_tarihi;
                a.musteri_Tc = k.musteri_Tc;
                n.SaveChanges();



            }
            return Redirect("/Admin/Index");


        }
        [HttpGet]

        
        public ActionResult İslemSil()
        {

            List<İslemler> list = n.İslemler.ToList();
            list = n.İslemler.SqlQuery(sql: "Select * From İslemler where temsilci_Tc='" + Session["Tc"] + "'").ToList();

            return View(list);

        }

        public void Sil(int id)
        {
            List<Makbuz> d = n.Makbuz.ToList();

            İslemler user = n.İslemler.FirstOrDefault(x => x.islem_id == id);
            d = n.Makbuz.SqlQuery(sql: "Select * From Makbuz where islem_id='" + id + "'").ToList();

            if (d != null)
            {

                n.Makbuz.RemoveRange(d);
                n.SaveChanges();
            }
            n.İslemler.Remove(user);
            n.SaveChanges();


        }


        public ActionResult İslemGuncelle()
        {
            List<İslemler> list = n.İslemler.ToList();
            list = n.İslemler.SqlQuery(sql: "Select * From İslemler where temsilci_Tc='" + Session["Tc"] + "'").ToList();



            return View(list);

        }

        public ActionResult Guncelle(int id)
        {

            İslemler user = n.İslemler.FirstOrDefault(x => x.islem_id == id);
            ViewBag.musteri = n.Musteri.ToList();
            Session["durum"] = "guncelle";
            return View("İslemEkle", user);
        }

    }
}