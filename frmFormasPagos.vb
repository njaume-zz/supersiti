Public Class frmFormasPagos

#Region "Métodos"

    Private Sub LimpiarCampos()
        Me.txtDescuento.Text = 0
        Me.txtSubTotal.Text = Funciones.FormatoMoneda("0.00")
        Me.txtTotalAPagar.Text = Funciones.FormatoMoneda("0.00")
        Me.txtTotalEnEfectivo.Text = Funciones.FormatoMoneda("0.00")
        Me.txtTotalEnTarjeta.Text = Funciones.FormatoMoneda("0.00")
        Me.txtVuelto.Text = Funciones.FormatoMoneda("0.00")
        Me.txtTotalEnEfectivo.Focus()
    End Sub

    Private Sub CerrarForm()
        If MsgBox("¿Está seguro que desea CANCELAR la Venta?", _
                MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CANCELACION DE VENTA ::.") = MsgBoxResult.Yes Then
            LimpiarCampos()
            Me.Close()
        End If
    End Sub
#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que consulta si se desea cancelar el comprobante de venta.-
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        CerrarForm()
    End Sub

    Private Sub frmFormasPagos_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CerrarForm()
    End Sub

    ''' <summary>
    ''' Evento que inicializa valores del formulario Formas de Pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmFormasPagos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtTotalAPagar.Text = Funciones.FormatoMoneda(Me.txtTotalAPagar.Text)
        Me.txtDescuento.Text = "0"
        Me.txtTotalEnEfectivo.Text = FormatoMoneda("0.00")
        Me.txtTotalEnTarjeta.Text = FormatoMoneda("0.00")
        Me.txtDescuento.Focus()
        Me.txtTotalAPagar.Enabled = False

    End Sub

    Private Sub txtDescuento_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescuento.GotFocus
        Dim o_Dt As DataTable
        Try
            o_Dt = New DataTable
            o_Dt = frmVentas.ol_dtUsr
            o_Dt.Select("ROL_NOMBRE = 'Administrado' OR ROL_NOMBRE = 'Supervisor'")
            If o_Dt.Rows.Count > 0 Then
                Me.txtDescuento.Enabled = True
                Me.txtDescuento.Focus()
            Else
                Me.txtDescuento.Text = "0"
                Me.txtDescuento.Enabled = False
            End If
        Catch ex As Exception
            Funciones.LogError(ex, "txtDescuento_GotFocus", Funciones.ObtieneUsuario)
        End Try
    End Sub

    ''' <summary>
    ''' Se calcula el descuento sobre el Monto Total de la Venta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescuento_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescuento.LostFocus
        Dim oiSubTotal, oiTotal, oiDescuento As String

        If Me.txtDescuento.Text <> 0 Then
            oiTotal = Me.txtTotalAPagar.Text
            oiDescuento = CInt(Me.txtDescuento.Text)
            oiSubTotal = Funciones.FormatoMoneda((oiDescuento * oiTotal / 100))
        Else
            oiTotal = Funciones.FormatoMoneda(Me.txtTotalAPagar.Text)
            oiSubTotal = Funciones.FormatoMoneda("0.00")
        End If
        Me.txtSubTotal.Text = Funciones.FormatoMoneda(oiTotal - oiSubTotal)
    End Sub

    Private Sub txtTotalEnEfectivo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalEnEfectivo.LostFocus
        If Me.txtTotalEnEfectivo.Text <> 0 And Me.txtTotalEnEfectivo.Text >= Me.txtSubTotal.Text Then
            Me.txtVuelto.Text = Funciones.FormatoMoneda(Me.txtTotalEnEfectivo.Text - Me.txtSubTotal.Text)
        Else
            Me.txtTotalEnEfectivo.Focus()
        End If
    End Sub

    Private Sub btnAceptarVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptarVenta.Click

    End Sub
#End Region


End Class