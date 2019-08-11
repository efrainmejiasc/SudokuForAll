
function OcultarObjeto(name)
{
    var objeto = '#'.concat(name);
    $(objeto).hide();
}

function GetEmail() {
    $.ajax({
        type: "POST",
        url: "/Home/EmailUser",
        datatype: "json",
        success: function (data) {
            if (data.Email != '')
                $('#email').val(data.Email);
        }
    });
}



$('#traductor').on('change', function (e) {
    var lenguaje = $('#traductor option:selected').html();
    var index = $('#traductor option:selected').val();
    console.log(lenguaje);
    $.ajax({
        type: "POST",
        url: "/Home/Index",
        data: { lenguaje: lenguaje , index: index},
        datatype: "json"
    });
}); 
  
