Imports FiscalPrinterLib

Public Class clsFiscal
    Private Structure tCLiente
        Dim Id As String
        Dim IdTipo As TiposDeDocumento
        Dim Detalle As String
        Dim Responsabilidad As TiposDeResponsabilidades
        Dim ResponsabilidadId As String
        Dim Domicilio As String
        Dim IngresosBrutos As String
        Dim CUIT As String
    End Structure

    Private Structure tComprobante
        Dim Tipo As eComprobante
        Dim Letra As String
        Dim Detalle As String
    End Structure

    Private Structure tPagos
        Dim Detalle As String
        Dim Monto As Double
    End Structure

    Private Structure ComprobanteFiscal
        Dim Cliente As tCLiente
        Dim Comprobante As tComprobante
        Dim Pagos() As tPagos
        Dim Cajero As Integer
        Dim NumeroDevuelto As String
        Dim ErrorMensaje As String
        Dim OK As Boolean
        Dim oDtItems As DataTable
        Dim frmFormulario As Form
        Dim arrDatosIva As String
    End Structure


    Dim Comprobante As ComprobanteFiscal

    Public strDatosComprobante As String
    Public strDatosItem As String



    Public Sub AperturaCajon()
        On Error Resume Next
        If SistemaPV.ImpFiscal.Utiliza Then
            If SistemaPV.ImpFiscal.Modelo = EpsonTM Then
                If Not (form_principal Is Nothing) Then form_principal = Nothing
                form_principal = frmVentas
                With form_principal.prf_port
                    If Not (.PortOpen = True) Then
                        .CommPort = SistemaPV.ImpFiscal.Puerto
                        If Not .PortOpen Then .PortOpen = True
                    End If
                    mdlFiscalEpson.FISCajon(1)
                    If .PortOpen Then .PortOpen = False
                End With
            Else
                Dim boolCierre As Boolean
                With Comprobante
                    If (.frmFormulario Is Nothing) Then
                        .frmFormulario = New frmSistema_PrintFiscal
                        Puerto_Hasar()
                        boolCierre = True
                    End If
                    If Not .frmFormulario.ncrFiscal.CajonAbierto Then .frmFormulario.ncrFiscal.AbrirCajonDeDinero()
                    If boolCierre Then
                        Comprobante.frmFormulario.ncrFiscal.Finalizar()
                        .frmFormulario = Nothing
                    End If
                End With
            End If
        End If

    End Sub

    '----------------------------------------------------------
    ' Imprimir:  Funcion Publica
    '          - Imprimir un comprobante fiscal
    '----------------------------------------------------------
    Public Function Imprimir( _
        ByVal strCliente As String, _
        ByVal strClienteDetalle As String, _
        ByVal strClienteResponsabilidad As String, _
        ByVal strClienteDomicilio As String, _
        ByVal intComprobante As eComprobante, _
        ByVal strComprobanteLetra As String, _
        ByVal intCajero As Integer, _
        ByVal rstItemsArti As ADODB.Recordset, _
        ByRef strNumeroDev As String, _
        ByRef strMensajeError As String, _
        ByRef strPagos As String) As Boolean
        ' Dim frmImpresion As Form
        On Error GoTo e
        If SistemaPV.ImpFiscal.Utiliza Then
            With Comprobante
                '-Inicializacion y limpieza de variables
                .Cajero = intCajero
                With .Cliente
                    .Detalle = strClienteDetalle
                    .IdTipo = CodigoTipo(strCliente)
                    .Id = strCliente
                    .Responsabilidad = Responsabilidades(strClienteResponsabilidad)
                    .ResponsabilidadId = strClienteResponsabilidad
                    .Domicilio = Trim(strClienteDomicilio)
                    .IngresosBrutos = ""
                End With
                With .Comprobante
                    .Letra = UCase(Trim(strComprobanteLetra))
                    If .Letra = "P" Then .Letra = "T"
                    .Tipo = intComprobante
                    Select Case .Tipo
                        Case eComprobante.factura
                            .Detalle = "FACTURA"
                        Case eComprobante.Credito
                            .Detalle = "NOTA DE CREDITO"
                        Case Else
                            .Detalle = "TICKET"
                    End Select

                End With
                Pagos_Colocar(strPagos)
                .rstItems = rstItemsArti
                .NumeroDevuelto = ""
                .ErrorMensaje = ""
                .OK = True
                '-Validaciones
                .OK = ValidarDatos()
                If .OK Then
                    .frmFormulario = New frmSistema_PrintFiscal
                    .frmFormulario.Show(vbModal)
                    If Not (.frmFormulario Is Nothing) Then .frmFormulario = Nothing
                End If
                'If Not (.rstItems Is Nothing) Then Set .rstItems = Nothing
                strNumeroDev = .NumeroDevuelto

                Imprimir = (Trim(.ErrorMensaje) = "")
            End With
        End If
        Exit Function
e:
        strMensajeError = Comprobante.ErrorMensaje & Chr(13) & Err.Description
        MostrarError()
        Comprobante.NumeroDevuelto = ""
        Imprimir = False
        'If Not (Comprobante.rstItems Is Nothing) Then Set Comprobante.rstItems = Nothing
        If Not (Comprobante.frmFormulario Is Nothing) Then Comprobante.frmFormulario = Nothing
    End Function

    '-----------------------------------------------------------
    ' Comenzar:  Funcion Publica
    '          - Comienza y administra la impresion de un Fiscal
    '-----------------------------------------------------------
    Public Function ComenzarC()
        On Error GoTo e

        '--> Depende el tipo de Impresora
        If (SistemaPV.ImpFiscal.Modelo = EpsonTM) Then
            ' - Establecer conexiones de Puertos
            'Puerto_Epson
            Comprobante.OK = True
            If Comprobante.OK Then
                ' - Imprimir el encabezado EPSON
                'MsgBox "Pide encabezado"
                Encabezado_Epson()
                If Comprobante.OK Then
                    ' - Imprimir Items EPSON
                    Items_Epson()
                    If Comprobante.OK Then
                        ' - Imprimir el Pago EPSON
                        Pagos_Epson()
                        If Comprobante.OK Then
                            ' - Imprimir el Pago EPSON
                            Cerrar_Epson()
                            If Comprobante.OK Then
                                Comprobante.ErrorMensaje = ""
                                Comprobante.NumeroDevuelto = ""
                            Else
                                CancelaEpson()
                            End If
                        Else
                            CancelaEpson()
                        End If
                    Else
                        CancelaEpson()
                    End If
                Else
                    CancelaEpson()
                End If
            End If

        ElseIf SistemaPV.ImpFiscal.Modelo = HASAR Then
            ' - Establecer conexiones de Puertos
            Puerto_Hasar()
            If Comprobante.OK Then
                ' - Imprimir el encabezado HASAR
                Encabezado_Hasar()
                If Comprobante.OK Then
                    ' - Imprimir Items HASAR
                    Items_Hasar()
                    If Comprobante.OK Then
                        ' - Imprimir el Pago HASAR
                        Pagos_Hasar()
                        If Comprobante.OK Then
                            ' - Imprimir el Pago HASAR
                            Cerrar_Hasar()
                            If Comprobante.OK Then
                                Comprobante.ErrorMensaje = ""
                                Comprobante.NumeroDevuelto = ""
                            End If
                        End If
                    End If
                End If
            End If
        End If
        With Comprobante
            .NumeroDevuelto = ""
            If .OK Then
                .ErrorMensaje = ""
                ObtenerInfoComprobante()
                If Not .OK Then
                    .ErrorMensaje = "El Numero de comprobante no es Válido - Reintente Impresión"
                Else
                    GrabarImpresion()
                End If
            Else
                Err.Raise(10, "Impresion Fiscal", .ErrorMensaje)
            End If
            If SistemaPV.ImpFiscal.Modelo = HASAR Then
                Comprobante.frmFormulario.ncrFiscal.Finalizar()
            Else
                'If Comprobante.frmFormulario.prf_port.PortOpen Then Comprobante.frmFormulario.prf_port.PortOpen = False
            End If

        End With
        Exit Function
