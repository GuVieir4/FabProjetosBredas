<template>
    <nav class="quick-nav" ontouchstart="pauseScroll()" ontouchend="resumeScroll()">
        <ul class="scroll-track" id="scrollTrack">
            <li v-for="categoria in categoriasDisplay" :key="`${categoria.id}-${categoria.duplicate || ''}`">
                <a @click="goToCategory(categoria.id)" href="#">
                    <i class="fas fa-tag"></i> {{ categoria.nome }}
                </a>
            </li>
        </ul>
    </nav>

    <div class="destaque-loja">
        <div class="chamada">
            <h1 id="texto-animacao">SEU SETUP, SEU ESTILO</h1>
            <p>Explore diversas opções de marcas e encontre tudo aquilo que sempre sonhou, com o melhor preço!</p>
            <div class="botoes">
                <router-link to="/produtos" class="btn-primary">Compre agora</router-link>
                <router-link to="/categorias" class="btn-secondary">Conheça nossas categorias</router-link>
            </div>
        </div>
    </div>

    <div class="banner-marcas">
        <img src="https://cdn.simpleicons.org/amd/FFF" alt="AMD logo" width="40" />
        <img src="https://cdn.simpleicons.org/razer/FFF" alt="Razer logo" width="40" />
        <img src="https://cdn.simpleicons.org/logitech/FFF" alt="Logitech logo" width="40" />
        <img src="https://cdn.simpleicons.org/alienware/FFF" alt="Alienware logo" width="40" />
        <img src="https://cdn.simpleicons.org/intel/FFF" alt="Intel logo" width="40" />
    </div>
</template>

<script>
import './SecaoDestaqueComponent.css';
import { categoriaService } from '../../services/categoriaService.js';

export default {
  name: 'SecaoDestaqueComponent',
  data() {
    return {
      categorias: [],
      loading: false,
      error: ''
    };
  },
  computed: {
    categoriasDisplay() {
      // Duplicar as categorias para efeito infinito de scroll
      const duplicated = [...this.categorias, ...this.categorias.map(cat => ({
        ...cat,
        duplicate: 'dup'
      }))];
      return duplicated;
    }
  },
  async mounted() {
    await this.loadCategorias();
  },
  methods: {
    async loadCategorias() {
      this.loading = true;
      this.error = '';

      try {
        // Buscar as primeiras 10 categorias
        const allCategorias = await categoriaService.getAll();
        this.categorias = allCategorias.slice(0, 10);
      } catch (error) {
        this.error = 'Erro ao carregar categorias';
        console.error('Erro ao carregar categorias:', error);
        
        // Fallback com categorias estáticas em caso de erro
        this.categorias = [
          { id: 1, nome: 'Processadores' },
          { id: 2, nome: 'Memória RAM' },
          { id: 3, nome: 'Placas de Vídeo' },
          { id: 4, nome: 'HDs & SSDs' },
          { id: 5, nome: 'Placas-mãe' },
          { id: 6, nome: 'Coolers' },
          { id: 7, nome: 'Fontes' },
          { id: 8, nome: 'Gabinetes' },
          { id: 9, nome: 'Teclados' },
          { id: 10, nome: 'Mouses' }
        ];
      } finally {
        this.loading = false;
      }
    },

    goToCategory(categoriaId) {
      this.$router.push({ 
        name: 'produtos', 
        query: { categoriaId: categoriaId } 
      });
    }
  }
};
</script>