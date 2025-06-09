<template>
  <div class="tabela-produtos">
    <div class="header-actions">
      <div class="search-container">
        <input 
          v-model="searchTerm" 
          @input="searchProdutos"
          placeholder="Buscar produtos pelo nome..."
          class="search-input"
        />
      </div>
      <button @click="showCreateModal = true" class="btn-criar">
        Criar Produto
      </button>
    </div>

    <div v-if="loading" class="loading">
      Carregando produtos...
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
            <th>Descrição</th>
            <th>Preço</th>
            <th>Marca</th>
            <th>Imagem</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody class="custom-table-body">
          <tr v-for="produto in filteredProdutos" :key="produto.id">
            <td>{{ produto.id }}</td>
            <td>{{ produto.nome }}</td>
            <td>{{ produto.descricao || '-' }}</td>
            <td>R$ {{ produto.preco.toFixed(2) }}</td>
            <td>{{ produto.marcaId || 'N/A' }}</td>
            <td>
              <img 
                v-if="produto.imagemUrl" 
                :src="produto.imagemUrl" 
                alt="Produto" 
                class="product-thumbnail"
                @error="handleImageError"
              />
              <span v-else>Sem imagem</span>
            </td>
            <td class="actions">
              <button @click="editProduto(produto)" class="btn-edit">Editar</button>
              <button @click="deleteProduto(produto.id)" class="btn-delete">Excluir</button>
            </td>
          </tr>
          <tr v-if="filteredProdutos.length === 0">
            <td colspan="7" style="text-align: center;">Nenhum produto encontrado</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Modal de Criar/Editar Produto -->
    <div v-if="showCreateModal || showEditModal" class="modal-overlay" @click="closeModals">
      <div class="modal" @click.stop>
        <h3>{{ showCreateModal ? 'Criar Produto' : 'Editar Produto' }}</h3>

        <form @submit.prevent="saveProduto">
          <div class="form-group">
            <label>Nome:</label>
            <input v-model="formData.nome" required :disabled="saving" />
          </div>

          <div class="form-group">
            <label>Descrição:</label>
            <textarea v-model="formData.descricao" rows="3" :disabled="saving"></textarea>
          </div>

          <div class="form-group">
            <label>Preço:</label>
            <input type="number" step="0.01" v-model.number="formData.preco" required :disabled="saving" />
          </div>

          <div class="form-group">
            <label>Marca:</label>
            <select v-model="formData.marcaId" required :disabled="saving">
              <option value="">Selecione uma marca</option>
              <option v-for="marca in marcas" :key="marca.id" :value="marca.id">
                {{ marca.nome }}
              </option>
            </select>
          </div>

          <div class="form-group">
            <label>Categoria:</label>
            <select v-model="formData.categoriaId" required :disabled="saving">
              <option value="">Selecione uma categoria</option>
              <option v-for="categoria in categorias" :key="categoria.id" :value="categoria.id">
                {{ categoria.nome }}
              </option>
            </select>
          </div>

          <div class="form-group">
            <label>URL da Imagem:</label>
            <input 
              v-model="formData.imagemUrl" 
              type="url" 
              placeholder="https://exemplo.com/imagem.jpg"
              :disabled="saving" 
            />
          </div>

          <div v-if="formData.imagemUrl" class="form-group">
            <label>Preview da Imagem:</label>
            <div class="image-preview">
              <img 
                :src="formData.imagemUrl" 
                alt="Preview" 
                @error="handlePreviewError"
                @load="handlePreviewLoad"
              />
              <p v-if="imageError" class="image-error">Erro ao carregar imagem</p>
            </div>
          </div>

          <div class="modal-actions">
            <button type="button" @click="closeModals" :disabled="saving">Cancelar</button>
            <button type="submit" :disabled="saving">{{ saving ? 'Salvando...' : 'Salvar' }}</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { produtoService } from '@/services'
import { marcaService } from '@/services/marcaService.js'
import { categoriaService } from '@/services/categoriaService.js'

