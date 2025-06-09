<template>
  <div class="tabela-usuarios">
    <div class="header-actions">
      <div class="search-container">
        <input 
          v-model="searchTerm" 
          @input="searchUsuarios"
          placeholder="Buscar usuários pelo nome..."
          class="search-input"
        />
      </div>
      <button @click="showCreateModal = true" class="btn-criar">
        Criar Usuário
      </button>
    </div>

    <div v-if="loading" class="loading">
      Carregando usuários...
    </div>

    <div v-else-if="error" class="error">
      {{ error }}
    </div>

   <div v-else class="table-container">
      <table class="custom-table">
        <thead class="custom-table-head">
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Email</th>
            <th>CPF</th>
            <th>Tipo</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody class="custom-table-body">
          <tr v-for="usuario in filteredUsuarios" :key="usuario.id">
            <td>{{ usuario.id }}</td>
            <td>{{ usuario.nome }}</td>
            <td>{{ usuario.email }}</td>
            <td>{{ formatCPF(usuario.cpf) }}</td>
            <td>
              <span :class="['badge', getTipoClass(usuario.tipo)]">
                {{ getTipoText(usuario.tipo) }}
              </span>
            </td>
            <td class="actions">
              <button @click="editUsuario(usuario)" class="btn-edit">
                Editar
              </button>
              <button @click="deleteUsuario(usuario.id)" class="btn-delete">
                Excluir
              </button>
            </td>
          </tr>
          <tr v-if="filteredUsuarios.length === 0">
            <td colspan="6" style="text-align: center;">Nenhum usuário encontrado</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Modal de Criar/Editar -->
    <div v-if="showCreateModal || showEditModal" class="modal-overlay" @click="closeModals">
      <div class="modal" @click.stop>
        <h3>{{ showCreateModal ? 'Criar Usuário' : 'Editar Usuário' }}</h3>
        
        <form @submit.prevent="saveUsuario">
          <div class="form-group">
            <label>Nome:</label>
            <input 
              type="text" 
              v-model="formData.nome" 
              required 
              :disabled="saving"
            />
          </div>

          <div class="form-group">
            <label>Email:</label>
            <input 
              type="email" 
              v-model="formData.email" 
              required 
              :disabled="saving"
            />
          </div>

          <div class="form-group">
            <label>CPF:</label>
            <input 
              type="text" 
              v-model="formData.cpf" 
              required 
              maxlength="11"
              :disabled="saving"
            />
          </div>

          <div class="form-group" v-if="showCreateModal">
            <label>Senha:</label>
            <input 
              type="password" 
              v-model="formData.senha" 
              required 
              :disabled="saving"
            />
          </div>

          <div class="form-group">
            <label>Tipo de Usuário:</label>
            <select v-model="formData.tipo" :disabled="saving" required>
              <option :value="0">Cliente</option>
              <option :value="1">Administrador</option>
            </select>
          </div>

          <div class="modal-actions">
            <button type="button" @click="closeModals" :disabled="saving">
              Cancelar
            </button>
            <button type="submit" :disabled="saving">
              {{ saving ? 'Salvando...' : 'Salvar' }}
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { usuarioService } from '../../services/usuarioService.js';

