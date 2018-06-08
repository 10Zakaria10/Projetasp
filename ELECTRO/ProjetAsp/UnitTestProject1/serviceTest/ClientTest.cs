using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetAsp.Models;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.mock
{
    [TestClass]
    class ClientTest
    {
        PrjContext2 prjcontext = new PrjContext2();
        List<Client> listeClient = new List<Client>();
        
        
        public ClientTest()
        {
            listeClient.Add(new Client(1, "login1", "mdp1", "nom1", "prenom1", "bghaz1", "ville1", "tel1",7));
            listeClient.Add(new Client(2, "login2", "mdp2", "nom2", "prenom2", "bghaz2", "ville2", "tel2", 7));
        }

        [TestMethod]
        public void DeleteClient(int id)
        {
           foreach(var item in listeClient )
            {
                if(item.numClient == id)
                {
                    listeClient.Remove(item);
                }
            }
            CollectionAssert.Contains(listeClient, new Client().numClient = id); 
        }

        [TestMethod]
        public void EditClient(Client c)
        {
            foreach(var item in listeClient)
            {
                if(item == c)
                {
                    item.numClient = c.numClient;
                    item.login = c.login;
                    item.mdp = c.mdp;
                    item.nom = c.nom;
                    item.prenom = c.prenom;
                    item.email = c.email;
                    item.ville = c.ville;
                    item.tel = c.tel;
                    item.role = c.role;
                }
            }
            CollectionAssert.Contains(listeClient, c, "ce client ne peut pas etre modifié,il n'existe pas!");
        }

        [TestMethod]
        public IEnumerable<Client> getAllClient()
        {
            CollectionAssert.AllItemsAreNotNull(listeClient);
            return listeClient;
        }

        [TestMethod]
        public IEnumerable<Client> getAllClientStartWith(string x)
        {
            return null;
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),"ce client n'existe pas")]
        public Client GetClienById(Client c)
        {
            CollectionAssert.Contains(listeClient, c);
            return c;
        }

        [TestMethod]
        public Client GetClienById(int id)
        {
            Client c = null;
            foreach (var item in listeClient)
            {
                if(item.numClient == id)
                {
                     c = new Client(id, item.login, item.mdp, item.nom, item.prenom, item.email, item.ville, item.tel, item.role.Value);
                }
            }
            CollectionAssert.Contains(listeClient, c);
            return null;
        }

        [TestMethod]
        public void Inscription(Client c)
        {
            listeClient.Add(c);
            CollectionAssert.DoesNotContain(listeClient, c, "vous etes déja enregistes");
        }

        [TestMethod]
        public bool isAdmin(Client c)
        {
            if (c.role == 0)
                return false;
            else
            {
                CollectionAssert.Contains(listeClient, c);
                return true;
            }
            
        }

        [TestMethod]
        public bool SeConnecter(Client c)
        {
          if(listeClient.Contains(c))
            {
                CollectionAssert.Contains(listeClient, c);
                return true;
            }
            return false;
        }
    }
}
