
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

function TextZero2(id) {

  document.getElementById(id).value = '';

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
               // console.log($(obj).val().length + ' ' + obj);
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

function DrawingMarket2(obj) {  // pinta el textbox

    $(obj).css({ "background-color": colorActivo });
}

function ColorWhiteTxt() { // blanque los textBox
    var obj = '#txt';
    var obj2 = '#txt_';
    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj + i + j;
            obj2 = obj2 + i + j;
            $(obj).css({ "background-color": colorActivo });
            $(obj2).css({ "background-color": colorActivo });
            obj = '#txt';
            obj2 = '#txt_';
        }
    }
}


function GuardarJuego(id, valor,contadorActivado,numGrilla) {
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
            GetLetrasJuego(contadorActivado,numGrilla);
        }
    });
}


function GetLetrasJuego(contadorActivado,numGrilla) {
    $.ajax({
        type: "POST",
        url: "/Process/GetLetrasJuego",
        data: { contadorActivado: contadorActivado , numGrilla: numGrilla},
        dataType: "json",
        success: function (data) {
            if (numGrilla === 2) {

                var obj = '#txt_';
                var j = 0;
                var ingresado = null;
                var eliminado = null;
                for (i = 0; i <= 80; i++) {
                    ingresado = data.ValorTxtSudoku2[i];
                    eliminado = data.ValorTxtSudoku2Eliminado[i];
                    if (i >= 0 && i <= 8) {
                        obj = obj.concat("0", i);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 9 && i <= 17) {
                        j = i - 9;
                        obj = obj.concat("1", j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 18 && i <= 26) {
                        j = i - 18;
                        obj = obj.concat("2", j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 27 && i <= 35) {
                        j = i - 27;
                        obj = obj.concat("3", j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 36 && i <= 44) {
                        j = i - 36;
                        obj = obj.concat("4",j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 45 && i <= 53) {
                        j = i - 45;
                        obj = obj.concat("5", j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 54 && i <= 62) {
                        j = i - 54;
                        obj = obj.concat("6", j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 63 && i <= 71) {
                        j = i - 63;
                        obj = obj.concat("7", j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                    else if (i >= 72 && i <= 80) {
                        j = i - 72;
                        obj = obj.concat("8", j);
                        SetTxtSudoku2(ingresado, eliminado, obj);
                        obj = '#txt_';
                    }
                }
            }
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
            $('#btnF').val(data.LetrasJuegoFEG.F);
            $('#btnE').val(data.LetrasJuegoFEG.E);
            $('#btnG').val(data.LetrasJuegoFEG.G);

        } ,complete: function () {
        }
    });
}

function SetTxtSudoku2(ingresado, eliminado, obj) {
    if (ingresado !== '') {
        $(obj).css("font-size", "32px");
        $(obj).css('color', 'green');
        $(obj).val(ingresado);
    }
    else {
        $(obj).css("font-size", "11px");
        $(obj).css('color', 'red');
        $(obj).val(eliminado);
    }
}

function Position(sentido, f, c)
{
    f = parseInt(f, 10);
    c = parseInt(c, 10);
    var i = null;
    var j = null;
    switch (sentido) {
        case "ArrowUp":
            i = f - 1; j = c;
            break;
        case "ArrowDown":
            i = f + 1; j = c;
            break;
        case "ArrowRight":
            i = f; j = c + 1;
            break;
        case "ArrowLeft":
            i = f; j = c - 1;
            break;
    }
    var obj = '#txt';
    obj = obj.concat(i, j);
    $(obj).focus();
    $('#idTxt').val(obj);
}

function Position2(sentido, f, c) {
    f = parseInt(f, 10);
    c = parseInt(c, 10);
    var i = null;
    var j = null;
    switch (sentido) {
        case "ArrowUp":
            i = f - 1; j = c;
            break;
        case "ArrowDown":
            i = f + 1; j = c;
            break;
        case "ArrowRight":
            i = f; j = c + 1;
            break;
        case "ArrowLeft":
            i = f; j = c - 1;
            break;
    }
    var obj = '#txt_';
    obj = obj.concat(i, j);
    $(obj).focus();
   $('#idTxt').val(obj);
}


function GetNumero(tipo, id) {

    var valor = $(id).val();
    if (valor === '' || valor === null)
        return false;
    $.ajax({
        type: "POST",
        url: "/Process/ReturnValorPlantilla",
        dataType: "json",
        data: { tipo: tipo , id: id , valor : valor},
        success: function (data) {
            $(data.Id).val(data.Valor);
        },
        complete: function () {
            console.log('GetNumeroValorEnPlantilla');
        }
    });
}

function ProcesosContables() {
    DeleteRows();
    var fila = '';
    $.ajax({
        type: "POST",
        url: "/Process/ProcesosContables",
        dataType: "json",
        success: function (data) {
            $.each(data, function (index, val) {
                fila = '<tr> <td> ' + val.Id + ' </td> <td> ' + val.Val1 + ' </td> <td> ' + val.Val2 + ' </td> <td> ' + val.Val3 + ' </td> <td>' + val.Itr + '</td> </tr>';
                $('#tablaTest').append(fila);
            });
        },
        complete: function () {
            PintarTabla();
            console.log('ProcesosContables');
        }
    });
}

function DeleteRows()
{
    $("#tablaTest tbody tr").remove();   
}

function PintarTabla() {
    var fila = document.getElementsByTagName('tr');
    for (var n = 1; n <= 27; n++) {
        if (n % 2 === 0)
            fila[n].style.backgroundColor = "green";
        else
            fila[n].style.backgroundColor = "white";

        fila[n].style.fontWeight;
    }
 
}

function ContadorIngresados() {

    var obj = '#txt'; 
    var contador = 0;
    var txt = null;

    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj.concat(i, j);
            txt = $(obj).val();
            if (txt !== '' && txt !== null) { contador++; }
            obj = '#txt';
        }
    }
    console.log(contador);
    if (contador >= 17) {
        $('#GuardarNuevoJuejo').show();
    }
}

function FilaRecuadroColumna(tipo) {
    $.ajax({
        type: "POST",
        url: "/Process/FilaRecuadroColumna",
        dataType: "json",
        data: { tipo: tipo},
        success: function (data) {
            console.log(data);
            $('#txtNota').val();
        },
        complete: function () {
            console.log('FilaRecuadroColumna');
        }
    });
}

