<template>
  <div class="tabela-marcas">
    <div class="header-actions">
      <div class="search-container">
        <input 
          v-model="searchTerm" 
          @input="searchMarcas"
          placeholder="Buscar marcas pelo nome..."
          class="search-input"
        />
      </div>
      <button @click="showCreateModal = true" class="btn-criar">
        Criar Marca
      </button>
    </div>

    <div v-if="loading" class="loading">
      Carregando marcas...
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
            <th>Ações</th>
          </tr>
        </thead>
        <tbody class="custom-table-body">
          <tr v-for="marca in filteredMarcas" :key="marca.id">
            <td>{{ marca.id }}</td>
            <td>{{ marca.nome }}</td>
            <td class="actions">
              <button @click="editMarca(marca)" class="btn-edit">
                Editar
              </button>
              <button @click="deleteMarca(marca.id)" class="btn-delete">
                Excluir
              </button>
            </td>
          </tr>
          <tr v-if="filteredMarcas.length === 0">
            <td colspan="3" style="text-align: center;">Nenhuma marca encontrada</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Modal de Criar/Editar -->
    <div v-if="showCreateModal || showEditModal" class="modal-overlay" @click="closeModals">
      <div class="modal" @click.stop>
        <h3>{{ showCreateModal ? 'Criar Marca' : 'Editar Marca' }}</h3>
        
        <form @submit.prevent="saveMarca">
          <div class="form-group">
            <label>Nome:</label>
            <input 
              type="text" 
              v-model="formData.nome" 
              required 
              :disabled="saving"
            />
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
import { marcaService } from '../../services/marcaService.js';

export default {
  name: 'TabelaMarcasComponent',
  data() {
    return {
      marcas: [],
      loading: false,
      error: '',
      showCreateModal: false,
      showEditModal: false,
      saving: false,
      editingId: null,
      searchTerm: '',
      formData: {
        nome: '',
      }
    };
  },
  computed: {
    filteredMarcas() {
      if (!this.searchTerm) return this.marcas;
      
      return this.marcas.filter(marca => 
        marca.nome.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  },
  async mounted() {
    await this.loadMarcas();
  },
  methods: {
    async loadMarcas() {
      this.loading = true;
      this.error = '';

      try {
        this.marcas = await marcaService.getAll();
      } catch (error) {
        this.error = 'Erro ao carregar marcas';
        console.error('Erro ao carregar marcas:', error);
      } finally {
        this.loading = false;
      }
    },

    searchMarcas() {
      // A busca é feita automaticamente através do computed filteredMarcas
    },

    editMarca(marca) {
      this.editingId = marca.id;
      this.formData = {
        nome: marca.nome,
      };
      this.showEditModal = true;
    },

    async deleteMarca(id) {
      if (!confirm('Tem certeza que deseja excluir esta marca?')) {
        return;
      }

      try {
        await marcaService.delete(id);
        this.marcas = this.marcas.filter(m => m.id !== id);
        alert('Marca excluída com sucesso!');
      } catch (error) {
        if (error.response && error.response.status === 400) {
          alert(error.response.data.message || 'Erro ao excluir marca');
        } else {
          alert('Erro ao excluir marca');
        }
        console.error('Erro ao excluir marca:', error);
      }
    },

    async saveMarca() {
      this.saving = true;

      try {
        if (this.showCreateModal) {
          const novaMarca = await marcaService.create(this.formData);
          this.marcas.push(novaMarca);
          alert('Marca criada com sucesso!');
        } else {
          await marcaService.update(this.editingId, this.formData);
          await this.loadMarcas(); // Recarregar lista
          alert('Marca atualizada com sucesso!');
        }
        
        this.closeModals();
      } catch (error) {
        alert('Erro ao salvar marca');
        console.error('Erro ao salvar marca:', error);
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
      };
    }
  }
};
</script>

<style scoped>
.tabela-marcas {
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
.form-group textarea {
  color: #333;
  width: 100%;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
  font-family: inherit;
  box-sizing: border-box;
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

