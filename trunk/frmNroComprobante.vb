Public Class frmNroComprobante

#Region "Eventos"

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If MsgBox("¿Está seguro que desea cerrar la Administración de Cajas?", _
                  MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CIERRE DE ADMINISTRACION DE CAJA ::.") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        MsgBox("Buscar Cabecera de Comprobante")
    End Sub
#End Region


End Class