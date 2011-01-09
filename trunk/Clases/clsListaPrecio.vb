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

    Public Sub New(ByVal lpr_id As system.int32, ByVal pro_id As system.int32, ByVal trp_id As system.int32, _
                   ByVal lpr_precio As system.decimal, ByVal lpr_precioxcantidad As system.decimal, _
                   ByVal lpr_estado As system.int32)
        _lpr_id = lpr_id
        _pro_id = pro_id
        _tpr_id = tpr_id
        _lpr_precio = lpr_precio
        _lpr_precioxcantidad = lpr_precioxcantidad
        _lpr_estado = lpr_estado
    End Sub

    Public Property LPR_ID() As Int32
        Get
            Return _LPR_ID
        End Get
        Set(ByVal Value As Int32)
            _LPR_ID = Value
        End Set
    End Property
    Public Property PRO_ID() As Int32
        Get
            Return _PRO_ID
        End Get
        Set(ByVal Value As Int32)
            _PRO_ID = Value
        End Set
    End Property
    Public Property TPR_ID() As Int32
        Get
            Return _TPR_ID
        End Get
        Set(ByVal Value As Int32)
            _TPR_ID = Value
        End Set
    End Property
    Public Property LPR_PRECIO() As Decimal
        Get
            Return _LPR_PRECIO
        End Get
        Set(ByVal Value As Decimal)
            _LPR_PRECIO = Value
        End Set
    End Property
    Public Property LPR_PRECIOXCANTIDAD() As Decimal
        Get
            Return _LPR_PRECIOXCANTIDAD
        End Get
        Set(ByVal Value As Decimal)
            _LPR_PRECIOXCANTIDAD = Value
        End Set
    End Property
    Public Property LPR_ESTADO() As Int32
        Get
            Return _LPR_ESTADO
        End Get
        Set(ByVal Value As Int32)
            _LPR_ESTADO = Value
        End Set
    End Property
    
#End Region
End Class

