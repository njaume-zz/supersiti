<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBuscaProducto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBuscaProducto))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Codigo_prd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Descripcion_prd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UnidadVenta_prd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Pesable_prd = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtDescripcionProd = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnBuscar = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Codigo_prd, Me.Descripcion_prd, Me.UnidadVenta_prd, Me.Pesable_prd})
        Me.DataGridView1.Location = New System.Drawing.Point(1, 61)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(443, 437)
        Me.DataGridView1.TabIndex = 0
        '
        'Codigo_prd
        '
        Me.Codigo_prd.HeaderText = "Código"
        Me.Codigo_prd.Name = "Codigo_prd"
        Me.Codigo_prd.ReadOnly = True
        '
        'Descripcion_prd
        '
        Me.Descripcion_prd.HeaderText = "Descripción"
        Me.Descripcion_prd.Name = "Descripcion_prd"
        Me.Descripcion_prd.ReadOnly = True
        '
        'UnidadVenta_prd
        '
        Me.UnidadVenta_prd.HeaderText = "Unidad de Venta"
        Me.UnidadVenta_prd.Name = "UnidadVenta_prd"
        Me.UnidadVenta_prd.ReadOnly = True
        '
        'Pesable_prd
        '
        Me.Pesable_prd.HeaderText = "Pesable"
        Me.Pesable_prd.Name = "Pesable_prd"
        Me.Pesable_prd.ReadOnly = True
        '
        'txtDescripcionProd
        '
        Me.txtDescripcionProd.Location = New System.Drawing.Point(121, 23)
        Me.txtDescripcionProd.Name = "txtDescripcionProd"
        Me.txtDescripcionProd.Size = New System.Drawing.Size(180, 20)
        Me.txtDescripcionProd.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Descripción"
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(323, 22)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(75, 23)
        Me.btnBuscar.TabIndex = 3
        Me.btnBuscar.Text = "&Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'frmBuscaProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 498)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDescripcionProd)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBuscaProducto"
        Me.Text = "Buscar Productos"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Codigo_prd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion_prd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnidadVenta_prd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pesable_prd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtDescripcionProd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
End Class
