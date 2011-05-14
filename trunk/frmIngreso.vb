Public Class frmIngreso


#Region "Métodos"

    ''' <summary>
    ''' Método que limpia los controles de la pantalla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimpiarForm()
        Me.txtPassword.Text = ""
        Me.txtUsuario.Text = ""
        Me.txtUsuario.Focus()
    End Sub

    ''' <summary>
    ''' Método que valida si el usuario está autorizado a ingresar
    ''' </summary>
    ''' <param name="p_usuario">Usuario</param>
    ''' <param name="p_password">Password</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarUsuario(ByVal p_usuario As String, ByVal p_password As String) As DataTable
        Dim o_dt As DataTable

        Try

            o_dt = New DataTable
            ' Si no encontró nada, devuelve Nothing, se valida posteriormente
            o_dt = clsUsuarioDAO.ValidaUsuario(p_usuario, p_password)
        Catch ex As Exception
            o_dt = Nothing
            Funciones.LogError(ex, "OK_Click", "Ninguno")
        End Try

        Return o_dt
    End Function

    ''' <summary>
    ''' Valida la existencia de una instancia anterior de la misma aplicación
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Friend Function PrevInstance() As Boolean
        If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Eventos"

    Private Sub frmIngreso_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If PrevInstance() Then
            MessageBox.Show("El sistema Ya se está ejecutando", ".:: APLICACION EN EJECUCION ::.", _
                            MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End
        End If
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim o_dtUsr As DataTable

        Try

            If Not Me.txtUsuario.Text = "" Or Not Me.txtPassword.Text = "" Then

                o_dtUsr = ValidarUsuario(Me.txtUsuario.Text, Me.txtPassword.Text)
                If o_dtUsr Is Nothing Then
                    LimpiarForm()
                    Me.txtUsuario.Focus()
                    MessageBox.Show("El usuario ingresado es incorrecto. Intente nuevamente.-", _
                                ".:: USUARIO INVALIDO ::.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    frmVentas.ol_dtUsr = o_dtUsr
                    LimpiarForm()
                    Me.Hide()
                    frmVentas.Show()
                End If
            Else
                Me.txtUsuario.Text = ""
                Me.txtPassword.Text = ""
                Me.txtUsuario.Focus()
            End If

        Catch ex As Exception
            Funciones.Manejador_Errores("OK_Click", ex)
            LogError(ex, "OK_Click", Me.txtUsuario.Text)
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        If MsgBox("¿Está seguro que desea cerrar la Aplicación?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, ".:: CIERRE DE APLICACIÓN ::.") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call OK_Click(sender, e)
        End If
    End Sub

    Private Sub txtUsuario_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsuario.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.txtPassword.Focus()
        End If
    End Sub

#End Region


End Class