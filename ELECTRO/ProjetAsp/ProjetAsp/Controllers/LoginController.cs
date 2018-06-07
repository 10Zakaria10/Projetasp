using ProjetAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjetAsp;
using ProjetAsp.Services;

namespace ProjetAsp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        IClient s0;
        IArticle s1;
        ICommande s2;
        ICategorie s3;
        IFavoris s4;

        public LoginController(IClient s0, IArticle s1, ICommande s2, ICategorie s3, IFavoris s4)
        {
            this.s0 = s0;
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            ViewBag.categories = s3.getAllCategorie();
            ViewBag.articles = s1.getAllArticle();
            ViewBag.TopSArticle = s1.getTopSeeledarticle();

            ViewBag.sessionkayna = false;
        }


        public ActionResult Index()
        {
            
            ViewBag.num = s2.countCommandeClient(9999);
            ViewBag.charts = s2.getCommandeById(9999);
            ViewBag.favoris = s4.getFavorisClient(9999);
            ViewBag.qtqfavoris = s4.totalFavorisClient(9999);
            ViewBag.totalcart = s2.totalClient(9999);

            return View("Login");
        }



        [HttpPost]
        public ActionResult Index(Client person)
        {
            
            if (s0.SeConnecter(person))
            {

                if(s0.isAdmin(person))
                {
                    Session["person"] = s0.GetClienById(person);

                    return RedirectToAction("Index", "AdminHome");
                    
                 }
                 else
                 {
                         Session["person"] =s0.GetClienById(person) ;
                         return RedirectToAction("Index", "Home");
                    
                 }
                 
            }

            else
                ViewBag.err = "Login ou mdp incorrect";

            ViewBag.num = s2.countCommandeClient(9999);
            ViewBag.charts = s2.getCommandeById(9999);
            ViewBag.favoris = s4.getFavorisClient(9999);
            ViewBag.qtqfavoris = s4.totalFavorisClient(9999);
            ViewBag.totalcart = s2.totalClient(9999);
            return View("Login");

        }

        public ActionResult Inscription()
        {

            ViewBag.num = s2.countCommandeClient(9999);
            ViewBag.charts = s2.getCommandeById(9999);
            ViewBag.favoris = s4.getFavorisClient(9999);
            ViewBag.qtqfavoris = s4.totalFavorisClient(9999);
            ViewBag.totalcart = s2.totalClient(9999);

            return View();
        }


        [HttpPost]
        public ActionResult Inscription(Client person)
        {


            if (ModelState.IsValid)
            {
                s0.Inscription(person);
            return RedirectToAction("Index");
            }

            ViewBag.num = s2.countCommandeClient(9999);
            ViewBag.charts = s2.getCommandeById(9999);
            ViewBag.favoris = s4.getFavorisClient(9999);
            ViewBag.qtqfavoris = s4.totalFavorisClient(9999);
            ViewBag.totalcart = s2.totalClient(9999);
            return View();
        }

    }
}