using ProjetAsp.Models;
using ProjetAsp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ProjetAsp.Controllers
{
    public class LangageController : Controller
    {

        IArticle s1;
        ICommande s2;
        ICategorie s3;
        IFavoris s4;
        public LangageController(IArticle s1, ICommande s2, ICategorie s3, IFavoris s4)
        {

            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            ViewBag.categories = s3.getAllCategorie();
            ViewBag.articles = s1.getAllArticle();
            ViewBag.TopSArticle = s1.getTopSeeledarticle();
            ViewBag.num = s2.countCommandeClient(9999);
            ViewBag.charts = s2.getCommandeById(9999);
            ViewBag.favoris = s4.getFavorisClient(9999);
            ViewBag.qtqfavoris = s4.totalFavorisClient(9999);
            ViewBag.totalcart = s2.totalClient(9999);
            ViewBag.LastCommande = s2.getLastCommande();





        }
        // GET: Langage
        public ActionResult Index()
        {
            try
            {
                if (Session["person"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                Client cli = (Client)Session["person"];
                ViewBag.cli = cli;

                return View("ChooseLanguage");
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }


        public ActionResult Change(String LanguageAbbrevation)
        {
            try
            {
                if (LanguageAbbrevation != null)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
                }

                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = LanguageAbbrevation;
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "AdminHome");
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }

        
    }
}