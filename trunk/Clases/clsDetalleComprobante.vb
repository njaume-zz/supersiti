Public Class clsDetalleComprobante

    Private _id As Integer
    Private _codigo As String
    Private _nombre As String
    Private _cantidad As Double ' Este valor es double porque puede ser pesable
    Private _pesable As Integer
    Private _pciounit As Double
    Private _pciototal As Double

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

    Public Property Cantidad() As Double
        Get
            Return _cantidad
        End Get
        Set(ByVal value As Double)
            _cantidad = value
        End Set
    End Property

    Public Property Pesable() As Integer
        Get
            Return _pesable
        End Get
        Set(ByVal value As Integer)
            _pesable = value
        End Set
    End Property

    Public Property PcioUnitario() As Double
        Get
            Return _pciounit
        End Get
        Set(ByVal value As Double)
            _pciounit = value
        End Set
    End Property

    Public Property PcioTotal() As Double
        Get
            Return _pciototal
        End Get
        Set(ByVal value As Double)
            _pciototal = value
        End Set
    End Property

End Class
