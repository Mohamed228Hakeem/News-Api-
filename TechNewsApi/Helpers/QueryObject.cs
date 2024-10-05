using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechNewsApi.Helpers
{
    public class QueryObject
    {
    public string? Keyword { get; set; }  // For general search
    public string? Language { get; set; }  = "en" ;// Filter by language
    public string? SortBy { get; set; }  = "popularity"; // Sort by relevancy, popularity, etc.
    // public DateTime? FromDate { get; set; } // Filter by date range (from)
    // public DateTime? ToDate { get; set; }   // Filter by date range (to)
    public int page {get;set;} = 1 ;
    public int pageSize {get;set;} = 5;


    
    }
}