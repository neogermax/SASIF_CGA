Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class OpeTurnosSQLClass

#Region "CRUD"

    ''' <summary>
    ''' funcion que consulta en la BD el turno programado
    ''' </summary>
    ''' <param name="ObjTurno_Ope"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadTurnosProg(ByVal ObjTurno_Ope As OpeTurnosClass)

        Dim conex As New Conector
        Dim ruta_conex As String = conex.typeConexion()
        Dim ListObjTurno_Op As New List(Of OpeTurnosClass)

        Dim Result As String
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQueryID As String = ""
        Dim StrQuery As String = ""

        sql.AppendLine("SELECT " & _
           "TO_Maquina_ID," & _
           "TO_Ano_Mes_ID," & _
           "TO_Dia_ID," & _
           "TO_StrDia," & _
           "TO_StrMes," & _
           "TO_Day_Type," & _
           "TO_Cod_1," & _
           "TO_HI_1," & _
           "TO_HF_1," & _
           "TO_Cod_2," & _
           "TO_HI_2," & _
           "TO_HF_2," & _
           "TO_Cod_3," & _
           "TO_HI_3," & _
           "TO_HF_3," & _
           "TO_Cod_4," & _
           "TO_HI_4," & _
           "TO_HF_4," & _
           "TO_Cod_5," & _
           "TO_HI_5," & _
           "TO_HF_5," & _
           "TO_Cod_6," & _
           "TO_HI_6," & _
           "TO_HF_6," & _
           "TO_Tiempo_Programado," & _
           "TO_FechaActualizacion," & _
           "TO_Usuario " & _
           "FROM CGA_TURNOS_OPERACIONAL " & _
           "WHERE  TO_Maquina_ID = '" & ObjTurno_Ope.PuestoTrabajo_ID & "' " & _
           "AND TO_Ano_Mes_ID = '" & ObjTurno_Ope.Ano_Mes_ID & "'")

        StrQuery = sql.ToString

        ListObjTurno_Op = ListTurnos_Prog(StrQuery, ruta_conex)

        Return ListObjTurno_Op


    End Function

    ''' <summary>
    ''' funcion que inserta en la BD el turno programado
    ''' </summary>
    ''' <param name="ObjTurno_Ope"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Insert(ByVal ObjTurno_Ope As OpeTurnosClass)

        Dim conex As New Conector
        Dim Result As String
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQueryID As String = ""
        Dim StrQuery As String = ""

        sql.AppendLine("INSERT INTO CGA_TURNOS_OPERACIONAL (" & _
           "TO_Maquina_ID," & _
           "TO_Ano_Mes_ID," & _
           "TO_Dia_ID," & _
           "TO_StrDia," & _
           "TO_StrMes," & _
           "TO_Day_Type," & _
           "TO_Cod_1," & _
           "TO_HI_1," & _
           "TO_HF_1," & _
           "TO_Cod_2," & _
           "TO_HI_2," & _
           "TO_HF_2," & _
           "TO_Cod_3," & _
           "TO_HI_3," & _
           "TO_HF_3," & _
           "TO_Cod_4," & _
           "TO_HI_4," & _
           "TO_HF_4," & _
           "TO_Cod_5," & _
           "TO_HI_5," & _
           "TO_HF_5," & _
           "TO_Cod_6," & _
           "TO_HI_6," & _
           "TO_HF_6," & _
           "TO_Tiempo_Programado," & _
           "TO_FechaActualizacion," & _
           "TO_Usuario" & _
           ")")
        sql.AppendLine("VALUES (")
        sql.AppendLine("'" & ObjTurno_Ope.PuestoTrabajo_ID & "',")
        sql.AppendLine("'" & ObjTurno_Ope.Ano_Mes_ID & "',")
        sql.AppendLine("'" & ObjTurno_Ope.Dia & "',")
        sql.AppendLine("'" & ObjTurno_Ope.StrDia & "',")
        sql.AppendLine("'" & ObjTurno_Ope.StrMes & "',")
        sql.AppendLine("'" & ObjTurno_Ope.Day_Type & "',")

        sql.AppendLine("'" & ObjTurno_Ope.T_1 & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_1_HI & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_1_HF & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_2 & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_2_HI & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_2_HF & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_3 & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_3_HI & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_3_HF & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_4 & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_4_HI & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_4_HF & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_5 & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_5_HI & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_5_HF & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_6 & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_6_HI & "',")
        sql.AppendLine("'" & ObjTurno_Ope.T_6_HF & "',")
        sql.AppendLine("'" & ObjTurno_Ope.Programado & "',")

        sql.AppendLine("'" & ObjTurno_Ope.FechaActualizacion & "',")
        sql.AppendLine("'" & ObjTurno_Ope.Usuario & "' ) ")

        StrQuery = sql.ToString

        Result = conex.StrInsert_and_Update_All(StrQuery)

        Return Result

    End Function

    ''' <summary>
    ''' funcion que crea el query para la eliminacion de los turnos programados (DELETE)
    ''' </summary>
    ''' <param name="vp_ObjTurno_Op"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(ByVal vp_ObjTurno_Op As OpeTurnosClass)

        Dim conex As New Conector
        Dim Result As String = ""
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQuery As String
        Dim SQL_general As New GeneralSQLClass

        sql.AppendLine("DELETE CGA_TURNOS_OPERACIONAL WHERE TO_Maquina_ID = '" & vp_ObjTurno_Op.PuestoTrabajo_ID & "' AND TO_Ano_Mes_ID ='" & vp_ObjTurno_Op.Ano_Mes_ID & "'")
        StrQuery = sql.ToString
        Result = conex.StrInsert_and_Update_All(StrQuery)

        Return Result

    End Function


