<template>
    <aside class="sidebar" id="sidebar">
        <button class="menu-toggle" @click="toggleMenu">☰</button>
        <button @click="selectMenu('voltar')" class="menu-item">
                <span class="material-symbols-outlined">arrow_back</span>
                <span>Voltar</span>
        </button>
        <button @click="selectMenu('produtos')" class="menu-item" :class="{ active: selectedMenu === 'produtos' }">
            <span class="material-symbols-outlined">sell</span>
            <span>Produtos</span>
        </button>
        <button @click="selectMenu('usuarios')" class="menu-item" :class="{ active: selectedMenu === 'usuarios' }">
            <span class="material-symbols-outlined">person</span>
            <span>Usuários</span>
        </button>
        <button @click="selectMenu('categorias')" class="menu-item" :class="{ active: selectedMenu === 'categorias' }">
            <span class="material-symbols-outlined">list</span>
            <span>Categorias</span>
        </button>
        <button @click="selectMenu('marcas')" class="menu-item" :class="{ active: selectedMenu === 'marcas' }">
            <span class="material-symbols-outlined">copyright</span>
            <span>Marcas</span>
        </button>
    </aside>
</template>

<script>
import './SidebarComponent.css';

export default {
  name: 'SidebarComponent',
  props: {
    selectedMenu: {
      type: String,
      default: 'dashboard'
    }
  },
  emits: ['menu-selected'],
  methods: {
    toggleMenu() {
      document.getElementById('sidebar').classList.toggle('collapsed');
    },
    selectMenu(menu) {
      if (menu === 'voltar') {
        this.$router.push('/');
        return;
      }
      this.$emit('menu-selected', menu);
    }
  }
};
</script>