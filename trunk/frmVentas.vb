Public Class frmVentas

#Region "Variables Locales"
    Dim ol_dt As DataTable
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
        IniciaStrip()
    End Sub

    Private Sub txtProductoBarra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProductoBarra.KeyDown
        If e.KeyCode = Keys.F2 Then
            frmBuscaProducto.Show()
        End If
        If e.KeyCode = Keys.Enter Then
            Me.txtCantidad.Focus()
        End If
    End Sub

    Private Sub txtCantidad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            MsgBox("Agregar Items")
        End If
    End Sub

    Private Sub txtProductoBarra_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProductoBarra.KeyPress
        If e.KeyChar = ChrW(13) Then
            frmBuscaProducto.Show()
        End If

    End Sub


    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            MsgBox("estás por borrar items")
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
        ol_dt.Columns.Add("FAC_id", GetType(Integer))
        ol_dt.Columns.Add("FAC_Nombre", GetType(String))
        ol_dt.Columns.Add("FAC_Codigo", GetType(Integer))
        ol_dt.Columns.Add("FAC_PcioUnitario", GetType(Double))
        ol_dt.Columns.Add("FAC_Cantidad", GetType(Integer))
        ol_dt.Columns.Add("FAC_PcioTotal", GetType(Double))
        ol_dt.Columns.Add("FAC_Pesable", GetType(Integer))

    End Sub

    Private Function AgregarItemDT(ByRef dt As DataTable, ByVal campos As clsDetalleComprobante) As DataTable
        Dim dr As DataRow

        dr.Item("FAC_id") = campos.ID
        dr.Item("FAC_Nombre") = campos.Nombre
        dr.Item("FAC_Codigo") = campos.Codigo
        dr.Item("FAC_PcioUnitario") = campos.PcioUnitario
        dr.Item("FAC_Cantidad") = campos.Cantidad
        dr.Item("FAC_PcioTotal") = campos.PcioTotal
        dr.Item("FAC_Pesable") = campos.Pesable
        dr.AcceptChanges()

        dt.Rows.Add(dr)
        Return dt
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
