<template>
    <h1 class="titulo-marcas">MARCAS</h1>

    <section class="container-marcas">
        <router-link 
            v-for="marca in marcas" 
            :key="marca.id" 
            :to="{ path: '/categoryPage', query: { id: marca.id } }" 
            class="container-marca" 
            style="text-decoration: none; color: inherit;">
            <h3>{{ marca.nome }}</h3>
        </router-link>
        
        <div v-if="marcas.length === 0" class="sem-marcas">
            <p>Nenhuma marca encontrada.</p>
        </div>
    </section>
</template>

<script setup>
import './MarcasComponent.css'
import { ref, onMounted } from 'vue'
import { marcaService } from '@/services'

const marcas = ref([])

// Função para carregar as marcas
const carregarMarcas = async () => {
    try {
        const marcasData = await marcaService.getAll()
        marcas.value = marcasData
    } catch (error) {
        console.error('Erro ao carregar marcas:', error)
        marcas.value = []
    }
}

onMounted(() => {
    carregarMarcas()
})
</script>

