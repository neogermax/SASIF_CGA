$(document).ready(function () {
    fecha();
});


//funcion para capturar la fecha
function fecha() {
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = d.getFullYear() + '/' +
    (('' + month).length < 2 ? '0' : '') + month + '/' +
    (('' + day).length < 2 ? '0' : '') + day;
    $("#Hours").html(output);

}