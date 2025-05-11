function menuShow() {
    let menuMobile = document.querySelector('.mobile-menu');
    if (menuMobile.classList.contains('open')) {
        menuMobile.classList.remove('open');
    } else {
        menuMobile.classList.add('open')
    }
}

document.addEventListener("DOMContentLoaded", () => {

    // Index > Fechar a promoção de cadastro
    const btnClose = document.getElementById('icon-close');
    const promoRegister = document.getElementById('promo-register');

    if (btnClose && promoRegister) {
        btnClose.addEventListener("click", () => {
            promoRegister.classList.add('active');
        });
    }

    // Register > Confirmar senha
    const form = document.getElementById("formulario");
    const senha = document.getElementById("password");
    const confirmarSenha = document.getElementById("confirm-password");
    const alertaDeErro = document.getElementById('alertErro');

    if (form && senha && confirmarSenha && alertaDeErro) {
        form.addEventListener("submit", function (event) {
            if (senha.value !== confirmarSenha.value) {
                event.preventDefault();
                alertaDeErro.classList.add('active');
            }
        });
    }
});


// Newsletter > Enviando Email com a API EmailJS
(function () {
    emailjs.init("kZkoynUIPV0OPGyY9");
    console.log("EmailJS funcionando");
})();

const newsletterForm = document.getElementById("newsletter-form");

if (newsletterForm) {
    newsletterForm.addEventListener("submit", function (event) {
        event.preventDefault();

        console.log("Formulário enviado");

        emailjs.sendForm("service_plko91s", "template_kg43dko", this)
            .then((response) => {
                console.log("Resposta do envio:", response);
                alert("Agradecemos por se inscrever em nossa Newsletter! Uma confirmação foi enviada para seu e-mail.");
                this.reset();
            }, (error) => {
                console.error("Erro ao enviar e-mail:", error);
                alert("Erro ao enviar. Tente novamente.");
            });
    });
}
