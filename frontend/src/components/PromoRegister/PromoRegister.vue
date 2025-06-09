<template>
  <div class="promo-register" ref="promoRegister">
    <div class="promo-content">
      <div class="promo-badge">OFERTA EXCLUSIVA</div>
      <p>
        <a href="/pages/register.html">Cadastre-se</a> agora e ganhe 20% de desconto no seu primeiro pedido!
      </p>
      <div class="countdown-container">
        <p>Oferta expira em:</p>
        <div class="countdown">
          <div class="time-block">
            <span id="hours">00</span>
            <span class="time-label">Horas</span>
          </div>
          <div class="time-block">
            <span id="minutes">59</span>
            <span class="time-label">Min</span>
          </div>
          <div class="time-block">
            <span id="seconds">59</span>
            <span class="time-label">Seg</span>
          </div>
        </div>
      </div>
      <i class="fa-solid fa-xmark" id="icon-close" @click="fecharPromo"></i>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import './PromoRegister.css'

const promoRegister = ref(null)

function fecharPromo() {
  if (promoRegister.value) {
    promoRegister.value.classList.add('promo-register-fechado')
  }
}
</script>
