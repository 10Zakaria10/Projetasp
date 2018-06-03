using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using ProjetAsp.Models;
using ProjetAsp.Services;

namespace ProjetAsp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
           container.RegisterType<IClient, ClientImp>();
            container.RegisterType<IArticle, ArticleImp>();
            container.RegisterType<ICommande, CommandeImp>();
            container.RegisterType<ICategorie, CategorieImp>();
            container.RegisterType<ICommentaire, CommentaireImp>();
            container.RegisterType<IFavoris, FavorisImp>();



            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}