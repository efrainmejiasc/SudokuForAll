﻿function GuardarJuego(id, valor) {
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

        }
    });
}

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

function Marcador(obj) {
    var cssClass = $('#marcador').attr('class');
    $('#marcador').removeClass(cssClass);
    $('#marcador').addClass(obj);

}

/*function TextLetra(id) {
    if (document.getElementById(id).style.color === 'black' || document.getElementById(id).style.color === 'blue') {
        document.getElementById(id).value;
    }
    else {
        document.getElementById(id).value = '';
    }
}*/




