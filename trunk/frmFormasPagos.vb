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

    Private Function ValidaPago() As String
        If Me.txtSubTotal.Text = "" Then
            ValidaPago = "El campo SubTotal no puede estar vacío.-"
            Exit Function
        End If
        If Me.txtTotalAPagar.Text = "" Then
            ValidaPago = "El campo Total a Pagar no puede estar vacío.-"
            Exit Function
        End If
        If Me.txtTotalEnEfectivo.Text = "" Then
            ValidaPago = "El campo Total en Efectivo no puede estar vacío.-"
            Exit Function
        End If
        If Me.txtTotalEnEfectivo.Text < Me.txtTotalAPagar.Text Then
            ValidaPago = "El importe a Abonar, debe ser mayor o igual al Total a Pagar.-"
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' Inicializa los valores del Compobante, tomando los valores del formulario
    ''' </summary>
    ''' <param name="poComprobante">Objeto Comprobante</param>
    ''' <returns>Objeto Comprobante</returns>
    ''' <remarks></remarks>
    Private Function InicializaComprobante(ByVal poComprobante As clsComprobante) As clsComprobante
        '--------------------------------------------------------
        'Datos propios obtenidos de un archivo de configuración
        '--------------------------------------------------------
        poComprobante.COM_CUITPROPIO = Funciones.ObtenerConfiguracion("DatosPropios/CUIT")
        poComprobante.COM_INGRESOBRUTOPROPIO = Funciones.ObtenerConfiguracion("DatosPropios/IngresosBrutos")
        poComprobante.COM_DOMICILIOPROPIO = Funciones.ObtenerConfiguracion("DatosPropios/DomicilioFiscal")
        poComprobante.COM_RAZONSOCIALPROPIO = Funciones.ObtenerConfiguracion("DatosPropios/RazonSocial")
        poComprobante.COM_TELEFONOPROPIO = Funciones.ObtenerConfiguracion("DatosPropios/Telefono")
        '--------------------------------------------------------
        poComprobante.COM_ID = 0
        poComprobante.COM_FECHA = Now
        'sección hardcodeado porque no existen datos de clientes
        '--------------------------------------------------------
        poComprobante.COM_CLICUIT = "99-99999999-9"
        poComprobante.COM_CLIDOMICILIO = ""
        poComprobante.COM_CLIDOMICILIOENTREGA = ""
        poComprobante.COM_CLIINGRESOBRUTO = ""
        poComprobante.COM_CLIRAZONSOCIAL = "Consumidor Final"
        poComprobante.COM_CLITELEFONO = ""
        poComprobante.COM_IMPORTEGRAVADO = Funciones.FormatoMoneda("0.00")
        poComprobante.COM_IMPORTENOGRAVADO = Funciones.FormatoMoneda("0.00")
        poComprobante.COM_IMPRESO = "S"
        poComprobante.COM_IVAFACTURADO = Funciones.FormatoMoneda("0.00")
        poComprobante.COM_NROEMITIDO = clsComprobanteDAO.ObtieneNroComprobante() 'Hacer función
        poComprobante.COM_PTOVTA = Funciones.ObtenerPuntoVenta()
        poComprobante.COM_TOTALFACTRADO = Funciones.FormatoMoneda(Me.txtTotalAPagar.Text)
        poComprobante.COM_USUNOMBRE = Funciones.ObtieneUsuario

        poComprobante.CTC_ID = Me.cmbTipoComprobante.SelectedValue

        If Me.txtTotalEnTarjeta.Text = "0.00" Then
            'momentaneamente va en duro.
            poComprobante.FOP_ID = 1
        End If
        'ver bien si conviene ir a buscarlo a la base.
        poComprobante.CAJ_ID = Funciones.ObtenerConfiguracion("Venta/NroCaja")

        Return poComprobante
    End Function

    ''' <summary>
    ''' Inicializa los valores del Detalle del comprobante tomándolos del formulario.
    ''' </summary>
    ''' <param name="piNroComp">Objeto ComprobanteDetalle</param>
    ''' <returns>Objeto ComprobanteDetalle</returns>
    ''' <remarks></remarks>
    Private Function InicializaDetalle(ByVal piNroComp As Integer) As clsComprobanteDetalle
        Dim oDetalle As New clsComprobanteDetalle
        oDetalle.COD_ID = 0
        oDetalle.COD_IVA = 0
        oDetalle.COD_PESABLE = "1"
        oDetalle.COD_PRECIOCANTIDAD = Funciones.FormatoMoneda("0.00")
        oDetalle.COD_PROCANTIDAD = "0"
        oDetalle.COD_PROCODIGO = ""
        oDetalle.COD_PRONOMBRE = ""
        oDetalle.COD_PROPCIOUNITARIO = Funciones.FormatoMoneda("0.00")
        oDetalle.COM_ID = piNroComp

        Return oDetalle
    End Function

    Private Sub LlenarTipoComprobante()
        Dim oDt As DataTable
        Dim oTipoComp As clsTipoComprobante
        Try
            oDt = New DataTable
            oTipoComp = New clsTipoComprobante
            oTipoComp.CTC_CODIGO = ""
            oTipoComp.CTC_DESCRIPCION = ""
            oTipoComp.CTC_ID = 0
            oTipoComp.CTC_LETRA = ""
            oTipoComp.CTC_SIGLA = ""
            oTipoComp.CTC_SIGNO = ""
            oTipoComp.CTC_UltimoNro = ""
            Me.cmbTipoComprobante.DisplayMember = "CTC_DESCRIPCION"
            Me.cmbTipoComprobante.ValueMember = "CTC_ID"
            oDt = clsTipoComprobanteDAO.GetTable(oTipoComp)
            Me.cmbTipoComprobante.DataSource = oDt
        Catch ex As Exception

        End Try
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
        If MsgBox("¿Está seguro que desea CANCELAR la Venta?", _
        MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CANCELACION DE VENTA ::.") = MsgBoxResult.Yes Then
            LimpiarCampos()
            Hide()
        End If
    End Sub

    Private Sub frmFormasPagos_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("¿Está seguro que desea CANCELAR la Venta?", _
                MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CANCELACION DE VENTA ::.") = MsgBoxResult.Yes Then
            LimpiarCampos()
            Hide()
        End If
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
        LlenarTipoComprobante()
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
        Dim oComprobante As clsComprobante
        Dim oDetalle() As clsComprobanteDetalle
        ' ver de hacer un arraylist con el detalle
        Dim wiNroComprobante As Integer
        Dim wiNroDetalle As Integer
        Try
            oComprobante = New clsComprobante
            'oDetalle() = New clsComprobanteDetalle
            If ValidaPago() <> "" Or ValidaPago() = Nothing Then
                'Cargo datos del comprobante
                oComprobante = InicializaComprobante(oComprobante)
                wiNroComprobante = clsComprobanteDAO.Insertar(oComprobante)

                For i = 0 To frmVentas.DataGridView1.Rows.Count
                    'oDetalle(i) = New clsComprobanteDetalle
                    oDetalle(i) = InicializaDetalle(wiNroComprobante)
                    wiNroDetalle = clsComprobanteDetalleDAO.Insertar(oDetalle(i))
                    oDetalle(i) = Nothing
                Next

                LimpiarCampos()
                frmVentas.Inicializar()
                'guardar detalle y ver bien que devuelve
            End If

        Catch ex As Exception
            Funciones.Manejador_Errores("btnAceptarVenta_Click", ex)
            Funciones.LogError(ex, "btnAceptarVenta_Click", Funciones.ObtieneUsuario)
        Finally
            oComprobante = Nothing
            oDetalle = Nothing
        End Try
    End Sub

    Private Sub txtTotalEnTarjeta_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalEnTarjeta.GotFocus
        frmAutorizacion.Show()
    End Sub
#End Region

End Class