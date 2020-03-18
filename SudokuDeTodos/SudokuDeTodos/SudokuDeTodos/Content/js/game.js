
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
    $('#txtNota2').val('0');
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

function SizeFontTxt() {
    var obj = '#txt_';


    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj.concat(i, j);
            $(obj).css("fontSize", 10);
           
            obj = '#txt_';
        }
    }
}

function DosLenght() {
    var obj = '#txt_';
    var content = null;
    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj.concat(i, j);
            content = $(obj).val();
            if (content.length === 4) {
                $(obj).css('background-color', 'Chartreuse');
            }
            obj = '#txt_';
        }
    }
    return false;
}

function TresLenght() {
    var obj = '#txt_';
    var content = null;
    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj.concat(i, j);
            content = $(obj).val();
            if (content.length === 6) {
                $(obj).css('background-color', 'Orange');
            }
            obj = '#txt_';
        }
    }
    return false;
}

function Reset23() {
    var obj = '#txt_';

    for (i = 0; i <= 8; i++) {
        for (j = 0; j <= 8; j++) {
            obj = obj.concat(i, j);
            $(obj).css('background-color', 'White');
            obj = '#txt_';
        }
    }
    return false;
}

function NumeroIngresado() {
    $.ajax({
        type: "POST",
        url: "/Process/NumeroIngresado",
        contentType: "application/json; chartset=utf-8",
        datatype: "json",
        success: function (r) {
            var obj = '#txt_';
            console.log(r);
            $('#txt_00').val(r.txt00); $('#txt_01').val(r.txt01); $('#txt_02').val(r.txt02); $('#txt_03').val(r.txt03); $('#txt_04').val(r.txt04); $('#txt_05').val(r.txt05); $('#txt_06').val(r.txt06); $('#txt_07').val(r.txt07); $('#txt_08').val(r.txt08);
            $('#txt_10').val(r.txt10); $('#txt_11').val(r.txt11); $('#txt_12').val(r.txt12); $('#txt_13').val(r.txt13); $('#txt_14').val(r.txt14); $('#txt_15').val(r.txt15); $('#txt_16').val(r.txt16); $('#txt_17').val(r.txt17); $('#txt_18').val(r.txt18);
            $('#txt_20').val(r.txt20); $('#txt_21').val(r.txt21); $('#txt_22').val(r.txt22); $('#txt_23').val(r.txt23); $('#txt_24').val(r.txt24); $('#txt_25').val(r.txt25); $('#txt_26').val(r.txt26); $('#txt_27').val(r.txt27); $('#txt_28').val(r.txt28);
            $('#txt_30').val(r.txt30); $('#txt_31').val(r.txt31); $('#txt_32').val(r.txt32); $('#txt_33').val(r.txt33); $('#txt_34').val(r.txt34); $('#txt_35').val(r.txt35); $('#txt_36').val(r.txt36); $('#txt_37').val(r.txt37); $('#txt_38').val(r.txt38);
            $('#txt_40').val(r.txt40); $('#txt_41').val(r.txt41); $('#txt_42').val(r.txt42); $('#txt_43').val(r.txt43); $('#txt_44').val(r.txt44); $('#txt_45').val(r.txt45); $('#txt_46').val(r.txt46); $('#txt_47').val(r.txt47); $('#txt_48').val(r.txt48);
            $('#txt_50').val(r.txt50); $('#txt_51').val(r.txt51); $('#txt_52').val(r.txt52); $('#txt_53').val(r.txt53); $('#txt_54').val(r.txt54); $('#txt_55').val(r.txt55); $('#txt_56').val(r.txt56); $('#txt_57').val(r.txt57); $('#txt_58').val(r.txt58);
            $('#txt_60').val(r.txt60); $('#txt_61').val(r.txt61); $('#txt_62').val(r.txt62); $('#txt_63').val(r.txt63); $('#txt_64').val(r.txt64); $('#txt_65').val(r.txt65); $('#txt_66').val(r.txt66); $('#txt_67').val(r.txt67); $('#txt_68').val(r.txt68);
            $('#txt_70').val(r.txt70); $('#txt_71').val(r.txt71); $('#txt_72').val(r.txt72); $('#txt_73').val(r.txt73); $('#txt_74').val(r.txt74); $('#txt_75').val(r.txt75); $('#txt_76').val(r.txt76); $('#txt_77').val(r.txt77); $('#txt_78').val(r.txt78);
            $('#txt_80').val(r.txt80); $('#txt_81').val(r.txt81); $('#txt_82').val(r.txt82); $('#txt_83').val(r.txt83); $('#txt_84').val(r.txt84); $('#txt_85').val(r.txt85); $('#txt_86').val(r.txt86); $('#txt_87').val(r.txt87); $('#txt_88').val(r.txt88);
        },
        complete: function () {
           
        }
    });
    return false;
}

/* 
    $('#txt00').val(); $('#txt01').val(); $('#txt02').val(); $('#txt03').val(); $('#txt04').val(); $('#txt05').val(); $('#txt06').val(); $('#txt07').val(); $('#txt08').val();
    $('#txt10').val(); $('#txt11').val(); $('#txt12').val(); $('#txt13').val(); $('#txt14').val(); $('#txt15').val(); $('#txt16').val(); $('#txt17').val(); $('#txt18').val();
    $('#txt20').val(); $('#txt21').val(); $('#txt22').val(); $('#txt23').val(); $('#txt24').val(); $('#txt25').val(); $('#txt26').val(); $('#txt27').val(); $('#txt28').val();
    $('#txt30').val(); $('#txt31').val(); $('#txt32').val(); $('#txt33').val(); $('#txt34').val(); $('#txt35').val(); $('#txt36').val(); $('#txt37').val(); $('#txt38').val();
    $('#txt40').val(); $('#txt41').val(); $('#txt42').val(); $('#txt43').val(); $('#txt44').val(); $('#txt45').val(); $('#txt46').val(); $('#txt47').val(); $('#txt48').val();
    $('#txt50').val(); $('#txt51').val(); $('#txt52').val(); $('#txt53').val(); $('#txt54').val(); $('#txt55').val(); $('#txt56').val(); $('#txt57').val(); $('#txt58').val();
    $('#txt60').val(); $('#txt61').val(); $('#txt62').val(); $('#txt63').val(); $('#txt64').val(); $('#txt65').val(); $('#txt66').val(); $('#txt67').val(); $('#txt68').val();
    $('#txt70').val(); $('#txt71').val(); $('#txt72').val(); $('#txt73').val(); $('#txt74').val(); $('#txt75').val(); $('#txt76').val(); $('#txt77').val(); $('#txt78').val();
    $('#txt80').val(); $('#txt81').val(); $('#txt82').val(); $('#txt83').val(); $('#txt84').val(); $('#txt85').val(); $('#txt86').val(); $('#txt87').val(); $('#txt88').val();
 */