using Banka.Models;
using Banka.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Banka.Controllers
{
    public class AdminController :Controller
    {
        BankaEntities1 n = new BankaEntities1();
       
        [HttpGet]

        [Authorize(Roles = "M,A,T")]
        public ActionResult Index()
        {
           

            ViewModel model = new ViewModel();
            model.MusteriList = n.Musteri.ToList();
            model.MusteriTemsilcisiList = n.Musteri_Temsilcisi.ToList();
            model.HesaplarList = n.Hesaplar.ToList();
            model.IslemlerList = n.son5islem().ToList();
            model.MakbuzList = n.son5makbuz().ToList();
            model.Banka_ToplamList = n.Banka_Toplam.ToList();
            model.KullaniciHesaplari= n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar where musteri_Tc='"+ Session["Tc"] +"'").ToList();
            model.KullaniciBilgileri = n.Musteri.SqlQuery(sql :"Select * From Musteri where musteri_Tc='" + Session["Tc"] + "'").ToList();

            model.TemsilciMusteri= n.Musteri.SqlQuery(sql: "Select * From Musteri where temsilci_Tc='" + Session["Tc"].ToString() + "'").ToList();
            
            model.TemsilciHesaplari = n.Hesaplar.SqlQuery(sql: "Select * From Hesaplar INNER JOIN Musteri ON Musteri.musteri_Tc=Hesaplar.musteri_Tc where temsilci_Tc='" + Session["Tc"] + "'").ToList();

            model.KurList = n.Kur.ToList();


            return View(model); 
        }


   






        [HttpPost]
        public JsonResult GetChartData()
        {
            try
            {
                decimal[] SeriesVal = new decimal[12];
                List < Banka_Toplam > a = n.Banka_Toplam.ToList();
                int i = 0;
                foreach (var item in a)
                {
                    SeriesVal[i] = Convert.ToDecimal(item.toplam_bakiye);
                    i++;
                }
           

                
                string[] LabelsVal = { "Ocak", "Şubat", "Mart", "Nisan","Mayıs","Haziran","Temmuz","Ağustos","Eylül","Ekim","Kasım","Aralık" };
                return Json(new { success = true, series = SeriesVal, labels = LabelsVal, message = "success.!" }, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}