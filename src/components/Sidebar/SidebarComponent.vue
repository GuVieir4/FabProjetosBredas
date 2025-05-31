<template>
    <aside class="sidebar" id="sidebar">
        <button class="menu-toggle" @click="toggleMenu">☰</button>
        <div class="menu-item">
            <span class="material-symbols-outlined">dashboard</span>
            <span>Dashboard</span>
        </div>
        <div class="menu-item">
            <span class="material-symbols-outlined">inventory_2</span>
            <span>Produtos</span>
        </div>
        <div class="menu-item">
            <span class="material-symbols-outlined">groups</span>
            <span>Clientes</span>
        </div>
        <div class="menu-item">
            <span class="material-symbols-outlined">shopping_cart</span>
            <span>Pedidos</span>
        </div>
        <div class="menu-item">
            <span class="material-symbols-outlined">bar_chart</span>
            <span>Relatórios</span>
        </div>
        <div class="menu-item">
            <span class="material-symbols-outlined">settings</span>
            <span>Configurações</span>
        </div>
    </aside>
</template>

<script setup>
import './SidebarComponent.css'

function toggleMenu() {
  document.getElementById('sidebar').classList.toggle('collapsed');
}
</script>