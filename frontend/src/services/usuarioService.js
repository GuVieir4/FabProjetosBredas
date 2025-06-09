import api from './api.js';

export const usuarioService = {
  // Obter todos os usuários
  async getAll() {
    try {
      const response = await api.get('/Usuarios');
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao buscar usuários' };
    }
  },

  // Obter usuário por ID
  async getById(id) {
    try {
      const response = await api.get(`/Usuarios/${id}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao buscar usuário' };
    }
  },

  // Criar usuário
  async create(usuario) {
    try {
      const response = await api.post('/Usuarios', usuario);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao criar usuário' };
    }
  },

  // Atualizar usuário
  async update(id, usuario) {
    try {
      const response = await api.put(`/Usuarios/${id}`, usuario);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao atualizar usuário' };
    }
  },

  // Deletar usuário
  async delete(id) {
    try {
      const response = await api.delete(`/Usuarios/${id}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao deletar usuário' };
    }
  }
};

