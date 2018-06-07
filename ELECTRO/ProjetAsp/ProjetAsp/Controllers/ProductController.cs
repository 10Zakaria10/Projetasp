using ProjetAsp.Models;
using ProjetAsp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetAsp.Controllers
{
    public class ProductController : Controller
    {
        IArticle s1;
        ICommande s2;
        ICategorie s3;
        IFavoris s4;
        ICommentaire s5;


        public ProductController(IArticle s1, ICommande s2, ICategorie s3, IFavoris s4 , ICommentaire s5)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            this.s5 = s5;

            ViewBag.categories = s3.getAllCategorie();

            ViewBag.sessionkayna = true;




        }

        // GET: Product
        public ActionResult Index(int artid)
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


            ViewBag.RelatedArticle = s1.getArticleByrefCat(s1.getcat(artid));
            ViewBag.Reviews = s5.GetCommentairebyArticle(artid);
            ViewBag.totalReview = s5.totalcomm(artid);
            return View(s1.getArticleById(artid));
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

            return RedirectToAction("Index", new { artid = artid});
        }
        [HttpPost]
        public ActionResult addCart(int artid,int qte)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }
            Client cli = (Client)Session["person"];

            s2.AjouteCommande(cli.numClient, artid,qte);

            ViewBag.num = s2.countCommandeClient(cli.numClient);
            ViewBag.charts = s2.getCommandeById(cli.numClient);
            ViewBag.totalcart = s2.totalClient(cli.numClient);

            // ici je dois ajoute hadak au favoris apres avoir cree f la bd

            return RedirectToAction("Index", new { artid = artid });
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

            // ici je dois ajoute hadak au favoris apres avoir cree f la bd

            return RedirectToAction("Index", new { artid = artid });
        }

        public ActionResult Review(string nom , string email , string descr , int rating, int artid)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            s5.AjoutezCommentaire(nom, descr, rating, artid);

            return RedirectToAction("Index", new { artid = artid });

        }
    }

    
}