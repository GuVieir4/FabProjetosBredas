<script setup>
import './FooterComponent.css'
</script>

<template>
    <div>
        <div class="container-newsletter">
            <h1>FIQUE ATUALIZADO SOBRE NOSSAS ÚLTIMAS OFERTAS</h1>
            <form id="newsletter-form">
                <div class="input-group">
                    <input type="email" id="email" name="email" placeholder="Digite seu e-mail" required />
                </div>
                <button class="btnSubmit" type="submit" id="btnSubmit">Inscreva-se na Newsletter</button>
            </form>
        </div>

        <footer class="footer">
            <div class="footer-container">
                <div class="sobre-kirashop">
                    <h1>KIRASHOP</h1>
                    <p>Temos jogos que combinam com o seu estilo e que você vai ter orgulho de jogar. Dos clássicos retrô aos lançamentos da nova geração.</p>
                </div>
                <div class="infos-kirashop">
                    <div class="institucional">
                        <p class="titulo-lista">INSTITUCIONAL</p>
                        <ul>
                            <li><i class="fa-solid fa-book-open-reader"></i> Quem Somos</li>
                            <li><i class="fa-solid fa-location-dot"></i> Localização</li>
                            <li><i class="fa-solid fa-newspaper"></i> Newsletter</li>
                            <li><i class="fa-solid fa-blog"></i> Blog</li>
                        </ul>
                    </div>
                    <div class="ajuda">
                        <p class="titulo-lista">AJUDA</p>
                        <ul>
                            <li><i class="fa-solid fa-headset"></i> Suporte ao Cliente</li>
                            <li><i class="fa-solid fa-message"></i> Fale Conosco</li>
                            <li><i class="fa-solid fa-file-signature"></i> Termos de Aceite</li>
                            <li><i class="fa-solid fa-building-shield"></i> Política de Privacidade</li>
                        </ul>
                    </div>
                    <div class="faq">
                        <p class="titulo-lista">FAQ</p>
                        <ul>
                            <li><i class="fa-solid fa-truck-fast"></i> Entrega</li>
                            <li><i class="fa-solid fa-award"></i> Garantia</li>
                            <li><i class="fa-solid fa-circle-info"></i> Como Comprar</li>
                            <li><i class="fa-solid fa-money-bill"></i> Pagamentos</li>
                        </ul>
                    </div>
                </div>      
            </div>
            <div class="social-icons">
                <a href="#"><i class="fa-brands fa-x-twitter"></i></a>
                <a href="#"><i class="fa-brands fa-facebook-f"></i></a>
                <a href="#"><i class="fa-brands fa-instagram"></i></a>
                <a href="#"><i class="fa-brands fa-github"></i></a>
            </div>
            <hr class="separacao-allrights">
            <p class="allrights">KIRASHOP © 2024-2025. <br class="quebra-allrights"> All Rights Reserved</p>
        </footer>
</div>
</template>
