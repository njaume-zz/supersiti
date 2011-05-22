Public Class clsFiscalEpson
    'Option Explicit

    '*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!
    '     ////////////////////////////////////////////
    '    /// Codigo extraido de : IfConstantes.bas //
    '   ////////////////////////////////////////////
    '*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!

    Public garraysalida(0 To 30) As String  'Array de salida con datos al IF
    Public garrayentrada(0 To 30) As String ' Array entrada de respuesta desde IF
    ' *** DATOS IMPORTANTES***
    Public dato(10) As String
    Public gDatoEnviado As String               ' Almacena el Ultimo dato Enviado
    Public gDatoRecibido As String              ' Almacena el Ultimo dato Recibido
    Public gDatoDelComando As String            ' String que se envia con los datos necesarios para el comando
    '----------- Variable Public que maneja el cancelamiento ------------
    '---------------------------------------------------------------------
    '--- Defino Constante de OK y MAL
    Const cok = 1       ' 1 Indica BIEN , Respuesta !OK!
    Const cmal = 0      ' 0 Indica MAL , Respuesta !MAL!
    ' ******************** CONSTANTES *******************************
    Public Const NAK = (&H15)
    Public Const ACK = (&H6)
    Public Const enk = (&H5)
    Public Const STX = (&H2)
    Public Const ETX = (&H3)
    Public Const UPRNCAMPO = (&H1C)     ' Limitador
    Public Const MASTIEMPO = (&H12)     ' MAS Tiempo
    ' definiciones para variar atributos de texto
    Const gciAtribBase = &HF0
    Const gciAtribNormal = &HF0
    Const gciAtribResaltado = &HF1
    Const gciAtribDobleAlto = &HF2
    Const gciAtribDobleAncho = &HF4
    Const gciAtribSubrayado = &HF8
    ' CONSTANTES de COMANDOS
    Public Const COMEstado = &H2A
    Public Const COMTicketOpen = &H40
    Public Const COMTicketDescripcionExtra = &H41
    Public Const COMTicketItemDeLinea = &H42
    Public Const COMTicketSubtotal = &H43
    Public Const COMTicketPago = &H44
    Public Const COMTicketCerrar = &H45
    Public Const COMCierreX = &H39
    Public Const COMFechaHoraPoner = &H58
    Public Const COMFechaHoraLeer = &H59
    Public Const COMFactOpen = &H60
    Public Const COMFactSendItem = &H62
    Public Const COMFactSubtotal = &H63
    Public Const COMFactPago = &H64
    Public Const COMFactCierre = &H65
    Public Const COMFactPercep = &H66
    Public Const COMCajon_1 = &H7B             'Apertura de cajon 1
    Public Const COMCajon_2 = &H7C             'Apertura de cajon 2
    Public Const COMCierreXZ = &H39
    Public Const COMCierreNF = &H4A           'Cierre de comprobante no fiscal
    Public Const COMSetHeaderTrailer = &H5D   'Comando para informar el header o trailer del comprobante
    Public Const COMGetHeaderTrailer = &H5E   'Comando para obtener el header o trailer del comprobante
    Public Const COMRippleTest = &H2E
    Public Const COMDiagnostic = &H22
    Public Const COMAuditaZ = &H3B
    Public Const COMAuditaFecha = &H3A
    Public Const COMAbreNoFiscal = &H48
    Public Const COMEnviaLineaNF = &H49
    Public Const COMCierraNoFiscal = &H4A
    Public Const COMCortaPapel = &H4B
    Public Const COMAvanzaTique = &H50
    Public Const COMAvanzaAuditor = &H51
    Public Const COMAvanzaAmbos = &H52
    Public Const COMAvanzaHoja = &H53
    Public Const COMSelModImpre = &H57
    Public Const COMSendDnfh = &H4F
    Public Const COMTimeOut = 10
    Public Const gg_resnext = True           ' Hace que los trapeos de error vuelvan por RESUME NEXT
    Public Const PFI_MAX_LONG_RECIBIDA = 2048 ' Es la maxima logitud que puede recibir la impresora
    Public Const SET_LABEL = 1        'Para la funcion que carga el estado de impresora y placa
    Public Const CLR_LABEL = 2        'Para Borrar Labels
    'Public pr_port_number As Integer    'Nro de comm para impresora
    'Public pr_port_settings As String   'Configuración del Port
    'SETEO la FORM PRINCIPAL
    Public form_principal As Form       'La seteo para saber cual es el form principal
    Public hubo_error_com As Integer    'es seteado por el evento del comm port



    '*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!
    '     //////////////////////////////////////////////
    '    /// Codigo extraido de : fiscal.bas //
    '   //////////////////////////////////////////////
    '*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!

    'COMANDOS DE MANEJO DE LA IMPRESORA FISCAL
    '-----------------------------------------------------------
    'TODAS LAS FUNCIONES DEVUELVEN TRUE O FALSE DE ACUERDO
    'A SI EL COMANDO SE EJECUTO CON EXITO.
    '-----------------------------------------------------------
    'LA RESPUESTA DE LA IMPRESORA FISCAL A CADA COMANDO
    'ENVIADO, SE ENCUENTRAN EN EL ARRAY Public garrayentrada(0 a 30)
    'EL CAMPO 0(CERO) INDICA CUANTOS CAMPOS DEVUELVE AL IMPRESORA
    '-----------------------------------------------------------
    'LOS DATOS QUE SE ENVIAN Y SE RECIBEN SE PUEDEN OBTENER DEL
    'MANUAL DE PROTOCOLO DE COMUNICACION DISPONIBLE EN NUESTRA
    'PAGINA DE INTERNET (www.epson.com.ar) EN EL AREA DE IMPRESORAS
    'FISCALES - ARCHIVOS Y DRIVERS.
    '-----------------------------------------------------------
    'LOS VALORES NUMERICOS QUE SE RECIBEN COMO DATOS NO DEBEN
    'CONTENER PUNTOS O COMAS ("." o ",").
    '-----------------------------------------------------------
    'LAS FECHAS SE INGRESAN EN EL FORMATO AAMMDD
    '-----------------------------------------------------------
    'LA HORA SE INGRESA EN EL FORMATO HHMMSS
    '-----------------------------------------------------------

    Function FISCierreX(ByVal dat1 As String) As Boolean
        'Esta funcion envia el comando de ejecutar un Cierre X.
        '-----------------------------------------------------------
        'Recibe:
        '       dat1: "P" Imprime el reporte
        '             "N" No Imprime el reporte
        '
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la impresora
        '       2 - Estado Fiscal
        '       3 - Numero de cierre X
        '       4 - Cuenta de Documentos Fiscales Cancelados
        '       5 - Cuenta de Documentos No Fiscales Homologados
        '       6 - Cuenta de Documentos No Fiscales NO Homologados
        '       7 - Cuenta de comprobantes fiscales Tique,Factura B, C y Tique-Factura B, C
        '       8 - Cuenta de comprobantes fiscales Factura A y Tique-Factura A
        '       9 - Número de último comprobante fiscal Tique,Factura B, C y Tique-Factura B, C
        '       10 - Monto Total Facturado
        '       11 - Monto Total de Iva Cobrado
        '       12 - Importe Total de las percepciones (Solo facturas o tique-factura tipo A)
        '       13 - Número de último comprobante fiscal Factura A y Tique-Factura A
        '-----------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida("X")                      'Cierre X
        Call PFAgregarCampoSalida(Left(Trim(dat1), 1))  'Opcion de imprimir Cierre
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMCierreXZ, 13)
        iret = isPFiscalOk(COMCierreXZ) 'Chequeo si el comando fue bien ejecutado
        FISCierreX = (continuar And iret)
    End Function
    Function FISCierreZ() As Boolean
        'Esta funcion envia el comando de ejecutar un cierre Z
        'No recibe campos.
        '-----------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la impresora
        '       2 - Estado Fiscal
        '       3 - Numero de cierre Z
        '       4 - Cuenta de Documentos Fiscales Cancelados
        '       5 - Cuenta de Documentos No Fiscales Homologados
        '       6 - Cuenta de Documentos No Fiscales NO Homologados
        '       7 - Cuenta de comprobantes fiscales Tique,Factura B, C y Tique-Factura B, C
        '       8 - Cuenta de comprobantes fiscales Factura A y Tique-Factura A
        '       9 - Número de último comprobante fiscal Tique,Factura B, C y Tique-Factura B, C
        '       10 - Monto Total Facturado
        '       11 - Monto Total de Iva Cobrado
        '       12 - Importe Total de las percepciones (Solo facturas o tique-factura tipo A)
        '       13 - Número de último comprobante fiscal Factura A y Tique-Factura A
        '-----------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida("Z")  'Cierre Z
        Call PFAgregarCampoSalida("P")  'Opcion de imprimir Cierre se ignora
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMCierreXZ, 13)
        iret = isPFiscalOk(COMCierreXZ) 'Chequeo si el comando fue bien ejecutado
        FISCierreZ = (continuar And iret)
    End Function
    Function FISCierreTique() As Boolean
        'Pide el cierre del comprobante TIQUE
        'No recibe campos.
        '-----------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la impresora
        '       2 - Estado Fiscal
        '       3 - Numero de Documento Fiscal recientemente emitido
        '-----------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMTicketCerrar, 3)
        iret = isPFiscalOk(COMTicketCerrar) 'Chequeo si el comando fue bien ejecutado
        FISCierreTique = (continuar And iret)
    End Function
    Function FISPoneFechaHora(ByVal dat1 As Object, ByVal dat2 As Object) As Boolean
        'Recibe fecha y hora y las envia al PF.
        '---------------------------------------------------------
        '   Recibe:
        '           1 - (dat1) Fecha en formato AAMMDD
        '           2 - (dat2) Hora en formato HHMMSS
        '
        '   Retorna:
        '           en variable Public garrayentrada
        '           1- Estado de la Impresora
        '           2- Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida("S")                   'Set
        Call PFAgregarCampoSalida(Format(dat1, "YYMMDD")) 'Envia Fecha
        Call PFAgregarCampoSalida(Format(dat2, "HHMMSS")) 'Envia Hora
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFechaHoraPoner, 2)
        iret = isPFiscalOk(COMFechaHoraPoner) 'Chequeo si el comando fue bien ejecutado
        FISPoneFechaHora = (continuar And iret)
    End Function
    Function FISPideFechaHora() As Boolean
        'Pide fecha y hora.
        '---------------------------------------------------------
        '   Retorna:
        '           en variable Public garrayentrada
        '           1- Estado de la Impresora
        '           2- Estado de la Placa Fiscal
        '           3- Fecha en formato AAMMDD
        '           4- Hora en formato HHMMSS
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida("O")                   'Obtener
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFechaHoraLeer, 4)
        iret = isPFiscalOk(COMFechaHoraLeer) 'Chequeo si el comando fue bien ejecutado
        FISPideFechaHora = (continuar And iret)
    End Function
    Function FISEnviarHeader(ByVal dat1 As Object, ByVal dat2 As Object) As Boolean
        ' Setea un dato fijo (encabezado o cola)
        '---------------------------------------------------------
        '   Recibe:
        '           1 - (dat1) Nro de dato fijo (encabezado o cola)
        '           2 - (dat2) Texto a enviar
        '   Retorna:
        '           en variable Public garrayentrada
        '           1- Estado de la Impresora
        '           2- Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida((Right("00000" & dat1, 5))) ' Numero de encabezado
        Call PFAgregarCampoSalida(dat2)  'Descripción
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMSetHeaderTrailer, 3)
        iret = isPFiscalOk(COMFactSubtotal) 'Chequeo si el comando fue bien ejecutado
        FISEnviarHeader = (continuar And iret)
    End Function
    Function FISObtenerHeader(ByVal dat1 As Object) As Boolean
        ' Obtiene el valor de un dato fijo (encabezado o cola)
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Nro de dato fijo (encabezado o cola)
        '
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '       3 - Texto del dato fijo pedido
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida((Right("00000" & dat1, 5))) ' Numero de encabezado
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMGetHeaderTrailer, 4)
        iret = isPFiscalOk(COMGetHeaderTrailer) 'Chequeo si el comando fue bien ejecutado
        FISObtenerHeader = (continuar And iret)
    End Function
    Function FISOpenTicket(ByVal dat1 As Object) As Boolean
        'Abre un tique
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Formato para almacenar los datos ("C" o "G")
        '
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim iret As Integer, continuar As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMTicketOpen, 2)
        iret = isPFiscalOk(COMTicketOpen) 'Chequeo si el comando fue bien ejecutado
        FISOpenTicket = (continuar And iret)
    End Function
    Function FISSendItemTique(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object, ByVal dat4 As Object _
        , ByVal dat5 As Object, ByVal dat6 As Object, ByVal dat7 As Object, ByVal dat8 As Object) As Boolean
        'Envia un Item a la impresora
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Descripción
        '       2 - (dat2) Cantidad de items
        '       3 - (dat3) Monto del item
        '       4 - (dat4) Tasa de iva estandar
        '       5 - (dat5) Calificador de item
        '       6 - (dat6) Unidades (bultos)
        '       7 - (dat7) Tasa de imp int porecentuales K
        '       8 - (dat8) Impuestos internos fijos
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim iret As Integer, continuar As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Trim(dat1))                     'Descripción
        Call PFAgregarCampoSalida(Right("00000000" & Trim(dat2), 8))   'Cantidad
        Call PFAgregarCampoSalida(Right("000000000" & Trim(dat3), 9))   'Monto del Item
        Call PFAgregarCampoSalida(Right("0000" & Trim(dat4), 4))   'Tasa Impositiva
        Call PFAgregarCampoSalida(Left(LTrim(dat5), 1))           'Calificador del Item
        Call PFAgregarCampoSalida(Right("00000" & Trim(dat6), 5))   'Unidades o Bultos vendidos
        Call PFAgregarCampoSalida(Right("00000000" & Trim(dat7), 8))   'Tasa de Ajuste Variable
        Call PFAgregarCampoSalida(Right("000000000000000" & Trim(dat8), 15))   'Impuestos internos fijos
        continuar = PFIEnviarComando(COMTicketItemDeLinea, 3)
        iret = isPFiscalOk(COMTicketItemDeLinea) 'Chequeo si el comando fue bien ejecutado
        FISSendItemTique = (continuar And iret)
    End Function
    Function FISCancelaTique() As Boolean
        'Cancela un Tique
        '---------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left("descripcion", 25))  'Descripción
        Call PFAgregarCampoSalida("0000000001")             'Importe que pago
        Call PFAgregarCampoSalida("C") 'C de cancelar
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMTicketPago, 4)
        iret = isPFiscalOk(COMTicketPago) 'Chequeo si el comando fue bien ejecutado
        FISCancelaTique = (continuar And iret)
    End Function
    Function FISPagoTique(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object) As Boolean
        'realiza un pago,decuento o recargo
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Descripción del pago
        '       2 - (dat2) Importe del pago
        '       3 - (dat3) Tipo de pago
        '           T=Pago
        '           t=Reversion de pago
        '           D=Descuento
        '           R=Recargo
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '       3 - Resto de lo que falta pagar
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 25))           'Descripción
        Call PFAgregarCampoSalida(dat2)                     'Importe que pago
        Call PFAgregarCampoSalida(Left(dat3, 1))            'Calificación del Pago
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMTicketPago, 4)
        iret = isPFiscalOk(COMTicketPago) 'Chequeo si el comando fue bien ejecutado
        FISPagoTique = (continuar And iret)
    End Function
    Function FISOpenFact(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object, ByVal dat4 As Object, ByVal dat5 As Object _
        , ByVal dat6 As Object, ByVal dat7 As Object, ByVal dat8 As Object, ByVal dat9 As Object, ByVal dat10 As Object, ByVal dat11 As Object _
        , ByVal dat12 As Object, ByVal dat13 As Object, ByVal dat14 As Object, ByVal dat15 As Object, ByVal dat16 As Object, ByVal dat17 As Object _
        , ByVal dat18 As Object, ByVal dat19 As Object) As Boolean
        ' Abre factura
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Tipo de documento fiscal (T=TiqueFactura o F=Factura)
        '       2 - (dat2) Salida de impresora (C=continuo o S=Slip)
        '       3 - (dat3) Letra del documento (A,B o C)
        '       4 - (dat4) Cantidad de copias
        '       5 - (dat5) Tipo de formulario (F,P o A)
        '       6 - (dat6) Tipo de letra
        '       7 - (dat7) Responsabilidad IVA Emisor (I,R,E,N o M)
        '       8 - (dat8) Responsabilidad IVa Comprador(I,R,E,N,M o F)
        '       9 - (dat9) Linea 1 Nombre del Comprador
        '       10 - (dat10) Linea 2 Nombre del Comprador
        '       11 - (dat11) Tipo Documento Comprador
        '       12 - (dat12) Nro. Documento Comprador
        '       13 - (dat13) Bien de Uso (B o N)
        '       14 - (dat14) Linea 1 Domicilio Comprador
        '       15 - (dat15) Linea 2 Domicilio Comprador
        '       16 - (dat16) Linea 3 Domicilio Comprador
        '       17 - (dat17) Linea 1 Remito
        '       18 - (dat18) Linea 2 Remito
        '       19 - (dat19) Tipo de tabla de item (C o G)
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim iret As Integer, continuar As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(LTrim(dat1), 1))   'Tipo doc.fisc
        Call PFAgregarCampoSalida(Left(LTrim(dat2), 1))   'Tipo sal impre
        Call PFAgregarCampoSalida(Left(LTrim(dat3), 1))   'Letra doc fisc
        Call PFAgregarCampoSalida(Left(LTrim(dat4), 1))   'Cant copias
        Call PFAgregarCampoSalida(Left(LTrim(dat5), 1))   'Tipo form
        Call PFAgregarCampoSalida(Left(LTrim(dat6), 2))   'Tipo font
        Call PFAgregarCampoSalida(Left(LTrim(dat7), 1))   'Resp IVA Emisor
        Call PFAgregarCampoSalida(Left(LTrim(dat8), 1))   'Resp.IVA Compra
        Call PFAgregarCampoSalida(Trim(dat9))             'L1 Nombre Compra
        Call PFAgregarCampoSalida(Trim(dat10))             'L2 Nombre Compra
        Call PFAgregarCampoSalida(Left(LTrim(dat11), 6))  'Tip.Doc.Compra
        Call PFAgregarCampoSalida(Left(LTrim(dat12), 11)) 'Nro.Doc.Compra
        Call PFAgregarCampoSalida(Left(LTrim(dat13), 1))  'Bien de Uso
        Call PFAgregarCampoSalida(Trim(dat14))            'L1 Domic Compra
        Call PFAgregarCampoSalida(Trim(dat15))            'L2 Domic Compra
        Call PFAgregarCampoSalida(Trim(dat16))            'L3 Domic Compra
        Call PFAgregarCampoSalida(Trim(dat17))            'L1 Remito
        Call PFAgregarCampoSalida(Trim(dat18))            'L2 Remito
        Call PFAgregarCampoSalida(Left(LTrim(dat19), 1))  'Tipo de tabla de Item
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFactOpen, 2)
        iret = isPFiscalOk(COMFactOpen) 'Chequeo si el comando fue bien ejecutado
        FISOpenFact = (continuar And iret)
    End Function

    Function FISSubTique(ByVal dat1 As Object, ByVal dat2 As Object) As Boolean
        'Imprime (en los casos posibles) y devuelve el subtotal
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Impresión (P o N)
        '       2 - (dat2) Descripción
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '       3 - Sin Uso
        '       4 - Cantidad de Items de línea
        '       5 - Total de mercadería o Total a pagar Bruto
        '       6 - Total de impuesto IVA
        '       7 - Total pago
        '       8 - Total de impuestos internos porcentuales
        '       9 - Total de impuestos internos fijos
        '       10 - Monto Neto total facturado sin impuestos
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 1)) 'Si Imprime o NO
        Call PFAgregarCampoSalida(Left(dat2, 25))  'Descripción
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMTicketSubtotal, 10) 'TIQUET
        iret = isPFiscalOk(COMTicketSubtotal) 'Chequeo si el comando fue bien ejecutado
        FISSubTique = (continuar And iret)
    End Function
    Function FISSubFact(ByVal dat1 As Object, ByVal dat2 As Object) As Boolean
        'Imprime (en los casos posibles) y devuelve el subtotal
        '---------------------------------------------------------
        'Recibe:
        '       Dat1=Impresión (P o N)
        '       Dat2=Descripción
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '       3 - Sin Uso
        '       4 - Cantidad de Items de línea
        '       5 - Total de mercadería o Total a pagar Bruto
        '       6 - Total de impuesto IVA
        '       7 - Total pago
        '       8 - Total de impuestos internos porcentuales
        '       9 - Total de impuestos internos fijos
        '       10 - Monto Neto total facturado sin impuestos
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer, A As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 1))  'Si Imprime o NO
        Call PFAgregarCampoSalida(Left(dat2, 25))  'Descripción
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFactSubtotal, 10)
        iret = isPFiscalOk(COMFactSubtotal) 'Chequeo si el comando fue bien ejecutado
        FISSubFact = (continuar And iret)
    End Function
    Function FISCancelaFact() As Boolean
        'Cancela una factura o tique factura
        'No recibe parametros
        '---------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left("Decripcion", 25))                 'Descripción
        'Call PFAgregarCampoSalida(fill_zeros("0000000001", 10, 2))  'Importe que pago
        Call PFAgregarCampoSalida("C")                                      'Cancelar con 'C'
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFactPago, 2)
        iret = isPFiscalOk(COMFactPago) 'Chequeo si el comando fue bien ejecutado
        FISCancelaFact = (continuar And iret)
    End Function
    Function FISPagoFact(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object)
        'Realiza un pago en una factura o tique factura
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Descripción del pago
        '       2 - (dat2) Importe del pago
        '       3 - (dat3) Tipo de pago
        '           T=Pago
        '           t=Reversion de pago
        '           D=Descuento
        '           R=Recargo
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '       3 - Resto de lo que falta pagar
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 25))             'Descripción
        Call PFAgregarCampoSalida(dat2)                       'Importe que pago
        Call PFAgregarCampoSalida(Left(dat3, 1))              'Calificación del Pago
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFactPago, 2)
        iret = isPFiscalOk(COMFactPago) 'Chequeo si el comando fue bien ejecutado
        FISPagoFact = (continuar And iret)
    End Function
    Function FISOpenNFis() As Boolean
        'Abre comprobante no fiscal
        '---------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMAbreNoFiscal, 2)
        iret = isPFiscalOk(COMAbreNoFiscal) 'Chequeo si el comando fue bien ejecutado
        FISOpenNFis = (continuar And iret)
    End Function
    Function FISCierraNFis() As Boolean
        'Cierra comprobante no fiscal
        '---------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '       3 - Nro de Documento No Fiscal recientemente emitido
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMCierraNoFiscal, 2)
        iret = isPFiscalOk(COMCierraNoFiscal) 'Chequeo si el comando fue bien ejecutado
        FISCierraNFis = (continuar And iret)
    End Function
    Function FISCortaPapel() As Boolean
        'Corta papel
        '---------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMCortaPapel, 2)
        If continuar Then
            iret = isPFiscalOk(COMCortaPapel) 'Chequeo si el comando fue bien ejecutado
        End If
        FISCortaPapel = (continuar And iret)
    End Function
    Function FISEnviaLineaNFis(ByVal dat1 As Object) As Boolean
        'Envia Linea no fiscal
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Texto no fiscal
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 40)) 'Linea de detalle
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMEnviaLineaNF, 2)
        iret = isPFiscalOk(COMEnviaLineaNF) 'Chequeo si el comando fue bien ejecutado
        FISEnviaLineaNFis = (continuar And iret)
    End Function
    Function FISSendItemFact(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object, ByVal dat4 As Object, ByVal dat5 As Object _
        , ByVal dat6 As Object, ByVal dat7 As Object, ByVal dat8 As Object, ByVal dat9 As Object, ByVal dat10 As Object, ByVal dat11 As Object _
        , ByVal dat12 As Object) As Boolean
        'Factura un item
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Descripcion del producto
        '       2 - (dat2) Cantidad de unidades
        '       3 - (dat3) Monto del item
        '       4 - (dat4) Tasa impositiva
        '       5 - (dat5) Calificador de línea de item ("M","m","R" o "r")
        '       6 - (dat6) Cantidad de Bultos
        '       7 - (dat7) Tasa de ajuste variable
        '       8 - (dat8) Linea extra 1
        '       9 - (dat9) Linea extra 2
        '       10 - (dat10) Linea extra 3
        '       11 - (dat11) Tasa de acrecentamiento
        '       12 - (dat12) Monto de impuestos internos fijos
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim iret As Integer, continuar As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Trim(dat1))                 'Descripción
        Call PFAgregarCampoSalida(Right("00000000" & dat2, 8))   'Cantidad
        Call PFAgregarCampoSalida(Right("000000000" & dat3, 9))   'Monto del Item
        Call PFAgregarCampoSalida(Right("0000" & dat4, 4))   'Tasa Impositiva
        Call PFAgregarCampoSalida(Left(LTrim(dat5), 1))       'Calificador del Item
        Call PFAgregarCampoSalida(Right("00000" & dat6, 5))   'Unidades o Bultos vendidos
        Call PFAgregarCampoSalida(Right("00000000" & dat7, 8))   'Tasa de Ajuste Variable
        Call PFAgregarCampoSalida(Trim(dat8))                 'Descr Complem. linea 1
        Call PFAgregarCampoSalida(Trim(dat9))                 'Descr Complem. linea 2
        Call PFAgregarCampoSalida(Trim(dat10))                 'Descr Complem. linea 3
        Call PFAgregarCampoSalida(Right("0000" & dat11, 4))  'Tasa Impositiva adicional RNI
        Call PFAgregarCampoSalida(Right("0000000000000000000" & dat12, 15))  'Monto de Impuestos Internos Fijos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFactSendItem, 11)
        iret = isPFiscalOk(COMFactSendItem) 'Chequeo si el comando fue bien ejecutado
        FISSendItemFact = (continuar And iret)
    End Function
    Function FISCierreFact(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object) As Boolean
        'Cierra una factura o tique factura
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Tipo de Documento (T,F o R)
        '       2 - (dat2) Letra de la Factura (A,B o C)
        '       3 - (dat3) Descripcion ??????????? no utilizada en tique-factura
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '       3 - Nro. del Documento fiscal que se acaba de emitir
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 1)) 'Tipo de Documento
        Call PFAgregarCampoSalida(Left(dat2, 1)) 'Letra de la Factura
        Call PFAgregarCampoSalida(dat3) 'Descripción de la Factura
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFactCierre, 2)
        iret = isPFiscalOk(COMFactCierre) 'Chequeo si el comando fue bien ejecutado
        FISCierreFact = (continuar And iret)
    End Function
    Function FISDnfhOS(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object, ByVal dat4 As Object, ByVal dat5 As Object _
        , ByVal dat6 As Object, ByVal dat7 As Object, ByVal dat8 As Object, ByVal dat9 As Object, ByVal dat10 As Object, ByVal dat11 As Object _
        , ByVal dat12 As Object, ByVal dat13 As Object, ByVal dat14 As Object, ByVal dat15 As Object, ByVal dat16 As Object, ByVal dat17 As Object _
        , ByVal dat18 As Object) As Boolean
        'Imprime un DNFH Obra Social
        '---------------------------------------------------------
        'Recibe:
        '       1 - (dat1) Nombre de la Obra Social
        '       2 - (dat2) Linea 1 Coseguro
        '       3 - (dat3) Linea 2 Coseguro
        '       4 - (dat4) Linea 3 Coseguro
        '       5 - (dat5) Nro. de Afiliado
        '       6 - (dat6) Nombre del Afiliado
        '       7 - (dat7) Fecha de vencimiento
        '       8 - (dat8) Linea 1 Domicilio Fiscal vendedor
        '       9 - (dat9) Linea 2 Domicilio Fiscal vendedor
        '       10 - (dat10) Numero o nombre del establecimiento
        '       11 - (dat11) Numero interno del comprobante
        '       12 - (dat12) Linea 1 Descripcion
        '       13 - (dat13) Linea 2 Descripcion
        '       14 - (dat14) Domicilio Imprime (P)
        '       15 - (dat15) Nro Documento (P)
        '       16 - (dat16) Firma (P)
        '       17 - (dat17) Aclaracion (P)
        '       18 - (dat18) Telefono (P)
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida("02")
        Call PFAgregarCampoSalida(Left(dat1, 20))
        Call PFAgregarCampoSalida(Left(dat2, 20))
        Call PFAgregarCampoSalida(Left(dat3, 20))
        Call PFAgregarCampoSalida(Left(dat4, 20))
        Call PFAgregarCampoSalida(Left(dat5, 20))
        Call PFAgregarCampoSalida(Left(dat6, 23))
        Call PFAgregarCampoSalida(Left(dat7, 6))
        Call PFAgregarCampoSalida(Left(dat8, 20))
        Call PFAgregarCampoSalida(Left(dat9, 20))
        Call PFAgregarCampoSalida(Left(dat10, 20))
        Call PFAgregarCampoSalida(Left(dat11, 20))
        Call PFAgregarCampoSalida(Left(dat12, 20))
        Call PFAgregarCampoSalida(Left(dat13, 20))
        Call PFAgregarCampoSalida(Left(dat14, 1))
        Call PFAgregarCampoSalida(Left(dat15, 1))
        Call PFAgregarCampoSalida(Left(dat16, 1))
        Call PFAgregarCampoSalida(Left(dat17, 1))
        Call PFAgregarCampoSalida(Left(dat18, 1))
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMSendDnfh, 3)
        iret = isPFiscalOk(COMSendDnfh)     'Chequeo si el comando fue bien ejecutado
        FISDnfhOS = (continuar And iret)
    End Function
    Function FISDnfhTC(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object, ByVal dat4 As Object, ByVal dat5 As Object _
        , ByVal dat6 As Object, ByVal dat7 As Object, ByVal dat8 As Object, ByVal dat9 As Object, ByVal dat10 As Object, ByVal dat11 As Object _
        , ByVal dat12 As Object, ByVal dat13 As Object, ByVal dat14 As Object, ByVal dat15 As Object, ByVal dat16 As Object, ByVal dat17 As Object _
        , ByVal dat18 As Object, ByVal dat19 As Object, ByVal dat20 As Object, ByVal dat21 As Object) As Boolean
        'Imprime un DNFH Tarjeta de Crédito
        '---------------------------------------------------------
        'Recibe:
        '       Dat1=Nombre de la Tarejta de Crédito
        '       Dat2=Nro Tarjeta
        '       Dat3=Nombre usuario
        '       Dat4=Fecha vencimiento
        '       Dat5=Nro de Establecimiento
        '       Dat6=Nro Cupon
        '       Dat7=Nro interno del comprobante
        '       Dat8=Código de Autorizacion
        '       Dat9=Tipo de operación
        '       Dat10=Importe
        '       Dat11=Cantidad de cuotas
        '       Dat12=Moneda
        '       Dat13=Nro de terminal
        '       Dat14=Nro de lote
        '       Dat15=Nro de terminal electrónica
        '       Dat16=Nro de sucursal
        '       Dat17=Nro o nombre del operador
        '       Dat18=Nro del Documento Fiscal al que se hace referencia
        '       Dat19=Firma (P)
        '       Dat20=Aclaracion (P)
        '       Dat21=Telefono (P)
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida("01")
        Call PFAgregarCampoSalida(Left(dat1, 20))
        Call PFAgregarCampoSalida(Left(dat2, 20))
        Call PFAgregarCampoSalida(Left(dat3, 23))
        Call PFAgregarCampoSalida(Left(dat4, 6))
        Call PFAgregarCampoSalida(Left(dat5, 20))
        Call PFAgregarCampoSalida(Left(dat6, 20))
        Call PFAgregarCampoSalida(Left(dat7, 20))
        Call PFAgregarCampoSalida(Left(dat8, 20))
        Call PFAgregarCampoSalida(Left(dat9, 20))
        Call PFAgregarCampoSalida(Left(dat10, 9))
        Call PFAgregarCampoSalida(Left(dat11, 20))
        Call PFAgregarCampoSalida(Left(dat12, 20))
        Call PFAgregarCampoSalida(Left(dat13, 20))
        Call PFAgregarCampoSalida(Left(dat14, 20))
        Call PFAgregarCampoSalida(Left(dat15, 20))
        Call PFAgregarCampoSalida(Left(dat16, 20))
        Call PFAgregarCampoSalida(Left(dat17, 20))
        Call PFAgregarCampoSalida(Left(dat18, 20))
        Call PFAgregarCampoSalida(Left(dat19, 1))
        Call PFAgregarCampoSalida(Left(dat20, 1))
        Call PFAgregarCampoSalida(Left(dat21, 1))
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMSendDnfh, 3)
        iret = isPFiscalOk(COMSendDnfh)     'Chequeo si el comando fue bien ejecutado
        FISDnfhTC = (continuar And iret)
    End Function
    Function FISDescripcionExtra(ByVal dat1 As Object) As Boolean
        'Envia la Descripción EXTRA
        '---------------------------------------------------------
        'Recibe:
        '       dat1: Texto de la descripción extra
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 40))  'Descripción extra
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMTicketDescripcionExtra, 3)
        iret = isPFiscalOk(COMTicketDescripcionExtra) 'Chequeo si el comando fue bien ejecutado
        FISDescripcionExtra = (continuar And iret)
    End Function
    Function FISAuditarFecha(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object) As Boolean
        'Audita por Fecha
        '---------------------------------------------------------
        'Recibe:
        '       dat1=Fecha inicio
        '       dat2=Fecha final
        '       dat3=Tipo de auditoria
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 6)) 'Fecha Cierre Z Inicio
        Call PFAgregarCampoSalida(Left(dat2, 6)) 'Fecha Cierre Z Final
        Call PFAgregarCampoSalida(Left(dat3, 1)) 'Totales o detalle
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMAuditaFecha, 2)
        iret = isPFiscalOk(COMAuditaFecha) 'Chequeo si el comando fue bien ejecutado
        FISAuditarFecha = (continuar And iret)
    End Function
    Function FISAuditarZ(ByVal dat1 As Object, ByVal dat2 As Object, ByVal dat3 As Object) As Boolean
        'Audita por cierre Z
        '---------------------------------------------------------
        'Recibe:
        '       dat1=Cierre Z inicial
        '       dat2=Cierre Z final
        '       dat3=Tipo de auditoria (T o D)
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 4)) 'Cierre Z Inicio
        Call PFAgregarCampoSalida(Left(dat2, 4)) 'Cierre Z Final
        Call PFAgregarCampoSalida(Left(dat3, 1)) 'Totales o detalle
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMAuditaZ, 2)
        iret = isPFiscalOk(COMAuditaZ) 'Chequeo si el comando fue bien ejecutado
        FISAuditarZ = (continuar And iret)
    End Function
    Function FISAvanzarAmbos(ByVal dat1 As Object) As Boolean
        'Avanza ambos papeles
        '---------------------------------------------------------
        'Recibe:
        '       Dat1: Cantidad de lineas a avanzar
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 2)) 'Cantidad a avanzar
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMAvanzaAmbos, 2)
        iret = isPFiscalOk(COMAvanzaAmbos) 'Chequeo si el comando fue bien ejecutado
        FISAvanzarAmbos = (continuar And iret)
    End Function
    Function FISAvanzarAuditor(ByVal dat1 As Object) As Boolean
        'Avanza el papel auditor
        '---------------------------------------------------------
        'Recibe:
        '       Dat1: Cantidad de lineas a avanzar
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 2)) 'Cantidad a avanzar
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMAvanzaAuditor, 2)
        iret = isPFiscalOk(COMAvanzaAuditor) 'Chequeo si el comando fue bien ejecutado
        FISAvanzarAuditor = (continuar And iret)
    End Function
    Function FISAvanzarHoja(ByVal dat1 As Object) As Boolean
        'Avanza la hoja suelta
        '---------------------------------------------------------
        'Recibe:
        '       Dat1: Cantidad de lineas a avanzar
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 2)) 'Cantidad a avanzar
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMAvanzaHoja, 2)
        iret = isPFiscalOk(COMAvanzaHoja) 'Chequeo si el comando fue bien ejecutado
        FISAvanzarHoja = (continuar And iret)
    End Function
    Function FISAvanzarTique(ByVal dat1 As Object) As Boolean
        'Avanza papel tique
        '---------------------------------------------------------
        'Recibe:
        '       Dat1: Cantidad de lineas a avanzar
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer, A As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 2)) 'Cantidad a avanzar
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMAvanzaTique, 2)
        iret = isPFiscalOk(COMAvanzaTique) 'Chequeo si el comando fue bien ejecutado
        FISAvanzarTique = (continuar And iret)
    End Function
    Function FISDiagnostico() As Boolean
        'Envia el comando de diagnostico
        '---------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMDiagnostic, 2)
        iret = isPFiscalOk(COMDiagnostic) 'Chequeo si el comando fue bien ejecutado
        FISDiagnostico = (continuar And iret)
    End Function
    Function FISRipple() As Boolean
        'Envia el comando de ripple test
        '---------------------------------------------------------
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMRippleTest, 2)
        iret = isPFiscalOk(COMRippleTest) 'Chequeo si el comando fue bien ejecutado
        FISRipple = (continuar And iret)
    End Function
    Function FISPercepcion(ByVal dat1 As Object, ByVal dat2 As Object) As Boolean
        'Envia uina percepcion.
        '---------------------------------------------------------
        'Recibe:
        '       dat1=Descripcion
        '       dat2=Calificacion de la percepcion
        '       dat3=Monto de la percepcion
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Call PFAgregarCampoSalida(Left(dat1, 25))             'Descripción
        Call PFAgregarCampoSalida(Left(dat2, 1))              'Calificación de Percepcion
        'Call PFAgregarCampoSalida(fill_zeros(dat3, 9, 2))     'Importe que Percibe
        'Envia datos a impresora
        continuar = PFIEnviarComando(COMFactPercep, 2)
        iret = isPFiscalOk(COMFactPercep) 'Chequeo si el comando fue bien ejecutado
        FISPercepcion = (continuar And iret)
    End Function
    Function FISStatus(ByVal Tipo As Object) As Boolean
        ' Informa el ESTADO de la Impresora Fiscal
        ' Parametros : Tipo "T"= tradicional (por Compatibilidad)
        '                   "N"= Normal, compatible con TM-300AF RG 22
        '                   "P"= Información sobre las caracteristicas del controlador
        '                   "C"= Información sobre el contribuyente
        '                   "A"= Información sobre los contadores de Documentos
        '---------------------------------------------------------------------------
        ' Public garraysalida(0 To 30) As String  'Array de salida al IF
        ' Public garrayentrada(0 To 30) As String ' Array de entrada de datos del IF
        ' En posicon CERO se indican cuantos campos tienen datos
        ' si es cero el campo cero hay error y no tengo datos validos
        '-- Respuesta del Impresor ---------------------------------
        ' Modo <T>radicional o <N>ormal
        ' 1. Estado Impresora
        ' 2. Estado Fiscal
        ' 3. Numero del ultimo Ticket
        ' 4. Fecha del primer Ticket AAMMDD  ' Año ,Mes, Dia
        ' 5. Hora del Primer Ticket  HHMMSS  ' Hora, min,seg
        ' 6. Numero del Ultimo Cierre Z
        ' 7. Dato de Auditoria Parcial
        ' 8. Dato de Auditoria Total
        ' 9. Número de Seria
        ' 10. Dato de Auditoría (Firmware version)

        ' Modo <P> Caracteristicas del Controlador Fiscal
        ' 1. Estado Impresora                           <HHHH>
        ' 2. Estado Fiscal                              <HHHH>
        ' 3. Ancho de la Impresora a 10 CPI en Facturas <nnn>
        ' 4. Ancho de la Impresora a 12 CPI en Facturas <nnn>
        ' 5. Ancho de la Impresora a 17 CPI en Facturas <nnn>
        ' 6. Ancho en Columnas de Tique.       <nnn>
        ' 7. Cant. líneas de Validación        <nnn>
        ' 8. Imprime Tickets                   <a>
        ' 9. Imprime Tickets-Factura           <a>
        ' 10. Imprime Facturas                 <a>
        ' 11. Centavos para Cierre Z           <n>
        ' 12. Estación Principal Seleccionada  <aa>
        ' 13  Modelo de la Impresora   <aaaaaaaaaaaaaaa>

        ' Modo <E>xtendido
        ' 1. Estado Impresora
        ' 2. Estado Fiscal
        ' 3. Razon social 1
        ' 4. Razon social 2
        ' 5. Cant columnas a 10CPI
        ' 6. Cant columnas a 12CPI
        ' 7. Cant columnas a 17CPI
        ' 8. Imprime Tickets
        ' 9. Imprime Tickets-Factura
        ' 10. Imprime Facturas
        ' 11. Centavos para Cierre Z
        '---------------------------------------------------------
        Dim midato, MIRESPUESTA, midatoout As Object
        Dim A, numobjeto As Integer
        Dim continuar As Integer, iret As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        'Llena string de datos
        Select Case Tipo
            Case "T"
                'Modo tradicional NO lleva CAMPO
            Case "N", "P", "C", "A", "D"

                Call PFAgregarCampoSalida(Tipo)                     'Tipo de consulta
            Case Else
                'nada
        End Select
        'Envio el comando
        continuar = PFIEnviarComando(COMEstado, 14)
        iret = isPFiscalOk(COMEstado) 'Chequeo si el comando fue bien ejecutado
        FISStatus = (continuar And iret)
    End Function
    Function FISCajon(ByVal da1 As Object)
        'Funcion PFICajon(da1 As Variant)
        'Activa el Cajon Indicado por da1
        '---------------------------------------------------------
        'Recibe:
        '       Da1 =1,2 Nro de cajon de dinero
        'Retorna:
        '       en variable Public garrayentrada
        '       1 - Estado de la Impresora
        '       2 - Estado de la Placa Fiscal
        '---------------------------------------------------------
        Dim continuar As Integer, iret As Integer, A As Integer
        Call PFIniciarsalida()  'Inicializa string de datos
        Select Case da1
            Case 1
                continuar = PFIEnviarComando(COMCajon_1, 3)
                iret = isPFiscalOk(COMCajon_1) 'Chequeo si el comando fue bien ejecutado
            Case 2
                continuar = PFIEnviarComando(COMCajon_2, 3)
                iret = isPFiscalOk(COMCajon_2) 'Chequeo si el comando fue bien ejecutado
            Case Else
                MsgBox("Opción no contemplada en cajon de dinero")
        End Select
        FISCajon = (continuar And iret)
    End Function

    '*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!
    '     //////////////////////////////////////////////
    '    /// Codigo extraido de : IfComunicacion.bas //
    '   //////////////////////////////////////////////
    '*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!

    Function isPFiscalOk(ByVal comando As Integer) As Integer
        'Retorna TRUE si el comando se realizo correctamente segun los estados recibidos
        'El comando enviado puede ser usado en el futuro
        Dim status_print As Double
        Dim status_fiscal As Double
        Dim and_mask As Double
        status_print = Val("&H" + Trim(garrayentrada(1)))
        status_fiscal = Val("&H" + Trim(garrayentrada(2)))
        'Para testear el bit 15
        and_mask = 2 ^ 15
        isPFiscalOk = IIf((status_print And and_mask) Or (status_fiscal And and_mask) = 0, True, False)
    End Function
    Sub PFAgregarCampoSalida(ByVal dato As Object)
        'Agrega Datos a la Variable Public para que sea ENVIADA
        'Por el Protocolo, si el campo es CERO se envia un LIMITADOR
        If Len(dato) > 0 Then
            gDatoDelComando = gDatoDelComando & Chr$(UPRNCAMPO) & dato
        Else
            'Se está enviando un campo nulo
            gDatoDelComando = gDatoDelComando & Chr$(UPRNCAMPO)
        End If
    End Sub
    Function PFControlaCRC()
        ' **********************************************************************
        '  Nombre  : PFControlaCRC ()
        '  Funcion : Lee los Ultimos 4 Bytes del buffer para chequear el CRC
        '            Entrada:
        '          : Salida :
        '                    true  ==> En gDatoRecibido, VARIABLE Public QUEDA EL COMADO
        '
        '                    false ==> NO Encontrado. No llego o un Time out
        '
        '                    NAK   ==> La impresora Fiscal Informa ERROR de Recepcion
        '                              o esta mal el número de Comando
        '                              o hay problema con el Protocolo
        ' **********************************************************************
        Static EnviadoPrimerPaqueteDelDia  ' FLAG Importante
        Dim CRCDatoRecibido As String ' Alamacena el CRC
        Dim continuar As Integer
        Dim auxContadorBytesCRC As Integer
        Dim tfinal As Long
        Dim ASCentrada As Integer
        Dim A As Integer, aux As Integer, su As Integer
        Dim crcout As String
        Dim dummy As Integer
        Dim cdebug As Boolean
