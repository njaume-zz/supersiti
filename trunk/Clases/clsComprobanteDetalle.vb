Public Class clsComprobanteDetalle

#Region "Variables"
    Private _cod_id As System.Int32
    Private _cod_procodigo As System.String
    Private _cod_pronombre As System.String
    Private _cod_propciounitario As System.Decimal
    Private _cod_procantidad As System.Int32
    Private _cod_preciocantidad As System.Decimal
    Private _cod_iva As System.Decimal
    Private _com_id As System.Int32
    Private _cod_pesable As System.Int32

#End Region
#Region "Propiedades"

    Public Sub New()
    End Sub

    Public Sub New(ByVal cod_id As System.Int32, ByVal cod_procodigo As System.String, ByVal cod_pronombre As System.String, _
                    ByVal cod_propciounit As System.Decimal, ByVal cod_procant As System.Int32, ByVal cod_pciocant As System.Decimal, _
                    ByVal cod_iva As System.Decimal, ByVal com_id As System.Int32, ByVal cod_pesable As System.Int32)
        _cod_id = cod_id
        _cod_procodigo = cod_procodigo
        _cod_pronombre = cod_pronombre
        _cod_propciounitario = cod_propciounit
        _cod_procantidad = cod_procant
        _cod_preciocantidad = cod_pciocant
        _cod_iva = cod_iva
        _com_id = com_id
        _cod_pesable = cod_pesable
    End Sub


    Public Property COD_ID() As Int32
        Get
            Return _cod_id
        End Get
        Set(ByVal Value As Int32)
            _cod_id = Value
        End Set
    End Property
    Public Property COD_PROCODIGO() As String
        Get
            Return _cod_procodigo
        End Get
        Set(ByVal Value As String)
            _cod_procodigo = Value
        End Set
    End Property
    Public Property COD_PRONOMBRE() As String
        Get
            Return _cod_pronombre
        End Get
        Set(ByVal Value As String)
            _cod_pronombre = Value
        End Set
    End Property
    Public Property COD_PROPCIOUNITARIO() As Decimal
        Get
            Return _cod_propciounitario
        End Get
        Set(ByVal Value As Decimal)
            _cod_propciounitario = Value
        End Set
    End Property
    Public Property COD_PROCANTIDAD() As Int32
        Get
            Return _cod_procantidad
        End Get
        Set(ByVal Value As Int32)
            _cod_procantidad = Value
        End Set
    End Property
    Public Property COD_PRECIOCANTIDAD() As Decimal
        Get
            Return _cod_preciocantidad
        End Get
        Set(ByVal Value As Decimal)
            _cod_preciocantidad = Value
        End Set
    End Property
    Public Property COD_IVA() As Decimal
        Get
            Return _cod_iva
        End Get
        Set(ByVal Value As Decimal)
            _cod_iva = Value
        End Set
    End Property
    Public Property COM_ID() As Int32
        Get
            Return _com_id
        End Get
        Set(ByVal Value As Int32)
            _com_id = Value
        End Set
    End Property

    Public Property COD_PESABLE() As Int32
        Get
            Return _cod_pesable
        End Get
        Set(ByVal value As Int32)
            _cod_pesable = value
        End Set
    End Property
#End Region
End Class

