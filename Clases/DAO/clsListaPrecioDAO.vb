imports System.Data
imports System.Data.SqlClient

Public Class clsListaPrecioDAO

    Public Shared Function getPrecioVenta(ByVal TPR_ID As Integer, ByVal PRO_ID As Integer) As System.Decimal
        Dim strSql As String = "STR_CONSULTA_C_LISTA_PRECIO"
        Dim Conexion As New SqlConnection
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As New DataTable
        Dim Retorno As System.Decimal

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
                        .AddWithValue("@LPR_ID", 0)
                        .AddWithValue("@PRO_ID", PRO_ID)
                        .AddWithValue("@TPR_ID", TPR_ID)
                    End With
                    salida.Load(.ExecuteReader)
                End With

                If salida Is Nothing Then
                    Retorno = 0.0
                Else
                    Retorno = Convert.ToDecimal(salida.Rows(0).Item("LPR_PRECIO"))
                End If
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
        Return Retorno
    End Function

End Class
