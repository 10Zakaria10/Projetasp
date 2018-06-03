using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetAsp.Models;

namespace ProjetAsp.Services
{
    public class FavorisImp : IFavoris
    {
        PrjContext2 prj = new PrjContext2();
        public void addtoFavoris(int id, int idclient)
        {
            Favori fav = new Favori();
            fav.idclient = idclient;
            fav.idarticle = id;

            prj.Favoris.Add(fav);
            prj.SaveChanges();


        }

        public void DeletefromFavoris(int id, int idclient)
        {
            var x = (from c in prj.Favoris where c.idarticle == id && c.idclient == idclient select c);
           
            prj.Favoris.RemoveRange(x);
            prj.SaveChanges();
        }

        public IEnumerable<Favori> getFavorisClient(int id)
        {
           return (from c in prj.Favoris where c.idclient == id select c).Distinct();
           
        }

        public int totalFavorisClient(int id)
        {
            var x = (from c in prj.Favoris where c.idclient == id select c).Distinct();
            
            return x.Count(); 
        }
    }
}