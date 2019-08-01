

function AbrirModalIndicada(openModal) {

    if (openModal == 'prueba') {
        var obj3 = document.getElementById('probarSitio');
        obj3.style.display = 'block';
    }
    else if (openModal == 'Open') {
        var obj9 = document.getElementById('intro');
        obj9.style.display = 'block';
    }
    else if (openModal == 'Index') { //Redirecciona a Index -> Home
        var obj4 = document.getElementById('intro');
        obj4.style.display = 'block';
        setTimeout(RedirectToIndex, 4000);
    }
    else if (openModal == 'Login') {//Redirecciona a Login -> Home
        var obj5 = document.getElementById('intro');
        obj5.style.display = 'block';
        setTimeout(RedirectToLogin, 4000);
    }
    else if (openModal == 'Contact') {//Redirecciona a Contact -> Home
        var obj6 = document.getElementById('intro');
        obj6.style.display = 'block';
        setTimeout(RedirectToContact, 4000);
    }
    else if (openModal == 'Register') {//Redirecciona a Register -> Home
        var obj7 = document.getElementById('intro');
        obj7.style.display = 'block';
        setTimeout(RedirectToRegister, 4000);
    }
    else if (openModal == 'EditPasswordNotify') {//Redirecciona a EditPasswordNotify -> Home
        var obj8 = document.getElementById('intro');
        obj8.style.display = 'block';
        setTimeout(RedirectToEditPasswordNotify, 4000);
    }
    else if (openModal == 'comprarRegistrarse') {
        var obj10 = document.getElementById('comprarRegistrarse');
        obj10.style.display = 'block';
    }
}

function CerrarIntro() {
    var obj = document.getElementById('intro');
    obj.style.display = 'none';
}

function RedirectToIndex() {
    var obj = document.getElementById('intro');
    obj.style.display = 'none';
    DireccionSite('Home', 'Index');
}

function RedirectToLogin() {
    var obj = document.getElementById('intro');
    obj.style.display = 'none';
    DireccionSite('Home', 'Login');
}

function RedirectToContact() {
    var obj = document.getElementById('intro');
    obj.style.display = 'none';
    DireccionSite('Home', 'Contact');
}

function RedirectToRegister() {
    var obj = document.getElementById('intro');
    obj.style.display = 'none';
    DireccionSite('Home', 'Register');
}

function ComprarRegistrarse() {
    DireccionSite('Home', 'Register');
}

function RedirectToEditPasswordNotify() {
    var obj = document.getElementById('intro');
    obj.style.display = 'none';
    DireccionSite('Home', 'EditPasswordNotify');
}

function OcultarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).hide();
}

function CerrarModalProbarSitio() {
    var modal = document.getElementById('probarSitio');
    modal.style.display = 'none';
    DireccionSite('Home', 'Index');
}

function CerrarModalComprarRegistrarse() {
    var modal = document.getElementById('comprarRegistrarse');
    modal.style.display = 'none';
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
            console.log(data.Descripcion);
            if (data.Descripcion == 'Exito')
                alert('Hemos enviado una notificacion a la direccion ' + data.Email);
            else if (data.Descripcion == 'Error Registrando')
                alert(data.Email + ' Ya esta registrado');
            else if (data.Descripcion == 'Error Enviando')
                alert('Disculpe surgio un error al intentar enviar email a : ' + data.Email);
        },
        complete: function () {
            var modal = document.getElementById('probarSitio');
            modal.style.display = 'none';
            DireccionSite('Home', 'Index');
        }
    });
}








