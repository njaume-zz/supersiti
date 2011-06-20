Public Class clsListaPrecio

#Region "Variables"
    Private _lpr_id As System.Int32
    Private _pro_id As System.Int32
    Private _tpr_id As System.Int32
    Private _lpr_precio As System.Decimal
    Private _lpr_precioxcantidad As System.Decimal
    Private _lpr_estado As System.Int32
#End Region

#Region "Propiedades"

    Public Sub New()
    End Sub

    Public Sub New(ByVal lpr_id As System.Int32, ByVal pro_id As System.Int32, ByVal trp_id As System.Int32, _
                   ByVal lpr_precio As System.Decimal, ByVal lpr_precioxcantidad As System.Decimal, _
                   ByVal lpr_estado As System.Int32)
        _lpr_id = lpr_id
        _pro_id = pro_id
        _tpr_id = TPR_ID
        _lpr_precio = lpr_precio
        _lpr_precioxcantidad = lpr_precioxcantidad
        _lpr_estado = lpr_estado
    End Sub

    Public Property LPR_ID() As Int32
        Get
            Return _lpr_id
        End Get
        Set(ByVal Value As Int32)
            _lpr_id = Value
        End Set
    End Property
    Public Property PRO_ID() As Int32
        Get
            Return _pro_id
        End Get
        Set(ByVal Value As Int32)
            _pro_id = Value
        End Set
    End Property
    Public Property TPR_ID() As Int32
        Get
            Return _tpr_id
        End Get
        Set(ByVal Value As Int32)
            _tpr_id = Value
        End Set
    End Property
    Public Property LPR_PRECIO() As Decimal
        Get
            Return _lpr_precio
        End Get
        Set(ByVal Value As Decimal)
            _lpr_precio = Value
        End Set
    End Property
    Public Property LPR_PRECIOXCANTIDAD() As Decimal
        Get
            Return _lpr_precioxcantidad
        End Get
        Set(ByVal Value As Decimal)
            _lpr_precioxcantidad = Value
        End Set
    End Property
    Public Property LPR_ESTADO() As Int32
        Get
            Return _lpr_estado
        End Get
        Set(ByVal Value As Int32)
            _lpr_estado = Value
        End Set
    End Property

#End Region
End Class

