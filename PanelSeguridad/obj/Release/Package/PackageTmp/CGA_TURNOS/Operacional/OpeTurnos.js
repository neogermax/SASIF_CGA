/*--------------- region de variables globales --------------------*/
var ArrayTurnosOp = [];
var ArrayCentroPlanta = [];
var CantDia_X_Mes = [];
var Array_Date = [];
var D_Semana = [];
var ArrayFestivos = [];
var ArrayTurno = [];
var ArrayTTurnos = [];
var ArrayCalendar = [];
var listCalendar = [];

var estado;
var Centro_ID;
var FechaAct;
var ID_Maquina;
var Bisiesto = "N";
var Mes;
var Year;
var Num_Mes;
var Dias_Habiles = 0;
var HorasProgramadas = 0;
var Turno_pos;
var State_Process;
var Key_ArrayCalendar = "";


CantDia_X_Mes[0] = ["Enero", 31];
CantDia_X_Mes[1] = ["Febrero", 28];
CantDia_X_Mes[2] = ["Marzo", 31];
CantDia_X_Mes[3] = ["Abril", 30];
CantDia_X_Mes[4] = ["Mayo", 31];
CantDia_X_Mes[5] = ["Junio", 30];
CantDia_X_Mes[6] = ["Julio", 31];
CantDia_X_Mes[7] = ["Agosto", 31];
CantDia_X_Mes[8] = ["Septiembre", 30];
CantDia_X_Mes[9] = ["Octubre", 31];
CantDia_X_Mes[10] = ["Noviembre", 30];
CantDia_X_Mes[11] = ["Diciembre", 31];

D_Semana[0] = "Domingo";
D_Semana[1] = "Lunes";
D_Semana[2] = "Martes";
D_Semana[3] = "Miercoles";
D_Semana[4] = "Jueves";
D_Semana[5] = "Viernes";
D_Semana[6] = "Sabado";

/*--------------- region de variables globales --------------------*/

//evento load de los Links
$(document).ready(function () {
    transacionAjax_CargaOperario('cargar_droplist_operario');
    transacionAjax_CargaFestivo('Cargar_Festivos');
    transacionAjax_CTurno('CTurno');
    transacionAjax_TTurno('TTurno');

    CCentro_Planta();
    CambioMaquina();

    T_Val(1);
    T_Val(2);
    T_Val(3);
    T_Val(4);
    T_Val(5);
    T_Val(6);

    $("#ESelect").css("display", "none");
    $("#DE").css("display", "none");
    $("#SE").css("display", "none");
    $("#WE").css("display", "none");
    $("#L_Bisiesto").css("display", "none");

    $("#Centro").css("display", "none");
    $("#Planta").css("display", "none");
    $("#LblCentro").css("display", "none");
    $("#lblPlanta").css("display", "none");

    $("#inicial").css("display", "none");
    $("#final").css("display", "none");
    $("#TurnosOp").css("display", "none");
    $("#DateStart").css("display", "none");
    $("#DateEnd").css("display", "none");
    $("#TD_L_TurnosOp").css("display", "none");
    $("#BtnShearch").css("display", "none");
    $("#Img1").css("display", "none");
    $("#Img2").css("display", "none");

    $("#container_TTurnosOp").css("display", "none");

    //funcion para las ventanas emergentes
    $("#dialog").dialog({
        autoOpen: false
    });

    $("#dialog_eliminar").dialog({
        autoOpen: false
    });

    $("#Dialog_Grid").dialog({
        autoOpen: false,
        dialogClass: "Dialog_personal",
        modal: true,
        width: 800,
        height: 580,
        overlay: {
            opacity: 0.5,
            background: "black"
        },
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show(); }
    });

    $("#Dialog_Ope_turnos").dialog({
        autoOpen: false,
        dialogClass: "Dialog_personal",
        modal: true,
        width: 600,
        height: 450,
        overlay: {
            opacity: 0.5,
            background: "black"
        },
        open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show(); }
    });

    //funcion para campos numericos
    $('.solo-numero').keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });

    //funcion para agregar el calendario
    $(function () {
        $("#DateStart").datepicker();
    });

    $("#DateStart").datepicker({
        changeMonth: true,
        changeYear: true,
        showWeek: true,
        yearRange: "2000:2020",
        dateFormat: 'MM yy',

        onClose: function (dateText, inst) {
            $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
            Bisiesto = Valida_Bisiesto();
            if (Bisiesto == "Y") {
                $("#L_Bisiesto").css("display", "-webkit-inline-box");
                $("#L_Bisiesto").css("display", "table-cell");
            }
            else {
                $("#L_Bisiesto").css("display", "none");
            }
            transacionAjax_Estado("Consulta_Proceso");
        }
    });

    $(".monthPicker").focus(function () {
        $(".ui-datepicker-calendar").hide();
        $("#ui-datepicker-div").position({
            my: "center top",
            at: "center bottom",
            of: $(this)
        });
    });
});

