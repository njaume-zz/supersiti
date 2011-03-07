Public Class clsComprobante

#Region "Variables"
    Private _com_id As System.Int32
    Private _com_ptovta As System.String
    Private _com_nroemitido As System.String
    Private _com_fecha As System.DateTime
    Private _com_importegravado As System.Decimal
    Private _com_importenogravado As System.Decimal
    Private _com_ivafacturado As System.Decimal
    Private _com_totalfactrado As System.Decimal
    Private _com_impreso As System.String
    Private _com_usunombre As System.String
    Private _com_clirazonsocial As System.String
    Private _com_clidomicilio As System.String
    Private _com_clitelefono As System.String
    Private _com_clidomicilioentrega As System.String
    Private _com_clicuit As System.String
    Private _com_cliingresobruto As System.String
    Private _com_razonsocialpropio As System.String
    Private _com_domiciliopropio As System.String
    Private _com_telefonopropio As System.String
    Private _com_cuitpropio As System.String
    Private _com_ingresobrutopropio As System.String
    Private _ctc_id As System.Int32
    Private _caj_id As System.Int32
    Private _fop_id As System.Int32
    Private _detalle As ArrayList

#End Region
#Region "Propiedades"

    Public Sub New()
    End Sub

    Public Property COM_ID() As Int32
        Get
            Return _com_id
        End Get
        Set(ByVal Value As Int32)
            _com_id = Value
        End Set
    End Property

    Public Property COM_PTOVTA() As String
        Get
            Return _com_ptovta
        End Get
        Set(ByVal Value As String)
            _com_ptovta = Value
        End Set
    End Property

    Public Property COM_NROEMITIDO() As String
        Get
            Return _com_nroemitido
        End Get
        Set(ByVal Value As String)
            _com_nroemitido = Value
        End Set
    End Property

    Public Property COM_FECHA() As DateTime
        Get
            Return _com_fecha
        End Get
        Set(ByVal Value As DateTime)
            _com_fecha = Value
        End Set
    End Property

    Public Property COM_IMPORTEGRAVADO() As Decimal
        Get
            Return _com_importegravado
        End Get
        Set(ByVal Value As Decimal)
            _com_importegravado = Value
        End Set
    End Property

    Public Property COM_IMPORTENOGRAVADO() As Decimal
        Get
            Return _com_importenogravado
        End Get
        Set(ByVal Value As Decimal)
            _com_importenogravado = Value
        End Set
    End Property

    Public Property COM_IVAFACTURADO() As Decimal
        Get
            Return _com_ivafacturado
        End Get
        Set(ByVal Value As Decimal)
            _com_ivafacturado = Value
        End Set
    End Property

    Public Property COM_TOTALFACTRADO() As Decimal
        Get
            Return _com_totalfactrado
        End Get
        Set(ByVal Value As Decimal)
            _com_totalfactrado = Value
        End Set
    End Property

    Public Property COM_IMPRESO() As String
        Get
            Return _com_impreso
        End Get
        Set(ByVal Value As String)
            _com_impreso = Value
        End Set
    End Property

    Public Property COM_USUNOMBRE() As String
        Get
            Return _com_usunombre
        End Get
        Set(ByVal Value As String)
            _com_usunombre = Value
        End Set
    End Property

    Public Property COM_CLIRAZONSOCIAL() As String
        Get
            Return _com_clirazonsocial
        End Get
        Set(ByVal Value As String)
            _com_clirazonsocial = Value
        End Set
    End Property

    Public Property COM_CLIDOMICILIO() As String
        Get
            Return _com_clidomicilio
        End Get
        Set(ByVal Value As String)
            _com_clidomicilio = Value
        End Set
    End Property

    Public Property COM_CLITELEFONO() As String
        Get
            Return _com_clitelefono
        End Get
        Set(ByVal Value As String)
            _com_clitelefono = Value
        End Set
    End Property

    Public Property COM_CLIDOMICILIOENTREGA() As String
        Get
            Return _com_clidomicilioentrega
        End Get
        Set(ByVal Value As String)
            _com_clidomicilioentrega = Value
        End Set
    End Property

    Public Property COM_CLICUIT() As String
        Get
            Return _com_clicuit
        End Get
        Set(ByVal Value As String)
            _com_clicuit = Value
        End Set
    End Property

    Public Property COM_CLIINGRESOBRUTO() As String
        Get
            Return _com_cliingresobruto
        End Get
        Set(ByVal Value As String)
            _com_cliingresobruto = Value
        End Set
    End Property

    Public Property COM_RAZONSOCIALPROPIO() As String
        Get
            Return _com_razonsocialpropio
        End Get
        Set(ByVal Value As String)
            _com_razonsocialpropio = Value
        End Set
    End Property

    Public Property COM_DOMICILIOPROPIO() As String
        Get
            Return _com_domiciliopropio
        End Get
        Set(ByVal Value As String)
            _com_domiciliopropio = Value
        End Set
    End Property

    Public Property COM_TELEFONOPROPIO() As String
        Get
            Return _com_telefonopropio
        End Get
        Set(ByVal Value As String)
            _com_telefonopropio = Value
        End Set
    End Property

    Public Property COM_CUITPROPIO() As String
        Get
            Return _com_cuitpropio
        End Get
        Set(ByVal Value As String)
            _com_cuitpropio = Value
        End Set
    End Property

    Public Property COM_INGRESOBRUTOPROPIO() As String
        Get
            Return _com_ingresobrutopropio
        End Get
        Set(ByVal Value As String)
            _com_ingresobrutopropio = Value
        End Set
    End Property

    Public Property CTC_ID() As Int32
        Get
            Return _ctc_id
        End Get
        Set(ByVal Value As Int32)
            _ctc_id = Value
        End Set
    End Property

    Public Property CAJ_ID() As Int32
        Get
            Return _caj_id
        End Get
        Set(ByVal Value As Int32)
            _caj_id = Value
        End Set
    End Property

    Public Property FOP_ID() As Int32
        Get
            Return _fop_id
        End Get
        Set(ByVal Value As Int32)
            _fop_id = Value
        End Set
    End Property

    Public Property DETALLE() As ArrayList
        Get
            Return _detalle
        End Get
        Set(ByVal value As ArrayList)
            _detalle = value
        End Set
    End Property
#End Region
End Class

