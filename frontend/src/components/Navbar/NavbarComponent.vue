<template>
    <header id="Header">
        <nav class="nav-bar">
            <div class="logo">
                <router-link to="/"><img src="@/assets/LogoKirashop.png" alt="Logo Kirashop" class="logo-kirashop"></router-link>
            </div>
            <div class="nav-list">
                <ul>
                    <li class="nav-item"><router-link to="/produtos">Produtos</router-link></li>
                    <li class="nav-item"><router-link to="/categoryPage">Categorias</router-link></li>
                    <li class="nav-item"><router-link to="/marcaPage">Marcas</router-link></li>
                </ul>
            </div>

            <div class="group">
                <svg class="icon" aria-hidden="true" viewBox="0 0 24 24">
                    <g>
                        <path
                            d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z">
                        </path>
                    </g>
                </svg>
                <input placeholder="Pesquisar..." type="search" class="input" name="search-bar">
            </div>

            <div class="icones">
                <router-link to="/kart"><i class="fa-solid fa-cart-shopping"></i></router-link>
                <router-link to="/adminDashboard"><i class="fa-regular fa-circle-user"></i></router-link>
            </div>

            <div class="mobile-menu-icon" @click="menuShow">
                <div class="line1"></div>
                <div class="line2"></div>
                <div class="line3"></div>
            </div>
        </nav>

        <div class="mobile-menu">
            <ul>
                <li class="nav-item"><router-link to="/produtos">Produtos</router-link></li>
                <li class="nav-item"><router-link to="/categoryPage">Categorias</router-link></li>
                <li class="nav-item"><router-link to="/marcaPage">Marcas</router-link></li>
            </ul>

            <div class="group">
                <svg class="icon" aria-hidden="true" viewBox="0 0 24 24">
                    <g>
                        <path
                            d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z">
                        </path>
                    </g>
                </svg>
                <input placeholder="Pesquisar..." type="search" class="input" name="search-bar">
            </div>

            <div class="icones">
                <a href="./pages/kart.html"><i class="fa-solid fa-cart-shopping"></i></a>
                <a href="#"><i class="fa-regular fa-circle-user"></i></a>
            </div>
        </div>
    </header>
</template>

<script setup>
import './NavbarComponent.css'

function menuShow() {
    let menuMobile = document.querySelector('.mobile-menu');
    if (menuMobile.classList.contains('open')) {
        menuMobile.classList.remove('open');
    } else {
        menuMobile.classList.add('open')
    }
}
</script>