Imports AxEPSON_Impresora_Fiscal

Public Class clsImpresiones

    'Public Function CierreX() As Boolean
    '    Dim oImpresora As FiscalNET.EpsonTicket
    '    Dim wbCierre As Boolean
    '    Dim algo As String
    '    Try
    '        oImpresora = Nothing

    '        wbCierre = oImpresora.CloseJournal("X")
    '        oImpresora.CutPaper()
    '        algo = oImpresora.GetTicketSubtotal("0000000110")
    '        CierreX = wbCierre
    '    Catch ex As Exception

    '    End Try
    'End Function

    Public Shared Function ImprimirTicket(ByVal poImpresora As AxPrinterFiscal, _
                                          ByVal poComprobante As clsComprobante) As Boolean
        'Dim oImpresora As EPSON_Impresora_Fiscal.PrinterFiscal
        Dim wbImprime As Boolean = False
        Dim i As Integer
        Dim wszFormaPago As String
        Dim oDt As DataTable
        Dim oFormaDePago As clsFormaPago

        Try
            oDt = New DataTable
            oFormaDePago = New clsFormaPago()

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
            'oFormaDePago.FOP_ID = poComprobante.FOP_ID
            'oDt = clsFormaPagoDAO.GetTable(oFormaDePago)
            'If oDt.Rows.Count > 0 Then
            '    wszFormaPago = oDt.Rows(0).Item("FOP_NOMBRE")
            'Else
            '    wszFormaPago = "Efectivo"
            'End If
            wbImprime = poImpresora.GetTicketSubtotal("P", "LINDO SUB")
            wbImprime = poImpresora.SendTicketPayment("TicketA", poComprobante.COM_TOTALFACTRADO, "T")
            wbImprime = poImpresora.CloseTicket()
            'ultimo ticket poImpresora.AnswerField_3
            '6) Los tipos de categorias son:
            'cons final: F,inscripto: I,iva exento: E, monotributo: M, no inscripto: R, no responsable: N, no categorizado: S. 
            '8) Cierres Z:
            'PrinterFiscal1.CloseJournal("Z", "P")
            'Cierres(X)
            'PrinterFiscal1.CloseJournal("X", "P")
            Return wbImprime

        Catch ex As Exception

        End Try

    End Function

    'Public Function ImprimirTicket1() As Boolean
    '    'Dim oImpresora As EPSON_Impresora_Fiscal.PrinterFiscal
    '    'Dim oImp As EPSON_Impresora_Fiscal.PrinterFiscal
    '    Dim oImpresora As Object

    '    Dim wbApertura As Boolean
    '    Dim respuesta
    '    Try
    '        oImpresora = CreateObject("EPSON_Impresora_Fistal.PrinterFiscal")

    '        'oImpresora.ConnectToNewObject("EPSON_Impresora_Fiscal.PrinterFiscal")

    '        oImpresora.BaudRate = 9600
    '        oImpresora.PortNumber = "COM1"

    '        respuesta = oImpresora.OpenInvoice("T", "C", "A", "1", "P", "12", "I", "I", "PEPE", "LE BOU", "CUIT", "30614104712", "N", "LA", "PAMPA", "98", "REM 1", "REM 2", "G")
    '        If respuesta Then respuesta = oImpresora.SendInvoiceItem("ARTICULO 1", "1000", "100", "2100", "M", "0", "0", "EXTRA", "EXTRA", "EXTRA", "1050", "0")
    '        If respuesta Then respuesta = oImpresora.GetInvoiceSubtotal("P", "LINDO SUB")
    '        If respuesta Then respuesta = oImpresora.SendInvoicePayment("PAGO1", "200", "T")
    '        If respuesta Then respuesta = oImpresora.CloseInvoice("T", "A", "HOLA")

    '        MsgBox(respuesta & Chr(13) & oImpresora.FiscalStatus & Chr(13) & oImpresora.PrinterStatus)

    '        If respuesta <> -1 Then
    '            wbApertura = True
    '        End If
    '        Return wbApertura
    '    Catch ex As Exception

    '    End Try

    'End Function

End Class