DoEvents: DoEvents() : DoEvents() : DoEvents()
        ' *** tiempo maximo 10 segundos ***
        Const ctiempomaximo = 10
        ' *** Flag para saber si hay error ***
        continuar = True
        ' *** Contador para saber si Llegaron los 4 Bytes del CRC ***
        auxContadorBytesCRC = 0
        ' *** El CRC lo voy a calcular ahora, por lo tanto lo pongo a CERO
        CRCDatoRecibido = ""
        ' *** Espero el Inicio de un Paquete hasta ctiempomaximo de segundos****
        tfinal = Timer + ctiempomaximo
        Do While (auxContadorBytesCRC < 4) And (Timer <= tfinal) And (continuar = True)
            ' *** Veo si llegaron los 4 bytes del CRC ***
            Do While (form_principal.prf_port.InBufferCount = 0) And (Timer <= tfinal)
                Debug.Print "Esperando.",
                DoEvents()
            Loop ' !!! WHILE port vacio AND (TIMER <= tfinal)
            ' *** Veo porque salio del loop ***
            Debug.Print("Llegaron: " & form_principal.prf_port.InBufferCount & " Bytes ", "PFControlaCRC")

            ' *** Veo si tengo un Time out ***
            If (Timer > tfinal) Then
                Debug.Print("Sale en PFControla CRC por TimeOut")
                continuar = False ' tengo un TIME OUT
                PFControlaCRC = False
            End If
            ' *** Analizo la informacion que entro ***
            Do While (form_principal.prf_port.InBufferCount > 0) And (continuar = True)
                ' *** Leo Un byte y lo Analizo ***
                ASCentrada = Asc(form_principal.prf_port.Input)
                Debug.Print("Bytes de CRC=", ASCentrada)
                Select Case ASCentrada
                    Case NAK
                    Case STX
                    Case ETX
                        ' *** Encontre un NAK ==> Hay error ***
                        ' *** Encontre un STX ==> Hay error ***
                        ' *** Encontre un ETX ==> Hay error ***
                        PFControlaCRC = NAK
                        ' *** Para salir loop ***
                        continuar = False
                    Case MASTIEMPO
                        ' *** Si el Comando consume mucho tiempo
                        tfinal = tfinal + 1.1
                    Case Else
                        CRCDatoRecibido = CRCDatoRecibido + Chr$(ASCentrada)
                        auxContadorBytesCRC = auxContadorBytesCRC + 1
                End Select
            Loop
        Loop '
        ' *** Si sali hasta aqui, controlo el CRC ***
        If continuar = True Then
            ' *** El string Ingresado lo tengo en una Variable Public ***
            If Len(gDatoRecibido) < 3 Then PFErrProg(" String muy Corto ") : Exit Function

            For A = 1 To Len(gDatoRecibido)
                aux = Asc(Mid$(gDatoRecibido, A, 1))
                su = su + aux
            Next A
            su = su + STX
            su = su + ETX
            crcout$ = (Hex$(su))
            crcout$ = Right$(("000000" + crcout$), 4)
            ' *** Ahora veo los resultados
            ' *** Si NO son Iguales Tengo error de CRC ***
            If continuar = True Then
                If crcout$ <> CRCDatoRecibido Then
                    ' *** Tengo error de CRC ***
                    ' *** Encontre un NAK ==> Hay error ***
                    ' *** Encontre un STX ==> Hay error ***
                    ' *** Encontre un ETX ==> Hay error ***
                    PFControlaCRC = NAK
                    ' *** Para salir loop ***
                    If cdebug = True Then MsgBox(" !!!! Error de CRC en lo Recibido !!!!")
                    continuar = False
                End If
            End If ' !!! De Continuar = true
            ' *** Veo si los Numeros de PAQUETES son los Correctos ***
            If continuar = True Then
                If Mid$(gDatoEnviado, 2, 1) <> Mid$(gDatoRecibido, 1, 1) Then
                    ' *** Tengo error en el Numero de Paquete ***
                    ' *** Encontre un NAK ==> Hay error ***
                    ' *** Encontre un STX ==> Hay error ***
                    ' *** Encontre un ETX ==> Hay error ***
                    PFControlaCRC = NAK
                    ' *** Para salir loop ***
                    If cdebug = True Then MsgBox(" !!!! Error en Numero de Paquete Recibido !!!!")
                    continuar = False
                End If
            End If ' !!! De Continuar = true
            ' *** Veo si los Numeros de Comando son los Correctos ***
            If continuar = True Then
                If Mid$(gDatoEnviado, 3, 1) <> Mid$(gDatoRecibido, 2, 1) Then
                    ' *** Tengo error de Numero de Comando ***
                    ' *** Encontre un NAK ==> Hay error ***
                    ' *** Encontre un STX ==> Hay error ***
                    ' *** Encontre un ETX ==> Hay error ***

                    ' *** Si este error se da en el primer paquete del dia
                    If EnviadoPrimerPaqueteDelDia = False Then
                        If continuar = True Then
                            ' *** Incremento el numero de paquete ***
                            dummy = PFNumeroPaquete("P")
                        End If
                    End If
                    ' informo error , que se debe retransmitir ***
                    PFControlaCRC = NAK
                    ' *** Para salir loop ***
                    If cdebug = True Then MsgBox(" !!!! Error en Numero de Comandos en lo Recibido !!!!")
                    continuar = False
                End If
            End If ' !!! De Continuar = true
            ' *** Si todo esta bien hasta aqui ***
            ' *** Incremento el numero de paquete ***
            If continuar = True Then
                dummy = PFNumeroPaquete("P")
                EnviadoPrimerPaqueteDelDia = True
                PFControlaCRC = True
            End If
            ' *** Si Tube error de ALGO , borro el STRING ***
            If continuar = False Then gDatoRecibido = ""
        End If ' !!! De Continuar = True
    End Function
    Function PFCRC$(ByVal entrada$)
        Dim A As Integer, aux As Integer, su As Integer
        Dim crcout As String
        ' *** De DEMO al Cliente ***
        If Len(entrada$) < 3 Then PFErrProg(" String muy Corto ") : Exit Function
        For A = 1 To Len(entrada$)
            aux = Asc(Mid$(entrada$, A, 1))
            su = su + aux
        Next A
        crcout$ = LTrim$(Hex$(su))
        crcout$ = Right$(("000000" + crcout$), 4)
        PFCRC$ = crcout$
    End Function

    Function PFEnviaString(ByVal comando, ByVal dato As String) As Integer
        '  Nombre  : PFEnviaString (fh, comando, dato$)
        '  Funcion : Envia un string a la Impresora Fiscal
        '            Entrada:
        '                     comando= numero del comando que se ejecuta
        '                     dato$= String con Limitadores que se desean enviar
        '            salida:
        '                     true=OK
        '                     False=Problemas
        ' **********************************************************************
        Dim continuar As Integer
        Dim datoout As String, CHK As String
        continuar = True
        'Fuerzo a false hubo_error_com para reintentar un nuevo envio del comando
        hubo_error_com = False  'VARIABLE Public que indica si hubo error en el PORT Serie
        ' *** Preparo el Paquete
        ' *** Controlo Valor del Comando ****
        If comando > &H7F Then
            MsgBox(" Valor de Comando muy Grande")
            continuar = False
        End If
        ' *** Controlo Longitud datos *****
        If (Len(dato$) > PFI_MAX_LONG_RECIBIDA) Then
            MsgBox("String de Datos demasiado grande- Verifique ")
            continuar = False
        End If
        ' *** Controlo Que EMPIEZA  con Limitador *****
        If Len(dato) > 0 Then
            If Mid$(dato, 1, 1) <> Chr$(UPRNCAMPO) Then
                MsgBox("El string a ENVIAR debe tener un limitador en el Primer Caracter para ser interpretado por el Impresor Fiscal ")
                continuar = False
            End If
        End If
        If continuar = True Then
            datoout$ = Chr$(STX) + Chr$(PFNumeroPaquete("P")) + Chr$(comando) + dato + Chr$(ETX)
            CHK$ = PFCRC(datoout$)
            datoout$ = datoout$ + CHK$
            gDatoEnviado = datoout$
            form_principal.prf_port.Output = gDatoEnviado
        End If
        'Hubo_error_com es seteado por el evento on_comm de prf_port
        'En caso de error de comunacación fuerza False
        If hubo_error_com = True Then
            PFEnviaString = False
        Else
            PFEnviaString = continuar
        End If
    End Function
    Function PFEsperaFinPaquete()
        '  Nombre  : PFEsperaFinPaquete ()
        '  Funcion : Espera por el FIB de un Paquete
        '            Entrada:
        '                     Ninguna
        '            Salida :
        '                    gDatoRecibido= Variable Public donde Almacena
        '                                   la informacion recibida si es valida
        '                    true  ==> Encontrado
        '                    false ==> NO Encontrado. No llego o un Time out
        '                    NAK   ==> La impresora Fiscal Informa ERROR de Recepcion
        ' **********************************************************************
        Dim ETXencontrado As Integer
        Dim continuar As Integer
        Dim tfinal As Long
        Dim xpos As Integer
        Dim ASCentrada As Integer
        ' *** tiempo maximo 10 segundos ***
        Const ctiempomaximo = 10
        ' *** Flag para saber si hay error ***
        continuar = True
        ' *** Valido el FH
        ' *** Flag para saber si encontre un ETX ***
        ETXencontrado = False
        ' *** Espero el Inicio de un Paquete hasta ctiempomaximo de segundos****
        tfinal = Timer + ctiempomaximo
        Do While (ETXencontrado = False) And (Timer <= tfinal) And (continuar = True)
            ' *** Veo si llego algo ***
            Do While (form_principal.prf_port.InBufferCount = 0) And (Timer <= tfinal)
                DoEvents()
            Loop ' !!! WHILE port vacio AND (TIMER <= tfinal)

            ' *** Veo si tengo un Time out ***
            If (Timer > tfinal) Then
                continuar = False ' tengo un TIME OUT
                PFEsperaFinPaquete = False
            End If
            ' *** Analizo la informacion que entro ***
            Do While (form_principal.prf_port.InBufferCount > 0) And (ETXencontrado = False) And (continuar = True)
                ' *** Leo Un byte y Analizo
                ASCentrada = Asc(form_principal.prf_port.Input)
                Select Case ASCentrada
                    Case NAK
                    Case STX
                        ' *** Encontre un NAK ==> Hay error ***
                        ' *** Encontre un STX ==> Hay error ***
                        PFEsperaFinPaquete = NAK
                        ' *** Para salir loop ***
                        ETXencontrado = True
                        ' *** Borro datos recibidos para evitar problemas ***
                        gDatoRecibido = ""
                    Case ETX
                        ' *** Encontre el ETX ***
                        PFEsperaFinPaquete = True
                        ' *** Para salir loop ***
                        ETXencontrado = True

                    Case MASTIEMPO
                        ' *** Si el Comando consume mucho tiempo
                        tfinal = tfinal + 1
                    Case Else
                        gDatoRecibido = gDatoRecibido + Chr$(ASCentrada)
                End Select
            Loop '!!! De WHILE (form_principal.prf_port.inbuffercount > 0) AND (ETXecontrado = False) AND (Continuar = True)
        Loop '!!! De (ETXecontrado = False) AND (TIMER <= tfinal) AND (continuar = True)
    End Function

    Function PFEsperaInicioPaquete()
        '  Nombre  : PFEsperaInicioPaquete (fh)
        '  Funcion : Espera por el PRINCIPIO de un Paquete
        '            Entrada:
        '                     fh= File Handel del OPEN
        '          : Salida :
        '                    true  ==> Encontrado
        '                    false ==> NO Encontrado. No llego o un Time out
        '                    NAK   ==> La impresora Fiscal Informa ERROR de Recepcion
        ' Autor: Carlos Marcante
        ' **********************************************************************
        Dim STXencontrado As Integer
        Dim continuar As Integer
        Dim tfinal As Long
        Dim ASCentrada As Integer
        ' *** tiempo maximo 10 segundos ***
        Const ctiempomaximo = 10
        ' *** Si voy a enviar y recibir un paquete, borro el ultimo recibido
        gDatoRecibido = ""
        ' *** Flag para saber si hay error ***
        continuar = True
        ' *** Flag para saber si encontre un STX ***
        STXencontrado = False
        ' *** Espero el Inicio de un Paquete hasta ctiempomaximo de segundos****
        tfinal = Timer + ctiempomaximo
        Do While (STXencontrado = False) And (Timer <= tfinal) And (continuar = True)
            ' *** Veo si llego algo ***
            Do While (form_principal.prf_port.InBufferCount = 0) And (Timer <= tfinal)
                DoEvents()
            Loop ' !!! WHILE port vacio AND (TIMER <= tfinal)
            ' *** Veo si tengo un Time out ***
            If (Timer > tfinal) Then
                continuar = False ' tengo un TIME OUT
                PFEsperaInicioPaquete = False
            End If
            If continuar = True Then
                'Analizo el primer Byte que entro
                ASCentrada = Asc(form_principal.prf_port.Input)
                'Elimino el primer carecter de gDatoRecibido
                Select Case ASCentrada
                    Case NAK
                        ' *** Encontre un NAK ==> Hay error ***
                        PFEsperaInicioPaquete = NAK
                        ' *** Para salir loop ***
                        STXencontrado = True
                    Case STX
                        ' *** Encontre el STX ***
                        PFEsperaInicioPaquete = True
                        ' *** Para salir loop ***
                        STXencontrado = True
                    Case MASTIEMPO
                        ' *** Si el Comando consume mucho tiempo
                        tfinal = tfinal + 1
                    Case Else
                        ' *** Si llega Basura, LIMPIO el BUffer
                        Call PFLimpiarBufferCOM()
                        ' *** Incremento Tiempo para NO tener FALSO TimeOut
                        tfinal = tfinal + 1
                End Select
            End If ' !!! de Continuar = True
        Loop ' !!! de (STXecontrado = False) AND (TIMER <= tfinal) AND (Continuar = True)
    End Function

    Function PFICheckOnComm(ByVal evento As Integer) As Integer
        Dim iret As Integer
        'Si el evento es >1000 indica que se produjo algún error de comunicación
        'entonces retorno falso
        If evento > 1000 Then
            iret = True
        Else
            iret = False
        End If
        PFICheckOnComm = iret
    End Function
    Function PFIEnviarComando(ByVal NumeroDeComando, ByVal CantidadCamposRecibidos)
        '  Nombre  : PFIEnviarComando (NumeroDeComando)
        '  Funcion : Envia un COMAMDO
        '            Entrada:
        '                     comando= numero del comando que se ejecuta
        '                              De una Variable Public se obtiene el String a enviar
        '                     CantidadCamposRecibidos= Cuantos Campos se obtienen
        '            salida:
        '                     true=OK
        '                     False=Problemas
        ' **********************************************************************
        ' *** En laVariable Public gDatoDelComando se ponen los datos de SALIDA
        Dim continuar As Integer
        Dim HayErrorProtocolo As Integer
        Dim HayErrorCom As Integer
        Dim Resultado As Integer
        Dim Cant_Reenvios As Integer
        Dim A As Integer
