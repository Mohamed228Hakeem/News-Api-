using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using TechNewsApi.Helpers;
using TechNewsApi.Model;
using TechNewsApi.Service;

namespace TechNewsApi.Controllers
{
    [ApiController]
    [Route("News")]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("Tech")]
        public async Task<ActionResult<List<NewsArticle>>> GetTechNews()
        {
            var articles = await _newsService.GetNewsArticlesAsync();

            return Ok(articles);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchNews([FromQuery] QueryObject query)
        {
            var news = await _newsService.SearchNewsAsync(query);

            var cacheKey = $"News_{query.Keyword}";

            if (!string.IsNullOrEmpty(query.Keyword))
            {
                // Create a unique cache key for the keyword
                Response.Headers["Cache-Key"] = cacheKey;
            }

            var cacheDuration = TimeSpan.FromMinutes(1);

            Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
            {
                MaxAge = cacheDuration,
                MustRevalidate = true
            };

            return Ok(news);
        }
    }
}