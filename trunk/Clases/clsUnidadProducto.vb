Public Class clsUnidadProducto

    Private _id As Integer
    Private _nombre As String
    Private _sigla As String
    Private _descripcion As String
    Private _estado As Integer

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Public Property Sigla() As String
        Get
            Return _sigla
        End Get
        Set(ByVal value As String)
            _sigla = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Public Property Estado() As Integer
        Get
            Return _estado
        End Get
        Set(ByVal value As Integer)
            _estado = value
        End Set
    End Property

End Class