DoEvents: DoEvents() : DoEvents() : DoEvents()
        ' *** Pongo Contador de Errores a cero ***
        Static contadorerrores As Integer
        contadorerrores = 0
        ' *** Pongo condiciones iniciales ***
        continuar = False
        HayErrorProtocolo = True
        HayErrorCom = True
        'Cantidad de reenvios
        Cant_Reenvios = 1
        Do While (contadorerrores < Cant_Reenvios) And ((HayErrorCom = True) Or (HayErrorProtocolo = True)) And (continuar = False)
            ' *** Pongo para que se retransmita ***
            continuar = True
            ' *** Envio un String ***

            If continuar = True Then Resultado = PFEnviaString(NumeroDeComando, gDatoDelComando)

            Select Case Resultado
                Case True                       ' ==> Encontrado
                    HayErrorCom = False        ' No hay error en COM
                    HayErrorProtocolo = False  ' No se debe Retransmitir
                    continuar = True           ' Procesar Siguiente ETAPA

                Case False                      ' ==> NO Encontrado. No llego o un Time out
                    HayErrorCom = True         ' Hay problemas con la comunicaci'on. Es un Time OUT
                    HayErrorProtocolo = False  ' No hay error en Protocolo , es un TIME out
                    continuar = False          ' NO Procesar Siguiente ETAPA
            End Select

            ' *** Espero el INICIO de un Paquete de DATOS ***
            If continuar = True Then
