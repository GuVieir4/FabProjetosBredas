<template>
  <div class="admin-dashboard">
    <Sidebar @menu-selected="handleMenuSelection" />
    <section class="content">
      <div v-if="selectedMenu === 'produtos'">
        <h2>Gerenciar Produtos</h2>
        <TabelaProdutos />
      </div>
      
      <div v-else-if="selectedMenu === 'usuarios'">
        <h2>Gerenciar Usuários</h2>
        <TabelaUsuarios />
      </div>
      
      <div v-else-if="selectedMenu === 'categorias'">
        <h2>Gerenciar Categorias</h2>
        <TabelaCategorias />
      </div>
      
      <div v-else-if="selectedMenu === 'marcas'">
        <h2>Gerenciar Marcas</h2>
        <TabelaMarcas />
      </div>
      
      <div v-else>
         <h2>Gerenciar Produtos</h2>
        <TabelaProdutos />
      </div>
    </section>
  </div>
</template>

<script>
import Sidebar from '../components/Sidebar/SidebarComponent.vue';
import CardsCrud from '../components/CardsCrud/CardsCrudComponent.vue';
import TabelaProdutos from '../components/TabelaProdutos/TabelaProdutosComponent.vue';
import TabelaUsuarios from '../components/TabelaUsuarios/TabelaUsuariosComponent.vue';
import TabelaCategorias from '../components/TabelaCategorias/TabelaCategoriasComponent.vue';
import TabelaMarcas from '../components/TabelaMarcas/TabelaMarcasComponent.vue';

export default {
  name: 'AdminDashboardView',
  components: {
    Sidebar,
    CardsCrud,
    TabelaProdutos,
    TabelaUsuarios,
    TabelaCategorias,
    TabelaMarcas
  },
  data() {
    return {
      selectedMenu: 'dashboard'
    };
  },
  methods: {
    handleMenuSelection(menu) {
      this.selectedMenu = menu;
    }
  }
};
</script>

<style scoped>
.admin-dashboard {
  height: 100vh;
  display: flex;
  background-color: #0d1423;
}

.content {
  flex: 1;
  padding: 40px;
  overflow-y: auto;
}

.content h2 {
  margin-bottom: 30px;
  color: #333;
  font-size: 2rem;
}
</style>