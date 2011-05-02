Public Class frmAdministraCaja

#Region "Métodos"

    Private Function Validar() As Boolean
        Dim ok As Boolean
        If Me.txtBonos.Text = gdNull Or Me.txtCredito.Text = gdNull Or Me.txtEfectivo.Text = gdNull Or _
            Me.txtTarjetas.Text = gdNull Or Me.txtImporteApertura.Text = gdNull Then
            ok = False
        Else
            ok = True
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
        Me.txtTotalCaja.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtFecha.Text = Format(Now, "dd/MM/yyyy")
    End Sub

    Private Sub CalcularTotal()
        Dim total As Double

        total = CDbl(Me.txtBonos.Text) + CDbl(Me.txtCredito.Text) + CDbl(Me.txtEfectivo.Text) + CDbl(Me.txtTarjetas.Text)
        Me.txtTotalCaja.Text = Str(CDbl(Me.txtImporteApertura.Text) - total)
        Me.txtTotalCaja.Enabled = False
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

    Private Function RecuperaImportesCaja() As DataTable
        Dim woDt As DataTable
        Try
            woDt = New DataTable
            ' obtener id de usuario!
            woDt = clsCajaDAO.ListarImporteCaja(0, frmVentas.TSSIdUsuario.Text, Funciones.ObtenerConfiguracion(gstrCaja), 3)

        Catch ex As Exception
            Funciones.LogError(ex, "RecuperaImportesCaja", Funciones.ObtieneUsuario)
        Finally
            woDt = Nothing
        End Try
        Return woDt
    End Function
#End Region

#Region "Eventos"

    Private Sub btnConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click
        Dim Estado As Integer
        Dim oDt As DataTable
        Dim wbApertura As Boolean
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
                        Me.Hide()
                        frmVentas.ShowDialog()
                    Case "Retiro"    'CAE_ID = 2
                        If wbApertura = True Then
                            Dim oCajaMov As New clsCajaMovimientos(0, CDec(Me.txtImporteApertura.Text), _
                                                                   2, Now(), frmVentas.TSSIdUsuario.Text, 1, 1, Now)
                            Estado = clsCajaDAO.InsertaCajaMovimientos(oCajaMov)
                        Else
                            Funciones.Manejador_Errores("Retiro de Dinero", New Exception("No existe una Apertura de Caja, por eso no se puede Retirar dinero.-"))
                        End If
                        Me.Hide()
                        frmVentas.ShowDialog()
                    Case "Cierre X"  'CAE_ID = 3
                        If wbApertura = True Then
                            Dim oCajaMov As New clsCajaMovimientos(0, CDec(Me.txtImporteApertura.Text), _
                                                                   3, Now(), frmVentas.TSSIdUsuario.Text, 1, 1, Now)
                            Estado = clsCajaDAO.InsertaCajaMovimientos(oCajaMov)
                            oDt = New DataTable
                            oDt = RecuperaImportesCaja()
                            '1º Recupero Importes de Caja entre fechas
                            '2º Sumo las ventas y descuento la (Apertura + Retiro)
                            'listar los valores recuperados
                            'recuperar los importes de cajas.
                            'Movimientos,Usuario, Caja, Tipo Movimiento
                            'RecuperaImportesCaja(CAM_ID,USU_ID,CAJ_ID,3)
                        Else
                            Funciones.Manejador_Errores("Cierre X de Caja", New Exception("No existe una Apertura pendiende de cierre.-"))
                        End If
                    Case "Cierre Z"  'CAE_ID = 4
                        If wbApertura = True Then
                            Dim oCajaMov As New clsCajaMovimientos(0, CDec(Me.txtImporteApertura.Text), _
                                                                   4, Now(), frmVentas.TSSIdUsuario.Text, 1, 1, Now)
                            Estado = clsCajaDAO.InsertaCajaMovimientos(oCajaMov)
                        Else
                            Funciones.Manejador_Errores("Cierre Z de Caja", New Exception("No existe una Apertura pendiende de cierre.-"))
                        End If
                End Select
                If Estado = -1 Then
                    MessageBox.Show("La operación " & Me.lblOperacion.Text & " falló y no pudo guardarse.-", ".:: ERROR EN CAJAS ::.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'GUARDAR EN ARCHIVO LOG
                Else
                    MessageBox.Show("La operación " & Me.lblOperacion.Text & " se realizó con éxito.-", ".:: Operación Exitosa ::.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
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
                VerificarRetiro()
                Me.txtImporteRetiro.Enabled = True
                Me.txtImporteApertura.Enabled = False
            Case "Cierre X"
                'acá se llama a los métodos para determinar si hay una apertura para ese usuario
                'si se permite el cierre, se recupera de la base los importes de las ventas
                'Estas ventas se deberían recuperar por cada tipo de pago.
                'Hacer un Store con la consulta hecha en el ESCRITORIO
            Case "Cierre Z"

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
        If Me.txtBonos.Text = "" Then
            Me.txtBonos.Text = "0.00"
        End If
        Me.txtBonos.Text = Convert.ToDouble(Me.txtBonos.Text)
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
        If Me.txtEfectivo.Text = "" Then
            Me.txtEfectivo.Text = "0.00"
        End If
        Me.txtEfectivo.Text = Convert.ToDouble(Me.txtEfectivo.Text)
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
        If Me.txtCredito.Text = "" Then
            Me.txtCredito.Text = "0.00"
        End If
        Me.txtCredito.Text = Convert.ToDouble(Me.txtCredito.Text)
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
        If Me.txtTarjetas.Text = "" Then
            Me.txtTarjetas.Text = "0.00"
        End If
        Me.txtTarjetas.Text = Convert.ToDouble(Me.txtTarjetas.Text)
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
        If Me.txtImporteApertura.Text = "" Then
            Me.txtImporteApertura.Text = "0.00"
        End If
        Me.txtImporteApertura.Text = Convert.ToDouble(Me.txtImporteApertura.Text)
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
        If Me.txtImporteRetiro.Text = "" Then
            Me.txtImporteRetiro.Text = "0.00"
        End If
        Me.txtImporteRetiro.Text = Convert.ToDouble(Me.txtImporteRetiro.Text)
        CalcularTotal()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If MsgBox("¿Está seguro que desea cerrar la Administración de Cajas?", _
                          MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CIERRE DE ADMINISTRACION DE CAJA ::.") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

#End Region
End Class