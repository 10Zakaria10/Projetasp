using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetAsp.Models;
using System.Threading.Tasks;

namespace ProjetAsp.Services
{
    public interface ICommentaire
    {
        IEnumerable<Commentaire> GetCommentairebyArticle(int id);
        void AjoutezCommentaire(string nom, string comm, float rating, int numart);
        int totalcomm(int numart);
    }
}
