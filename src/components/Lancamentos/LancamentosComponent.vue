<template>
    <section id="Lancamentos">
        <h1 class="lancamentos">LANÇAMENTOS</h1>

        <!-- Os 5 Cards estão de exemplo, remover eles quando estivermos com a API pronta -->
        <div class="container-lancamentos">
            <div class="cards-produtos">
                <router-link to="/productDetails">
                    <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark" class="imagem-produto">
                </router-link>
                <div class="info-produto">
                    <h4 class="nome-produto">Attack Shark</h4>
                    <div class="avaliacao-produto">
                        <!-- Esquema de Avaliação Obtido no Uiverse -->
                        <input value="5" name="rating1" id="star5_1" type="radio">
                        <label for="star5_1"></label>
                        <input value="4" name="rating1" id="star4_1" type="radio">
                        <label for="star4_1"></label>
                        <input value="3" name="rating1" id="star3_1" type="radio">
                        <label for="star3_1"></label>
                        <input value="2" name="rating1" id="star2_1" type="radio">
                        <label for="star2_1"></label>
                        <input value="1" name="rating1" id="star1_1" type="radio">
                        <label for="star1_1"></label>
                        <!-- Fim da Seção de Avaliação -->
                    </div>
                    <h3 class="valor-produto"><span>R$</span>120</h3>
                </div>
            </div>

            <div class="cards-produtos">
                <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark" class="imagem-produto">
                <div class="info-produto">
                    <h4 class="nome-produto">Attack Shark</h4>
                    <div class="avaliacao-produto">
                        <!-- Esquema de Avaliação Obtido no Uiverse -->
                        <input value="5" name="rating2" id="star5_2" type="radio">
                        <label for="star5_2"></label>
                        <input value="4" name="rating2" id="star4_2" type="radio">
                        <label for="star4_2"></label>
                        <input value="3" name="rating2" id="star3_2" type="radio">
                        <label for="star3_2"></label>
                        <input value="2" name="rating2" id="star2_2" type="radio">
                        <label for="star2_2"></label>
                        <input value="1" name="rating2" id="star1_2" type="radio">
                        <label for="star1_2"></label>
                        <!-- Fim da Seção de Avaliação -->
                    </div>
                    <h3 class="valor-produto"><span>R$</span>120</h3>
                </div>
            </div>

            <div class="cards-produtos">
                <div class="imagem-produto">
                    <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark">
                </div>
                <div class="info-produto">
                    <h4 class="nome-produto">Attack Shark</h4>
                    <div class="avaliacao-produto">
                        <!-- Esquema de Avaliação Obtido no Uiverse -->
                        <input value="5" name="rating3" id="star5_3" type="radio">
                        <label for="star5_3"></label>
                        <input value="4" name="rating3" id="star4_3" type="radio">
                        <label for="star4_3"></label>
                        <input value="3" name="rating3" id="star3_3" type="radio">
                        <label for="star3_3"></label>
                        <input value="2" name="rating3" id="star2_3" type="radio">
                        <label for="star2_3"></label>
                        <input value="1" name="rating3" id="star1_3" type="radio">
                        <label for="star1_3"></label>
                        <!-- Fim da Seção de Avaliação -->
                    </div>
                    <h3 class="valor-produto"><span>R$</span>120</h3>
                </div>
            </div>

            <div class="cards-produtos">
                <div class="imagem-produto">
                    <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark">
                </div>
                <div class="info-produto">
                    <h4 class="nome-produto">Attack Shark</h4>
                    <div class="avaliacao-produto">
                        <!-- Esquema de Avaliação Obtido no Uiverse -->
                        <input value="5" name="rating4" id="star5_4" type="radio">
                        <label for="star5_4"></label>
                        <input value="4" name="rating4" id="star4_4" type="radio">
                        <label for="star4_4"></label>
                        <input value="3" name="rating4" id="star3_4" type="radio">
                        <label for="star3_4"></label>
                        <input value="2" name="rating4" id="star2_4" type="radio">
                        <label for="star2_4"></label>
                        <input value="1" name="rating4" id="star1_4" type="radio">
                        <label for="star1_4"></label>
                        <!-- Fim da Seção de Avaliação -->
                    </div>
                    <h3 class="valor-produto"><span>R$</span>120</h3>
                </div>
            </div>

            <div class="cards-produtos">
                <div class="imagem-produto">
                    <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark">
                </div>
                <div class="info-produto">
                    <h4 class="nome-produto">Attack Shark</h4>
                    <div class="avaliacao-produto">
                        <!-- Esquema de Avaliação Obtido no Uiverse -->
                        <input value="5" name="rating5" id="star5_5" type="radio">
                        <label for="star5_5"></label>
                        <input value="4" name="rating5" id="star4_5" type="radio">
                        <label for="star4_5"></label>
                        <input value="3" name="rating5" id="star3_5" type="radio">
                        <label for="star3_5"></label>
                        <input value="2" name="rating5" id="star2_5" type="radio">
                        <label for="star2_5"></label>
                        <input value="1" name="rating5" id="star1_5" type="radio">
                        <label for="star1_5"></label>
                        <!-- Fim da Seção de Avaliação -->
                    </div>
                    <h3 class="valor-produto"><span>R$</span>120</h3>
                </div>
            </div>
        </div>
        <button class="carregar-mais">Carregar mais</button>
    </section>
</template>

<script setup>
import './LancamentosComponent.css'
</script>