
Public Class clsTasaIva

    private _id as integer
    private _nombre as string 
    private _tasa as double 
    private _estado as integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal nombre As String, ByVal tasa As Double, ByVal estado As Integer)
        _id = id
        _nombre = nombre
        _tasa = tasa
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

    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Public Property Tasa() As Double
        Get
            Return _tasa
        End Get
        Set(ByVal value As Double)
            _tasa = value
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
