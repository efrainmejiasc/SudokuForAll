
$(document).ready(function () {

   
});


function MostrarModalIndicada(id) {
    console.log(id);
    if (id === 0) {

        document.getElementById('pruebaSitio').style.display = 'block';
    }
}

function redireccionar(url) {
    window.location.href = url;
}

