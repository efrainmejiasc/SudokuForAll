
$(document).ready(function () {

   
});


function MostrarModalIndicada(id) {
    console.log(id);
    if (id === 0) {
        document.getElementById('pruebaSitio').style.display = 'block';
       // document.getElementById('ctaNoActivada').style.display = 'block';
        //setTimeout(SendOtherMail, 5000);
    }
}

function OpenModal() {
    document.getElementById('pruebaSitio').style.display = 'block';
}

function OcultarModalIndicada(id) {
    document.getElementById(id).style.display = 'none';
    Redireccionar('/Home/Index');
}

function Redireccionar(url) {
    window.location.href = url;
}

function SendOtherMail() {
    document.getElementById('ctaNoActivada').style.display = 'none';
    document.getElementById('pruebaSitio').style.display = 'none';
    document.getElementById('enviarOtroEmail').style.display = 'block';
}

