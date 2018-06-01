using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAsp.Models;
namespace ProjetAsp.Services
{
   public interface IFavoris
    {
        void addtoFavoris(int id,int idclient);
        IEnumerable<Favori> getFavorisClient(int id);
        int totalFavorisClient(int id);
        void DeletefromFavoris(int id, int idclient);

    }
}
