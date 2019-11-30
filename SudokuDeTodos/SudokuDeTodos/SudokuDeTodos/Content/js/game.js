function GuardarJuego(id, valor) {
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

/*function TextLetra(id) {
    if (document.getElementById(id).style.color === 'black' || document.getElementById(id).style.color === 'blue') {
        document.getElementById(id).value;
    }
    else {
        document.getElementById(id).value = '';
    }
}*/




