Imports System.Data.SqlClient
Imports System.Data

Public Class clsTasaIvaDAO

    Public Shared Function getTasaIva(ByVal IVA_ID As Integer, ByVal IVA_NOMBRE As String, _
                                   ByVal IVA_TASA As Double) As DataTable
        Dim strSql As String = "STR_CONSULTA_IMPUESTOIVA"
        Dim Conexion As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As New DataTable

        Try
            Conexion.Open()
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
                        .AddWithValue("@IVA_ID", IVA_ID)
                        .AddWithValue("@IVA_NOMBRE", IVA_NOMBRE)
                        .AddWithValue("@IVA_TASA", IVA_TASA)
                    End With
                    salida.Load(.ExecuteReader)
                End With
            Else
                salida = Nothing
            End If
            Conexion.Close()
        Catch ex As Exception
            salida = Nothing
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function

End Class
