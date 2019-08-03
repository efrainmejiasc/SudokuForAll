
function SeleccionarProceso() {
    var lblTexto = document.getElementById('lblTexto').innerText;
    if (lblTexto == 'Restablecer Password')
    {
        NotificacionRestablecerPassword();
    }
    else if (lblTexto =='Ingrese Codigo')
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
            alert(data.Descripcion);
            DireccionSite('Home', 'Index');
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
            console.log(data.RespuestaAccion);
            if (data.RespuestaAccion == 'Exito') {
                DireccionSite('Home', 'EditPassword');
            }
            else {
                alert(data.Descripcion);
                DireccionSite('Home', 'Index');
            }
        }
    });
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

function OlvidoPassword() {
    $('#olvidoPassword').hide();
}

function OcultarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).hide();
}
