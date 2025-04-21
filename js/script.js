const btnClose = document.getElementById('icon-close');
const promoRegister = document.getElementById('promo-register');
const btnLogin = document.getElementById('login');


btnClose.addEventListener("click", () => {
    promoRegister.classList.add('active')
})