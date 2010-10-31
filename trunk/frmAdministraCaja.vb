﻿Public Class frmAdministraCaja

#Region "Métodos"

    Private Function Validar() As Boolean
        Dim ok As Boolean
        If Me.txtBonos.Text = "0.00" Or Me.txtCredito.Text = "0.00" Or Me.txtEfectivo.Text = "0.00" Or _
            Me.txtTarjetas.Text = "0.00" Or Me.txtImporteApertura.Text = "0.00" Then
            ok = False
        Else
            ok = True
        End If
        Return ok
    End Function

    Private Sub LimiparCampos()
        Me.txtBonos.Text = "0.00"
        Me.txtCredito.Text = "0.00"
        Me.txtEfectivo.Text = "0.00"
        Me.txtTarjetas.Text = "0.00"
        Me.txtImporteApertura.Text = "0.00"
        Me.txtOperador.Text = ""
        Me.txtTotalCaja.Text = "0.00"
        Me.txtFecha.Text = Format(Now, "dd/MM/yyyy")
    End Sub

    Private Sub CalcularTotal()
        Dim total As Double

        total = CDbl(Me.txtBonos.Text) + CDbl(Me.txtCredito.Text) + CDbl(Me.txtEfectivo.Text) + CDbl(Me.txtTarjetas.Text)
        Me.txtTotalCaja.Text = Str(CDbl(Me.txtImporteApertura.Text) - total)
        Me.txtTotalCaja.Enabled = False
    End Sub

    Private Function VerificarApertura() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaAccionesCaja(1, idUsr)  ' 1 = Apertura
        Return ok
    End Function

    Private Function VerificarRetiro() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaAccionesCaja(2, idUsr) ' 2 = Retiro
        Return ok
    End Function

    Private Function VerificarCierreX() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaAccionesCaja(3, idUsr) ' 3 = Cierre X
        Return ok
    End Function

    Private Function VerificarCierreZ() As Boolean
        Dim ok As Boolean
        Dim idUsr As Integer = CInt(frmVentas.SSTInformaUsuario.Items("TSSIdUsuario").Text)
        ok = clsCajaDAO.ValidaAccionesCaja(4, idUsr) '4 = Cierre Z
        Return ok
    End Function

    Private Function RecuperaImportesCaja() As DataTable
        Dim str As String = "SELECT	CAJ.CAJ_ID,CAJ.CAJ_NUMERO,CAJ.CAE_ID,CAJ.CAJ_FECHAAPERTURA,CAJ.CAJ_FECHACIERRE, " & _
                            "CAE.CAE_ID,CAE.CAE_NOMBRE,CAA.CAA_ID,SUM(CAA.CAA_IMPORTE) AS 'CAA_IMPORTE',CAA.CAA_FECHA," & _
                            "CAA.USU_ID  FROM V_CAJA CAJ INNER JOIN V_CAJA_ESTADO CAE ON CAJ.CAE_ID = CAE.CAE_ID INNER JOIN V_CAJA_ACCIONES CAA " & _
                            " ON CAE.CAE_ID = CAA.CAE_ID GROUP BY CAJ.CAE_ID,CAJ.CAJ_ID,CAJ.CAJ_NUMERO,CAJ.CAJ_FECHAAPERTURA," & _
                            "CAJ.CAJ_FECHACIERRE,CAE.CAE_ID,CAE.CAE_NOMBRE,CAA.CAA_ID,CAA.CAA_FECHA,CAA.USU_ID() ORDER BY SUM(CAA.CAA_IMPORTE)"
    End Function
#End Region

