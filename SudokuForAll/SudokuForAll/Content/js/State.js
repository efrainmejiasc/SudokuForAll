


function AbrirModalIndicada(openModal) {
    var probarSitio = document.getElementById('probarSitio');
    var intro = document.getElementById('intro');
    var comprarRegistrarse = document.getElementById('comprarRegistrarse');

    if (openModal == 'prueba') {
        probarSitio.style.display = 'block';
    }
    else if (openModal == 'Open') {
        intro.style.display = 'block';
    }
    else if (openModal == 'Index') { //Redirecciona a Index -> Home
        intro.style.display = 'block';
        setTimeout(RedirectToIndex, 4000);
    }
    else if (openModal == 'Login') {//Redirecciona a Login -> Home
        intro.style.display = 'block';
        setTimeout(RedirectToLogin, 4000);
    }
    else if (openModal == 'Contact') {//Redirecciona a Contact -> Home
        intro.style.display = 'block';
        setTimeout(RedirectToContact, 4000);
    }
    else if (openModal == 'Register') {//Redirecciona a Register -> Home
        intro.style.display = 'block';
        setTimeout(RedirectToRegister, 4000);
    }
    else if (openModal == 'EditPasswordNotify') {//Redirecciona a EditPasswordNotify -> Home
        intro.style.display = 'block';
        setTimeout(RedirectToEditPasswordNotify, 4000);
    }
    else if (openModal == 'comprarRegistrarse') {
        comprarRegistrarse.style.display = 'block';
    }
    else if (openModal == 'EditPassword') {//Redirecciona a EditPassword
        intro.style.display = 'block';
        setTimeout(RedirectToEditPassword, 4000);
    }
}

function CerrarIntro() {
    var intro = document.getElementById('intro');
    intro.style.display = 'none';
}

function RedirectToIndex() {
    var intro = document.getElementById('intro');
    intro.style.display = 'none';
    DireccionSite('Home', 'Index');
}

function RedirectToLogin() {
    var intro = document.getElementById('intro');
    intro.style.display = 'none';
    DireccionSite('Home', 'Login');
}

function RedirectToContact() {
    var intro = document.getElementById('intro');
    intro.style.display = 'none';
    DireccionSite('Home', 'Contact');
}

function RedirectToRegister() {
    var intro = document.getElementById('intro');
    intro.style.display = 'none';
    DireccionSite('Home', 'Register');
}

function ComprarRegistrarse() {
    DireccionSite('Home', 'Register');
}

function RedirectToEditPasswordNotify() {
    var intro = document.getElementById('intro');
    intro.style.display = 'none';
    DireccionSite('Home', 'EditPasswordNotify');
}

function RedirectToEditPassword() {
    var intro = document.getElementById('intro');
    intro.style.display = 'none';
    DireccionSite('Home', 'EditPassword');
}

function OcultarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).hide();
}

function CerrarModalProbarSitio() {
    var probarSitio = document.getElementById('probarSitio');
    probarSitio.style.display = 'none';
    DireccionSite('Home', 'Index');
}

function CerrarModalComprarRegistrarse() {
    var comprarRegistrarse = document.getElementById('comprarRegistrarse');
    comprarRegistrarse.style.display = 'none';
    DireccionSite('Home', 'Index');
}

function CerrarModalActivarCuenta() {
    var modal = document.getElementById('activarCuenta');
    modal.style.display = 'none';
    DireccionSite('Home', 'Index');
}

function DireccionSite(nombreControlador, nombreAccion) {
    $.ajax({
        type: "POST",
        url: "/Home/DireccionSite",
        data: { nombreControlador: nombreControlador, nombreAccion: nombreAccion },
        datatype: "json",
        success: function (data) {
            window.location.href = data.Descripcion;
        }
    });
}

function NotificacionPrueba() {
    var email = $('#Email').val();
    $.ajax({
        type: "POST",
        url: "/Home/NotificacionPrueba",
        data: { email: email },
        datatype: "json",
        success: function (data) {
            MostrarAlert(data.Descripcion,data.CulturaInfo,data.Email);
        },
        complete: function () {
            probarSitio.style.display = 'none';
            DireccionSite('Home', 'Index');
        }
    });
}

function MostrarAlert(descripcion, cultura, email) {
    let respuesta = '';
    console.log(cultura);
    if (descripcion == 'Exito') {
        if (cultura == 'es-VE') {
            respuesta = 'Hemos enviado una notificacion a la direccion ';
        }
        else if (cultura == 'en-US') {
            respuesta = 'We have sent a notification to the address ';
        }
        else if (cultura == 'pt-PT') {
            respuesta = 'Enviamos uma notificação para o endereço ';
        }
        alert(respuesta + email);
    }
    else if (descripcion == 'Error Registrando') {
        if (cultura == 'es-VE') {
            respuesta = ' Ya esta registrado';
        }
        else if (cultura == 'en-US') {
            respuesta = ' Are you already registered';
        }
        else if (cultura == 'pt-PT') {
            respuesta = ' Já cadastrado';
        }
        alert(data.Email +  respuesta);
    }
    else if (descripcion == 'Error Enviando') {
        if (cultura == 'es-VE') {
            respuesta = 'Disculpe surgio un error al intentar enviar email a : ';
        }
        else if (cultura == 'en-US') {
            respuesta = 'You are already registered Sorry for an error when trying to send email to: ';
        }
        else if (cultura == 'pt-PT') {
            respuesta = 'Desculpe, ocorreu um erro ao tentar enviar um email para: ';
        }
        alert(respuesta + email);
    }  
}










