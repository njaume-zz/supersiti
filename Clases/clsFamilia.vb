Public Class clsFamilia

    Private _id As Integer
    Private _codigo As String
    Private _nombre As String
    Private _descripcion As String
    Private _rub_id As Integer
    Private _estado As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal codigo As String, ByVal nombre As String, _
                   ByVal descripcion As String, ByVal rub_id As Integer, ByVal estado As Integer)
        _id = id
        _codigo = codigo
        _nombre = nombre
        _descripcion = descripcion
        _rub_id = rub_id
        _estado = estado
    End Sub

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return _codigo
        End Get
        Set(ByVal value As String)
            _codigo = value
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

    Public Property Rub_Id() As Integer
        Get
            Return _rub_id
        End Get
        Set(ByVal value As Integer)
            _rub_id = value
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
