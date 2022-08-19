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
    public class HesapController: Controller
    {
        BankaEntities1 n = new BankaEntities1();
        [HttpGet]



        [Authorize(Roles = "M")]


        public ActionResult HesapEkle()
        {
           Hesaplar k = new Hesaplar();
            ViewBag.kur = n.Kur.ToList();

            return View(k);
        }

        [HttpPost]
        public ActionResult HesapEkle(Hesaplar k)
        {
            k.musteri_Tc = Session["Tc"].ToString();
            k.hesap_bakiye = "0";
            
                    n.Hesaplar.Add(k);
                    n.SaveChanges();
                return Redirect("/Admin/Index");
            

          
        }
        [HttpGet]

        [MyAuthorization(Roles = "M")]
        public ActionResult HesapSil()
        {

            List<Hesaplar> list = n.Hesaplar.ToList();
            list=n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + Session["Tc"] + "'").ToList();

            return View(list);

        }

        public void Sil(int id)
        {
            Hesaplar user = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id);
            n.Hesaplar.Remove(user);
            n.SaveChanges();


        }


        public ActionResult HesapBakiye()
        {
            List<Hesaplar> list = n.Hesaplar.ToList();
            list = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + Session["Tc"] + "'").ToList();

            return View(list);


           

        }
        [HttpPost]
        public ActionResult Guncelle(int id,string bakiye)
        {

            Hesaplar user = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id);
            user.hesap_bakiye = (Convert.ToDouble(user.hesap_bakiye)+Convert.ToDouble(bakiye)).ToString();
            n.SaveChanges();


            return RedirectToAction("HesapBakiye");
        }
        public ActionResult HesapBakiye1()
        {
            List<Hesaplar> list = n.Hesaplar.ToList();
            list = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + Session["Tc"] + "'").ToList();

            return View(list);




        }
        [HttpPost]
        public ActionResult Guncelle1(int id,string bakiye)
        {

            Hesaplar user = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id);
            if (Convert.ToDouble(user.hesap_bakiye) >= Convert.ToDouble(bakiye))
            {
                user.hesap_bakiye = (Convert.ToDouble(user.hesap_bakiye) - Convert.ToDouble(bakiye)).ToString();
            }

            else
            {
                Session["m"] = "Hesapta Yeterli Bakiye Bulunmuyor";
                return RedirectToAction("HesapBakiye1");
            }
            n.SaveChanges();


            return RedirectToAction("HesapBakiye1");
        }




        public ActionResult Transfer()
        {


            List<Hesaplar> list = n.Hesaplar.ToList();
            list = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + Session["Tc"] + "'and hesap_turu='" + "TR" + "'").ToList();


            return View(list);
        }
        public ActionResult Transfer1()
        {

            ViewModel model = new ViewModel();
            model.HesaplarList = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc !='" + Session["Tc"] + "'and hesap_turu='" + "TR" + "'").ToList();
            model.KullaniciHesaplari = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + Session["Tc"] + "'and hesap_turu='" + "TR" + "'").ToList();


            return View(model);
        }
        public ActionResult Gonder(int id,int id1,string bakiye)
        {
            Hesaplar user = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id);
            Hesaplar user1 = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id1);
            if (id != id1)
            {
                if (Convert.ToDouble(user.hesap_bakiye) >= Convert.ToDouble(bakiye))
                {
                    user1.hesap_bakiye = (Convert.ToDouble(bakiye) + Convert.ToDouble(user1.hesap_bakiye)).ToString();
                    user.hesap_bakiye = (Convert.ToDouble(user.hesap_bakiye) - Convert.ToDouble(bakiye)).ToString();
                    n.SaveChanges();
                }
                else
                {
                    Session["mesaj"] = "Hesapta Yeterli Bakiye Bulunmuyor";
                    return RedirectToAction("Transfer");
                }
            }
            else
            {
                Session["mesaj"] = "Hesaplar Aynı Olamaz";
                return RedirectToAction("Transfer");
            }



            return RedirectToAction("HesapBakiye");
        }

        public ActionResult Gonder1(int id, int id1, string bakiye)
        {
            Hesaplar user = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id);
            Hesaplar user1 = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id1);
            if (id != id1)
            {
                if (Convert.ToDouble(user.hesap_bakiye) >= Convert.ToDouble(bakiye))
                {
                    user1.hesap_bakiye = (Convert.ToDouble(bakiye) + Convert.ToDouble(user1.hesap_bakiye)).ToString();
                    user.hesap_bakiye = (Convert.ToDouble(user.hesap_bakiye) - Convert.ToDouble(bakiye)).ToString();
                    n.SaveChanges();
                }
                else
                {
                    Session["mesaj"] = "Hesapta Yeterli Bakiye Bulunmuyor";
                    return RedirectToAction("Transfer1");
                }
            }
            else
            {
                Session["mesaj"] = "Hesaplar Aynı Olamaz";
                return RedirectToAction("Transfer1");
            }



            return RedirectToAction("HesapBakiye");
        }

        public ActionResult dovizal()
        {

            ViewModel model = new ViewModel();
            model.HesaplarList = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc ='" + Session["Tc"] + "'and hesap_turu='"+"TR"+"'").ToList();
            model.KullaniciHesaplari = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + Session["Tc"] + "'and hesap_turu !='" + "TR" + "'").ToList();


            return View(model);
        }
        public ActionResult dovizsat()
        {

            ViewModel model = new ViewModel();
            model.HesaplarList = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc ='" + Session["Tc"] + "'and hesap_turu='" + "TR" + "'").ToList();
            model.KullaniciHesaplari = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='" + Session["Tc"] + "'and hesap_turu !='" + "TR" + "'").ToList();


            return View(model);
        }
        public ActionResult Gonder2(int id, int id1, string bakiye)
        {
            Hesaplar user = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id);
            Hesaplar user1 = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id1);
            Kur a = n.Kur.FirstOrDefault(x => x.kur_adi == user1.hesap_turu);
            if (id != id1)
            {



                if (Convert.ToDouble(user.hesap_bakiye) >= Convert.ToDouble(bakiye))
                {
                   
                    double k = (Convert.ToDouble(bakiye))/(Convert.ToDouble(a.kur_miktari));
                    k = Math.Round(k, 2);

                    user1.hesap_bakiye = (k + Convert.ToDouble(user1.hesap_bakiye)).ToString();
                    user.hesap_bakiye = (Convert.ToDouble(user.hesap_bakiye) - Convert.ToDouble(bakiye)).ToString();
                    n.SaveChanges();
                }
                else
                {
                    Session["mesaj"] = "Hesapta Yeterli Bakiye Bulunmuyor";
                    return RedirectToAction("dovizal");
                }
            }
            else
            {
                Session["mesaj"] = "Hesaplar Aynı Olamaz";
                return RedirectToAction("dovizal");
            }



            return RedirectToAction("HesapBakiye");
        }

        public ActionResult Gonder3(int id, int id1, string bakiye)
        {
            Hesaplar user = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id);
            Hesaplar user1 = n.Hesaplar.FirstOrDefault(x => x.hesap_id == id1);
            Kur a = n.Kur.FirstOrDefault(x => x.kur_adi == user1.hesap_turu);
            if (id != id1)
            {



                if (Convert.ToDouble(user1.hesap_bakiye) >= Convert.ToDouble(bakiye))
                {
                  
                    double k = (Convert.ToDouble(bakiye) * Convert.ToDouble(a.kur_miktari));
                    k = Math.Round(k, 2);
                    user.hesap_bakiye = (k + Convert.ToDouble(user.hesap_bakiye)).ToString();
                    user1.hesap_bakiye = (Convert.ToDouble(user1.hesap_bakiye) - Convert.ToDouble(bakiye)).ToString();
                    n.SaveChanges();
                }
                else
                {
                    Session["mesaj"] = "Hesapta Yeterli Bakiye Bulunmuyor";
                    return RedirectToAction("dovizsat");
                }
            }
            else
            {
                Session["mesaj"] = "Hesaplar Aynı Olamaz";
                return RedirectToAction("dovizsat");
            }



            return RedirectToAction("HesapBakiye");
        }

    }
}