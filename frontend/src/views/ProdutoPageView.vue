<template>
  <div class="produtos-page">
    <div class="container">
      <h1>Produtos</h1>
      
      <div class="content-wrapper">
        <!-- Filtros -->
        <FiltrosComponent
          v-model="filters"
          :marcas="marcas"
          :categorias="categorias"
          @filter-change="applyFilters"
        />

        <!-- Lista de produtos -->
        <main class="produtos-content">
          <div v-if="loading" class="loading">
            Carregando produtos...
          </div>

          <div v-else-if="error" class="error">
            {{ error }}
          </div>

          <div v-else>
            <div class="produtos-grid">
              <div 
                v-for="produto in produtos" 
                :key="produto.id"
                class="produto-card"
                @click="goToProductDetails(produto.id)"
              >
                <div class="produto-image">
                  <img 
                    :src="produto.imagemUrl || ''" 
                    :alt="produto.nome"
                    @error="handleImageError"
                  />
                </div>
                <div class="produto-info">
                  <h3>{{ produto.nome }}</h3>
                  <p class="marca">{{ produto.marca?.nome }}</p>
                  <p class="preco">R$ {{ formatPrice(produto.preco) }}</p>
                  <div class="categorias">
                    <span 
                      v-for="categoria in produto.categorias" 
                      :key="categoria.id"
                      class="categoria-tag"
                    >
                      {{ categoria.nome }}
                    </span>
                  </div>
                  <button 
                    @click.stop="addToCart(produto)"
                    class="add-to-cart-btn"
                    :disabled="addingToCart === produto.id"
                  >
                    {{ addingToCart === produto.id ? 'Adicionando...' : 'Adicionar ao Carrinho' }}
                  </button>
                </div>
              </div>
            </div>

            <!-- Paginação -->
            <div v-if="pagination && pagination.totalPages > 1" class="pagination">
              <button 
                @click="goToPage(pagination.currentPage - 1)"
                :disabled="!pagination.hasPreviousPage"
                class="pagination-btn"
              >
                Anterior
              </button>
              
              <span class="pagination-info">
                Página {{ pagination.currentPage }} de {{ pagination.totalPages }}
              </span>
              
              <button 
                @click="goToPage(pagination.currentPage + 1)"
                :disabled="!pagination.hasNextPage"
                class="pagination-btn"
              >
                Próxima
              </button>
            </div>
          </div>
        </main>
      </div>
    </div>
  </div>
</template>

<script>
import { produtoService } from '../services/produtoService.js';
import { marcaService } from '../services/marcaService.js';
import { categoriaService } from '../services/categoriaService.js';
import { carrinhoService } from '../services/carrinhoService.js';
import { authService } from '../services/authService.js';
import FiltrosComponent from '../components/Filtros/FiltrosComponent.vue';
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()

const filters = ref({
  search: '',
  marcaIds: [],
  categoriaIds: []
})