e:

        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->" & Err.Description
        Comprobante.OK = False
        If SistemaPV.ImpFiscal.Modelo = HASAR Then
            Comprobante.frmFormulario.ncrFiscal.TratarDeCancelarTodo()
        End If
        MostrarError()
    End Function

    '------------------------------------------------------------------------------------------
    ' Puerto_Epson: Procedimiento Privado
    '         - Inicializa el puerto de la impresora Fiscal Epson
    '------------------------------------------------------------------------------------------
    Public Sub Puerto_Epson()
        On Error GoTo impresora_apag
        If Not (form_principal Is Nothing) Then form_principal = Nothing
        form_principal = frmVentas
        With form_principal.prf_port
            .CommPort = SistemaPV.ImpFiscal.Puerto
            If Not .PortOpen Then .PortOpen = True
        End With
        Comprobante.ErrorMensaje = ""
        Comprobante.OK = True
Procesar:
        ' Comprobante.frmFormulario.ncrFiscal.TratarDeCancelarTodo
        Exit Sub
impresora_apag:
        If MsgBox("Error Impresora:" & Err.Description, vbRetryCancel, "Errores") = vbRetry Then
            'Resume Procesar
            Comprobante.OK = False
        Else
            Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->" & Err.Description
            Comprobante.OK = False
        End If
    End Sub
    '------------------------------------------------------------------------------------------
    ' Puerto_Hasar: Procedimiento Privado
    '         - Inicializa el puerto de la impresora Fiscal Hasar
    '------------------------------------------------------------------------------------------
    Private Sub Puerto_Hasar()
        On Error GoTo impresora_apag
        With Comprobante.frmFormulario.ncrFiscal
            .Puerto = SistemaPV.ImpFiscal.Puerto
            .Modelo = MODELO_615
            .Comenzar()
        End With
        Comprobante.ErrorMensaje = ""
        Comprobante.OK = True

Procesar:
        Comprobante.frmFormulario.ncrFiscal.TratarDeCancelarTodo()
        Exit Sub
