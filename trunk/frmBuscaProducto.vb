Public Class frmBuscaProducto

    Dim o_dt As DataTable
    Dim strBuscar As String

#Region "Eventos"


    Private Sub dgrProductos_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgrProductos.CellMouseDoubleClick
        Dim item As Integer
        Try

            item = Me.dgrProductos.CurrentRow.Index
            Me.dgrProductos.FirstDisplayedScrollingRowIndex = item

            If MsgBox("Está seguro que desea seleccionar el producto: " & _
                        dgrProductos.Item(0, item).Value & "?", _
                        MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.") = vbYes Then
                MsgBox("Se agregó el Producto: " & dgrProductos.Item(4, item).Value)
                AsignaSeleccion(o_dt, dgrProductos.Item(0, item).Value)
                Me.Close()
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

        Try
            wo_Dt = New DataTable
            wo_Dt = o_dt.Clone()
            Select Case e.KeyChar
                Case ChrW(Keys.Enter) ' se confirma la selección
                    'muestra el item seleccionado
                    item = Me.dgrProductos.CurrentRow.Index
                    'Me.dgrProductos.FirstDisplayedScrollingRowIndex = item

                    If MsgBox("Está seguro que desea seleccionar el producto: " & _
                            dgrProductos.Item(4, item).Value & "?", _
                            MsgBoxStyle.Information + MsgBoxStyle.YesNo, ".:: ADVERTENCIA ::.") = vbYes Then
                        MsgBox("Se agregó el Producto: " & dgrProductos.Item(4, item).Value)
                        AsignaSeleccion(o_dt, dgrProductos.Item(0, item).Value)
                        'Debo pasar los valores a un datatable y llevarlo al formulario de ventas
                        Me.Close()
                    End If
                Case ChrW(Keys.Escape) 'Se cierra el formulario actual
                    strBuscar = ""
                    wo_Dt = o_dt
                    Me.Close()
                Case ChrW(Keys.Back) ' borro la última letra escrita
                    strBuscar = Microsoft.VisualBasic.Left(strBuscar, strBuscar.Length - 1)
                    wo_Dt = AplicarFiltro(o_dt, strBuscar)
                Case Else
                    strBuscar = strBuscar & e.KeyChar.ToString
                    wo_Dt = AplicarFiltro(o_dt, strBuscar)

            End Select

            Me.dgrProductos.DataSource = wo_Dt

            ConfigurarGrilla()

        Catch ex As Exception
            Funciones.LogError(ex, "dgrProductos_KeyPress", Funciones.ObtieneUsuario)
        Finally
            wo_Dt = Nothing

        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub AsignaSeleccion(ByVal poDt As DataTable, ByVal piItem As Integer)
        Dim oDr As DataRow
        Dim oDt As New DataTable

        If piItem > 0 Then
            oDt = poDt.Clone
            For Each oDr In poDt.Select("PRO_ID = " & piItem & "")
                oDt.ImportRow(oDr)
                oDt.AcceptChanges()
                oDr = Nothing
            Next
            'lo dejo en memoria del actual datatable de Ventas
            frmVentas.ol_DtProducto = poDt
            'Agrego los datos al formulario para que ingrese cantidad
            With frmVentas
                .txtProductoBarra.Text = oDt.Rows(0).Item("PRO_CODIGO_BARRA")
                .txtCantidad.Focus()
            End With
        End If
    End Sub

    Private Function AplicarFiltro(ByVal poDt As DataTable, ByVal pszTexto As String) As DataTable
        Dim o_dr As DataRow
        Dim woDT As New DataTable

        woDT = poDt.Clone

        For Each wo_DR As DataRow In poDt.Select("PRO_NOMBRE LIKE '" & strBuscar & "%'")
            
            woDT.ImportRow(wo_DR)
            woDT.AcceptChanges()
            o_dr = Nothing

        Next

        AplicarFiltro = woDT
    End Function

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