#End Region

#Region "CONSULTAS DROP LIST"

    ''' <summary>
    ''' crea la consulta para cargar el combo
    ''' </summary>
    ''' <param name="vp_S_Table"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadCharge_DropList(ByVal vp_S_Table As String)

        Dim ObjListDroplist As New List(Of Droplist_Class)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        sql.Append(" SELECT M_Maquina_ID As ID, M_Descripcion As descripcion FROM CGA_MAQUINA ")
        StrQuery = sql.ToString

        ObjListDroplist = conex.ObjLoad_All(StrQuery, "Droplist_General")

        Return ObjListDroplist


    End Function


    ''' <summary>
    ''' crea la consulta para cargar el combo
    ''' </summary>
    ''' <param name="vp_S_Table"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadCharge_DropList_Turno(ByVal vp_S_Table As String)

        Dim ObjListDroplist As New List(Of Droplist_Class)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        sql.Append(" SELECT T_Turno_ID As ID,T_Descripcion As descripcion FROM CGA_TURNOS ")
        StrQuery = sql.ToString

        ObjListDroplist = conex.ObjLoad_All(StrQuery, "Droplist_General")

        Return ObjListDroplist

    End Function


#End Region

#Region "OTRAS CONSULTAS"

    ''' <summary>
    ''' funcion que consulta el centro y planta del puesto de trabajo o maquina
    ''' </summary>
    ''' <param name="vp_S_ID_maquina"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SearchCentroPlanta(ByVal vp_S_ID_maquina As String)

        Dim ObjlistOpeTurnos As New List(Of OpeTurnosClass)
        Dim StrQuery As String = ""
        Dim conex As New Conector
        Dim ruta_conex As String = conex.typeConexion()

        Dim sql As New StringBuilder

        sql.Append(" SELECT C_Centro_ID, C_Descripcion, C_Descripcion_Planta FROM CGA_CENTROS C " & _
                   " INNER JOIN CGA_MAQUINA M ON M.M_Centro_ID = C.C_Centro_ID " & _
                   " WHERE M_Maquina_ID = '" & vp_S_ID_maquina & "'")
        StrQuery = sql.ToString


        ObjlistOpeTurnos = ListCentroMaquina(StrQuery, ruta_conex)

        Return ObjlistOpeTurnos

    End Function

    ''' <summary>
    ''' funcion que consulta todo los festivos del año
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AllFestivos()

        Dim ObjlistFestivos As New List(Of CGA_FestivosClass)
        Dim StrQuery As String = ""
        Dim conex As New Conector
        Dim ruta_conex As String = conex.typeConexion()

        Dim sql As New StringBuilder

        sql.Append("SELECT F_Año, SUBSTRING(convert(nvarchar(4),F_Mes_Dia), 1, 2)as Mes, SUBSTRING(convert(nvarchar(4),F_Mes_Dia), 3, 4)as Dia  FROM CGA_Festivos ")

        StrQuery = sql.ToString

        ObjlistFestivos = listFestivos(StrQuery, ruta_conex)

        Return ObjlistFestivos
    End Function

    ''' <summary>
    ''' funcion que consulta los tiempos de los turnos en la BD
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Allturno()

        Dim ObjlistTurno As New List(Of CGA_TurnosClass)

        Dim StrQuery As String = ""
        Dim conex As New Conector
        Dim ruta_conex As String = conex.typeConexion()

        Dim sql As New StringBuilder

        sql.Append("SELECT T_Turno_ID,T_HoraInicio,T_Tiempo FROM CGA_TURNOS ")

        StrQuery = sql.ToString

        ObjlistTurno = listTurnos(StrQuery, ruta_conex)

        Return ObjlistTurno

    End Function

    ''' <summary>
    ''' funcion que consulta si eseditar o crear turnos
    ''' </summary>
    ''' <param name="ObjTurno_OP"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ReadExist(ByVal ObjTurno_OP As OpeTurnosClass)

        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        sql.AppendLine("SELECT COUNT(1) FROM CGA_TURNOS_OPERACIONAL" & _
                       " WHERE  TO_Maquina_ID = '" & ObjTurno_OP.PuestoTrabajo_ID & _
                       "' AND TO_Ano_Mes_ID ='" & ObjTurno_OP.Ano_Mes_ID & "'")

        StrQuery = sql.ToString

        Dim Result As String = conex.IDis(StrQuery)

        Return Result

    End Function


#End Region

