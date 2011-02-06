Option Explicit On

Public Class frmVentas

#Region "Variables Locales"
    'Esta variable contiene los elementos necesarios para ingresar en la grilla.
    Dim ol_dt As DataTable
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
        If e.KeyCode = Keys.F2 Then
            frmBuscaProducto.Show()
        End If
    End Sub

    Private Sub frmVentas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(Keys.F2) Then
            frmBuscaProducto.ShowDialog()
        End If
    End Sub

    Private Sub frmVentas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Funciones.InicializarConfiguracion()
        Me.lblFecha.Text = Format(Date.Now, "dd/MM/yyyy")
        CrearDTItems()
        IniciaStrip()
    End Sub

    'Private Sub txtProductoBarra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProductoBarra.KeyDown
    '    'If Not Me.txtProductoBarra.Text = "" Then
    '    Try

    '        If e.KeyCode = Keys.F2 Then
    '            frmBuscaProducto.ShowDialog()
    '        End If
    '        If Not Me.txtProductoBarra.Text = "" Then
    '            If e.KeyCode = Keys.Enter Then
    '                'Buscar en la base y completar una Clase detalle
    '                Me.txtCantidad.Focus()
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub txtCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        'If Not Funciones.ValidaNumerico(e.KeyChar) Then
        If Not IsNumeric(e.KeyChar) Then
            Me.txtCantidad.Text = ""
            e.Handled = False
        End If
    End Sub

    Private Sub txtCantidad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not Me.txtCantidad.Text = "" Then
                If Not ol_DtProducto Is Nothing Then
                    AgregarAGrilla(ol_DtProducto)
                Else
                    'el 2º parámetro me indica que lo busco y agrego a la grilla
                    BuscarProducto(Me.txtProductoBarra.Text, "agregar")
                End If
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            If MsgBox("Está seguro/a desea CONFIRMAR la venta?.", _
                      MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: CONFIRMAR VENTA ::.") Then
                CompletarFormaPago()
            End If
        End If
    End Sub

    Private Sub txtProductoBarra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProductoBarra.KeyDown
        If e.KeyCode = Keys.F3 Then
            frmBuscaProducto.Show()
        End If
        If Not Me.txtProductoBarra.Text = "" Then
            If e.KeyCode = Keys.Enter Then
                ' el 2º parámetro me indica que lo mantengo en memoria hasta ingresar la cantidad.
                BuscarProducto(Me.txtProductoBarra.Text, "buscar")

                Me.txtCantidad.Focus()
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            If MsgBox("Está seguro/a desea CONFIRMAR la venta?.", _
                      MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: CONFIRMAR VENTA ::.") = MsgBoxResult.Yes Then
                CompletarFormaPago()
            End If
        End If
    End Sub

    Private Sub txtProductoBarra_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProductoBarra.KeyPress
        If e.KeyChar = ChrW(Keys.F2) Then
            frmBuscaProducto.ShowDialog()
        End If

    End Sub

    Private Sub txtProductoBarra_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProductoBarra.LostFocus
        If Me.txtProductoBarra.Text <> "" Then
            BuscarProducto(Me.txtProductoBarra.Text, "buscar")
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            If MsgBox("¿Está seguro de quitar este Item de la venta?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.") = MsgBoxResult.Yes Then
                ol_DtProducto = QuitarItemDT(ol_DtProducto, Me.DataGridView1.CurrentCell.RowIndex)
                Me.DataGridView1.DataSource = ol_DtProducto
            End If
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
        frmBuscaProducto.ShowDialog()
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
        frmBuscaProducto.ShowDialog()
    End Sub

    Private Sub TSBAceptaVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBAceptaVenta.Click
        MsgBox("Aceptar Venta")
    End Sub

    Private Sub TSBCancelaVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBCancelaVenta.Click
        MsgBox("Cancelar Venta")
    End Sub

    Private Sub TSBEliminaItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBEliminaItem.Click
        MsgBox("Ubicarme en la grilla y seleccionar un item")
    End Sub
#End Region

#Region "Métodos"
    Public Sub CompletarFormaPago()
        If Me.DataGridView1.RowCount > 0 Then
            frmFormasPagos.txtTotalAPagar.Text = Funciones.FormatoMoneda(Me.txtPcioTotal.Text)
            frmFormasPagos.ShowDialog()
        End If
    End Sub

    Private Sub RealizarCalculos(ByVal poDetalle As clsComprobanteDetalle)
        Me.txtPcioProducto.Text = Funciones.FormatoMoneda(CStr(poDetalle.COD_PROPCIOUNITARIO))
        Me.txtSubTotal.Text = Funciones.FormatoMoneda(Me.txtSubTotal.Text + (poDetalle.COD_PROPCIOUNITARIO * poDetalle.COD_PROCANTIDAD))
        Me.txtPcioTotal.Text = Funciones.FormatoMoneda(Math.Round(Me.txtPcioTotal.Text + (poDetalle.COD_PROPCIOUNITARIO * poDetalle.COD_PROCANTIDAD), 2))
    End Sub

    ''' <summary>
    ''' Método que relaciona el DataTable a la grilla de venta.
    ''' </summary>
    ''' <param name="poDT"></param>
    ''' <remarks></remarks>
    Public Sub AgregarAGrilla(ByVal poDT As DataTable)
        Dim oDetalle As New clsComprobanteDetalle
        Dim wstrPrecio As String
        If Me.txtCantidad.Text > 0 Then
            If poDT.Rows.Count > 0 Then
                wstrPrecio = Funciones.FormatoMoneda(IIf(IsDBNull(poDT.Rows(0).Item("LPR_PRECIO")), poDT.Rows(0).Item("PRO_PRECIOCOSTO"), poDT.Rows(0).Item("LPR_PRECIO")))

                oDetalle.COD_ID = poDT.Rows(0).Item("PRO_ID")
                oDetalle.COD_PRONOMBRE = poDT.Rows(0).Item("PRO_NOMBRE")
                oDetalle.COD_PROCODIGO = poDT.Rows(0).Item("PRO_CODIGO")
                oDetalle.COD_PROPCIOUNITARIO = Funciones.FormatoMoneda(wstrPrecio)
                oDetalle.COD_PROCANTIDAD = CInt("0" & Me.txtCantidad.Text)
                oDetalle.COD_PRECIOCANTIDAD = Funciones.FormatoMoneda(Math.Round(CDbl(CInt(Me.txtCantidad.Text) * CDbl(oDetalle.COD_PROPCIOUNITARIO)), 2))
                oDetalle.COD_PESABLE = poDT.Rows(0).Item("PRO_PESABLE")
                ol_dt = AgregarItemDT(oDetalle)


                Me.DataGridView1.DataSource = ol_dt
                'Me.DataGridView1.Columns.Item("PRO_ID").Visible = False
                Me.txtProductoBarra.Text = ""
                Me.txtCantidad.Text = ""
                Me.txtProductoBarra.Focus()
                RealizarCalculos(oDetalle)
            End If
        End If
    End Sub


    Private Sub BuscarProducto(ByVal pstrProducto As String, ByVal accion As String)
        Dim oDt As DataTable

        Try
            oDt = New DataTable
            oDt = clsProductoDAO.getProducto(0, "", "", "", 0, 0, 0, pstrProducto, 0)

            If Not oDt Is Nothing Then
                If accion = "buscar" Then
                    If oDt.Rows.Count = 0 Then
                        frmBuscaProducto.Show()
                    ElseIf oDt.Rows.Count = 1 Then
                        ol_DtProducto = oDt
                        txtDescripcion.Text = oDt.Rows(0).Item("PRO_NOMBRE")
                        Me.txtCantidad.Focus()
                    ElseIf oDt.Rows.Count > 1 Then
                        frmBuscaProducto.Lista(oDt)
                        frmBuscaProducto.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
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
        If MsgBox("¿Está seguro que desea cerrar la Aplicación?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CIERRE DE APLICACIÓN ::.") = MsgBoxResult.Yes Then
            BorrarDT(ol_dt)
            End
        End If
    End Sub

    Public Sub CargarMenu(ByVal po_dt As DataTable)
        'Dim o_item As MenuItem
        'Dim o_men As MenuStrip

        'For Each dr As DataRow In po_dt.Rows
        '    o_men = New MenuStrip

        'Next

    End Sub
#End Region

#Region "Creación de métodos auxiliares"

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

            For Each odrVerifica As DataRow In ol_dt.Select("COD_ID = " & campos.COD_ID & "")
                If Not odrVerifica.Item("COD_ID") Is Nothing Then
                    obRepetido = True
                    odrVerifica.Item("COD_PROCANTIDAD") = odrVerifica.Item("COD_PROCANTIDAD") + campos.COD_PROCANTIDAD
                    odrVerifica.Item("COD_PRECIOCANTIDAD") = Funciones.FormatoMoneda(campos.COD_PRECIOCANTIDAD)
                    Exit For
                End If
            Next
            If obRepetido = False Then
                dr = ol_dt.NewRow()
                dr.Item("COD_ID") = campos.COD_ID
                dr.Item("COD_PRONOMBRE") = campos.COD_PRONOMBRE
                dr.Item("COD_PROCODIGO") = campos.COD_PROCODIGO
                dr.Item("COD_PROPCIOUNITARIO") = Funciones.FormatoMoneda(campos.COD_PROPCIOUNITARIO)
                dr.Item("COD_PROCANTIDAD") = campos.COD_PROCANTIDAD
                dr.Item("COD_PRECIOCANTIDAD") = Funciones.FormatoMoneda(campos.COD_PRECIOCANTIDAD)
                dr.Item("COD_PESABLE") = campos.COD_PESABLE

                ol_dt.Rows.Add(dr)
            End If

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
