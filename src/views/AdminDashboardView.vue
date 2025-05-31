<script setup>
import Sidebar from '../components/Sidebar/SidebarComponent.vue'
import CardsCrud from '../components/CardsCrud/CardsCrudComponent.vue'
import TabelaProdutos from '../components/TabelaProdutos/TabelaProdutosComponent.vue'
import GraficosProdutos from '../components/GraficosProdutos/GraficosProdutosComponent.vue'
</script>

<template>
    <body class="bodyAdminDashboard">
        <Sidebar />
        <section class="content">
            <CardsCrud/>
            <GraficosProdutos/>
            <TabelaProdutos/>
        </section>
    </body>
</template>

<style>
    .bodyAdminDashboard {
        height: 100vh;
        display: flex;
    }

    .content {
        flex: 1;
        padding: 40px;
    }
</style>