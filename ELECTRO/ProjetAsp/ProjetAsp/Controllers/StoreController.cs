using ProjetAsp.Models;
using ProjetAsp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetAsp.Controllers
{
    public class StoreController : Controller
    {

        IArticle s1;
        ICommande s2;
        ICategorie s3;
        IFavoris s4;
        int num = 4;

        public StoreController(IArticle s1, ICommande s2, ICategorie s3, IFavoris s4)
        {

            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            ViewBag.sessionkayna = true;

            ViewBag.categories = s3.getAllCategorie();
            ViewBag.articles = s1.getAllArticle();
            ViewBag.TopSArticle = s1.getTopSeeledarticle();
            ViewBag.brands = s1.getBrand();






        }
        // GET: Store
        public ActionResult Index()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];

            ViewBag.num = s2.countCommandeClient(cli.numClient);
            ViewBag.charts = s2.getCommandeById(cli.numClient);
            ViewBag.totalcart = s2.totalClient(cli.numClient);
            ViewBag.favoris = s4.getFavorisClient(cli.numClient);
            ViewBag.qtqfavoris = s4.totalFavorisClient(cli.numClient);

            return View("Store");
        }

        public ActionResult filt(int refcat)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];

            ViewBag.num = s2.countCommandeClient(cli.numClient);
            ViewBag.charts = s2.getCommandeById(cli.numClient);
            ViewBag.totalcart = s2.totalClient(cli.numClient);
            ViewBag.favoris = s4.getFavorisClient(cli.numClient);
            ViewBag.qtqfavoris = s4.totalFavorisClient(cli.numClient);
            ViewBag.articles = s1.getArticleByrefCat(refcat);
            return View("Store");
        }

        [HttpPost]
        public ActionResult Indx(FormCollection clx)
        {



            Client cli = (Client)Session["person"];

            ViewBag.num = s2.countCommandeClient(cli.numClient);
            ViewBag.charts = s2.getCommandeById(cli.numClient);
            ViewBag.totalcart = s2.totalClient(cli.numClient);
            ViewBag.favoris = s4.getFavorisClient(cli.numClient);
            ViewBag.qtqfavoris = s4.totalFavorisClient(cli.numClient);

            string catfilt = clx["thiddencat"];
            string brandfilt = clx["thiddenbrand"];

            double prixmin = Double.Parse(clx["pricemin"]);
            double prixmax = Double.Parse(clx["pricemax"]);

            ViewBag.articles = s1.getArticleByCatorBrand(catfilt, brandfilt, prixmin, prixmax);



            return View("Store");
        }




        public ActionResult Favoris(int artid)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }
            Client cli = (Client)Session["person"];

            s4.addtoFavoris(artid, cli.numClient);

            ViewBag.favoris = s4.getFavorisClient(cli.numClient);
            ViewBag.qtqfavoris = s4.totalFavorisClient(cli.numClient);



            // ici je dois ajoute hadak au favoris apres avoir cree f la bd

            return RedirectToAction("Index");
        }

        public ActionResult AddToChart(int artid)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }
            Client cli = (Client)Session["person"];

            s2.AjouteCommande(cli.numClient, artid, 1);

            ViewBag.num = s2.countCommandeClient(cli.numClient);
            ViewBag.charts = s2.getCommandeById(cli.numClient);
            ViewBag.totalcart = s2.totalClient(cli.numClient);


            return RedirectToAction("Index");
        }

        public ActionResult Hotdeal()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];

            ViewBag.num = s2.countCommandeClient(cli.numClient);
            ViewBag.charts = s2.getCommandeById(cli.numClient);
            ViewBag.totalcart = s2.totalClient(cli.numClient);
            ViewBag.favoris = s4.getFavorisClient(cli.numClient);
            ViewBag.qtqfavoris = s4.totalFavorisClient(cli.numClient);
            ViewBag.articles = s1.getHotdeals();
            ViewBag.pg = "HotDeals";

            return View("Store");

        }





    }
}