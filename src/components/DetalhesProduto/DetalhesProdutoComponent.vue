<script setup>
import './DetalhesProdutoComponent.css'
</script>

<template>
    <section class="informacoes-produto">
        <div class="imagens-produto">
            <div class="imagens-produto-vertical">
                <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark" class="imagem-vertical">
                <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark" class="imagem-vertical">
                <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark" class="imagem-vertical">
            </div>
            <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark" class="imagem-produto-principal">
            <div class="detalhes-produto">
                <h1 class="nome-produto">Mouse Gamer Attack Shark X6</h1>
                <h1 class="valor-produto">R$240</h1>
                    <p class="descricao-produto">Mouse muito preciso, confortável e com sensor HERO 25K. Ideal pra quem
                        joga FPS. RGB personalizável. Excelente custo-benefício pra um mouse gamer de alto desempenho!
                    </p>
                    <button class="quantidade-produto">- 1 +</button>
                    <button class="botao-carrinho">Adicionar ao carrinho</button>
            </div>
        </div>
    </section>
</template>