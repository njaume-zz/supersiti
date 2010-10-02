Public Class clsRol

    Private _id As Integer
    Private _nombre As String
    Private _estado As Integer
    Private _usuario As ArrayList
    Private _modulo As ArrayList

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal nom As String, ByVal estado As Integer, _
                    ByVal usuario As ArrayList, ByVal modulo As ArrayList)
        _id = id
        _nombre = nom
        _estado = estado
        _usuario = usuario
        _modulo = modulo

    End Sub

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

    Public Property Estado() As Integer
        Get
            Return _estado
        End Get
        Set(ByVal value As Integer)
            _estado = value
        End Set
    End Property

    Public Property Usuario() As ArrayList
        Get
            Return _usuario
        End Get
        Set(ByVal value As ArrayList)
            _usuario = value
        End Set
    End Property

    Public Property Modulo() As ArrayList
        Get
            Return _modulo
        End Get
        Set(ByVal value As ArrayList)
            _modulo = value
        End Set
    End Property

End Class
