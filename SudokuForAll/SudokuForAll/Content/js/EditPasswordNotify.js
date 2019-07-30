
function SeleccionarProceso() {
    var lblTexto = document.getElementById('lblTexto').innerText;
    if (lblTexto == 'Restablecer Password')
    {
        NotificacionRestablecerPassword();
    }
    else
    {
        ValidarCodigo();
    }
}


function NotificacionRestablecerPassword() {
    var email = $('#email').val();
    $.ajax({
        type: "POST",
        url: "/Home/NotificacionRestablecerPassword",
        data: { email: email },
        datatype: "json",
        success: function (data) {
            console.log(data.RespuestaAccion);
            if (data.RespuestaAccion == 'Exito')
                alert('Hemos enviado una notificacion a la direccion ' + data.Email + " ,con un password provicional");
            else if (data.RespuestaAccion == 'Error Enviando')
                alert('Disculpe surgio un error al intentar enviar email a : ' + data.Email);
        },
        complete: function () {
            IrIndex();
        }
    });
}

function ValidarCodigo() {
    var email = $('#email').val();
    var codigo = $('#codigo').val();
    $.ajax({
        type: "POST",
        url: "/Home/ValidarCodigoRestablecerPassword",
        data: { email: email, codigo: codigo },
        datatype: "json",
        success: function (data) {
            console.log(data.RespuestaAccion);
            if (data.RespuestaAccion == 'Exito')
                alert(data.Email);
            else if (data.RespuestaAccion == 'Error')
                alert(data.Email);
        },
        complete: function () {
            IrEditPassword();
        }
    });
}
function IrIndex() {
        window.location.href = "http://localhost:49799/";
}

function IrEditPassword() {
    window.location.href = "http://localhost:49799/Home/EditPassword/";
}
