
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


