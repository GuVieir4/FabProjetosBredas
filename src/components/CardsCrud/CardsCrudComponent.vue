<template>
    <div class="cards-crud">
        <div class="card-crud" id="get">
            <a href="#">Listar</a>
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
        <form action="">
            <div class="modal-header">
                <h1 class="modal-title">Editar Produto</h1>

                <button class="close-modal" data-modal="modal-put" type="button">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            </div>

            <div class="modal-body">
                <div class="input-group">
                    <label for="id">ID</label>
                    <input type="text" id="id_produto" name="id_produto" placeholder="ID" required>
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
                    <label for="email">Valor</label>
                    <input type="number" id="valor_produto" name="valor_produto" placeholder="Valor" required>
                </div>

                <div class="input-group">
                    <label for="text">Categoria</label>
                    <input type="text" id="Categoria" name="Categoria" placeholder="Categoria" required>
                </div>

                <button class="btn-confirm">Confirmar</button>

            </div>
        </form>
    </dialog>

      <dialog id="modal-post">
    <form action="">
      <div class="modal-header">
        <h1 class="modal-title">Adicionar Produto</h1>

        <button class="close-modal" data-modal="modal-post" type="button">
          <i class="fa-solid fa-xmark"></i>
        </button>
      </div>

      <div class="modal-body">
        <div class="input-group">
          <label for="text">Categoria</label>
          <input type="text" id="Categoria" name="Categoria" placeholder="Categoria" required>
        </div>

        <div class="input-group">
          <label for="email">Nome do Produto</label>
          <input type="text" id="nome_produto" name="nome_produto" placeholder="Nome" required>
        </div>

        <div class="input-group">
          <label for="email">Descrição do Produto</label>
          <input type="text" id="descricao_produto" name="descricao_produto" placeholder="Descrição" required>
        </div>

        <div class="input-group">
          <label for="email">Valor</label>
          <input type="number" id="valor_produto" name="valor_produto" placeholder="Valor" required>
        </div>

        <button class="btn-confirm">Confirmar</button>

      </div>
    </form>
  </dialog>

  <dialog id="modal-delete">
    <form action="">
      <div class="modal-header">
        <h1 class="modal-title">Deletar Produto</h1>

        <button class="close-modal" data-modal="modal-delete" type="button">
          <i class="fa-solid fa-xmark"></i>
        </button>
      </div>

      <div class="modal-body">
        <div class="input-group">
          <label for="number">ID</label>
          <input type="text" id="id_produto" name="id_produto" placeholder="ID" required>
        </div>
      </div>

      <button class="btn-confirm">Confirmar</button>
    </form>
  </dialog>
</template>

<script setup>
import './CardsCrudComponent.css'
import { onMounted } from 'vue'

onMounted(() => {
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