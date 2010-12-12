Public Class frmBuscaProducto

    Dim o_dt As DataTable
    Dim strBuscar As String

#Region "Eventos"

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgrProductos.CellMouseClick
        Dim rta As VariantType
        Dim item As Integer
        'muestra el item seleccionado
        item = Me.dgrProductos.CurrentRow.Index
        Me.dgrProductos.FirstDisplayedScrollingRowIndex = item
        rta = MsgBox("Está seguro que desea seleccionar el producto: " & dgrProductos.Item(0, item).Value & "?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.")
        'MsgBox("Agregar el Producto: " & dgrProductos.Item(0, item).Value)
        If rta = vbYes Then
            MsgBox("Agregar el Producto: " & dgrProductos.Item(4, item).Value)
        End If
    End Sub


    Private Sub frmBuscaProducto_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        strBuscar = ""
        o_dt = Nothing
    End Sub

    Private Sub frmBuscaProducto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListarProductos()
    End Sub


    Private Sub dgrProductos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgrProductos.KeyPress
        Dim wo_Dt As DataTable
        Dim item As Integer
        Dim rta As VariantType
        Try
            wo_Dt = New DataTable

            If e.KeyChar = ChrW(Keys.Enter) Then
                'muestra el item seleccionado
                item = Me.dgrProductos.CurrentRow.Index
                Me.dgrProductos.FirstDisplayedScrollingRowIndex = item
                rta = MsgBox("Está seguro que desea seleccionar el producto: " & dgrProductos.Item(0, item).Value & "?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.")
                'MsgBox("Agregar el Producto: " & dgrProductos.Item(0, item).Value)
                If rta = vbYes Then
                    MsgBox("Agregar el Producto: " & dgrProductos.Item(4, item).Value)
                End If
            ElseIf e.KeyChar = ChrW(Keys.Escape) Or e.KeyChar = ChrW(Keys.Back) Then
                strBuscar = ""
                wo_Dt = o_dt
            Else
                strBuscar = strBuscar & e.KeyChar.ToString
                wo_Dt = o_dt.Clone()
                For Each wo_DR As DataRow In o_dt.Select("PRO_NOMBRE LIKE '" & strBuscar & "%'")
                    Dim o_dr As DataRow = wo_Dt.NewRow()

                    o_dr.Item("PRO_ID") = wo_DR.Item("PRO_ID")
                    o_dr.Item("PRO_CODIGO") = wo_DR.Item("PRO_CODIGO")
                    o_dr.Item("PRO_NOMBRE") = wo_DR.Item("PRO_NOMBRE")
                    o_dr.Item("LPR_PRECIO") = wo_DR.Item("LPR_PRECIO")
                    o_dr.Item("IVA_TASA") = wo_DR.Item("IVA_TASA")
                    o_dr.Item("IVA_NOMBRE") = wo_DR.Item("IVA_NOMBRE")
                    o_dr.Item("RUB_ID") = wo_DR.Item("RUB_ID")
                    o_dr.Item("FAM_NOMBRE") = wo_DR.Item("FAM_NOMBRE")
                    o_dr.Item("RUB_NOMBRE") = wo_DR.Item("RUB_NOMBRE")
                    o_dr.Item("PRO_ALERTA") = wo_DR.Item("PRO_ALERTA")
                    o_dr.Item("PRO_MAXIMO") = wo_DR.Item("PRO_MAXIMO")
                    o_dr.Item("PRO_MINIMO") = wo_DR.Item("PRO_MINIMO")
                    o_dr.Item("PRO_IDPADRE") = wo_DR.Item("PRO_IDPADRE")
                    o_dr.Item("PRO_IMPUESTOINTERNO") = wo_DR.Item("PRO_IMPUESTOINTERNO")
                    o_dr.Item("PRO_PRECIOCOMPRA") = wo_DR.Item("PRO_PRECIOCOMPRA")
                    o_dr.Item("PRO_PRECIOCOSTO") = wo_DR.Item("PRO_PRECIOCOSTO")
                    o_dr.Item("PRO_CODIGO_PROVEEDOR") = wo_DR.Item("PRO_CODIGO_PROVEEDOR")
                    o_dr.Item("PRO_CODIGO_BARRA") = wo_DR.Item("PRO_CODIGO_BARRA")
                    o_dr.Item("PRO_DESCRIPCION") = wo_DR.Item("PRO_DESCRIPCION")
                    o_dr.Item("PRO_NOMBREETIQUETA") = wo_DR.Item("PRO_NOMBREETIQUETA")
                    wo_Dt.Rows.Add(o_dr)

                Next

            End If
            Me.dgrProductos.DataSource = wo_Dt
            ConfigurarGrilla()

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub ListarProductos()

        Try

            o_dt = New DataTable
            o_dt = clsProductoDAO.getProducto(0, "", "", "", 0, 0)
            Me.dgrProductos.DataSource = o_dt
            ConfigurarGrilla()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, ". : E R R O R : .", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ConfigurarGrilla()
        Me.dgrProductos.Columns.Item("PRO_ID").Visible = False
        Me.dgrProductos.Columns.Item("IVA_TASA").Visible = False
        Me.dgrProductos.Columns.Item("IVA_NOMBRE").Visible = False
        Me.dgrProductos.Columns.Item("RUB_ID").Visible = False
        Me.dgrProductos.Columns.Item("FAM_NOMBRE").Visible = False
        Me.dgrProductos.Columns.Item("RUB_NOMBRE").Visible = False
        Me.dgrProductos.Columns.Item("PRO_ALERTA").Visible = False
        Me.dgrProductos.Columns.Item("PRO_MAXIMO").Visible = False
        Me.dgrProductos.Columns.Item("PRO_MINIMO").Visible = False
        Me.dgrProductos.Columns.Item("PRO_IDPADRE").Visible = False
        Me.dgrProductos.Columns.Item("PRO_IMPUESTOINTERNO").Visible = False
        Me.dgrProductos.Columns.Item("PRO_PRECIOCOMPRA").Visible = False
        Me.dgrProductos.Columns.Item("PRO_PRECIOCOSTO").Visible = False
        Me.dgrProductos.Columns.Item("PRO_CODIGO_PROVEEDOR").Visible = False
        Me.dgrProductos.Columns.Item("PRO_CODIGO_BARRA").Visible = False
        Me.dgrProductos.Columns.Item("PRO_DESCRIPCION").Visible = False
        Me.dgrProductos.Columns.Item("PRO_NOMBREETIQUETA").Visible = False

        Me.dgrProductos.Columns.Item("PRO_CODIGO").HeaderText = "Codigo"
        Me.dgrProductos.Columns.Item("PRO_NOMBRE").HeaderText = "Nombre"
        Me.dgrProductos.Columns.Item("LPR_PRECIO").HeaderText = "Precio"
        Me.dgrProductos.Columns.Item("PRO_MARCA").HeaderText = "Marca"
    End Sub
#End Region

End Class