export default {
  name: 'TabelaProdutosComponent',
  data() {
    return {
      produtos: [],
      marcas: [],
      categorias: [],
      loading: false,
      error: '',
      showCreateModal: false,
      showEditModal: false,
      saving: false,
      editingId: null,
      searchTerm: '',
      imageError: false,
      formData: {
        nome: '',
        descricao: '',
        preco: 0,
        marcaId: '',
        imagemUrl: '',
        categoriaId: '',}
    }
  },
  computed: {
    filteredProdutos() {
      if (!this.searchTerm) return this.produtos;
      
      return this.produtos.filter(produto => 
        produto.nome.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    },
  },
  async mounted() {
    await this.loadData()
  },
  methods: {
    async loadData() {
      await Promise.all([
        this.loadProdutos(),
        this.loadMarcas(),
        this.loadCategorias()
      ]);
    },

    async loadProdutos() {
      this.loading = true
      this.error = ''

      try {
        this.produtos = await produtoService.getAll()
      } catch (error) {
        this.error = 'Erro ao carregar produtos'
        console.error(error)
      } finally {
        this.loading = false
      }
    },

    async loadMarcas() {
      try {
        this.marcas = await marcaService.getAll()
      } catch (error) {
        console.error('Erro ao carregar marcas:', error)
      }
    },

    async loadCategorias() {
      try {
        this.categorias = await categoriaService.getAll()
      } catch (error) {
        console.error('Erro ao carregar categorias:', error)
      }
    },

    searchProdutos() {
      // A busca é feita automaticamente através do computed filteredProdutos
    },

    editProduto(produto) {
      this.editingId = produto.id
      this.formData = {
        nome: produto.nome,
        descricao: produto.descricao || '',
        preco: produto.preco,
        marcaId: produto.marcaId,
        categoriaId: produto.categoriaId,
        imagemUrl: produto.imagemUrl || '',
      }
      this.imageError = false
      this.showEditModal = true
    },

    async deleteProduto(id) {
      if (!confirm('Tem certeza que deseja excluir este produto?')) return

      try {
        await produtoService.delete(id)
        this.produtos = this.produtos.filter(p => p.id !== id)
        alert('Produto excluído com sucesso!')
      } catch (error) {
        alert('Erro ao excluir produto')
        console.error(error)
      }
    },

    async saveProduto() {
      this.saving = true

      try {
        const produtoData = {
          ...this.formData,
          marcaId: parseInt(this.formData.marcaId)
        };

        if (this.showCreateModal) {
          const novoProduto = await produtoService.create(produtoData)
          this.produtos.push(novoProduto)
          alert('Produto criado com sucesso!')
        } else {
          await produtoService.update(this.editingId, produtoData)
          await this.loadProdutos()
          alert('Produto atualizado com sucesso!')
        }

        this.closeModals()
      } catch (error) {
        alert('Erro ao salvar produto')
        console.error(error)
      } finally {
        this.saving = false
      }
    },

    closeModals() {
      this.showCreateModal = false
      this.showEditModal = false
      this.editingId = null
      this.imageError = false
      this.formData = {
        nome: '',
        descricao: '',
        preco: 0,
        marcaId: '',
        imagemUrl: '',
      }
    },

    handleImageError(event) {
      event.target.src = '';
    },

    handlePreviewError() {
      this.imageError = true;
    },

    handlePreviewLoad() {
      this.imageError = false;
    }
  }
}
</script>

<style scoped>
.tabela-produtos {
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
    min-width: 800px;
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
    max-width: 300px;
    word-wrap: break-word;
}

.custom-table tbody tr:hover {
    background-color: var(--primary-blue);
    opacity: 0.8;
}

.custom-table tbody tr:hover button {
    border: 2px solid var(--gray-200);
}

.product-thumbnail {
  width: 50px;
  height: 50px;
  object-fit: cover;
  border-radius: 4px;
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
  max-width: 600px;
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
.form-group select,
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

.form-group small {
  display: block;
  margin-top: 5px;
  color: #666;
  font-size: 12px;
}

.image-preview {
  margin-top: 10px;
  text-align: center;
}

.image-preview img {
  max-width: 200px;
  max-height: 200px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.image-error {
  color: #dc3545;
  font-size: 14px;
  margin-top: 5px;
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

