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
    public class MakbuzController: Controller
    {
        BankaEntities1 n = new BankaEntities1();


        [HttpGet]



        public ActionResult Makbuzekle()
        {
            Makbuz k = new Makbuz();
            ViewBag.islemler = n.İslemler.SqlQuery(sql: "Select * From İslemler where temsilci_Tc='" + Session["Tc"] + "'");

            return View(k);
        }

        [HttpPost]
        public ActionResult Makbuzekle(Makbuz k)
        {

            n.Makbuz.Add(k);
            n.SaveChanges();
            return Redirect("/Admin/Index");



        }
        [HttpGet]


        public ActionResult MakbuzSil()
        {

            List<Makbuz> list = n.Makbuz.ToList();
            list = n.Makbuz.SqlQuery(sql: "Select * From Makbuz  INNER JOIN İslemler ON Makbuz.islem_id=İslemler.islem_id where temsilci_Tc='" + Session["Tc"] + "'").ToList();

            return View(list);

        }

        public void Sil(int id)
        {
            Makbuz user = n.Makbuz.FirstOrDefault(x => x.makbuz_id == id);
            n.Makbuz.Remove(user);
            n.SaveChanges();


        }


   




    }
}