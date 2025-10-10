using backOfficeMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backOfficeMvc.DataAccess
{
    public interface IArticleDao
    {
        List<Article> GetAllArticles();

        List<Category> SelectAllCategories();

        List<Article> GetArticlesByCategory(string category);

        Article GetArticleById(int id);

        void AddArticle(Article article);

        void UpdateArticle(int id, Article article);

        void DeleteArticle(int id);
    }
}
