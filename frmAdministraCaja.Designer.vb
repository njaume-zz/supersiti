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
        Me.txtImporteRetiro = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblOperacion = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFecha = New System.Windows.Forms.TextBox
        Me.txtOperador = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtImporteApertura = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grpCaja = New System.Windows.Forms.GroupBox
        Me.txtCredito = New System.Windows.Forms.TextBox
        Me.txtTarjetas = New System.Windows.Forms.TextBox
        Me.txtBonos = New System.Windows.Forms.TextBox
        Me.txtEfectivo = New System.Windows.Forms.TextBox
        Me.txtTotalCaja = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnConfirmar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnSalir = New System.Windows.Forms.Button
        Me.grpApertura.SuspendLayout()
        Me.grpCaja.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpApertura
        '
        Me.grpApertura.Controls.Add(Me.txtImporteRetiro)
        Me.grpApertura.Controls.Add(Me.Label9)
        Me.grpApertura.Controls.Add(Me.lblOperacion)
        Me.grpApertura.Controls.Add(Me.Label8)
        Me.grpApertura.Controls.Add(Me.txtFecha)
        Me.grpApertura.Controls.Add(Me.txtOperador)
        Me.grpApertura.Controls.Add(Me.Label7)
        Me.grpApertura.Controls.Add(Me.txtImporteApertura)
        Me.grpApertura.Controls.Add(Me.Label1)
        Me.grpApertura.Location = New System.Drawing.Point(13, 13)
        Me.grpApertura.Name = "grpApertura"
        Me.grpApertura.Size = New System.Drawing.Size(505, 143)
        Me.grpApertura.TabIndex = 0
        Me.grpApertura.TabStop = False
        Me.grpApertura.Text = "Datos de Apertura"
        '
        'txtImporteRetiro
        '
        Me.txtImporteRetiro.Location = New System.Drawing.Point(155, 106)
        Me.txtImporteRetiro.Name = "txtImporteRetiro"
        Me.txtImporteRetiro.Size = New System.Drawing.Size(112, 20)
        Me.txtImporteRetiro.TabIndex = 22
        Me.txtImporteRetiro.Text = "0.00"
        Me.txtImporteRetiro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(30, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Importe Retiro"
        '
        'lblOperacion
        '
        Me.lblOperacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperacion.Location = New System.Drawing.Point(273, 28)
        Me.lblOperacion.Name = "lblOperacion"
        Me.lblOperacion.Size = New System.Drawing.Size(224, 66)
        Me.lblOperacion.TabIndex = 6
        Me.lblOperacion.Text = "lblOperacion"
        Me.lblOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(30, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Fecha"
        '
        'txtFecha
        '
        Me.txtFecha.Location = New System.Drawing.Point(155, 25)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.Size = New System.Drawing.Size(112, 20)
        Me.txtFecha.TabIndex = 19
        '
        'txtOperador
        '
        Me.txtOperador.Location = New System.Drawing.Point(155, 52)
        Me.txtOperador.Name = "txtOperador"
        Me.txtOperador.ReadOnly = True
        Me.txtOperador.Size = New System.Drawing.Size(112, 20)
        Me.txtOperador.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(30, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Operador"
        '
        'txtImporteApertura
        '
        Me.txtImporteApertura.Location = New System.Drawing.Point(155, 78)
        Me.txtImporteApertura.Name = "txtImporteApertura"
        Me.txtImporteApertura.Size = New System.Drawing.Size(112, 20)
        Me.txtImporteApertura.TabIndex = 0
        Me.txtImporteApertura.Text = "0.00"
        Me.txtImporteApertura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Importe de Apertura"
        '
        'grpCaja
        '
        Me.grpCaja.Controls.Add(Me.txtCredito)
        Me.grpCaja.Controls.Add(Me.txtTarjetas)
        Me.grpCaja.Controls.Add(Me.txtBonos)
        Me.grpCaja.Controls.Add(Me.txtEfectivo)
        Me.grpCaja.Controls.Add(Me.txtTotalCaja)
        Me.grpCaja.Controls.Add(Me.Label6)
        Me.grpCaja.Controls.Add(Me.Label5)
        Me.grpCaja.Controls.Add(Me.Label4)
        Me.grpCaja.Controls.Add(Me.Label3)
        Me.grpCaja.Controls.Add(Me.Label2)
        Me.grpCaja.Controls.Add(Me.btnConfirmar)
        Me.grpCaja.Controls.Add(Me.btnCancelar)
        Me.grpCaja.Location = New System.Drawing.Point(13, 162)
        Me.grpCaja.Name = "grpCaja"
        Me.grpCaja.Size = New System.Drawing.Size(505, 231)
        Me.grpCaja.TabIndex = 1
        Me.grpCaja.TabStop = False
        Me.grpCaja.Text = "Gestión de Caja"
        '
        'txtCredito
        '
        Me.txtCredito.Location = New System.Drawing.Point(155, 111)
        Me.txtCredito.Name = "txtCredito"
        Me.txtCredito.Size = New System.Drawing.Size(100, 20)
        Me.txtCredito.TabIndex = 4
        Me.txtCredito.Text = "0.00"
        Me.txtCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTarjetas
        '
        Me.txtTarjetas.Location = New System.Drawing.Point(155, 84)
        Me.txtTarjetas.Name = "txtTarjetas"
        Me.txtTarjetas.Size = New System.Drawing.Size(100, 20)
        Me.txtTarjetas.TabIndex = 3
        Me.txtTarjetas.Text = "0.00"
        Me.txtTarjetas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBonos
        '
        Me.txtBonos.AutoCompleteCustomSource.AddRange(New String() {"0.00", "00.00", "000.00"})
        Me.txtBonos.Location = New System.Drawing.Point(155, 58)
        Me.txtBonos.Name = "txtBonos"
        Me.txtBonos.Size = New System.Drawing.Size(100, 20)
        Me.txtBonos.TabIndex = 2
        Me.txtBonos.Text = "0.00"
        Me.txtBonos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEfectivo
        '
        Me.txtEfectivo.Location = New System.Drawing.Point(155, 32)
        Me.txtEfectivo.Name = "txtEfectivo"
        Me.txtEfectivo.Size = New System.Drawing.Size(100, 20)
        Me.txtEfectivo.TabIndex = 1
        Me.txtEfectivo.Text = "0.00"
        Me.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalCaja
        '
        Me.txtTotalCaja.Location = New System.Drawing.Point(155, 156)
        Me.txtTotalCaja.Name = "txtTotalCaja"
        Me.txtTotalCaja.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalCaja.TabIndex = 5
        Me.txtTotalCaja.Text = "0.00"
        Me.txtTotalCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(95, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Total"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Importe en Crédito"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Importe en Tarjetas"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Importe en Bonos"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Importe en Efectivo"
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Location = New System.Drawing.Point(341, 187)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(75, 23)
        Me.btnConfirmar.TabIndex = 6
        Me.btnConfirmar.Text = "Con&firmar"
        Me.btnConfirmar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(422, 187)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(443, 399)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(75, 23)
        Me.btnSalir.TabIndex = 8
        Me.btnSalir.Text = "&Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'frmAdministraCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 434)
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
    Friend WithEvents txtCredito As System.Windows.Forms.TextBox
    Friend WithEvents txtTarjetas As System.Windows.Forms.TextBox
    Friend WithEvents txtBonos As System.Windows.Forms.TextBox
    Friend WithEvents txtEfectivo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFecha As System.Windows.Forms.TextBox
    Friend WithEvents txtOperador As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblOperacion As System.Windows.Forms.Label
    Friend WithEvents txtImporteRetiro As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
