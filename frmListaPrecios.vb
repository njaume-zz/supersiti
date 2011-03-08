Public Class frmListaPrecios

#Region "Eventos"

    Private Sub frmListaPrecios_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompletarGrilla()
    End Sub

#End Region

#Region "Métodos"
    Private Sub ConfigurarGrilla()
        Me.dgrProductos.Columns.Item("PRO_ID").Visible = False
        Me.dgrProductos.Columns.Item("PRO_CODIGO_BARRA").Visible = True
        Me.dgrProductos.Columns.Item("PRO_CODIGO_BARRA").HeaderText = "Código de Barra"
        Me.dgrProductos.Columns.Item("PRO_NOMBRE").Visible = True
        Me.dgrProductos.Columns.Item("PRO_NOMBRE").HeaderText = "Producto"
        Me.dgrProductos.Columns.Item("PRO_MARCA").Visible = True
        Me.dgrProductos.Columns.Item("PRO_MARCA").HeaderText = "Marca"
        Me.dgrProductos.Columns.Item("LPR_PRECIO").Visible = True
        Me.dgrProductos.Columns.Item("LPR_PRECIO").HeaderText = "Precio"
        Me.dgrProductos.Columns.Item("PRO_PESABLE").Visible = False
        Me.dgrProductos.Columns.Item("LPR_PRECIOXCANTIDAD").Visible = True
        Me.dgrProductos.Columns.Item("LPR_PRECIOXCANTIDAD").HeaderText = "Precio x Cant."
        Me.dgrProductos.Columns.Item("UNC_ID").Visible = False
        Me.dgrProductos.Columns.Item("UNV_ID").Visible = False
        Me.dgrProductos.Columns.Item("FAM_NOMBRE").Visible = True
        Me.dgrProductos.Columns.Item("FAM_NOMBRE").HeaderText = "Familia"
        Me.dgrProductos.Columns.Item("RUB_NOMBRE").Visible = True
        Me.dgrProductos.Columns.Item("RUB_NOMBRE").HeaderText = "Rubro"
        Me.dgrProductos.Columns.Item("LPR_ID").Visible = False
        Me.dgrProductos.Columns.Item("TPR_ID").Visible = False
        Me.dgrProductos.Columns.Item("TPR_CODIGO").Visible = False
        Me.dgrProductos.Columns.Item("PRO_CODIGO").Visible = False
        Me.dgrProductos.Columns.Item("TPR_NOMBRE").Visible = False
        Me.dgrProductos.Columns.Item("TPR_DESCRIPCION").Visible = False

    End Sub

    Private Sub CompletarGrilla()
        Dim oDt As DataTable

        Try
            oDt = New DataTable
            oDt = clsProductoDAO.GetListaPrecio()
            Me.dgrProductos.DataSource = oDt
            ConfigurarGrilla()
        Catch ex As Exception
            LogError(ex, "CompletarGrilla.ListaPrecios", ObtieneUsuario)
            Manejador_Errores("Lista de Precios", ex)
        Finally
            oDt = Nothing
        End Try
    End Sub
#End Region

End Class