impresora_apag:
        If MsgBox("Error Impresora:" & Err.Description, vbRetryCancel, "Errores") = vbRetry Then
            Resume Procesar
        Else
            Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->" & Err.Description
            Comprobante.OK = False
        End If
    End Sub

    '------------------------------------------------------------------------------------------
    ' Encabezado_Epson: Procedimiento Privado
    '         - Imprimir el encabezado de los tickets Epson
    '------------------------------------------------------------------------------------------
    Private Sub Encabezado_Epson()
        On Error GoTo e
        Dim strTipoId As String, strRazonSocial(1) As String, strResp As String
        Select Case Comprobante.Cliente.Responsabilidad
            Case CONSUMIDOR_FINAL
                strResp = "F"
            Case RESPONSABLE_INSCRIPTO
                strResp = "I"
            Case MONOTRIBUTO
                strResp = "M"
            Case RESPONSABLE_EXENTO
                strResp = "E"
        End Select
        Select Case Comprobante.Cliente.IdTipo
            Case TIPO_CUIT
                strTipoId = "CUIT"
            Case Else
                strTipoId = "ID"
        End Select
        strRazonSocial(0) = Left(Comprobante.Cliente.Detalle, 50)
        strRazonSocial(1) = Mid(Comprobante.Cliente.Detalle, 51, Len(Comprobante.Cliente.Detalle))
        'strDomicilio(0) = Left(mDatos.Domicilio, 30)
        If (Comprobante.Comprobante.Letra = "A" Or Comprobante.Comprobante.Letra = "B") Then
            'fFacturaImprimir = FISOpenFact("T", "C", mDatos.Tipo, 1, "P", 17, "I", mDatos.ClienteIva, mRazonSocial(0), mRazonSocial(1), mIdent, mDatos.Identificador, "N", mDomicilio(0), mDomicilio(1), "", "", "", "G")
            Comprobante.OK = mdlFiscalEpson.FISOpenFact("T", "C", Comprobante.Comprobante.Letra, 1, "P", 17, "I", strResp, strRazonSocial(0), strRazonSocial(1), strTipoId, Comprobante.Cliente.Id, "N", Comprobante.Cliente.Domicilio, "", "", "", "", "G")
        Else
            Comprobante.OK = mdlFiscalEpson.FISOpenTicket("T")
        End If
        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Enc." & Err.Description
    End Sub

    '------------------------------------------------------------------------------------------
    ' Encabezado_Hasar: Procedimiento Privado
    '         - Imprimir el encabezado de los tickets Hasar
    '------------------------------------------------------------------------------------------
    Private Sub Encabezado_Hasar()
        On Error GoTo e
        Dim comando As String
        Dim FS As String

        FS = Chr$(28)

        With Comprobante.frmFormulario.ncrFiscal
            'With frmPrintFiscal.ncrFiscal
            If Comprobante.Comprobante.Tipo = Ticket Then
                Dim strResp As String, strTipoId As String
                Select Case Comprobante.Cliente.Responsabilidad
                    Case CONSUMIDOR_FINAL
                        strResp = "F"
                    Case RESPONSABLE_INSCRIPTO
                        strResp = "I"
                    Case MONOTRIBUTO
                        strResp = "M"
                    Case RESPONSABLE_EXENTO
                        strResp = "E"
                End Select
                Select Case Comprobante.Cliente.IdTipo
                    Case TIPO_CUIT
                        strTipoId = "C"
                        Comprobante.Cliente.CUIT = Comprobante.Cliente.Id
                    Case Else
                        Comprobante.Cliente.IdTipo = TIPO_NINGUNO
                        Comprobante.Cliente.CUIT = ""
                        ''fiscalprinterlibctl.TIPO_NINGUNO
                        strTipoId = "0"
                End Select

                If Comprobante.Comprobante.Letra = "A" Then
                    If SistemaPV.ImpFiscal.Tipo = 615 Then
                        comando = Chr$(98) & FS & Comprobante.Cliente.Detalle & FS & Replace(Comprobante.Cliente.Id, "-", "") & FS & strResp & FS & strTipoId
                    Else
                        comando = Chr$(98) & FS & Comprobante.Cliente.Detalle & FS & Replace(Comprobante.Cliente.Id, "-", "") & FS & strResp & FS & strTipoId & FS & Comprobante.Cliente.Domicilio
                    End If
                    .Enviar(comando)
                    'comando = Chr$(128) & FS & "R" & FS & "T"
                    '.Enviar comando

                    .AbrirComprobanteFiscal(TICKET_FACTURA_A)

                ElseIf Comprobante.Comprobante.Letra = "B" Then
                    If SistemaPV.ImpFiscal.Tipo = 615 Then
                        .DatosCliente(Comprobante.Cliente.Detalle, Replace(Comprobante.Cliente.Id, "-", ""), Comprobante.Cliente.IdTipo, Comprobante.Cliente.Responsabilidad)
                    Else
                        comando = Chr$(98) & FS & Comprobante.Cliente.Detalle & FS & Replace(Comprobante.Cliente.Id, "-", "") & FS & strResp & FS & strTipoId & FS & "." & Comprobante.Cliente.Domicilio
                        .Enviar(comando)
                    End If
                    .AbrirComprobanteFiscal(TICKET_FACTURA_B)

                ElseIf Comprobante.Comprobante.Letra = "" Or Comprobante.Comprobante.Letra = "T" Then
                    .AbrirComprobanteFiscal(TICKET_C)
                End If
            End If
        End With
        ''        Case 1, 6 ' Items
        ''            ncrFiscal.ImprimirItem Trim(txtParam(5).Text), Val(Trim(txtParam(6).Text)), Val(Trim(txtParam(7).Text)), Val(Trim(txtParam(8).Text)), 0
        ''            strMensaje = "Item Ticket"
        ''        Case 2, 7  ' Pagos
        ''            strMensaje = "Pagos Ticket"
        ''            ncrFiscal.ImprimirPago txtParam(3).Text, Val(txtParam(4).Text)
        ''        Case 3, 8  ' Cerrar
        ''            strMensaje = "Cerrando"
        ''            ncrFiscal.CerrarComprobanteFiscal
        ''        Case 4  ' Clientes
        ''            strMensaje = "Clientes"
        ''            ncrFiscal.DatosCliente txtParam(1).Text, txtParam(2).Text, cmb(0).ItemData(cmb(0).ListIndex), cmb(1).ItemData(cmb(1).ListIndex)
        ''        Case 5  ' Abrir Factura
        ''            strMensaje = "Factura " & txtParam(0).Text
        ''            If UCase(txtParam(0).Text) = "A" Then
        ''                ncrFiscal.AbrirComprobanteFiscal TICKET_FACTURA_A
        ''            Else
        ''                ncrFiscal.AbrirComprobanteFiscal TICKET_FACTURA_B
        ''            End If

        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Enc." & Err.Description
        Comprobante.frmFormulario.ncrFiscal.TratarDeCancelarTodo()
    End Sub


    '------------------------------------------------------------------------------------------
    ' Items_Epson: Procedimiento Privado
    '         - Imprimir los items de los tickets Epson
    '------------------------------------------------------------------------------------------
    Private Sub Items_Epson()
        On Error GoTo e
        Dim strPrecio As String, strCantidad As String, strIva As String
        Dim dblPrecioFinal As Double
        With Comprobante.rstItems
            If .RecordCount > 0 Then .MoveFirst()
            Do While Not .EOF
                If Comprobante.Comprobante.Letra = "A" Then
                    dblPrecioFinal = (Val("" & !final) / (1 + Val("" & !iva) / 100))
                Else
                    dblPrecioFinal = Val("" & !final)
                End If
                strPrecio = Replace(Format(Val("" & dblPrecioFinal), "###0.00"), ".", "")
                strCantidad = Replace(Format(Val("" & !Cantidad), "###0.000"), ".", "")
                strIva = Replace(Format(Val("" & !iva), "###0.00"), ".", "")
                If (Comprobante.Comprobante.Letra = "A" Or Comprobante.Comprobante.Letra = "B") Then
                    Comprobante.OK = mdlFiscalEpson.FISSendItemFact(Trim("" & !Detalleticket), strCantidad, strPrecio, strIva, "M", "1", "00000000", "", "", "", "0000000000", "0000000000")
                Else
                    Comprobante.OK = mdlFiscalEpson.FISSendItemTique(Trim("" & !Detalleticket), strCantidad, strPrecio, strIva, "M", "1", "0000", "0000")
                End If
                .MoveNext()
            Loop
            If .RecordCount > 0 Then .MoveFirst()
        End With
        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Items." & Err.Description
    End Sub
    '------------------------------------------------------------------------------------------
    ' Items_Hasar: Procedimiento Privado
    '         - Imprimir los items de los tickets Hasar
    '------------------------------------------------------------------------------------------
    Private Sub Items_Hasar()
        On Error GoTo e
        With Comprobante.rstItems
            Do While Not .EOF
                If Val("" & !Cantidad) > 0 Then
                    Comprobante.frmFormulario.ncrFiscal.ImprimirItem(Trim("" & !Detalleticket), Val("" & !Cantidad), Val("" & !final), Val("" & !iva), 0)
                Else
                    Comprobante.frmFormulario.ncrFiscal.DevolucionDescuento(Trim("" & !Detalleticket) & "[" & Abs(Val("" & !Cantidad)) & "] ", (Abs(Val("" & !Cantidad)) * Val("" & !final)), Val("" & !iva), 0, True, DEVOLUCION_DE_ENVASES)
                End If
                .MoveNext()
            Loop
        End With
        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Items." & Err.Description
        Comprobante.frmFormulario.ncrFiscal.TratarDeCancelarTodo()
    End Sub

    '------------------------------------------------------------------------------------------
    ' Pagos_Epson: Procedimiento Privado
    '         - Imprimir los pagos en Epson
    '------------------------------------------------------------------------------------------
    Private Sub Pagos_Epson()
        On Error GoTo e

        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Pagos." & Err.Description
    End Sub

    '------------------------------------------------------------------------------------------
    ' Pagos_Hasar: Procedimiento Privado
    '         - Imprimir los pagos en Hasar
    '------------------------------------------------------------------------------------------
    Private Sub Pagos_Hasar()
        On Error GoTo e
        Dim v As Integer
        For v = 0 To UBound(Comprobante.Pagos) - 1
            Comprobante.frmFormulario.ncrFiscal.ImprimirPago(Comprobante.Pagos(v).Detalle, Comprobante.Pagos(v).Monto)
        Next v
        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Pagos." & Err.Description
        Comprobante.frmFormulario.ncrFiscal.TratarDeCancelarTodo()
    End Sub
    '------------------------------------------------------------------------------------------
    ' Cerrar_Epson: Procedimiento Privado
    '         - Cerrar Ticket en Epson
    '------------------------------------------------------------------------------------------
    Private Sub Cerrar_Epson()
        On Error GoTo e
        If (Comprobante.Comprobante.Letra = "A" Or Comprobante.Comprobante.Letra = "B") Then
            Comprobante.OK = mdlFiscalEpson.FISCierreFact("T", Comprobante.Comprobante.Letra, " ")
            'Comprobante.frmFormulario.prf_port.PortOpen = False
        Else
            ' Sleep (100)
            Comprobante.OK = mdlFiscalEpson.FISCierreTique
            'Comprobante.frmFormulario.ncrFiscal.Finalizar
        End If
        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Cerrar." & Err.Description
    End Sub
    '------------------------------------------------------------------------------------------
    ' Cerrar_Hasar: Procedimiento Privado
    '         - Cerrar Ticket en Hasar
    '------------------------------------------------------------------------------------------
    Private Sub Cerrar_Hasar()
        On Error GoTo e
        Comprobante.frmFormulario.ncrFiscal.CerrarComprobanteFiscal()
        Exit Sub
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Cerrar." & Err.Description
        Comprobante.frmFormulario.ncrFiscal.TratarDeCancelarTodo()
    End Sub
    '------------------------------------------------------------------------------------------
    ' Numero: Funcion Privado
    '        - Traer el numero devuelto
    '------------------------------------------------------------------------------------------
    Private Function Numero() As String
        On Error GoTo e
        If SistemaPV.ImpFiscal.Modelo = EpsonTM Then
            Numero = Trim(garrayentrada(3))
        ElseIf SistemaPV.ImpFiscal.Modelo = HASAR Then
            If (Comprobante.Comprobante.Letra = "A" Or Comprobante.Comprobante.Letra = "B") Then
                ' Numero = Comprobante.frmFormulario.ncrFiscal.UltimaFactura
                Numero = Format(SistemaPV.ImpFiscal.PuntoDeVenta, "0000") & "-" & Format(Comprobante.frmFormulario.ncrFiscal.Respuesta(3), "00000000")
            Else

                Numero = Format(SistemaPV.ImpFiscal.PuntoDeVenta, "0000") & "-" & Format(Comprobante.frmFormulario.ncrFiscal.Respuesta(3), "00000000")
            End If
        End If
        Exit Function
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Numero." & Err.Description
    End Function


    Public Function ObtenerUltimoNumero(ByVal intTipo As Integer) As String
        ' 0 - t / 1-f / 2-z
        On Error GoTo e
        If SistemaPV.ImpFiscal.Utiliza Then
            If SistemaPV.ImpFiscal.Modelo = EpsonTM Then
                ObtenerUltimoNumero = ""
            ElseIf SistemaPV.ImpFiscal.Modelo = HASAR Then
                With Comprobante
                    Dim boolCierre As Boolean
                    If (.frmFormulario Is Nothing) Then
                        .frmFormulario = New frmSistema_PrintFiscal
                        Puerto_Hasar()
                        boolCierre = True
                    End If
                    Select Case intTipo
                        Case 0
                            ObtenerUltimoNumero = .frmFormulario.ncrFiscal.UltimoTicket
                        Case 1
                            ObtenerUltimoNumero = .frmFormulario.ncrFiscal.UltimaFactura
                        Case 2
                            ObtenerUltimoNumero = .frmFormulario.ncrFiscal.UltimaFactura
                    End Select
                    If boolCierre Then
                        .frmFormulario.ncrFiscal.Finalizar()
                        .frmFormulario = Nothing
                    End If
                End With
            End If
        Else
            ObtenerUltimoNumero = ""
        End If
        Exit Function
