// src/services/newsService.js
import axios from 'axios';

// Base URL for your API
const BASE_URL = 'http://localhost:5105/News';

const newsService = {
  getTechNews: async () => {
    try {
      const response = await axios.get(`${BASE_URL}/Tech`);
      return response.data;
    } catch (error) {
      throw error; // Rethrow the error for the component to handle
    }
  },

  searchNews: async ({ keyword, language, sortBy, page, pageSize }) => {
    if (!keyword) throw new Error('Keyword is required');
    
    try {
      const response = await axios.get(`${BASE_URL}/Search`, {
        params: {
          Keyword: keyword,
          Language: language,
          SortBy: sortBy,
          Page: page,
          PageSize: pageSize,
        },
      });
      return response.data;
    } catch (error) {
      throw error; // Rethrow the error for the component to handle
    }
  }
};

export default newsService;