//valida si es creacion oedicion para la grilla
function TurnoEstado(Process) {
    if (Process == "CREAR") {
        Grid_Calendar();
        $("#L_Mes").html(Mes);
        $("#L_Year").html(Year);
        $("#Dialog_Grid").dialog("option", "title", "Maquina: " + $("#DDLMaquina :selected").text());
        $("#Dialog_Grid").dialog("open");
        Dias_Habiles = 0;
    }
    else {
        transacionAjax_ReadGrid("Read_grid");
        $("#L_Mes").html(Mes);
        $("#L_Year").html(Year);
        $("#Dialog_Grid").dialog("option", "title", "Maquina: " + $("#DDLMaquina :selected").text());
        $("#Dialog_Grid").dialog("open");
    }
}

//salida del formulario
function btnSalir() {
    window.location = "../../Menu/menu.aspx?User=" + $("#User").html();
}

//funcion que dispara elcombo del tipo
function CCentro_Planta() {

    $("#DDLMaquina").change(function () {
        loadChildrenMaquina($(this));
    });

}

//fucion que carga desde ddl MAQUINA el centro y la planta
function loadChildrenMaquina(obj) {
    ID_Maquina = $(obj).val();
    transacionAjax_CargaCentroPlanta('cargar_CentroPlanta', ID_Maquina);
}


//que limpia y desbloquea para nuevo turno
function CambioMaquina() {
    $("#DDLMaquina").change(function () {

        ArrayCalendar = [];
        $("#BtnSave").css("display", "-webkit-inline-box");
        $("#BtnSave").css("display", "table-cell");

    });
}