DoEvents:       DoEvents() : DoEvents() : DoEvents()
                Resultado = PFEsperaInicioPaquete()

                Select Case Resultado
                    Case True                       ' ==> Encontrado
                        HayErrorCom = False        ' No hay error en COM
                        HayErrorProtocolo = False  ' No se debe Retransmitir
                        continuar = True           ' Procesar Siguiente ETAPA
                    Case False                      ' ==> NO Encontrado. No llego o un Time out
                        HayErrorCom = True         ' Hay problemas con la comunicaci'on. Es un Time OUT
                        HayErrorProtocolo = False  ' No hay error en Protocolo , es un TIME out
                        continuar = False          ' NO Procesar Siguiente ETAPA

                    Case NAK                        ' ==> La impresora Fiscal Informa ERROR de Recepcion
                        HayErrorCom = False        ' La comunicacion se realizo
                        HayErrorProtocolo = True   ' Hay error en Protocolo, Se debe retransmitir
                        continuar = False          ' NO Procesar Siguiente ETAPA
                End Select
            End If


            If continuar = True Then
DoEvents:       DoEvents() : DoEvents() : DoEvents()
                Resultado = PFEsperaFinPaquete()
                ' *** Analizo la salida de encontrar el ETX ***
                Select Case Resultado
                    Case True                       ' ==> Encontrado
                        Debug.Print("LLego el FIN de PAQUETE")
                        HayErrorCom = False        ' No hay error en COM
                        HayErrorProtocolo = False  ' No se debe Retransmitir
                        continuar = True           ' Procesar Siguiente ETAPA

                    Case False                      ' ==> NO Encontrado. No llego o un Time out
                        HayErrorCom = True         ' Hay problemas con la comunicaci'on. Es un Time OUT
                        HayErrorProtocolo = False  ' No hay error en Protocolo , es un TIME out
                        continuar = False          ' NO Procesar Siguiente ETAPA

                    Case NAK                        ' ==> La impresora Fiscal Informa ERROR de Recepcion
                        HayErrorCom = False        ' La comunicacion se realizo
                        HayErrorProtocolo = True  ' Hay error en Protocolo, Se debe retransmitir
                        continuar = False          ' NO Procesar Siguiente ETAPA
                End Select
            End If

            If continuar = True Then