e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->Numero." & Err.Description
    End Function


    Public Function ObtenerPtoVta() As Integer
        ' 0 - t / 1-f / 2-z
        On Error GoTo e
        If SistemaPV.ImpFiscal.Utiliza Then
            If SistemaPV.ImpFiscal.Modelo = EpsonTM Then
                ObtenerPtoVta = 0
            ElseIf SistemaPV.ImpFiscal.Modelo = HASAR Then
                With Comprobante
                    Dim boolCierre As Boolean
                    If (.frmFormulario Is Nothing) Then
                        .frmFormulario = New frmSistema_PrintFiscal
                        Puerto_Hasar()
                        boolCierre = True
                    End If
                    .frmFormulario.ncrFiscal.ObtenerDatosDeInicializacion()
                    ObtenerPtoVta = Val(.frmFormulario.ncrFiscal.Respuesta(7))
                    If boolCierre Then
                        .frmFormulario.ncrFiscal.Finalizar()
                        .frmFormulario = Nothing
                    End If
                End With

            End If
        Else
            ObtenerPtoVta = 0
        End If
        Exit Function

e:
        Comprobante.OK = False
        Comprobante.ErrorMensaje = Comprobante.ErrorMensaje & "-->PtoVta." & Err.Description
    End Function

    ''
    ''Private Sub CMD_Click(Index As Integer)
    ''    Dim strMensaje As String
    ''    On Error GoTo e:
    ''    Select Case Index
    ''        Case 0  ' Abrir Ticket
    ''            ncrFiscal.AbrirComprobanteFiscal TICKET_C
    ''            strMensaje = "Abrir Ticket " & ncrFiscal.HuboErrorFiscal
    ''        Case 1, 6 ' Items
    ''            ncrFiscal.ImprimirItem Trim(txtParam(5).Text), Val(Trim(txtParam(6).Text)), Val(Trim(txtParam(7).Text)), Val(Trim(txtParam(8).Text)), 0
    ''            strMensaje = "Item Ticket"
    ''        Case 2, 7  ' Pagos
    ''            strMensaje = "Pagos Ticket"
    ''            ncrFiscal.ImprimirPago txtParam(3).Text, Val(txtParam(4).Text)
    ''        Case 3, 8  ' Cerrar
    ''            strMensaje = "Cerrando"
    ''            ncrFiscal.CerrarComprobanteFiscal
    ''        Case 4  ' Clientes
    ''            strMensaje = "Clientes"
    ''            ncrFiscal.DatosCliente txtParam(1).Text, txtParam(2).Text, cmb(0).ItemData(cmb(0).ListIndex), cmb(1).ItemData(cmb(1).ListIndex)
    ''        Case 5  ' Abrir Factura
    ''            strMensaje = "Factura " & txtParam(0).Text
    ''            If UCase(txtParam(0).Text) = "A" Then
    ''                ncrFiscal.AbrirComprobanteFiscal TICKET_FACTURA_A
    ''            Else
    ''                ncrFiscal.AbrirComprobanteFiscal TICKET_FACTURA_B
    ''            End If
    ''        Case 9  ' Cierre Z
    ''            strMensaje = "Cierre Z"
    ''            ncrFiscal.ReporteZ
    ''        Case 10 ' Cierre X
    ''            strMensaje = "Cierre X"
    ''            ncrFiscal.ReporteX
    ''        Case 11
    ''
    ''            With ncrFiscal
    ''                .Encabezado(0) = "Quien es el mas Capo"
    ''                .Encabezado(1) = "----------- ( lea Abajo )"
    ''                .Encabezado(12) = "EZEQUIEL el Capo"
    ''                .Encabezado(13) = "Gracias por su Visita"
    ''                .Encabezado(14) = "y usar controladores HASAR"
    ''            End With
    ''    End Select
    ''    Bitacora strMensaje ' , ncrFiscal.Respuesta(1) & "/  /" & ncrFiscal.Respuesta(1)
    ''    Exit Sub
    ''e:
    ''If MsgBox("Error Impresora:" & Err.Description, vbRetryCancel, "Errores") = vbRetry Then
    ''    ncrFiscal.Abortar
    ''    Err.Clear
    ''End If
    ''End Sub
    ''
    'Private Sub Form_Load()
    '    If Sistema.Impresora = HASAR Then
    '        Puerto_Hasar
    '        Imprimir_Hasar
    '    ElseIf Sistema.Impresora = EpsonTM Then
    '        ' Puerto
    '        Imprimir_Epson
    '    End If
    'End Sub
    '
    ''------------------------------------------------------------------------------------------
    '' Puerto_Hasar: Procedimiento Privado
    ''         - Inicializa el puerto de la impresora Fiscal Hasar
    ''------------------------------------------------------------------------------------------
    'Private Sub Puerto_Hasar()
    '    On Error GoTo impresora_apag
    '    With ncrFiscal
    '        .Puerto = 1
    '        .Modelo = MODELO_615
    '        .Comenzar
    '    End With
    'Procesar:
    '    ncrFiscal.TratarDeCancelarTodo
    '    Exit Sub
    'impresora_apag:
    '    If MsgBox("Error Impresora:" & Err.Description, vbRetryCancel, "Errores") = vbRetry Then Resume Procesar
    'End Sub
    '
    '
    ''------------------------------------------------------------------------------------------
    '' Puerto_Epson: Procedimiento Privado
    ''         - Inicializa el puerto de la impresora Fiscal Epson
    ''------------------------------------------------------------------------------------------
    'Private Sub Puerto_Epson()
    '    On Error GoTo impresora_apag
    'Procesar:
    '    Exit Sub
    'impresora_apag:
    '    If MsgBox("Error Impresora:" & Err.Description, vbRetryCancel, "Errores") = vbRetry Then Resume Procesar
    'End Sub
    '
    '
    '

    Private Function CodigoTipo(ByRef strCodigo As String) As TiposDeDocumento
        If ValidaCuit(strCodigo, True) Then
            CodigoTipo = TIPO_CUIT
            strCodigo = Trim(Replace(strCodigo, "-", ""))
        Else
            CodigoTipo = TIPO_DNI
        End If
    End Function
    Private Function Responsabilidades(ByRef strResp As String) As TiposDeResponsabilidades
        Select Case UCase(strResp)
            Case "E"
                Responsabilidades = RESPONSABLE_EXENTO
            Case "I"
                Responsabilidades = RESPONSABLE_INSCRIPTO
            Case "C", ""
                Responsabilidades = CONSUMIDOR_FINAL
            Case "M"
                Responsabilidades = MONOTRIBUTO
        End Select
    End Function


    Private Function ValidarDatos() As Boolean
        Dim dblCantidad As Double, dblNuevaCantidad As Double
        Dim intTope As Integer, i As Integer, v As Integer, j As Integer
        Dim boolGrabar As Boolean
        Dim varDatos() As Object, varBookActual As Object
        Const cCantidadMaxima = 999

        ValidarDatos = False
        On Error GoTo e
        With Comprobante
            ' - Validacion Tipos y datos del comprobante
            .Cliente.Detalle = xValida(.Cliente.Detalle)
            .Cliente.Domicilio = Trim(xValida(Left(.Cliente.Domicilio, 50)))
            If .Comprobante.Letra = "A" Then
                If Not (.Cliente.IdTipo = TIPO_CUIT) Then .Comprobante.Letra = "B"
            End If
            ' - Validacion de Items
            With .rstItems
                If .RecordCount > 0 Then
                    .MoveFirst()
                    For j = 1 To .RecordCount
                        !Detalleticket = Left(Valida(Trim("" & !Detalleticket)), 20)
                        ReDim varDatos(.Fields.Count - 1)
                        For v = 0 To .Fields.Count - 1
                            varDatos(v) = Trim(.Fields(v).Value)
                        Next v
                        dblCantidad = Val("" & !Cantidad)
                        Debug.Print("------>")
                        Debug.Print(Trim("" & !Id_arti) & " Cantidad :" & dblCantidad)
                        Debug.Print("Posicion :" & .AbsolutePosition & " Boock :" & .Bookmark)
                        Debug.Print("------>")
                        varBookActual = .Bookmark
                        ' Distribucion de las cantidades
                        intTope = 10
                        boolGrabar = False
                        For i = 1 To intTope
                            dblNuevaCantidad = CInt(dblCantidad / i)
                            If Abs(dblNuevaCantidad) <= cCantidadMaxima Then
                                If (boolGrabar Or i > 1) And dblNuevaCantidad > 0 Then  ' Agregar
                                    dblCantidad = dblCantidad - dblNuevaCantidad

                                    If Not boolGrabar Then  ' es el primer cambio then
                                        Debug.Print(Trim("" & !Id_arti) & " Cantidad Act:" & dblNuevaCantidad)
                                        !Cantidad = dblNuevaCantidad
                                        .Update()
                                        boolGrabar = True
                                    Else                    ' tengo que agregar
                                        .AddNew()
                                        Debug.Print(" Cantidad New:" & dblNuevaCantidad)
                                        For v = 0 To .Fields.Count - 1
                                            .Fields(v).Value = varDatos(v)
                                        Next v
                                        !Cantidad = dblNuevaCantidad
                                        .Update()
                                        .Bookmark = varBookActual
                                    End If
                                    i = 0
                                Else
                                    i = intTope + 1
                                End If
                            End If
                        Next i
                        .MoveNext()
                    Next j
                    .MoveFirst()
                End If
            End With
        End With
        ValidarDatos = True
        Exit Function
