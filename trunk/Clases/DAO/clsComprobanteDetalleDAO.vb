' **** Capa de datos generada automáticamente por GenexPichita
' **** Maximiliano Adad - Carolina Di Bert **** 

Imports System.Data.SqlClient
Imports System.Data
Public Class clsComprobanteDetalleDAO

    Public Shared Function Insertar(ByVal pComprobanteDetalle As clsComprobanteDetalle) As Integer
        Dim strStore As String = "STR_NUEVO_ComprobanteDetalle"
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
                .CommandText = "STR_NUEVO_ComprobanteDetalle"
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@COD_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@COD_PROCODIGO", pComprobanteDetalle.COD_PROCODIGO)
                    .AddWithValue("@COD_PRONOMBRE", pComprobanteDetalle.COD_PRONOMBRE)
                    .AddWithValue("@COD_PROPCIOUNITARIO", pComprobanteDetalle.COD_PROPCIOUNITARIO)
                    .AddWithValue("@COD_PROCANTIDAD", pComprobanteDetalle.COD_PROCANTIDAD)
                    .AddWithValue("@COD_PRECIOCANTIDAD", pComprobanteDetalle.COD_PRECIOCANTIDAD)
                    .AddWithValue("@COD_IVA", pComprobanteDetalle.COD_IVA)
                    .AddWithValue("@COM_ID", pComprobanteDetalle.COM_ID)
                    .AddWithValue("@COD_PESABLE", pComprobanteDetalle.COD_PESABLE)
                End With
                .ExecuteNonQuery()
            End With
            iSalida = oCmd.Parameters.Item("@COM_ID").Value
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

    Public Shared Function Modificar(ByVal pComprobanteDetalle As clsComprobanteDetalle) As Integer
        Dim strStore As String = "STR_MODIFICA_ComprobanteDetalle"
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
                .CommandText = "STR_MODIFICA_ComprobanteDetalle"
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@COD_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@COD_PROCODIGO", pComprobanteDetalle.COD_PROCODIGO)
                    .AddWithValue("@COD_PRONOMBRE", pComprobanteDetalle.COD_PRONOMBRE)
                    .AddWithValue("@COD_PROPCIOUNITARIO", pComprobanteDetalle.COD_PROPCIOUNITARIO)
                    .AddWithValue("@COD_PROCANTIDAD", pComprobanteDetalle.COD_PROCANTIDAD)
                    .AddWithValue("@COD_PRECIOCANTIDAD", pComprobanteDetalle.COD_PRECIOCANTIDAD)
                    .AddWithValue("@COD_IVA", pComprobanteDetalle.COD_IVA)
                    .AddWithValue("@COM_ID", pComprobanteDetalle.COM_ID)
                    .AddWithValue("@COD_PESABLE", pComprobanteDetalle.COD_PESABLE)
                End With
                .ExecuteNonQuery()
            End With
            iSalida = oCmd.Parameters.Item("@COM_ID").Value
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

    Public Shared Function GetTable(ByVal pComprobanteDetalle As clsComprobanteDetalle) As Integer
        Dim strStore As String = "STR_CONSULTA_ComprobanteDetalle"
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
                .CommandText = "STR_CONSULTA_ComprobanteDetalle"
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@COD_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@COD_PROCODIGO", pComprobanteDetalle.COD_PROCODIGO)
                    .AddWithValue("@COD_PRONOMBRE", pComprobanteDetalle.COD_PRONOMBRE)
                    .AddWithValue("@COD_PROPCIOUNITARIO", pComprobanteDetalle.COD_PROPCIOUNITARIO)
                    .AddWithValue("@COD_PROCANTIDAD", pComprobanteDetalle.COD_PROCANTIDAD)
                    .AddWithValue("@COD_PRECIOCANTIDAD", pComprobanteDetalle.COD_PRECIOCANTIDAD)
                    .AddWithValue("@COD_IVA", pComprobanteDetalle.COD_IVA)
                    .AddWithValue("@COM_ID", pComprobanteDetalle.COM_ID)
                    .AddWithValue("@COD_PESABLE", pComprobanteDetalle.COD_PESABLE)
                End With
                .ExecuteNonQuery()
            End With
            iSalida = oCmd.Parameters.Item("@COM_ID").Value
            clsConexion.Desconectar(oCn)
        Catch ex As Exception
            iSalida = -1
        Finally
            oCmd = Nothing
            oCn = Nothing
        End Try
        Return iSalida
    End Function
End Class
