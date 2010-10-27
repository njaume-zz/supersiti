Imports System.Data.SqlClient

Public Class clsCajaDAO

    Public Shared Function InsertaCaja(ByVal pCaja As clsCaja) As Integer
        Dim strSql As String = "STR_NUEVO_CAJA"
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
        'MsgBox(RUB_ID & "-" & FAM_DESCRIPCION & "-" & FAM_ESTADO & "-" & FAM_ID & "-" & FAM_NOMBRE)
        Try
            If conecto Then
                With cmd
                    .Connection = Conexion
                    .CommandText = strSql
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .AddWithValue("@CAJ_NUMERO", pCaja.Numero)
                        .AddWithValue("@CAE_ID", pCaja.CajaEstado)
                        .AddWithValue("@CAJ_FECHAAPERTURA", pCaja.FechaApertura)
                        .AddWithValue("@CAJ_FECHACIERRE", pCaja.FechaCierre)
                        .AddWithValue("@CAJ_ESTADO", pCaja.Estado)
                        .Add("@CAJ_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    End With
                    .ExecuteNonQuery()
                End With
                salida = cmd.Parameters.Item("@CAJ_ID").Value
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

    Public Shared Function ModificaCaja(ByVal pCaja As clsCaja) As Integer
        Dim strSql As String = "STR_MODIFICA_CAJA"
        Dim Conexion As SqlConnection '= New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As Integer
        Dim dt As DataTable
        Try
            Conexion.Open()
            conecto = True
        Catch ex As Exception
            conecto = False
        End Try
        'MsgBox(RUB_ID & "-" & FAM_DESCRIPCION & "-" & FAM_ESTADO & "-" & FAM_ID & "-" & FAM_NOMBRE)
        Try
            If conecto Then
                dt = New DataTable
                With cmd
                    .Connection = Conexion
                    .CommandText = strSql
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .AddWithValue("@CAJ_ID", pCaja.ID)
                        .AddWithValue("@CAJ_NUMERO", pCaja.Numero)
                        .AddWithValue("@CAE_ID", pCaja.CajaEstado)
                        .AddWithValue("@CAJ_FECHAAPERTURA", pCaja.FechaApertura)
                        .AddWithValue("@CAJ_FECHACIERRE", pCaja.FechaCierre)
                        .AddWithValue("@CAJ_ESTADO", pCaja.Estado)
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

    Public Shared Function getCaja(ByVal CAJ_ID As Integer, ByVal CAJ_NUMERO As String, _
                                   ByVal CAJ_FECHAAPERTURA As Date,ByVal CAJ_FECHACIERRE As Date) As DataTable
        Dim strSql As String = "STR_CONSULTA_CAJA"
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
                        .AddWithValue("@CAJ_ID", CAJ_ID)
                        .AddWithValue("@CAJ_NUMERO", CAJ_NUMERO)
                        .AddWithValue("@CAJ_FECHAAPERTURA", CAJ_FECHAAPERTURA)
                        .AddWithValue("@CAJ_FECHACIERRE", CAJ_FECHACIERRE)
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

    Public Shared Function getCajaEstado(ByVal CAE_ID As Integer, ByVal CAE_NOMBRE As String) As DataTable
        Dim strSql As String = "STR_CONSULTA_CAJA_ESTADO"
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
                        .AddWithValue("@CAE_ID", CAE_ID)
                        .AddWithValue("@CAE_NOMBRE", CAE_NOMBRE)
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

    Public Shared Function getCajaAcciones(ByVal CAA_ID As Integer, ByVal CAA_FECHA As Date, _
                                           ByVal CAE_ID As Integer, ByVal USU_ID As Integer) As DataTable
        Dim strSql As String = "STR_CONSULTA_CAJA_ACCIONES"
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
                        .AddWithValue("@CAA_ID", CAA_ID)
                        .AddWithValue("@CAA_FECHA", CAA_FECHA)
                        .AddWithValue("@CAE_ID", CAE_ID)
                        .AddWithValue("@USU_ID", USU_ID)
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
End Class
