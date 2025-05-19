function toggleMenu() {
    document.getElementById('sidebar').classList.toggle('collapsed');
}

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

new Chart(document.getElementById('productsChart'), {
    type: 'bar',
    data: productsData,
    options: {
        responsive: true,
        scales: {
            y: { beginAtZero: true }
        }
    }
});

new Chart(document.getElementById('clientsChart'), {
    type: 'bar',
    data: clientsData,
    options: {
        responsive: true,
        scales: {
            y: { beginAtZero: true }
        }
    }
});

const openButtons = document.querySelectorAll('.open-modal');
const closeButtons = document.querySelectorAll('.close-modal');

openButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modalID = button.getAttribute('data-modal');
        const modal = document.getElementById(modalID);

        modal.show();
    });
});

closeButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modalID = button.getAttribute('data-modal');
        const modal = document.getElementById(modalID);

        modal.close();
    });
});
