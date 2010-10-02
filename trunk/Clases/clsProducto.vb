Public Class clsProducto
    Private _id As Integer
    Private _rub_id As Integer
    Private _codigo As String
    Private _idpadre As Integer
    Private _nombre As String
    Private _marca As String
    Private _descripcion As String
    Private _nombreetiqueta As String
    Private _univ_id As Integer 'Unidad de venta
    Private _unic_id As Integer 'Unidad de Compra
    Private _pciocompra As Double
    Private _pciocosto As Double
    Private _iva As Double
    Private _impuestointerno As Double
    Private _maximo As Integer ' Máximo a reponer
    Private _minimo As Integer ' Minimo a Reponer
    Private _tiemporeposicion As Integer ' Tiempo de reposición
    Private _fam_id As Integer
    Private _estado As Integer
    Private _prd_asociados As Integer
    Private _alerta As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal rub_id As Integer, ByVal codigo As String, ByVal idpadre As Integer, _
                    ByVal nombre As String, ByVal marca As String, ByVal descripcion As String, ByVal nombreetiqueta As String, _
                    ByVal univ_id As Integer, ByVal unic_id As Integer, ByVal pciocompra As Double, ByVal iva As Double, _
                    ByVal pciocosto As Double, ByVal impuestointerno As Double, ByVal maximo As Integer, ByVal minimo As Integer, _
                    ByVal tiemporeposicion As Integer, ByVal fam_id As Integer, ByVal estado As Integer, _
                    ByVal prd_asociado As Integer, ByVal alerta As Integer)
        _id = id
        _rub_id = rub_id
        _codigo = codigo
        _idpadre = idpadre
        _nombre = nombre
        _marca = marca
        _nombreetiqueta = nombreetiqueta
        _descripcion = descripcion
        _unic_id = unic_id
        _univ_id = univ_id
        _pciocompra = pciocompra
        _pciocosto = pciocosto
        _iva = iva
        _impuestointerno = impuestointerno
        _maximo = maximo
        _minimo = minimo
        _tiemporeposicion = tiemporeposicion
        _fam_id = fam_id
        _estado = estado
        _prd_asociados = prd_asociado
        _alerta = alerta
    End Sub

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Rub_Id() As Integer
        Get
            Return _rub_id
        End Get
        Set(ByVal value As Integer)
            _rub_id = value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return _codigo
        End Get
        Set(ByVal value As String)
            _codigo = value
        End Set
    End Property

    Public Property IdPadre() As Integer
        Get
            Return _idpadre
        End Get
        Set(ByVal value As Integer)
            _idpadre = value
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

    Public Property Marca() As String
        Get
            Return _marca
        End Get
        Set(ByVal value As String)
            _marca = value
        End Set
    End Property

    Public Property NombreEtiqueta() As String
        Get
            Return _nombreetiqueta
        End Get
        Set(ByVal value As String)
            _nombreetiqueta = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Public Property Unic_Id() As Integer
        Get
            Return _unic_id
        End Get
        Set(ByVal value As Integer)
            _unic_id = value
        End Set
    End Property

    Public Property Univ_Id() As Integer
        Get
            Return _univ_id
        End Get
        Set(ByVal value As Integer)
            _univ_id = value
        End Set
    End Property

    Public Property PcioCompra() As Double
        Get
            Return _pciocompra
        End Get
        Set(ByVal value As Double)
            _pciocompra = value
        End Set
    End Property

    Public Property PcioCosto() As Double
        Get
            Return _pciocosto
        End Get
        Set(ByVal value As Double)
            _pciocosto = value
        End Set
    End Property

    Public Property Iva() As Double
        Get
            Return _iva
        End Get
        Set(ByVal value As Double)
            _iva = value
        End Set
    End Property

    Public Property ImpuestoInterno() As Double
        Get
            Return _impuestointerno
        End Get
        Set(ByVal value As Double)
            _impuestointerno = value
        End Set
    End Property

    Public Property Maximo() As Integer
        Get
            Return _maximo
        End Get
        Set(ByVal value As Integer)
            _maximo = value
        End Set
    End Property

    Public Property Minimo() As Integer
        Get
            Return _minimo
        End Get
        Set(ByVal value As Integer)
            _minimo = value
        End Set
    End Property

    Public Property TiempoReposicion() As Integer
        Get
            Return _tiemporeposicion
        End Get
        Set(ByVal value As Integer)
            _tiemporeposicion = value
        End Set
    End Property

    Public Property Fam_Id() As Integer
        Get
            Return _fam_id
        End Get
        Set(ByVal value As Integer)
            _fam_id = value
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

    Public Property PrdAsociado() As Integer
        Get
            Return _prd_asociados
        End Get
        Set(ByVal value As Integer)
            _prd_asociados = value
        End Set
    End Property

    Public Property Alerta() As Integer
        Get
            Return _alerta
        End Get
        Set(ByVal value As Integer)
            _alerta = value
        End Set
    End Property
End Class
