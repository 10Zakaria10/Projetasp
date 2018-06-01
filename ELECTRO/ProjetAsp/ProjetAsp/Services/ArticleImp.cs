using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetAsp.Models;

namespace ProjetAsp.Services
{
    public class ArticleImp : IArticle
    {
        PrjContext2 prj = new PrjContext2();


        public void CreateArticle(Article art)
        {
            prj.Articles.Add(art);
            prj.SaveChanges();

        }

        public void DeleteArticle(int id)
        {

            var x = (from c in prj.Articles where c.numArticle == id select c).SingleOrDefault();
            var xx = (from c in prj.Commandes where c.numarticle == id select c);
            prj.Commandes.RemoveRange(xx);
            prj.Articles.Remove(x);
            prj.SaveChanges();

        }

        public void EditArticle(Article cl)
        {
            var x = (from c in prj.Articles where c.numArticle == cl.numArticle select c).SingleOrDefault();
            x.designation = cl.designation;
            x.numArticle = cl.numArticle;
            x.photo = cl.photo;
            x.prixU = cl.prixU;
            x.refcat = cl.refcat;
            x.stock = cl.stock;
            prj.SaveChanges();


        }

        public IEnumerable<Article> getAllArticle()
        {
            return prj.Articles.ToList();
        }

        public IEnumerable<Article> getArticleByCatorBrand(string cat, string brand, double prixmin, double prixmax)
        {

            String[] arrcat = cat.Split(',');
            String[] arrbrand = brand.Split(',');

            if (arrcat[0] == "all" || arrbrand[0] == "all")
            {

                if (arrcat[0] == "all")
                {
                    return (from c in prj.Articles
                            where arrbrand.Contains(c.marque) && c.prixU <= prixmax && c.prixU >= prixmin
                            select c);
                }
                else
                {
                    return (from c in prj.Articles
                            where arrcat.Contains(c.Categorie.nomCat) && c.prixU <= prixmax && c.prixU >= prixmin
                            select c);
                }
            }
            else
            {
                return (from c in prj.Articles
                        where arrcat.Contains(c.Categorie.nomCat) && arrbrand.Contains(c.marque)
                        && c.prixU <= prixmax && c.prixU >= prixmin
                        select c);

            }







        }

        public Article getArticleById(int id)
        {
            return (from c in prj.Articles where c.numArticle == id select c).SingleOrDefault();

        }

        public IEnumerable<Article> getArticleByIdV2(int id)
        {
            return from c in prj.Articles where c.numArticle == id select c;

        }


        public IEnumerable<Article> getArticleByrefCat(int id)
        {
            return from c in prj.Articles where c.refcat == id select c;

        }

        public IEnumerable<String> getBrand()
        {
            return (from c in prj.Articles select c.marque).Distinct();
        }

        public int getcat(int artid)
        {
            Article x = (from c in prj.Articles where c.numArticle == artid select c).SingleOrDefault();

            return (Int32)x.refcat;
        }

        public IEnumerable<Article> getHotdeals()
        {
            return from c in prj.Articles where c.promo >=40  orderby c.promo descending select c;

        }

        public IEnumerable<Article> getTopSeeledarticle()
        {
            return from c in prj.Articles orderby c.vendu descending select c;
        }

        public IEnumerable<Article> getTopSeelingArticleByrefCat(int id)
        {
            return from c in prj.Articles where c.refcat == id orderby c.vendu descending select c;
        }
    }
}