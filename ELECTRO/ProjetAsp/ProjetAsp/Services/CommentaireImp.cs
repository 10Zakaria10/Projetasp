using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetAsp.Models;

namespace ProjetAsp.Services
{
    public class CommentaireImp : ICommentaire
    {
        prjcontext prj = new prjcontext();
        public void AjoutezCommentaire(string nom, string comm, float rating, int numart)
        {
            DateTime localDate = DateTime.Now;

            Commentaire commentaire = new Commentaire();
            commentaire.date = localDate;
            commentaire.nomclient = nom;
            commentaire.commentaire1 = comm;
            commentaire.rating = rating;
            commentaire.numart = numart;

            prj.Commentaires.Add(commentaire);
            prj.SaveChanges();


        }

        public IEnumerable<Commentaire> GetCommentairebyArticle(int id)
        {
            return (from c in prj.Commentaires where c.numart == id select c);

        }

        public int totalcomm(int numart)
        {
            var x= (from c in prj.Commentaires where c.numart == numart select c);
            return x.Count();
        }
    }
}