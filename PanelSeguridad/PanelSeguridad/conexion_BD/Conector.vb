Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class Conector
#Region "variables globales"
    'Dim vg_S_Ruta As String = "C:\Documents and Settings\analista01\CGA.sdf"
    Dim vg_S_Ruta As String = "D:\SASIF\desarrollo 1\CGA.sdf"
    Dim vg_S_Pass As String = "admin"
    'conexion a la BD
    Dim vg_S_StrConexion As String = "Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;Data Source=" & vg_S_Ruta & ";SSCE:Database Password=" & vg_S_Pass & ";"
#End Region

    ''' <summary>
    ''' funcion generica para la consulta de cualquier tabla sin condiciones (sin where)
    ''' </summary>
    ''' <param name="vp_S_StrQuery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObjLoad_All(ByVal vp_S_StrQuery As String, ByVal vp_S_Proccess As String)

        Try
            Select Case vp_S_Proccess

                Case "Login"

                    Dim ObjListLogin As New List(Of LoginClass)
                    ObjListLogin = ListLogin(vp_S_StrQuery)
                    Return ObjListLogin

                Case "Menu"

                    Dim ObjListMenu As New List(Of MenuClass)
                    ObjListMenu = listMenu(vp_S_StrQuery)
                    Return ObjListMenu

            End Select

        Catch ex As Exception

            Select Case vp_S_Proccess

                Case "Login"

                    Dim ObjListLogin As New List(Of LoginClass)
                    Dim objUser As New LoginClass
                    objUser.Name = "Error consulta"
                    ObjListLogin.Add(objUser)
                    'retornamos el error 
                    Return ObjListLogin

                Case "Menu"

                    Dim ObjListMenu As New List(Of MenuClass)
                    Dim objUser As New MenuClass
                    objUser.Nombre = "Error consulta"
                    ObjListMenu.Add(objUser)
                    Return ObjListMenu

            End Select

        End Try

    End Function
    ''' <summary>
    ''' trae ellistado solicitado por login
    ''' </summary>
    ''' <param name="vp_S_StrQuery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLogin(ByVal vp_S_StrQuery As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjListLogin As New List(Of LoginClass)

        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()
        'recorremos la consulta por la cantidad de datos en la BD
        While ReadConsulta.Read

            Dim objUser As New LoginClass
            'cargamos datos sobre el objeto de login
            objUser.Name = ReadConsulta.GetString(0)
            objUser.Password = ReadConsulta.GetString(1)
            'agregamos a la lista
            ObjListLogin.Add(objUser)

        End While
        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjListLogin
    End Function

    Public Function listMenu(ByVal vp_S_StrQuery As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        objConexBD = New OleDbConnection(vg_S_StrConexion)
        Dim ReadConsulta As OleDbDataReader = Nothing

        objcmd = objConexBD.CreateCommand

        Dim ObjListMenu As New List(Of MenuClass)
        'abrimos conexion
        objConexBD.Open()
        'cargamos consulta
        objcmd.CommandText = vp_S_StrQuery
        'ejecutamos consulta
        ReadConsulta = objcmd.ExecuteReader()
        'recorremos la consulta por la cantidad de datos en la BD
        While ReadConsulta.Read

            Dim objMenu As New MenuClass
            'cargamos datos sobre el objeto de login
            objMenu.Nombre = ReadConsulta.GetString(0)
            objMenu.EstadoUsuario = ReadConsulta.GetString(1)
            objMenu.IDRol = ReadConsulta.GetString(2)
            objMenu.DescripcionRol = ReadConsulta.GetString(3)
            objMenu.Sigla = ReadConsulta.GetString(4)
            objMenu.IDOpcionRol = ReadConsulta.GetString(5)
            objMenu.Consecutivo = ReadConsulta.GetValue(6)
            objMenu.Tipo = ReadConsulta.GetString(7)
            objMenu.Sub_Rol = ReadConsulta.GetString(8)
            objMenu.IDlink = ReadConsulta.GetString(9)
            objMenu.DescripcionLink = ReadConsulta.GetString(10)
            objMenu.Parametro_1 = ReadConsulta.GetValue(11)
            objMenu.Parametro_2 = ReadConsulta.GetValue(12)
            objMenu.Ruta = ReadConsulta.GetString(13)

            'agregamos a la lista
            ObjListMenu.Add(objMenu)

        End While

        'cerramos conexiones
        ReadConsulta.Close()
        objConexBD.Close()
        'retornamos la consulta
        Return ObjListMenu

    End Function
    ''' <summary>
    ''' funcion generica para la insercion o actualizacion de datos en la BD
    ''' </summary>
    ''' <param name="vp_S_StrQuery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function StrInsert_and_Update_All(ByVal vp_S_StrQuery As String)

        'inicializamos conexiones a la BD
        Dim objcmd As OleDbCommand = Nothing
        Dim objConexBD As OleDbConnection = Nothing
        Dim vl_S_processUpdate As String

        Try
            objConexBD = New OleDbConnection(vg_S_StrConexion)
            objConexBD.ConnectionString = vg_S_StrConexion

            'abrimos conexion
            objConexBD.Open()
            'cargamos la carga y la conexion
            objcmd = New OleDbCommand(vp_S_StrQuery, objConexBD)
            'ejecutamos la carga
            objcmd.ExecuteNonQuery()
            'cerramos conexiones
            objConexBD.Close()

            vl_S_processUpdate = "Exito"

        Catch ex As Exception
            vl_S_processUpdate = "Error"
        End Try
        Return vl_S_processUpdate

    End Function


End Class
