
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
  