#Region "Eventos"

    Private Sub btnConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click
        Dim Estado As Integer

        Try

            If Not Validar() Then
                MsgBox("No se puede confirmar la operación porque faltan datos.-", MsgBoxStyle.Exclamation, ".::OPERACION FALLIDA")
                LimiparCampos()
            Else
                Select Case Me.lblOperacion.Text
                    Case "Apertura"  'CAE_ID = 1
                        Dim caja As New clsCaja(0, frmVentas.TSSPtoVta.Text, 1, Now, "1900/01/01", 1)
                        Estado = clsCajaDAO.InsertaCaja(caja)

                    Case "Retiro"    'CAE_ID = 2
                        Dim caja As New clsCaja(0, frmVentas.TSSPtoVta.Text, 2, Now, "1900/01/01", 1)
                        Estado = clsCajaDAO.InsertaCaja(caja)
                    Case "Cierre X"  'CAE_ID = 3
                        Dim caja As New clsCaja(0, frmVentas.TSSPtoVta.Text, 3, Now, "1900/01/01", 1)
                        Estado = clsCajaDAO.InsertaCaja(caja)
                    Case "Cierre Z"  'CAE_ID = 4
                        Dim caja As New clsCaja(0, frmVentas.TSSPtoVta.Text, 4, Now, "1900/01/01", 1)
                        Estado = clsCajaDAO.InsertaCaja(caja)
                End Select
                If Estado = -1 Then
                    MessageBox.Show("La operación " & Me.lblOperacion.Text & " falló y no pudo guardarse.-", ".:: ERROR EN CAJAS ::.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'GUARDAR EN ARCHIVO LOG
                Else
                    MessageBox.Show("La operación " & Me.lblOperacion.Text & " se realizó con éxito.-", ".:: Operación Exitosa ::.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            Funciones.LogError(ex, "Confimación de operación en caja - " & Me.lblOperacion.Text, frmVentas.TSSUsuario.Text)
        End Try
    End Sub

    Private Sub frmAdministraCaja_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtOperador.Text = ObtieneUsuario()
        Me.txtFecha.Text = Format(Now, "dd/MM/yyyy")
        If Me.lblOperacion.Text = "Apertura" Then
            Me.txtImporteApertura.Enabled = True
        Else
            Me.txtImporteApertura.Enabled = False
        End If

        Select Case Me.lblOperacion.Text
            Case "Apertura"
                'acá se llama a los métodos para determinar si no hay aperturas pendientes
                ' para este usuario 
                If VerificarApertura() Then
                    MessageBox.Show("Existe una APERTURA PENDIENTE para el Usuario: " & frmVentas.TSSUsuario.Text & _
                                    ". Debe realizar el Cierre X, primero.", ".:: Caja con Apertura ::.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.Close()
                End If
            Case "Retiro"
                'acá se llama a los métodos para determinar si hay una apertura para ese usuario
                VerificarRetiro()
            Case "Cierre X"
                'acá se llama a los métodos para determinar si hay una apertura para ese usuario
                'si se permite el cierre, se recupera de la base los importes de las ventas
                'Estas ventas se deberían recuperar por cada tipo de pago.
                'Hacer un Store con la consulta hecha en el ESCRITORIO
            Case "Cierre Z"

        End Select
    End Sub

    Private Sub txtBonos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBonos.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtBonos_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBonos.LostFocus
        If Me.txtBonos.Text = "" Then
            Me.txtBonos.Text = "0.00"
        End If
        Me.txtBonos.Text = Convert.ToDouble(Me.txtBonos.Text)
        CalcularTotal()
    End Sub

    Private Sub txtEfectivo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEfectivo.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtEfectivo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEfectivo.LostFocus
        If Me.txtEfectivo.Text = "" Then
            Me.txtEfectivo.Text = "0.00"
        End If
        Me.txtEfectivo.Text = Convert.ToDouble(Me.txtEfectivo.Text)
        CalcularTotal()
    End Sub

    Private Sub txtCredito_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCredito.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtCredito_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCredito.LostFocus
        If Me.txtCredito.Text = "" Then
            Me.txtCredito.Text = "0.00"
        End If
        Me.txtCredito.Text = Convert.ToDouble(Me.txtCredito.Text)
        CalcularTotal()
    End Sub

    Private Sub txtTarjetas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTarjetas.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtTarjetas_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTarjetas.LostFocus
        If Me.txtTarjetas.Text = "" Then
            Me.txtTarjetas.Text = "0.00"
        End If
        Me.txtTarjetas.Text = Convert.ToDouble(Me.txtTarjetas.Text)
        CalcularTotal()
    End Sub

    Private Sub txtImporteApertura_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteApertura.KeyPress
        If Not ValidaNumerico(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtImporteApertura_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImporteApertura.LostFocus
        If Me.txtImporteApertura.Text = "" Then
            Me.txtImporteApertura.Text = "0.00"
        End If
        Me.txtImporteApertura.Text = Convert.ToDouble(Me.txtImporteApertura.Text)
        CalcularTotal()
    End Sub

#End Region


End Class