﻿Imports Newtonsoft.Json

Public Class menuAjax
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'trae el jquery para hacer todo por debajo del servidor
        If Request.Form("action") <> Nothing Then
            'aterrizamos las opciones del proceso
            Dim vl_S_option_login As String = Request.Form("action")

            Select Case vl_S_option_login

                Case "consulta"
                    Consulta_menu()
            End Select

        End If
    End Sub

    Protected Sub Consulta_menu()

        Dim SQL_Menu As New MenuSQLClass
        Dim ObjListMenu As New List(Of MenuClass)

        ObjListMenu = SQL_Menu.Read_AllOptionsMenu()
        'serializamos el objeto
        Response.Write(JsonConvert.SerializeObject(ObjListMenu.ToArray()))

    End Sub

End Class