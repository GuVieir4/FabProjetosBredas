document.addEventListener("DOMContentLoaded", () => {

    // Pessoal, manter os scripts separados por comentários/html por enquanto. Se futuramente o tamanho do JavaScript aumentar, nós criamos outros arquivos.
    
    // Index > Fechar a promoção de cadastro
    
    const btnClose = document.getElementById('icon-close');
    const promoRegister = document.getElementById('promo-register');
    const btnLogin = document.getElementById('login');
    
        if (btnClose && promoRegister) {
            btnClose.addEventListener("click", () => {
                promoRegister.classList.add('active')
            })
        }

    // Index > Menu Hambúrguer

    // document.getElementById("hamburger-menu").addEventListener("click", function() {
    //     const mobileMenu = document.getElementById("mobile-menu");
    //     mobileMenu.classList.toggle("active");
    // });
    
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
    
    // const form = document.getElementById("formulario");
    // const senha = document.getElementById("password");
    // const confirmarSenha = document.getElementById("confirm-password");
    // const mensagemDeErro = document.getElementById("mensagemDeErro");
    // const alertaDeErro = document.getElementById('alertErro');
    
            // form.addEventListener("submit", function (event) {
            //     if (senha.value !== confirmarSenha.value) {
            //         event.preventDefault();
            //         alertaDeErro.classList.add('active')
            //     }
            // });
    
    });

// Newsletter > Enviando Email com a API EmailJS
(function() {
    emailjs.init("kZkoynUIPV0OPGyY9");
    console.log("EmailJS inicializado");
  })();

  document.getElementById("newsletter-form").addEventListener("submit", function(event) {
    event.preventDefault();

    console.log("Formulário enviado");

    emailjs.sendForm("service_plko91s", "template_kg43dko", this)
      .then((response) => {
        console.log("Resposta do envio:", response);
        alert("Inscrição enviada com sucesso! Verifique seu e-mail.");
        this.reset();
      }, (error) => {
        console.error("Erro ao enviar e-mail:", error);
        alert("Erro ao enviar. Tente novamente.");
      });
  });
  