/*-------------------- carga ---------------------------*/
//hacemos la transaccion al code behind por medio de Ajax para cargar el droplist
function transacionAjax_CargaOperario(State) {
    $.ajax({
        url: "OpeTurnosAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State,
            "tabla": 'CGA_OPERARIOS'
        },
        //Transaccion Ajax en proceso
        success: function (result) {
            if (result == "") {
                ArrayCombo = [];
            }
            else {
                ArrayCombo = JSON.parse(result);
                charge_CatalogList(ArrayCombo, "DDLMaquina", 1);
            }
        },
        error: function () {

        }
    });
}

/*-------------------- carga turno nueva causal---------------------------*/
//hacemos la transaccion al code behind por medio de Ajax para los DDl Turno
function transacionAjax_CTurno(State) {
    $.ajax({
        url: "OpeTurnosAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State
        },
        //Transaccion Ajax en proceso
        success: function (result) {
            if (result == "") {
                ArrayTurno = [];
            }
            else {
                ArrayTurno = JSON.parse(result);

                charge_CatalogList(ArrayTurno, "DDLTurno_1", 1);
                charge_CatalogList(ArrayTurno, "DDLTurno_2", 1);
                charge_CatalogList(ArrayTurno, "DDLTurno_3", 1);
                charge_CatalogList(ArrayTurno, "DDLTurno_4", 1);
                charge_CatalogList(ArrayTurno, "DDLTurno_5", 1);
                charge_CatalogList(ArrayTurno, "DDLTurno_6", 1);
            }
        },
        error: function () {

        }
    });
}

/*-------------------- carga festivos---------------------------*/
//hacemos la transaccion al code behind por medio de Ajax para TRAER LOS FESTIVOS
function transacionAjax_CargaFestivo(State) {

    $.ajax({
        url: "OpeTurnosAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State
        },
        //Transaccion Ajax en proceso
        success: function (result) {
            if (result == "") {
                ArrayFestivos = [];
            }
            else {
                ArrayFestivos = JSON.parse(result);
            }
        },
        error: function () {
        }
    });
}

/*-------------------- carga turnos ---------------------------*/
//hacemos la transaccion al code behind por medio de Ajax para TRAER los tiempos de cada turno
function transacionAjax_TTurno(State) {
    $.ajax({
        url: "OpeTurnosAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State
        },
        //Transaccion Ajax en proceso
        success: function (result) {
            if (result == "") {
                ArrayTTurnos = [];
            }
            else {
                ArrayTTurnos = JSON.parse(result);
            }
        },
        error: function () {
        }
    });
}

/*-------------------- carga turnos ---------------------------*/
//hacemos la transaccion al code behind por medio de Ajax para TRAER los tiempos de cada turno
function transacionAjax_Estado(State) {
    $.ajax({
        url: "OpeTurnosAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State,
            "Maq": $("#DDLMaquina").val(),
            "Mes": Mes,
            "Year": Year
        },
        //Transaccion Ajax en proceso
        success: function (result) {
            State_Process = result;
            TurnoEstado(result);
        },
        error: function () {
        }
    });
}

/*-------------------- carga turnos Grid ---------------------------*/
//hacemos la transaccion al code behind por medio de Ajax para traer la programacion guardada en la BD
function transacionAjax_ReadGrid(State) {
    $.ajax({
        url: "OpeTurnosAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State,
            "Maq": $("#DDLMaquina").val(),
            "Mes": Mes,
            "Year": Year
        },
        //Transaccion Ajax en proceso
        success: function (result) {
            if (result == "") {
                ArrayCalendar = [];
            }
            else {
                ArrayCalendar = JSON.parse(result);
                Complete_Edit_Calendar();
                Grid_Calendar_Edit();
            }
        },
        error: function () {
        }
    });
}

