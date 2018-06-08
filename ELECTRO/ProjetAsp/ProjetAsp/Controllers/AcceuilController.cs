using ProjetAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetAsp.Services;
namespace ProjetAsp.Controllers
{
    public class AcceuilController : Controller
    {
        // GET: Acceuil

        PrjContext2 prj = new PrjContext2();

        IArticle s1;
        ICommande s2;
        ICategorie s3;

        public AcceuilController(IArticle s1, ICommande s2 ,ICategorie s3)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
        }



        public ActionResult Index()
        {
            try
            {
                Client person = (Client)Session["person"];
                return View(person);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        public ActionResult Consultez()
        {
            try
            {
                return View(s1.getAllArticle());
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                return View(s1.getArticleById(id));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }


        public ActionResult Visualisez()
        {
            try
            {
                Client person = (Client)Session["person"];

                var x = s2.getCommandeById(person.numClient);

                double prix = 0;

                foreach (var i in x)
                {


                    prix = prix + (double)i.Article.prixU * (double)i.qtearticle;
                }
                ViewBag.prix = prix;


                return View(x);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        public ActionResult DeleteArt(int id)
        {
            try
            {
                Client person = (Client)Session["person"];
                s2.DeleteArt(id, person);

                return RedirectToAction("Visualisez");
            }
            catch (Exception)
            {
                return View("Error");
            }
            

        }

        public ActionResult AjouterPanier()
        {
            try
            {
                ViewBag.e = new SelectList(s3.getAllCategorie(), "refCat", "nomCat");

                Client person = (Client)Session["person"];

                var x = s2.getCommandeById(person.numClient);

                return View(x);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        [HttpPost]
        public ActionResult AjouterPanier(FormCollection formx)
        {
            try
            {
                ViewBag.e = new SelectList(s3.getAllCategorie(), "refCat", "nomCat");

                Client person = (Client)Session["person"];

                var x = s2.getCommandeById(person.numClient);

                int contenu = Int32.Parse(formx["contenu"]);
                int qte = Int32.Parse(formx["qte"]);
                int stock = Int32.Parse(formx["stock"]);


                if (stock < qte)
                {
                    ViewBag.err = "Stock insuffisant";
                }
                else
                {
                    s2.AjouteCommande(person.numClient, contenu, qte);
                }

                return View(x);
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        public JsonResult GetArticle(int ID)

        {
                prj.Configuration.ProxyCreationEnabled = false;
                return Json(prj.Articles.Where(p => p.refcat == ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStock(int ID)

        {
            prj.Configuration.ProxyCreationEnabled = false;
            return Json(prj.Articles.Where(p => p.numArticle == ID), JsonRequestBehavior.AllowGet);
        }

        



    }
}