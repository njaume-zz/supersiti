Public Class clsCierreCaja

#Region "Atributos"

    Private _importe As Decimal
    Private _nombre As String
    Private _caja As Integer

#End Region

#Region "Propiedades"

    Public Property Importe() As Decimal
        Get
            Return _importe
        End Get
        Set(ByVal value As Decimal)
            _importe = value
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

    Public Property Caja() As Integer
        Get
            Return _caja
        End Get
        Set(ByVal value As Integer)
            _caja = value
        End Set
    End Property

#End Region

End Class
