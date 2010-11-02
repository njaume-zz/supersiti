Public Class clsCajaMovimientos

#Region "Variables"


    Private _CAM_ID As System.Int32
    Private _CAM_IMPORTE As System.Decimal
    Private _CAE_ID As System.Int32
    Private _CAM_FECHA As System.DateTime
    Private _USU_ID As System.Int32
    Private _CAM_ESTADO As System.Int32
    Private _CAM_FECHAALTA As System.DateTime


#End Region
#Region "Propiedades"


    Public Sub New()

    End Sub

    Public Sub New(ByVal cam_id As System.Int32, ByVal cam_importe As System.Decimal, ByVal cae_id As System.Int32, _
                    ByVal cam_fecha As System.DateTime, ByVal usu_id As System.Int32, ByVal cam_estado As System.Int32, _
                    ByVal cam_fechaalta As System.DateTime)
        _CAM_ID = cam_id
        _CAM_IMPORTE = cam_importe
        _CAE_ID = cae_id
        _CAM_FECHA = cam_fecha
        _USU_ID = usu_id
        _CAM_ESTADO = cam_estado
        _CAM_FECHAALTA = cam_fechaalta
    End Sub


    Public Property CAM_ID() As Int32
        Get
            Return _CAM_ID
        End Get
        Set(ByVal Value As Int32)
            _CAM_ID = Value
        End Set
    End Property
    Public Property CAM_IMPORTE() As Decimal
        Get
            Return _CAM_IMPORTE
        End Get
        Set(ByVal Value As Decimal)
            _CAM_IMPORTE = Value
        End Set
    End Property
    Public Property CAE_ID() As Int32
        Get
            Return _CAE_ID
        End Get
        Set(ByVal Value As Int32)
            _CAE_ID = Value
        End Set
    End Property
    Public Property CAM_FECHA() As DateTime
        Get
            Return _CAM_FECHA
        End Get
        Set(ByVal Value As DateTime)
            _CAM_FECHA = Value
        End Set
    End Property
    Public Property USU_ID() As Int32
        Get
            Return _USU_ID
        End Get
        Set(ByVal Value As Int32)
            _USU_ID = Value
        End Set
    End Property
    Public Property CAM_ESTADO() As Int32
        Get
            Return _CAM_ESTADO
        End Get
        Set(ByVal Value As Int32)
            _CAM_ESTADO = Value
        End Set
    End Property
    Public Property CAM_FECHAALTA() As DateTime
        Get
            Return _CAM_FECHAALTA
        End Get
        Set(ByVal Value As DateTime)
            _CAM_FECHAALTA = Value
        End Set
    End Property


#End Region

End Class
