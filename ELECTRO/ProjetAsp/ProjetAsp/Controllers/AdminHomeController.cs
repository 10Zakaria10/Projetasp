﻿using System;
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

        PrjContext2 prj = new PrjContext2();
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
            ViewBag.categories = s3.getAllCategorie();
            ViewBag.articles = s1.getAllArticle();
            ViewBag.TopSArticle = s1.getTopSeeledarticle();
            ViewBag.num = s2.countCommandeClient(9999);
            ViewBag.charts = s2.getCommandeById(9999);
           ViewBag.totalcart = s2.totalClient(9999);
            ViewBag.LastCommande = s2.getLastCommande();
            ViewBag.nbclient = s0.countClients();
            ViewBag.nbarticlevendu = s1.nbarticleVendu();
            ViewBag.thisweek = s2.thisweek();
            ViewBag.totalearn = s2.totalearn();
        }

        public ActionResult Index()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            if (cli.role == 1) { 
            ViewBag.cli = cli;

            return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public PartialViewResult AfficherEtd()
        {
            string id = Request.Form["searchitem"];

            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_AfficherDetails", s0.getAllClient());

            }
            else
            {
                return PartialView("_AfficherDetails", s0.getAllClientStartWith(id));
               

            }
            

        }

        public ActionResult GestionClient()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            return View("GestionClient", s0.getAllClient());

        }

        [HttpPost]
        public ActionResult GestionClient(string searchitem)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

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
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            return View("EditClient",s0.GetClienById(id));
        }

        [HttpPost]
        public ActionResult EditClient(Client cl)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            s0.EditClient(cl);

            return RedirectToAction("GestionClient",s0.getAllClient());

        }


        public ActionResult DeleteClient(int id)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            s0.DeleteClient(id);
            return View("GestionClient", s0.getAllClient());

        }


            

        public ActionResult GestionArticle()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            ViewBag.e = new SelectList(prj.Categories, "refCat", "nomCat");
            return View("GestionArticle",s1.getAllArticle());
        }


        [HttpPost]
        public ActionResult ChercherArticle(FormCollection formx)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

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
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            s1.DeleteArticle(id);
            return RedirectToAction("GestionArticle");

        }


        public ActionResult EditArticle(int id)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            return View("EditArticle", s1.getArticleById(id));

        }

        [HttpPost]
        public ActionResult EditArticle(Article cl)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            s1.EditArticle(cl);
            return RedirectToAction("GestionArticle");

        }




        public ActionResult CreateArticle()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

            return View("CreateArticle");
        }
        [HttpPost]
        public ActionResult CreateArticle(Article art)
        {
            try
            {
                String filename = System.IO.Path.GetFileNameWithoutExtension(art.Imagefile.FileName);
                String extension = System.IO.Path.GetExtension(art.Imagefile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                art.photo = "~/Images/" + filename;
                filename = System.IO.Path.Combine(Server.MapPath("~/Images"), filename);
                art.Imagefile.SaveAs(filename);
                s1.CreateArticle(art);
            }
            catch (Exception e)
            {
                ViewBag.err = "ERREUR";
                return View("CreateArticle");
            }
            return RedirectToAction("GestionArticle");
        }



        public ActionResult StatistiquePage()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;
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

            new Chart(width: 1200, height: 400).AddTitle(ProjetAsp.Resources.HomeTexts.TitireStatistique).AddSeries(

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

            var Mychart = new Chart(width: 1200, height: 600)
           .AddTitle(ProjetAsp.Resources.HomeTexts.TitireStatistiqueRound).AddLegend(" ")
             .AddSeries("Default", chartType: "pie",
                 xValue: xv, xField: "noob",
                 yValues: yv)
            .GetBytes("png");
            return File(Mychart, "image/png");
        }

        public ActionResult GestionCat()
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;
            return View(s3.getAllCategorie());
        }

        public ActionResult DeleteCat(int id)
        {
            s3.DeleteCat(id);
            return RedirectToAction("GestionCat");

        }

        public ActionResult EditCat(int id)
        {
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;

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
            if (Session["person"] == null)
            {
                return RedirectToAction("Index", "Login");


            }

            Client cli = (Client)Session["person"];
            ViewBag.cli = cli;
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