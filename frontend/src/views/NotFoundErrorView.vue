<template>
    <body>
    <div class="card-error">
        <h1>404<span>🧐</span></h1>
        <h3>Página não encontrada</h3>
        <h6>A página que você está procurando não existe</h6>
        <a href="index.html">Voltar para a página principal</a>
    </div>
    <img src="./images/fullogo.png" alt="">
</body>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Martian+Mono:wght@100..800&display=swap');

:root {
    --primary-blue: #2E54A6;
    --light-blue: #6c63ff;
    --background-dark: #10101E;
    --text-white: #ffffff;
    --gray-200: #c3c3d1;
    --gray-400: #1e1e2c;
    --gray-500: #181826;
    --sucess-color: #1d8338;
    --error-color: #881337;
}

body {
    background-color: var(--background-dark);
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100vh;
    padding: 20px;
}

img {
    width: 200px;
    margin-top: 40px;
}

* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-family: "Martian Mono", monospace;
}

.card-error {
    border: 1px solid var(--primary-blue);
    border-radius: 12px;
    width: 100%;
    max-width: 500px;
    height: 400px;
    padding: 20px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    text-align: center;
    align-items: center;
    background-color: var(--background-dark);
}

.card-error h1 {
    color: var(--text-white);
    font-size: 90px;
    width: 100%;
    word-wrap: break-word;
}

.card-error h3 {
    color: var(--text-white);
    width: 100%;
    margin-top: 20px;
    text-transform: uppercase;
    font-size: 24px;
}

.card-error h6 {
    color: var(--text-white);
    margin-top: 30px;
    font-size: 16px;
}

.card-error a {
    color: var(--text-white);
    margin-top: 24px;
    background-color: transparent;
    border-radius: 8px;
    cursor: pointer;
    border: 1px solid var(--primary-blue);
    text-decoration: none;
    padding: 12px 20px;
    font-size: 14px;
    transition: all 0.3s ease;
}

.card-error a:hover {
    background-color: #F0F0F0;
    color: #000;
    font-style: italic;
    opacity: 0.95;
}

@media (max-width: 768px) {
    .card-error a {
        padding: 12px 16px;
        font-size: 12px;
    }

    .card-error h3 {
        font-size: 18px;
    }

    .card-error h6 {
        font-size: 16px;
    }
    
    img {
        margin-top: 30px;
    }
}
</style>