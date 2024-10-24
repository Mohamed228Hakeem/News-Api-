import React, { useState, useEffect } from 'react';
import newsService from './Services/NewsService'; // Import the news service

function App() {
  // State to store news articles, loading state, and search parameters
  const [news, setNews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [keyword, setKeyword] = useState('');
  const [language, setLanguage] = useState('en');
  const [sortBy, setSortBy] = useState('popularity');
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);

  // Fetch tech news from the API when the component loads
  useEffect(() => {
    const fetchNews = async () => {
      try {
        const techNews = await newsService.getTechNews();
        setNews(techNews);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching news:', error);
        setLoading(false);
      }
    };

    fetchNews();
  }, []);

  // Function to handle search
  const handleSearch = async () => {
    if (!keyword) return; // Prevent empty search

    setLoading(true); // Set loading state

    try {
      const searchResults = await newsService.searchNews({
        keyword,
        language,
        sortBy,
        page,
        pageSize,
      });
      setNews(searchResults); // Update the news state with the search results
    } catch (error) {
      console.error('Error fetching search results:', error);
    } finally {
      setLoading(false); // Set loading to false after fetching
    }
  };

  // Show loading state while data is being fetched
  if (loading) {
    return <p>Loading news articles...</p>;
  }

  return (
    <div className="App">
      <h1>News Articles</h1>

      {/* Search input and filters */}
      <input 
        type="text" 
        placeholder="Search for news..." 
        value={keyword}
        onChange={(e) => setKeyword(e.target.value)} // Update keyword state
      />
      <br></br>
      <select value={language} onChange={(e) => setLanguage(e.target.value)}>
        <option value="en">English</option>
        <option value="es">Spanish</option>
        <option value="fr">French</option>
        {/* Add more languages as needed */}
      </select>
      <br></br>
      <select value={sortBy} onChange={(e) => setSortBy(e.target.value)}>
        <option value="popularity">Popularity</option>
        <option value="relevancy">Relevancy</option>
        {/* Add more sorting options as needed */}
      </select>
      <br></br>
      <label>Page Number</label>
      <input 
        type="number" 
        min="1" 
        value={page} 
        onChange={(e) => setPage(Number(e.target.value))} 
        placeholder="Page number" 
      />
      <br></br>
      <label>Page Size</label>
      <input 
        type="number" 
        min="1" 
        value={pageSize} 
        onChange={(e) => setPageSize(Number(e.target.value))} 
        placeholder="Page size" 
      />
      <br></br>
      <button onClick={handleSearch}>Search</button>
      <br></br>
      <ul>
        {news.length > 0 ? (
          news.map((article, index) => (
            <li key={index}>
              <h2>{article.title}</h2>
              <p>{article.description}</p>
              <p>{article.content}</p>
              <a href={article.url} target="_blank" rel="noopener noreferrer">Read more</a>
            </li>
          ))
        ) : (
          <p>No articles found.</p>
        )}
      </ul>
    </div>
  );
}

export default App;
