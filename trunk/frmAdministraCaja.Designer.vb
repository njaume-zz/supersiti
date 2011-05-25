Imports Microsoft.VisualBasic.PowerPacks

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdministraCaja))
        Me.grpApertura = New System.Windows.Forms.GroupBox
        Me.txtImporteRetiro = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFecha = New System.Windows.Forms.TextBox
        Me.txtOperador = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtImporteApertura = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblOperacion = New System.Windows.Forms.Label
        Me.grpCaja = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtImportesCaja = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtTotal = New System.Windows.Forms.TextBox
        Me.txtTotalApertura = New System.Windows.Forms.TextBox
        Me.txtTotalRetiros = New System.Windows.Forms.TextBox
        Me.txtTotalVentas = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtCredito = New System.Windows.Forms.TextBox
        Me.txtTarjetas = New System.Windows.Forms.TextBox
        Me.txtBonos = New System.Windows.Forms.TextBox
        Me.txtEfectivo = New System.Windows.Forms.TextBox
        Me.txtSubTotal = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnConfirmar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.EpsonPrinter = New AxEPSON_Impresora_Fiscal.AxPrinterFiscal
        Me.grpApertura.SuspendLayout()
        Me.grpCaja.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EpsonPrinter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpApertura
        '
        Me.grpApertura.Controls.Add(Me.txtImporteRetiro)
        Me.grpApertura.Controls.Add(Me.Label9)
        Me.grpApertura.Controls.Add(Me.Label8)
        Me.grpApertura.Controls.Add(Me.txtFecha)
        Me.grpApertura.Controls.Add(Me.txtOperador)
        Me.grpApertura.Controls.Add(Me.Label7)
        Me.grpApertura.Controls.Add(Me.txtImporteApertura)
        Me.grpApertura.Controls.Add(Me.Label1)
        Me.grpApertura.Controls.Add(Me.lblOperacion)
        Me.grpApertura.Location = New System.Drawing.Point(13, 26)
        Me.grpApertura.Name = "grpApertura"
        Me.grpApertura.Size = New System.Drawing.Size(505, 143)
        Me.grpApertura.TabIndex = 0
        Me.grpApertura.TabStop = False
        Me.grpApertura.Text = "Datos de Apertura"
        '
        'txtImporteRetiro
        '
        Me.txtImporteRetiro.Location = New System.Drawing.Point(154, 106)
        Me.txtImporteRetiro.Name = "txtImporteRetiro"
        Me.txtImporteRetiro.Size = New System.Drawing.Size(112, 20)
        Me.txtImporteRetiro.TabIndex = 1
        Me.txtImporteRetiro.Text = "0.00"
        Me.txtImporteRetiro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(32, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Importe Retiro"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(32, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Fecha"
        '
        'txtFecha
        '
        Me.txtFecha.Location = New System.Drawing.Point(154, 25)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.Size = New System.Drawing.Size(112, 20)
        Me.txtFecha.TabIndex = 101
        '
        'txtOperador
        '
        Me.txtOperador.Location = New System.Drawing.Point(154, 52)
        Me.txtOperador.Name = "txtOperador"
        Me.txtOperador.ReadOnly = True
        Me.txtOperador.Size = New System.Drawing.Size(112, 20)
        Me.txtOperador.TabIndex = 100
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(32, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Operador"
        '
        'txtImporteApertura
        '
        Me.txtImporteApertura.Location = New System.Drawing.Point(154, 78)
        Me.txtImporteApertura.Name = "txtImporteApertura"
        Me.txtImporteApertura.Size = New System.Drawing.Size(112, 20)
        Me.txtImporteApertura.TabIndex = 0
        Me.txtImporteApertura.Text = "0.00"
        Me.txtImporteApertura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Importe de Apertura"
        '
        'lblOperacion
        '
        Me.lblOperacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperacion.Location = New System.Drawing.Point(278, 7)
        Me.lblOperacion.Name = "lblOperacion"
        Me.lblOperacion.Size = New System.Drawing.Size(224, 66)
        Me.lblOperacion.TabIndex = 6
        Me.lblOperacion.Text = "lblOperacion"
        Me.lblOperacion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grpCaja
        '
        Me.grpCaja.Controls.Add(Me.EpsonPrinter)
        Me.grpCaja.Controls.Add(Me.GroupBox1)
        Me.grpCaja.Controls.Add(Me.txtCredito)
        Me.grpCaja.Controls.Add(Me.txtTarjetas)
        Me.grpCaja.Controls.Add(Me.txtBonos)
        Me.grpCaja.Controls.Add(Me.txtEfectivo)
        Me.grpCaja.Controls.Add(Me.txtSubTotal)
        Me.grpCaja.Controls.Add(Me.Label6)
        Me.grpCaja.Controls.Add(Me.Label5)
        Me.grpCaja.Controls.Add(Me.Label4)
        Me.grpCaja.Controls.Add(Me.Label3)
        Me.grpCaja.Controls.Add(Me.Label2)
        Me.grpCaja.Controls.Add(Me.btnConfirmar)
        Me.grpCaja.Controls.Add(Me.btnCancelar)
        Me.grpCaja.Location = New System.Drawing.Point(13, 175)
        Me.grpCaja.Name = "grpCaja"
        Me.grpCaja.Size = New System.Drawing.Size(505, 262)
        Me.grpCaja.TabIndex = 1
        Me.grpCaja.TabStop = False
        Me.grpCaja.Text = "Gestión de Caja"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtImportesCaja)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtTotalApertura)
        Me.GroupBox1.Controls.Add(Me.txtTotalRetiros)
        Me.GroupBox1.Controls.Add(Me.txtTotalVentas)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Location = New System.Drawing.Point(297, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(204, 197)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Resúmen"
        '
        'txtImportesCaja
        '
        Me.txtImportesCaja.Location = New System.Drawing.Point(106, 109)
        Me.txtImportesCaja.Name = "txtImportesCaja"
        Me.txtImportesCaja.Size = New System.Drawing.Size(73, 20)
        Me.txtImportesCaja.TabIndex = 10
        Me.txtImportesCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(13, 111)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 13)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Dinero en Caja"
        '
        'txtTotal
        '
        Me.txtTotal.Enabled = False
        Me.txtTotal.Location = New System.Drawing.Point(106, 148)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 11
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalApertura
        '
        Me.txtTotalApertura.Enabled = False
        Me.txtTotalApertura.Location = New System.Drawing.Point(106, 79)
        Me.txtTotalApertura.Name = "txtTotalApertura"
        Me.txtTotalApertura.Size = New System.Drawing.Size(73, 20)
        Me.txtTotalApertura.TabIndex = 9
        Me.txtTotalApertura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalRetiros
        '
        Me.txtTotalRetiros.Enabled = False
        Me.txtTotalRetiros.Location = New System.Drawing.Point(106, 49)
        Me.txtTotalRetiros.Name = "txtTotalRetiros"
        Me.txtTotalRetiros.Size = New System.Drawing.Size(73, 20)
        Me.txtTotalRetiros.TabIndex = 8
        Me.txtTotalRetiros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalVentas
        '
        Me.txtTotalVentas.Enabled = False
        Me.txtTotalVentas.Location = New System.Drawing.Point(106, 19)
        Me.txtTotalVentas.Name = "txtTotalVentas"
        Me.txtTotalVentas.Size = New System.Drawing.Size(73, 20)
        Me.txtTotalVentas.TabIndex = 7
        Me.txtTotalVentas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(10, 150)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 20)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "TOTAL"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 78)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Apertura"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 51)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Total de Retiros"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Total de Ventas"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(96, 124)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(97, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "_______________"
        '
        'txtCredito
        '
        Me.txtCredito.Location = New System.Drawing.Point(154, 111)
        Me.txtCredito.Name = "txtCredito"
        Me.txtCredito.Size = New System.Drawing.Size(100, 20)
        Me.txtCredito.TabIndex = 5
        Me.txtCredito.Text = "0.00"
        Me.txtCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTarjetas
        '
        Me.txtTarjetas.Location = New System.Drawing.Point(154, 84)
        Me.txtTarjetas.Name = "txtTarjetas"
        Me.txtTarjetas.Size = New System.Drawing.Size(100, 20)
        Me.txtTarjetas.TabIndex = 4
        Me.txtTarjetas.Text = "0.00"
        Me.txtTarjetas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBonos
        '
        Me.txtBonos.AutoCompleteCustomSource.AddRange(New String() {"0.00", "00.00", "000.00"})
        Me.txtBonos.Location = New System.Drawing.Point(154, 58)
        Me.txtBonos.Name = "txtBonos"
        Me.txtBonos.Size = New System.Drawing.Size(100, 20)
        Me.txtBonos.TabIndex = 3
        Me.txtBonos.Text = "0.00"
        Me.txtBonos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEfectivo
        '
        Me.txtEfectivo.Location = New System.Drawing.Point(154, 32)
        Me.txtEfectivo.Name = "txtEfectivo"
        Me.txtEfectivo.Size = New System.Drawing.Size(100, 20)
        Me.txtEfectivo.TabIndex = 2
        Me.txtEfectivo.Text = "0.00"
        Me.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Location = New System.Drawing.Point(154, 156)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.Size = New System.Drawing.Size(100, 20)
        Me.txtSubTotal.TabIndex = 6
        Me.txtSubTotal.Text = "0.00"
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(86, 159)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Sub Total"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Importe Ctas. Ctes."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Importe en Tarjetas"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Importe en Bonos"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Importe en Efectivo"
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Image = Global.PV_Super.My.Resources.Resources.ok_x_19
        Me.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirmar.Location = New System.Drawing.Point(342, 226)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(75, 30)
        Me.btnConfirmar.TabIndex = 12
        Me.btnConfirmar.Text = "Con&firmar  "
        Me.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirmar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = Global.PV_Super.My.Resources.Resources.cancel_x_19
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.Location = New System.Drawing.Point(424, 226)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 30)
        Me.btnCancelar.TabIndex = 13
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PV_Super.My.Resources.Resources.Encabezado
        Me.PictureBox1.Location = New System.Drawing.Point(1, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(528, 25)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'EpsonPrinter
        '
        Me.EpsonPrinter.Enabled = True
        Me.EpsonPrinter.Location = New System.Drawing.Point(-12, 247)
        Me.EpsonPrinter.Name = "EpsonPrinter"
        Me.EpsonPrinter.OcxState = CType(resources.GetObject("EpsonPrinter.OcxState"), System.Windows.Forms.AxHost.State)
        Me.EpsonPrinter.Size = New System.Drawing.Size(32, 32)
        Me.EpsonPrinter.TabIndex = 4
        '
        'frmAdministraCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 449)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.grpCaja)
        Me.Controls.Add(Me.grpApertura)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAdministraCaja"
        Me.Text = "Administración de Cajas"
        Me.grpApertura.ResumeLayout(False)
        Me.grpApertura.PerformLayout()
        Me.grpCaja.ResumeLayout(False)
        Me.grpCaja.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EpsonPrinter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpApertura As System.Windows.Forms.GroupBox
    Friend WithEvents txtImporteApertura As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpCaja As System.Windows.Forms.GroupBox
    Friend WithEvents btnConfirmar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtTotalApertura As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalRetiros As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalVentas As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtImportesCaja As System.Windows.Forms.TextBox
    Friend WithEvents EpsonPrinter As AxEPSON_Impresora_Fiscal.AxPrinterFiscal
    'Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
    'Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
End Class