export default {
  name: 'ProdutoPageView',
  data() {
    return {
      produtos: [],
      marcas: [],
      categorias: [],
      pagination: null,
      loading: false,
      error: '',
      addingToCart: null,
      filters: {
        search: '',
        marcaIds: [],
        categoriaIds: [],
        page: 1,
        pageSize: 12
      }
    };
  },
  async mounted() {
    await this.loadFiltersData();
    await this.loadProdutos();
    this.applyUrlFilters();
  },
  methods: {
    async loadFiltersData() {
      try {
        const [marcasResponse, categoriasResponse] = await Promise.all([
          marcaService.getAll(),
          categoriaService.getAll()
        ]);
        
        this.marcas = marcasResponse;
        this.categorias = categoriasResponse;
      } catch (error) {
        console.error('Erro ao carregar dados dos filtros:', error);
      }
    },

    async loadProdutos() {
      this.loading = true;
      this.error = '';

      try {
        const params = {
          page: this.filters.page,
          pageSize: this.filters.pageSize
        };

        if (this.filters.search) {
          params.search = this.filters.search;
        }

        if (this.filters.marcaIds.length > 0) {
          params.marcaId = this.filters.marcaIds[0]; // Por simplicidade, usar apenas a primeira marca selecionada
        }

        if (this.filters.categoriaIds.length > 0) {
          params.categoriaId = this.filters.categoriaIds[0]; // Por simplicidade, usar apenas a primeira categoria selecionada
        }

        const response = await produtoService.getAllWithFilters(params);
        this.produtos = response.produtos;
        this.pagination = response.pagination;
      } catch (error) {
        this.error = 'Erro ao carregar produtos';
        console.error('Erro ao carregar produtos:', error);
      } finally {
        this.loading = false;
      }
    },

    applyUrlFilters() {
      const query = this.$route.query;
      
      if (query.marcaId) {
        this.filters.marcaIds = [parseInt(query.marcaId)];
      }
      
      if (query.categoriaId) {
        this.filters.categoriaIds = [parseInt(query.categoriaId)];
      }
      
      if (this.filters.marcaIds.length > 0 || this.filters.categoriaIds.length > 0) {
        this.loadProdutos();
      }
    },

    async applyFilters() {
      this.filters.page = 1; // Reset para primeira página
      await this.loadProdutos();
    },

    clearFilters() {
      this.filters = {
        search: '',
        marcaIds: [],
        categoriaIds: [],
        page: 1,
        pageSize: 12
      };
      this.loadProdutos();
    },

    async goToPage(page) {
      this.filters.page = page;
      await this.loadProdutos();
    },

    goToProductDetails(productId) {
      this.$router.push({ name: 'productDetails', query: { id: productId } });
    },

    async addToCart(produto) {
      if (!authService.isAuthenticated()) {
        this.$router.push({ 
          name: 'login', 
          query: { redirect: this.$route.fullPath } 
        });
        return;
      }

      this.addingToCart = produto.id;
      
      try {
        const user = authService.getCurrentUser();
        await carrinhoService.addProdutoToUsuario(user.id, produto.id);
        
        // Mostrar mensagem de sucesso
        alert('Produto adicionado ao carrinho!');
      } catch (error) {
        if (error.message && error.message.includes('já está no carrinho')) {
          alert('Este produto já está no seu carrinho!');
        } else {
          alert('Erro ao adicionar produto ao carrinho');
        }
        console.error('Erro ao adicionar ao carrinho:', error);
      } finally {
        this.addingToCart = null;
      }
    },

    formatPrice(price) {
      return new Intl.NumberFormat('pt-BR', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      }).format(price);
    },

    handleImageError(event) {
      event.target.src = '';
    }
  },
  components: {
    FiltrosComponent
  },
};

onMounted(() => {
  const query = route.query

  filters.value = {
    search: query.search || '',
    marcaIds: Array.isArray(query.marcaId)
      ? query.marcaId.map(id => parseInt(id))
      : query.marcaId
        ? [parseInt(query.marcaId)]
        : [],
    categoriaIds: Array.isArray(query.categoriaId)
      ? query.categoriaId.map(id => parseInt(id))
      : query.categoriaId
        ? [parseInt(query.categoriaId)]
        : []
  }

  applyFilters() // ou carregar produtos com filtros
})

</script>

<style scoped>
.produtos-page {
  min-height: 100vh;
  background-color: #0d1423;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

.container h1 {
  margin-bottom: 30px;
  color: #333;
}

.content-wrapper {
  display: grid;
  grid-template-columns: 250px 1fr;
  gap: 30px;
}

.produtos-content {
  background: #0d1423;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.loading, .error {
  text-align: center;
  padding: 40px;
  color: #666;
}

.error {
  color: #dc3545;
}

.produtos-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 20px;
  margin-bottom: 30px;
}

.produto-card {
  background-color: #fff;
  border: 1px solid #eee;
  border-radius: 8px;
  overflow: hidden;
  transition: transform 0.3s, box-shadow 0.3s;
  cursor: pointer;
}

.produto-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

.produto-image {
  height: 200px;
  width: 200px;
  justify-self: center;
  overflow: hidden;
}

.produto-image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.produto-info {
  padding: 15px;
}

.produto-info h3 {
  margin-bottom: 8px;
  color: #333;
  font-size: 16px;
}

.marca {
  color: #666;
  font-size: 14px;
  margin-bottom: 8px;
}

.preco {
  font-size: 18px;
  font-weight: bold;
  color: #007bff;
  margin-bottom: 10px;
}

.categorias {
  margin-bottom: 15px;
}

.categoria-tag {
  display: inline-block;
  background-color: #cbcbcb;
  color: #495057;
  padding: 2px 8px;
  border-radius: 12px;
  font-size: 12px;
  margin-right: 5px;
  margin-bottom: 5px;
}

.add-to-cart-btn {
  width: 100%;
  padding: 10px;
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.add-to-cart-btn:hover:not(:disabled) {
  background-color: #218838;
}

.add-to-cart-btn:disabled {
  background-color: #6c757d;
  cursor: not-allowed;
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 20px;
  margin-top: 30px;
}

.pagination-btn {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.pagination-btn:hover:not(:disabled) {
  background-color: #0056b3;
}

.pagination-btn:disabled {
  background-color: #6c757d;
  cursor: not-allowed;
}

.pagination-info {
  color: #666;
  font-weight: 500;
}

@media (max-width: 768px) {
  .content-wrapper {
    grid-template-columns: 1fr;
  }
  
  
  .produtos-grid {
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  }
}


</style>

