using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechNewsApi.Model
{
    public class NewsResponse
    {
        public List<NewsArticle> Articles { get; set; }
    }
}