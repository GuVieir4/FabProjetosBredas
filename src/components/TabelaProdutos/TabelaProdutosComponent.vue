<template>
    <section class="table-container">
        <table class="custom-table">
            <thead class="custom-table-head">
                <tr>
                    <th>Produto</th>
                    <th>Valor</th>
                    <th>Categoria</th>
                    <th>Ações</th>
                </tr>
            </thead>
            <tbody class="custom-table-body">
                <tr>
                    <td>Attack Shark X6</td>
                    <td>120</td>
                    <td>Mouses</td>
                    <td>
                        <button>Editar</button>
                        <button>Deletar</button>
                    </td>
                </tr>
                <tr>
                    <td>Shark K86</td>
                    <td>220</td>
                    <td>Teclados</td>
                    <td>
                        <button>Editar</button>
                        <button>Deletar</button>
                    </td>
                </tr>
                <tr>
                    <td>Machenike K500</td>
                    <td>130</td>
                    <td>Teclados</td>
                    <td>
                        <button>Editar</button>
                        <button>Deletar</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </section>
</template>

<style scoped>
.table-container {
    overflow-x: auto;
    margin-top: 50px;
    border-radius: 8px;
    overflow: hidden;
}

.custom-table {
    width: 100%;
    border-collapse: collapse;
    text-align: left;
    min-width: 600px;
}

.custom-table-head {
    background-color: #1e1e2c;
    color: #ffffff;
}

.custom-table-body {
    background-color: #303041;
    color: #d1d1d1;
}

.custom-table th,
.custom-table td {
    border: 1px solid #1e1e2c;
    padding: 12px 16px;
}

.custom-table tbody tr:hover {
    background-color: var(--primary-blue);
    opacity: 0.8;
}

.custom-table tbody tr:hover button {
    border: 2px solid var(--gray-200);
}

table button {
    background: transparent;
    border: none;
    padding: 10px 10px;
    border: 2px solid var(--primary-blue);
    border-radius: 8px;
    cursor: pointer;
}
</style>