//funcion que crea el calendario segun opcion
function Grid_Calendar() {

    var html_TG_turnos = "";
    var Cant_Dias = 0;
    if (Mes == "Febrero" && Bisiesto == "Y") {
        Cant_Dias = 29;
    }
    else {
        for (item in CantDia_X_Mes) {
            if (Mes == CantDia_X_Mes[item][0]) {
                Cant_Dias = CantDia_X_Mes[item][1];
                Num_Mes = item;
            }
        }
    }

    html_TG_turnos = "<table id='T_GTurno'  border='1' cellpadding='1' cellspacing='1' style='width: 100%; text-align: center;'><thead><tr><th>Seleccione</th><th>Nº Dia</th><th>Dia</th><th>Tiempo Programado</th></tr></thead><tbody>";
    var flag_festivo = 0;

    for (D = 1; D <= Cant_Dias; D++) {
        var Cons_Fecha = new Date(Year, Num_Mes, D);
        var StrDia = D_Semana[Cons_Fecha.getDay()];
        var jsonCalendar;

        if (StrDia != "Domingo") {
            for (DF in ArrayFestivos) {
                if (Year == ArrayFestivos[DF].Year) {
                    var MesVal = parseInt(ArrayFestivos[DF].StrMes) - 1;
                    if (Num_Mes == MesVal) {
                        var DiaVal = parseInt(ArrayFestivos[DF].StrDia);
                        if (D == DiaVal) {
                            html_TG_turnos += "<tr class='Grid_Festivos' id= 'T_GTurno_" + Year + "_" + Mes + "_" + D + "'><td><input type ='radio' class= 'TurnoOpe' name='verG' onclick=\"Turno_Operacional('" + D + "','" + Mes + "','" + Year + "','" + StrDia + "')\"></input></td><td>" + D + "</td><td>" + StrDia + "</td><td id = 'T_P_" + Year + "_" + Mes + "_" + D + "'>0</td></tr>";

                            //creamos json para guardarlos en un array
                            jsonCalendar = {
                                "PuestoTrabajo_ID": $("#DDLMaquina").val(),
                                "Ano_Mes_ID": "",
                                "Dia": D,
                                "Mes": Num_Mes,
                                "Year": Year,
                                "StrDia": StrDia,
                                "StrMes": Mes,
                                "Day_Type": "F",
                                "T_1": "0",
                                "T_1_HI": "0",
                                "T_1_HF": "0",
                                "T_2": "0",
                                "T_2_HI": "0",
                                "T_2_HF": "0",
                                "T_3": "0",
                                "T_3_HI": "0",
                                "T_3_HF": "0",
                                "T_4": "0",
                                "T_4_HI": "0",
                                "T_4_HF": "0",
                                "T_5": "0",
                                "T_5_HI": "0",
                                "T_5_HF": "0",
                                "T_6": "0",
                                "T_6_HI": "0",
                                "T_6_HF": "0",
                                "Programado": "0"
                            };

                            //cargamos el array con el json
                            ArrayCalendar.push(jsonCalendar);

                            flag_festivo = 1;
                        }
                    }
                }
            }
            if (flag_festivo == 0) {
                html_TG_turnos += "<tr id= 'T_GTurno_" + Year + "_" + Mes + "_" + D + "'><td><input type ='radio' class= 'TurnoOpe' name='verG' onclick=\"Turno_Operacional('" + D + "','" + Mes + "','" + Year + "','" + StrDia + "')\"></input></td><td>" + D + "</td><td>" + StrDia + "</td><td id = 'T_P_" + Year + "_" + Mes + "_" + D + "'>0</td></tr>";

                //creamos json para guardarlos en un array
                jsonCalendar = {
                    "PuestoTrabajo_ID": $("#DDLMaquina").val(),
                    "Ano_Mes_ID": "",
                    "Dia": D,
                    "Mes": Num_Mes,
                    "Year": Year,
                    "StrDia": StrDia,
                    "StrMes": Mes,
                    "Day_Type": "H",
                    "T_1": "0",
                    "T_1_HI": "0",
                    "T_1_HF": "0",
                    "T_2": "0",
                    "T_2_HI": "0",
                    "T_2_HF": "0",
                    "T_3": "0",
                    "T_3_HI": "0",
                    "T_3_HF": "0",
                    "T_4": "0",
                    "T_4_HI": "0",
                    "T_4_HF": "0",
                    "T_5": "0",
                    "T_5_HI": "0",
                    "T_5_HF": "0",
                    "T_6": "0",
                    "T_6_HI": "0",
                    "T_6_HF": "0",
                    "Programado": "0"
                };

                //cargamos el array con el json
                ArrayCalendar.push(jsonCalendar);

                Dias_Habiles = Dias_Habiles + 1;
            } else {
                flag_festivo = 0;
            }
        }
        else {
            html_TG_turnos += "<tr class='Grid_Domingos' id= 'T_GTurno_" + Year + "_" + Mes + "_" + D + "'><td><input type ='radio' class= 'TurnoOpe' name='verG' onclick=\"Turno_Operacional('" + D + "','" + Mes + "','" + Year + "','" + StrDia + "')\"></input></td><td>" + D + "</td><td>" + StrDia + "</td><td id = 'T_P_" + Year + "_" + Mes + "_" + D + "'>0</td></tr>";

            //creamos json para guardarlos en un array
            jsonCalendar = {
                "PuestoTrabajo_ID": $("#DDLMaquina").val(),
                "Ano_Mes_ID": "",
                "Dia": D,
                "Mes": Num_Mes,
                "Year": Year,
                "StrDia": StrDia,
                "StrMes": Mes,
                "Day_Type": "D",
                "T_1": "0",
                "T_1_HI": "0",
                "T_1_HF": "0",
                "T_2": "0",
                "T_2_HI": "0",
                "T_2_HF": "0",
                "T_3": "0",
                "T_3_HI": "0",
                "T_3_HF": "0",
                "T_4": "0",
                "T_4_HI": "0",
                "T_4_HF": "0",
                "T_5": "0",
                "T_5_HI": "0",
                "T_5_HF": "0",
                "T_6": "0",
                "T_6_HI": "0",
                "T_6_HF": "0",
                "Programado": "0"
            };

            //cargamos el array con el json
            ArrayCalendar.push(jsonCalendar);
        }
    }

    html_TG_turnos += "</tbody></table>";
    $("#Container_OP_Turnos").html(html_TG_turnos);

    $(".TurnoOpe").click(function () {
    });

    $("#T_GTurno").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#L_habil").html(Dias_Habiles);
    SumaTotal();
}

