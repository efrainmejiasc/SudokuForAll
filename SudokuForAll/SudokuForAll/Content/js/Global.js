
function OcultarObjeto(name)
{
    var objeto = '#'.concat(name);
    $(objeto).hide();
}

function MostrarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).show();
}

function GetEmail() {
    $.ajax({
        type: "POST",
        url: "/Home/EmailUser",
        datatype: "json",
        success: function (data) {
            if (data.Email != '')
                $('#email').val(data.Email);
        }
    });
}


$('#traductor').on('change', function (e) {
    var lenguaje = $('#traductor option:selected').html();
    var index = $('#traductor option:selected').val();
    console.log(lenguaje);
    $.ajax({
        type: "POST",
        url: "/Home/Index",
        data: { lenguaje: lenguaje , index: index},
        datatype: "json",
        success: function () {
            if (index > 0)
                SetObjectsLayount(index);
        }
    });
}); 


function SetObjectsLayount(index) {
    $.ajax({
        type: "POST",
        url: "/Home/ListaLenguaje",
        data: { index: index },
        datatype: "json",
        success: function (data) {
            $('#olvidoPassword').text(data.ResetPassword);
            $('#normas').text(data.Terminos);
            $('#inicio').text(data.Inicio);
            $('#entrar').text(data.Entrar);
            $('#entrar').show();
            $('#equipo').text(data.Equipo);
            $('#videoInit').attr('src', '../Content/imagen/expSudoku.mp4');
            $('#triggerModal').removeAttr('disabled');
            $("#traductor").empty();
            $('#traductor').append('<option selected disabled value="0">Seleccione Idioma. . .</option>');
            $('#traductor').append('<option  value="' + 1 + '">' + data.Español + '</option>');
            $('#traductor').append('<option  value="' + 2 + '">' + data.Ingles + '</option>');
            $('#traductor').append('<option  value="' + 3 + '">' + data.Portugues + '</option>');
            $('#traductor').val(data.Id);
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

function AgregarOption(name) {
    var objeto = '#'.concat(name);
    $(objeto).append('<option selected disabled value="0">Seleccione...</option>');
}



 
 
