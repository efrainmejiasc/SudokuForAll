
$(document).ready(function () {

   
});

//ResponseMessage

function MostrarModalIndicada(id) {
    id = parseInt(id, 10);
    console.log(id);
    if (id === 0) {
        document.getElementById('pruebaSitio').style.display = 'block';
    }
    else if (id === 1) {
        document.getElementById('ctaNoActivada').style.display = 'block';
        setTimeout(ShowSendOtherMail, 5000, 'ctaNoActivada');
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
    else if (id >= 100) {

        if (id === 100) 
            document.getElementById('mensajeRespuesta').style.display = 'block';
         else if (id > 100) 
            document.getElementById('respuesta').style.display = 'block';
    }
}

function OcultarModalIndicada(id) {
    document.getElementById(id).style.display = 'none';
    Redireccionar('/Home/Index');
}

function Redireccionar(url) {
    window.location.href = url;
}

function ShowSendOtherMail(value) {
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
        success: function (data)
        {
            if (data.Id === 100)
                setTimeout(ShowSendOtherMail, 5000, 'pruebaSitio');
            else
                MostrarModalIndicada(data.Id);

            console.log(data.Id);
        },
        complete: function () {
            console.log('Notificacion_Test');
        }
    });
}

function SendOtherNotificacion(email, v) {

    if (email === '')
        return false;

    var id = parseInt(v, 10);
    if (id === 0 || id === 1) { //prueba sitio 0 y cuenta no activada 1
        console.log(id);
        $.ajax({
            type: "POST",
            url: "/Process/EnviarOtraNotificacionPrueba",
            data: { email: email },
            datatype: "json",
            success: function (data)
            {
                if (data.Id === 100) {
                    document.getElementById('enviarOtroEmail').style.display = 'none';
                    document.getElementById('respuesta').style.display = 'none';
                    document.getElementById('otroMail').style.display = 'block';
                } else {
                    document.getElementById('respuesta').style.display = 'block';
                }
                console.log(data.Id);
            },
            complete: function () {
                console.log('Otro_Email');
            }
        });
    }
}

//**************************************State


function ShowModalIndicada(id) {
    console.log(id);
    if (id === 0) {
        //link expiro
        document.getElementById('tiempoLink').style.display = 'block';
    }
    else if (id >= 1 && id <= 4) {
        //email no es valido 1 - type no valido 2 - error interno 3 - error al actualizar test 4
        document.getElementById('respuesta').style.display = 'block';
        setTimeout(Redireccionar, 5000, '/Home/Contact');
    }
    else if (id === 5 || id === 7) {
        //activacion test o cliente exitosa
        document.getElementById('respuesta').style.display = 'block';
        setTimeout(Redireccionar, 5000, '/Vista/GameAOne.aspx');
    }
    else if (id === 6) {
        // error al actualizar cliente 4
        document.getElementById('respuesta').style.display = 'block';
        setTimeout(Redireccionar, 5000, '/Home/Contact');
    }
}