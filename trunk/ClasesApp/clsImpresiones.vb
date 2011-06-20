
Imports AxEPSON_Impresora_Fiscal
Imports CapaNegocio.CapaNegocio

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

        Finally
            poImpresora.Dispose()
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

        Finally
            poImpresora.Dispose()
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
        Dim wszImporte, wszDetalle, wszImporteIva As String
        Try

            poImpresora.PortNumber = ObtenerConfiguracion(gstrPuertoCOM)
            poImpresora.BaudRate = 9600
            poImpresora.MessagesOn = True
            wbImprime = poImpresora.OpenTicket("G") ' Establecer Datos en Config
            If wbImprime Then
                For i = 0 To poComprobante.DETALLE.Count - 1
                    If poComprobante.DETALLE.Item(i).cod_pronombre.ToString().Length >= 20 Then
                        wszDetalle = poComprobante.DETALLE.Item(i).cod_pronombre.ToString().Substring(0, 19)
                    Else
                        wszDetalle = poComprobante.DETALLE.Item(i).cod_pronombre.ToString()
                    End If
                    wszImporte = Funciones.CompletaCeros(poComprobante.DETALLE.Item(i).cod_propciounitario.ToString, 9)
                    wszImporteIva = Funciones.CompletaCeros((poComprobante.DETALLE.Item(i).cod_iva * 100).ToString(), 4)
                    wbImprime = poImpresora.SendTicketItem(wszDetalle, _
                                                           Funciones.CompletaCeros(CInt(poComprobante.DETALLE.Item(i).cod_procantidad) * 100, 8), _
                                                           wszImporte, _
                                                           wszImporteIva, _
                                                           "M", Funciones.CompletaCeros("1", 5), Funciones.CompletaCeros("0", 8), _
                                                           Funciones.CompletaCeros("0", 15))
                Next
                '=================== DETALLES =============================
                'Calificador Item de venta(Item 5)= 
                '                   M Monto agregado mercadería SUMA
                '                   m Reversión Resta
                '                   R Bonificación Resta
                '                   r Anula la bonificación SUMA

                wbImprime = poImpresora.GetTicketSubtotal("P", "SUBTOTAL")
                wbImprime = poImpresora.SendTicketPayment("Ticket", Funciones.CompletaCeros(poComprobante.COM_TOTALFACTRADO.ToString, 9), "T")
                wbImprime = poImpresora.CutPaper()
                poImpresora.OpenCashDrawer(ObtenerConfiguracion(gstrCaja))
                wszRetorno = poImpresora.AnswerField_3
                wbImprime = poImpresora.CloseTicket()
            End If
            If wbImprime = False Then
                wszRetorno = "Error"
                poImpresora.CloseTicket()
            End If
    
            Return wszRetorno

        Catch ex As Exception
            Manejador_Errores("ImprimirTicket", ex)
            poImpresora.CloseTicket()
        Finally
            poImpresora.Dispose()
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
        Dim wszDetalle As String
        Dim wszImporte As String
        Dim wszImporteIva As String

        Try

            poImpresora.PortNumber = ObtenerConfiguracion(gstrPuertoCOM)
            poImpresora.BaudRate = 9600
            wbImprime = poImpresora.OpenTicket("G") ' Establecer Datos en Config
            If wbImprime Then
                For i = 0 To poComprobante.DETALLE.Count - 1
                    If poComprobante.DETALLE.Item(i).cod_pronombre.ToString().Length >= 20 Then
                        wszDetalle = poComprobante.DETALLE.Item(i).cod_pronombre.ToString().Substring(0, 19)
                    Else
                        wszDetalle = poComprobante.DETALLE.Item(i).cod_pronombre.ToString()
                    End If
                    wszImporte = Funciones.CompletaCeros(poComprobante.DETALLE.Item(i).cod_propciounitario.ToString, 9)
                    wszImporteIva = Funciones.CompletaCeros((poComprobante.DETALLE.Item(i).cod_iva * 100).ToString(), 4)
                    wbImprime = poImpresora.SendTicketItem(wszDetalle, _
                                                           Funciones.CompletaCeros(CInt(poComprobante.DETALLE.Item(i).cod_procantidad) * 100, 8), _
                                                           wszImporte, _
                                                           wszImporteIva, _
                                                           "m", Funciones.CompletaCeros("1", 5), Funciones.CompletaCeros("0", 8), _
                                                           Funciones.CompletaCeros("0", 15))
                Next
                '=================== DETALLES =============================
                'Calificador Item de venta(Item 5)= 
                '                   M Monto agregado mercadería SUMA
                '                   m Reversión Resta
                '                   R Bonificación Resta
                '                   r Anula la bonificación SUMA

                wbImprime = poImpresora.GetTicketSubtotal("P", "SUBTOTAL")
                wbImprime = poImpresora.SendTicketPayment("Ticket", Funciones.CompletaCeros(poComprobante.COM_TOTALFACTRADO.ToString, 9), "T")
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
            End If
            If wbImprime = False Then
                wszRetorno = "Error"
            End If

            Return wszRetorno

        Catch ex As Exception

        Finally
            poImpresora.Dispose()
        End Try
    End Function

End Class
