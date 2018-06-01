using ProjetAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAsp.Services
{
    public interface ICategorie
    {
        IEnumerable<Categorie> getAllCategorie();
        void DeleteCat(int id );
        Categorie getCatById(int id);
        void EditCat(Categorie cat);
        void CreateCat(Categorie cat);

    }
}
