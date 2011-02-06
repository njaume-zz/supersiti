Public Class clsTipoComprobante

#Region "Variables"
    Private _ctc_id As System.Int32
    Private _ctc_codigo As System.String
    Private _ctc_descripcion As System.String
    Private _ctc_sigla As System.String
    Private _ctc_letra As System.String
    Private _ctc_signo As System.String
#End Region
#Region "Propiedades"

    Public Sub New()
    End Sub



    Public Property CTC_ID() As Int32
        Get
            Return _ctc_id
        End Get
        Set(ByVal Value As Int32)
            _ctc_id = Value
        End Set
    End Property
    Public Property CTC_CODIGO() As String
        Get
            Return _ctc_codigo
        End Get
        Set(ByVal Value As String)
            _ctc_codigo = Value
        End Set
    End Property
    Public Property CTC_DESCRIPCION() As String
        Get
            Return _ctc_descripcion
        End Get
        Set(ByVal Value As String)
            _ctc_descripcion = Value
        End Set
    End Property
    Public Property CTC_SIGLA() As String
        Get
            Return _ctc_sigla
        End Get
        Set(ByVal Value As String)
            _ctc_sigla = Value
        End Set
    End Property
    Public Property CTC_LETRA() As String
        Get
            Return _ctc_letra
        End Get
        Set(ByVal Value As String)
            _ctc_letra = Value
        End Set
    End Property
    Public Property CTC_SIGNO() As String
        Get
            Return _ctc_signo
        End Get
        Set(ByVal Value As String)
            _ctc_signo = Value
        End Set
    End Property
#End Region
End Class

