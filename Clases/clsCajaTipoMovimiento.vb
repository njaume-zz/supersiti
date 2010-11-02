Public Class clsCajaTipoMovimiento

#Region "Variables"

    Private _CAE_ID As System.Int32
    Private _CAE_NOMBRE As System.String
    Private _CAE_ESTADO As System.Int32
    Private _CAE_FECHAALTA As System.DateTime

#End Region

#Region "Propiedades"

    Public Sub New()

    End Sub

    Public Sub New(ByVal cae_id As System.Int32, ByVal cae_nombre As System.String, _
                   ByVal cae_estado As System.Int32, ByVal cae_fechaalta As System.DateTime)
        _CAE_ID = cae_id
        _CAE_NOMBRE = cae_nombre
        _CAE_ESTADO = cae_estado
        _CAE_FECHAALTA = cae_fechaalta
    End Sub


    Public Property CAE_ID() As Int32
        Get
            Return _CAE_ID
        End Get
        Set(ByVal Value As Int32)
            _CAE_ID = Value
        End Set
    End Property
    Public Property CAE_NOMBRE() As String
        Get
            Return _CAE_NOMBRE
        End Get
        Set(ByVal Value As String)
            _CAE_NOMBRE = Value
        End Set
    End Property
    Public Property CAE_ESTADO() As Int32
        Get
            Return _CAE_ESTADO
        End Get
        Set(ByVal Value As Int32)
            _CAE_ESTADO = Value
        End Set
    End Property
    Public Property CAE_FECHAALTA() As DateTime
        Get
            Return _CAE_FECHAALTA
        End Get
        Set(ByVal Value As DateTime)
            _CAE_FECHAALTA = Value
        End Set
    End Property


#End Region

End Class
