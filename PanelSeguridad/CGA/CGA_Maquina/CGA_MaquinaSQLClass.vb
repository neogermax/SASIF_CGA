﻿Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class CGA_MaquinaSQLClass

#Region "CRUD"

    ''' <summary>
    ''' creala consulta para la tabla Maquina parametrizada (READ)
    ''' </summary>
    ''' <param name="vp_S_Filtro"></param>
    ''' <param name="vp_S_Opcion"></param>
    ''' <param name="vp_S_Contenido"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Read_AllMaquina(ByVal vp_S_Filtro As String, ByVal vp_S_Opcion As String, ByVal vp_S_Contenido As String)

        Dim ObjListMaquina As New List(Of CGA_MaquinaClass)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        If vp_S_Filtro = "N" And vp_S_Opcion = "ALL" Then
            sql.Append(" SELECT M_Maquina_ID, M_Descripcion, " & _
                       " M_Centro_ID, M_GRPMaquina_ID, " & _
                       " M_CentroCosto_ID, M_FechaActualizacion, " & _
                       " M_Usuario, " & _
                       " C.C_Descripcion AS DesCentro, " & _
                       " GPR.GRP_Descripcion AS DesGPRMaquina,  " & _
                       " CC.CC_Descripcion AS DesCCosto  " & _
                       " FROM CGA_MAQUINA M " & _
                       " INNER JOIN CGA_CENTROS C ON C.C_Centro_ID = M.M_Centro_ID " & _
                       " INNER JOIN CGA_GRP_MAQUINAS GPR ON GPR.GRP_Maquina_ID = M.M_GRPMaquina_ID" & _
                       " INNER JOIN CGA_CENTRO_COSTO CC ON CC.CC_CentroCosto_ID = M.M_CentroCosto_ID")
        Else

            If vp_S_Contenido = "ALL" Then
                sql.Append(" SELECT M_Maquina_ID, M_Descripcion, " & _
                       " M_Centro_ID, M_GRPMaquina_ID, " & _
                       " M_CentroCosto_ID, M_FechaActualizacion, " & _
                       " M_Usuario, " & _
                       " C.C_Descripcion AS DesCentro, " & _
                       " GPR.GRP_Descripcion AS DesGPRMaquina,  " & _
                       " CC.CC_Descripcion AS DesCCosto  " & _
                       " FROM CGA_MAQUINA M " & _
                       " INNER JOIN CGA_CENTROS C ON C.C_Centro_ID = M.M_Centro_ID " & _
                       " INNER JOIN CGA_GRP_MAQUINAS GPR ON GPR.GRP_Maquina_ID = M.M_GRPMaquina_ID" & _
                       " INNER JOIN CGA_CENTRO_COSTO CC ON CC.CC_CentroCosto_ID = M.M_CentroCosto_ID")
            Else
                sql.Append(" SELECT M_Maquina_ID, M_Descripcion, " & _
                       " M_Centro_ID, M_GRPMaquina_ID, " & _
                       " M_CentroCosto_ID, M_FechaActualizacion, " & _
                       " M_Usuario, " & _
                       " C.C_Descripcion AS DesCentro, " & _
                       " GPR.GRP_Descripcion AS DesGPRMaquina,  " & _
                       " CC.CC_Descripcion AS DesCCosto " & _
                       " FROM CGA_MAQUINA M " & _
                       " INNER JOIN CGA_CENTROS C ON C.C_Centro_ID = M.M_Centro_ID " & _
                       " INNER JOIN CGA_GRP_MAQUINAS GPR ON GPR.GRP_Maquina_ID = M.M_GRPMaquina_ID" & _
                       " INNER JOIN CGA_CENTRO_COSTO CC ON CC.CC_CentroCosto_ID = M.M_CentroCosto_ID" & _
                           " WHERE " & vp_S_Opcion & " like '%" & vp_S_Contenido & "%'")
            End If
        End If

        StrQuery = sql.ToString

        ObjListMaquina = conex.ObjLoad_All(StrQuery, "Maquina")

        Return ObjListMaquina

    End Function

    ''' <summary>
    ''' funcion que crea el query para la insercion de la nueva Maquina (INSERT)
    ''' </summary>
    ''' <param name="vp_Obj_Maquina"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertMaquina(ByVal vp_Obj_Maquina As CGA_MaquinaClass)

        Dim conex As New Conector
        Dim Result As String
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQueryID As String = ""
        Dim StrQuery As String = ""

        sql.AppendLine("INSERT CGA_MAQUINA (" & _
            "M_Maquina_ID," & _
            "M_Descripcion," & _
            "M_Centro_ID," & _
            "M_GRPMaquina_ID," & _
            "M_CentroCosto_ID," & _
            "M_FechaActualizacion," & _
            "M_Usuario" & _
            ")")
        sql.AppendLine("VALUES (")
        sql.AppendLine("'" & vp_Obj_Maquina.Maquina_ID & "',")
        sql.AppendLine("'" & vp_Obj_Maquina.Descripcion & "',")
        sql.AppendLine("'" & vp_Obj_Maquina.Centro_ID & "',")
        sql.AppendLine("'" & vp_Obj_Maquina.GRPMaquina_ID & "',")
        sql.AppendLine("'" & vp_Obj_Maquina.CentroCosto_ID & "',")
        sql.AppendLine("'" & vp_Obj_Maquina.FechaActualizacion & "',")
        sql.AppendLine("'" & vp_Obj_Maquina.Usuario & "' ) ")

        StrQuery = sql.ToString

        Result = conex.StrInsert_and_Update_All(StrQuery)

        Return Result

    End Function

    ''' <summary>
    ''' funcion que crea el query para la modificacion de la Maquina  (UPDATE)
    ''' </summary>
    ''' <param name="vp_Obj_Maquina"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateMaquina(ByVal vp_Obj_Maquina As CGA_MaquinaClass)

        Dim conex As New Conector
        Dim Result As String
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQueryID As String = ""
        Dim StrQuery As String = ""
        sql.AppendLine("UPDATE CGA_MAQUINA SET " & _
                       " M_Descripcion ='" & vp_Obj_Maquina.Descripcion & "', " & _
                       " M_Centro_ID =" & vp_Obj_Maquina.Centro_ID & ", " & _
                       " M_GRPMaquina_ID ='" & vp_Obj_Maquina.GRPMaquina_ID & "', " & _
                       " M_CentroCosto_ID ='" & vp_Obj_Maquina.CentroCosto_ID & "', " & _
                       " M_FechaActualizacion ='" & vp_Obj_Maquina.FechaActualizacion & "', " & _
                       " M_Usuario ='" & vp_Obj_Maquina.Usuario & "' " & _
                       " WHERE M_Maquina_ID = '" & vp_Obj_Maquina.Maquina_ID & "'")

        StrQuery = sql.ToString

        Result = conex.StrInsert_and_Update_All(StrQuery)

        Return Result

    End Function

    ''' <summary>
    ''' funcion que crea el query para la eliminacion de la turno (DELETE)
    ''' </summary>
    ''' <param name="vp_Obj_Maquina"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EraseMaquina(ByVal vp_Obj_Maquina As CGA_MaquinaClass)

        Dim conex As New Conector
        Dim Result As String = ""
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim StrQuery As String
        Dim SQL_general As New GeneralSQLClass

        sql.AppendLine("DELETE CGA_MAQUINA WHERE M_Maquina_ID = '" & vp_Obj_Maquina.Maquina_ID & "'")
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

        sql.Append(" SELECT T_IndexColumna As ID, T_Traductor As descripcion FROM TC_TABLAS " & _
                   " WHERE T_Tabla = '" & vp_S_Table & "' AND T_Param = '1' ")
        StrQuery = sql.ToString

        ObjListDroplist = conex.ObjLoad_All(StrQuery, "Droplist_General")

        Return ObjListDroplist


    End Function

    ''' <summary>
    ''' funcion que consulta la tabla centro para el DDl Centro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadCharge_DDL_Centro()

        Dim ObjListDroplist As New List(Of Droplist_Class)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        sql.Append(" SELECT C_Centro_ID AS ID, C_Descripcion AS descripcion FROM CGA_CENTROS ")
        StrQuery = sql.ToString

        ObjListDroplist = conex.ObjLoad_All(StrQuery, "Droplist_General")

        Return ObjListDroplist

    End Function

    ''' <summary>
    ''' funcion que consulta la tabla Grupo de maquinas para el DDl GrpMaquinas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadCharge_DDL_GrpMaquinas()

        Dim ObjListDroplist As New List(Of Droplist_Class)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        sql.Append(" SELECT GRP_Maquina_ID AS ID, GRP_Descripcion AS descripcion FROM CGA_GRP_MAQUINAS ")
        StrQuery = sql.ToString

        ObjListDroplist = conex.ObjLoad_All(StrQuery, "Droplist_General")

        Return ObjListDroplist

    End Function

    ''' <summary>
    ''' funcion que consulta la tabla centro de costo para el DDl CCosto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadCharge_DDL_CCosto()

        Dim ObjListDroplist As New List(Of Droplist_Class)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder

        sql.Append(" SELECT CC_CentroCosto_ID AS ID, CC_Descripcion AS descripcion FROM CGA_CENTRO_COSTO ")
        StrQuery = sql.ToString

        ObjListDroplist = conex.ObjLoad_All(StrQuery, "Droplist_General")

        Return ObjListDroplist

    End Function


