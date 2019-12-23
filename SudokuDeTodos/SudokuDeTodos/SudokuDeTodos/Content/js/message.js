
$(document).ready(function () {

   
});


function MostrarModalIndicada(id) {
    console.log(id);
    if (id === 0) {
        document.getElementById('pruebaSitio').style.display = 'block';
    }
    else if (id === 1) {
        document.getElementById('ctaNoActivada').style.display = 'block';
        setTimeout(SendOtherMail, 5000, 'ctaNoActivada');
    }
    else if (id === 4) {
        document.getElementById('comprar').style.display = 'block';
        setTimeout(Redireccionar, 5000, '/Payment/Index');
    }
    else if (id === 5) {
        document.getElementById('comprar1Eravez').style.display = 'block';
    }
    else if (id === 6) {
        document.getElementById('errorInterno').style.display = 'block';
    }
}


function OcultarModalIndicada(id) {
    document.getElementById(id).style.display = 'none';
    Redireccionar('/Home/Index');
}

function Redireccionar(url) {
    window.location.href = url;
}

function SendOtherMail(value) {
    if (value === 'ctaNoActivada')
        document.getElementById('ctaNoActivada').style.display = 'none';
    else if (value === 'pruebaSitio')
        document.getElementById('pruebaSitio').style.display = 'none';

    document.getElementById('enviarOtroEmail').style.display = 'block';
}

function SendNotificacionPrueba(email) {
    if (email === '')
        return false;

    $.ajax({
        type: "POST",
        url: "/Process/EnviarNotificacionPrueba",
        data: { email: email },
        datatype: "json",
        success: function (data) {
            console.log('data');
        },
        complete: function () {
            console.log('complete');
        }
    });
    setTimeout(SendOtherMail, 5000, 'pruebaSitio');
}

