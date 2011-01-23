Imports System.Data.SqlClient

Public Class clsUsuarioDAO

    Public Shared Function InsertaUsuario(ByVal usuario As clsUsuario) As Integer
        Dim salida As Integer
        Dim strSQL As String = "STR_NUEVO_USUARIO"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        '        Dim tran As SqlTransaction

        Try
            cn = clsConexion.Conectar
            'tran = cn.BeginTransaction

            With cmd
                .Connection = cn
                '.Transaction = tran
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .Add("@USU_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@USU_NOMBRE", usuario.Nombre)
                    .AddWithValue("@USU_PASSWORD", usuario.Password)
                    .AddWithValue("@USU_MAIL", usuario.Mail)
                    .AddWithValue("@USU_ESTADO", usuario.Estado)
                    .AddWithValue("@ROL_ID", usuario.Rol)
                End With
                .ExecuteNonQuery()
            End With
            salida = cmd.Parameters.Item("@USU_ID").Value

            If Not salida = -1 Then
                ' tran.Commit()
            End If
            clsConexion.Desconectar(cn)
        Catch ex As Exception
            Manejador_Errores("Inserta Usuario", ex)
            'tran.Rollback()
        Finally
            'tran = Nothing
            cmd = Nothing
            cn = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function ModificaUsuario(ByVal usuario As clsUsuario) As Integer
        Dim salida As Integer
        Dim strSQL As String = "STR_MODIFICA_USUARIO"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        'Dim tran As SqlTransaction

        Try
            cn = clsConexion.Conectar
            'tran = cn.BeginTransaction

            With cmd
                .Connection = cn
                '.Transaction = tran
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .AddWithValue("@USU_ID", usuario.ID)
                    .AddWithValue("@USU_NOMBRE", usuario.Nombre)
                    .AddWithValue("@USU_PASSWORD", usuario.Password)
                    .AddWithValue("@USU_MAIL", usuario.Mail)
                    .AddWithValue("@USU_ESTADO", usuario.Estado)
                    .AddWithValue("@ROL_ID", usuario.Rol)
                End With
                .ExecuteNonQuery()
            End With
            salida = cmd.Parameters.Item("@USU_ID").Value

            If Not salida = -1 Then
                'tran.Commit()
            End If
            clsConexion.Desconectar(cn)
        Catch ex As Exception
            Manejador_Errores("Modifica Usuario", ex)
            'tran.Rollback()
        Finally
            'tran = Nothing
            cmd = Nothing
            cn = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function getUsuario(ByVal USU_ID As Integer, ByVal USU_NOMBRE As String, _
                                      ByVal USU_MAIL As String, ByVal ROL_ID As Integer) As DataTable
        Dim strSQL As String = "STR_CONSULTA_USUARIO"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dt As DataTable

        Try
            cn = clsConexion.Conectar
            
            With cmd
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .AddWithValue("@USU_ID", USU_ID)
                    .AddWithValue("@USU_NOMBRE", USU_NOMBRE)
                    .AddWithValue("@USU_MAIL", USU_MAIL)
                    .AddWithValue("@ROL_ID", ROL_ID)
                End With
                dt = New DataTable
                dt.Load(.ExecuteReader)
            End With

            If dt.Rows.Count > 0 Then
                dt = Nothing
            End If
            clsConexion.Desconectar(cn)
        Catch ex As Exception
            Manejador_Errores("Consulta Usuario", ex)
            dt = Nothing
        Finally
            cmd = Nothing
            cn = Nothing
        End Try
        Return dt
    End Function

    Public Shared Function ValidaUsuario(ByVal USU_NOMBRE As String, _
                                         ByVal USU_PASSWORD As String) As DataTable
        Dim strSQL As String = "STR_VALIDA_INGRESO"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dt As DataTable

        Try
            cn = clsConexion.Conectar

            With cmd
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .AddWithValue("@USU_NOMBRE", USU_NOMBRE)
                    .AddWithValue("@USU_PASSWORD", USU_PASSWORD)
                End With
                dt = New DataTable
                dt.Load(.ExecuteReader)
            End With

            If Not dt.Rows.Count > 0 Then
                dt = Nothing
            End If
            clsConexion.Desconectar(cn)
        Catch ex As Exception
            Manejador_Errores("Valida Ingreso", ex)
            dt = Nothing
        Finally
            cmd = Nothing
            cn = Nothing
        End Try
        Return dt
    End Function

End Class
