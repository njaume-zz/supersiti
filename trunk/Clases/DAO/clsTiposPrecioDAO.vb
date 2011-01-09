imports System.data
imports System.Data.sqlClient

Public Class clsTiposPrecioDAO

    Public Shared Function getTipoPrecio(ByVal TPR_ID As Integer, ByVal TPR_CODIGO As String, _
                                  ByVal TPR_NOMBRE As String, ByVal TPR_DESCRIPCION As String) As DataTable
        Dim strSql As String = "STR_CONSULTA_C_TIPOS_PRECIOS"
        Dim Conexion As New SqlConnection
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As New DataTable

        Try
            Conexion = clsConexion.Conectar
            conecto = True
        Catch ex As Exception
            conecto = False
        End Try

        Try
            If conecto Then
                With cmd
                    .Connection = Conexion
                    .CommandText = strSql
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .AddWithValue("@TPR_ID", TPR_ID)
                        .AddWithValue("@TPR_CODIGO", TPR_CODIGO)
                        .AddWithValue("@TPR_NOMBRE", TPR_NOMBRE)
                        .AddWithValue("@TPR_DESCRIPCION", TPR_DESCRIPCION)
                        .AddWithValue("@TPR_ESTADO", 1)
                    End With
                    salida.Load(.ExecuteReader)
                End With
            Else
                salida = Nothing
            End If
            Conexion.Close()
        Catch ex As Exception
            salida = Nothing
            '            MsgBox(ex.Message)
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function
End Class
