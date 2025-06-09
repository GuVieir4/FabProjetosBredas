<template>
  <div class="login-container">
    <div class="login-card">
      <h2>{{ isRegisterMode ? 'Cadastro' : 'Login' }}</h2>
      
      <form @submit.prevent="handleSubmit">
        <div v-if="isRegisterMode" class="form-group">
          <label for="nome">Nome:</label>
          <input 
            type="text" 
            id="nome" 
            v-model="form.nome" 
            required 
            :disabled="loading"
          />
        </div>

        <div class="form-group">
          <label for="email">Email:</label>
          <input 
            type="email" 
            id="email" 
            v-model="form.email" 
            required 
            :disabled="loading"
          />
        </div>

        <div class="form-group">
          <label for="senha">Senha:</label>
          <input 
            type="password" 
            id="senha" 
            v-model="form.senha" 
            required 
            :disabled="loading"
          />
        </div>

        <div v-if="isRegisterMode" class="form-group">
          <label for="cpf">CPF:</label>
          <input 
            type="text" 
            id="cpf" 
            v-model="form.cpf" 
            required 
            maxlength="11"
            :disabled="loading"
          />
        </div>

        <div v-if="error" class="error-message">
          {{ error }}
        </div>

        <button type="submit" :disabled="loading" class="submit-btn">
          {{ loading ? 'Carregando...' : (isRegisterMode ? 'Cadastrar' : 'Entrar') }}
        </button>
      </form>

      <div class="form-actions">
        <button 
          @click="toggleMode" 
          class="toggle-btn"
          :disabled="loading"
        >
          {{ isRegisterMode ? 'Já tem conta? Faça login' : 'Não tem conta? Cadastre-se' }}
        </button>
        
        <button 
          @click="goBack" 
          class="back-btn"
          :disabled="loading"
        >
          Voltar
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { authService } from '../services/authService.js';

export default {
  name: 'LoginView',
  data() {
    return {
      isRegisterMode: false,
      loading: false,
      error: '',
      form: {
        nome: '',
        email: '',
        senha: '',
        cpf: ''
      }
    };
  },
  methods: {
    async handleSubmit() {
      this.loading = true;
      this.error = '';

      try {
        if (this.isRegisterMode) {
          await authService.register(
            this.form.nome,
            this.form.email,
            this.form.senha,
            this.form.cpf
          );
        } else {
          await authService.login(this.form.email, this.form.senha);
        }

        // Redirecionar para a página anterior ou home
        const redirectTo = this.$route.query.redirect || '/';
        this.$router.push(redirectTo);
      } catch (error) {
        this.error = error.message || 'Erro ao processar solicitação';
      } finally {
        this.loading = false;
      }
    },

    toggleMode() {
      this.isRegisterMode = !this.isRegisterMode;
      this.error = '';
      this.resetForm();
    },

    resetForm() {
      this.form = {
        nome: '',
        email: '',
        senha: '',
        cpf: ''
      };
    },

    goBack() {
      this.$router.go(-1);
    }
  }
};
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background-color: #0d1423;
  padding: 20px;
}

.login-card {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  width: 100%;
  max-width: 400px;
}

.login-card h2 {
  text-align: center;
  margin-bottom: 1.5rem;
  color: #333;
}

.form-group {
  margin-bottom: 1rem;
}

.form-group label {
  display: block;
  margin-bottom: 0.5rem;
  color: #555;
  font-weight: 500;
}

.form-group input {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ddd;
  color: #333;
  border-radius: 4px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus {
  outline: none;
  border-color: #007bff;
}

.form-group input:disabled {
  background-color: #f8f9fa;
  cursor: not-allowed;
}

.error-message {
  background-color: #f8d7da;
  color: #721c24;
  padding: 0.75rem;
  border-radius: 4px;
  margin-bottom: 1rem;
  border: 1px solid #f5c6cb;
}

.submit-btn {
  width: 100%;
  padding: 0.75rem;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s;
  margin-bottom: 1rem;
}

.submit-btn:hover:not(:disabled) {
  background-color: #0056b3;
}

.submit-btn:disabled {
  background-color: #6c757d;
  cursor: not-allowed;
}

.form-actions {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.toggle-btn, .back-btn {
  padding: 0.5rem;
  border: 1px solid #ddd;
  background-color: white;
  color: #007bff;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.3s;
}

.toggle-btn:hover:not(:disabled), .back-btn:hover:not(:disabled) {
  background-color: #f8f9fa;
  border-color: #007bff;
}

.toggle-btn:disabled, .back-btn:disabled {
  color: #6c757d;
  cursor: not-allowed;
}
</style>