//funcion que crea el calendario segun opcion
function Grid_Calendar_Edit() {

    var html_TG_turnos = "";
    var Cant_Dias_habiles = 0;

    html_TG_turnos = "<table id='T_GTurno'  border='1' cellpadding='1' cellspacing='1' style='width: 100%; text-align: center;'><thead><tr><th>Seleccione</th><th>Nº Dia</th><th>Dia</th><th>Tiempo Programado</th></tr></thead><tbody>";
    var flag_festivo = 0;

    for (item_C in ArrayCalendar) {
        if (ArrayCalendar[item_C].StrDia != "Domingo") {
            if (ArrayCalendar[item_C].Day_Type == "F") {
                html_TG_turnos += "<tr class='Grid_Festivos' id= 'T_GTurno_" + ArrayCalendar[item_C].Year + "_" + ArrayCalendar[item_C].StrMes + "_" + ArrayCalendar[item_C].Dia + "'><td><input type ='radio' class= 'TurnoOpe' name='verG' onclick=\"Turno_Operacional('" + ArrayCalendar[item_C].Dia + "','" + ArrayCalendar[item_C].StrMes + "','" + ArrayCalendar[item_C].Year + "','" + ArrayCalendar[item_C].StrDia + "')\"></input></td><td>" + ArrayCalendar[item_C].Dia + "</td><td>" + ArrayCalendar[item_C].StrDia + "</td><td id = 'T_P_" + ArrayCalendar[item_C].Year + "_" + ArrayCalendar[item_C].StrMes + "_" + ArrayCalendar[item_C].Dia + "'>" + ArrayCalendar[item_C].Programado + "</td></tr>";
            }
            else {
                html_TG_turnos += "<tr id= 'T_GTurno_" + ArrayCalendar[item_C].Year + "_" + ArrayCalendar[item_C].StrMes + "_" + ArrayCalendar[item_C].Dia + "'><td><input type ='radio' class= 'TurnoOpe' name='verG' onclick=\"Turno_Operacional('" + ArrayCalendar[item_C].Dia + "','" + ArrayCalendar[item_C].StrMes + "','" + ArrayCalendar[item_C].Year + "','" + ArrayCalendar[item_C].StrDia + "')\"></input></td><td>" + ArrayCalendar[item_C].Dia + "</td><td>" + ArrayCalendar[item_C].StrDia + "</td><td id = 'T_P_" + ArrayCalendar[item_C].Year + "_" + ArrayCalendar[item_C].StrMes + "_" + ArrayCalendar[item_C].Dia + "'>" + ArrayCalendar[item_C].Programado + "</td></tr>";
                Cant_Dias_habiles = Cant_Dias_habiles + 1;
                Num_Mes = ArrayCalendar[item_C].Mes; 
            }
        }
        else {
            html_TG_turnos += "<tr class='Grid_Domingos' id= 'T_GTurno_" + ArrayCalendar[item_C].Year + "_" + ArrayCalendar[item_C].StrMes + "_" + ArrayCalendar[item_C].Dia + "'><td><input type ='radio' class= 'TurnoOpe' name='verG' onclick=\"Turno_Operacional('" + ArrayCalendar[item_C].Dia + "','" + ArrayCalendar[item_C].StrMes + "','" + ArrayCalendar[item_C].Year + "','" + ArrayCalendar[item_C].StrDia + "')\"></input></td><td>" + ArrayCalendar[item_C].Dia + "</td><td>" + ArrayCalendar[item_C].StrDia + "</td><td id = 'T_P_" + ArrayCalendar[item_C].Year + "_" + ArrayCalendar[item_C].StrMes + "_" + ArrayCalendar[item_C].Dia + "'>" + ArrayCalendar[item_C].Programado + "</td></tr>";
        }
    }

    html_TG_turnos += "</tbody></table>";
    $("#Container_OP_Turnos").html(html_TG_turnos);

    $(".TurnoOpe").click(function () {
    });

    $("#T_GTurno").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#L_habil").html(Cant_Dias_habiles);
    SumaTotal();
}


