<template>
    <div class="cards-crud">
        <div class="card-crud" id="get">
            <a href="#" @click.prevent="listarProdutos">Listar</a>
            <span class="material-symbols-outlined">table_view</span>
        </div>
        <div class="card-crud open-modal" id="post" data-modal="modal-post">
            <a href="#">Adicionar</a>
            <span class="material-symbols-outlined">add_circle</span>
        </div>
        <div class="card-crud open-modal" id="put" data-modal="modal-put">
            <a href="#">Editar</a>
            <span class="material-symbols-outlined">edit</span>
        </div>
        <div class="card-crud open-modal" id="delete" data-modal="modal-delete">
            <a href="#">Deletar</a>
            <span class="material-symbols-outlined">delete</span>
        </div>
    </div>

    <dialog id="modal-put">
        <form @submit.prevent="atualizarProduto">
            <div class="modal-header">
                <h1 class="modal-title">Editar Produto</h1>

                <button class="close-modal" data-modal="modal-put" type="button">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            </div>

            <div class="modal-body">
                <div class="input-group">
                    <label for="id">ID</label>
                    <input type="text" id="id_produto" name="id_produto" placeholder="ID" required readonly>
                </div>

                <div class="input-group">
                    <label for="nome_produto">Nome do Produto</label>
                    <input type="text" id="nome_produto" name="nome_produto" placeholder="Nome" required>
                </div>

                <div class="input-group">
                    <label for="descricao_produto">Descrição do Produto</label>
                    <input type="text" id="descricao_produto" name="descricao_produto" placeholder="Descrição" required>
                </div>

                <div class="input-group">
                    <label for="valor_produto">Valor</label>
                    <input type="number" id="valor_produto" name="valor_produto" placeholder="Valor" required>
                </div>

                <div class="input-group">
                    <label for="marca_id">Marca ID</label>
                    <select id="marca_id" name="marca_id" required>
                        <option v-for="marca in marcas" :key="marca.id" :value="marca.id">{{ marca.nome }}</option>
                    </select>
                </div>

                <div class="input-group">
                    <label>Categorias</label>
                    <div class="categorias-checkbox">
                        <div v-for="categoria in categorias" :key="categoria.id" class="categoria-item">
                            <input type="checkbox" :id="'categoria_' + categoria.id" :value="categoria.id" v-model="categoriasSelected">
                            <label :for="'categoria_' + categoria.id">{{ categoria.nome }}</label>
                        </div>
                    </div>
                </div>

                <button class="btn-confirm" type="submit">Confirmar</button>
            </div>
        </form>
    </dialog>

    <dialog id="modal-post">
        <form @submit.prevent="criarProduto">
            <div class="modal-header">
                <h1 class="modal-title">Adicionar Produto</h1>

                <button class="close-modal" data-modal="modal-post" type="button">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            </div>

            <div class="modal-body">
                <div class="input-group">
                    <label for="nome_produto_novo">Nome do Produto</label>
                    <input type="text" id="nome_produto_novo" v-model="novoProduto.nome" placeholder="Nome" required>
                </div>

                <div class="input-group">
                    <label for="descricao_produto_novo">Descrição do Produto</label>
                    <input type="text" id="descricao_produto_novo" v-model="novoProduto.descricao" placeholder="Descrição" required>
                </div>

                <div class="input-group">
                    <label for="valor_produto_novo">Valor</label>
                    <input type="number" id="valor_produto_novo" v-model="novoProduto.preco" placeholder="Valor" required>
                </div>

                <div class="input-group">
                    <label for="marca_id_novo">Marca</label>
                    <select id="marca_id_novo" v-model="novoProduto.marcaId" required>
                        <option v-for="marca in marcas" :key="marca.id" :value="marca.id">{{ marca.nome }}</option>
                    </select>
                </div>

                <div class="input-group">
                    <label>Categorias</label>
                    <div class="categorias-checkbox">
                        <div v-for="categoria in categorias" :key="categoria.id" class="categoria-item">
                            <input type="checkbox" :id="'categoria_novo_' + categoria.id" :value="categoria.id" v-model="novoProduto.categoriaIds">
                            <label :for="'categoria_novo_' + categoria.id">{{ categoria.nome }}</label>
                        </div>
                    </div>
                </div>

                <div class="input-group">
                    <label for="imagem_url_novo">URL da Imagem</label>
                    <input type="text" id="imagem_url_novo" v-model="novoProduto.imagemUrl" placeholder="URL da imagem">
                </div>

                <button class="btn-confirm" type="submit">Confirmar</button>
            </div>
        </form>
    </dialog>

    <dialog id="modal-delete">
        <form @submit.prevent="deletarProduto">
            <div class="modal-header">
                <h1 class="modal-title">Deletar Produto</h1>

                <button class="close-modal" data-modal="modal-delete" type="button">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            </div>

            <div class="modal-body">
                <div class="input-group">
                    <label for="id_produto_delete">ID</label>
                    <input type="text" id="id_produto_delete" v-model="produtoIdDelete" placeholder="ID" required>
                </div>
            </div>

            <button class="btn-confirm" type="submit">Confirmar</button>
        </form>
    </dialog>
