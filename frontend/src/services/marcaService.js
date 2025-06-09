import api from './api';

// Serviço para operações relacionadas a marcas
export const marcaService = {
  // Buscar todas as marcas
  getAll: async () => {
    try {
      const response = await api.get('/Marca');
      return response.data;
    } catch (error) {
      console.error('Erro ao buscar marcas:', error);
      throw error;
    }
  },

  // Buscar marca por ID
  getById: async (id) => {
    try {
      const response = await api.get(`/Marca/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar marca com ID ${id}:`, error);
      throw error;
    }
  },

  // Criar uma nova marca
  create: async (marca) => {
    try {
      const response = await api.post('/Marca', marca);
      return response.data;
    } catch (error) {
      console.error('Erro ao criar marca:', error);
      throw error;
    }
  },

  // Atualizar uma marca existente
  update: async (id, marca) => {
    try {
      const response = await api.put(`/Marca/${id}`, marca);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar marca com ID ${id}:`, error);
      throw error;
    }
  },

  // Excluir uma marca
  delete: async (id) => {
    try {
      const response = await api.delete(`/Marca/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao excluir marca com ID ${id}:`, error);
      throw error;
    }
  }
};

