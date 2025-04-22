// Pessoal, manter os scripts separados por comentários/html por enquanto. Se futuramente o tamanho do JavaScript aumentar, nós criamos outros arquivos.

// Index > Fechar a promoção de cadastro

const btnClose = document.getElementById('icon-close');
const promoRegister = document.getElementById('promo-register');
const btnLogin = document.getElementById('login');


btnClose.addEventListener("click", () => {
    promoRegister.classList.add('active')
})

// Index > Dropdown

const userIcon = document.getElementById("user-icon");
const dropdownMenu = document.getElementById("dropdown-menu");

userIcon.addEventListener("click", (event) => {
    event.preventDefault();
    dropdownMenu.style.display = dropdownMenu.style.display === "block" ? "none" : "block";
});

document.addEventListener("click", (event) => {
    if (!userIcon.contains(event.target) && !dropdownMenu.contains(event.target)) {
        dropdownMenu.style.display = "none";
    }
});