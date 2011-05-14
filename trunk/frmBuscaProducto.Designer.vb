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
        Me.dgrProductos = New System.Windows.Forms.DataGridView
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblBusqueda = New System.Windows.Forms.Label
        CType(Me.dgrProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrProductos
        '
        Me.dgrProductos.AllowUserToAddRows = False
        Me.dgrProductos.AllowUserToDeleteRows = False
        Me.dgrProductos.AllowUserToOrderColumns = True
        Me.dgrProductos.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.dgrProductos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgrProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrProductos.Location = New System.Drawing.Point(0, 56)
        Me.dgrProductos.MultiSelect = False
        Me.dgrProductos.Name = "dgrProductos"
        Me.dgrProductos.ReadOnly = True
        Me.dgrProductos.Size = New System.Drawing.Size(570, 469)
        Me.dgrProductos.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PV_Super.My.Resources.Resources.Encabezado
        Me.PictureBox1.Location = New System.Drawing.Point(0, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(571, 25)
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'lblBusqueda
        '
        Me.lblBusqueda.AutoSize = True
        Me.lblBusqueda.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblBusqueda.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblBusqueda.Location = New System.Drawing.Point(14, 33)
        Me.lblBusqueda.MinimumSize = New System.Drawing.Size(300, 0)
        Me.lblBusqueda.Name = "lblBusqueda"
        Me.lblBusqueda.Size = New System.Drawing.Size(300, 13)
        Me.lblBusqueda.TabIndex = 5
        '
        'frmBuscaProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 526)
        Me.Controls.Add(Me.lblBusqueda)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.dgrProductos)
        Me.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBuscaProducto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar Productos"
        CType(Me.dgrProductos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrProductos As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblBusqueda As System.Windows.Forms.Label
End Class
