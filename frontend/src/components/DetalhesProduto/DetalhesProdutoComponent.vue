<template>
    <section class="informacoes-produto">
        <div v-if="loading" class="loading">
            Carregando produto...
        </div>

        <div v-else-if="error" class="error">
            {{ error }}
        </div>

        <div v-else-if="produto" class="imagens-produto">
            <div class="imagens-produto-vertical">
                <img 
                    :src="produto.imagemUrl || ''" 
                    :alt="produto.nome" 
                    class="imagem-vertical"
                    @error="handleImageError"
                >
                <img 
                    :src="produto.imagemUrl || ''" 
                    :alt="produto.nome" 
                    class="imagem-vertical"
                    @error="handleImageError"
                >
                <img 
                    :src="produto.imagemUrl || ''" 
                    :alt="produto.nome" 
                    class="imagem-vertical"
                    @error="handleImageError"
                >
            </div>
            <img 
                :src="produto.imagemUrl || ''" 
                :alt="produto.nome" 
                class="imagem-produto-principal"
                @error="handleImageError"
            >
            <div class="detalhes-produto">
                <h1 class="nome-produto">{{ produto.nome }}</h1>
                <h1 class="valor-produto">R$ {{ formatPrice(produto.preco) }}</h1>
                <p class="descricao-produto">{{ produto.descricao }}</p>
                
                <div class="quantidade-container">
                    <button 
                        class="quantidade-btn" 
                        @click="decrementQuantity"
                        :disabled="quantidade <= 1"
                    >
                        -
                    </button>
                    <span class="quantidade-display">{{ quantidade }}</span>
                    <button 
                        class="quantidade-btn" 
                        @click="incrementQuantity"
                    >
                        +
                    </button>
                </div>
                
                <button 
                    class="botao-carrinho" 
                    @click="addToCart"
                    :disabled="addingToCart"
                >
                    {{ addingToCart ? 'Adicionando...' : 'Adicionar ao carrinho' }}
                </button>
                
                <div v-if="successMessage" class="success-message">
                    {{ successMessage }}
                </div>
            </div>
        </div>
    </section>
</template>

<script>
import './DetalhesProdutoComponent.css';
import { produtoService } from '../../services/produtoService.js';
import { carrinhoService } from '../../services/carrinhoService.js';

export default {
  name: 'DetalhesProdutoComponent',
  data() {
    return {
      produto: null,
      loading: false,
      error: '',
      quantidade: 1,
      addingToCart: false,
      successMessage: ''
    };
  },
  async mounted() {
    await this.loadProduto();
  },
  watch: {
    '$route.query.id': {
      handler() {
        this.loadProduto();
      },
      immediate: true
    }
  },
  methods: {
    async loadProduto() {
      const productId = this.$route.query.id;
      
      if (!productId) {
        this.error = 'ID do produto não fornecido';
        return;
      }

      this.loading = true;
      this.error = '';

      try {
        this.produto = await produtoService.getById(productId);
      } catch (error) {
        this.error = 'Erro ao carregar produto';
        console.error('Erro ao carregar produto:', error);
      } finally {
        this.loading = false;
      }
    },

    incrementQuantity() {
      this.quantidade++;
    },

    decrementQuantity() {
      if (this.quantidade > 1) {
        this.quantidade--;
      }
    },

    async addToCart() {
      if (!this.produto) return;

      this.addingToCart = true;
      this.successMessage = '';

      try {
        await carrinhoService.addItem(this.produto.id, this.quantidade);
        this.successMessage = `${this.quantidade} item(s) adicionado(s) ao carrinho!`;
        
        // Limpar mensagem após 3 segundos
        setTimeout(() => {
          this.successMessage = '';
        }, 3000);
        
        // Resetar quantidade
        this.quantidade = 1;
      } catch (error) {
        this.error = 'Erro ao adicionar produto ao carrinho';
        console.error('Erro ao adicionar ao carrinho:', error);
      } finally {
        this.addingToCart = false;
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
  }
};
</script>

