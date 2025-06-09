import api from './api';

// Serviço para operações relacionadas a categorias
export const categoriaService = {
  // Buscar todas as categorias
  getAll: async () => {
    try {
      const response = await api.get('/Categoria');
      return response.data;
    } catch (error) {
      console.error('Erro ao buscar categorias:', error);
      throw error;
    }
  },

  // Buscar categoria por ID
  getById: async (id) => {
    try {
      const response = await api.get(`/Categoria/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar categoria com ID ${id}:`, error);
      throw error;
    }
  },

  // Criar uma nova categoria
  create: async (categoria) => {
    try {
      const response = await api.post('/Categoria', categoria);
      return response.data;
    } catch (error) {
      console.error('Erro ao criar categoria:', error);
      throw error;
    }
  },

  // Atualizar uma categoria existente
  update: async (id, categoria) => {
    try {
      const response = await api.put(`/Categoria/${id}`, categoria);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar categoria com ID ${id}:`, error);
      throw error;
    }
  },

  // Excluir uma categoria
  delete: async (id) => {
    try {
      const response = await api.delete(`/Categoria/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao excluir categoria com ID ${id}:`, error);
      throw error;
    }
  }
};

