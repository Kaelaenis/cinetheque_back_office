using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models.ViewModels
{
    public class ArticleListViewModel
    {
        public List<ArticleViewModel> Articles { get; set; } = new List<ArticleViewModel>();
    }
}