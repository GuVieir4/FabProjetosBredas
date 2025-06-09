<template>
  <div class="category-page">
    <div class="container">
      <h1>Categorias</h1>
      
      <div v-if="loading" class="loading">
        Carregando categorias...
      </div>

      <div v-else-if="error" class="error">
        {{ error }}
      </div>

      <div v-else class="categorias-grid">
        <div 
          v-for="categoria in categorias" 
          :key="categoria.id"
          class="categoria-card"
          @click="goToProducts(categoria.id)"
        >
          <div class="categoria-info">
            <h3>{{ categoria.nome }}</h3>
            <p v-if="categoria.descricao">{{ categoria.descricao }}</p>
            <button class="view-products-btn">
              Ver Produtos
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { categoriaService } from '../services/categoriaService.js';

export default {
  name: 'CategoryPageView',
  data() {
    return {
      categorias: [],
      loading: false,
      error: ''
    };
  },
  async mounted() {
    await this.loadCategorias();
  },
  methods: {
    async loadCategorias() {
      this.loading = true;
      this.error = '';

      try {
        this.categorias = await categoriaService.getAll();
      } catch (error) {
        this.error = 'Erro ao carregar categorias';
        console.error('Erro ao carregar categorias:', error);
      } finally {
        this.loading = false;
      }
    },

    goToProducts(categoriaId) {
      this.$router.push({ 
        name: 'produtos', 
        query: { categoriaId: categoriaId } 
      });
    },

    handleImageError(event) {
      event.target.src = '';
    }
  }
};
</script>

<style scoped>
.category-page {
  min-height: 100vh;
  background-color: #0d1423;
  padding: 20px 0;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
}

.container h1 {
  text-align: center;
  margin-bottom: 40px;
  color: #333;
  font-size: 2.5rem;
}

.loading, .error {
  text-align: center;
  padding: 40px;
  font-size: 1.2rem;
}

.loading {
  color: #666;
}

.error {
  color: #dc3545;
}

.categorias-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 30px;
  margin-top: 30px;
}

.categoria-card {
  background: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s, box-shadow 0.3s;
  cursor: pointer;
}

.categoria-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
}

.categoria-info {
  padding: 25px;
  text-align: center;
}

.categoria-info h3 {
  margin-bottom: 10px;
  color: #0d1423;
  font-size: 1.5rem;
  font-weight: 600;
}

.categoria-info p {
  color: #666;
  margin-bottom: 20px;
  line-height: 1.5;
}

.view-products-btn {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 12px 24px;
  border-radius: 6px;
  font-size: 1rem;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.3s;
  width: 100%;
}

.view-products-btn:hover {
  background-color: #0056b3;
}

@media (max-width: 768px) {
  .container h1 {
    font-size: 2rem;
  }
  
  .categorias-grid {
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
    gap: 20px;
  }
  
  .categoria-info {
    padding: 20px;
  }
}
</style>