#End Region

#Region "CARGAR LISTAS"

    ''' <summary>
    ''' funcion que trae el listado de Maquina para armar la tabla
    ''' </summary>
    ''' <param name="vp_S_StrQuery"></param>
    ''' <param name="vg_S_StrConexion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function listMaquina(ByVal vp_S_StrQuery As String, ByVal vg_S_StrConexion As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjListMaquina As New List(Of CGA_MaquinaClass)

        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()

        'recorremos la consulta por la cantidad de datos en la BD
        While ReadConsulta.Read

            Dim objMaquina As New CGA_MaquinaClass
            'cargamos datos sobre el objeto de login
            objMaquina.Maquina_ID = ReadConsulta.GetValue(0)
            objMaquina.Descripcion = ReadConsulta.GetString(1)
            objMaquina.Centro_ID = ReadConsulta.GetValue(2)
            objMaquina.GRPMaquina_ID = ReadConsulta.GetValue(3)
            objMaquina.CentroCosto_ID = ReadConsulta.GetValue(4)
            objMaquina.FechaActualizacion = ReadConsulta.GetString(5)
            objMaquina.Usuario = ReadConsulta.GetString(6)
            objMaquina.DesCentro = ReadConsulta.GetString(7)
            objMaquina.DesGRPMaquina = ReadConsulta.GetString(8)
            objMaquina.DesCentroCosto = ReadConsulta.GetString(9)

            'agregamos a la lista
            ObjListMaquina.Add(objMaquina)

        End While

        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjListMaquina

    End Function

#End Region


End Class
