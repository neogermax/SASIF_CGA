var ArrayMenu = [];
   

$(document).ready(function () {

    //capturamos la url
    var URLPage = window.location.search.substring(1);
    var URLVariables = URLPage.split('&');
    var User = URLVariables[0].replace("User=", "");
    $("#User").html(User.toUpperCase());

    $("#tituloPrincipal").html("GESTION Y CONTROL DE ORDENES DE TRABAJO");

    //traemos los datos
    transacionAjax("consulta");

    //funcion para las ventanas emergentes
    $("#dialog").dialog({
        autoOpen: false
    });

});

//hacemos la transaccion al code behind por medio de Ajax
function transacionAjax(State) {
    $.ajax({
        url: "menuAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State,
            "user": $("#User").html()
        },
        //mostrar resultados de la creacion de la idea
        success: function (result) {
            if (result == "") {
                ArrayMenu = [];
            }
            else {
                ArrayMenu = JSON.parse(result);
            }
        },
        error: function () {
            $("#dialog").dialog("option", "title", "Disculpenos :(");
            $("#Mensaje_alert").text("Se genero error al realizar la transacción Ajax!");
            $("#dialog").dialog("open");
            $("#DE").css("display", "block");
        }
    });
}