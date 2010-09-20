Public Class clsCaja

    Private _id As Integer
    Private _numero As Integer
    Private _cajaestado As Integer
    Private _fechaapertura As Date
    Private _fechacierre As Date
    Private _estado As Integer

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Numero() As Integer
        Get
            Return _numero
        End Get
        Set(ByVal value As Integer)
            _numero = value
        End Set
    End Property

    Public Property CajaEstado() As Integer
        Get
            Return _cajaestado
        End Get
        Set(ByVal value As Integer)
            _cajaestado = value
        End Set
    End Property

    Public Property FechaApertura() As Date
        Get
            Return _fechaapertura
        End Get
        Set(ByVal value As Date)
            _fechaapertura = value
        End Set
    End Property

    Public Property FechaCierre() As Date
        Get
            Return _fechacierre
        End Get
        Set(ByVal value As Date)
            _fechacierre = value
        End Set
    End Property

    Public Property Estado() As Integer
        Get
            Return _estado
        End Get
        Set(ByVal value As Integer)
            _estado = value
        End Set
    End Property
End Class
