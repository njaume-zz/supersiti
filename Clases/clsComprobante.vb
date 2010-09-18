Public Class clsComprobante

    Private _id As Integer
    Private _nrocomp As String  ' estará compuesto por el ptovta-nro
    Private _fecha As Date
    Private _idcliente As Integer
    Private _nomcliente As String
    Private _cuitcliente As String
    Private _subtotal As Double
    Private _total As Double
    Private _detalles As ArrayList

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property NroComp() As String
        Get
            Return _nrocomp
        End Get
        Set(ByVal value As String)
            _nrocomp = value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return _fecha
        End Get
        Set(ByVal value As Date)
            _fecha = value
        End Set
    End Property

    Public Property IdCliente() As Integer
        Get
            Return _idcliente
        End Get
        Set(ByVal value As Integer)
            _idcliente = value
        End Set
    End Property

    Public Property NombreCliente() As String
        Get
            Return _nomcliente
        End Get
        Set(ByVal value As String)
            _nomcliente = value
        End Set
    End Property

    Public Property CuitCliente() As String
        Get
            Return _cuitcliente
        End Get
        Set(ByVal value As String)
            _cuitcliente = value
        End Set
    End Property

    Public Property SubTotal() As Double
        Get
            Return _subtotal
        End Get
        Set(ByVal value As Double)
            _subtotal = value
        End Set
    End Property

    Public Property Total() As Double
        Get
            Return _total
        End Get
        Set(ByVal value As Double)
            _total = value
        End Set
    End Property

    Public Property Detalles() As ArrayList
        Get
            Return _detalles
        End Get
        Set(ByVal value As ArrayList)
            _detalles = value
        End Set
    End Property

End Class
