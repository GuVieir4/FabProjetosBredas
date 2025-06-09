<template>
    <section id="Lancamentos">
        <h1 class="lancamentos">LANÇAMENTOS</h1>

        <div v-if="loading" class="loading">
            Carregando lançamentos...
        </div>

        <div v-else-if="error" class="error">
            {{ error }}
        </div>

        <div v-else class="container-lancamentos">
            <div 
                v-for="produto in lancamentos" 
                :key="produto.id"
                class="cards-produtos"
                @click="goToProductDetails(produto.id)"
            >
                <div class="imagem-produto">
                    <img 
                        :src="produto.imagemUrl || ''" 
                        :alt="produto.nome"
                        @error="handleImageError"
                    />
                </div>
                <div class="info-produto">
                    <h4 class="nome-produto">{{ produto.nome }}</h4>
                    <h3 class="valor-produto"><span>R$</span>{{ formatPrice(produto.preco) }}</h3>
                </div>
            </div>
        </div>
        
        <button @click="goToProducts" class="carregar-mais">Ver todos os produtos</button>
    </section>
</template>

<script>
import './LancamentosComponent.css';
import { produtoService } from '../../services/produtoService.js';

export default {
  name: 'LancamentosComponent',
  data() {
    return {
      lancamentos: [],
      loading: false,
      error: ''
    };
  },
  async mounted() {
    await this.loadLancamentos();
  },
  methods: {
    async loadLancamentos() {
      this.loading = true;
      this.error = '';

      try {
        // Buscar os últimos 5 produtos usando o endpoint de lançamentos
        const response = await produtoService.getLancamentos();
        this.lancamentos = response || [];
      } catch (error) {
        this.error = 'Erro ao carregar lançamentos';
        console.error('Erro ao carregar lançamentos:', error);
      } finally {
        this.loading = false;
      }
    },

    goToProductDetails(productId) {
      this.$router.push({ name: 'productDetails', query: { id: productId } });
    },

    goToProducts() {
      this.$router.push('/produtos');
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
  }
};
</script>