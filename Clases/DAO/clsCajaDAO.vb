Imports System.Data.SqlClient

Public Class clsCajaDAO

    ''' <summary>
    ''' Función que inserta una caja nueva
    ''' </summary>
    ''' <param name="pCaja"></param>
    ''' <returns>entero que determina si tuvo éxito o no</returns>
    ''' <remarks></remarks>
    Public Shared Function InsertaCaja(ByVal pCaja As clsCaja) As Integer
        Dim strSql As String = "STR_NUEVO_CAJA"
        Dim Conexion As SqlConnection '= New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As Integer

        Try
            Conexion = clsConexion.Conectar() '.Open()
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

    ''' <summary>
    ''' Modificación de Caja
    ''' </summary>
    ''' <param name="pCaja"></param>
    ''' <returns>Entero que determina si modificó bien o no</returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Función parametrizada que tiene como fin recuperar los datos necesario
    ''' de la tabla de Cajas
    ''' </summary>
    ''' <param name="CAJ_ID"></param>
    ''' <param name="CAJ_NUMERO"></param>
    ''' <param name="CAJ_FECHAAPERTURA"></param>
    ''' <param name="CAJ_FECHACIERRE"></param>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Public Shared Function getCaja(ByVal CAJ_ID As Integer, ByVal CAJ_NUMERO As String, _
                                   ByVal CAJ_FECHAAPERTURA As Date, ByVal CAJ_FECHACIERRE As Date) As DataTable
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

    ''' <summary>
    ''' Función que devuelve los estados de la caja
    ''' </summary>
    ''' <param name="CAE_ID"></param>
    ''' <param name="CAE_NOMBRE"></param>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Lista las ACciones realizadas en las cajas, aperturas, retiro, etc.
    ''' </summary>
    ''' <param name="CAA_ID"></param>
    ''' <param name="CAA_FECHA"></param>
    ''' <param name="CAE_ID"></param>
    ''' <param name="USU_ID"></param>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
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

    Public Shared Function ValidaMovimientosCaja(ByVal cae_id As Integer, ByVal usu_id As Integer) As Boolean
        Dim strSql As String = "STR_VERIFICA_CAJA_MOVIMIENTOS"
        Dim Conexion As SqlConnection
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As Boolean

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
                        .AddWithValue("@CAE_ID", cae_id)
                        .AddWithValue("@USU_ID", usu_id)
                        .Add("@SALIDA", SqlDbType.Int).Direction = ParameterDirection.Output
                    End With
                    .ExecuteNonQuery()
                End With
                If cmd.Parameters("@SALIDA").Value = -1 Then
                    salida = False
                Else
                    salida = True
                End If
            Else
                salida = False
            End If
            Conexion.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd = Nothing
            Conexion = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function InsertaCajaMovimientos(ByVal pCajaMovimiento As clsCajaMovimientos)
        Dim strSql As String = "STR_NUEVO_CAJA_MOVIMIENTOS"
        Dim Conexion As SqlConnection
        Dim cmd As New SqlCommand
        Dim conecto As Boolean
        Dim salida As Integer

        Try
            Conexion = clsConexion.Conectar()
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
                        .Add("@CAM_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                        .AddWithValue("@CAM_IMPORTE", pCajaMovimiento.CAM_IMPORTE)
                        .AddWithValue("@CAM_FECHA", pCajaMovimiento.CAM_FECHA)
                        .AddWithValue("@CAE_ID", pCajaMovimiento.CAE_ID)
                        .AddWithValue("@USU_ID", pCajaMovimiento.USU_ID)
                        .AddWithValue("@CAM_ESTADO", pCajaMovimiento.CAM_ESTADO)
                    End With
                    .ExecuteNonQuery()
                End With
                salida = cmd.Parameters.Item("@CAM_ID").Value
                'If salida > 0 Then
                '    salida = 1
                'Else
                '    salida = -1
                'End If
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
