import api from './api.js';

export const carrinhoService = {
  // Obter carrinho por usuário
  async getByUsuario(usuarioId) {
    try {
      const response = await api.get(`/Carrinho/usuario/${usuarioId}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao buscar carrinho' };
    }
  },

  // Obter carrinho por ID
  async getById(carrinhoId) {
    try {
      const response = await api.get(`/Carrinho/${carrinhoId}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao buscar carrinho' };
    }
  },

  // Adicionar produto ao carrinho do usuário
  async addProdutoToUsuario(usuarioId, produtoId, quantidade = 1) {
    try {
      const response = await api.post(
        `/Carrinho/usuario/${usuarioId}/produto/${produtoId}`,
        {
          produtoId,
          quantidade
        }
      );
    return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao adicionar produto ao carrinho' };
    }
  },

  // Adicionar produto ao carrinho
  async addProduto(carrinhoId, produtoId) {
    try {
      const response = await api.post(`/Carrinho/${carrinhoId}/produto/${produtoId}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao adicionar produto ao carrinho' };
    }
  },

  // Remover produto do carrinho
  async removeProduto(carrinhoId, produtoId) {
    try {
      const response = await api.delete(`/Carrinho/${carrinhoId}/produto/${produtoId}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao remover produto do carrinho' };
    }
  },

  // Criar carrinho
  async create(usuarioId) {
    try {
      const response = await api.post('/Carrinho', { usuarioId });
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao criar carrinho' };
    }
  },

  // Atualizar produtos do carrinho
  async updateProdutos(carrinhoId, produtoIds) {
    try {
      const response = await api.put(`/Carrinho/${carrinhoId}`, { produtoIds });
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao atualizar carrinho' };
    }
  },

  // Adicionar item ao carrinho (método simplificado)
  addItem: async (produtoId, quantidade = 1) => {
    try {
      const user = JSON.parse(localStorage.getItem('user'));
      if (!user) throw new Error('Usuário não autenticado');

      const response = await api.post(
        `/Carrinho/usuario/${user.id}/produto/${produtoId}`,
        {
          produtoId,
          quantidade
        }
      );

      return response.data;
    } catch (error) {
      console.error('Erro ao adicionar item ao carrinho:', error);
      throw error.response?.data || { message: 'Erro ao adicionar produto ao carrinho' };
    }
  }
};

