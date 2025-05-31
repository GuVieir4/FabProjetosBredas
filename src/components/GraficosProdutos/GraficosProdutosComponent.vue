<template>
    <section class="charts">
        <div class="chart-container">
            <canvas id="productsChart"></canvas>
        </div>
        <div class="chart-container">
            <canvas id="clientsChart"></canvas>
        </div>
    </section>
</template>

<style scoped>
.charts {
    display: flex;
    flex-wrap: wrap;
    gap: 40px;
}

.chart-container {
    background-color: #1e1e2c;
    padding: 20px;
    border-radius: 12px;
    flex: 1;
    min-width: 300px;
}
</style>

<script setup>
import { onMounted } from 'vue'

const productsData = {
    labels: ['Produto A', 'Produto B', 'Produto C', 'Produto D'],
    datasets: [{
        label: 'Produtos Cadastrados',
        data: [12, 19, 3, 5],
        backgroundColor: '#2e54a6'
    }]
};

const clientsData = {
    labels: ['Cliente 1', 'Cliente 2', 'Cliente 3', 'Cliente 4'],
    datasets: [{
        label: 'Clientes Cadastrados',
        data: [5, 9, 8, 3],
        backgroundColor: '#6c63ff'
    }]
};

onMounted(() => {
    const productsCtx = document.getElementById('productsChart');
    const clientsCtx = document.getElementById('clientsChart');

    new Chart(productsCtx, {
        type: 'bar',
        data: productsData,
        options: {
            responsive: true,
            scales: {
                y: { beginAtZero: true }
            }
        }
    });

    new Chart(clientsCtx, {
        type: 'bar',
        data: clientsData,
        options: {
            responsive: true,
            scales: {
                y: { beginAtZero: true }
            }
        }
    });
});
</script>