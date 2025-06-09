<script setup>
import { RouterView, useRoute } from 'vue-router'
import NavbarComponent from './components/Navbar/NavbarComponent.vue'
import FooterComponent from './components/Footer/FooterComponent.vue'
import PromoRegister from './components/PromoRegister/PromoRegister.vue'
const route = useRoute()

// Define quais views não devem exibir a Navbar
const hideNavbarOn = ['adminDashboard']
const hideFooterOn = ['adminDashboard']
const hidePromoRegisterOn = ['adminDashboard']
</script>

<template>
  <PromoRegister v-if="!hidePromoRegisterOn.includes(route.name)" />
  <NavbarComponent v-if="!hideNavbarOn.includes(route.name)" />
  <RouterView />
  <FooterComponent v-if="!hideFooterOn.includes(route.name)" />
</template>
