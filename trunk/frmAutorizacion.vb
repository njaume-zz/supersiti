Public Class frmAutorizacion

#Region "Variables locales"
    Public pAccionPosterior As Integer
#End Region

#Region "Eventos"
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'validar usuario autorizado para la venta
        Dim obAutorizado As Boolean

        Try
            obAutorizado = Funciones.AutorizarMovimientos(Trim(Me.txtUsuario.Text), Trim(Me.txtPassword.Text))
            If obAutorizado Then
                Select Case pAccionPosterior
                    Case Definiciones.gcAccionAutorizaVenta
                        'autoriza la venta, con lo cual, se debe cerrar acá y levantar el 
                        'formulario de Formas de Pago. 
                        If frmVentas.DataGridView1.RowCount > 0 Then
                            frmFormasPagos.txtTotalAPagar.Text = Funciones.FormatoMoneda(frmVentas.txtPcioTotal.Text)
                            frmFormasPagos.ShowDialog()
                        End If
                    Case Definiciones.gcAccionAutorizaDescuento
                        'autoriza el descuento, se debe cerrar acá y continuar con el 
                        'formulario de pagos.
                    Case Definiciones.gcAccionAutorizaTarjeta
                        'autoriza el pago con Tarjeta y se debe cerar acá y continuar con el
                        'formulario de pagos.
                End Select
                Me.Close()
            Else
                MessageBox.Show("El USUARIO y CONTRASEÑA ingresadas son INCORRECTAS o no tiene permisos.", ".:: ADVERTENCIA ::.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If
        Catch ex As Exception
            Funciones.LogError(ex, "Aceptar - Autorizaion", Funciones.ObtieneUsuario)
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If MsgBox("¿Está seguro que desea cerrar la Administración de Cajas?", _
                  MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CIERRE DE ADMINISTRACION DE CAJA ::.") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Asc(Keys.Enter) Then
            Call btnAceptar_Click(sender, e)
        End If
    End Sub

    Private Sub txtUsuario_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsuario.KeyDown
        If e.KeyCode = Asc(Keys.Enter) Then
            Me.txtPassword.Focus()
        End If
    End Sub
#End Region


End Class