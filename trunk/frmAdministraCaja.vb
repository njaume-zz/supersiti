Public Class frmAdministraCaja

#Region "Métodos"

    Private Function Validar() As Boolean
        Dim ok As Boolean = True

        'If Me.txtBonos.Text = gdNull Then
        '    ok = False
        'End If

        'If Me.txtCredito.Text = gdNull Then
        '    ok = False
        'End If

        'If Me.txtTarjetas.Text = gdNull Then
        '    ok = False
        'End If

        If Me.txtEfectivo.Text = gdNull Then
            ok = False
        End If

        If Me.txtImporteApertura.Text = gdNull Then
            ok = False
        End If

        Return ok
    End Function

    Private Sub LimiparCampos()
        Me.txtBonos.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtCredito.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtEfectivo.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtTarjetas.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtImporteApertura.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtOperador.Text = ""
        Me.txtSubTotal.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtFecha.Text = Format(Now, "dd/MM/yyyy")
    End Sub

    Private Sub CalcularTotal()
        Dim wdSubTotal As Double

        wdSubTotal = CDec(Me.txtBonos.Text) + CDec(Me.txtCredito.Text) + CDec(Me.txtEfectivo.Text) + CDec(Me.txtTarjetas.Text)
        Me.txtSubTotal.Text = wdSubTotal.ToString
        Me.txtSubTotal.Enabled = False
        Me.txtTotalApertura.Text = Me.txtImporteApertura.Text
        Me.txtTotalRetiros.Text = Me.txtImporteRetiro.Text
        Me.txtTotalVentas.Text = Me.txtSubTotal.Text
        Me.txtImportesCaja.Text = IIf(Me.txtImportesCaja.Text = "", 0, Me.txtImportesCaja.Text)
        Me.txtTotal.Text = (CDec(Me.txtImportesCaja.Text) + (CDec(Me.txtTotalApertura.Text) + (CDec(Me.txtTotalRetiros.Text)) - CDec(Me.txtTotalVentas.Text)))
    End Sub

    Public Function VerificarApertura() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaMovimientosCaja(1, idUsr)  ' 1 = Apertura
        Return ok
    End Function

    Private Function VerificarRetiro() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaMovimientosCaja(2, idUsr) ' 2 = Retiro
        Return ok
    End Function

    Private Function VerificarCierreX() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaMovimientosCaja(3, idUsr) ' 3 = Cierre X
        Return ok
    End Function

    Private Function VerificarCierreZ() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaMovimientosCaja(4, idUsr) '4 = Cierre Z
        Return ok
    End Function

    Private Sub RecuperaVentas(ByVal piCaja As Integer)
        Dim woDt As DataTable

        Try
            woDt = New DataTable

            woDt = clsCajaDAO.ListarImporteCaja(piCaja)

            For Each oDr As DataRow In woDt.Rows

                Select Case LCase(oDr("NOMBRE"))
                    Case LCase("Efectivo")
                        Me.txtEfectivo.Text = oDr("IMPORTES").ToString
                    Case LCase("Tarjeta")
                        Me.txtTarjetas.Text = oDr("IMPORTES").ToString
                    Case LCase("Vale")
                        Me.txtBonos.Text = oDr("IMPORTES").ToString
                    Case LCase("Cuenta Corriente")
                        Me.txtCredito.Text = oDr("IMPORTES").ToString
                End Select

            Next

        Catch ex As Exception
            Funciones.LogError(ex, "RecuperaVentas", Funciones.ObtieneUsuario)
        Finally
            woDt = Nothing
        End Try
    End Sub

    Private Sub RecuperaCierres(ByVal pbEsRetiro As Boolean, ByVal piCaja As Integer)
        Dim woDt As DataTable

        Try
            woDt = New DataTable

            woDt = clsCajaDAO.ListarImporteCierres(piCaja)

            For Each oDr As DataRow In woDt.Rows

                Select Case LCase(oDr("NOMBRE"))
                    Case LCase("Apertura")
                        Me.txtImporteApertura.Text = oDr("IMPORTES").ToString
                    Case LCase("Retiro")
                        If pbEsRetiro = False Then
                            Me.txtImporteRetiro.Text = oDr("IMPORTES").ToString
                        End If
                End Select

            Next

        Catch ex As Exception
            Funciones.LogError(ex, "RecuperaCierres", Funciones.ObtieneUsuario)
        Finally
            woDt = Nothing
        End Try
    End Sub

    Private Sub HabilitarCampos(ByVal pValor As Boolean)
        Me.txtBonos.Enabled = pValor
        Me.txtEfectivo.Enabled = pValor
        Me.txtImporteApertura.Enabled = pValor
        Me.txtImporteRetiro.Enabled = pValor
        Me.txtTarjetas.Enabled = pValor
        Me.txtCredito.Enabled = pValor
        Me.txtTotalVentas.Enabled = pValor
        Me.txtTotalRetiros.Enabled = pValor
        Me.txtTotalApertura.Enabled = pValor
    End Sub
#End Region

