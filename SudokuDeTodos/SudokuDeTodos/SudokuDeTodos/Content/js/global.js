function OcultarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).hide();
}

function MostrarObjeto(name) {
    var objeto = '#'.concat(name);
    $(objeto).show();
}

function Redireccionamiento(url) {
    window.location.href = url;
}

function OcultarValidacion() {
    document.getElementById('validacion').style.display = 'none';
    $('#password').val('');
    $('#password2').val('');
}

function OcultarUsarMismoEmail() {
    document.getElementById('msjEmail').style.display = 'none';
    $('#email').val('');
    $('#password').val('');
    $('#password2').val('');
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
        }
    });
    return false;
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
    return false;
}

function SetObjectsLayount(index) {
    $.ajax({
        type: "POST",
        url: "/Process/ListaLenguaje",
        data: { index: index },
        datatype: "json",
        success: function (data) {
            $('#olvidoPassword').text(data.ResetPassword);
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
    return false;
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

function TraductorHide() {
    var cssClass = $('#traductor').attr('class');
    if (cssClass === 'btn-primary btn') {
        $('#traductor').removeClass('btn-primary btn');
        $('#traductor').addClass('btn-success btn');
    } else {
        $('#traductor').removeClass('btn-danger btn');
        $('#traductor').addClass('btn-success btn');
    }
}

function GetEmailSession(nameVar) {
    if (nameVar === '')
        return false;
    $.ajax({
        type: "POST",
        url: "/Process/ReturnVarSession",
        data: { nameVar: nameVar },
        datatype: "json",
        success: function (data) {
            console.log(data.Descripcion);
            $('#mail').val(data.Descripcion);
        }
    });
    return false;
}

function SetTitulo(page) {
    $.ajax({
        type: "POST",
        url: "/Process/TagForm",
        datatype: "json",
        success: function (data) {
            console.log(data);
            if (page === 'GameAOne' || page === 'GameCOne') {
                $('#titulo').html(data.Numeros);
            }
            else if (page === 'GameATwo'){
                $('#titulo').html(data.Numeros);
                $('#titulo2').html(data.NummerosYCandidatosExcluidos);
            }
            else if (page === 'GameBOne') {
                $('#titulo').html(data.CandidatosIndividuales);
                $('#titulo2').html(data.CandidatosExcluidos);
            }
            else if (page === 'GameBTwo') {
                $('#titulo').html(data.CandidatosOrganizados);
                $('#titulo2').html(data.CandidatosExcluidos);
                $('#Button1').val(data.Fila);
                $('#Button2').val(data.Columna);
                $('#Button3').val(data.Recuadro);
            }
            else if (page === 'GameBThree') {
                $('#titulo').html(data.CandidatosIndividuales);
                $('#titulo2').html(data.Candidatos);
            }
            else if (page === 'GameCTwo') {
                $('#titulo').html(data.Solucion);
                $('#titulo2').html(data.Numeros);
            }
            else if (page === 'GameCThree') {
                $('#titulo').html(data.Solucion);
                $('#titulo2').html(data.CandidatosExcluidos);
            }
            else if (page === 'NewGame') {
                $('#titulo').html(data.Solucion);
            }
               
        }
    });
    return false;
}

function EstablecerFechas() {
    var today = new Date();
    var fecha = today.toISOString().substr(0, 10);
    $('#fechaInicial').val(fecha);
    $('#fechaFinal').val(fecha);
}


function ReportePago() {
    var fechaInicial = $('#fechaInicial').val();
    var fechaFinal = $('#fechaFinal').val();
  
    if (fechaInicial === '' || fechaFinal === '' ) {
        alert('Seleccione fechas');
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Manager/ReportePago",
        datatype: "json",
        data: { fechaInicial: fechaInicial, fechaFinal: fechaFinal },
        success: function (data) {
      
            CrearTabla(data);
        },
        complete: function () {
            TablaPlus();
            console.log('REPORTEPAGO');
        }
    });
    return false;
}

function CrearTabla(emp) {
    var subtotal = 0;
    var impuesto = 0;
    var total = 0;
    $('#tableReport tbody tr').remove();
    $.each(emp, function (index, item) {
        subtotal = subtotal + item.MontoPago;
        impuesto = impuesto + item.Impuesto;
        total = total + item.MontoTotal;
        let tr = `<tr> 
                      <td style="text-align: center;"> ${index + 1} </td>
                      <td style="text-align: justify;"> ${item.Email} </td>
                      <td style="text-align: justify;"> ${item.FechaPago} </td>
                      <td style="text-align: justify;"> ${item.FechaVencimiento} </td>
                      <td style="text-align: justify;"> ${item.MontoPago} </td>
                      <td style="text-align: justify;"> ${item.Impuesto} </td>
                      <td style="text-align: justify;"> ${item.MontoTotal} </td>
                      <td style="text-align: justify;"> ${item.Estado} </td>
                      <td style="text-align: center;"> <input type="button" value="Editar" class="btn btn-primary" style="width:80px;" onclick="PreEdit('${item.IdClientePago}','${item.Email}','${item.FechaPago}','${item.FechaVencimiento}');"> </td>
                      </tr>`;
        $('#tableReport tbody').append(tr);

        $('#subtotal').val(subtotal);
        $('#totalTax').val(impuesto);
        $('#total').val(total);
    });
}


function TablaPlus() {
    var initDataTable = $('#initDataTable').val();
    if (initDataTable === 'yes') return false;

    $('#tableReport').DataTable({
        language: {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: "true",
        dom: 'Bfrtilp',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel"></i> ',
                titleAttr: 'Exportar a Excel',
                className: 'btn btn-success',
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf"></i> ',
                titleAttr: 'Exportar a PDF',
                className: 'btn btn-danger'
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> ',
                titleAttr: 'Imprimir',
                className: 'btn btn-info'
            },

        ],

    });
    $('#initDataTable').val('yes');
}


function PreEdit(idClientePago, email, fechaPago, vencimiento) {

    $('#id').val(idClientePago);
    $('#email').val(email);
    $('#fechaPago').val(fechaPago);

    var parts = vencimiento.split('/');
    var fechaVencimiento = new Date(parts[2] + "-" + parts[1] + "-" + parts[0]);
    fechaVencimiento = fechaVencimiento.toISOString().substr(0, 10);
    $('#fechaVencimiento').val(fechaVencimiento);
    MostrarModalT();
    return false;
}

function EditarClientePagoFechaVencimiento() {

    var idClientePago = $('#id').val();
    var fechaVencimiento=  $('#fechaVencimiento').val();
    $.ajax({
        type: "POST",
        url: "/Manager/EditarClientePagoFechaVencimiento",
        datatype: "json",
        data: { idClientePago: idClientePago, fechaVencimiento: fechaVencimiento },
        success: function (data) {
            if (data.Descripcion === "Transaccion Exitosa") {
                ReportePago();
                alert(data.Descripcion);
                CerrarModalT();
            }
            else {
                alert(data.Descripcion);
            }
        },
        complete: function () {
            console.log('EDITARCLIENTEPAGOFECHAVENCIMIENTO');
        }
    });
    return false;

}

function MostrarModalT() {
    var modal = document.getElementById('myModal');
    modal.style.display = 'block';
}

function CerrarModalT() {
    var modal = document.getElementById('myModal');
    modal.style.display = "none";
}

function NavAdmin(page) {
    $.ajax({
        type: "POST",
        url: "/Manager/ReturnVarGerente",
        dataType: "json",
        success: function (data) {
            if (data.Descripcion === 'ACTIVO')
                Redireccionamiento(page);
            else
                Redireccionamiento('/Manager/Index');
        },
        complete: function () {
            console.log('NAVADMIN');
        }
    });
    return false;
}