//abre la ventana emergente para la edicion o creacion de turno por dia
function Turno_Operacional(V_D, V_Mes, V_Year, V_StrDia) {

    Clear();

    for (item_B in ArrayCalendar) {
        if (V_D == ArrayCalendar[item_B].Dia) {
            if (ArrayCalendar[item_B].Programmado != "0") {


                if (ArrayCalendar[item_B].T_1 == "0")
                    $("#DDLTurno_1").val("-1");
                else
                    $("#DDLTurno_1").val(ArrayCalendar[item_B].T_1);

                $("#Txt_I1").val(ArrayCalendar[item_B].T_1_HI);
                $("#Txt_F1").val(ArrayCalendar[item_B].T_1_HF);

                if (ArrayCalendar[item_B].T_2 == "0")
                    $("#DDLTurno_2").val("-1");
                else
                    $("#DDLTurno_2").val(ArrayCalendar[item_B].T_2);

                $("#Txt_I2").val(ArrayCalendar[item_B].T_2_HI);
                $("#Txt_F2").val(ArrayCalendar[item_B].T_2_HF);

                if (ArrayCalendar[item_B].T_3 == "0")
                    $("#DDLTurno_3").val("-1");
                else
                    $("#DDLTurno_3").val(ArrayCalendar[item_B].T_3);

                $("#Txt_I3").val(ArrayCalendar[item_B].T_3_HI);
                $("#Txt_F3").val(ArrayCalendar[item_B].T_3_HF);

                if (ArrayCalendar[item_B].T_4 == "0")
                    $("#DDLTurno_4").val("-1");
                else
                    $("#DDLTurno_4").val(ArrayCalendar[item_B].T_4);

                $("#Txt_I4").val(ArrayCalendar[item_B].T_4_HI);
                $("#Txt_F4").val(ArrayCalendar[item_B].T_4_HF);

                if (ArrayCalendar[item_B].T_5 == "0")
                    $("#DDLTurno_5").val("-1");
                else
                    $("#DDLTurno_5").val(ArrayCalendar[item_B].T_5);

                $("#Txt_I5").val(ArrayCalendar[item_B].T_5_HI);
                $("#Txt_F5").val(ArrayCalendar[item_B].T_5_HF);

                if (ArrayCalendar[item_B].T_6 == "0")
                    $("#DDLTurno_6").val("-1");
                else
                    $("#DDLTurno_6").val(ArrayCalendar[item_B].T_6);

                $("#Txt_I6").val(ArrayCalendar[item_B].T_6_HI);
                $("#Txt_F6").val(ArrayCalendar[item_B].T_6_HF);

                $("#L_horas").html(ArrayCalendar[item_B].Programado);
            }
        }
    }

    if (State_Process == "EDITAR") {
        $("#DDLTurno_1").trigger("change");
        $("#DDLTurno_2").trigger("change");
        $("#DDLTurno_3").trigger("change");
        $("#DDLTurno_4").trigger("change");
        $("#DDLTurno_5").trigger("change");
        $("#DDLTurno_6").trigger("change");
    }

    $("#Dialog_Ope_turnos").dialog("option", "title", V_StrDia + ", " + V_D + " del mes " + V_Mes + " del " + V_Year);
    $("#Dialog_Ope_turnos").dialog("open");

    Key_ArrayCalendar = V_D + "|" + Num_Mes + "|" + V_Year;
}

