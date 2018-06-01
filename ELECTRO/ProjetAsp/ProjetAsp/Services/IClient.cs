using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAsp.Models
{
   public interface IClient
    {
        Boolean SeConnecter(Client c);

        Boolean isAdmin(Client c);

        void Inscription(Client c);

        Client GetClienById(Client c);

        Client GetClienById(int id);

        IEnumerable<Client> getAllClient();

        IEnumerable<Client> getAllClientStartWith(String x);

        void EditClient(Client c);

        void DeleteClient(int id);

    }
}
