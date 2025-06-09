<template>
    <section class="container-produtos">
        <div class="informacoes" style="display: flex; margin-bottom: 20px; justify-content: space-between;">
            <button style="font-size: 32px; font-weight: bold; color: #2e54a6;
                background-color: transparent; border: none; cursor: pointer;" class="open-modal"
                data-modal="modal-filtros">{{ marcaNome }}</button>
        </div>
        <div class="cards-produto-categorias">
            <div v-for="produto in produtos" :key="produto.id" class="card-produto-categoria">
                <img :src="produto.imagemUrl || '@/assets/MouseAttackShark.svg'" :alt="produto.nome">
                <h3 class="nome-produto">{{ produto.nome }}</h3>
                <h3 class="valor-produto">R${{ produto.preco }}</h3>
            </div>
            <div v-if="produtos.length === 0" class="sem-produtos">
                <p>Nenhum produto encontrado desta marca.</p>
            </div>
        </div>
        <hr>
    </section>
</template>

<script setup>
import './ProdutosMarca.css'
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { produtoService, marcaService } from '@/services'

const route = useRoute()
const produtos = ref([])
const marcaNome = ref('Produtos')
const marcaId = ref(null)

// Função para carregar os produtos da marca
const carregarProdutos = async () => {
    try {
        // Verifica se há um ID de marca na rota
        const id = route.query.id || 1 // ID padrão caso não seja especificado
        marcaId.value = id
        
        // Busca os produtos da marca
        const produtosData = await produtoService.getByMarca(id)
        produtos.value = produtosData
        
        // Busca o nome da marca
        try {
            const marca = await marcaService.getById(id)
            marcaNome.value = marca.nome
        } catch (error) {
            console.error('Erro ao buscar nome da marca:', error)
            marcaNome.value = 'Produtos'
        }
    } catch (error) {
        console.error('Erro ao carregar produtos:', error)
        produtos.value = []
    }
}

// Carrega os produtos quando o componente é montado
onMounted(() => {
    carregarProdutos()
})
</script>

