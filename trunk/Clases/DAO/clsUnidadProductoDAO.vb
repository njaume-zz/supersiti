Imports System.Data
Imports System.Data.SqlClient

Public Class clsUnidadProductoDAO

    Public Shared Function getUnidad(ByVal UNI_ID As Integer, ByVal UNI_NOMBRE As String, _
                                     ByVal UNI_SIGLA As String) As DataTable
        Dim strSql As String = "STR_CONSULTA_UNIDAD"
        Dim Conexion As SqlConnection '= New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
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
                        .AddWithValue("@UNI_ID", UNI_ID)
                        .AddWithValue("@UNI_NOMBRE", UNI_NOMBRE)
                        .AddWithValue("@UNI_SIGLA", UNI_SIGLA)
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
