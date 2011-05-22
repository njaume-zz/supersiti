
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

    Public Function ImprimirTicket() As Boolean
        Dim oImpresora As FiscalPrinterLib.HASAR
        Dim wbImprime As Boolean = False
        Dim wiImpuesto, wiTotal, wiOpen, wiClose As Integer
        Dim wszFact As String

        Try

            oImpresora = New FiscalPrinterLib.HASAR
            'dat1, G o C
            oImpresora.FISOpenTicket("G")
            '---------------------------------------------------------
            '       1 - (dat1) Descripción
            '       2 - (dat2) Cantidad de items
            '       3 - (dat3) Monto del item
            '       4 - (dat4) Tasa de iva estandar
            '       5 - (dat5) Calificador de item
            '       6 - (dat6) Unidades (bultos)
            '       7 - (dat7) Tasa de imp int porecentuales K
            '       8 - (dat8) Impuestos internos fijos
            wbImprime = oImpresora.FISSendItemTique("Descrip", "2", "2.00", "21", "123", "5", "21", "10")
            'P o N, descripcion
            wbImprime = oImpresora.FISSubTique("P", "Imprimir")

            wbImprime = oImpresora.FISCierreTique()
            'oImpresora.Comenzar()
            ''oImpresora.Baudios = 9600
            ''oImpresora.Puerto = 1
            'oImpresora.UsarDisplay = False
            ''oImpresora.MODELO_614()
            'oImpresora.ImprimirTextoNoFiscal("TEXTO NO FISCAL")
            'oImpresora.AutodetectarControlador(1)
            'oImpresora.Comenzar()
            'oImpresora.ImprimirItem("Descrip", 2, 2.0, 21.0, 12.0)
            'oImpresora.ImprimirPago("Pago", 21.2)
            'wiTotal = oImpresora.Respuesta(3)
            'wiClose = oImpresora.UltimoTicket

            'wbImprime = True


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
