import axios from 'axios';

// Criando uma instância do axios com a URL base da API
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'http://localhost:5000/api',
});

export default api;