function transacionAjax_Save(State) {
    //recorer array para el ingreso del calendario
    listCalendar = JSON.stringify(ArrayCalendar);

    if (State == "CREAR") {
        $.ajax({
            url: "OpeTurnosAjax.aspx",
            type: "POST",
            //crear json
            data: { "action": State,
                "listCalendar": listCalendar,
                "user": $("#User").html()
            },
            //Transaccion Ajax en proceso
            success: function (result) {
                if (result == "CREO") {
                    $("#dialog").dialog("option", "title", "Exito");
                    $("#Mensaje_alert").text("la programacion de la maquina (" + $('#DDLMaquina  option:selected').text() + ") del mes de " + Mes + " fue creada exitosamente! ");
                    $("#dialog").dialog("open");
                    $("#DE").css("display", "none");
                    $("#SE").css("display", "block");
                    $("#WE").css("display", "none");
                    $("#BtnSave").css("display", "none");
                    ArrayCalendar = [];
                }
                else {
                    $("#dialog").dialog("option", "title", "Atención");
                    $("#Mensaje_alert").text("la programacion de la maquina (" + $('#DDLMaquina  option:selected').text() + ") del mes de " + Mes + " no se realizo! ");
                    $("#dialog").dialog("open");
                    $("#DE").css("display", "block");
                    $("#SE").css("display", "none");
                    $("#WE").css("display", "none");
                    $("#BtnSave").css("display", "none");
                }
            },
            error: function () {
            }
        });
    }
    else {
        $.ajax({
            url: "OpeTurnosAjax.aspx",
            type: "POST",
            //crear json
            data: { "action": State,
                "listCalendar": listCalendar,
                "Maq": $("#DDLMaquina").val(),
                "Mes": Mes,
                "Year": Year,
                "user": $("#User").html()
            },
            //Transaccion Ajax en proceso
            success: function (result) {
                if (result == "CREO") {
                    $("#dialog").dialog("option", "title", "Exito");
                    $("#Mensaje_alert").text("la programacion de la maquina (" + $('#DDLMaquina  option:selected').text() + ") del mes de " + Mes + " fue modificada exitosamente! ");
                    $("#dialog").dialog("open");
                    $("#DE").css("display", "none");
                    $("#SE").css("display", "block");
                    $("#WE").css("display", "none");
                    $("#BtnSave").css("display", "none");
                    ArrayCalendar = [];
                }
                else {
                    $("#dialog").dialog("option", "title", "Atención");
                    $("#Mensaje_alert").text("la programacion de la maquina (" + $('#DDLMaquina  option:selected').text() + ") del mes de " + Mes + " no se realizo! ");
                    $("#dialog").dialog("open");
                    $("#DE").css("display", "block");
                    $("#SE").css("display", "none");
                    $("#WE").css("display", "none");
                    $("#BtnSave").css("display", "none");
                }
            },
            error: function () {
            }
        });
    }
}

/*-------------------- carga centro planta---------------------------*/
//hacemos la transaccion al code behind por medio de Ajax para los campos del TVC
function transacionAjax_CargaCentroPlanta(State, ID_Maquina) {

    $.ajax({
        url: "OpeTurnosAjax.aspx",
        type: "POST",
        //crear json
        data: { "action": State,
            "ID_Maquina": ID_Maquina

        },
        //Transaccion Ajax en proceso
        success: function (result) {
            if (result == "") {
                ArrayCentroPlanta = [];
            }
            else {
                ArrayCentroPlanta = JSON.parse(result);
                Centro_ID = ArrayCentroPlanta[0].Centro_ID;
                $("#LblCentro").html(ArrayCentroPlanta[0].Centro_ID + " " + ArrayCentroPlanta[0].Descripcion);
                $("#lblPlanta").html(ArrayCentroPlanta[0].Descripcion_Planta);
                $("#Centro").css("display", "-webkit-inline-box");
                $("#Planta").css("display", "-webkit-inline-box");
                $("#LblCentro").css("display", "-webkit-inline-box");
                $("#lblPlanta").css("display", "-webkit-inline-box");
                $("#Centro").css("display", "table-cell");
                $("#Planta").css("display", "table-cell");
                $("#LblCentro").css("display", "table-cell");
                $("#lblPlanta").css("display", "table-cell");

                $("#inicial").css("display", "-webkit-inline-box");
                $("#DateStart").css("display", "-webkit-inline-box");
                $("#inicial").css("display", "table-cell");
                $("#DateStart").css("display", "table-cell");

                $("#BtnShearch").css("display", "-webkit-inline-box");
            }
        },
        error: function () {

        }
    });
}
