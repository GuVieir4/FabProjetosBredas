<template>
    <body class="bodyCategory">
        <Filtros/>
        <ProdutosCategoria/>
    </body>
</template>

<style scoped>
.bodyCategory {
    display: flex;
    justify-content: center;
    gap: 40px;
    padding-top: 40px;
}
</style>

<script setup>
import Filtros from '../components/Filtros/FiltrosComponent.vue'
import ProdutosCategoria from '../components/ProdutosCategoria/ProdutosCategoria..vue'
</script>