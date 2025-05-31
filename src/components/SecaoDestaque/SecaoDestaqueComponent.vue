<template>
    <nav class="quick-nav" ontouchstart="pauseScroll()" ontouchend="resumeScroll()">
        <ul class="scroll-track" id="scrollTrack">
            <li><a href="#"><i class="fas fa-microchip"></i> Processadores</a></li>
            <li><a href="#"><i class="fas fa-memory"></i> Memória RAM</a></li>
            <li><a href="#"><i class="fas fa-video"></i> Placas de Vídeo</a></li>
            <li><a href="#"><i class="fas fa-hdd"></i> HDs & SSDs</a></li>
            <li><a href="#"><i class="fas fa-broadcast-tower"></i> Placas-mãe</a></li>
            <li><a href="#"><i class="fas fa-fan"></i> Coolers</a></li>
            <li><a href="#"><i class="fas fa-bolt"></i> Fontes</a></li>
            <li><a href="#"><i class="fas fa-desktop"></i> Gabinetes</a></li>
            <li><a href="#"><i class="fas fa-keyboard"></i> Teclados</a></li>
            <li><a href="#"><i class="fas fa-mouse"></i> Mouses</a></li>
            <!-- Duplicação para efeito infinito -->
            <li><a href="#"><i class="fas fa-microchip"></i> Processadores</a></li>
            <li><a href="#"><i class="fas fa-memory"></i> Memória RAM</a></li>
            <li><a href="#"><i class="fas fa-video"></i> Placas de Vídeo</a></li>
            <li><a href="#"><i class="fas fa-hdd"></i> HDs & SSDs</a></li>
            <li><a href="#"><i class="fas fa-broadcast-tower"></i> Placas-mãe</a></li>
            <li><a href="#"><i class="fas fa-fan"></i> Coolers</a></li>
            <li><a href="#"><i class="fas fa-bolt"></i> Fontes</a></li>
            <li><a href="#"><i class="fas fa-desktop"></i> Gabinetes</a></li>
            <li><a href="#"><i class="fas fa-keyboard"></i> Teclados</a></li>
            <li><a href="#"><i class="fas fa-mouse"></i> Mouses</a></li>
        </ul>
    </nav>

    <div class="destaque-loja">
        <div class="chamada">
            <h1 id="texto-animacao">SEU SETUP, SEU ESTILO</h1>
            <p>Explore diversas opções de marcas e encontre tudo aquilo que sempre sonhou, com o melhor preço!</p>
            <div class="botoes">
                <a href="#" class="btn-primary">Compre agora</a>
                <a href="#" class="btn-secondary">Conheça nossa marca</a>
            </div>
        </div>
    </div>

    <div class="banner-marcas">
        <img src="https://cdn.simpleicons.org/amd/FFF" alt="AMD logo" width="40" />
        <img src="https://cdn.simpleicons.org/razer/FFF" alt="Razer logo" width="40" />
        <img src="https://cdn.simpleicons.org/logitech/FFF" alt="Logitech logo" width="40" />
        <img src="https://cdn.simpleicons.org/alienware/FFF" alt="Alienware logo" width="40" />
        <img src="https://cdn.simpleicons.org/intel/FFF" alt="Intel logo" width="400" />
    </div>
</template>

<script setup>
import './SecaoDestaqueComponent.css'
</script>