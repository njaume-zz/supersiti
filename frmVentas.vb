Option Explicit On

Public Class frmVentas

#Region "Variables Locales"
    Dim ol_dt As DataTable
    'Declaro esta variable como s, para poder obtener los datos del Usuario
    'en todo momento y poder validar los permisos. Además de utilizarlos en el
    'Status Strip Tools
    Public ol_dtUsr As DataTable
#End Region

#Region "Eventos"

    Private Sub frmVentas_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("Está seguro de cerrar la Aplicación?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, ". : : ATENCION : : .") = MsgBoxResult.Yes Then
            CerrarAplicacion()
        End If
    End Sub

    Private Sub frmVentas_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Then
            frmBuscaProducto.Show()
        End If
    End Sub

    Private Sub frmVentas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = ChrW(13) Then
            frmBuscaProducto.Show()
        End If
    End Sub

    Private Sub frmVentas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblFecha.Text = Format(Date.Now, "dd/MM/yyyy")
        CrearDTItems()
        IniciaStrip()
    End Sub

    Private Sub txtProductoBarra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProductoBarra.KeyDown
        If e.KeyCode = Keys.F2 Then
            frmBuscaProducto.Show()
        End If
        If e.KeyCode = Keys.Enter Then
            'Buscar en la base y completar una Clase detalle

            Me.txtCantidad.Focus()
        End If
    End Sub

    Private Sub txtCantidad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not Me.txtCantidad.Text = "" Then

                Dim o_Detalle As New clsDetalleComprobante
                o_Detalle.Nombre = "Producto"
                o_Detalle.ID = Me.DataGridView1.RowCount + 1
                o_Detalle.Codigo = 123333 'txtProductoBarra.Text
                o_Detalle.Cantidad = txtCantidad.Text
                o_Detalle.PcioTotal = 0.0
                o_Detalle.PcioUnitario = 0.0
                o_Detalle.Pesable = 1
                'AgregarItemDT(ol_dt, o_Detalle)
                'Dim ds As New DataSet
                'ds.Tables.Add(AgregarItemDT(ol_dt, o_Detalle))
                Me.DataGridView1.DataSource = AgregarItemDT(ol_dt, o_Detalle) 'ds

                MsgBox("Agregar Items")

            End If
        End If
    End Sub

    Private Sub txtProductoBarra_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProductoBarra.KeyPress
        If e.KeyChar = ChrW(Keys.F2) Then
            frmBuscaProducto.Show()
        End If

    End Sub


    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            If MsgBox("Está seguro de Quitar este Item de la venta?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, ". : : ADVERTENCIA : : .") = MsgBoxResult.Yes Then
                QuitarItemDT(ol_dt, Me.DataGridView1.CurrentCell.RowIndex)
            End If
        End If
    End Sub

#End Region

#Region "Métodos"

    Public Sub IniciaStrip()
        SSTInformaUsuario.Items("TSSUsuario").Text = ol_dtUsr.Rows(0).Item("USU_NOMBRE").ToString
        SSTInformaUsuario.Items("TSSFecha").Text = FormatDateTime(Now, DateFormat.ShortDate)
        SSTInformaUsuario.Items("TSSPtoVta").Text = "0001"
        SSTInformaUsuario.Items("TSSPC").Text = Funciones.NombrePC
    End Sub

    Private Sub CerrarAplicacion()
        BorrarDT(ol_dt)
        End
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

    Private Sub CrearDTItems()
        ol_dt = New DataTable
        ol_dt.Columns.Add("ID", GetType(Integer))
        ol_dt.Columns.Add("Nombre", GetType(String))
        ol_dt.Columns.Add("Codigo", GetType(Integer))
        ol_dt.Columns.Add("PcioUnitario", GetType(Double))
        ol_dt.Columns.Add("Cantidad", GetType(Integer))
        ol_dt.Columns.Add("PcioTotal", GetType(Double))
        ol_dt.Columns.Add("Pesable", GetType(Char))

    End Sub

    Private Function AgregarItemDT(ByRef dt As DataTable, ByVal campos As clsDetalleComprobante) As DataTable
        Try

            Dim dr As DataRow = dt.NewRow()

            'dr = dt.NewRow()
            dr.Item("ID") = campos.ID
            dr.Item("Nombre") = campos.Nombre
            dr.Item("Codigo") = campos.Codigo
            dr.Item("PcioUnitario") = campos.PcioUnitario
            dr.Item("Cantidad") = campos.Cantidad
            dr.Item("PcioTotal") = campos.PcioTotal
            dr.Item("Pesable") = IIf(campos.Pesable = 1, "S", "N")
            'dr.AcceptChanges()

            dt.Rows.Add(dr)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function QuitarItemDT(ByVal dt As DataTable, ByVal indice As Integer) As DataTable

        dt.Rows(indice).Delete()
        dt.AcceptChanges()

        Return dt
    End Function

    Private Sub BorrarDT(ByRef dt As DataTable)
        'dt.Clear()
        dt = Nothing
    End Sub
#End Region


End Class