e:
        Err.Raise(1, "Validacion para Imprimir", Comprobante.ErrorMensaje)
    End Function

    'Public Function fFacturaImprimir(mDatos As tFactura, mItems As tFacturaItems) As Boolean
    '    Dim mRazonSocial(1) As String, mIdent As String, mDomicilio(1) As String
    '    Dim mCanti As String, mPrecio As String, mIva As String, mMen As String, mMensaje As String
    '    Dim i As Integer
    '    '// CONFIGURACION DE DATOS
    '    mDatos.Tipo = UCase(Trim(mDatos.Tipo))
    '    mDatos.RazónSocial = xValida(mDatos.RazónSocial)
    '    mDatos.Domicilio = Trim(xValida(mDatos.Domicilio))
    '
    '    mRazonSocial(0) = Left(mDatos.RazónSocial, 30)
    '    mRazonSocial(1) = Mid(mDatos.RazónSocial, 31, Len(mDatos.RazónSocial))
    '    mDomicilio(0) = Left(mDatos.Domicilio, 30)
    '    mDomicilio(1) = Mid(mDatos.Domicilio, 31, 30)
    '
    '    mIdent = "ID"
    '    If mDatos.IdentificadorTipo = cuit Then mIdent = "CUIT"
    '    If mDatos.ClienteIva = "C" Then mDatos.ClienteIva = "F"         'Si es Consumidor FInal
    '    mDatos.Identificador = Replace(Trim(mDatos.Identificador), "-", "") 'CUIT O DNI
    '    If mImpresionRed Then
    '        mMensaje = fEnviarDatos(mDatos, mItems)
    '        If mMensaje <> "" Then
    '            MsgBox mMensaje, vbCritical, "impresion cancelada"
    '            fFacturaImprimir = False
    '        Else '-- Impresion OK
    '            ' si imprimio bien (21) coloca de nuevo en 20
    '            If fEstadoServidor(mruta & "imprime.ini") = 21 Then fEstadoServidor mruta & "imprime.ini", 20
    '            fFacturaImprimir = True
    '        End If
    '    Else
    '        '// APERTURA DE TICKET O  FACTURA
    '        If mDatos.Tipo <> "T" Then
    '            DoEvents
    '            fFacturaImprimir = FISOpenFact("T", "C", mDatos.Tipo, 1, "P", 17, "I", mDatos.ClienteIva, mRazonSocial(0), mRazonSocial(1), mIdent, mDatos.Identificador, "N", mDomicilio(0), mDomicilio(1), "", "", "", "G")
    '        Else
    '            fFacturaImprimir = Fiscal.FISOpenTicket("T")
    '        End If
    '        If Not fImpresoraPapelOK Then
    '            fFacturaImprimir = False
    '            If mDatos.Tipo <> "T" Then
    '                Fiscal.FISCancelaFact
    '            Else
    '                Fiscal.FISCancelaTique
    '            End If
    '            Exit Function
    '        End If
    '        If Not fFacturaImprimir Then
    '            If MsgBox("Se ha Producido un error en la Controladora Fiscal " & Chr(13) & " en el Momento que se Producia la apertura del Comprobante" & Chr(13) & "¿ Intentar Cancelar Ticket ?", vbCritical + vbYesNo, "ERROR IMPRESORA FISCAL") = vbYesNo Then
    '                If mDatos.Tipo <> "T" Then
    '                    Fiscal.FISCancelaFact
    '                Else
    '                    Fiscal.FISCancelaTique
    '                End If
    '            End If
    '            fFacturaImprimir = False
    '            Exit Function
    '        End If
    '        '// IMPRESION DE ITEMS
    '        For i = 0 To UBound(mItems.Cantidad) - 1
    '            mItems.Detalle(i) = xValida(Left(mItems.Detalle(i), 20))
    '            If mDatos.Tipo = "A" Then mItems.Precio(i) = mItems.Precio(i) / (1 + mItems.IvaPorcentaje(i) / 100)
    '            mPrecio = Replace(Format(mItems.Precio(i), "###0.00"), ".", "")
    '            mCanti = Replace(Format(mItems.Cantidad(i), "###0.000"), ".", "")
    '            mIva = Replace(Format(mItems.IvaPorcentaje(i), "###0.00"), ".", "")
    '            If mDatos.Tipo <> "T" Then
    '                fFacturaImprimir = Fiscal.FISSendItemFact(mItems.Detalle(i), mCanti, mPrecio, mIva, "M", "1", "00000000", "", "", "", "0000000000", "0000000000")
    '            Else
    '                DoEvents
    '                fFacturaImprimir = Fiscal.FISSendItemTique(mItems.Detalle(i), mCanti, mPrecio, mIva, "M", "1", "0000", "0000")
    '            End If
    '            mTotal = mTotal + Val(mItems.Cantidad(i)) * Val(mItems.Precio(i))
    '            If Not fFacturaImprimir Then
    '                If MsgBox("Se ha Producido un error en la Controladora Fiscal " & Chr(13) & " en el Momento que se transmitian los Items" & Chr(13) & "¿ Intentar Cancelar Ticket ?", vbCritical + vbYesNo, "ERROR IMPRESORA FISCAL") = vbYesNo Then
    '                    error_fiscal mItems.Detalle(i), mCanti, mPrecio
    '                    If mDatos.Tipo <> "T" Then
    '                        Fiscal.FISCancelaFact
    '                    Else
    '                        Fiscal.FISCancelaTique
    '                    End If
    '                End If
    '                fFacturaImprimir = False
    '                Exit Function
    '            End If
    '            If Not fImpresoraPapelOK Then
    '                If mDatos.Tipo <> "T" Then
    '                    Fiscal.FISCancelaFact
    '                Else
    '                    Fiscal.FISCancelaTique
    '                End If
    '                Exit Function
    '            End If
    '        Next i
    '        '// IMPRESION PAGO 1
    '        If mDatos.PagoDetalle1 <> "" Then
    '            mMen = xValida(Left(mDatos.PagoDetalle1, 20)) & "  " & Replace(Format(mDatos.PagoMonto1, "####0.00"), ".", "")
    '            If fFacturaImprimir Then
    '                If mDatos.Tipo <> "T" Then
    '                    fFacturaImprimir = Fiscal.FISPagoFact(xValida(Left(mDatos.PagoDetalle1, 20)), Replace(Format(mDatos.PagoMonto1, "####0.00"), ".", ""), "T")
    '                Else
    '                    fFacturaImprimir = Fiscal.FISPagoTique(xValida(Left(mDatos.PagoDetalle1, 20)), Replace(Format(mDatos.PagoMonto1, "####0.00"), ".", ""), "T")
    '                End If
    '            End If
    '        End If
    '        '// IMPRESION PAGO2
    '        If mDatos.PagoDetalle2 <> "" Then
    '            mMen = xValida(Left(mDatos.PagoDetalle2, 20)) & "  " & Replace(Format(mDatos.PagoMonto2, "####0.00"), ".", "")
    '            If fFacturaImprimir Then
    '                If mDatos.Tipo <> "T" Then
    '                    fFacturaImprimir = Fiscal.FISPagoFact(xValida(Left(mDatos.PagoDetalle2, 20)), Replace(Format(mDatos.PagoMonto2, "####0.00"), ".", ""), "T")
    '                Else
    '                    fFacturaImprimir = Fiscal.FISPagoTique(xValida(Left(mDatos.PagoDetalle2, 20)), Replace(Format(mDatos.PagoMonto2, "####0.00"), ".", ""), "T")
    '                End If
    '            End If
    '        End If
    '        '// DATOS DE TARJETAS DE CREDITO
    '        If mDatos.TarjetaAutoriza <> "" Then
    '            mMen = "Codigo de Autorizacion"
    '            If fFacturaImprimir Then fFacturaImprimir = Fiscal.FISPagoFact(xValida(Left("Auto: " & Trim(mDatos.TarjetaAutoriza), 14)), "000", "T")
    '        End If
    '        If mDatos.TarjetaCupon <> "" Then
    '            mMen = "Codigo de Numero de Cupon"
    '            If fFacturaImprimir Then fFacturaImprimir = Fiscal.FISPagoFact(xValida(Left("Cupon: " & Trim(mDatos.TarjetaCupon), 13)), "000", "T")
    '        End If
    '        If Not fImpresoraPapelOK Then
    '            fFacturaImprimir = False
    '            If mDatos.Tipo <> "T" Then
    '                Fiscal.FISCancelaFact
    '            Else
    '                Fiscal.FISCancelaTique
    '            End If
    '            Exit Function
    '        End If
    '        If Not fFacturaImprimir Then
    '            If MsgBox("Se produjo un Error en Formas de Pago en : " & Chr(13) & mMen & Chr(13) & "¿ Intentar Cancelar Ticket ?", vbCritical + vbYesNo, "ERROR IMPRESORA FISCAL") = vbYesNo Then
    '                If mDatos.Tipo <> "T" Then
    '                    Fiscal.FISCancelaFact
    '                Else
    '                    Fiscal.FISCancelaTique
    '                End If
    '            End If
    '            fFacturaImprimir = False
    '            Exit Function
    '        End If
    '        'Fiscal.PFLimpiarBufferCOM
    '        DoEvents
    '        '// CIERRE DE COMPROBANTE
    '        If mDatos.Tipo <> "T" Then
    '            fFacturaImprimir = Fiscal.FISCierreFact("T", mDatos.Tipo, " ")
    '        Else
    '            ' Sleep (100)
    '            fFacturaImprimir = Fiscal.FISCierreTique
    '        End If
    '        '***** Avanzar Ambos
    '        DoEvents
    '        If fFacturaImprimir Then mDatos.NumeroDevuelto = Val(garrayentrada(3))
    '        fFacturaImprimir = Fiscal.FISAvanzarAmbos(1)
    '        If Not fImpresoraPapelOK Then MsgBox "El Ultimo Comprobante se ha Impreso Correctamente pero " & Chr(13) & "el controlador se esta quedando sin papel", vbInformation, "PAPEL ! ! !"
    '        fFacturaImprimir = True
    '    End If
    'End Function


    '  /////////////////////////////////////////////////////
    ' // Valida y devuelve un texto para impresion Fiscal//
    '/////////////////////////////////////////////////////
    Private Function Valida(ByVal mTexto As String) As String
        Dim mBien As Boolean, i As Integer
        Dim mTextoFinal As String, mLetra As String
        On Error GoTo err_ed
        mTextoFinal = ""
        For i = 1 To Len(Trim(mTexto))
            mLetra = Mid(mTexto, i, 1)
            mBien = False
            Select Case mLetra
                Case 0 To 9, "A" To "Z", "a" To "z", "[", "]", "(", ")", "<", ">", "'", ":", "?", " ", "=", "-", "+", "*", ".", "*", ","
                    mBien = True
            End Select
            If Not mBien Then mLetra = ""
            mTextoFinal = mTextoFinal & mLetra
        Next
        Valida = mTextoFinal
        Exit Function
