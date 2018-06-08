using ProjetAsp.Models;
using ProjetAsp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetAsp.Controllers
{
    public class DevisController : Controller
    {

        IArticle s1;
        ICommande s2;
        ICategorie s3;
        IFavoris s4;
        ICommentaire s5;
        IClient s0;

        public DevisController(IArticle s1, ICommande s2, ICategorie s3, IFavoris s4, ICommentaire s5,IClient s0)
        {
            this.s0 = s0;
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
            this.s4 = s4;
            this.s5 = s5;

            ViewBag.categories = s3.getAllCategorie();
            ViewBag.sessionkayna = true;




        }

        // GET: Devis
        public ActionResult Index(int idclient)
        {

            try
            {
                if (Session["person"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                ViewBag.num = s2.countCommandeClient(idclient);
                ViewBag.charts = s2.getCommandeById(idclient);
                ViewBag.favoris = s4.getFavorisClient(idclient);
                ViewBag.qtqfavoris = s4.totalFavorisClient(idclient);
                ViewBag.totalcart = s2.totalClient(idclient);
                return View(s0.GetClienById(idclient));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        

    

    }
}