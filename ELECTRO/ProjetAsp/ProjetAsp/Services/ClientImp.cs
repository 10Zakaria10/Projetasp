using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetAsp.Models;

namespace ProjetAsp.Models
{
    public class ClientImp : IClient

    {
        prjcontext prj = new prjcontext();
        public void DeleteClient(int id)
        {
            var x = (from c in prj.Clients where c.numClient == id select c).SingleOrDefault();
            var xx = (from c in prj.Commandes where c.numclient == id select c);
            prj.Commandes.RemoveRange(xx);
            prj.Clients.Remove(x);
            prj.SaveChanges();

        }

        public void EditClient(Client cl)
        {

            var x = (from c in prj.Clients where c.numClient == cl.numClient select c).SingleOrDefault();
            x.numClient = cl.numClient;
            x.nom = cl.nom;
            x.login = cl.login;
            x.mdp = cl.mdp;
            x.prenom = cl.prenom;
            x.tel = cl.tel;
            x.ville = cl.ville;
            x.role = cl.role;
            prj.SaveChanges();

        }

        public IEnumerable<Client> getAllClient()
        {
            return prj.Clients.ToList();
        }

        public IEnumerable<Client> getAllClientStartWith(string x)
        {
            return prj.Clients.Where(y => y.nom.StartsWith(x)).ToList();
        }

        public Client GetClienById(Client person)
        {
            var xx = (from c in prj.Clients where c.login == person.login && c.mdp == person.mdp select c).SingleOrDefault();
            return xx;
        }

        public Client GetClienById(int id)
        {
            var xx = (from c in prj.Clients where c.numClient==id select c).SingleOrDefault();
            return xx;
        }

        public void Inscription(Client person)
        {
            person.role = 0;
            prj.Clients.Add(person);
            prj.SaveChanges();
        }

        public bool isAdmin(Client person)
        {

            var xx = (from c in prj.Clients where c.login == person.login && c.mdp == person.mdp select c).SingleOrDefault();
            if (xx.role == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Boolean SeConnecter(Client person)
        {

            var x = from c in prj.Clients where c.login == person.login && c.mdp == person.mdp select c;

            if (x.Count() > 0)
            {

                return true;
            }
            else

                return false;

        }
    }
}