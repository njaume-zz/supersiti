' **** Capa de datos generada automáticamente por GenexPichita
' **** Maximiliano Adad - Carolina Di Bert **** 

Imports System.Data.SqlClient
Imports System.Data
Public Class clsTipoComprobanteDAO 

Public Shared Function Insertar(ByVal pTipoComprobante As clsTipoComprobante) As Integer
	Dim strStore As String = "STR_NUEVO_TipoComprobante"
	Dim oCn As SqlConnection 
	Dim oCmd As New SqlCommand 
	Dim iSalida As Integer
        Dim conecto As Boolean
        'Seleccionar modo de conexion
        Try

            Try
                oCn = clsConexion.Conectar
                conecto = True
            Catch ex As Exception
                conecto = False
            End Try
            With oCmd
                .Connection = oCn
                .CommandText = "STR_NUEVO_TipoComprobante"
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@CTC_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@CTC_CODIGO", pTipoComprobante.CTC_CODIGO)
                    .AddWithValue("@CTC_DESCRIPCION", pTipoComprobante.CTC_DESCRIPCION)
                    .AddWithValue("@CTC_SIGLA", pTipoComprobante.CTC_SIGLA)
                    .AddWithValue("@CTC_LETRA", pTipoComprobante.CTC_LETRA)
                    .AddWithValue("@CTC_SIGNO", pTipoComprobante.CTC_SIGNO)

                End With
                .ExecuteNonQuery()
            End With
            iSalida = oCmd.Parameters.Item("@CTC_ID").Value
            clsConexion.Desconectar(oCn)
        Catch ex As Exception
            iSalida = -1
        Finally
            oCmd = Nothing
            oCn = Nothing
        End Try
	Return iSalida

End Function
' **** Capa de datos generada automáticamente por GenexPichita
' **** Maximiliano Adad - Carolina Di Bert **** 

Public Shared Function Modificar(ByVal pTipoComprobante As clsTipoComprobante) As Integer
	Dim strStore As String = "STR_MODIFICA_TipoComprobante"
	Dim oCn As SqlConnection 
	Dim oCmd As New SqlCommand 
	Dim iSalida As Integer
        Dim conecto As Boolean
        'Seleccionar modo de conexion
        Try

            Try
                oCn = clsConexion.Conectar
                conecto = True
            Catch ex As Exception
                conecto = False
            End Try
            With oCmd
                .Connection = oCn
                .CommandText = "STR_MODIFICA_TipoComprobante"
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@CTC_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@CTC_CODIGO", pTipoComprobante.CTC_CODIGO)
                    .AddWithValue("@CTC_DESCRIPCION", pTipoComprobante.CTC_DESCRIPCION)
                    .AddWithValue("@CTC_SIGLA", pTipoComprobante.CTC_SIGLA)
                    .AddWithValue("@CTC_LETRA", pTipoComprobante.CTC_LETRA)
                    .AddWithValue("@CTC_SIGNO", pTipoComprobante.CTC_SIGNO)

                End With
                .ExecuteNonQuery()
            End With
            iSalida = oCmd.Parameters.Item("@CTC_ID").Value
            clsConexion.Desconectar(oCn)
        Catch ex As Exception
            iSalida = -1
        Finally
            oCmd = Nothing
            oCn = Nothing
        End Try
	Return iSalida

End Function
' **** Capa de datos generada automáticamente por GenexPichita
' **** Maximiliano Adad - Carolina Di Bert **** 

    Public Shared Function GetTable(ByVal pTipoComprobante As clsTipoComprobante) As DataTable
        Dim strStore As String = "STR_CONSULTA_V_TIPO_COMPROBANTE"
        Dim oCn As SqlConnection
        Dim oCmd As New SqlCommand
        Dim iSalida As Integer
        Dim conecto As Boolean
        Dim oDt As DataTable
        'Seleccionar modo de conexion
        Try

            Try
                oCn = clsConexion.Conectar
                conecto = True
            Catch ex As Exception
                conecto = False
            End Try

            If conecto Then
                oDt = New DataTable
                With oCmd
                    .Connection = oCn
                    .CommandText = strStore
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .AddWithValue("@CTC_ID", pTipoComprobante.CTC_ID)
                        .AddWithValue("@CTC_CODIGO", pTipoComprobante.CTC_CODIGO)
                        .AddWithValue("@CTC_DESCRIPCION", pTipoComprobante.CTC_DESCRIPCION)
                        .AddWithValue("@CTC_SIGLA", pTipoComprobante.CTC_SIGLA)
                        .AddWithValue("@CTC_LETRA", pTipoComprobante.CTC_LETRA)
                        .AddWithValue("@CTC_SIGNO", pTipoComprobante.CTC_SIGNO)
                        .AddWithValue("@CTC_UltimoNro", pTipoComprobante.CTC_UltimoNro)
                    End With
                    oDt.Load(.ExecuteReader)
                End With
                clsConexion.Desconectar(oCn)

            End If

        Catch ex As Exception
            iSalida = -1
        Finally
            oCmd = Nothing
            oCn = Nothing
            'oDt = Nothing
        End Try
        Return oDt
    End Function
End Class
