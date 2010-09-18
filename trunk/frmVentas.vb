Public Class frmVentas

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

    Public Sub IniciaStrip()
        SSTInformaUsuario.Items("TSSUsuario").Text = "Maxi"
        SSTInformaUsuario.Items("TSSFecha").Text = FormatDateTime(Now, DateFormat.ShortDate)
        SSTInformaUsuario.Items("TSSPtoVta").Text = "0001"
        SSTInformaUsuario.Items("TSSPC").Text = "Maxi"
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

#Region "Creación de métodos auxiliares"

    Private Sub CrearDTItems()
        Dim dt As New DataTable

        dt.Columns.Add("FAC_id", GetType(Integer))
        dt.Columns.Add("FAC_Nombre", GetType(String))
        dt.Columns.Add("FAC_Codigo", GetType(Integer))
        dt.Columns.Add("FAC_PcioUnitario", GetType(Double))
        dt.Columns.Add("FAC_Cantidad", GetType(Integer))
        dt.Columns.Add("FAC_PcioTotal", GetType(Double))
        dt.Columns.Add("FAC_Pesable", GetType(Integer))

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
        dt.Clear()
        dt = Nothing
    End Sub
#End Region


End Class
