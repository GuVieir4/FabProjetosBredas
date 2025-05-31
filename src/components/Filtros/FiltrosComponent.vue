<script setup>
import { onMounted } from 'vue'
import './FiltrosComponent.css'

onMounted(() => {
  const botoes = document.querySelectorAll('.item-cores')

  botoes.forEach(botao => {
    botao.addEventListener('click', () => {
      const ativo = botao.classList.contains('botao-ativo')

      botoes.forEach(b => b.classList.remove('botao-ativo'))

      if (!ativo) {
        botao.classList.add('botao-ativo')
      }
    })
  })
})
</script>

<template>
  <aside class="aside-desktop">
    <h3>Marcas</h3>
    <hr>
    <ul class="lista-marcas">
      <li class="item-marcas">Attack Shark</li>
      <li class="item-marcas">Corsair</li>
      <li class="item-marcas">Logitech</li>
      <li class="item-marcas">Razer</li>
      <li class="item-marcas">Redragon</li>
    </ul>
    <hr>
    <h3>Cores</h3>
    <div class="filtro-cores">
      <button class="item-cores">Cinza</button>
      <button class="item-cores">Branco</button>
      <button class="item-cores">Verde</button>
      <button class="item-cores">Vermelho</button>
      <button class="item-cores">Preto</button>
      <button class="item-cores">Azul</button>
    </div>
    <hr>
    <button class="botao-filtro">Aplicar Filtros</button>
  </aside>
</template>