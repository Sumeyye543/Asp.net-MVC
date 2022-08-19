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
    public class KrediController:Controller
    {

        BankaEntities1 n = new BankaEntities1();

        public ActionResult Borc()
        {

            List<Kredi_Borclari> list = n.Kredi_Borclari.ToList();
            list = n.Kredi_Borclari.SqlQuery(sql: "Select * From Kredi_Borclari where musteri_Tc='" + Session["Tc"] + "'").ToList();

            return View(list);

        }
        public void Sil(int id)
        {
            Kredi_Borclari user = n.Kredi_Borclari.FirstOrDefault(x => x.kredi_id == id);
            n.Kredi_Borclari.Remove(user);
            n.SaveChanges();


        }





    }
}