src = "http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js";

function GuardarJuego(id, value) {
    if (value === '')
           value = 0;

    var i = id.substring(3, 4);
    var j = id.substring(4, 5);
    $.ajax({
        type: "POST",
        url: "/Vista/GameOne.aspx/GuardarJuego",
        contentType: "application/json",
        dataType: "json; charset=utf-8",
        data: { value: `${value}`, i: `${i}`, j: `${j}`},
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

/*function TextLetra(id) {
    if (document.getElementById(id).style.color === 'black' || document.getElementById(id).style.color === 'blue') {
        document.getElementById(id).value;
    }
    else {
        document.getElementById(id).value = '';
    }
}*/


