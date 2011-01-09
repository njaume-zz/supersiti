Public Class clsTiposPrecios

#Region "Variables"
    Private _tpr_id As System.Int32
    Private _tpr_codigo As System.String
    Private _trp_nombre As System.String
    Private _tpr_descripcion As System.String
    Private _tpr_estado As System.Int32
    Private _tpr_fechaalta As System.DateTime
#End Region
#Region "Propiedades"

    Public Sub New()
    End Sub


    Public Property TPR_ID() As Int32
        Get
            Return _TPR_ID
        End Get
        Set(ByVal Value As Int32)
            _TPR_ID = Value
        End Set
    End Property
    Public Property TPR_CODIGO() As String
        Get
            Return _TPR_CODIGO
        End Get
        Set(ByVal Value As String)
            _TPR_CODIGO = Value
        End Set
    End Property
    Public Property TRP_NOMBRE() As String
        Get
            Return _TRP_NOMBRE
        End Get
        Set(ByVal Value As String)
            _TRP_NOMBRE = Value
        End Set
    End Property
    Public Property TPR_DESCRIPCION() As String
        Get
            Return _TPR_DESCRIPCION
        End Get
        Set(ByVal Value As String)
            _TPR_DESCRIPCION = Value
        End Set
    End Property
    Public Property TPR_ESTADO() As Int32
        Get
            Return _TPR_ESTADO
        End Get
        Set(ByVal Value As Int32)
            _TPR_ESTADO = Value
        End Set
    End Property
    Public Property TPR_FECHAALTA() As DateTime
        Get
            Return _TPR_FECHAALTA
        End Get
        Set(ByVal Value As DateTime)
            _TPR_FECHAALTA = Value
        End Set
    End Property
#End Region
End Class