export default {
  name: 'TabelaUsuariosComponent',
  data() {
    return {
      usuarios: [],
      loading: false,
      error: '',
      showCreateModal: false,
      showEditModal: false,
      saving: false,
      editingId: null,
      searchTerm: '',
      formData: {
        nome: '',
        email: '',
        cpf: '',
        senha: '',
        tipo: 0
      }
    };
  },
  computed: {
    filteredUsuarios() {
      if (!this.searchTerm) return this.usuarios;
      
      return this.usuarios.filter(usuario => 
        usuario.nome.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        usuario.email.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  },
  async mounted() {
    await this.loadUsuarios();
  },
  methods: {
    async loadUsuarios() {
      this.loading = true;
      this.error = '';

      try {
        this.usuarios = await usuarioService.getAll();
      } catch (error) {
        this.error = 'Erro ao carregar usuários';
        console.error('Erro ao carregar usuários:', error);
      } finally {
        this.loading = false;
      }
    },

    searchUsuarios() {
      // A busca é feita automaticamente através do computed filteredUsuarios
    },

    editUsuario(usuario) {
      this.editingId = usuario.id;
      this.formData = {
        nome: usuario.nome,
        email: usuario.email,
        cpf: usuario.cpf,
        senha: '',
        tipo: usuario.tipo
      };
      this.showEditModal = true;
    },

    async deleteUsuario(id) {
      if (!confirm('Tem certeza que deseja excluir este usuário?')) {
        return;
      }

      try {
        await usuarioService.delete(id);
        this.usuarios = this.usuarios.filter(u => u.id !== id);
        alert('Usuário excluído com sucesso!');
      } catch (error) {
        alert('Erro ao excluir usuário');
        console.error('Erro ao excluir usuário:', error);
      }
    },

    async saveUsuario() {
      this.saving = true;

      try {
        if (this.showCreateModal) {
          const novoUsuario = await usuarioService.create(this.formData);
          this.usuarios.push(novoUsuario);
          alert('Usuário criado com sucesso!');
        } else {
          await usuarioService.update(this.editingId, this.formData);
          await this.loadUsuarios(); // Recarregar lista
          alert('Usuário atualizado com sucesso!');
        }
        
        this.closeModals();
      } catch (error) {
        alert('Erro ao salvar usuário');
        console.error('Erro ao salvar usuário:', error);
      } finally {
        this.saving = false;
      }
    },

    closeModals() {
      this.showCreateModal = false;
      this.showEditModal = false;
      this.editingId = null;
      this.formData = {
        nome: '',
        email: '',
        cpf: '',
        senha: '',
        tipo: 0
      };
    },

    getTipoClass(tipo) {
      return tipo === 1 ? 'admin' : 'user';
    },

    getTipoText(tipo) {
      return tipo === 1 ? 'Administrador' : 'Cliente';
    },

    formatCPF(cpf) {
      if (!cpf) return '';
      return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
    }
  }
};
</script>

<style scoped>
.tabela-usuarios {
  background: #0d1423;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.header-actions {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
  gap: 20px;
}

.search-container {
  flex: 1;
  max-width: 400px;
}

.search-input {
  width: 100%;
  padding: 10px 15px;
  border: 1px solid #444;
  border-radius: 4px;
  background-color: #333;
  color: #e0e0e0;
  font-size: 14px;
}

.search-input::placeholder {
  color: #888;
}

.search-input:focus {
  outline: none;
  border-color: #007bff;
}

.btn-criar {
  background-color: #28a745;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
  transition: background-color 0.3s;
  white-space: nowrap;
}

.btn-criar:hover {
  background-color: #218838;
}

.loading, .error {
  text-align: center;
  padding: 40px;
  font-size: 1.1rem;
}

.error {
  color: #dc3545;
}

.table-container {
    overflow-x: auto;
    margin-top: 20px;
    border-radius: 8px;
    overflow: hidden;
}

.custom-table {
    width: 100%;
    border-collapse: collapse;
    text-align: left;
    min-width: 600px;
}

.custom-table-head {
    background-color: #1e1e2c;
    color: #ffffff;
}

.custom-table-body {
    background-color: #303041;
    color: #d1d1d1;
}

.custom-table th,
.custom-table td {
    border: 1px solid #1e1e2c;
    padding: 12px 16px;
    max-width: 500px;
}

.custom-table tbody tr:hover {
    background-color: var(--primary-blue);
    opacity: 0.8;
}

.custom-table tbody tr:hover button {
    border: 2px solid var(--gray-200);
}

.badge {
  padding: 4px 8px;
  border-radius: 12px;
  font-size: 12px;
  font-weight: 500;
}

.badge.admin {
  background-color: #dc3545;
  color: white;
}

.badge.user {
  background-color: #28a745;
  color: white;
}

table button {
    background: transparent;
    border: none;
    padding: 8px 12px;
    border: 2px solid var(--primary-blue);
    border-radius: 8px;
    cursor: pointer;
    margin-right: 5px;
    font-size: 12px;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal {
  background: white;
  padding: 30px;
  border-radius: 8px;
  width: 90%;
  max-width: 500px;
  max-height: 90vh;
  overflow-y: auto;
}

.modal h3 {
  margin-bottom: 20px;
  color: #333;
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
  font-weight: 500;
  color: #555;
}

.form-group input,
.form-group textarea,
.form-group option {
  color: #333;
  width: 100%;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
  font-family: inherit;
  box-sizing: border-box;
}

.form-group select {
  color: #333;
  width: 100%;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
  font-family: inherit;
  box-sizing: border-box;
  background-color: white;
}

.modal-actions {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  margin-top: 20px;
}

.modal-actions button {
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
}

.modal-actions button[type="button"] {
  background-color: #6c757d;
  color: white;
}

.modal-actions button[type="submit"] {
  background-color: #007bff;
  color: white;
}

.modal-actions button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}
</style>

