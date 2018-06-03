using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetAsp.Models;

namespace ProjetAsp.Services
{
    public class CategorieImp : ICategorie
    {
        prjcontext prj = new prjcontext();
        public void CreateCat(Categorie cat)
        {
            prj.Categories.Add(cat);
            prj.SaveChanges();
        }

        public void DeleteCat(int id)
        {
            var x = (from c in prj.Categories where c.refcat == id select c).SingleOrDefault();
            var xx = (from c in prj.Articles where c.refcat == id select c);
           var xxx = (from c in prj.Commandes where c.Article.refcat == id select c);
            prj.Commandes.RemoveRange(xxx);
            prj.Articles.RemoveRange(xx);
            prj.Categories.Remove(x);
            prj.SaveChanges();
        }

        public void EditCat(Categorie cat)
        {

            var x = (from c in prj.Categories where c.refcat == cat.refcat select c).SingleOrDefault();
           
            x.refcat = cat.refcat;
            x.nomCat = cat.nomCat;
            prj.SaveChanges();
        }

        public IEnumerable<Categorie> getAllCategorie()
        {
            return prj.Categories;
        }

        public Categorie getCatById(int id)
        {
           return  (from c in prj.Categories where c.refcat == id select c).SingleOrDefault();
         
        }

    
    }
}