#Region "CARGAR LISTAS"

    ''' <summary>
    ''' funcion que trae los datos de centro y planta por puesto de trabajo o maquina
    ''' </summary>
    ''' <param name="vp_S_StrQuery"></param>
    ''' <param name="vg_S_StrConexion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCentroMaquina(ByVal vp_S_StrQuery As String, ByVal vg_S_StrConexion As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjlistOpeTurnos As New List(Of OpeTurnosClass)

        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()


        While ReadConsulta.Read

            Dim objOpeTurnos As New OpeTurnosClass
            'cargamos datos sobre el objeto de login
            objOpeTurnos.Centro_ID = ReadConsulta.GetValue(0)
            objOpeTurnos.Descripcion = ReadConsulta.GetString(1)
            objOpeTurnos.Descripcion_Planta = ReadConsulta.GetString(2)

            'agregamos a la lista
            ObjlistOpeTurnos.Add(objOpeTurnos)

        End While

        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjlistOpeTurnos

    End Function

    ''' <summary>
    ''' funcion que trae los datos de los festivos
    ''' </summary>
    ''' <param name="vp_S_StrQuery"></param>
    ''' <param name="vg_S_StrConexion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function listFestivos(ByVal vp_S_StrQuery As String, ByVal vg_S_StrConexion As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjListFestivos As New List(Of CGA_FestivosClass)

        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()

        'recorremos la consulta por la cantidad de datos en la BD
        While ReadConsulta.Read

            Dim objFestivos As New CGA_FestivosClass
            'cargamos datos sobre el objeto de login
            objFestivos.Year = ReadConsulta.GetValue(0)
            objFestivos.StrMes = ReadConsulta.GetValue(1)
            objFestivos.StrDia = ReadConsulta.GetValue(2)

            'agregamos a la lista
            ObjListFestivos.Add(objFestivos)

        End While

        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjListFestivos

    End Function

    Public Function listTurnos(ByVal vp_S_StrQuery As String, ByVal vg_S_StrConexion As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjListTurno As New List(Of CGA_TurnosClass)

        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()

        'recorremos la consulta por la cantidad de datos en la BD
        While ReadConsulta.Read

            Dim objTurno As New CGA_TurnosClass
            'cargamos datos sobre el objeto de login
            objTurno.Turno_ID = ReadConsulta.GetValue(0)
            objTurno.HoraInicio = ReadConsulta.GetValue(1)
            objTurno.Tiempo = ReadConsulta.GetValue(2)

            'agregamos a la lista
            ObjListTurno.Add(objTurno)

        End While

        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjListTurno

    End Function

    Public Function ListTurnos_Prog(ByVal vp_S_StrQuery As String, ByVal vg_S_StrConexion As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjListTurno_Prog As New List(Of OpeTurnosClass)

        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()

        'recorremos la consulta por la cantidad de datos en la BD
        While ReadConsulta.Read

            Dim objTurno_Prog As New OpeTurnosClass

            objTurno_Prog.PuestoTrabajo_ID = ReadConsulta.GetValue(0)
            objTurno_Prog.Ano_Mes_ID = ReadConsulta.GetValue(1)
            objTurno_Prog.Dia = ReadConsulta.GetValue(2)
            objTurno_Prog.StrDia = ReadConsulta.GetValue(3)
            objTurno_Prog.StrMes = ReadConsulta.GetValue(4)
            objTurno_Prog.Day_Type = ReadConsulta.GetValue(5)

            objTurno_Prog.T_1 = ReadConsulta.GetValue(6)
            objTurno_Prog.T_1_HI = ReadConsulta.GetValue(7)
            objTurno_Prog.T_1_HF = ReadConsulta.GetValue(8)

            objTurno_Prog.T_2 = ReadConsulta.GetValue(9)
            objTurno_Prog.T_2_HI = ReadConsulta.GetValue(10)
            objTurno_Prog.T_2_HF = ReadConsulta.GetValue(11)

            objTurno_Prog.T_3 = ReadConsulta.GetValue(12)
            objTurno_Prog.T_3_HI = ReadConsulta.GetValue(13)
            objTurno_Prog.T_3_HF = ReadConsulta.GetValue(14)

            objTurno_Prog.T_4 = ReadConsulta.GetValue(15)
            objTurno_Prog.T_4_HI = ReadConsulta.GetValue(16)
            objTurno_Prog.T_4_HF = ReadConsulta.GetValue(17)

            objTurno_Prog.T_5 = ReadConsulta.GetValue(18)
            objTurno_Prog.T_5_HI = ReadConsulta.GetValue(19)
            objTurno_Prog.T_5_HF = ReadConsulta.GetValue(20)

            objTurno_Prog.T_6 = ReadConsulta.GetValue(21)
            objTurno_Prog.T_6_HI = ReadConsulta.GetValue(22)
            objTurno_Prog.T_6_HF = ReadConsulta.GetValue(23)

            objTurno_Prog.Programado = ReadConsulta.GetValue(24)
            objTurno_Prog.FechaActualizacion = ReadConsulta.GetValue(25)
            objTurno_Prog.Usuario = ReadConsulta.GetValue(26)

            ObjListTurno_Prog.Add(objTurno_Prog)

        End While

        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjListTurno_Prog

    End Function

#End Region


End Class
