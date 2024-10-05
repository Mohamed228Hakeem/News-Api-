using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechNewsApi.Helpers;
using TechNewsApi.Model;

namespace TechNewsApi.Service
{
    public class NewsService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration.GetValue<string>("NewsApi:ApiKey");
        }

        


        public async Task<List<NewsArticle>> GetNewsArticlesAsync()
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyNewsApi/1.0");
            
            var requestUrl = $"https://newsapi.org/v2/everything?q=Tech&apiKey={_apiKey}";
            
            var response = await _httpClient.GetAsync(requestUrl);
            
            var content = await response.Content.ReadFromJsonAsync<NewsResponse>();
            return content.Articles;

        }

        public async Task<List<NewsArticle>> SearchNewsAsync(QueryObject query)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyNewsApi/1.0");

            var requestUrl = $"https://newsapi.org/v2/everything?apiKey={_apiKey}";
            

            if (!string.IsNullOrEmpty(query.Keyword))
            {
                requestUrl += $"&q={Uri.EscapeDataString(query.Keyword)}";
            }
            if (!string.IsNullOrEmpty(query.Language))
            {
                requestUrl += $"&language={query.Language}";
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                requestUrl += $"&sortBy={query.SortBy}";
            }
            if (string.IsNullOrEmpty(query.SortBy))
            {
                query.SortBy = "relevancy";
            }

            // Set a default SortBy if none is provided
            requestUrl += $"&sortBy={(string.IsNullOrEmpty(query.SortBy) ? "relevancy" : query.SortBy)}";
    
            // Add pagination parameters
            requestUrl += $"&page={query.page}&pageSize={query.pageSize}";

            var response = await _httpClient.GetAsync(requestUrl);

            var JsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<NewsResponse>();
                return content.Articles;
            }
            Console.WriteLine($"{response.StatusCode} content = {response.Content}");
            return new List<NewsArticle>();

        }
    }
}