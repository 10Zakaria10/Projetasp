using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetAsp.Models;

namespace ProjetAsp.Services
{
    public class CommandeImp : ICommande
    {
        PrjContext2 prj = new PrjContext2();

        public void AjouteCommande(int person, int idarticle, int qte)
        {
            DateTime localDate = DateTime.Now;

            Commande commande = new Commande();
            commande.datecmd = localDate;
            commande.numarticle = idarticle;
            commande.numclient = person;
            commande.qtearticle = qte;

            prj.Commandes.Add(commande);


            Article art = (from c in prj.Articles where c.numArticle == idarticle select c).SingleOrDefault();
            art.stock = art.stock - qte;
            art.vendu = art.vendu + 1;

            prj.SaveChanges();


        }

        public void DeleteCommande(int idclient, int idart)
        {

            var xx = (from c in prj.Commandes where c.numarticle==idart && c.numclient==idclient select c);
            prj.Commandes.RemoveRange(xx);
            prj.SaveChanges();
        }

        public int countCommandeClient(int idclient)
        {

            var x = (from c in prj.Commandes where c.numclient == idclient select c);

            return x.Count();
        }

        public void DeleteArt(int id,Client person)
        {

            var x = (from c in prj.Commandes where c.numarticle == id && c.numclient == person.numClient select c);

            prj.Commandes.RemoveRange(x);
            prj.SaveChanges();
        }

        public IEnumerable<Commande> getCommandebyArticle(Article x)
        {

            return  from c in prj.Commandes where c.numarticle == x.numArticle select c;

        }

        public IEnumerable<Commande> getCommandebyCat(Categorie x)
        {
            return from c in prj.Commandes where c.Article.Categorie.refcat == x.refcat select c;
        }

        public IEnumerable<Commande> getCommandeById(int person)
        {
           return from c in prj.Commandes where c.numclient == person select c;
        }

        public IEnumerable<Commande> getLastCommande()
        {
            return from c in prj.Commandes orderby c.numcmd descending select c;
        }

        public float totalClient(int idclient)
        {
           var x= from c in prj.Commandes where c.numclient==idclient select c;
            float somme = 0;
            foreach ( var i in x )
            {
                somme =(float)( somme + i.Article.prixU*i.qtearticle);
            }
            return somme;
        }

        public double thisweek()
        {
            DateTime localDate = DateTime.Now;
            DateTime date =localDate.AddDays(-7);

            var x = from c in prj.Commandes where c.datecmd < localDate && c.datecmd > date select c;
            double somme = 0;

            foreach(var i in x )
            {

                somme = Convert.ToDouble(somme + i.Article.prixU*i.qtearticle);
            }

            return somme;

        }

        public double totalearn()
        {
            var x = from c in prj.Commandes  select c;
            double somme = 0;

            foreach (var i in x)
            {

                somme = Convert.ToDouble(somme + i.Article.prixU * i.qtearticle);
            }

            return somme;
        }
    }
}