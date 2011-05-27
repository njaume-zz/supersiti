Option Explicit On

Public Class frmVentas

#Region "Variables Locales"
    'Esta variable contiene los elementos necesarios para ingresar en la grilla.
    Public ol_dt As DataTable
    'Declaro esta variable como s, para poder obtener los datos del Usuario
    'en todo momento y poder validar los permisos. Además de utilizarlos en el
    'Status Strip Tools
    Public ol_dtUsr As DataTable
    'Creo la variable ol_DtProducto, sólo para no realizar 2 accesos a la base
    ' y mantener en memoria los datos necesarios para ingresar en la grilla
    Public ol_DtProducto As DataTable
#End Region

#Region "Eventos"

    Private Sub frmVentas_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CerrarAplicacion()
    End Sub

    Private Sub frmVentas_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            ConfirmarVenta()
        End If
        If e.KeyCode = (Keys.ControlKey + Keys.B) Then
            frmListaPrecios.ShowDialog()
        End If
    End Sub

    Private Sub frmVentas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(Keys.F3) Then
            Me.Visible = False
            frmBuscaProducto.dgrProductos.Focus()
            frmBuscaProducto.ShowDialog()
            Me.Visible = True
        End If
    End Sub

    Private Sub frmVentas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim wbApertura As Boolean
        Dim rta As Long
        Inicializar()
        wbApertura = frmAdministraCaja.VerificarApertura()
        If wbApertura = False Then
            rta = MessageBox.Show("Debe existir una Apertura para poder realizar Ventas." & vbNewLine & _
                            " Desea realizar una Apertura de caja?", ".:: NO EXISTEN APERTURAS ::.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If rta = vbYes Then
                frmAdministraCaja.lblOperacion.Text = "Apertura"
                frmAdministraCaja.ShowDialog()
            Else
                End
            End If
        End If
        Me.DataGridView1.ForeColor = Color.Black
    End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = False
            e.KeyChar = ChrW(0)
            Me.txtCantidad.Text = 1
        End If
    End Sub

    Private Sub txtCantidad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        Dim wdImporte As Decimal
        Dim wszImporte As String
        If e.KeyCode = Keys.Enter Then
            If Not Me.txtCantidad.Text = "" Then
                If Not ol_DtProducto Is Nothing Then
                    If Me.txtProductoBarra.Text.Length <= 2 Then
                        wszImporte = InputBox("Ingrese el importe del Producto", ".:: Ingreso de Importe ::.", 0)
                        If IsNumeric(wszImporte) Then
                            wdImporte = CDec(Replace(wszImporte, ",", "."))
                        Else
                            MessageBox.Show("Debe ingresar un valor numérico.-", ".:: VALOR INVALIDO ::.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If
                    AgregarAGrilla(ol_DtProducto, wdImporte)
                Else
                    'el 2º parámetro me indica que lo busco y agrego a la grilla
                    BuscarProducto(Me.txtProductoBarra.Text, "agregar")
                End If
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            ConfirmarVenta()
        End If
        If e.KeyCode = (Keys.ControlKey + Keys.B) Then
            frmListaPrecios.ShowDialog()
        End If
    End Sub

    Private Sub txtProductoBarra_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProductoBarra.LostFocus
        If Me.txtCantidad.Focused Then
            BuscarProducto(Me.txtProductoBarra.Text, "buscar")
        End If
    End Sub

    Private Sub txtProductoBarra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProductoBarra.KeyDown
        If e.KeyCode = Keys.F3 Then
            Me.Visible = False
            frmBuscaProducto.ShowDialog()
            Me.Visible = True
        End If
        If Not Me.txtProductoBarra.Text = "" Then
            If e.KeyCode = Keys.Enter Then
                ' el 2º parámetro me indica que lo mantengo en memoria hasta ingresar la cantidad.
                'BuscarProducto(Me.txtProductoBarra.Text, "buscar")
                Call txtProductoBarra_LostFocus(sender, New System.EventArgs)
                Me.txtCantidad.Focus()
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            ConfirmarVenta()
        End If

    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Dim oDetalle As New clsComprobanteDetalle
        'Dim wstrprecio As String

        If e.KeyCode = Keys.Delete Then
            If MsgBox("¿Está seguro de quitar este Item de la venta?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.") = MsgBoxResult.Yes Then
                ol_dt = QuitarItemDT(ol_dt, Me.DataGridView1.CurrentCell.RowIndex)
                If ol_dt.Rows.Count > 0 Then
                    Me.DataGridView1.DataSource = ol_dt
                    DefinirCabeceras()
                    RealizarCalculos(ol_dt)
                Else
                    'CrearDTItems()
                    Inicializar()
                End If
            End If
        End If

        If e.KeyCode = Keys.F5 Then
            ConfirmarVenta()
        End If

        If e.KeyCode = (Keys.ControlKey + Keys.B) Then
            frmListaPrecios.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Item del menú que solicita realizar un retiro de plata de la caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        frmAdministraCaja.lblOperacion.Text = "Retiro"
        frmAdministraCaja.lblOperacion.ForeColor = Color.DarkGreen
        frmAdministraCaja.ShowDialog()
    End Sub

    ''' <summary>
    ''' Item del menú que solicita realizar una apertura de caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        frmAdministraCaja.lblOperacion.Text = "Apertura"
        frmAdministraCaja.lblOperacion.ForeColor = Color.Blue
        frmAdministraCaja.ShowDialog()
    End Sub

    ''' <summary>
    ''' Item del menú que solicita realizar un Cierre de Caja por operador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        frmAdministraCaja.lblOperacion.Text = "Cierre X"
        frmAdministraCaja.lblOperacion.ForeColor = Color.DarkRed
        frmAdministraCaja.ShowDialog()
    End Sub

    ''' <summary>
    ''' Item del menú que solicita realizar un Cierre Z, un cierre completo de las ventas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        frmAdministraCaja.lblOperacion.Text = "Cierre Z"
        frmAdministraCaja.lblOperacion.ForeColor = Color.Red
        frmAdministraCaja.ShowDialog()
    End Sub

    Private Sub SalirDelSistemaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirDelSistemaToolStripMenuItem.Click
        CerrarAplicacion()
    End Sub

    Private Sub btnBuscarProductoXNombre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Visible = False
        frmBuscaProducto.ShowDialog()
        Me.Visible = True
    End Sub

    ''' <summary>
    ''' Método que invoca al formulario para validar si se autoriza o no una vento o decuento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        frmAutorizacion.pAccionPosterior = Definiciones.gcAccionAutorizaVenta
        frmAutorizacion.ShowDialog(Me)
    End Sub

    Private Sub TSBBuscaProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBBuscaProducto.Click
        Me.Visible = False
        frmBuscaProducto.ShowDialog()
        Me.Visible = True
    End Sub

    Private Sub TSBAceptaVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBAceptaVenta.Click
        If MsgBox("Está seguro/a desea CONFIRMAR la venta?.", _
                      MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: CONFIRMAR VENTA ::.") = MsgBoxResult.Yes Then
            CompletarFormaPago()
        End If
    End Sub

    Private Sub TSBCancelaVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBCancelaVenta.Click
        If MsgBox("Está seguro de Cancelar la Venta?.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, _
                  ".:: CANCELAR VENTA ::.") = MsgBoxResult.Ok Then
            LimpiarCampos()
            Inicializar()
        End If
    End Sub

    Private Sub TSBEliminaItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBEliminaItem.Click
        Me.DataGridView1.Focus()
        If Not Me.DataGridView1.RowCount = 0 Then
            Me.DataGridView1.SelectedCells(0).Selected = True
        Else
            Me.txtProductoBarra.Focus()
        End If
        MessageBox.Show("Se encuentra posicionado en la grilla, " & ControlChars.NewLine & _
                        "seleccione un item para eliminar y presione [Supr] or [Del].-", _
                            ".:: ELIMINACION DE ITEMS ::.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub TSBBuscarPrecio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBBuscarPrecio.Click
        frmListaPrecios.ShowDialog()
    End Sub

    Private Sub tsmBuscarProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmBuscarProducto.Click
        BuscarProducto("", "buscar")
    End Sub

    Private Sub tsmConsultaPrecio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmConsultaPrecio.Click
        frmListaPrecios.ShowDialog()
    End Sub

    Private Sub CerrarSesiónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarSesiónToolStripMenuItem.Click
        If MsgBox("Está seguro/a que desea CERRAR SESION?.", _
                MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: CERRAR SESION ::.") Then
            LimpiarCampos()
            BorrarDT(ol_dt)
            BorrarDT(ol_DtProducto)
            BorrarDT(ol_dtUsr)
            CerrarSesion()
        End If
    End Sub
#End Region

#Region "Métodos"
    Private Sub ConfirmarVenta()
        If MsgBox("Está seguro/a desea CONFIRMAR la venta?.", _
                      MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: CONFIRMAR VENTA ::.") Then
            CompletarFormaPago()
            LimpiarCampos()
            ol_dt = Nothing
            ol_DtProducto = Nothing
            CrearDTItems()
        End If
    End Sub

    ''' <summary>
    ''' Método que inicializa el formulario.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Inicializar()
        Funciones.InicializarConfiguracion()
        Me.lblFecha.Text = Format(Date.Now, "dd/MM/yyyy")
        CrearDTItems()
        IniciaStrip()
        LimpiarCampos()
    End Sub

    ''' <summary>
    ''' Método que limpia la pantalla inicializando los valores vacíos
    ''' </summary>
    ''' <remarks>Maxi Adad</remarks>
    Private Sub LimpiarCampos()
        Me.DataGridView1.DataBindings.Clear()
        Me.DataGridView1.Refresh()
        Me.txtCantidad.Text = 1
        Me.txtDescripcion.Text = ""
        Me.txtPcioProducto.Text = ""
        Me.txtPcioTotal.Text = Funciones.FormatoMoneda("0.00")
        Me.txtSubTotal.Text = Funciones.FormatoMoneda("0.00")
    End Sub

    ''' <summary>
    ''' Método que pasa los valores del formulario ventas al formulario Formas de Pago
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CompletarFormaPago()
        If Me.DataGridView1.RowCount > 0 Then
            frmFormasPagos.goDt = Me.DataGridView1.DataSource
            frmFormasPagos.txtTotalAPagar.Text = Funciones.FormatoMoneda(Me.txtPcioTotal.Text)
            frmFormasPagos.txtDescuento.Focus()
            frmFormasPagos.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Método qu erealiza los cálculos de los importes del producto por la cantidad.
    ''' Sumándolo al subtotal actual.
    ''' </summary>
    ''' <param name="poDetalle"></param>
    ''' <remarks></remarks>
    Private Sub RealizarCalculos(ByVal poDt As DataTable)
        Dim woDt As New DataTable
        Dim suma As Double = 0
        woDt = DataGridView1.DataSource
        If Not woDt Is Nothing Then
            For Each oDr As DataRow In woDt.Rows
                suma = suma + oDr.Item("COD_PRECIOCANTIDAD")
                Me.txtPcioProducto.Text = oDr.Item("COD_PRECIOCANTIDAD")
            Next

            Me.txtSubTotal.Text = Funciones.FormatoMoneda(suma)
            Me.txtPcioTotal.Text = Funciones.FormatoMoneda(suma)

        End If
        Me.txtProductoBarra.Text = ""
        Me.txtCantidad.Text = "1"
        Me.txtProductoBarra.Focus()
    End Sub

    ''' <summary>
    ''' Método que relaciona el DataTable a la grilla de venta.
    ''' </summary>
    ''' <param name="poDT"></param>
    ''' <remarks></remarks>
    Public Sub AgregarAGrilla(ByVal poDT As DataTable, Optional ByVal pdImporte As Decimal = 0)
        Dim oDetalle As New clsComprobanteDetalle
        Dim woDt As DataTable
        Dim wstrPrecio As String
        Dim wiCantidad As Integer

        Try
            woDt = New DataTable

            If Me.txtCantidad.Text > 0 Then

                wiCantidad = CInt(Me.txtCantidad.Text)

                poDT.Select("PRO_CODIGO_BARRA = '" & Me.txtProductoBarra.Text.Trim & "'")
                If pdImporte = 0 Then
                    wstrPrecio = Funciones.FormatoMoneda((IIf(IsDBNull(poDT.Rows(0).Item("LPR_PRECIO")), poDT.Rows(0).Item("PRO_PRECIOCOSTO"), poDT.Rows(0).Item("LPR_PRECIO")) * wiCantidad))
                    oDetalle.COD_PROPCIOUNITARIO = Funciones.FormatoMoneda(poDT.Rows(0).Item("LPR_PRECIO"))
                Else
                    wstrPrecio = Funciones.FormatoMoneda(pdImporte * wiCantidad)
                    oDetalle.COD_PROPCIOUNITARIO = Funciones.FormatoMoneda(pdImporte)
                End If
                oDetalle.COD_ID = poDT.Rows(0).Item("PRO_ID")
                oDetalle.COD_PRONOMBRE = poDT.Rows(0).Item("PRO_NOMBRE")
                oDetalle.COD_PROCODIGO = poDT.Rows(0).Item("PRO_CODIGO_BARRA")
                'oDetalle.COD_PROPCIOUNITARIO = Funciones.FormatoMoneda(poDT.Rows(0).Item("LPR_PRECIO"))
                oDetalle.COD_PROCANTIDAD = wiCantidad
                oDetalle.COD_PRECIOCANTIDAD = Funciones.FormatoMoneda(wstrPrecio)
                oDetalle.COD_PESABLE = poDT.Rows(0).Item("PRO_PESABLE")

                ol_dt = AgregarItemDT(oDetalle)

                Me.lblProductoNombre.Text = Me.txtDescripcion.Text
                Me.txtDescripcion.Text = ""

                Me.DataGridView1.DataSource = ol_dt
                poDT.Select("")
                RealizarCalculos(poDT)
            End If
            'End If

        Catch ex As Exception

        Finally
            oDetalle = Nothing
            wiCantidad = 0
            wstrPrecio = ""
            woDt = Nothing
        End Try
    End Sub

    Private Sub BuscarProducto(ByVal pstrProducto As String, ByVal accion As String)
        Dim oDt As DataTable

        Try

            oDt = New DataTable
            oDt = clsProductoDAO.getProducto(0, "", "", "", 0, 0, 0, pstrProducto, 0)

            If Not oDt Is Nothing Then
                If accion = "buscar" Then
                    If oDt.Rows.Count = 0 Then
                        frmBuscaProducto.dgrProductos.Focus()
                        Me.Visible = False
                        frmBuscaProducto.ShowDialog()
                        Me.Visible = True
                    ElseIf oDt.Rows.Count = 1 Then
                        ol_DtProducto = oDt
                        txtDescripcion.Text = oDt.Rows(0).Item("PRO_NOMBRE")
                        Me.txtCantidad.Focus()
                    ElseIf oDt.Rows.Count > 1 Then
                        frmBuscaProducto.Lista(oDt)
                        Me.Visible = False
                        frmBuscaProducto.ShowDialog()
                        Me.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, ".:: ERROR ::.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDt = Nothing
        End Try
    End Sub

    Public Sub IniciaStrip()
        SSTInformaUsuario.Items("TSSUsuario").Text = ol_dtUsr.Rows(0).Item("USU_NOMBRE").ToString
        SSTInformaUsuario.Items("TSSIdUsuario").Text = ol_dtUsr.Rows(0).Item("USU_ID").ToString
        SSTInformaUsuario.Items("TSSFecha").Text = FormatDateTime(Now, DateFormat.ShortDate)
        SSTInformaUsuario.Items("TSSPtoVta").Text = "0001"
        SSTInformaUsuario.Items("TSSPC").Text = Funciones.NombrePC
    End Sub

    Private Sub CerrarAplicacion()
        If MsgBox("¿Está seguro que desea cerrar la Aplicación?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, _
                    ".:: CIERRE DE APLICACIÓN ::.") = MsgBoxResult.Yes Then
            BorrarDT(ol_dt)
            End
        Else : End If
    End Sub

    Private Sub CerrarSesion()
        Me.Visible = False
        Me.Hide()
        frmIngreso.ShowDialog()
        'Me.Visible = True
    End Sub
#End Region

#Region "Creación de métodos auxiliares"

    Private Sub DefinirCabeceras()
        Me.DataGridView1.Columns.Item("COD_ID").HeaderText = "Nro. Producto"
        Me.DataGridView1.Columns.Item("COD_ID").Visible = False
        Me.DataGridView1.Columns.Item("COD_PRONOMBRE").HeaderText = "Producto"
        Me.DataGridView1.Columns.Item("COD_PROCODIGO").HeaderText = "Código Producto"
        Me.DataGridView1.Columns.Item("COD_PROPCIOUNITARIO").HeaderText = "Pcio. Unitario"
        Me.DataGridView1.Columns.Item("COD_PROCANTIDAD").HeaderText = "Cantidad"
        Me.DataGridView1.Columns.Item("COD_PRECIOCANTIDAD").HeaderText = "Pcio. Cantidad"
        Me.DataGridView1.Columns.Item("COD_PESABLE").HeaderText = "Pesable"

        Me.DataGridView1.Columns.Item("COD_ID").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridView1.Columns.Item("COD_PRONOMBRE").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridView1.Columns.Item("COD_PROCODIGO").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridView1.Columns.Item("COD_PROPCIOUNITARIO").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridView1.Columns.Item("COD_PROCANTIDAD").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridView1.Columns.Item("COD_PRECIOCANTIDAD").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridView1.Columns.Item("COD_PESABLE").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader

        Me.DataGridView1.Refresh()
    End Sub

    ''' <summary>
    ''' Método que define las columnas necesarias para un datatable
    ''' </summary>
    ''' <param name="pszNombre">Nombre del Campo</param>
    ''' <returns>DataColumn</returns>
    ''' <remarks></remarks>
    Private Function DefinirColumna(ByVal pszNombre As String) As DataColumn
        Dim wDcol As New DataColumn

        wDcol.DataType = System.Type.GetType("System.String")
        wDcol.ColumnName = pszNombre
        wDcol.DefaultValue = pszNombre

        Return wDcol
    End Function

    ''' <summary>
    ''' Inicializa el DataTable que será utilizado para el formulario Ventas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CrearDTItems()
        Dim oDet As New clsComprobanteDetalle
        ol_dt = New DataTable

        With ol_dt.Columns
            .Add(DefinirColumna("COD_ID"))
            .Add(DefinirColumna("COD_PRONOMBRE"))
            .Add(DefinirColumna("COD_PROCODIGO"))
            .Add(DefinirColumna("COD_PROPCIOUNITARIO"))
            .Add(DefinirColumna("COD_PROCANTIDAD"))
            .Add(DefinirColumna("COD_PRECIOCANTIDAD"))
            .Add(DefinirColumna("COD_PESABLE"))
        End With
        Me.DataGridView1.DataSource = ol_dt
        Me.DataGridView1.Refresh()
        DefinirCabeceras()
    End Sub

    ''' <summary>
    ''' Método que agrega un ítem al DataTable que será utilizado para Ventas
    ''' </summary>
    ''' <param name="campos">Clase del Detalle de Comprobantes</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function AgregarItemDT(ByVal campos As clsComprobanteDetalle) As DataTable
        Try
            Dim obRepetido As Boolean = False
            Dim dr As DataRow ' = dt.NewRow()

            'Verifico la cantidad y la actualizo
            For Each odrVerifica As DataRow In ol_dt.Select("COD_PROCODIGO = " & campos.COD_PROCODIGO & "")
                'If Not odrVerifica.Item("COD_ID") Is Nothing Then
                obRepetido = True
                odrVerifica.Item("COD_PROCANTIDAD") = odrVerifica.Item("COD_PROCANTIDAD") + campos.COD_PROCANTIDAD
                'wiCantidad = odrVerifica.Item("COD_PROCANTIDAD")
                odrVerifica.Item("COD_PRECIOCANTIDAD") = Funciones.FormatoMoneda(campos.COD_PRECIOCANTIDAD * odrVerifica.Item("COD_PROCANTIDAD"))
                ol_dt.AcceptChanges()
                Exit For
                'End If
            Next
            If obRepetido = False Then
                dr = ol_dt.NewRow()
                dr.Item("COD_ID") = campos.COD_ID
                dr.Item("COD_PRONOMBRE") = campos.COD_PRONOMBRE
                dr.Item("COD_PROCODIGO") = campos.COD_PROCODIGO
                dr.Item("COD_PROPCIOUNITARIO") = Funciones.FormatoMoneda(campos.COD_PROPCIOUNITARIO)
                dr.Item("COD_PROCANTIDAD") = CInt(campos.COD_PROCANTIDAD)
                dr.Item("COD_PRECIOCANTIDAD") = Funciones.FormatoMoneda(campos.COD_PRECIOCANTIDAD)
                dr.Item("COD_PESABLE") = IIf(campos.COD_PESABLE = 0, "No", "Si")

                ol_dt.Rows.Add(dr)
            End If

            'End If

            Return ol_dt

        Catch ex As Exception
            'Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Se quita el elemento indicado del DataTable
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <param name="indice">Indice a eliminar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function QuitarItemDT(ByVal dt As DataTable, ByVal indice As Integer) As DataTable

        dt.Rows(indice).Delete()
        dt.AcceptChanges()

        Return dt
    End Function

    ''' <summary>
    ''' Elimina el DataTable creado para usarse en este formulario.
    ''' </summary>
    ''' <param name="dt">Se pasa el DataTable declarado de manera global</param>
    ''' <remarks></remarks>
    Private Sub BorrarDT(ByRef dt As DataTable)
        'dt.Clear()
        dt = Nothing
    End Sub
#End Region

End Class
