<template>
    <section class="mais-marcas">
        <h1 class="titulo-secao">MARCAS MAIS VENDIDAS</h1>
        <div class="cards-marcas">
            <div v-for="marca in marcas" :key="marca.id" class="marca-item" @click="navegarParaProdutosMarca(marca.id)">
                <img class="card-marcas" :src="getImagemMarca(marca.nome)" :alt="`Logo ${marca.nome}`">
            </div>
            <div v-if="marcas.length === 0" class="sem-marcas">
                <p>Nenhuma marca encontrada.</p>
            </div>
        </div>
    </section>
</template>

<script setup>
import './SecaoMarcasComponent.css'
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { marcaService } from '@/services'

const router = useRouter()
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

// Função para obter a imagem da marca com base no nome
const getImagemMarca = (nome) => {
    const nomeNormalizado = nome.toLowerCase()
    if (nomeNormalizado.includes('redragon')) {
        return new URL('@/assets/LogoRedragon.svg', import.meta.url).href
    } else if (nomeNormalizado.includes('razer')) {
        return new URL('@/assets/LogoRazer.svg', import.meta.url).href
    } else if (nomeNormalizado.includes('logitech')) {
        return new URL('@/assets/LogoLogitech.svg', import.meta.url).href
    } else if (nomeNormalizado.includes('hyperx')) {
        return new URL('@/assets/LogoHyperX.svg', import.meta.url).href
    } else {
        // Logo padrão caso não encontre correspondência
        return new URL('@/assets/LogoRedragon.svg', import.meta.url).href
    }
}

// Função para navegar para a página de produtos da marca
const navegarParaProdutosMarca = (marcaId) => {
    router.push({ path: '/marcaPage', query: { id: marcaId } })
}

// Carrega as marcas quando o componente é montado
onMounted(() => {
    carregarMarcas()
})
</script>

<style scoped>
.marca-item {
    cursor: pointer;
    transition: transform 0.3s ease;
}

.marca-item:hover {
    transform: scale(1.05);
}

.sem-marcas {
    width: 100%;
    text-align: center;
    padding: 20px;
    color: #666;
}
</style>