</template>

<script setup>
import './CardsCrudComponent.css'
import { ref, onMounted } from 'vue'
import { produtoService, categoriaService, marcaService } from '@/services'

// Referências para os dados
const marcas = ref([])
const categorias = ref([])
const categoriasSelected = ref([])
const produtoIdDelete = ref('')
const novoProduto = ref({
    nome: '',
    descricao: '',
    preco: 0,
    marcaId: null,
    categoriaIds: [],
    imagemUrl: ''
})

// Função para carregar marcas e categorias
const carregarDados = async () => {
    try {
        const [marcasData, categoriasData] = await Promise.all([
            marcaService.getAll(),
            categoriaService.getAll()
        ])
        
        marcas.value = marcasData
        categorias.value = categoriasData
    } catch (error) {
        console.error('Erro ao carregar dados:', error)
    }
}

// Função para listar produtos (recarregar a tabela)
const listarProdutos = () => {
    // Emitir um evento para recarregar a tabela de produtos
    window.dispatchEvent(new CustomEvent('recarregar-produtos'))
}

// Função para criar um novo produto
const criarProduto = async () => {
    try {
        await produtoService.create({
            nome: novoProduto.value.nome,
            descricao: novoProduto.value.descricao,
            preco: parseFloat(novoProduto.value.preco),
            marcaId: parseInt(novoProduto.value.marcaId),
            categoriaIds: novoProduto.value.categoriaIds,
            imagemUrl: novoProduto.value.imagemUrl
        })
        
        // Limpar o formulário
        novoProduto.value = {
            nome: '',
            descricao: '',
            preco: 0,
            marcaId: null,
            categoriaIds: [],
            imagemUrl: ''
        }
        
        // Fechar o modal
        document.getElementById('modal-post').close()
        
        // Recarregar a tabela
        listarProdutos()
        
        alert('Produto criado com sucesso!')
    } catch (error) {
        console.error('Erro ao criar produto:', error)
        alert('Erro ao criar o produto. Verifique os dados e tente novamente.')
    }
}

// Função para atualizar um produto existente
const atualizarProduto = async (event) => {
    const form = event.target
    const id = form.id_produto.value
    const nome = form.nome_produto.value
    const descricao = form.descricao_produto.value
    const preco = form.valor_produto.value
    const marcaId = form.marca_id.value
    
    try {
        await produtoService.update(id, {
            nome,
            descricao,
            preco: parseFloat(preco),
            marcaId: parseInt(marcaId),
            categoriaIds: categoriasSelected.value,
            imagemUrl: '' // Manter a imagem atual
        })
        
        // Fechar o modal
        document.getElementById('modal-put').close()
        
        // Recarregar a tabela
        listarProdutos()
        
        alert('Produto atualizado com sucesso!')
    } catch (error) {
        console.error('Erro ao atualizar produto:', error)
        alert('Erro ao atualizar o produto. Verifique os dados e tente novamente.')
    }
}

// Função para deletar um produto
const deletarProduto = async () => {
    const id = produtoIdDelete.value
    
    if (!id) {
        alert('Por favor, informe o ID do produto.')
        return
    }
    
    try {
        await produtoService.delete(id)
        
        // Limpar o campo
        produtoIdDelete.value = ''
        
        // Fechar o modal
        document.getElementById('modal-delete').close()
        
        // Recarregar a tabela
        listarProdutos()
        
        alert('Produto excluído com sucesso!')
    } catch (error) {
        console.error('Erro ao excluir produto:', error)
        alert('Erro ao excluir o produto. Verifique o ID e tente novamente.')
    }
}

onMounted(() => {
    // Carregar dados iniciais
    carregarDados()
    
    // Configurar os modais
    const openButtons = document.querySelectorAll('.open-modal')
    const closeButtons = document.querySelectorAll('.close-modal')

    openButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalID = button.getAttribute('data-modal')
            const modal = document.getElementById(modalID)
            modal?.showModal()
        })
    })

    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalID = button.getAttribute('data-modal')
            const modal = document.getElementById(modalID)
            modal?.close()
        })
    })
})
</script>

<style scoped>
.categorias-checkbox {
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
    margin-top: 5px;
}

.categoria-item {
    display: flex;
    align-items: center;
    gap: 5px;
}

.categoria-item input[type="checkbox"] {
    margin: 0;
}
</style>

