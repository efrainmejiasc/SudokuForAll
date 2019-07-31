﻿
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
            console.log(data.Descripcion);
            if (data.Descripcion == 'Exito')
                alert('Hemos enviado una notificacion a la direccion ' + data.Email + " ,con un codigo de verificacion para restablecer contraseña");
            else if (data.Descripcion == 'Error')
                alert('Disculpe surgio un error al intentar enviar email a : ' + data.Email);
        },
        complete: function () {
            IrIndex();
        }
    });
}

function ValidarCodigo() {
    var email = $('#email').val();
    var codigo = $('#codigoReset').val();
    $.ajax({
        type: "POST",
        url: "/Home/ValidarCodigoRestablecerPassword",
        data: { email: email, codigo: codigo },
        datatype: "json",
        success: function (data) {
            console.log(data.Descripcion);
            if (data.Descripcion == 'Exito')
                alert(data.Email);
            else if (data.Descripcion == 'Error')
                alert(data.Email);
        },
        complete: function () {
            IrEditPassword();
        }
    });
}
function IrIndex() {
       // window.location.href = "http://localhost:49983/";
    window.location.href = "http://joselelu-001-site1.etempurl.com/";
}

function IrEditPassword() {
    //window.location.href = "http://joselelu-001-site1.etempurl.com/Home/EditPassword/";
    window.location.href = "http://joselelu-001-site1.etempurl.com/Home/EditPassword/";
}

function OlvidoPassword() {
    $('#olvidoPassword').hide();
}

function OcultarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).hide();
}
