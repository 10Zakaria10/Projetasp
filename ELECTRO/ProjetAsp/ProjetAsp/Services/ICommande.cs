using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAsp.Models;

namespace ProjetAsp.Services
{
    public interface ICommande
    {
        IEnumerable<Commande> getCommandeById(int idclient);
        void DeleteArt(int id,Client person);
        void AjouteCommande(int person, int idarticle, int qte);
        IEnumerable<Commande> getCommandebyCat(Categorie x);
        IEnumerable<Commande> getCommandebyArticle(Article x);
        int countCommandeClient(int idclient);
        void DeleteCommande(int idclient , int idart);
        float totalClient(int idclient);




    }
}
