using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechNewsApi.Model
{
    public class NewsArticle
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string PublishedAt{set;get;}
        public string Content { get; set; }
    }
}