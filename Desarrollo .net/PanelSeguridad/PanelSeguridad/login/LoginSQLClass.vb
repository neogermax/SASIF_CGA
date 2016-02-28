Public Class LoginSQLClass
    ''' <summary>
    ''' funcion query para la consulta tabla Usuarios para ingreso a la aplicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Read_AllUserLogin()

        Dim objUser As New LoginClass
        Dim ObjListLogin As New List(Of LoginClass)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder
        sql.Append("SELECT U_Nombre, U_password	FROM Usuarios")
        StrQuery = sql.ToString

        ObjListLogin = conex.ObjLoad_All(StrQuery, "Login")

        Return ObjListLogin

    End Function
    ''' <summary>
    ''' funcion query para la actualizacion de la contrazeña desde el login
    ''' </summary>
    ''' <param name="pl_S_User"></param>
    ''' <param name="pl_S_Password"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update_ChagePassword(ByVal pl_S_User As String, ByVal pl_S_Password As String)

        Dim vl_S_processUpdate As String
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder
        sql.Append(" UPDATE Usuarios SET " & _
                   " U_password = '" & pl_S_Password & _
                   "' WHERE  U_Nombre = '" & UCase(pl_S_User) & "'")

        StrQuery = sql.ToString

        vl_S_processUpdate = conex.StrInsert_and_Update_All(StrQuery)

        Return vl_S_processUpdate

    End Function

End Class
