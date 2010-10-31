Public Class clsCaja

    Private _id As Integer
    Private _numero As Integer
    Private _estado As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal numero As Integer, ByVal estado As Integer)
        _id = id
        _numero = numero
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

    Public Property Numero() As Integer
        Get
            Return _numero
        End Get
        Set(ByVal value As Integer)
            _numero = value
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
