<template>
    <h1 class="titulo-categorias">CATEGORIAS</h1>

    <section class="container-categorias">
        <router-link 
            v-for="categoria in categorias" 
            :key="categoria.id" 
            :to="{ path: '/produtos', query: { categoriaId: categoria.id } }" 
            class="container-categoria" 
            style="text-decoration: none; color: inherit;">
            <h3>{{ categoria.nome }}</h3>
        </router-link>
        <div v-if="categorias.length === 0" class="sem-categorias">
            <p>Nenhuma categoria encontrada.</p>
        </div>
    </section>
</template>

<script setup>
import './CategoriasComponent.css'
import { ref, onMounted } from 'vue'
import { categoriaService } from '@/services'

const categorias = ref([])

// Função para carregar as categorias
const carregarCategorias = async () => {
    try {
        const categoriasData = await categoriaService.getAll()
        categorias.value = categoriasData
    } catch (error) {
        console.error('Erro ao carregar categorias:', error)
        categorias.value = []
    }
}

// Carrega as categorias quando o componente é montado
onMounted(() => {
    carregarCategorias()
})
</script>

