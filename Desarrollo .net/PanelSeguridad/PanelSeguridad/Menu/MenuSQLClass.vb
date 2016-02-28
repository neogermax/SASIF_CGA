Public Class MenuSQLClass

    Public Function Read_AllOptionsMenu()

        Dim ObjListMenu As New List(Of MenuClass)
        Dim StrQuery As String = ""
        Dim conex As New Conector

        Dim sql As New StringBuilder
        sql.Append("SELECT U_nombre AS Nombre," & _
                          "U_Estado AS EstadoUsuario," & _
                          "R_Rol_ID AS IDRol," & _
                          "R_Descripcion AS DescripcionRol," & _
                          "R_Sigla AS Sigla," & _
                          "OR_OPRol_ID AS IDOpcionRol," & _
                          "OR_Consecutivo AS Consecutivo," & _
                          "OR_Tipo As Tipo," & _
                          "[OR_Subrol/rol] AS Sub_Rol," & _
                          "L_Link_ID AS IDlink," & _
                          "L_Descripcion AS DescripcionLink," & _
                          "L_Param1 AS Parametro_1," & _
                          "L_Param2 AS Parametro_2," & _
                          "l_LinkPag AS Ruta " & _
                  "FROM USUARIOS U " & _
                  "INNER JOIN ROLES r ON r.R_Rol_ID = u.U_Rol_ID " & _
                  "INNER JOIN OPTION_ROL op ON op.OR_OPRol_ID = u.U_OPRol_ID " & _
                  "INNER JOIN LINKS l ON l.L_Link_ID = op.OR_Link_ID")
        StrQuery = sql.ToString

        ObjListMenu = conex.ObjLoad_All(StrQuery, "Menu")

        Return ObjListMenu

    End Function

End Class
