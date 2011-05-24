Public Class frmFormasPagos

#Region "Variables"
    Public goDt As DataTable
    Public gbAutorizado As Boolean
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que inicializa los campos en vacío
    ''' </summary>
    ''' <remarks>madad</remarks>
    Private Sub LimpiarCampos()
        Me.txtDescuento.Text = 0
        Me.txtSubTotal.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtTotalAPagar.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtTotalEnEfectivo.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtTotalEnTarjeta.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtVuelto.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtTotalEnEfectivo.Focus()
    End Sub

    ''' <summary>
    ''' Método que valida las formas de pago.
    ''' </summary>
    ''' <returns>string con mensaje a mostrar</returns>
    ''' <remarks>madad</remarks>
    Private Function ValidaPago() As String
        Try

            If Me.txtSubTotal.Text = "" Or Me.txtSubTotal.Text = "0" Or Me.txtSubTotal.Text = Definiciones.gdNull Then
                ValidaPago = "El campo SubTotal no puede estar vacío.-"
                Exit Function
            End If
            If Me.txtTotalAPagar.Text = "" Or Me.txtTotalAPagar.Text = "0" Or Me.txtTotalAPagar.Text = Definiciones.gdNull Then
                ValidaPago = "El campo Total a Pagar no puede estar vacío.-"
                Exit Function
            End If
            If Me.txtTotalEnEfectivo.Text = "" Or Me.txtTotalEnEfectivo.Text = 0 Or Me.txtTotalEnEfectivo.Text = Definiciones.gdNull Then
                ValidaPago = "El campo Total en Efectivo no puede estar vacío.-"
                Exit Function
            End If
            If CDbl(Me.txtTotalEnEfectivo.Text) < CDbl(Me.txtSubTotal.Text) Then
                ValidaPago = "El importe a Abonar, debe ser mayor o igual al Total a Pagar.-"
                Exit Function
            End If

        Catch ex As Exception
            ValidaPago = ""
        End Try
    End Function

    ''' <summary>
    ''' Inicializa los valores del Compobante, tomando los valores del formulario
    ''' </summary>
    ''' <param name="poComprobante">Objeto Comprobante</param>
    ''' <returns>Objeto Comprobante</returns>
    ''' <remarks></remarks>
    Private Function InicializaComprobante(ByVal poComprobante As clsComprobante, ByVal parrDetalle As ArrayList) As clsComprobante
        Dim NroComp As String
        Dim TipoComp As String
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
        poComprobante.COM_IMPORTEGRAVADO = Funciones.FormatoMoneda(gdNull)
        poComprobante.COM_IMPORTENOGRAVADO = Funciones.FormatoMoneda(gdNull)
        poComprobante.COM_IMPRESO = "S"
        poComprobante.COM_IVAFACTURADO = Funciones.FormatoMoneda(gdNull)
        TipoComp = ObtenerConfiguracion(gstrTipoComprobante)
        NroComp = FormatoNroComprobante(clsComprobanteDAO.ObtieneNroComprobante(TipoComp)) 'Hacer función
        poComprobante.COM_NROEMITIDO = NroComp
        poComprobante.COM_PTOVTA = Funciones.ObtenerPuntoVenta()
        poComprobante.COM_TOTALFACTRADO = Funciones.FormatoMoneda(Me.txtTotalAPagar.Text)
        poComprobante.COM_USUNOMBRE = Funciones.ObtieneUsuario

        poComprobante.CTC_ID = clsComprobanteDAO.ObtieneTipoComprobante(TipoComp)

        If Me.txtTotalEnTarjeta.Text = gdNull Then
            'momentaneamente va en duro.
            poComprobante.FOP_ID = 1
        End If
        'ver bien si conviene ir a buscarlo a la base.
        poComprobante.CAJ_ID = Funciones.ObtenerConfiguracion(gstrCaja)
        poComprobante.DETALLE = parrDetalle
        Return poComprobante
    End Function

    ''' <summary>
    ''' Función que recupera los tipos de comprobantes.
    ''' </summary>
    ''' <remarks></remarks>
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
            Me.cmbTipoComprobante.DataBindings.Clear()
            Me.cmbTipoComprobante.DisplayMember = "CTC_DESCRIPCION"
            Me.cmbTipoComprobante.ValueMember = "CTC_CODIGO"
            oDt = clsTipoComprobanteDAO.GetTable(oTipoComp)
            Me.cmbTipoComprobante.DataSource = oDt
            Me.cmbTipoComprobante.Text = "TICKET"
            Me.cmbTipoComprobante.Enabled = False
        Catch ex As Exception
            Manejador_Errores("LlenarTipoComprobante", ex)
        End Try
    End Sub

    Private Sub CerrarForm(ByVal pValor As String)
        If pValor = "Cancelar" Then
            If MsgBox("¿Está seguro que desea CANCELAR la Venta?", _
                    MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CANCELACION DE VENTA ::.") = MsgBoxResult.Yes Then
                LimpiarCampos()
                Me.Hide()
            End If
        Else
            Me.Hide()
        End If
        frmVentas.Inicializar()
    End Sub

    Private Sub CalcularImporte()
        Dim oiSubTotal, oiTotal, oiDescuento As String
        oiTotal = Me.txtTotalAPagar.Text
        oiDescuento = CInt(Me.txtDescuento.Text)
        oiSubTotal = Funciones.FormatoMoneda((oiDescuento * oiTotal / 100))
        Me.txtSubTotal.Text = Funciones.FormatoMoneda(oiTotal - oiSubTotal)
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
        CerrarForm("Cancelar")
    End Sub

    Private Sub frmFormasPagos_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason <> 0 Then
            CerrarForm("Cancelar")
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
        Me.txtTotalEnEfectivo.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtTotalEnTarjeta.Text = Funciones.FormatoMoneda(gdNull)
        Me.txtTotalAPagar.Enabled = False
        CalcularImporte()
        Me.txtDescuento.Focus()
    End Sub

    Private Sub txtSubTotal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubTotal.TextChanged
        CalcularImporte()
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

    Private Sub txtDescuento_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescuento.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = False
            e.KeyChar = ChrW(0)
            Me.txtDescuento.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' Se calcula el descuento sobre el Monto Total de la Venta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescuento_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescuento.LostFocus
        If Me.txtDescuento.Text = "" Then Me.txtDescuento.Text = "0"
        If Me.txtDescuento.Text <> 0 Then
            frmAutorizacion.pAccionPosterior = Definiciones.gcAccionAutorizaDescuento
            frmAutorizacion.ShowDialog()
            If Not gbAutorizado Then
                MessageBox.Show("No tiene permiso para realizar descuento.-", ".:: SIN AUTORIZACION ::.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.txtDescuento.Text = "0"
            End If
        End If
        CalcularImporte()

    End Sub

    Private Sub txtTotalEnEfectivo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalEnEfectivo.GotFocus
        Me.txtTotalEnEfectivo.SelectAll()
    End Sub

    Private Sub txtTotalEnEfectivo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalEnEfectivo.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = False
            e.KeyChar = ChrW(0)
            Me.txtTotalEnEfectivo.Text = ""
        End If
    End Sub

    Private Sub txtTotalEnEfectivo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalEnEfectivo.LostFocus
        If Me.txtTotalEnEfectivo.Text = "" Then Me.txtTotalEnEfectivo.Text = "0"
        If Me.txtTotalEnEfectivo.Text <> 0 And (CDbl(Me.txtTotalEnEfectivo.Text) >= CDbl(Me.txtSubTotal.Text)) Then
            CalcularImporte()
            Me.txtVuelto.Text = Funciones.FormatoMoneda(Me.txtTotalEnEfectivo.Text - Me.txtSubTotal.Text)
            Me.btnAceptarVenta.Focus()
        End If
    End Sub

    Private Sub txtTotalAPagar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalAPagar.GotFocus
        Me.txtTotalAPagar.SelectAll()
    End Sub

    Private Sub txtTotalAPagar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalAPagar.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = False
            e.KeyChar = ChrW(0)
            Me.txtTotalAPagar.Text = ""
        End If
    End Sub

    Private Sub txtTotalEnTarjeta_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalEnTarjeta.GotFocus
        Me.txtTotalEnTarjeta.SelectAll()
    End Sub

    Private Sub txtTotalEnTarjeta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalEnTarjeta.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = False
            e.KeyChar = ChrW(0)
            Me.txtTotalEnTarjeta.Text = ""
        End If
    End Sub

    Private Sub btnAceptarVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptarVenta.Click
        Dim oComprobante As clsComprobante
        Dim arrDetalle As New ArrayList
        ' ver de hacer un arraylist con el detalle
        Dim wiNroComprobante As Integer
        Dim strValida As String = ""
        Try
            oComprobante = New clsComprobante
            'oDetalle() = New clsComprobanteDetalle
            strValida = ValidaPago()
            If strValida = "" Then
                'Cargo datos del comprobante
                'wiNroComprobante = clsComprobanteDAO.Insertar(oComprobante)

                For i = 0 To goDt.Rows.Count - 1
                    Dim oDetalle As New clsComprobanteDetalle
                    'oComprobante.DETALLE.Item(0) = 0
                    'oComprobante.DETALLE.Item(0) = 0
                    oDetalle.COD_ID = 0
                    oDetalle.COD_IVA = 0
                    oDetalle.COD_PESABLE = IIf(goDt.Rows(i).Item("COD_PESABLE") = "No", 0, 1)
                    oDetalle.COD_PRECIOCANTIDAD = goDt.Rows(i).Item("COD_PRECIOCANTIDAD")
                    oDetalle.COD_PROCANTIDAD = goDt.Rows(i).Item("COD_PROCANTIDAD")
                    oDetalle.COD_PROCODIGO = goDt.Rows(i).Item("COD_PROCODIGO")
                    oDetalle.COD_PRONOMBRE = goDt.Rows(i).Item("COD_PRONOMBRE")
                    oDetalle.COD_PROPCIOUNITARIO = goDt.Rows(i).Item("COD_PROPCIOUNITARIO")
                    oDetalle.COM_ID = wiNroComprobante
                    'wiNroDetalle = clsComprobanteDetalleDAO.Insertar(oDetalle(i))
                    arrDetalle.Add(oDetalle)

                Next
                oComprobante = InicializaComprobante(oComprobante, arrDetalle)
                wiNroComprobante = clsComprobanteDAO.Insertar(oComprobante)
                If wiNroComprobante <> -1 Then
                    Dim oTipoComp As New clsTipoComprobante()
                    oTipoComp.CTC_ID = ObtenerConfiguracion(gstrTipoComprobante)
                    oTipoComp.CTC_LETRA = ""
                    oTipoComp.CTC_CODIGO = ""
                    oTipoComp.CTC_DESCRIPCION = ""
                    oTipoComp.CTC_SIGLA = ""
                    oTipoComp.CTC_SIGNO = ""
                    oTipoComp.CTC_UltimoNro = oComprobante.COM_NROEMITIDO
                    clsTipoComprobanteDAO.Modificar(oTipoComp)
                    LimpiarCampos()
                    frmVentas.ol_dt = Nothing
                    frmVentas.ol_DtProducto = Nothing
                    frmVentas.Inicializar()
                    clsImpresiones.ImprimirTicket(Me.EpsonFis, oComprobante)
                    MessageBox.Show("La venta se realizó de manera exitosa.-", ".:: VENTA REALIZADA ::.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CerrarForm("Cerrar")
                Else
                    MessageBox.Show("Ocurrió un problema al guardar el comprobante. La VENTA NO FUE REALIZADA.-", ".:: ERROR GRAVE ::.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    LimpiarCampos()
                    CerrarForm("Cerrar")
                End If
            Else
                MessageBox.Show(strValida, ".:: CAMPOS INCOMPLETOS ::.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            Funciones.Manejador_Errores("btnAceptarVenta_Click", ex)
            Funciones.LogError(ex, "btnAceptarVenta_Click", Funciones.ObtieneUsuario)
        Finally
            oComprobante = Nothing
        End Try
    End Sub


#End Region

End Class