//evento del boton salir
function x() {
    $("#dialog").dialog("close");
}

//guarda los tiempos programados
function BtnIngresar(Process) {

    //validar si esta lineal la seleccion de turnos
    var lineal = Turno_lineal();
    if (lineal != 0) {
        $("#dialog").dialog("option", "title", "Cuidado");
        $("#Mensaje_alert").text("el Turno " + lineal + " debe estar en secuencia lineal, seleccione turno!");
        $("#dialog").dialog("open");
        $("#DE").css("display", "none");
        $("#SE").css("display", "none");
        $("#WE").css("display", "block");
    }
    else {
        $("#Dialog_Ope_turnos").dialog("close");
        validarGuardarGrid();
    }
}

//funcion que validalos espacios en blanco y los combossin seleccionar para el guardadoa la grilla
function validarGuardarGrid() {
    var Val_D1 = $("#DDLTurno_1").val();
    if (Val_D1 == "-1")
        Val_D1 = 0;

    var Val_I1 = $("#Txt_I1").val();
    if (Val_I1 == "")
        Val_I1 = 0;

    var Val_F1 = $("#Txt_F1").val();
    if (Val_F1 == "")
        Val_F1 = 0;

    var Val_D2 = $("#DDLTurno_2").val();
    if (Val_D2 == "-1")
        Val_D2 = 0;

    var Val_I2 = $("#Txt_I2").val();
    if (Val_I2 == "")
        Val_I2 = 0;

    var Val_F2 = $("#Txt_F2").val();
    if (Val_F2 == "")
        Val_F2 = 0;

    var Val_D3 = $("#DDLTurno_3").val();
    if (Val_D3 == "-1")
        Val_D3 = 0;

    var Val_I3 = $("#Txt_I3").val();
    if (Val_I3 == "")
        Val_I3 = 0;

    var Val_F3 = $("#Txt_F3").val();
    if (Val_F3 == "")
        Val_F3 = 0;

    var Val_D4 = $("#DDLTurno_4").val();
    if (Val_D4 == "-1")
        Val_D4 = 0;

    var Val_I4 = $("#Txt_I4").val();
    if (Val_I4 == "")
        Val_I4 = 0;

    var Val_F4 = $("#Txt_F4").val();
    if (Val_F4 == "")
        Val_F4 = 0;

    var Val_D5 = $("#DDLTurno_5").val();
    if (Val_D5 == "-1")
        Val_D5 = 0;

    var Val_I5 = $("#Txt_I5").val();
    if (Val_I5 == "")
        Val_I5 = 0;

    var Val_F5 = $("#Txt_F5").val();
    if (Val_F5 == "")
        Val_F5 = 0;

    var Val_D6 = $("#DDLTurno_6").val();
    if (Val_D6 == "-1")
        Val_D6 = 0;

    var Val_I6 = $("#Txt_I6").val();
    if (Val_I6 == "")
        Val_I6 = 0;

    var Val_F6 = $("#Txt_F6").val();
    if (Val_F6 == "")
        Val_F6 = 0;

    var Split_Key = Key_ArrayCalendar.split("|");

    for (item in ArrayCalendar) {
        if (parseInt(Split_Key[0]) == ArrayCalendar[item].Dia && Split_Key[1] == ArrayCalendar[item].Mes && Split_Key[2] == ArrayCalendar[item].Year) {

            ArrayCalendar[item].T_1 = Val_D1;
            ArrayCalendar[item].T_1_HI = Val_I1;
            ArrayCalendar[item].T_1_HF = Val_F1;

            ArrayCalendar[item].T_2 = Val_D2;
            ArrayCalendar[item].T_2_HI = Val_I2;
            ArrayCalendar[item].T_2_HF = Val_F2;

            ArrayCalendar[item].T_3 = Val_D3;
            ArrayCalendar[item].T_3_HI = Val_I3;
            ArrayCalendar[item].T_3_HF = Val_F3;

            ArrayCalendar[item].T_4 = Val_D4;
            ArrayCalendar[item].T_4_HI = Val_I4;
            ArrayCalendar[item].T_4_HF = Val_F4;

            ArrayCalendar[item].T_5 = Val_D5;
            ArrayCalendar[item].T_5_HI = Val_I5;
            ArrayCalendar[item].T_5_HF = Val_F5;

            ArrayCalendar[item].T_6 = Val_D6;
            ArrayCalendar[item].T_6_HI = Val_I6;
            ArrayCalendar[item].T_6_HF = Val_F6;

            ArrayCalendar[item].Programado = $("#L_horas").html();
            $("#T_P_" + ArrayCalendar[item].Year + "_" + ArrayCalendar[item].StrMes + "_" + ArrayCalendar[item].Dia).html($("#L_horas").html());
        }
    }
    Clear();
    SumaTotal();
}

