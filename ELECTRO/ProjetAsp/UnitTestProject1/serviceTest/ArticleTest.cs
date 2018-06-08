using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetAsp.Models;
using System.Linq;

namespace UnitTestProject1.serviceTest
{
  
    [TestClass]
    public class ArticleTest
    {
        List<Article> listeArticle = new List<Article>();

        public ArticleTest()
        {
// public Article(int numArticle, string designation,double? prixU,int? stock, string photo, string statut,int? promo, string description, string detail,DateTime? datee,double? rating,string marque,int? vendu,int? refcat)

            listeArticle.Add(new Article(1,"desi1", 111, 1,  "photo1", "statut1", 1, "description1", "det1", DateTime.Now, 11.1, "marque1",111, 1));
            listeArticle.Add(new Article(2,"desi2", 222, 2, "photo2", "statut2", 2, "description2", "det2", DateTime.Now, 22.2, "marque2", 222, 2));

        }

        [TestMethod]
        public void CreateArticle(Article art)
        {
            listeArticle.Add(art);
            CollectionAssert.Contains(listeArticle, art);

        }

        [TestMethod]
        public void DeleteArticle(int id)
        {
            Article art = null;
            foreach(var item in listeArticle)
            {
                if(item.numArticle == id)
                {
                    art = item;
                }
            }

            listeArticle.Remove(art);
            CollectionAssert.DoesNotContain(listeArticle, art);
        }

        [TestMethod]
        public void EditArticle(Article cl)
        {
            foreach(var item in listeArticle)
            {
                if(item.numArticle == cl.numArticle)
                {
                    item.designation = cl.designation;
                    item.prixU = cl.prixU;
                    item.statut = cl.statut;
                    item.photo = cl.photo;
                    item.stock = cl.stock;
                    item.refcat = cl.refcat;
                }
            }
            CollectionAssert.Contains(listeArticle, cl);
        }

        [TestMethod]
        public IEnumerable<Article> getAllArticle()
        {

            return listeArticle;
        }

        [TestMethod]
        public IEnumerable<Article> getArticleByCatorBrand(string cat, string brand, double prixmin, double prixmax)
        {
            return null;
        }

        [TestMethod]
        public Article getArticleById(int id)
        {
            Article art = null;
            foreach(var item in listeArticle)
            {
                if(item.numArticle == id)
                {
                    art = item;
                }
            }
            Assert.IsNotNull(art,"cet article n'existe pas");
            return art;
        }

        [TestMethod]
        public IEnumerable<Article> getArticleByrefCat(int id)
        {
            List<Article> liste = new List<Article>();
            foreach(var item in listeArticle)
            {
                if(item.refcat == id)
                {
                    liste.Add(item);
                }
            }
            CollectionAssert.AllItemsAreNotNull(liste);
            return liste;
        }

        [TestMethod]
        public IEnumerable<string> getBrand()
        {
            IEnumerable<string> brandList = (from d in listeArticle select d.marque).Distinct();
            return brandList;
        }

        [TestMethod]
        public int getcat(int artid)
        {
            return 0;
        }









    }
}
