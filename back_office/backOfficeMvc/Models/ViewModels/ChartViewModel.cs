using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models.ViewModels
{
    public class ChartViewModel
    {
        public List<ChartCategoryViewModel> CategoryData { get; set; }
        public List<ChartArticleViewModel> ArticleData { get; set; }
    }
}