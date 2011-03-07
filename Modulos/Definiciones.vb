Module Definiciones

#Region "Constantes"
    Public cXMLConfig As String = "XMLConfig.xml"


    'Constantes que definen las acciones posteriores a la selección
    ' de autorización de: Venta/Descuento/Tarjeta, etc.
    Public Const gcAccionAutorizaVenta = 1
    Public Const gcAccionAutorizaDescuento = 2
    Public Const gcAccionAutorizaTarjeta = 3

    'Constantes de Ventas
    Public Const gstrTipoComprobante = "Venta/TipoComprobante"
    Public Const gstrCaja = "Venta/NroCaja"
#End Region

#Region "Variables"
    Public gszPath = ""

#End Region

End Module
