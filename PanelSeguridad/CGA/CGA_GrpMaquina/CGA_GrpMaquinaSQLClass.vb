﻿Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class CGA_GrpMaquinaSQLClass

#Region "CRUD"

    ''' <summary>
    ''' creala consulta para la tabla GrpMaquina parametrizada (READ)
    ''' </summary>
    ''' <param name="vp_S_Filtro"></param>
    ''' <param name="vp_S_Opcion"></param>
    ''' <param name="vp_S_Contenido"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Read_AllGrpMaquina(ByVal vp_S_Filtro As String, ByVal vp_S_Opcion As String, ByVal vp_S_Contenido As String)

        Dim ObjListGrpMaquina As New List(Of CGA_GrpMaquinaClass)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        If vp_S_Filtro = "N" And vp_S_Opcion = "ALL" Then
            sql.Append("SELECT GRP_Maquina_ID, GRP_Descripcion, GRP_FechaActualizacion, GRP_Usuario FROM CGA_GRP_MAQUINAS")
        Else

            If vp_S_Contenido = "ALL" Then
                sql.Append("SELECT GRP_Maquina_ID, GRP_Descripcion, GRP_FechaActualizacion, GRP_Usuario FROM CGA_GRP_MAQUINAS")
            Else
                sql.Append("SELECT GRP_Maquina_ID, GRP_Descripcion, GRP_FechaActualizacion, GRP_Usuario FROM CGA_GRP_MAQUINAS " & _
                      "WHERE " & vp_S_Opcion & " like '%" & vp_S_Contenido & "%'")
            End If
        End If

        StrQuery = sql.ToString

        ObjListGrpMaquina = conex.ObjLoad_All(StrQuery, "GrpMaquina")

        Return ObjListGrpMaquina

    End Function

    ''' <summary>
    ''' funcion que crea el query para la insercion de nuevo GrpMaquina (INSERT)
    ''' </summary>
    ''' <param name="vp_Obj_GrpMaquina"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertGrpMaquina(ByVal vp_Obj_GrpMaquina As CGA_GrpMaquinaClass)

        Dim conex As New Conector
        Dim Result As String
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQueryID As String = ""
        Dim StrQuery As String = ""

        sql.AppendLine("INSERT CGA_GRP_MAQUINAS (" & _
            "GRP_Maquina_ID," & _
            "GRP_Descripcion," & _
            "GRP_FechaActualizacion," & _
            "GRP_Usuario" & _
            ")")
        sql.AppendLine("VALUES (")
        sql.AppendLine("'" & vp_Obj_GrpMaquina.Maquina_ID & "',")
        sql.AppendLine("'" & vp_Obj_GrpMaquina.Descripcion & "',")
        sql.AppendLine("'" & vp_Obj_GrpMaquina.FechaActualizacion & "',")
        sql.AppendLine("'" & vp_Obj_GrpMaquina.Usuario & "' ) ")

        StrQuery = sql.ToString

        Result = conex.StrInsert_and_Update_All(StrQuery)

        Return Result

    End Function

    ''' <summary>
    ''' funcion que crea el query para la modificacion del GrpMaquina (UPDATE)
    ''' </summary>
    ''' <param name="vp_Obj_GrpMaquina"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateGrpMaquina(ByVal vp_Obj_GrpMaquina As CGA_GrpMaquinaClass)

        Dim conex As New Conector
        Dim Result As String
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQuery As String = ""
        sql.AppendLine("UPDATE CGA_GRP_MAQUINAS SET " & _
                       " GRP_Descripcion ='" & vp_Obj_GrpMaquina.Descripcion & "', " & _
                       " GRP_FechaActualizacion ='" & vp_Obj_GrpMaquina.FechaActualizacion & "', " & _
                       " GRP_Usuario ='" & vp_Obj_GrpMaquina.Usuario & "' " & _
                       " WHERE GRP_Maquina_ID = '" & vp_Obj_GrpMaquina.Maquina_ID & "'")

        StrQuery = sql.ToString

        Result = conex.StrInsert_and_Update_All(StrQuery)

        Return Result

    End Function

    ''' <summary>
    ''' funcion que crea el query para la eliminacion del GrpMaquina (DELETE)
    ''' </summary>
    ''' <param name="vp_Obj_GrpMaquina"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EraseGrpMaquina(ByVal vp_Obj_GrpMaquina As CGA_GrpMaquinaClass)

        Dim conex As New Conector
        Dim Result As String = ""
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQuery As String
        Dim SQL_general As New GeneralSQLClass
        Dim ExistMaquina As String

        ExistMaquina = SQL_general.ReadExist("CGA_MAQUINA", vp_Obj_GrpMaquina.Maquina_ID, "M_GRPMaquina_ID", "")

        If ExistMaquina = "0" Then

            sql.AppendLine("DELETE CGA_GRP_MAQUINAS WHERE GRP_Maquina_ID = '" & vp_Obj_GrpMaquina.Maquina_ID & "'")
            StrQuery = sql.ToString
            Result = conex.StrInsert_and_Update_All(StrQuery)

        Else
            Result = "Exist_M"

        End If

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

        sql.Append(" SELECT T_IndexColumna As ID, T_Traductor As descripcion FROM TC_TABLAS " & _
                   " WHERE T_Tabla = '" & vp_S_Table & "' AND T_Param = '1' ")
        StrQuery = sql.ToString

        ObjListDroplist = conex.ObjLoad_All(StrQuery, "Droplist_General")

        Return ObjListDroplist


    End Function

#End Region

#Region "CARGAR LISTAS"

    ''' <summary>
    ''' funcion que trae el listado de GrpMaquina para armar la tabla
    ''' </summary>
    ''' <param name="vp_S_StrQuery"></param>
    ''' <param name="vg_S_StrConexion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function listGrpMaquina(ByVal vp_S_StrQuery As String, ByVal vg_S_StrConexion As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjListGrpMaquina As New List(Of CGA_GrpMaquinaClass)

        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()

        'recorremos la consulta por la cantidad de datos en la BD
        While ReadConsulta.Read

            Dim objGrpMaquina As New CGA_GrpMaquinaClass
            'cargamos datos sobre el objeto de login
            objGrpMaquina.Maquina_ID = ReadConsulta.GetValue(0)
            objGrpMaquina.Descripcion = ReadConsulta.GetString(1)
            objGrpMaquina.FechaActualizacion = ReadConsulta.GetString(2)
            objGrpMaquina.Usuario = ReadConsulta.GetString(3)

            'agregamos a la lista
            ObjListGrpMaquina.Add(objGrpMaquina)

        End While

        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjListGrpMaquina

    End Function


#End Region


End Class
