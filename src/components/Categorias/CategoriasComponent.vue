<template>
    <h1 class="titulo-categorias">CATEGORIAS</h1>

    <section class="container-categorias">
        <div class="container-headset">
            <h3>Headsets</h3>
            <img src="@/assets/ProdutosCategorias/HeadsetCategorias.png" alt="Imagem de um headset">
        </div>
        <div class="container-soundbar">
            <h3>Soundbar</h3>
            <img src="@/assets/ProdutosCategorias/SoundbarCategorias.png" alt="Imagem de um sounbdar">
        </div>
        <div class="container-teclado">
            <h3>Teclados</h3>
            <img src="@/assets/ProdutosCategorias/TecladoCategorias.png" alt="Imagem de um teclado">
        </div>
        <router-link to="/categoryPage" class="container-mouse" style="text-decoration: none; color: inherit;">
            <h3>Mouses</h3>
            <img src="@/assets/ProdutosCategorias/MouseCategorias.png" alt="Imagem de um mouse">
        </router-link>
    </section>
</template>

<script setup>
import './CategoriasComponent.css'
</script>
