Imports System.Data.SqlClient
Imports System.Data

Public Class clsRubroDAO

    Public Shared Function getRubro(ByVal RUB_ID As Integer, ByVal RUB_NOMBRE As String) As DataTable
        Dim strSql As String = "STR_CONSULTA_RUBRO"
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
                        .AddWithValue("@RUB_ID", RUB_ID)
                        .AddWithValue("@RUB_NOMBRE", RUB_NOMBRE)
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

    Public Shared Function ModificaRubro(ByVal RUB_ID As Integer, ByVal RUB_NOMBRE As String, _
                                        ByVal RUB_DESCRIPCION As String, ByVal RUB_ESTADO As Integer) As Integer
        Dim strSql As String = "STR_MODIFICA_RUBRO"
        Dim Conexion As SqlConnection '= New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As Integer

        Try
            Conexion.Open()
            conecto = True
        Catch ex As Exception
            conecto = False
        End Try

        Try
            If conecto Then
                Dim dt As New DataTable
                With cmd
                    .Connection = Conexion
                    .CommandText = strSql
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .AddWithValue("@RUB_ID", RUB_ID)
                        .AddWithValue("@RUB_NOMBRE", RUB_NOMBRE)
                        .AddWithValue("@RUB_DESCRIPCION", RUB_DESCRIPCION)
                        .AddWithValue("@RUB_ESTADO", RUB_ESTADO)
                    End With
                    dt.Load(.ExecuteReader)
                End With
                If dt.Rows.Count > 0 Then
                    salida = 1
                Else
                    salida = -1
                End If

            Else
                salida = -1
            End If
            Conexion.Close()
        Catch ex As Exception
            salida = -1
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function InsertaRubro(ByVal RUB_ID As Integer, ByVal RUB_NOMBRE As String, _
                                        ByVal RUB_DESCRIPCION As String,ByVal RUB_ESTADO As Integer) As Integer
        Dim strSql As String = "STR_NUEVO_RUBRO"
        Dim Conexion As SqlConnection '= New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As Integer

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
                        .Add("@RUB_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                        .AddWithValue("@RUB_NOMBRE", RUB_NOMBRE)
                        .AddWithValue("@RUB_DESCRIPCION", RUB_DESCRIPCION)
                        .AddWithValue("@RUB_ESTADO", RUB_ESTADO)
                    End With
                    .ExecuteNonQuery()
                End With
                salida = cmd.Parameters.Item("@RUB_ID").Value
            Else
                salida = -1
            End If
            Conexion.Close()
        Catch ex As Exception
            salida = -1
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function
End Class
