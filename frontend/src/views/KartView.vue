<template>
  <div class="cart-page">
    <div class="container">
      <h1 class="titulo-carrinho">Seu Carrinho</h1>

      <div v-if="loading" class="loading">Carregando carrinho...</div>

      <div v-else-if="error" class="error">{{ error }}</div>

      <div v-else class="secao-carrinho">
        <div class="container-carrinho">
          <div v-if="!carrinho.itens || carrinho.itens.length === 0" class="carrinho-vazio">
            <h3>Seu carrinho está vazio</h3>
            <p>Adicione produtos para começar suas compras!</p>
            <button @click="goToProducts" class="btn-continuar-comprando">Continuar Comprando</button>
          </div>

          <div v-else>
            <div 
              v-for="item in carrinho.itens" 
              :key="item.produtoId"
              class="card-carrinho"
            >
              <div class="imagem-produto">
                <img 
                  :src="item.produtoImagemUrl && item.produtoImagemUrl.trim() !== '' ? item.produtoImagemUrl : '/src/assets/placeholder-product.png'" 
                  :alt="item.produtoNome"
                  @error="handleImageError"
                />
              </div>
              <div class="informacoes-produto">
                <h3 class="nome-produto">{{ item.produtoNome }}</h3>

                <div class="quantidade-container">
                  <button 
                    class="quantidade-btn" 
                    @click="decrementQuantity(item.produtoId)"
                    :disabled="getQuantity(item.produtoId) <= 1"
                  >-</button>

                  <span class="quantidade-display">{{ getQuantity(item.produtoId) }}</span>

                  <button 
                    class="quantidade-btn" 
                    @click="incrementQuantity(item.produtoId)"
                  >+</button>
                </div>

                <h3 class="valor-produto">R$ {{ formatPrice(item.produtoPreco * getQuantity(item.produtoId)) }}</h3>

                <button 
                  @click="removeFromCart(item.produtoId)"
                  class="btn-remover"
                  :disabled="removingProduct === item.produtoId"
                >
                  {{ removingProduct === item.produtoId ? 'Removendo...' : 'Remover' }}
                </button>
              </div>
            </div>
          </div>
        </div>

        <div v-if="carrinho.itens && carrinho.itens.length > 0" class="container-resumo">
          <div class="informacoes-pedido">
            <h3>Resumo do Pedido</h3>
            <div class="informacoes-topico">
              <p>Subtotal</p>
              <h3>R$ {{ formatPrice(subtotal) }}</h3>
            </div>
            <div class="informacoes-topico">
              <p>Desconto</p>
              <h3>R$ 0,00</h3>
            </div>
            <div class="informacoes-topico">
              <p>Frete</p>
              <h3 style="color: #2e54a6;">Grátis</h3>
            </div>
            <div class="informacoes-topico total">
              <p>Total</p>
              <h3>R$ {{ formatPrice(subtotal) }}</h3>
            </div>
          </div>

          <div class="componentes-resumo">
            <form @submit.prevent="aplicarCupom" class="formulario-carrinho">
              <input v-model="cupom" class="input-cupom" type="text" placeholder="Inserir cupom..." />
              <button type="submit">Aplicar</button>
            </form>

            <button @click="finalizarCompra" class="btn-finalizar">
              Finalizar Compra
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { carrinhoService } from '../services/carrinhoService.js';
import { authService } from '../services/authService.js';

export default {
  name: 'KartView',
  data() {
    return {
      carrinho: { itens: [] },
      loading: false,
      error: '',
      removingProduct: null,
      cupom: '',
      quantities: {}
    };
  },
  computed: {
    subtotal() {
      if (!this.carrinho.itens) return 0;
      return this.carrinho.itens.reduce((total, item) => {
        const qnt = this.getQuantity(item.produtoId);
        return total + (item.produtoPreco * qnt);
      }, 0);
    }
  },
  async mounted() {
    await this.loadCarrinho();
  },
  methods: {
    async loadCarrinho() {
      this.loading = true;
      this.error = '';

      try {
        const user = authService.getCurrentUser();
        if (!user) {
          this.$router.push('/login');
          return;
        }

        const dados = await carrinhoService.getByUsuario(user.id);
        this.carrinho = dados;

        if (Array.isArray(dados.itens)) {
          dados.itens.forEach(item => {
            this.quantities[item.produtoId] = item.quantidade || 1;
          });
        }
      } catch (error) {
        this.error = 'Erro ao carregar carrinho';
        console.error(error);
      } finally {
        this.loading = false;
      }
    },

    getQuantity(produtoId) {
      return this.quantities[produtoId] || 1;
    },

    incrementQuantity(produtoId) {
      this.quantities[produtoId] = this.getQuantity(produtoId) + 1;
    },

    decrementQuantity(produtoId) {
      if (this.getQuantity(produtoId) > 1) {
        this.quantities[produtoId] = this.getQuantity(produtoId) - 1;
      }
    },

    async removeFromCart(produtoId) {
      this.removingProduct = produtoId;
      try {
        await carrinhoService.removeProduto(this.carrinho.id, produtoId);
        this.carrinho.itens = this.carrinho.itens.filter(item => item.produtoId !== produtoId);
        delete this.quantities[produtoId];
        alert('Produto removido com sucesso!');
      } catch (error) {
        alert('Erro ao remover produto.');
        console.error(error);
      } finally {
        this.removingProduct = null;
      }
    },

    goToProducts() {
      this.$router.push('/produtos');
    },

    aplicarCupom() {
      if (!this.cupom.trim()) {
        alert('Digite um cupom válido.');
        return;
      }
      alert('Funcionalidade de cupom em breve.');
    },

    finalizarCompra() {
      alert('Funcionalidade de finalização de compra em breve.');
    },

    formatPrice(value) {
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL',
        minimumFractionDigits: 2
      }).format(value);
    },

    handleImageError(e) {
      e.target.src = '/src/assets/placeholder-product.png';
    }
  }
};
</script>

