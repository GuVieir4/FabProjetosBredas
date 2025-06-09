import api from './api';

// Serviço para operações relacionadas a produtos
export const produtoService = {
  // Obter todos os produtos com filtros e paginação
  getAllWithFilters: async (params = {}) => {
    try {
      const response = await api.get('/Produto', { params });
      return response.data;
    } catch (error) {
      console.error('Erro ao buscar produtos com filtros:', error);
      throw error;
    }
  },

  // Buscar todos os produtos (método original mantido para compatibilidade)
  getAll: async () => {
    try {
      const response = await api.get('/Produto');
      return response.data.produtos || response.data;
    } catch (error) {
      console.error('Erro ao buscar produtos:', error);
      throw error;
    }
  },

  // Obter lançamentos (últimos 5 produtos)
  getLancamentos: async () => {
    try {
      const response = await api.get('/Produto/lancamentos');
      return response.data;
    } catch (error) {
      console.error('Erro ao buscar lançamentos:', error);
      throw error;
    }
  },

  // Buscar produto por ID
  getById: async (id) => {
    try {
      const response = await api.get(`/Produto/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar produto com ID ${id}:`, error);
      throw error;
    }
  },

  // Buscar produtos por categoria
  getByCategoria: async (categoriaId) => {
    try {
      const response = await api.get(`/Produto/categoria/${categoriaId}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar produtos da categoria ${categoriaId}:`, error);
      throw error;
    }
  },

  // Buscar produtos por marca
  getByMarca: async (marcaId) => {
    try {
      const response = await api.get(`/Produto/marca/${marcaId}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar produtos da marca ${marcaId}:`, error);
      throw error;
    }
  },

  // Criar um novo produto
  create: async (produto) => {
    try {
      const response = await api.post('/Produto', produto);
      return response.data;
    } catch (error) {
      console.error('Erro ao criar produto:', error);
      throw error;
    }
  },

  // Atualizar um produto existente
  update: async (id, produto) => {
    try {
      const response = await api.put(`/Produto/${id}`, produto);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar produto com ID ${id}:`, error);
      throw error;
    }
  },

  // Excluir um produto
  delete: async (id) => {
    try {
      const response = await api.delete(`/Produto/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao excluir produto com ID ${id}:`, error);
      throw error;
    }
  }
};

