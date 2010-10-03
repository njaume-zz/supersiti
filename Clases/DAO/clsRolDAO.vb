Imports System.Data.SqlClient

Public Class clsRolDAO


    Public Shared Function InsertaRol(ByVal rol As clsRol) As Integer
        Dim strSQL As String = "STR_NUEVO_ROL"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim tran As SqlTransaction
        Dim salida As Integer

        Try
            cn = clsConexion.Conectar
            tran = cn.BeginTransaction
            With cmd
                .Connection = cn
                .Transaction = tran
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .Add("@ROL_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@ROL_NOMBRE", rol.Nombre)
                    .AddWithValue("@ROL_ESTADO", rol.Estado)
                End With
                .ExecuteNonQuery()
            End With
            salida = cmd.Parameters.Item("@ROL_ID").Value
            If Not salida = -1 Then

                Dim cmdMod As SqlCommand
                Dim strMod As String = "STR_NUEVO_ROL_MODULO"

                'Recorro los módulos 
                For Each _mod As clsModulo In rol.Modulo
                    cmdMod = New SqlCommand
                    With cmdMod
                        .Connection = cn
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = strMod
                        With .Parameters
                            .Add("@ROM_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                            .AddWithValue("@ROL_ID", salida)
                            .AddWithValue("@MOD_ID", _mod.ID)
                        End With
                        .ExecuteNonQuery()
                    End With
                    cmdMod = Nothing
                Next
                tran.Commit()
            Else
                tran.Rollback()
            End If
            clsConexion.Desconectar(cn)

        Catch ex As Exception
            Manejador_Errores("Inserta Rol", ex)
            salida = -1
            tran.Rollback()
        Finally
            tran = Nothing
            cmd = Nothing
            cn = Nothing
        End Try
        Return salida
    End Function


    Public Shared Function ModificaRol(ByVal rol As clsRol) As Integer
        Dim strSQL As String = "STR_MODIFICA_ROL"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim salida As Integer
        Dim tran As SqlTransaction

        Try
            cn = clsConexion.Conectar
            tran = cn.BeginTransaction
            With cmd
                .Connection = cn
                .Transaction = tran
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .AddWithValue("@ROL_ID", rol.ID)
                    .AddWithValue("@ROL_NOMBRE", rol.Nombre)
                    .AddWithValue("@ROL_ESTADO", rol.Estado)
                End With
                .ExecuteNonQuery()
            End With
            salida = cmd.Parameters.Item("@ROL_ID").Value

            If Not salida = -1 Then
                '=========== VER BIEN ESTA IMPLEMENTACION ===============

                'Dim cmdMod As SqlCommand
                'Dim strMod As String = "STR_MODIFICA_ROL_MODULO"

                ''Recorro los módulos 
                'For Each _mod As clsModulo In rol.Modulo
                '    cmdMod = New SqlCommand
                '    With cmdMod
                '        .Connection = cn
                '        .CommandType = CommandType.StoredProcedure
                '        .CommandText = strMod
                '        With .Parameters
                '            .AddWithValue("@ROM_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                '            .AddWithValue("@ROL_ID", salida)
                '            .AddWithValue("@MOD_ID", _mod.ID)
                '        End With
                '        .ExecuteNonQuery()
                '    End With
                '    cmdMod = Nothing
                'Next
                tran.Commit()
            Else
                tran.Rollback()
            End If
            clsConexion.Desconectar(cn)

        Catch ex As Exception
            Manejador_Errores("Modifica Rol", ex)
            salida = -1
            tran.Rollback()
        Finally
            tran = Nothing
            cmd = Nothing
            cn = Nothing
        End Try
        Return salida
    End Function

End Class