DoEvents:       DoEvents() : DoEvents() : DoEvents()
                Resultado = PFControlaCRC()
                ' *** Analizo la salida Luego de Levantar el CRC ***
                Select Case Resultado
                    Case True                       ' ==> Encontrado
                        Debug.Print("CRC BIEN")
                        HayErrorCom = False        ' No hay error en COM
                        HayErrorProtocolo = False  ' No se debe Retransmitir
                        continuar = True           ' Procesar Siguiente ETAPA

                    Case False                      ' ==> NO Encontrado. No llego o un Time out
                        Debug.Print("CRC MAL")
                        HayErrorCom = True         ' Hay problemas con la comunicación.
                        HayErrorProtocolo = False  ' NO Hay error en Protocolo , Problema con CRC
                        continuar = False          ' NO Procesar Siguiente ETAPA

                    Case NAK                        ' ==> La impresora Fiscal Informa ERROR de Recepcion
                        HayErrorCom = False        ' La comunicacion se realizo
                        HayErrorProtocolo = True   ' Hay error en Protocolo, Se debe retransmitir
                        continuar = False          ' NO Procesar Siguiente ETAPA
                End Select
            End If
            ' *** Si hay error, reintento
            If HayErrorProtocolo = True Then contadorerrores = contadorerrores + 1
            If HayErrorCom = True Then contadorerrores = contadorerrores + 1
            ' *** Informo Flags temporales en caso de estar en modo DESAROLLO ***
            If gNivel = UPRN_LEVEL_DEVELOPER Then
                If HayErrorCom = True Then MsgBox(" Hay error de Comunicacion. Puede ser un Time OUT", , "G&C--Resultado ----") ': Stop
                If HayErrorProtocolo = True Then MsgBox(" Hay error en Protocolo, Se debe retransmitir ", , "G&C--Resultado ----")
            End If
        Loop ' De!!! WHILE (contadorerrores < 4) AND (Continuar = True)
        ' *** Pongo el Dato de Salida ***
        PFIEnviarComando = continuar
        ' *** Ahora tomo los Datos de Salida y los guardo en los campos ***
        ' *** Lo menos que se reciben son dos campos ***
        If CantidadCamposRecibidos <= 3 Then CantidadCamposRecibidos = 2
        ' *** Saco los Campos ***
        ' *** Si todo OK ***
        If continuar = True Then
            For A = 1 To (CantidadCamposRecibidos + 1)
                garrayentrada(A) = UPrn_SacarCampo(gDatoRecibido + Chr(0), A)
            Next A
            garrayentrada(0) = Str(CantidadCamposRecibidos + 1)
        End If
        ' *** Si HUBO error, LIMPIO los campos ***
        If continuar = False Then
            For A = 1 To CantidadCamposRecibidos
                garrayentrada(A) = " "
            Next A
            garrayentrada(0) = "0"
            garrayentrada(1) = ""
            garrayentrada(2) = ""
            garrayentrada(3) = ""
            MsgBox("ERROR de COMUNICACION con la Impresora Fiscal" & Chr$(13) & Chr$(13) & "Revise las conecciones, si el equipo esta encendido y si usa la puerta serie correcta.", vbCritical, App.EXEName)
        End If
    End Function
    Sub PFIniciarsalida()
        ' *** Borra la Variable de Salida ***
        gDatoDelComando = ""    'Limpio la Salida
        garrayentrada(0) = "0"   'Limpio Mensaje de error 1
        garrayentrada(1) = ""   'Limpio Mensaje de error 1
        garrayentrada(2) = ""   'Limpio Mensaje de error 2
        garrayentrada(3) = ""   'Limpio Mensaje de error 3
        garrayentrada(4) = ""   'Limpio Mensaje de error
        garrayentrada(5) = ""   'Limpio Mensaje de error
        garrayentrada(6) = ""   'Limpio Mensaje de error
        garrayentrada(7) = ""   'Limpio Mensaje de error
    End Sub
    Sub PFLimpiarBufferCOM()
        '  Nombre  : PFLimpiarBufferCOM ()
        '  Funcion : Limpia los datos almacenados en el Buffer Serie
        '            Entrada:
        '                     Ninguna
        '          : Salida :
        '                     Ninguna
        ' **********************************************************************
        Dim Nada
        Do While form_principal.prf_port.InBufferCount > 0
            Nada = form_principal.prf_port.Input
            DoEvents()
        Loop
    End Sub
    Function PFNumeroPaquete(ByVal orden$)
        '  Nombre:PFNumeroPaquete (orden$)
        '  Funcion : Maneja el Numero de Paquete
        '            Entrada: "U" = Informa el Ultimo Numero de Paquete
        '                     "P" = Pasa al Proximo numero de Paquete
        '            Salida : Numero de Paquete
        ' **********************************************************************
        Static NumeroPaquete As Integer
        ' *** Para la Primera Vez ***
        If NumeroPaquete = 0 Then NumeroPaquete = (&H20) + Int(Rnd(1) * 20)
        ' *** Si pide el ultimo ****
        If UCase$(orden$) = "U" Then
            NumeroPaquete = NumeroPaquete
        End If

        ' *** Si pide el Proximo ****
        If UCase$(orden$) = "P" Then
            NumeroPaquete = NumeroPaquete + 1
        End If
        ' *** Si llegue al limite Superior ****
        If NumeroPaquete > &H7F Then NumeroPaquete = &H20
        ' *** Pongo Valor de Retorno ****
        PFNumeroPaquete = NumeroPaquete
    End Function

    Function UPRNCampoRecibido$(ByVal da2)
        ' TOMA la ENTRADA de una VARIABLE Public
        '
        ' UPRNSacarCampo$ (da1 AS STRING, DA2)
        ' Dado un número   |111|22222|33333|44444|555555
        ' Le pido el campo de ese número
        ' en da1 ingresa el string
        ' en da2 ingresa el número
        Dim aux, posi1, posi2, posiencontrada, proxbusqueda
        Dim msg As String
        Dim da1 As String
        da1 = gDatoRecibido
        ' posiencontrada es un contador de posicion
        posiencontrada = 0
        If da2 <= 0 Then
            msg = "Error, se pide un campo menor a 1"
            MsgBox(msg)

            UPRNCampoRecibido$ = "ERROR en sacar campo"
            Exit Function
        End If
        ' *** Primero debo Ubicar el primer caracter ***
        proxbusqueda = 1    ' Seteo la proximo posicion dentro del string
        Do While proxbusqueda < Len(da1) And posiencontrada <> da2
            posi1 = InStr(proxbusqueda, da1, Chr$(UPRNCAMPO))
            If posi1 > 0 Then
                posiencontrada = posiencontrada + 1
            Else
                Exit Do
            End If
            proxbusqueda = posi1 + 1
        Loop  'proxbusqueda < Len(da1)
        ' *** Aqui tendria la posicion si posiencontrada=da2 ***
        If posiencontrada <> da2 Then   ' No llegue error
            ' si no se encuentra el campo puede ser que es una respuesta con error y no existe
            UPRNCampoRecibido = cNoSeRecibio
            Exit Function
        End If
        ' *** Busco la Posicion DOS ao el LIMITADOR DOS                    ***
        ' *** El comportamiento de esta parte depende del BASIC QUE SE USA ***
        ' *** por si se envia o no el caracter 0x00H                       ***
        posi2 = InStr(proxbusqueda, da1, Chr$(UPRNCAMPO))
        If posi2 = 0 Then
            '  *** Asumo el Final del String ***
            posi2 = Len(da1) + 1
            If posi2 = 0 Then
                MsgBox("Error en UPRNSacarCampo$ - No hallo el fin del String ")
            End If
        End If
        UPRNCampoRecibido = Mid$(da1, (posi1 + 1), (posi2 - posi1 - 1))
        Exit Function
    End Function
    Sub UPRNretardo(ByVal da1)
        Dim maxtime  'AS STRING
        Dim gcancelar As Integer
        If gcancelar = True Then Exit Sub
        If da1 > 10 Then da1 = 10
        maxtime = Timer + da1
        'While (ans = 0 And maxtime < Timer)                   ' Si es 0 No termino el comando
        Do While (True)     ' lazo infinito
            If Timer > maxtime Then    ' new
                Exit Do             ' new
            End If
        Loop
    End Sub
    Sub PFErrProg(ByVal mensa As String)
        MsgBox(mensa, 0, "* *  E R R O R  * *")
    End Sub
    Function UPrn_SacarCampo(ByVal da1 As Object, ByVal da2 As Object)
        ' Dado un número   |111|22222|33333|44444|555555
        ' Le pido el campo de ese número
        ' en da1 ingresa el string
        ' en da2 ingresa el número
        Dim aux, posi1, posi2, posiencontrada, proxbusqueda As Object
        Dim msg As Object
        ' posiencontrada es un contador de posicion
        posiencontrada = 0
        DoEvents()
        'da1 = Chr(UPRNCAMPO) & "11111" & Chr(UPRNCAMPO) & "212" & Chr(UPRNCAMPO) & "333" & Chr(UPRNCAMPO) & "4444"
        'da2 = 2
        If da2 <= 0 Then
            DoEvents()
            msg = IIf(cspanish, "Error, se pide un campo menor a 1", "ERROR. You want a field below field number 1")
            MsgBox(msg)

            UPrn_SacarCampo = IIf(cspanish, "ERROR en sacar campo", "ERROR get a field")
            Exit Function
        End If
        ' Primero debo Ubicar el primer caracter
        proxbusqueda = 1    ' Seteo la proximo posicion dentro del string
        Do While proxbusqueda < Len(da1) And posiencontrada <> da2
            DoEvents()
            posi1 = InStr(proxbusqueda, da1, Chr(UPRNCAMPO))
            If posi1 > 0 Then
                posiencontrada = posiencontrada + 1
            Else
                Exit Do
            End If
            proxbusqueda = posi1 + 1
        Loop  'proxbusqueda < Len(da1)
        ' Aqui tendria la posición si posiencontrada=da2
        If posiencontrada <> da2 Then   ' No llegue error
            ' si no se encuentra el campo puede ser que es una respuesta con error y no existe
            UPrn_SacarCampo = cNoSeRecibio
            DoEvents()
            Exit Function
        End If
        DoEvents()
        posi2 = InStr(proxbusqueda, da1, Chr(UPRNCAMPO))
        If posi2 = 0 Then
            'veo si encuentro el 00H
            posi2 = InStr(proxbusqueda, da1, Chr(0))
            If posi2 = 0 Then
                MsgBox("Error en UPRN_sacarcampo - No hallo el fin del String ")
            End If
        End If
        UPrn_SacarCampo = Mid(da1, (posi1 + 1), (posi2 - posi1 - 1))
        Exit Function
        ' Aqui tendria la posición si posiencontrada=da2
        ' Busco el Otro limitador
    End Function








    'Public Function Impresion_control(Id As Integer, Optional mOtros As String) As Integer
    '    '// Si ID < 0  ==> Pide estado  !!!
    '    '// Si ID >= 0 ==> Graba Estado !!!
    '    '// Definicion de Errores (Impresion_Errores)
    '    '//         * 3 Apertura de Ticket / Factura
    '    '//         * 4 Impresión de Items
    '    '//         * 5 Formas de Pago
    '    '//         * 6 Cierre
    '    '//         * 7 Status T
    '    If Id < 0 Then
    '        Dim rstControl As New ADODB.Recordset
    '        Set rstControl = Gs_BaseGen.Execute("SELECT * FROM control WHERE lcase(identificador) = 'imprimiendo'")
    '        With rstControl
    '            Impresion_control = Val("" & !dato)
    '            mSaleN(20) = Trim("" & !Otros)
    '            .Close
    '        End With
    '        Set rstControl = Nothing
    '    Else
    '        Gs_BaseGen.Execute "UPDATE control SET dato = " & Id & ", otros = '" & Trim(mOtros) & "' WHERE lcase(identificador) = 'imprimiendo'"
    '    End If
    'End Function
    '''''''''''''Public Function Impresion_Servidor()
    '''''''''''''    Dim rstTicketEnc As New ADODB.Recordset
    '''''''''''''    Select Case Val(Impresion_control(-1))
    '''''''''''''        Case 0 '// no hay nada
    '''''''''''''            Set rstTicketEnc = Gs_BaseGen.Execute("SELECT comprobante FROM TicketEnc")
    '''''''''''''            With rstTicketEnc
    '''''''''''''                If Not .EOF Then            '    // hay Tickets para Imprimir
    '''''''''''''                    If Not (Fiscal.FISStatus("T")) Then
    '''''''''''''                        Impresion_control 7     '// Impresora no esta en estado
    '''''''''''''                        DoEvents: DoEvents
    '''''''''''''                        .Close
    '''''''''''''                        Set rstTicketEnc = Nothing
    '''''''''''''                        Exit Function
    '''''''''''''                    End If
    '''''''''''''                    DoEvents: DoEvents
    '''''''''''''                    Impresion_control 1 '// Empezo a Imprimir
    '''''''''''''                    If UCase(Trim("" & !comprobante)) = "T" Then
    '''''''''''''                        impresion_ticket
    '''''''''''''                    Else
    '''''''''''''                        impresion_Factura
    '''''''''''''                    End If
    '''''''''''''                End If
    '''''''''''''                .Close
    '''''''''''''            End With
    '''''''''''''            Set rstTicketEnc = Nothing
    '''''''''''''        Case 1 '// imprimiendo
    '''''''''''''        Case Is > 1 '// cancelado, numero error
    '''''''''''''    End Select
    '''''''''''''End Function
    '''''''''''''Private Function impresion_ticket()
    '''''''''''''Dim rstDatos As New ADODB.Recordset
    '''''''''''''Dim Correcto As Boolean
    '''''''''''''Dim mNum As String
    '''''''''''''    Correcto = Fiscal.FISOpenTicket("T") '// Abrimos Ticket
    '''''''''''''    If fBinario(garrayentrada(1), 14) = "1" Or fBinario(garrayentrada(1), 4) = "1" Then
    '''''''''''''        MsgBox "El Impresor Fiscal se Encuentra sin Papel", vbInformation, "CAMBIE EL PAPEL"
    '''''''''''''        Impresion_control 3 '// Error al Abrir
    '''''''''''''        DoEvents: DoEvents
    '''''''''''''        Fiscal.FISCancelaTique
    '''''''''''''        Exit Function
    '''''''''''''    End If
    '''''''''''''    If Not Correcto Then
    '''''''''''''        Impresion_control 3 '// Error al Abrir
    '''''''''''''        DoEvents: DoEvents
    '''''''''''''        Fiscal.FISCancelaTique
    '''''''''''''        Exit Function
    '''''''''''''    End If
    '''''''''''''    Set rstDatos = Gs_BaseGen.Execute("SELECT * FROM TicketItems")
    '''''''''''''    With rstDatos '// Imprime los Items
    '''''''''''''        Do While Not .EOF
    '''''''''''''            DoEvents: DoEvents
    '''''''''''''            Fiscal.PFLimpiarBufferCOM
    '''''''''''''            Correcto = Fiscal.FISSendItemTique(Trim("" & !Detalle), Trim("" & !Cantidad), Trim("" & !Precio), "2100", "M", "1", "0000", "0000")
    '''''''''''''            If fBinario(garrayentrada(1), 14) = "1" Or fBinario(garrayentrada(1), 4) = "1" Then
    '''''''''''''                MsgBox "El Impresor Fiscal se Encuentra sin Papel", vbInformation, "CAMBIE EL PAPEL"
    '''''''''''''                Impresion_control 3 '// Error al Abrir
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                Fiscal.FISCancelaTique
    '''''''''''''                Exit Function
    '''''''''''''            End If
    '''''''''''''            If Not Correcto Then
    '''''''''''''                Impresion_control 4 '// Error al Imprimir Items
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos Items
    '''''''''''''                Fiscal.FISCancelaTique
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            Else
    '''''''''''''                .MoveNext
    '''''''''''''            End If
    '''''''''''''        Loop
    '''''''''''''        .Close
    '''''''''''''    End With
    '''''''''''''    Set rstDatos = Nothing
    '''''''''''''    Set rstDatos = Gs_BaseGen.Execute("SELECT * FROM TicketPagos")
    '''''''''''''    With rstDatos '// Imprime las Formas de Pago
    '''''''''''''        Do While Not .EOF
    '''''''''''''            DoEvents: DoEvents
    '''''''''''''            Fiscal.PFLimpiarBufferCOM
    '''''''''''''            Correcto = Fiscal.FISPagoTique(Trim("" & !Detalle), Trim("" & !monto), Trim("" & !Tipo))
    '''''''''''''            If fBinario(garrayentrada(1), 14) = "1" Or fBinario(garrayentrada(1), 4) = "1" Then
    '''''''''''''                MsgBox "El Impresor Fiscal se Encuentra sin Papel", vbInformation, "CAMBIE EL PAPEL"
    '''''''''''''                Impresion_control 3 '// Error al Abrir
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                Fiscal.FISCancelaTique
    '''''''''''''                Exit Function
    '''''''''''''            End If
    '''''''''''''            If Not Correcto Then
    '''''''''''''                Impresion_control 5 '// Error al Imprimir Forma de Paqo
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos tabla
    '''''''''''''                Fiscal.FISCancelaTique
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            Else
    '''''''''''''                .MoveNext
    '''''''''''''            End If
    '''''''''''''        Loop
    '''''''''''''        .Close
    '''''''''''''    End With
    '''''''''''''    Set rstDatos = Nothing
    '''''''''''''    Fiscal.PFLimpiarBufferCOM
    '''''''''''''    Correcto = Fiscal.FISCierreTique
    '''''''''''''    mNum = Trim(garrayentrada(3))
    '''''''''''''    Impresion_control 2, mNum
    '''''''''''''End Function
    '''''''''''''Private Function impresion_Factura()
    '''''''''''''Dim rstDatos As New ADODB.Recordset
    '''''''''''''Dim Correcto As Boolean
    '''''''''''''Dim mLetra As String * 1, mNum As String
    '''''''''''''    Set rstDatos = Gs_BaseGen.Execute("SELECT * FROM TicketEnc")
    '''''''''''''    With rstDatos '// Imprime Encabezado
    '''''''''''''        If Not .EOF Then
    '''''''''''''            DoEvents: DoEvents
    '''''''''''''            Fiscal.PFLimpiarBufferCOM
    '''''''''''''            Correcto = Fiscal.FISOpenFact("T", "C", Trim("" & !letra), "1", "P", "17", "I", Trim("" & !respComprador), Trim("" & !nombre1), Trim("" & !nombre2), Trim("" & !nombreId), Trim("" & !detalleID), "N", Trim("" & !domicilio1), Trim("" & !domicilio2), "", "", Trim("" & !extra), "G")
    '''''''''''''            If fBinario(garrayentrada(1), 14) = "1" Or fBinario(garrayentrada(1), 4) = "1" Then
    '''''''''''''                MsgBox "El Impresor Fiscal se Encuentra sin Papel", vbInformation, "CAMBIE EL PAPEL"
    '''''''''''''                Impresion_control 4 '// Error al Imprimir Items
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos Items
    '''''''''''''                Fiscal.FISCancelaFact
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            End If
    '''''''''''''            mLetra = Trim("" & !letra) '// Guardamos la letra porque nos sirve para el cierre
    '''''''''''''            If Not Correcto Then
    '''''''''''''                Impresion_control 3 '// Error al Abrir el Comprobante
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos Items
    '''''''''''''                Fiscal.FISCancelaFact
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            End If
    '''''''''''''        End If
    '''''''''''''        .Close
    '''''''''''''    End With
    '''''''''''''    Set rstDatos = Nothing
    '''''''''''''    Set rstDatos = Gs_BaseGen.Execute("SELECT * FROM TicketItems")
    '''''''''''''    With rstDatos '// Imprime los Items
    '''''''''''''        Do While Not .EOF
    '''''''''''''            DoEvents: DoEvents
    '''''''''''''            Fiscal.PFLimpiarBufferCOM
    '''''''''''''            Correcto = Fiscal.FISSendItemFact(Trim("" & !Detalle), Trim("" & !Cantidad), Trim("" & !Precio), "2100", "M", "1", "0000", "", "", "", "0000000", "000000")
    '''''''''''''            If fBinario(garrayentrada(1), 14) = "1" Or fBinario(garrayentrada(1), 4) = "1" Then
    '''''''''''''                MsgBox "El Impresor Fiscal se Encuentra sin Papel", vbInformation, "CAMBIE EL PAPEL"
    '''''''''''''                Impresion_control 4 '// Error al Imprimir Items
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos Items
    '''''''''''''                Fiscal.FISCancelaFact
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            End If
    '''''''''''''            If Not Correcto Then
    '''''''''''''                Impresion_control 4 '// Error al Imprimir Items
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos Items
    '''''''''''''                Fiscal.FISCancelaFact
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            Else
    '''''''''''''                .MoveNext
    '''''''''''''            End If
    '''''''''''''        Loop
    '''''''''''''        .Close
    '''''''''''''    End With
    '''''''''''''    Set rstDatos = Nothing
    '''''''''''''    Set rstDatos = Gs_BaseGen.Execute("SELECT * FROM TicketPagos")
    '''''''''''''    With rstDatos '// Imprime las Formas de Pago
    '''''''''''''        Do While Not .EOF
    '''''''''''''            DoEvents: DoEvents
    '''''''''''''            Fiscal.PFLimpiarBufferCOM
    '''''''''''''            Correcto = Fiscal.FISPagoFact(Trim("" & !Detalle), Trim("" & !monto), Trim("" & !Tipo))
    '''''''''''''            If fBinario(garrayentrada(1), 14) = "1" Or fBinario(garrayentrada(1), 4) = "1" Then
    '''''''''''''                MsgBox "El Impresor Fiscal se Encuentra sin Papel", vbInformation, "CAMBIE EL PAPEL"
    '''''''''''''                Impresion_control 4 '// Error al Imprimir Items
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos Items
    '''''''''''''                Fiscal.FISCancelaFact
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            End If
    '''''''''''''            If Not Correcto Then
    '''''''''''''                Fiscal.FISPagoFact Trim("cancela"), "000", "C"
    '''''''''''''                Impresion_control 5 '// Error al Imprimir Forma de Paqo
    '''''''''''''                DoEvents: DoEvents
    '''''''''''''                .Close              '// Cerramos tabla
    '''''''''''''                Set rstDatos = Nothing
    '''''''''''''                Exit Function
    '''''''''''''            Else
    '''''''''''''                .MoveNext
    '''''''''''''            End If
    '''''''''''''        Loop
    '''''''''''''        .Close
    '''''''''''''    End With
    '''''''''''''    Set rstDatos = Nothing
    '''''''''''''    Fiscal.PFLimpiarBufferCOM
    '''''''''''''    Correcto = Fiscal.FISCierreFact("T", mLetra, " ")
    '''''''''''''    DoEvents
    '''''''''''''    mNum = Trim(garrayentrada(3))
    '''''''''''''    Correcto = Fiscal.FISAvanzarAmbos(1)
    '''''''''''''    Impresion_control 2, mNum
    '''''''''''''End Function
    '''''''''''''Public Function Impresion_Errores(mError As Integer)
    '''''''''''''    Dim mMensaje As String
    '''''''''''''    Select Case mError
    '''''''''''''        ''// Case 0 // Libre de Impresion
    '''''''''''''        '// Case 1 // Imprimiendo
    '''''''''''''        '// case 2 // Imprimio Bien
    '''''''''''''        Case 3 '// Abrir
    '''''''''''''            mMensaje = "Error al Abrir el Comprobante"
    '''''''''''''        Case 4 '// Items
    '''''''''''''            mMensaje = "Error al Enviar un Item"
    '''''''''''''        Case 5 '// Forma de Pago
    '''''''''''''            mMensaje = "Error al enviar Forma de Pago"
    '''''''''''''        Case 6 '// Cierre
    '''''''''''''            mMensaje = "Error al enviar Cierre"
    '''''''''''''        Case 7 '// Estatus T
    '''''''''''''            mMensaje = "La Impresora presenta Anomalías"
    '''''''''''''    End Select
    '''''''''''''    Gs_BaseGen.Execute "DELETE * FROM Ticketitems"
    '''''''''''''    Gs_BaseGen.Execute "DELETE * FROM TicketEnc"
    '''''''''''''    Gs_BaseGen.Execute "DELETE * FROM TicketPagos"
    '''''''''''''    MsgBox mMensaje, vbCritical, "CUIDADO!!!!!"
    '''''''''''''End Function

End Class