err_ed:
        MsgBox("Cuidado: " & Err.Description & " " & Err())
    End Function



    Private Sub CancelaEpson()
        On Error GoTo e
        If Comprobante.Comprobante.Letra = "A" Or Comprobante.Comprobante.Letra = "B" Then
            mdlFiscalEpson.FISCancelaFact()
        Else
            mdlFiscalEpson.FISCancelaTique()
        End If
        Exit Sub
e:
    End Sub

    Private Function Pagos_Colocar(ByVal strPagosComprobante As String)
        Dim ps As tParametros
        Dim P As tParametro
        Dim v As Integer, i As Integer
        Dim strDetalle As String
        Dim dblMonto As Double
        Dim strFila As String
        ps = Parametros_Obtener(strPagosComprobante)
        Dim strPagos(3) As String
        strPagos(0) = "Contado"
        strPagos(1) = "Tickets"
        strPagos(2) = "Bonos"
        strPagos(3) = "Tarjeta"



        ReDim Preserve Comprobante.Pagos(0)
        For v = 0 To UBound(Split(strPagosComprobante, "|")) - 1
            strFila = Split(strPagosComprobante, "|")(v)
            strDetalle = ""
            If Not (Val(Split(strFila, "=")(1)) = 0) Then
                strDetalle = strPagos(v)
                dblMonto = Val(Split(strFila, "=")(1))
                If v = 3 Then
                    strFila = Split(strPagosComprobante, "|")(v + 1)
                    strDetalle = Split(Split(strFila, "/")(3), "=")(1)
                    strDetalle = strDetalle & " [" & Split(Split(strFila, "/")(1), "=")(1) & "]"
                    v = v + 1
                End If
            End If
            If Not (strDetalle = "") Then
                ReDim Preserve Comprobante.Pagos(UBound(Comprobante.Pagos) + 1)
                Comprobante.Pagos(UBound(Comprobante.Pagos) - 1).Detalle = strDetalle
                Comprobante.Pagos(UBound(Comprobante.Pagos) - 1).Monto = dblMonto
            End If
        Next v


    End Function

    ' 0 - X / 1  - Z
    Public Function Cierre(ByVal intTipo As Integer) As Boolean
        Dim boolOK As Boolean
        Dim strDevolver As String
        On Error Resume Next
        strDevolver = ""
        strDatosComprobante = ""
        Comprobante.arrDatosIva = ""
        If SistemaPV.ImpFiscal.Modelo = EpsonTM Then
            If Not (form_principal Is Nothing) Then form_principal = Nothing
            form_principal = frmVentas
            With form_principal.prf_port
                If Not (.PortOpen = True) Then
                    .CommPort = SistemaPV.ImpFiscal.Puerto
                    If Not .PortOpen Then .PortOpen = True
                End If
                If intTipo = 0 Then
                    boolOK = mdlFiscalEpson.FISCierreX("")
                Else
                    boolOK = mdlFiscalEpson.FISCierreZ
                End If
                If .PortOpen Then .PortOpen = False
            End With
        Else
            Dim boolCierre As Boolean
            With Comprobante
                If (.frmFormulario Is Nothing) Then
                    .frmFormulario = New frmSistema_PrintFiscal
                    Puerto_Hasar()
                    boolCierre = True
                End If
                If intTipo = 0 Then
                    boolOK = .frmFormulario.ncrFiscal.ReporteX
                Else
                    boolOK = .frmFormulario.ncrFiscal.ReporteZ
                End If
                On Error Resume Next
                strDevolver = ""
                Parametros_Agregar(strDevolver, "Numero", Val(.frmFormulario.ncrFiscal.Respuesta(3)))
                Parametros_Agregar(strDevolver, "AcumuladoVentas", Val(.frmFormulario.ncrFiscal.Respuesta(11)))
                Parametros_Agregar(strDevolver, "AcumuladoIva", Val(.frmFormulario.ncrFiscal.Respuesta(12)))
                Parametros_Agregar(strDevolver, "UltimoA", Val(.frmFormulario.ncrFiscal.Respuesta(10)))
                Parametros_Agregar(strDevolver, "UltimoBC", Val(.frmFormulario.ncrFiscal.Respuesta(9)))
                If boolCierre Then
                    .frmFormulario.ncrFiscal.Finalizar()
                    .frmFormulario = Nothing
                End If
            End With
        End If
        strDatosComprobante = strDevolver
        Cierre = boolOK
    End Function

    Private Sub GrabarImpresion()
        Dim strEncabezado As String
        Dim strIvas As String
        Dim dblTotal(4) As Double
        Dim v As Integer
        Dim parametros As tParametros, P As tParametro

        If Not (Trim(Comprobante.NumeroDevuelto) = "") And Not (Trim(Comprobante.arrDatosIva) = "") Then

            Dim objLibro As clsLibroVentas
            Parametros_Agregar(strEncabezado, "Ent_cuit", " " & Comprobante.Cliente.CUIT)
            Parametros_Agregar(strEncabezado, "Ent_RazonSocial", Comprobante.Cliente.Detalle)
            Parametros_Agregar(strEncabezado, "Ent_tipoIva", Comprobante.Cliente.ResponsabilidadId)
            Parametros_Agregar(strEncabezado, "Ent_IngBrutos", " " & Comprobante.Cliente.IngresosBrutos)
            Parametros_Agregar(strEncabezado, "Lib_Fecha", Format(CDate(Now()), "yyyy/mm/dd"))
            Parametros_Agregar(strEncabezado, "Lib_PtoVta", SistemaPV.ImpFiscal.PuntoDeVenta)
            Parametros_Agregar(strEncabezado, "Lib_Comprobante", Comprobante.Comprobante.Detalle)
            Parametros_Agregar(strEncabezado, "Lib_ComprobanteLetra", Comprobante.Comprobante.Letra)
            Parametros_Agregar(strEncabezado, "Lib_Numero", Comprobante.NumeroDevuelto)

            parametros = Parametros_Obtener(Comprobante.arrDatosIva)
            '---------------------------------------------
            ' Obtener los importes impresos
            '---------------------------------------------
            ' Ivas Generales
            strIvas = Parametros_ObtenerValor(parametros, "general").Valor
            For v = 0 To 3
                dblTotal(v) = Val(Split(strIvas, ";")(v))
            Next v

            ' strIvas = strIvas & "porcentaje;neto;iva;nogravado;total/"

            ' Ivas según Porcentajes Ivas
            strIvas = ""
            For v = 1 To parametros.Cantidad - 1
                strIvas = strIvas & parametros.item(v).Valor & "/"
            Next v


            Parametros_Agregar(strEncabezado, "Lib_Neto", dblTotal(0))
            Parametros_Agregar(strEncabezado, "Lib_Iva", dblTotal(1))
            Parametros_Agregar(strEncabezado, "Lib_NoGravado", dblTotal(2))
            Parametros_Agregar(strEncabezado, "Lib_Total", dblTotal(3))

            Parametros_Agregar(strEncabezado, "arrIvas", strIvas)

            objLibro = New clsLibroVentas
            objLibro.Actualizar(SistemaPV.datosPV.Caja, strEncabezado)
            MatarObjeto(objLibro)

        End If

        Exit Sub
