Imports System.Data.SqlClient
Imports System.Data
Imports PV_Super.clsConexion

Public Class clsFamiliaDAO

    Public Shared Function getFamilia(ByVal FAM_ID As Integer, ByVal FAM_NOMBRE As String, _
                                      ByVal RUB_ID As Integer) As DataTable
        Dim strSql As String = "STR_CONSULTA_FAMILIA"
        Dim Conexion As SqlConnection '= New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As New DataTable

        Conexion = clsConexion.Conectar
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
                        .AddWithValue("@FAM_ID", FAM_ID)
                        .AddWithValue("@FAM_NOMBRE", FAM_NOMBRE)
                        .AddWithValue("@RUB_ID", RUB_ID)
                    End With
                    salida.Load(.ExecuteReader)
                End With
            Else
                salida = Nothing
            End If
            Conexion.Close()
        Catch ex As Exception
            salida = Nothing
            MsgBox(ex.Message)
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function ModificaFamilia(ByVal FAM_ID As Integer, ByVal FAM_NOMBRE As String, _
                                        ByVal FAM_DESCRIPCION As String, ByVal RUB_ID As Integer, _
                                        ByVal FAM_ESTADO As Integer) As Integer
        Dim strSql As String = "STR_MODIFICA_FAMILIA"
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
                        .AddWithValue("@FAM_ID", FAM_ID)
                        .AddWithValue("@FAM_NOMBRE", FAM_NOMBRE)
                        .AddWithValue("@FAM_DESCRIPCION", FAM_DESCRIPCION)
                        .AddWithValue("RUB_ID", RUB_ID)
                        .AddWithValue("@FAM_ESTADO", FAM_ESTADO)
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
            MsgBox(ex.Message)
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function InsertaFamilia(ByVal FAM_ID As Integer, ByVal FAM_NOMBRE As String, _
                                        ByVal FAM_DESCRIPCION As String, ByVal RUB_ID As Integer, _
                                        ByVal FAM_ESTADO As Integer) As Integer
        Dim strSql As String = "STR_NUEVO_FAMILIA"
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
        MsgBox(RUB_ID & "-" & FAM_DESCRIPCION & "-" & FAM_ESTADO & "-" & FAM_ID & "-" & FAM_NOMBRE)
        Try
            If conecto Then
                With cmd
                    .Connection = Conexion
                    .CommandText = strSql
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .AddWithValue("@FAM_NOMBRE", FAM_NOMBRE)
                        .AddWithValue("@FAM_DESCRIPCION", FAM_DESCRIPCION)
                        .AddWithValue("@RUB_ID", RUB_ID)
                        .AddWithValue("@FAM_ESTADO", FAM_ESTADO)
                        .Add("@FAM_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    End With
                    .ExecuteNonQuery()
                End With
                salida = cmd.Parameters.Item("@FAM_ID").Value
            Else
                salida = -1
            End If
            Conexion.Close()
        Catch ex As Exception
            salida = -1
            MsgBox(ex.Message)
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function
End Class