<style scoped>
.cart-page {
  min-height: 100vh;
  background-color: #1a1a1a;
  padding: 20px 0;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
}

.titulo-carrinho {
  margin-top: 50px;
  font-size: 48px;
  font-weight: 700;
  color: #e0e0e0;
  text-align: center;
  margin-bottom: 50px;
}

.loading, .error {
  text-align: center;
  padding: 40px;
  font-size: 1.2rem;
}

.loading {
  color: #e0e0e0;
}

.error {
  color: #dc3545;
}

.secao-carrinho {
  display: flex;
  gap: 48px;
  justify-content: center;
  align-items: flex-start;
}

.container-carrinho {
  width: 715px;
  min-height: 160px;
  background-color: transparent;
  border: 1px solid #e0e0e0;
  border-radius: 20px;
  padding: 20px;
}

.carrinho-vazio {
  text-align: center;
  padding: 60px 20px;
  color: #e0e0e0;
}

.carrinho-vazio h3 {
  font-size: 24px;
  margin-bottom: 10px;
}

.carrinho-vazio p {
  font-size: 16px;
  margin-bottom: 30px;
  color: #b0b0b0;
}

.btn-continuar-comprando {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 12px 24px;
  border-radius: 8px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn-continuar-comprando:hover {
  background-color: #0056b3;
}

.card-carrinho {
  display: flex;
  padding: 20px 0;
  border-bottom: 1px solid #333;
}

.card-carrinho:last-child {
  border-bottom: none;
}

.imagem-produto {
  width: 124px;
  height: 124px;
  background-color: white;
  border-radius: 12px;
  overflow: hidden;
  display: flex;
  align-items: center;
  justify-content: center;
}

.imagem-produto img {
  width: 100%;
  height: 100%;
  object-fit: contain;
}

.informacoes-produto {
  display: flex;
  flex-direction: column;
  margin-left: 20px;
  justify-content: space-between;
  flex: 1;
}

.nome-produto {
  font-size: 20px;
  font-weight: 600;
  color: #e0e0e0;
  margin-bottom: 5px;
}

.marca-produto {
  font-size: 14px;
  color: #b0b0b0;
  margin-bottom: 10px;
}

.quantidade-container {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 10px;
}

.quantidade-btn {
  width: 30px;
  height: 30px;
  border: 2px solid #007bff;
  border-radius: 6px;
  background-color: transparent;
  color: #e0e0e0;
  font-size: 16px;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s ease;
}

.quantidade-btn:hover:not(:disabled) {
  background-color: #007bff;
}

.quantidade-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.quantidade-display {
  font-size: 16px;
  font-weight: bold;
  min-width: 25px;
  text-align: center;
  color: #e0e0e0;
}

.valor-produto {
  font-size: 20px;
  font-weight: bold;
  color: #007bff;
  margin-bottom: 10px;
}

.btn-remover {
  background-color: #dc3545;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
  align-self: flex-start;
  transition: background-color 0.3s;
}

.btn-remover:hover:not(:disabled) {
  background-color: #c82333;
}

.btn-remover:disabled {
  background-color: #6c757d;
  cursor: not-allowed;
}

.container-resumo {
  width: 505px;
  padding: 20px 24px;
  border: 1px solid #e0e0e0;
  border-radius: 20px;
  background-color: #2a2a2a;
}

.informacoes-pedido h3 {
  font-size: 24px;
  color: #e0e0e0;
  margin-bottom: 20px;
}

.informacoes-topico {
  display: flex;
  justify-content: space-between;
  margin-bottom: 15px;
}

.informacoes-topico p {
  color: #b0b0b0;
  font-size: 16px;
  margin: 0;
}

.informacoes-topico h3 {
  font-size: 16px;
  font-weight: bold;
  color: #e0e0e0;
  margin: 0;
}

.informacoes-topico.total {
  border-top: 1px solid #444;
  padding-top: 15px;
  margin-top: 15px;
}

.informacoes-topico.total p,
.informacoes-topico.total h3 {
  font-size: 18px;
  font-weight: bold;
}

.componentes-resumo {
  margin-top: 30px;
}

.formulario-carrinho {
  display: flex;
  gap: 10px;
  margin-bottom: 15px;
}

.input-cupom {
  flex: 1;
  height: 48px;
  border: 1px solid #444;
  border-radius: 8px;
  background-color: #333;
  color: #e0e0e0;
  font-size: 15px;
  padding: 0 15px;
}

.input-cupom::placeholder {
  color: #888;
}

.input-cupom:focus {
  outline: none;
  border-color: #007bff;
}

.formulario-carrinho button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 0 20px;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.formulario-carrinho button:hover {
  background-color: #0056b3;
}

.btn-finalizar {
  background-color: #28a745;
  color: white;
  border: none;
  width: 100%;
  height: 48px;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn-finalizar:hover {
  background-color: #218838;
}

@media (max-width: 768px) {
  .titulo-carrinho {
    font-size: 36px;
  }

  .secao-carrinho {
    flex-direction: column;
    align-items: center;
  }

  .container-carrinho,
  .container-resumo {
    width: 100%;
    max-width: 500px;
  }

  .card-carrinho {
    flex-direction: column;
    text-align: center;
  }

  .informacoes-produto {
    margin-left: 0;
    margin-top: 15px;
    align-items: center;
  }

  .imagem-produto {
    align-self: center;
  }

  .quantidade-container {
    justify-content: center;
  }
}
</style>

