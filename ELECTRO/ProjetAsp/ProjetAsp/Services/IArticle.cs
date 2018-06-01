using ProjetAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAsp.Services
{
    public interface IArticle
    {
        IEnumerable<Article> getAllArticle();
        Article getArticleById(int id);
        IEnumerable<Article> getArticleByIdV2(int id);
        void DeleteArticle(int id);
        void EditArticle(Article art);
        void CreateArticle(Article art);
        IEnumerable<Article> getArticleByrefCat(int id);
        IEnumerable<Article> getTopSeeledarticle();
        IEnumerable<Article> getTopSeelingArticleByrefCat(int id);
        int getcat(int artid);
        IEnumerable<String> getBrand();
        IEnumerable<Article> getArticleByCatorBrand(string cat, string brand, double prixmin, double prixmax);
        IEnumerable<Article> getHotdeals();


    }
}
