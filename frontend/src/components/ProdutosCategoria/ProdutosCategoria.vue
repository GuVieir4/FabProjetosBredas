<template>
    <section class="container-produtos">
        <div class="informacoes" style="display: flex; margin-bottom: 20px; justify-content: space-between;">
            <button style="font-size: 32px; font-weight: bold; color: #2e54a6;
                background-color: transparent; border: none; cursor: pointer;" class="open-modal"
                data-modal="modal-filtros">{{ categoriaNome }}</button>
        </div>
        <div class="cards-produto-categorias">
            <div v-for="produto in produtos" :key="produto.id" class="card-produto-categoria">
                <img :src="produto.imagemUrl || '@/assets/MouseAttackShark.svg'" :alt="produto.nome">
                <h3 class="nome-produto">{{ produto.nome }}</h3>
                <h3 class="valor-produto">R${{ produto.preco }}</h3>
            </div>
            <div v-if="produtos.length === 0" class="sem-produtos">
                <p>Nenhum produto encontrado nesta categoria.</p>
            </div>
        </div>
        <hr>
    </section>
</template>

<script setup>
import './ProdutosCategoria.css'
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { produtoService, categoriaService } from '@/services'

const route = useRoute()
const produtos = ref([])
const categoriaNome = ref('Produtos')
const categoriaId = ref(null)

// Função para carregar os produtos da categoria
const carregarProdutos = async () => {
    try {
        // Verifica se há um ID de categoria na rota
        const id = route.query.id || 1 // ID padrão caso não seja especificado
        categoriaId.value = id
        
        // Busca os produtos da categoria
        const produtosData = await produtoService.getByCategoria(id)
        produtos.value = produtosData
        
        // Busca o nome da categoria
        try {
            const categoria = await categoriaService.getById(id)
            categoriaNome.value = categoria.nome
        } catch (error) {
            console.error('Erro ao buscar nome da categoria:', error)
            categoriaNome.value = 'Produtos'
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

