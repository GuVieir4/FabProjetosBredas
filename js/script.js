document.addEventListener("DOMContentLoaded", () => {

    // Index > Fechar a promoção de cadastro
    const btnClose = document.getElementById('icon-close');
    const promoRegister = document.getElementById('promo-register');

    if (btnClose && promoRegister) {
        btnClose.addEventListener("click", () => {
            promoRegister.classList.add('active');
        });
    }

    // Index > Dropdown
    const userIcon = document.getElementById("user-icon");
    const dropdownMenu = document.getElementById("dropdown-menu");

    if (userIcon && dropdownMenu) {
        userIcon.addEventListener("click", (event) => {
            event.preventDefault();
            dropdownMenu.style.display = dropdownMenu.style.display === "block" ? "none" : "block";
        });

        document.addEventListener("click", (event) => {
            if (!userIcon.contains(event.target) && !dropdownMenu.contains(event.target)) {
                dropdownMenu.style.display = "none";
            }
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
