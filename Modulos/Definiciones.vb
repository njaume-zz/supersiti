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

    'Constantes para Datos Propios
    Public Const gstrRazonSocial = "DatosPropios/RazonSocial"
    Public Const gstrNombreFantasia = "DatosPropios/NombreFantasia"
    Public Const gstrCUIT = "DatosPropios/CUIT"
    Public Const gIngresosBrutos = "DatosPropios/IngresosBrutos"
    Public Const gstrDomicilioFiscal = "DatosPropios/DomicilioFiscal"
    Public Const gstrDomicilioComercial = "DatosPropios/DomicilioComercial"
    Public Const gstrTelefono = "DatosPropios/Telefono"
    Public Const gstrHabilitacionMunicipal = "DatosPropios/HabilitacionMunicipal"
    Public Const gstrCondicionImpositiva = "DatosPropios/CondicionImpositiva"
    Public Const gstrLeyendaTicket = "DatosPropios/LeyendaTicket"
    Public Const gstrInicioActividad = "DatosPropios/InicioActividad"

    Public Const gstrPuertoCOM = "Venta/PuertoCOM"
    'Constantes de Formato
    Public Const gdNull = "0.00"

#End Region

#Region "Variables"
    Public gszPath = ""

#End Region

End Module