e:
        MatarObjeto(objLibro)
    End Sub

    Private Sub ObtenerInfoComprobante()
        Dim v As Integer
        Dim strFila As String
        Dim dblTotal(4), dblFila(4) As Double
        If SistemaPV.ImpFiscal.Utiliza Then
            With Comprobante
                .NumeroDevuelto = Numero()
                If Val(Trim(Right(.NumeroDevuelto, 6))) = 0 Then
                    .NumeroDevuelto = ""
                    .OK = False
                Else
                    ' Obtención de Valores de Iva
                    If SistemaPV.ImpFiscal.Modelo = EpsonTM Then
                        'Numero = Trim(garrayentrada(3))
                    ElseIf SistemaPV.ImpFiscal.Modelo = HASAR Then
                        '"porcentaje;neto;iva;nogravado;total/"
                        With Comprobante.frmFormulario.ncrFiscal
                            For v = 0 To 6
                                If v = 0 Then
                                    .PedirPrimerIVA()
                                Else
                                    .PedirSiguienteIVA()
                                End If
                                If UBound(Split(.Respuesta(0), "")) > 5 Then
                                    dblFila(1) = Val(.Respuesta(5)) * Val(.Respuesta(3)) ' Iva
                                    dblFila(2) = Val(.Respuesta(8)) * Val(.Respuesta(3)) ' Neto
                                    dblFila(0) = dblFila(2) + dblFila(1)
                                    dblTotal(0) = dblTotal(0) + dblFila(0)
                                    dblTotal(1) = dblTotal(1) + dblFila(1)
                                    dblTotal(2) = dblTotal(2) + dblFila(2)

                                    Parametros_Agregar(strFila, "IVA", Val(.Respuesta(4)) & ";" & dblFila(2) & ";" & dblFila(1) & ";0;" & dblFila(0))
                                Else
                                    v = 7
                                End If
                            Next v
                        End With
                        .arrDatosIva = "general=" & dblTotal(2) & ";" & dblTotal(1) & ";0;" & dblTotal(0) & "|" & strFila
                        Debug.Print("-> " & .arrDatosIva)
                    End If
                End If
            End With
        End If


    End Sub

End Class
