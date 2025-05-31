<template>
    <h1 class="titulo-carrinho">Seu Carrinho</h1>
    <section class="secao-carrinho">
        <div class="container-carrinho">
            <div class="card-carrinho">
                <div class="imagem produto">
                    <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark">
                </div>
                <div class="informacoes-produto">
                    <h3 class="nome-produto">Mouse Attack Shark</h3>
                    <h3 class="valor-produto">R$145</h3>
                </div>
            </div>
            <div class="card-carrinho">
                <div class="imagem produto">
                    <img src="@/assets/MouseAttackShark.svg" alt="Mouse Attack Shark">
                </div>
                <div class="informacoes-produto">
                    <h3 class="nome-produto">Mouse Attack Shark</h3>
                    <h3 class="valor-produto">R$145</h3>
                </div>
            </div>
        </div>

        <div class="container-resumo">
            <div class="informacoes-pedido">
                <h3>Resumo do Pedido</h3>
                <div class="informacoes-topico">
                    <p>Subtotal</p>
                    <h3>R$565</h3>
                </div>
                <div class="informacoes-topico">
                    <p>Desconto</p>
                    <h3>R$0</h3>
                </div>
                <div class="informacoes-topico">
                    <p>Frete</p>
                    <h3 style="color: #2e54a6;">Grátis</h3>
                </div>
                <div class="informacoes-topico">
                    <p>Total</p>
                    <h3>R$565</h3>
                </div>
            </div>
            <div class="componentes-resumo">
                <form action="" class="formulario-carrinho">
                    <input class="input-cupom" type="text" placeholder="Inserir cupom...">
                    <button>Aplicar</button>
                </form>
                <a>Enviar Orçamento</a>
            </div>
        </div>
    </section>
</template>

<style scoped>
* {
    font-family: "Martian Mono", monospace;
    color: var(--text-white);
}

.titulo-carrinho {
    margin-top: 50px;
    font-size: 48px;
    font-weight: 700;
    color: var(--gray-200);
    text-align: center;
}

.container-carrinho {
    width: 715px;
    min-height: 160px;
    height: auto;
    background-color: transparent;
    border: 1px solid var(--gray-200);
    border-radius: 20px;
}

.container-carrinho img {
    background-color: var(--text-white);
    width: 124px;
    height: 124px;
    border-radius: 12px;
    object-fit: contain;
}

.secao-carrinho {
    margin: 50px auto;
    display: flex;
    gap: 48px;
    justify-content: center;
}

.card-carrinho {
    display: flex;
    padding: 20px 24px;
}

.informacoes-produto {
    display: flex;
    flex-direction: column;
    margin-left: 20px;
    font-size: 20px;
    justify-content: center;
    gap: 12px;
}

.container-resumo {
    width: 505px;
    height: auto;
    padding: 20px 24px;
    border: 1px solid var(--gray-200);
    border-radius: 20px;
}

.informacoes-topico {
    display: flex;
    justify-content: space-between;
}

.informacoes-topico h3 {
    margin-top: 25px;
    font-size: 20px;
    font-weight: bold;
}

.informacoes-pedido h3 {
    font-size: 24px;
}

.informacoes-pedido p:last-child {
    color: var(--text-white);
    margin: 0;
    margin-top: 42px;
}

.informacoes-pedido p {
    color: var(--gray-200);
    font-size: 20px;
    margin-top: 25px;
}


.componentes-resumo a {
    background-color: var(--primary-blue);
    display: flex;
    justify-content: center;
    width: 100%;
    text-decoration: none;
    margin: 0 auto;
    margin-top: 12px;
    height: 48px;
    align-items: center;
    border-radius: 8px;
    display: flex;
    cursor: pointer;
    gap: 12px;
}

.componentes-resumo a:hover {
    opacity: 0.8;
    color: var(--text-white);
    font-weight: 400;
}

.formulario-carrinho {
    display: flex;
    margin-top: 24px;
    gap: 10px;
}

.componentes-resumo input {
    width: 326px;
    height: 48px;
    border: none;
    border-radius: 8px;
    background-color: var(--text-white);
    font-size: 15px;
    transition: ease 0.25s;
}

.componentes-resumo input:focus {
    background-color: var(--gray-200);
    outline-style: none;
    box-shadow: 5px 5px var(--primary-blue);
    text-align: center;
    color: var(--background-dark);
    text-transform: uppercase;
}

.componentes-resumo input::placeholder {
    text-align: center;
    color: var(--background-dark);
}

.componentes-resumo button {
    background-color: var(--primary-blue);
    width: 119px;
    height: 48px;
    border: none;
    border-radius: 8px;
}

@media (max-width: 768px) {
    .titulo-carrinho {
        font-size: 36px;
        margin-left: 0;
        text-align: center;
    }

    .secao-carrinho {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .container-carrinho {
        width: 358px;
        margin-left: 0;
    }

    .informacoes-produto {
        font-size: 16px;
        font-weight: 400;
    }

    .trilha-navegacao {
        margin-left: 0;
        display: flex;
        text-align: center;
        align-items: center;
    }

    .trilha-navegacao {
        display: flex;
        justify-content: center;
    }

    .secao-carrinho {
        display: flex;
        flex-direction: column;
    }

    .container-resumo {
        width: 358px;
    }

    .componentes-resumo a {
        font-size: 12px;
    }
}
</style>