#Region "Eventos"

    Private Sub btnConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click
        Dim Estado As Integer
        Dim wbApertura, wbCierre As Boolean
        Dim wdblImporte As Double

        Try

            If Not Validar() Then
                MsgBox("No se puede confirmar la operación porque faltan datos.-", MsgBoxStyle.Exclamation, ".:: OPERACION FALLIDA ::.")
                LimiparCampos()
            Else
                wbApertura = VerificarApertura()
                Select Case Me.lblOperacion.Text
                    'CAMBIAR LA CAJA POR UNA VARIABLE
                    Case "Apertura"  'CAE_ID = 1
                        If wbApertura = False Then
                            Dim oCajaMov As New clsCajaMovimientos(0, CDec(Me.txtImporteApertura.Text), _
                                                                   1, Now(), frmVentas.TSSIdUsuario.Text, 1, 1, Now)
                            Estado = clsCajaDAO.InsertaCajaMovimientos(oCajaMov)
                        Else
                            Funciones.Manejador_Errores("Apertura de Caja", New Exception("Ya existe una Apertura pendiende de cierre.-"))
                        End If
                        'Me.Hide()
                        'frmVentas.ShowDialog()
                    Case "Retiro"    'CAE_ID = 2
                        If wbApertura = True Then
                            Dim oCajaMov As New clsCajaMovimientos(0, CDec(Me.txtImporteApertura.Text), _
                                                                   2, Now(), frmVentas.TSSIdUsuario.Text, 1, 1, Now)
                            Estado = clsCajaDAO.InsertaCajaMovimientos(oCajaMov)
                        Else
                            Funciones.Manejador_Errores("Retiro de Dinero", New Exception("No existe una Apertura de Caja, por eso no se puede Retirar dinero.-"))
                        End If
                        'Me.Hide()
                        'frmVentas.ShowDialog()
                    Case "Cierre X"  'CAE_ID = 3
                        If wbApertura = True Then
                            Dim oCajaMov As New clsCajaMovimientos(0, CDec(Me.txtTotalVentas.Text), _
                                                                   3, Now(), frmVentas.TSSIdUsuario.Text, _
                                                                   ObtenerConfiguracion(gstrCaja), 1, Now)
                            Estado = clsCajaDAO.InsertaCajaMovimientos(oCajaMov)
                            '1º Recupero Importes de Caja entre fechas
                            '2º Sumo las ventas y descuento la (Apertura + Retiro)
                            'listar los valores recuperados
                            'recuperar los importes de cajas.
                            'Movimientos,Usuario, Caja, Tipo Movimiento
                            'RecuperaImportesCaja(CAM_ID,USU_ID,CAJ_ID,3)
                            wbCierre = clsImpresiones.CierreX(EpsonPrinter, True)
                            If wbCierre Then
                                Estado = 1
                            Else
                                Estado = -1
                            End If
                        Else
                            Funciones.Manejador_Errores("Cierre X de Caja", New Exception("No existe una Apertura pendiende de cierre.-"))
                        End If
                    Case "Cierre Z"  'CAE_ID = 4
                        If wbApertura = True Then
                            Dim oCajaMov As New clsCajaMovimientos(0, CDec(Me.txtImporteApertura.Text), _
                                                                   4, Now(), frmVentas.TSSIdUsuario.Text, 1, 1, Now)
                            Estado = clsCajaDAO.InsertaCajaMovimientos(oCajaMov)
                            wbCierre = clsImpresiones.CierreZ(EpsonPrinter, True)
                            If wbCierre Then
                                Estado = 1
                            Else
                                Estado = -1
                            End If
                        Else
                            Funciones.Manejador_Errores("Cierre Z de Caja", New Exception("No existe una Apertura pendiende de cierre.-"))
                        End If
                End Select
                If Estado = -1 Then
                    MessageBox.Show("La operación " & Me.lblOperacion.Text & " falló y no pudo guardarse.-", ".:: ERROR EN CAJAS ::.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    LogError(New Exception, "Error al Confirmar acciones de Caja", ObtieneUsuario)
                    'GUARDAR EN ARCHIVO LOG
                Else
                    MessageBox.Show("La operación " & Me.lblOperacion.Text & " se realizó con éxito.-", ".:: Operación Exitosa ::.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Me.Hide()
                frmVentas.ShowDialog()
            End If

        Catch ex As Exception
            Funciones.LogError(ex, "Confimación de operación en caja - " & Me.lblOperacion.Text, frmVentas.TSSUsuario.Text)
        End Try
    End Sub

    Private Sub frmAdministraCaja_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtOperador.Text = ObtieneUsuario()
        Me.txtFecha.Text = Format(Now, "dd/MM/yyyy")
        If Me.lblOperacion.Text = "Apertura" Then
            Me.txtImporteApertura.Enabled = True
        Else
            Me.txtImporteApertura.Enabled = False
        End If

        Select Case Me.lblOperacion.Text
            Case "Apertura"
                'acá se llama a los métodos para determinar si no hay aperturas pendientes
                ' para este usuario 
                If VerificarApertura() Then
                    MessageBox.Show("Existe una APERTURA PENDIENTE para el Usuario: " & frmVentas.TSSUsuario.Text & "." & _
                                    ControlChars.CrLf & " Debe realizar el Cierre X, primero.", ".:: Caja con Apertura ::.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.Close()
                End If
                Me.txtImporteRetiro.Enabled = False
                Me.txtImporteApertura.Enabled = True
            Case "Retiro"
                'acá se llama a los métodos para determinar si hay una apertura para ese usuario
                ' se debe recuperar el importe de apertura y los retiros hechos por el usuario.
                'RecuperaImporteApertura()
                'RecuperaVentas()
                RecuperaCierres(True, Funciones.ObtenerConfiguracion(Definiciones.gstrCaja)) 'envío verdadero para indicar que es un retiro de dinero
                VerificarRetiro()
                Me.txtImporteRetiro.Enabled = True
                Me.txtImporteApertura.Enabled = False
            Case "Cierre X"
                'acá se llama a los métodos para determinar si hay una apertura para ese usuario
                'si se permite el cierre, se recupera de la base los importes de las ventas
                'Estas ventas se deberían recuperar por cada tipo de pago.
                'Hacer un Store con la consulta hecha en el ESCRITORIO
                RecuperaVentas(Funciones.ObtenerConfiguracion(Definiciones.gstrCaja))
                RecuperaCierres(False, Funciones.ObtenerConfiguracion(Definiciones.gstrCaja)) 'envío falso para indicar que no es retiro de dinero
                CalcularTotal()
                HabilitarCampos(False)
            Case "Cierre Z"
                ' en este caso de deben listar todos los importes de todas las cajas
                RecuperaVentas(0) 'envío 0 para recuperar todas las cajas
                RecuperaCierres(False, Funciones.ObtenerConfiguracion(Definiciones.gstrCaja)) 'envío falso para indicar que no es retiro de dinero
                HabilitarCampos(False)
        End Select
    End Sub

    Private Sub txtBonos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBonos.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtBonos_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBonos.LostFocus
        If Me.txtBonos.Text = "" Or Me.txtBonos.Text = "0" Then
            Me.txtBonos.Text = gdNull
        End If
        Me.txtBonos.Text = Convert.ToDouble(Me.txtBonos.Text).ToString("###0.00")
        CalcularTotal()
    End Sub

    Private Sub txtEfectivo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEfectivo.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtEfectivo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEfectivo.LostFocus
        If Me.txtEfectivo.Text = "" Or Me.txtEfectivo.Text = "0" Then
            Me.txtEfectivo.Text = gdNull
        End If
        Me.txtEfectivo.Text = Convert.ToDouble(Me.txtEfectivo.Text).ToString("###0.00")
        CalcularTotal()
    End Sub

    Private Sub txtCredito_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCredito.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtCredito_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCredito.LostFocus
        If Me.txtCredito.Text = "" Or Me.txtCredito.Text = "0" Then
            Me.txtCredito.Text = gdNull
        End If
        Me.txtCredito.Text = Convert.ToDouble(Me.txtCredito.Text).ToString("###0.00")
        CalcularTotal()
    End Sub

    Private Sub txtTarjetas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTarjetas.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtTarjetas_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTarjetas.LostFocus
        If Me.txtTarjetas.Text = "" Or Me.txtTarjetas.Text = "0" Then
            Me.txtTarjetas.Text = gdNull
        End If
        Me.txtTarjetas.Text = Convert.ToDouble(Me.txtTarjetas.Text).ToString("###0.00")
        CalcularTotal()
    End Sub

    Private Sub txtImporteApertura_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteApertura.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtImporteApertura_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImporteApertura.LostFocus
        If Me.txtImporteApertura.Text = "" Or Me.txtImporteApertura.Text = "0" Then
            Me.txtImporteApertura.Text = gdNull
        End If
        Me.txtImporteApertura.Text = Convert.ToDouble(Me.txtImporteApertura.Text).ToString("###0.00")
        CalcularTotal()
    End Sub

    Private Sub txtImporteRetiro_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteRetiro.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtImporteRetiro_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImporteRetiro.LostFocus
        If Me.txtImporteRetiro.Text = "" Or Me.txtImporteRetiro.Text = "0" Then
            Me.txtImporteRetiro.Text = gdNull
        End If
        Me.txtImporteRetiro.Text = Convert.ToDouble(Me.txtImporteRetiro.Text).ToString("###0.00")
        CalcularTotal()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If MsgBox("¿Está seguro que desea cerrar la Administración de Cajas?", _
                          MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CIERRE DE ADMINISTRACION DE CAJA ::.") = MsgBoxResult.Yes Then
            LimiparCampos()
            Me.Close()
        End If
    End Sub

    Private Sub txtImportesCaja_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImportesCaja.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtImportesCaja_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImportesCaja.LostFocus
        'Me.txtTotal.Text = (CDec(Me.txtTotal.Text) - CDec(IIf(Me.txtImportesCaja.Text = "", 0, Me.txtImportesCaja.Text)))
        Me.txtImportesCaja.Text = CDec(IIf(Me.txtImportesCaja.Text = "", 0, Me.txtImportesCaja.Text)).ToString("###0.00")
        CalcularTotal()
    End Sub
#End Region


End Class