//revisa q el desbordamiento sea en linea no halla huecos en la seleccion
function Turno_lineal() {
    var lineal_pri;
    var lineal_Vacio = 0;

    for (var num = 6; num >= 1; num--) {
        if ($("#DDLTurno_" + num).val() != "-1") {
            lineal_pri = num;
            break;
        }
    }

    for (var num2 = lineal_pri; num2 >= 1; num2--) {
        if ($("#DDLTurno_" + num2).val() == "-1") {
            lineal_Vacio = num2;
            break;
        }
    }

    return lineal_Vacio;
}

function BtnSave(Process) {

    for (item in ArrayCalendar) {
        var Mes_2;
        var sumaMes = parseInt(ArrayCalendar[item].Mes) + 1;

        var LMes = sumaMes.toString();
        if (LMes.length == 1)
            Mes_2 = "0" + sumaMes.toString();
        else
            Mes_2 = sumaMes.toString();

        ArrayCalendar[item].Ano_Mes_ID = ArrayCalendar[item].Year + Mes_2;
    }

    transacionAjax_Save(Process);

}

//completa Array con mes y año
function Complete_Edit_Calendar() {

    for (item_c in ArrayCalendar) {
        for (item in CantDia_X_Mes) {
            if (ArrayCalendar[item_c].StrMes == CantDia_X_Mes[item][0]) {
                ArrayCalendar[item_c].Mes = item;
                var StrYear = ArrayCalendar[item_c].Ano_Mes_ID;
                ArrayCalendar[item_c].Year = StrYear.substring(0, 4);
            }
        }
    }
}

//funcion que limpia el dialog de operacionalidad
function Clear() {
    for (var num = 1; num <= 6; num++) {
        $("#DDLTurno_" + num).val("-1");
        $("#L_I" + num).html("");
        $("#L_F" + num).html("");
        $("#L_" + num).html("0");
        $("#Txt_I" + num).val("");
        $("#Txt_F" + num).val("");
    }
    $("#L_horas").html("0");

}
