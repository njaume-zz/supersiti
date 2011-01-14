Public Class frmBuscaProducto

    Dim o_dt As DataTable
    Dim strBuscar As String

#Region "Eventos"

    Private Sub DataGridView1_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgrProductos.CellMouseClick
        Dim rta As VariantType
        Dim item As Integer
        'muestra el item seleccionado
        Try

            item = Me.dgrProductos.CurrentRow.Index
            Me.dgrProductos.FirstDisplayedScrollingRowIndex = item
            rta = MsgBox("Está seguro que desea seleccionar el producto: " & dgrProductos.Item(0, item).Value & "?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.")

            If rta = vbYes Then
                MsgBox("Agregar el Producto: " & dgrProductos.Item(4, item).Value)
            End If

        Catch ex As Exception
            Funciones.LogError(ex, "DataGridView_CellMouseClick", Funciones.ObtieneUsuario)
        End Try
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
            wo_Dt = o_dt.Clone()
            If e.KeyChar = ChrW(Keys.Enter) Then
                'muestra el item seleccionado
                item = Me.dgrProductos.CurrentRow.Index
                Me.dgrProductos.FirstDisplayedScrollingRowIndex = item
                rta = MsgBox("Está seguro que desea seleccionar el producto: " & dgrProductos.Item(4, item).Value & "?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.")
                'MsgBox("Agregar el Producto: " & dgrProductos.Item(0, item).Value)
                If rta = vbYes Then
                    MsgBox("Agregar el Producto: " & dgrProductos.Item(4, item).Value)
                    Dim oDr As DataRow = wo_Dt.NewRow

                    'Agrego los datos al datatable
                    oDr.Item("PRO_ID") = dgrProductos.Item(0, item).Value
                    oDr.Item("PRO_NOMBRE") = dgrProductos.Item(4, item).Value
                    oDr.Item("PRO_CODIGO") = dgrProductos.Item(1, item).Value
                    oDr.Item("LPR_PRECIO") = dgrProductos.Item(8, item).Value
                    oDr.Item("PRO_PESABLE") = dgrProductos.Item(15, item).Value
                    oDr.Item("PRO_CODIGO_BARRA") = dgrProductos.Item(2, item).Value
                    oDr.Item("IVA_TASA") = 0.0
                    oDr.Item("IVA_NOMBRE") = ""
                    oDr.Item("RUB_ID") = 0
                    oDr.Item("FAM_NOMBRE") = ""
                    oDr.Item("RUB_NOMBRE") = ""
                    oDr.Item("PRO_ALERTA") = 0
                    oDr.Item("PRO_MAXIMO") = 0
                    oDr.Item("PRO_MINIMO") = 0
                    oDr.Item("PRO_IDPADRE") = 0
                    oDr.Item("PRO_IMPUESTOINTERNO") = 0
                    oDr.Item("PRO_PRECIOCOMPRA") = 0.0
                    oDr.Item("PRO_PRECIOCOSTO") = 0.0
                    oDr.Item("PRO_CODIGO_PROVEEDOR") = 0
                    oDr.Item("PRO_DESCRIPCION") = ""
                    oDr.Item("PRO_NOMBREETIQUETA") = ""
                    wo_Dt.Rows.Add(oDr)
                    'lo dejo en memoria del actual datatable de Ventas
                    frmVentas.ol_DtProducto = wo_Dt
                    'Agrego los datos al formulario para que ingrese cantidad
                    With frmVentas
                        .txtProductoBarra.Text = oDr.Item("PRO_CODIGO_BARRA")
                        .txtCantidad.Focus()
                    End With
                    oDr = Nothing
                    'Debo pasar los valores a un datatable y llevarlo al formulario de ventas
                End If
            ElseIf e.KeyChar = ChrW(Keys.Escape) Or e.KeyChar = ChrW(Keys.Back) Then
                strBuscar = ""
                wo_Dt = o_dt
            Else
                strBuscar = strBuscar & e.KeyChar.ToString

                Dim o_dr As DataRow

                For Each wo_DR As DataRow In o_dt.Select("PRO_NOMBRE LIKE '" & strBuscar & "%'")
                    o_dr = wo_Dt.NewRow()

                    o_dr.Item("PRO_ID") = wo_DR.Item("PRO_ID")
                    o_dr.Item("PRO_CODIGO") = wo_DR.Item("PRO_CODIGO") & ""
                    o_dr.Item("PRO_NOMBRE") = wo_DR.Item("PRO_NOMBRE") & ""
                    o_dr.Item("LPR_PRECIO") = wo_DR.Item("LPR_PRECIO")
                    o_dr.Item("IVA_TASA") = wo_DR.Item("IVA_TASA")
                    o_dr.Item("IVA_NOMBRE") = wo_DR.Item("IVA_NOMBRE")
                    o_dr.Item("RUB_ID") = wo_DR.Item("RUB_ID")
                    o_dr.Item("FAM_NOMBRE") = wo_DR.Item("FAM_NOMBRE")
                    o_dr.Item("RUB_NOMBRE") = wo_DR.Item("RUB_NOMBRE")
                    o_dr.Item("PRO_ALERTA") = wo_DR.Item("PRO_ALERTA")
                    o_dr.Item("PRO_PESABLE") = wo_DR.Item("PRO_PESABLE")
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
                    o_dr = Nothing
                Next

            End If
            Me.dgrProductos.DataSource = wo_Dt
            ConfigurarGrilla()
            Me.Close()
        Catch ex As Exception
            Funciones.LogError(ex, "dgrProductos_KeyPress", Funciones.ObtieneUsuario)
        Finally
            wo_Dt = Nothing

        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que enlaza la Grilla con el DataTable pasado por parámetro
    ''' </summary>
    ''' <param name="pDt">DataTable a enlazar con la grilla</param>
    ''' <remarks></remarks>
    Public Sub Lista(ByVal pDt As DataTable)
        Me.dgrProductos.DataSource = pDt
        ConfigurarGrilla()
    End Sub

    ''' <summary>
    ''' Método que recupera de la base de datos los productos según un filtro.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ListarProductos()
        Try

            o_dt = New DataTable
            o_dt = clsProductoDAO.getProducto(0, "", "", "", 0, 0, 0, "", 0)
            If Not o_dt Is Nothing Then
                Me.dgrProductos.DataSource = o_dt
                ConfigurarGrilla()
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, ". : E R R O R : .", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            Funciones.LogError(ex, "ListarProductos", Funciones.ObtieneUsuario)
        End Try

    End Sub

    ''' <summary>
    ''' Método que configura la visualización de la grilla y asigna la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarGrilla()
        Me.dgrProductos.Columns.Item("PRO_ID").Visible = False
        Me.dgrProductos.Columns.Item("IVA_TASA").Visible = False
        Me.dgrProductos.Columns.Item("IVA_NOMBRE").Visible = False
        Me.dgrProductos.Columns.Item("RUB_ID").Visible = False
        Me.dgrProductos.Columns.Item("FAM_NOMBRE").Visible = False
        Me.dgrProductos.Columns.Item("RUB_NOMBRE").Visible = False
        Me.dgrProductos.Columns.Item("PRO_ALERTA").Visible = False
        Me.dgrProductos.Columns.Item("PRO_PESABLE").Visible = True
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