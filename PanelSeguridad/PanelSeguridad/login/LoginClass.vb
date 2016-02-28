' se crea clase para mamejar los datos como objetos para manipulacion en la BD 
Public Class LoginClass

#Region "campos"
    Private _Name As String
    Private _Password As String
#End Region

#Region "propiedades"
    Public Property Name() As String
        Get
            Return Me._Name
        End Get
        Set(ByVal value As String)
            Me._Name = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return Me._Password
        End Get
        Set(ByVal value As String)
            Me._Password = value
        End Set
    End Property
#End Region

End Class
