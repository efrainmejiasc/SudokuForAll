
function TextNumero(id,value) {
    if (document.getElementById(id).style.color === 'black') {
        document.getElementById(id).value;
    }
    else {
        if (value === 'Backspace' || value === 'Delete')
            return false;
            document.getElementById(id).value = value;
    }
} 

function TextZero(id) {
    if (document.getElementById(id).style.color === 'black') {
        document.getElementById(id).value;
    }
    else {
        document.getElementById(id).value = '';
    }
} 

function Navegacion(page) {
    window.open(page, "_self");
}

function StyleTxtFully() {
    var id = 'txt';
    var i = null;
    var j = null;
    var obj = '#';

    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj = '#'.concat(id, i, j);
                $(obj).css("font-size", "32px");
        }
    }
}

function StyleTxt()
{
    var id = 'txt_';
    var id1 = 'txt';
    var i = null;
    var j = null;
    var obj = null;
    var obj1 = null;
    var color = null;
 
    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj1 = '#'.concat(id1, i, j);
            $(obj1).css("font-size", "32px");
            obj = '#'.concat(id, i, j);
            color = $(obj).css("color");
            if (color === 'rgb(255, 0, 0)') {
                $(obj).css("font-size", "11px");
                console.log($(obj).val().length + ' ' + obj);
            } else {
                $(obj).css("font-size", "32px");
            }  
        }
    }
}

var colorActivo = '';
function Marcador(obj,color) { // setea el color activo y borde del marcador
    var cssClass = $('#marcador').attr('class');
    $('#marcador').removeClass(cssClass);
    $('#marcador').addClass(obj);
    $('#marcador').css('border-color', 'black');
    colorActivo = color;
    if (color === 'white') {
        ColorWhiteTxt();
    }
}

function DrawingMarket(txt) {  // pinta el textbox
    var obj = '#'.concat(txt.id);
    $(obj).css({ "background-color": colorActivo });
}

function ColorWhiteTxt() { // blanque los textBox
    var obj = '#txt';
    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj + i + j;
            $(obj).css({ "background-color": colorActivo });
            obj = '#txt';
        }
    }
}


function GuardarJuego(id, valor,contadorActivado) {
    if (valor === '')
        valor = 0;

    var i = id.substring(3, 4);
    var j = id.substring(4, 5);
    $.ajax({
        type: "POST",
        url: "/Process/GuardarJuego",
        dataType: "json",
        data: { valor: valor, i: i, j: j },
        success: function (data) {
            console.log('Guardado');
        },      
        complete: function () {
            console.log(contadorActivado);
            GetLetrasJuego(contadorActivado);
        }
    });
}

function GetLetrasJuego(contadorActivado) {
    $.ajax({
        type: "POST",
        url: "/Process/GetLetrasJuego",
        data: { contadorActivado: contadorActivado},
        dataType: "json",
        success: function (data) {
            if (!data.ContadorActivado) {
               $('#btnA').hide();
               $('#btnB').hide();
            }
            $('#btnA').val(data.LetrasJuegoACB.A);
            $('#btnB').val(data.LetrasJuegoACB.B);
            if (data.LetrasJuegoACB.A + data.LetrasJuegoACB.B > 0) {
                $('#btnBB').hide();
                $('#btnBB').show();
            }
            else
            {
                $('#BtnBB').show();
            }
            if (!data.LetrasJuegoACB.C) {
                $('#btnC').attr("src", '../Content/imagen/Look.JPG');
                $('#btnBB').show();
            }
            else if (data.LetrasJuegoACB.C)
            {
                $('#BtnC').attr("src",'../Content/imagen/UnLook.JPG');
                $('#BtnBB').hide();
                $('#BtnBB').show();
            }
            else
            {
                $('#BtnC').attr("src", '../Content/imagen/Black.JPG');
            }
            console.log(data.LetrasJuegoFEG.F);
            console.log(data.LetrasJuegoFEG.E);
            console.log(data.LetrasJuegoFEG.G);
            $('#btnF').val(data.LetrasJuegoFEG.F);
            $('#btnE').val(data.LetrasJuegoFEG.E);
            $('#btnG').val(data.LetrasJuegoFEG.G);
        }
    });
}

