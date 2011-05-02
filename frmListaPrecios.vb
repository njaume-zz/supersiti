Public Class frmListaPrecios

#Region "Variables"
    Dim o_dt As DataTable
    Dim strBuscar As String
#End Region

#Region "Eventos"


    Private Sub frmListaPrecios_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompletarGrilla()
        'o_dt = New DataTable
    End Sub

    Private Sub dgrProductos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgrProductos.KeyPress
        Dim wo_Dt As DataTable
        Dim item As Integer

        Try
            wo_Dt = New DataTable
            wo_Dt = o_dt.Clone()
            Select Case e.KeyChar
                Case ChrW(Keys.Escape) 'Se cierra el formulario actual
                    strBuscar = ""
                    wo_Dt = o_dt
                    Me.Close()
                Case ChrW(Keys.Back) ' borro la última letra escrita
                    If Not strBuscar = "" Then
                        strBuscar = Microsoft.VisualBasic.Left(strBuscar, strBuscar.Length - 1)
                        wo_Dt = AplicarFiltro(o_dt, strBuscar)
                    Else
                        strBuscar = ""
                        wo_Dt = o_dt
                    End If
                Case Else
                    strBuscar = strBuscar & e.KeyChar.ToString
                    wo_Dt = AplicarFiltro(o_dt, strBuscar)

            End Select

            Me.dgrProductos.DataSource = wo_Dt

            ConfigurarGrilla()

        Catch ex As Exception
            Funciones.Manejador_Errores("dgrProductos_KeyPress", ex)
        Finally
            wo_Dt = Nothing

        End Try
    End Sub
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método genérico que permite aplicar un filtro sobre la grilla
    ''' a partir de la introducción de text en la misma.
    ''' </summary>
    ''' <param name="poDt">DataTable global</param>
    ''' <param name="pszTexto">Palabra a buscar</param>
    ''' <returns>DataTable filtrado</returns>
    ''' <remarks>Maxi Adad</remarks>
    Private Function AplicarFiltro(ByVal poDt As DataTable, ByVal pszTexto As String) As DataTable
        Dim o_dr As DataRow
        Dim woDT As New DataTable

        woDT = poDt.Clone
        If Not pszTexto = "" Then
            For Each wo_DR As DataRow In poDt.Select("PRO_NOMBRE LIKE '" & strBuscar & "%'")
                woDT.ImportRow(wo_DR)
                woDT.AcceptChanges()
                o_dr = Nothing
            Next
            AplicarFiltro = woDT
        Else
            AplicarFiltro = poDt
        End If
    End Function

    ''' <summary>
    ''' Método que permite realizar la configuración sobre la grilla.
    ''' Se establecen los campos a mostrar
    ''' </summary>
    ''' <remarks>Maxi Adad</remarks>
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

    ''' <summary>
    ''' Asigna la lista de productos completa a la grilla
    ''' </summary>
    ''' <remarks>Maxi Adad</remarks>
    Private Sub CompletarGrilla()
        Dim oDt As DataTable

        Try
            oDt = New DataTable
            oDt = clsProductoDAO.GetListaPrecio()
            'Me.dgrProductos.Rows.Clear()
            Me.dgrProductos.DataSource = oDt
            o_dt = oDt
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