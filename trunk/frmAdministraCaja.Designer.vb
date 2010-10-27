<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdministraCaja
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
        Me.grpApertura = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtImporteApertura = New System.Windows.Forms.TextBox
        Me.grpCaja = New System.Windows.Forms.GroupBox
        Me.btnSalir = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnConfirmar = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtTotalCaja = New System.Windows.Forms.TextBox
        Me.grpApertura.SuspendLayout()
        Me.grpCaja.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpApertura
        '
        Me.grpApertura.Controls.Add(Me.txtImporteApertura)
        Me.grpApertura.Controls.Add(Me.Label1)
        Me.grpApertura.Location = New System.Drawing.Point(13, 13)
        Me.grpApertura.Name = "grpApertura"
        Me.grpApertura.Size = New System.Drawing.Size(505, 67)
        Me.grpApertura.TabIndex = 0
        Me.grpApertura.TabStop = False
        Me.grpApertura.Text = "Datos de Apertura"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(105, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Importe de Apertura"
        '
        'txtImporteApertura
        '
        Me.txtImporteApertura.Location = New System.Drawing.Point(230, 26)
        Me.txtImporteApertura.Name = "txtImporteApertura"
        Me.txtImporteApertura.Size = New System.Drawing.Size(112, 20)
        Me.txtImporteApertura.TabIndex = 1
        Me.txtImporteApertura.Text = "0.00"
        Me.txtImporteApertura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grpCaja
        '
        Me.grpCaja.Controls.Add(Me.txtTotalCaja)
        Me.grpCaja.Controls.Add(Me.Label6)
        Me.grpCaja.Controls.Add(Me.Label5)
        Me.grpCaja.Controls.Add(Me.Label4)
        Me.grpCaja.Controls.Add(Me.Label3)
        Me.grpCaja.Controls.Add(Me.Label2)
        Me.grpCaja.Controls.Add(Me.btnConfirmar)
        Me.grpCaja.Controls.Add(Me.btnCancelar)
        Me.grpCaja.Location = New System.Drawing.Point(13, 96)
        Me.grpCaja.Name = "grpCaja"
        Me.grpCaja.Size = New System.Drawing.Size(505, 273)
        Me.grpCaja.TabIndex = 1
        Me.grpCaja.TabStop = False
        Me.grpCaja.Text = "Gestión de Caja"
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(435, 384)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(75, 23)
        Me.btnSalir.TabIndex = 3
        Me.btnSalir.Text = "&Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(422, 244)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Location = New System.Drawing.Point(341, 244)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(75, 23)
        Me.btnConfirmar.TabIndex = 4
        Me.btnConfirmar.Text = "Con&firmar"
        Me.btnConfirmar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Label3"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Label4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 244)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Total"
        '
        'txtTotalCaja
        '
        Me.txtTotalCaja.Location = New System.Drawing.Point(90, 244)
        Me.txtTotalCaja.Name = "txtTotalCaja"
        Me.txtTotalCaja.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalCaja.TabIndex = 10
        Me.txtTotalCaja.Text = "0.00"
        Me.txtTotalCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmAdministraCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 419)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.grpCaja)
        Me.Controls.Add(Me.grpApertura)
        Me.Name = "frmAdministraCaja"
        Me.Text = "Administración de Cajas"
        Me.grpApertura.ResumeLayout(False)
        Me.grpApertura.PerformLayout()
        Me.grpCaja.ResumeLayout(False)
        Me.grpCaja.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpApertura As System.Windows.Forms.GroupBox
    Friend WithEvents txtImporteApertura As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpCaja As System.Windows.Forms.GroupBox
    Friend WithEvents btnConfirmar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTotalCaja As System.Windows.Forms.TextBox
End Class
