<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVentas
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVentas))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.SSTInformaUsuario = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSUsuario = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSFecha = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSPtoVta = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSPC = New System.Windows.Forms.ToolStripStatusLabel
        Me.TSSIdUsuario = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtProductoBarra = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.TextBox
        Me.btnIngresaProducto = New System.Windows.Forms.Button
        Me.btnIngresaNegro = New System.Windows.Forms.Button
        Me.lblProductoNombre = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSubTotal = New System.Windows.Forms.TextBox
        Me.txtPcioProducto = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPcioTotal = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblFecha = New System.Windows.Forms.Label
        Me.SSTDescripciones = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel8 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel9 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel10 = New System.Windows.Forms.ToolStripStatusLabel
        Me.toolstMenu = New System.Windows.Forms.ToolStrip
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDescripcion = New System.Windows.Forms.TextBox
        Me.TSBBuscaProducto = New System.Windows.Forms.ToolStripButton
        Me.TSBBuscarPrecio = New System.Windows.Forms.ToolStripButton
        Me.TSBAceptaVenta = New System.Windows.Forms.ToolStripButton
        Me.TSBCancelaVenta = New System.Windows.Forms.ToolStripButton
        Me.TSBEliminaItem = New System.Windows.Forms.ToolStripButton
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.AdministraciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.tsmAnularTicket = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.CerrarSesiónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.SalirDelSistemaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BúsquedasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tsmBuscarProducto = New System.Windows.Forms.ToolStripMenuItem
        Me.ClienteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tsmConsultaPrecio = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ProductsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSTInformaUsuario.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SSTDescripciones.SuspendLayout()
        Me.toolstMenu.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 132)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(545, 477)
        Me.DataGridView1.TabIndex = 0
        '
        'SSTInformaUsuario
        '
        Me.SSTInformaUsuario.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.SSTInformaUsuario.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.TSSUsuario, Me.ToolStripStatusLabel2, Me.TSSFecha, Me.ToolStripStatusLabel3, Me.TSSPtoVta, Me.ToolStripStatusLabel4, Me.TSSPC, Me.TSSIdUsuario})
        Me.SSTInformaUsuario.Location = New System.Drawing.Point(0, 634)
        Me.SSTInformaUsuario.Name = "SSTInformaUsuario"
        Me.SSTInformaUsuario.Size = New System.Drawing.Size(831, 22)
        Me.SSTInformaUsuario.TabIndex = 1
        Me.SSTInformaUsuario.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusLabel1.Text = "Usuario: "
        '
        'TSSUsuario
        '
        Me.TSSUsuario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TSSUsuario.Name = "TSSUsuario"
        Me.TSSUsuario.Size = New System.Drawing.Size(71, 17)
        Me.TSSUsuario.Text = "TSSUsuario"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(41, 17)
        Me.ToolStripStatusLabel2.Text = "Fecha:"
        '
        'TSSFecha
        '
        Me.TSSFecha.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TSSFecha.Name = "TSSFecha"
        Me.TSSFecha.Size = New System.Drawing.Size(61, 17)
        Me.TSSFecha.Text = "TSSFecha"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(88, 17)
        Me.ToolStripStatusLabel3.Text = "Punto de Venta"
        '
        'TSSPtoVta
        '
        Me.TSSPtoVta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TSSPtoVta.Name = "TSSPtoVta"
        Me.TSSPtoVta.Size = New System.Drawing.Size(66, 17)
        Me.TSSPtoVta.Text = "TSSPtoVta"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(199, 17)
        Me.ToolStripStatusLabel4.Text = "El sistema se está ejecutando desde: "
        '
        'TSSPC
        '
        Me.TSSPC.Name = "TSSPC"
        Me.TSSPC.Size = New System.Drawing.Size(41, 17)
        Me.TSSPC.Text = "TSSPC"
        '
        'TSSIdUsuario
        '
        Me.TSSIdUsuario.Name = "TSSIdUsuario"
        Me.TSSIdUsuario.Size = New System.Drawing.Size(76, 17)
        Me.TSSIdUsuario.Text = "TSSidUsuario"
        Me.TSSIdUsuario.Visible = False
        '
        'txtProductoBarra
        '
        Me.txtProductoBarra.Location = New System.Drawing.Point(12, 90)
        Me.txtProductoBarra.Name = "txtProductoBarra"
        Me.txtProductoBarra.Size = New System.Drawing.Size(247, 20)
        Me.txtProductoBarra.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Código de Barra"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(494, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Cantidad"
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(497, 90)
        Me.txtCantidad.MaxLength = 4
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(47, 20)
        Me.txtCantidad.TabIndex = 5
        '
        'btnIngresaProducto
        '
        Me.btnIngresaProducto.Location = New System.Drawing.Point(632, 84)
        Me.btnIngresaProducto.Name = "btnIngresaProducto"
        Me.btnIngresaProducto.Size = New System.Drawing.Size(75, 30)
        Me.btnIngresaProducto.TabIndex = 7
        Me.btnIngresaProducto.Text = "Enter [F4]"
        Me.btnIngresaProducto.UseVisualStyleBackColor = True
        '
        'btnIngresaNegro
        '
        Me.btnIngresaNegro.Location = New System.Drawing.Point(713, 84)
        Me.btnIngresaNegro.Name = "btnIngresaNegro"
        Me.btnIngresaNegro.Size = New System.Drawing.Size(75, 30)
        Me.btnIngresaNegro.TabIndex = 8
        Me.btnIngresaNegro.Text = "Enter [F8]"
        Me.btnIngresaNegro.UseVisualStyleBackColor = True
        '
        'lblProductoNombre
        '
        Me.lblProductoNombre.AutoSize = True
        Me.lblProductoNombre.Location = New System.Drawing.Point(16, 116)
        Me.lblProductoNombre.Name = "lblProductoNombre"
        Me.lblProductoNombre.Size = New System.Drawing.Size(127, 13)
        Me.lblProductoNombre.TabIndex = 9
        Me.lblProductoNombre.Text = "Producto Seleccionado..."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtSubTotal)
        Me.GroupBox1.Controls.Add(Me.txtPcioProducto)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPcioTotal)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(552, 362)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(236, 245)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Importes"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 24)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "SubTotal"
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtSubTotal.Location = New System.Drawing.Point(107, 95)
        Me.txtSubTotal.MaxLength = 8
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(123, 29)
        Me.txtSubTotal.TabIndex = 6
        Me.txtSubTotal.Text = "0.00"
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcioProducto
        '
        Me.txtPcioProducto.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtPcioProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPcioProducto.Location = New System.Drawing.Point(107, 45)
        Me.txtPcioProducto.MaxLength = 8
        Me.txtPcioProducto.Name = "txtPcioProducto"
        Me.txtPcioProducto.ReadOnly = True
        Me.txtPcioProducto.Size = New System.Drawing.Size(123, 26)
        Me.txtPcioProducto.TabIndex = 5
        Me.txtPcioProducto.Text = "0.00"
        Me.txtPcioProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Producto"
        '
        'txtPcioTotal
        '
        Me.txtPcioTotal.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtPcioTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPcioTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPcioTotal.Location = New System.Drawing.Point(107, 185)
        Me.txtPcioTotal.MaxLength = 8
        Me.txtPcioTotal.Name = "txtPcioTotal"
        Me.txtPcioTotal.ReadOnly = True
        Me.txtPcioTotal.Size = New System.Drawing.Size(123, 35)
        Me.txtPcioTotal.TabIndex = 2
        Me.txtPcioTotal.Text = "0.00"
        Me.txtPcioTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 185)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 29)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Total"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Location = New System.Drawing.Point(601, 71)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(37, 13)
        Me.lblFecha.TabIndex = 12
        Me.lblFecha.Text = "Fecha"
        '
        'SSTDescripciones
        '
        Me.SSTDescripciones.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.SSTDescripciones.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.SSTDescripciones.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel5, Me.ToolStripStatusLabel6, Me.ToolStripStatusLabel7, Me.ToolStripStatusLabel8, Me.ToolStripStatusLabel9, Me.ToolStripStatusLabel10})
        Me.SSTDescripciones.Location = New System.Drawing.Point(0, 612)
        Me.SSTDescripciones.Name = "SSTDescripciones"
        Me.SSTDescripciones.Size = New System.Drawing.Size(831, 22)
        Me.SSTDescripciones.TabIndex = 13
        Me.SSTDescripciones.Text = "StatusStrip2"
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(104, 17)
        Me.ToolStripStatusLabel5.Text = "Confirmar=[Enter]"
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(122, 17)
        Me.ToolStripStatusLabel6.Text = "Confirmar Venta=[F5]"
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(122, 17)
        Me.ToolStripStatusLabel7.Text = "Buscar Producto=[F3]"
        '
        'ToolStripStatusLabel8
        '
        Me.ToolStripStatusLabel8.Name = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Size = New System.Drawing.Size(128, 17)
        Me.ToolStripStatusLabel8.Text = "Buscar Precio=[Ctrl+B]"
        '
        'ToolStripStatusLabel9
        '
        Me.ToolStripStatusLabel9.Name = "ToolStripStatusLabel9"
        Me.ToolStripStatusLabel9.Size = New System.Drawing.Size(168, 17)
        Me.ToolStripStatusLabel9.Text = "Autorizar Venta=[Ctrl+Shft+A]"
        '
        'ToolStripStatusLabel10
        '
        Me.ToolStripStatusLabel10.Name = "ToolStripStatusLabel10"
        Me.ToolStripStatusLabel10.Size = New System.Drawing.Size(132, 17)
        Me.ToolStripStatusLabel10.Text = "Quitar Producto=[Supr]"
        '
        'toolstMenu
        '
        Me.toolstMenu.ImageScalingSize = New System.Drawing.Size(35, 35)
        Me.toolstMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBBuscaProducto, Me.TSBBuscarPrecio, Me.ToolStripSeparator1, Me.TSBAceptaVenta, Me.TSBCancelaVenta, Me.TSBEliminaItem})
        Me.toolstMenu.Location = New System.Drawing.Point(0, 24)
        Me.toolstMenu.Name = "toolstMenu"
        Me.toolstMenu.Size = New System.Drawing.Size(831, 42)
        Me.toolstMenu.TabIndex = 14
        Me.toolstMenu.Text = "Iconos Menu"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 42)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(262, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Descripción del Producto"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Enabled = False
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDescripcion.Location = New System.Drawing.Point(265, 90)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(225, 20)
        Me.txtDescripcion.TabIndex = 16
        '
        'TSBBuscaProducto
        '
        Me.TSBBuscaProducto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBBuscaProducto.Image = Global.PV_Super.My.Resources.Resources._229
        Me.TSBBuscaProducto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBBuscaProducto.Name = "TSBBuscaProducto"
        Me.TSBBuscaProducto.Size = New System.Drawing.Size(39, 39)
        Me.TSBBuscaProducto.Text = "Buscar Producto"
        Me.TSBBuscaProducto.ToolTipText = "Buscar Producto"
        '
        'TSBBuscarPrecio
        '
        Me.TSBBuscarPrecio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBBuscarPrecio.Image = Global.PV_Super.My.Resources.Resources._338
        Me.TSBBuscarPrecio.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBBuscarPrecio.Name = "TSBBuscarPrecio"
        Me.TSBBuscarPrecio.Size = New System.Drawing.Size(39, 39)
        Me.TSBBuscarPrecio.Text = "Lista de Precios"
        '
        'TSBAceptaVenta
        '
        Me.TSBAceptaVenta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBAceptaVenta.Image = Global.PV_Super.My.Resources.Resources.Aprobar_venta
        Me.TSBAceptaVenta.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBAceptaVenta.Name = "TSBAceptaVenta"
        Me.TSBAceptaVenta.Size = New System.Drawing.Size(39, 39)
        Me.TSBAceptaVenta.Text = "TSBAceptarVenta"
        Me.TSBAceptaVenta.ToolTipText = "Aceptar Venta"
        '
        'TSBCancelaVenta
        '
        Me.TSBCancelaVenta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBCancelaVenta.Image = Global.PV_Super.My.Resources.Resources.Cancelar_venta
        Me.TSBCancelaVenta.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBCancelaVenta.Name = "TSBCancelaVenta"
        Me.TSBCancelaVenta.Size = New System.Drawing.Size(39, 39)
        Me.TSBCancelaVenta.Text = "TSBCancelaVenta"
        Me.TSBCancelaVenta.ToolTipText = "Cancelar Venta"
        '
        'TSBEliminaItem
        '
        Me.TSBEliminaItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBEliminaItem.Image = Global.PV_Super.My.Resources.Resources.Quitar_Producto_venta
        Me.TSBEliminaItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBEliminaItem.Name = "TSBEliminaItem"
        Me.TSBEliminaItem.Size = New System.Drawing.Size(39, 39)
        Me.TSBEliminaItem.Text = "TSBQuitarItem"
        Me.TSBEliminaItem.ToolTipText = "Quitar Item"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.MenuStrip1.BackgroundImage = Global.PV_Super.My.Resources.Resources.Encabezado
        Me.MenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AdministraciónToolStripMenuItem, Me.BúsquedasToolStripMenuItem, Me.ReportesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(831, 24)
        Me.MenuStrip1.TabIndex = 11
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AdministraciónToolStripMenuItem
        '
        Me.AdministraciónToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripMenuItem5, Me.ToolStripMenuItem4, Me.ToolStripMenuItem6, Me.ToolStripMenuItem7, Me.ToolStripMenuItem3, Me.tsmAnularTicket, Me.ToolStripSeparator2, Me.CerrarSesiónToolStripMenuItem, Me.ToolStripMenuItem1, Me.SalirDelSistemaToolStripMenuItem})
        Me.AdministraciónToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.AdministraciónToolStripMenuItem.Name = "AdministraciónToolStripMenuItem"
        Me.AdministraciónToolStripMenuItem.Size = New System.Drawing.Size(100, 20)
        Me.AdministraciónToolStripMenuItem.Text = "&Administración"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(248, 22)
        Me.ToolStripMenuItem2.Text = "Autorización Venta"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.ShortcutKeys = System.Windows.Forms.Keys.F11
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(248, 22)
        Me.ToolStripMenuItem5.Text = "Apertura de Caja"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(248, 22)
        Me.ToolStripMenuItem4.Text = "Retirar Efectivo"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(248, 22)
        Me.ToolStripMenuItem6.Text = "Cierre de Caja"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(248, 22)
        Me.ToolStripMenuItem7.Text = "Cierre Z"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(245, 6)
        Me.ToolStripMenuItem3.Visible = False
        '
        'tsmAnularTicket
        '
        Me.tsmAnularTicket.Name = "tsmAnularTicket"
        Me.tsmAnularTicket.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.tsmAnularTicket.Size = New System.Drawing.Size(248, 22)
        Me.tsmAnularTicket.Text = "Anular Ticket"
        Me.tsmAnularTicket.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(245, 6)
        '
        'CerrarSesiónToolStripMenuItem
        '
        Me.CerrarSesiónToolStripMenuItem.Name = "CerrarSesiónToolStripMenuItem"
        Me.CerrarSesiónToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CerrarSesiónToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.CerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(245, 6)
        '
        'SalirDelSistemaToolStripMenuItem
        '
        Me.SalirDelSistemaToolStripMenuItem.Name = "SalirDelSistemaToolStripMenuItem"
        Me.SalirDelSistemaToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.SalirDelSistemaToolStripMenuItem.Text = "Salir del Sistema"
        '
        'BúsquedasToolStripMenuItem
        '
        Me.BúsquedasToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmBuscarProducto, Me.ClienteToolStripMenuItem, Me.tsmConsultaPrecio})
        Me.BúsquedasToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.BúsquedasToolStripMenuItem.Name = "BúsquedasToolStripMenuItem"
        Me.BúsquedasToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.BúsquedasToolStripMenuItem.Text = "&Búsquedas"
        '
        'tsmBuscarProducto
        '
        Me.tsmBuscarProducto.Name = "tsmBuscarProducto"
        Me.tsmBuscarProducto.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.tsmBuscarProducto.Size = New System.Drawing.Size(214, 22)
        Me.tsmBuscarProducto.Text = "&Producto"
        '
        'ClienteToolStripMenuItem
        '
        Me.ClienteToolStripMenuItem.Name = "ClienteToolStripMenuItem"
        Me.ClienteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ClienteToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ClienteToolStripMenuItem.Text = "Pro&veedor"
        Me.ClienteToolStripMenuItem.Visible = False
        '
        'tsmConsultaPrecio
        '
        Me.tsmConsultaPrecio.Name = "tsmConsultaPrecio"
        Me.tsmConsultaPrecio.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.tsmConsultaPrecio.Size = New System.Drawing.Size(214, 22)
        Me.tsmConsultaPrecio.Text = "Consulta de Precio"
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProductsToolStripMenuItem, Me.StockToolStripMenuItem})
        Me.ReportesToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.ReportesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Silver
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        Me.ReportesToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.ReportesToolStripMenuItem.Text = "&Reportes"
        Me.ReportesToolStripMenuItem.Visible = False
        '
        'ProductsToolStripMenuItem
        '
        Me.ProductsToolStripMenuItem.Name = "ProductsToolStripMenuItem"
        Me.ProductsToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.ProductsToolStripMenuItem.Text = "Productos"
        '
        'StockToolStripMenuItem
        '
        Me.StockToolStripMenuItem.Name = "StockToolStripMenuItem"
        Me.StockToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.StockToolStripMenuItem.Text = "Stock"
        '
        'frmVentas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(831, 656)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.toolstMenu)
        Me.Controls.Add(Me.SSTDescripciones)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblProductoNombre)
        Me.Controls.Add(Me.btnIngresaNegro)
        Me.Controls.Add(Me.btnIngresaProducto)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtProductoBarra)
        Me.Controls.Add(Me.SSTInformaUsuario)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmVentas"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Administración de Ventas"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSTInformaUsuario.ResumeLayout(False)
        Me.SSTInformaUsuario.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SSTDescripciones.ResumeLayout(False)
        Me.SSTDescripciones.PerformLayout()
        Me.toolstMenu.ResumeLayout(False)
        Me.toolstMenu.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents SSTInformaUsuario As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSSUsuario As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSSFecha As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSSPtoVta As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSSPC As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtProductoBarra As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents btnIngresaProducto As System.Windows.Forms.Button
    Friend WithEvents btnIngresaNegro As System.Windows.Forms.Button
    Friend WithEvents lblProductoNombre As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPcioTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPcioProducto As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents AdministraciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CerrarSesiónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SalirDelSistemaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BúsquedasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmBuscarProducto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClienteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StockToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmConsultaPrecio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents SSTDescripciones As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel7 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel8 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel9 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel10 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSSIdUsuario As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents toolstMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents TSBBuscaProducto As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents TSBBuscarPrecio As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSBAceptaVenta As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBCancelaVenta As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBEliminaItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsmAnularTicket As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator

End Class
