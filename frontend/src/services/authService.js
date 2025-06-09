import api from './api.js';

export const authService = {
  // Login do usuário
  async login(email, senha) {
    try {
      const response = await api.post('/auth/login', {
        email,
        senha
      });
      
      // Salvar token no localStorage
      if (response.data.token) {
        localStorage.setItem('authToken', response.data.token);
        localStorage.setItem('user', JSON.stringify(response.data));
      }
      
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao fazer login' };
    }
  },

  // Cadastro do usuário
  async register(nome, email, senha, cpf) {
    try {
      const response = await api.post('/auth/register', {
        nome,
        email,
        senha,
        cpf
      });
      
      // Salvar token no localStorage
      if (response.data.token) {
        localStorage.setItem('authToken', response.data.token);
        localStorage.setItem('user', JSON.stringify(response.data));
      }
      
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Erro ao fazer cadastro' };
    }
  },

  // Validar token
  async validateToken(token) {
    try {
      const response = await api.post('/auth/validate', token, {
        headers: {
          'Content-Type': 'application/json'
        }
      });
      return response.data;
    } catch (error) {
      throw error.response?.data || { message: 'Token inválido' };
    }
  },

  // Logout
  logout() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('user');
  },

  // Verificar se está logado
  isAuthenticated() {
    const token = localStorage.getItem('authToken');
    return !!token;
  },

  // Obter usuário atual
  getCurrentUser() {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  },

  // Obter token
  getToken() {
    return localStorage.getItem('authToken');
  },

  // Verificar se é admin
  isAdmin() {
    const user = this.getCurrentUser();
    return user && user.tipo === true;
  }
};

