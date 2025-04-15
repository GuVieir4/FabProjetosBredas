const btnClose = document.getElementById('icon-close');
const promoRegister = document.getElementById('promo-register')

btnClose.addEventListener("click", () => {
    promoRegister.classList.add('active')
})