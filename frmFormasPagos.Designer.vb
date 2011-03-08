<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFormasPagos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFormasPagos))
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbTipoComprobante = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSubTotal = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnAceptarVenta = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.txtVuelto = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtTotalEnTarjeta = New System.Windows.Forms.TextBox
        Me.txtTotalEnEfectivo = New System.Windows.Forms.TextBox
        Me.txtDescuento = New System.Windows.Forms.TextBox
        Me.txtTotalAPagar = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(40, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(295, 37)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Resumen de Pago"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbTipoComprobante)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtSubTotal)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnAceptarVenta)
        Me.GroupBox1.Controls.Add(Me.btnCancelar)
        Me.GroupBox1.Controls.Add(Me.txtVuelto)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtTotalEnTarjeta)
        Me.GroupBox1.Controls.Add(Me.txtTotalEnEfectivo)
        Me.GroupBox1.Controls.Add(Me.txtDescuento)
        Me.GroupBox1.Controls.Add(Me.txtTotalAPagar)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 68)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 395)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Importes y Formas de Pago"
        '
        'cmbTipoComprobante
        '
        Me.cmbTipoComprobante.FormattingEnabled = True
        Me.cmbTipoComprobante.Location = New System.Drawing.Point(194, 29)
        Me.cmbTipoComprobante.Name = "cmbTipoComprobante"
        Me.cmbTipoComprobante.Size = New System.Drawing.Size(146, 21)
        Me.cmbTipoComprobante.TabIndex = 14
        Me.cmbTipoComprobante.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(181, 20)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Tipo de Comprobante"
        Me.Label8.Visible = False
        '
        'txtSubTotal
        '
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.Location = New System.Drawing.Point(196, 138)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.Size = New System.Drawing.Size(144, 26)
        Me.txtSubTotal.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 144)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 20)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "SubTotal"
        '
        'btnAceptarVenta
        '
        Me.btnAceptarVenta.Image = Global.PV_Super.My.Resources.Resources.ok_x_19
        Me.btnAceptarVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAceptarVenta.Location = New System.Drawing.Point(196, 357)
        Me.btnAceptarVenta.Name = "btnAceptarVenta"
        Me.btnAceptarVenta.Size = New System.Drawing.Size(75, 30)
        Me.btnAceptarVenta.TabIndex = 6
        Me.btnAceptarVenta.Text = "&Aceptar"
        Me.btnAceptarVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptarVenta.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = Global.PV_Super.My.Resources.Resources.cancel_x_19
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.Location = New System.Drawing.Point(274, 357)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 30)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'txtVuelto
        '
        Me.txtVuelto.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtVuelto.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVuelto.Enabled = False
        Me.txtVuelto.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVuelto.Location = New System.Drawing.Point(197, 304)
        Me.txtVuelto.Name = "txtVuelto"
        Me.txtVuelto.Size = New System.Drawing.Size(144, 28)
        Me.txtVuelto.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 307)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 25)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "VUELTO"
        '
        'txtTotalEnTarjeta
        '
        Me.txtTotalEnTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalEnTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalEnTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalEnTarjeta.Location = New System.Drawing.Point(196, 225)
        Me.txtTotalEnTarjeta.Name = "txtTotalEnTarjeta"
        Me.txtTotalEnTarjeta.Size = New System.Drawing.Size(144, 26)
        Me.txtTotalEnTarjeta.TabIndex = 4
        '
        'txtTotalEnEfectivo
        '
        Me.txtTotalEnEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalEnEfectivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalEnEfectivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalEnEfectivo.Location = New System.Drawing.Point(196, 180)
        Me.txtTotalEnEfectivo.Name = "txtTotalEnEfectivo"
        Me.txtTotalEnEfectivo.Size = New System.Drawing.Size(144, 26)
        Me.txtTotalEnEfectivo.TabIndex = 3
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescuento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.Location = New System.Drawing.Point(196, 97)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(144, 26)
        Me.txtDescuento.TabIndex = 1
        '
        'txtTotalAPagar
        '
        Me.txtTotalAPagar.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtTotalAPagar.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalAPagar.Enabled = False
        Me.txtTotalAPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAPagar.Location = New System.Drawing.Point(196, 58)
        Me.txtTotalAPagar.Name = "txtTotalAPagar"
        Me.txtTotalAPagar.Size = New System.Drawing.Size(144, 19)
        Me.txtTotalAPagar.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 227)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 20)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Pago con Tarjeta"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(145, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Pago en Efectivo"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Descuento (%)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Total A Pagar"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PV_Super.My.Resources.Resources.Encabezado
        Me.PictureBox1.Location = New System.Drawing.Point(-1, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(384, 25)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'frmFormasPagos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 469)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFormasPagos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Formas de Pagos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalAPagar As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalEnTarjeta As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalEnEfectivo As System.Windows.Forms.TextBox
    Friend WithEvents txtVuelto As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptarVenta As System.Windows.Forms.Button
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoComprobante As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
