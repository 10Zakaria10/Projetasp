using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ProjetAsp.Models;
using ProjetAsp.Services;

namespace ProjetAsp.Controllers
{
    public class AdminHomeController : Controller
    {

        // GET: AdminHome

        prjcontext prj = new prjcontext();
        IClient s0;
        IArticle s1;
        ICommande s2;
        ICategorie s3;

        public AdminHomeController(IClient s0,IArticle s1,ICategorie s3, ICommande s2)
        {
            this.s0 = s0;
            this.s1 = s1;
            this.s2 = s2;
            this.s3 = s3;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GestionClient()
        {

            return View("GestionClient", s0.getAllClient());

        }

        [HttpPost]
        public ActionResult GestionClient(string searchitem)
        {
            if (string.IsNullOrEmpty(searchitem))
            {
                return View("GestionClient", s0.getAllClient());

            }
            else
            {
                return View("GestionClient", s0.getAllClientStartWith(searchitem));

            }
        }


        public ActionResult EditClient(int id)
        {
            return View("EditClient",s0.GetClienById(id));
        }

        [HttpPost]
        public ActionResult EditClient(Client cl)
        {
            s0.EditClient(cl);

            return RedirectToAction("GestionClient",s0.getAllClient());

        }


        public ActionResult DeleteClient(int id)
        {
            s0.DeleteClient(id);
            return View("GestionClient", s0.getAllClient());

        }


            

        public ActionResult GestionArticle()
        {
            ViewBag.e = new SelectList(prj.Categories, "refCat", "nomCat");
            return View("GestionArticle",s1.getAllArticle());
        }


        [HttpPost]
        public ActionResult ChercherArticle(FormCollection formx)
        {
            ViewBag.e = new SelectList(prj.Categories, "refCat", "nomCat");

            try { 
            int contenu = Int32.Parse(formx["contenu"]);

            return View("GestionArticle",s1.getArticleByIdV2(contenu));
            }catch(Exception e)
            {
                return View("GestionArticle", s1.getAllArticle());

            }
        }

        public ActionResult DeleteArticle(int id)
        {
            s1.DeleteArticle(id);
            return RedirectToAction("GestionArticle");

        }


        public ActionResult EditArticle(int id)
        {

            return View("EditArticle", s1.getArticleById(id));

        }

        [HttpPost]
        public ActionResult EditArticle(Article cl)
        {
            s1.EditArticle(cl);
            return RedirectToAction("GestionArticle");

        }

        
        public ActionResult CreateArticle()
        {
            return View("CreateArticle");
        }
        [HttpPost]
        public ActionResult CreateArticle(Article art)
        {
            try {
               
                String filename = System.IO.Path.GetFileNameWithoutExtension(art.Imagefile.FileName);
                String extension = System.IO.Path.GetExtension(art.Imagefile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                art.photo = "~/Images/" + filename;
                filename = System.IO.Path.Combine(Server.MapPath("~/Images"), filename);
                art.Imagefile.SaveAs(filename);
                s1.CreateArticle(art);
                
            }catch(Exception e)
            {
                ViewBag.err = "ERREUR";
                return View("CreateArticle");
            }
            return RedirectToAction("GestionArticle");
        }



        public ActionResult StatistiquePage()
        {

            return View("StatistiquePage");
        }

        public ActionResult myChart()
        {
            var art = s3.getAllCategorie();

            List<String> xv = new List<string>();

            List<int> yv = new List<int>();

            foreach (var x in art)
            {
                var commande = s2.getCommandebyCat(x);

                xv.Add(x.nomCat);
                yv.Add(commande.Count());

            }

            new Chart(width: 800, height: 300).AddTitle(ProjetAsp.Resources.HomeTexts.TitireStatistique).AddSeries(

                chartType: "Column",
                xValue: xv.ToArray(),
                yValues: yv.ToArray()
                ).Write("png");

            return null;
        }


        public ActionResult ChartProduit()
        {
            var art = s1.getAllArticle();

            List<String> xv = new List<string>();

            List<int> yv = new List<int>();

            foreach (var x in art)
            {
                var commande = s2.getCommandebyArticle(x);

                xv.Add(x.designation);
                yv.Add(commande.Count());

            }

            var Mychart = new Chart(width: 900, height: 600)
           .AddTitle(ProjetAsp.Resources.HomeTexts.TitireStatistiqueRound).AddLegend(" ")
             .AddSeries("Default", chartType: "pie",
                 xValue: xv, xField: "noob",
                 yValues: yv)
            .GetBytes("png");
            return File(Mychart, "image/png");
        }

        public ActionResult GestionCat()
        {
            return View(s3.getAllCategorie());
        }

        public ActionResult DeleteCat(int id)
        {
            s3.DeleteCat(id);
            return RedirectToAction("GestionCat");

        }

        public ActionResult EditCat(int id)
        {

            return View(s3.getCatById(id));
        }

        [HttpPost]
        public ActionResult EditCat(Categorie cat)
        {

            s3.EditCat(cat);
            return RedirectToAction("GestionCat");
        }

        public ActionResult CreateCat ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCat(Categorie cat)
        {
            s3.CreateCat(cat);
            return RedirectToAction("GestionCat");
        }
    }
}