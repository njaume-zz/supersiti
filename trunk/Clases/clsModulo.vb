Public Class clsModulo

    Private _id As Integer
    Private _nombre As String
    Private _descripcion As String
    Private _estado As Integer
    Private _tipo As String
    Private _idpadre As Integer
    Private _url As String
    Private _orden As Integer
    Private _rol As ArrayList


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

    Public Property Tipo() As String
        Get
            Return _tipo
        End Get
        Set(ByVal value As String)
            _tipo = value
        End Set
    End Property

    Public Property IdPadre() As Integer
        Get
            Return _idpadre
        End Get
        Set(ByVal value As Integer)
            _idpadre = value
        End Set
    End Property

    Public Property Url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Public Property Orden() As Integer
        Get
            Return _orden
        End Get
        Set(ByVal value As Integer)
            _orden = value
        End Set
    End Property

    Public Property Rol() As ArrayList
        Get
            Return _rol
        End Get
        Set(ByVal value As ArrayList)
            _rol = value
        End Set
    End Property

End Class
