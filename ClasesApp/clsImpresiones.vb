Imports AxEPSON_Impresora_Fiscal

Public Class clsImpresiones

    ''' <summary>
    ''' Método que permite realizar el cierre de la caja.
    ''' </summary>
    ''' <param name="poImpresora">Objeto de Impresora</param>
    ''' <param name="pTipoCierre">Tipo de Cierre (x, y)</param>
    ''' <param name="pImprimir">Se imprime el cierre</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CierreX(ByVal poImpresora As AxPrinterFiscal, _
                                   ByVal pImprimir As Boolean) As Boolean
        Dim wbCierre As Boolean
        Try

            poImpresora.PortNumber = ObtenerConfiguracion(gstrPuertoCOM)
            poImpresora.BaudRate = 9600
            If pImprimir = True Then
                wbCierre = poImpresora.CloseJournal("X", "P")
            Else
                wbCierre = poImpresora.CloseJournal("X")
            End If
            Return wbCierre
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Método que realiza el cierre Z
    ''' </summary>
    ''' <param name="poImpresora">Objeto Impresora</param>
    ''' <param name="pImprimir">Determina si se imprime o no el cierre</param>
    ''' <returns>Verdadero o Falso</returns>
    ''' <remarks></remarks>
    Public Shared Function CierreZ(ByVal poImpresora As AxPrinterFiscal, _
                               ByVal pImprimir As Boolean) As Boolean
        Dim wbCierre As Boolean
        Try

            poImpresora.PortNumber = ObtenerConfiguracion(gstrPuertoCOM)
            poImpresora.BaudRate = 9600
            If pImprimir = True Then
                wbCierre = poImpresora.CloseJournal("Z", "P")
            Else
                wbCierre = poImpresora.CloseJournal("Z")
            End If
            Return wbCierre
        Catch ex As Exception

        End Try
    End Function

    ''' <summary>
    ''' Imprime Ticket de venta a Consumidor Final
    ''' </summary>
    ''' <param name="poImpresora">Objeto Impresora</param>
    ''' <param name="poComprobante">Objeto Comprobante con el detalle.</param>
    ''' <returns>True si se imprime bien, sino, false</returns>
    ''' <remarks></remarks>
    Public Shared Function ImprimirTicket(ByVal poImpresora As AxPrinterFiscal, _
                                          ByVal poComprobante As clsComprobante) As String
        Dim wbImprime As Boolean = False
        Dim i As Integer
        Dim wszRetorno As String
        Try

            poImpresora.PortNumber = ObtenerConfiguracion(gstrPuertoCOM)
            poImpresora.BaudRate = 9600
            poImpresora.OpenTicket("G") ' Establecer Datos en Config

            For i = 0 To poComprobante.DETALLE.Count - 1
                wbImprime = poImpresora.SendTicketItem(poComprobante.DETALLE.Item(i).cod_pronombre, _
                                                       poComprobante.DETALLE.Item(i).cod_procantidad, _
                                                       poComprobante.DETALLE.Item(i).cod_propciounitario, _
                                                       "." & poComprobante.DETALLE.Item(i).cod_iva, _
                                                       "M", "0", "0")

            Next
            '=================== DETALLES =============================
            'Calificador Item de venta(Item 5)= 
            '                   M Monto agregado mercadería SUMA
            '                   m Reversión Resta
            '                   R Bonificación Resta
            '                   r Anula la bonificación SUMA

            wbImprime = poImpresora.GetTicketSubtotal("P", "LINDO SUB")
            wbImprime = poImpresora.SendTicketPayment("Ticket", poComprobante.COM_TOTALFACTRADO, "T")
            wbImprime = poImpresora.CutPaper()
            poImpresora.OpenCashDrawer("1")
            wszRetorno = poImpresora.AnswerField_3
            wbImprime = poImpresora.CloseTicket()
            '=================== DETALLES ============================
            'Calificador del Item de Pago (Item 3)=
            '                   C Cancela Comprobante
            '                   T Suma el importe pagado
            '                   t Anula un pago hecho con Ticket
            '                   D Realiza un descuento global por monto Fijo()
            '                   R Realiza un recargo global por monto Fijo()

            'ultimo ticket poImpresora.AnswerField_3
            '6) Los tipos de categorias son:
            'cons final: F,inscripto: I,iva exento: E, monotributo: M, no inscripto: R, no responsable: N, no categorizado: S. 
            '8) Cierres Z:
            'PrinterFiscal1.CloseJournal("Z", "P")
            'Cierres(X)
            'PrinterFiscal1.CloseJournal("X", "P")
            If wbImprime = False Then
                wszRetorno = "Error"
            End If

            Return wszRetorno

        Catch ex As Exception
            Manejador_Errores("ImprimirTicket", ex)
        End Try
    End Function
    Public Shared Function ImprimirTicket2(ByVal poImpresora As AxPrinterFiscal, _
                                          ByVal poComprobante As clsComprobante) As String
        Dim wbImprime As Boolean = False
        Dim i As Integer
        Dim wszRetorno As String
        Try

            poImpresora.PortNumber = ObtenerConfiguracion(gstrPuertoCOM)
            poImpresora.BaudRate = 9600
            poImpresora.OpenTicket("G") ' Establecer Datos en Config
            '    oDCOCX_EPSON_Impresora_Fiscal1 : OpenInvoice("T", "C", "A", "1", "P",
            poImpresora.OpenInvoice("T", "C", "A", "1", "P", "10", "I", "F", "Consumidor Final", _
                                    "Consumidor Final", "CUIT", "00000000000", "N", "Laguna", "Larga", _
                                    "00", "REM 1", "REM 2", "C")
            'oDCOCX_EPSON_Impresora_Fiscal1 : GetInvoiceSubtotal("P", "SUB")
            'oDCOCX_EPSON_Impresora_Fiscal1 : SendInvoicePayment("EFECTIVO", "200",
            '"T")
            'oDCOCX_EPSON_Impresora_Fiscal1 : CloseInvoice("T", "A", "")
            For i = 0 To poComprobante.DETALLE.Count - 1
                wbImprime = poImpresora.SendInvoiceItem(poComprobante.DETALLE.Item(i).cod_pronombre, _
                                            poComprobante.DETALLE.Item(i).cod_procantidad, _
                                            poComprobante.DETALLE.Item(i).cod_propciounitario, _
                                            poComprobante.DETALLE.Item(i).cod_iva, "M", "0", _
                                            "0", "EXTRA", "EXTRA", "EXTRA", "0")

            Next
            '=================== DETALLES =============================
            'Calificador Item de venta(Item 5)= 
            '                   M Monto agregado mercadería SUMA
            '                   m Reversión Resta
            '                   R Bonificación Resta
            '                   r Anula la bonificación SUMA

            wbImprime = poImpresora.GetInvoiceSubtotal("P", "SUB")
            wbImprime = poImpresora.SendInvoicePayment("EFECTIVO", poComprobante.COM_TOTALFACTRADO, "T")
            poImpresora.OpenCashDrawer("1")
            wszRetorno = poImpresora.AnswerField_3
            wbImprime = poImpresora.CloseInvoice("T", "A", " ")
            '=================== DETALLES ============================
            'Calificador del Item de Pago (Item 3)=
            '                   C Cancela Comprobante
            '                   T Suma el importe pagado
            '                   t Anula un pago hecho con Ticket
            '                   D Realiza un descuento global por monto Fijo()
            '                   R Realiza un recargo global por monto Fijo()

            'ultimo ticket poImpresora.AnswerField_3
            '6) Los tipos de categorias son:
            'cons final: F,inscripto: I,iva exento: E, monotributo: M, no inscripto: R, no responsable: N, no categorizado: S. 
            '8) Cierres Z:
            'PrinterFiscal1.CloseJournal("Z", "P")
            'Cierres(X)
            'PrinterFiscal1.CloseJournal("X", "P")
            If wbImprime = False Then
                wszRetorno = "Error"
            End If

            Return wszRetorno

        Catch ex As Exception
            Manejador_Errores("ImprimirTicket", ex)
        End Try
    End Function

    ''' <summary>
    ''' Este método permite abrir el cajón según la caja
    ''' </summary>
    ''' <param name="poImpresora">Objeto Impresora</param>
    ''' <remarks></remarks>
    Public Shared Sub AperturaCajon(ByVal poImpresora As AxPrinterFiscal)
        poImpresora.OpenCashDrawer(ObtenerConfiguracion(gstrCaja))
    End Sub

    ''' <summary>
    ''' Método que cancela un comprobante emitido
    ''' </summary>
    ''' <param name="poImpresora">Objeto Impresora</param>
    ''' <param name="poComprobante">Objeto Comprobante</param>
    ''' <returns>True o False según haya sucedido</returns>
    ''' <remarks></remarks>
    Public Shared Function CancelaTicket(ByVal poImpresora As AxPrinterFiscal, _
                                         ByVal poComprobante As clsComprobante) As Boolean
        Dim wbImprime As Boolean = False
        Dim i As Integer
        Dim wszRetorno As String
        Try

            poImpresora.PortNumber = ObtenerConfiguracion(gstrPuertoCOM)
            poImpresora.BaudRate = 9600
            poImpresora.OpenTicket("G") ' Establecer Datos en Config

            For i = 0 To poComprobante.DETALLE.Count - 1

                wbImprime = poImpresora.SendTicketItem(poComprobante.DETALLE.Item(i).cod_pronombre, _
                                                       poComprobante.DETALLE.Item(i).cod_procantidad, _
                                                       poComprobante.DETALLE.Item(i).cod_propciounitario, _
                                                       "." & poComprobante.DETALLE.Item(i).cod_iva, _
                                                       "m", "0", "0")

            Next
            '=================== DETALLES =============================
            'Calificador Item de venta(Item 5)= 
            '                   M Monto agregado mercadería SUMA
            '                   m Reversión Resta
            '                   R Bonificación Resta
            '                   r Anula la bonificación SUMA

            wbImprime = poImpresora.GetTicketSubtotal("P", "LINDO SUB")
            wbImprime = poImpresora.SendTicketPayment("Ticket", "-" & poComprobante.COM_TOTALFACTRADO, "C")
            wbImprime = poImpresora.CutPaper()
            poImpresora.OpenCashDrawer(ObtenerConfiguracion(gstrCaja))
            wszRetorno = poImpresora.AnswerField_3
            wbImprime = poImpresora.CloseTicket()
            '=================== DETALLES ============================
            'Calificador del Item de Pago (Item 3)=
            '                   C Cancela Comprobante
            '                   T Suma el importe pagado
            '                   t Anula un pago hecho con Ticket
            '                   D Realiza un descuento global por monto Fijo()
            '                   R Realiza un recargo global por monto Fijo()

            If wbImprime = False Then
                wszRetorno = "Error"
            End If

            Return wszRetorno

        Catch ex As Exception

        End Try
    End Function

End Class
