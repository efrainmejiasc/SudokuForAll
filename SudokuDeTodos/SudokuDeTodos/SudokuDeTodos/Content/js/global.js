function OcultarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).hide();
}

function MostrarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).show();
}


$('#traductor').on('change', function (e) {
    var lenguaje = $('#traductor option:selected').html();
    var index = $('#traductor option:selected').val();
    console.log(lenguaje);
    $.ajax({
        type: "POST",
        url: "/Home/Index",
        data: { lenguaje: lenguaje, index: index },
        datatype: "json",
        success: function () {
                if (index > 0)
                 SetObjectsLayount(index);
            //Stop();
        }
    });
}); 

function GetIdiomas() {
    $.ajax({
        type: "POST",
        url: "/Process/ListaLenguaje",
        data: { index: 0 },
        dataType: "json",
        success: function (data) {
            $('#triggerModal').removeAttr('disabled')
            $("#traductor").empty();
            $('#traductor').append('<option selected disabled value="0"></option>');
            $('#traductor').append('<option  value="' + 1 + '">' + data.Español + '</option>');
            $('#traductor').append('<option  value="' + 2 + '">' + data.Ingles + '</option>');
            $('#traductor').append('<option  value="' + 3 + '">' + data.Portugues + '</option>');
            if (data.Id > 1)
                $('#traductor').val(data.Id);
            else
                OcultarObjeto('entrar');

            SenialTraductor();
        }
    });
}

function SetObjectsLayount(index) {
    $.ajax({
        type: "POST",
        url: "/Process/ListaLenguaje",
        data: { index: index },
        datatype: "json",
        success: function (data) {
            //$('#olvidoPassword').text(data.ResetPassword);
            //$('#normas').text(data.Terminos);
            $('#inicio').text(data.Inicio);
            $('#entrar').text(data.Entrar);
            $('#entrar').show();
            $('#equipo').text(data.Equipo);
            $('#triggerModal').removeAttr('disabled');
            $("#traductor").empty();
            $('#traductor').append('<option selected disabled value="0">Seleccione Idioma. . .</option>');
            $('#traductor').append('<option  value="' + 1 + '">' + data.Español + '</option>');
            $('#traductor').append('<option  value="' + 2 + '">' + data.Ingles + '</option>');
            $('#traductor').append('<option  value="' + 3 + '">' + data.Portugues + '</option>');
            $('#traductor').val(data.Id);
            Stop();
        }
    });
}


var nueva = null;
var n = 0;
function SenialTraductor() {
    var text = ['Selecione um idioma. . .', 'Select a Language. . .', 'Seleccione Idioma. . .', 'Selecione um idioma. . .', 'Select a Language. . .', 'Seleccione Idioma. . .'];
    var i = Math.floor(Math.random() * (6 - 0));
    CambioCss();
    $('#traductor option[value=' + 0 + ']').text(text[i]);
    Stop();
    nueva = setInterval(SenialTraductor, 1500);
}

function Stop() {
    clearTimeout(nueva);
} 

function CambioCss() {
    var cssClass = $('#traductor').attr('class');
    if (cssClass === 'btn-primary btn') {
        $('#traductor').removeClass('btn-primary btn');
        $('#traductor').addClass('btn-danger btn');
    } else {
        $('#traductor').removeClass('btn-danger btn');
        $('#traductor').addClass('btn-primary btn');
    }
}


function VerificarEmail() {
    var email = $('#email').val();
    $.ajax({
        type: "POST",
        url: "/Process/VerificarEmail",
        data: { email: email },
        datatype: "json",
        success: function (data) {
            if (data.Status == true) {
                console.log(data.Descripcion);
            } 
        }
    });

}
