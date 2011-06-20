' **** Capa de datos generada automáticamente por GenexPichita
' **** Maximiliano Adad - Carolina Di Bert **** 

Imports System.Data.SqlClient
Imports System.Data

Namespace CapaNegocio

    Public Class clsFormaPagoDAO

        Public Shared Function Insertar(ByVal pFormaPago As clsFormaPago) As Integer
            Dim strStore As String = "STR_NUEVO_FormaPago"
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
                    .CommandText = "STR_NUEVO_FormaPago"
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .Add("@FOP_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                        .AddWithValue("@FOP_NOMBRE", pFormaPago.FOP_NOMBRE)
                        .AddWithValue("@FOP_DESCRIPCION", pFormaPago.FOP_DESCRIPCION)
                        .AddWithValue("@FOP_SIGLA", pFormaPago.FOP_SIGLA)
                        .AddWithValue("@FOP_ESTADO", pFormaPago.FOP_ESTADO)
                        .AddWithValue("@FOP_FECHAALTA", pFormaPago.FOP_FECHAALTA)

                    End With
                    .ExecuteNonQuery()
                End With
                iSalida = oCmd.Parameters.Item("@FOP_ID").Value
                clsConexion.Desconectar(oCn)
            Catch ex As Exception
                iSalida = -1
            Finally
                oCmd = Nothing
                oCn = Nothing
            End Try
            Return iSalida

        End Function

        Public Shared Function Modificar(ByVal pFormaPago As clsFormaPago) As Integer
            Dim strStore As String = "STR_MODIFICA_FormaPago"
            Dim oCn As SqlConnection
            Dim oCmd As New SqlCommand
            Dim iSalida As Integer
            Dim conecto As Boolean
            Try

                Try
                    oCn = clsConexion.Conectar
                    conecto = True
                Catch ex As Exception
                    conecto = False
                End Try

                With oCmd
                    .Connection = oCn
                    .CommandText = "STR_MODIFICA_FormaPago"
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .Add("@FOP_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                        .AddWithValue("@FOP_NOMBRE", pFormaPago.FOP_NOMBRE)
                        .AddWithValue("@FOP_DESCRIPCION", pFormaPago.FOP_DESCRIPCION)
                        .AddWithValue("@FOP_SIGLA", pFormaPago.FOP_SIGLA)
                        .AddWithValue("@FOP_ESTADO", pFormaPago.FOP_ESTADO)
                        .AddWithValue("@FOP_FECHAALTA", pFormaPago.FOP_FECHAALTA)

                    End With
                    .ExecuteNonQuery()
                End With
                iSalida = oCmd.Parameters.Item("@FOP_ID").Value
                clsConexion.Desconectar(oCn)
            Catch ex As Exception
                iSalida = -1
            Finally
                oCmd = Nothing
                oCn = Nothing
            End Try
            Return iSalida

        End Function

        Public Shared Function GetTable(ByVal pFormaPago As clsFormaPago) As DataTable
            Dim strStore As String = "STR_CONSULTA_FORMA_PAGO"
            Dim oCn As SqlConnection
            Dim oCmd As New SqlCommand
            Dim oSalida As DataTable
            Dim conecto As Boolean

            Try
                oSalida = New DataTable

                Try
                    oCn = clsConexion.Conectar
                    conecto = True
                Catch ex As Exception
                    conecto = False
                End Try

                With oCmd
                    .Connection = oCn
                    .CommandText = strStore
                    .CommandType = CommandType.StoredProcedure
                    With .Parameters
                        .AddWithValue("@FOP_ID", pFormaPago.FOP_ID)
                        .AddWithValue("@FOP_NOMBRE", pFormaPago.FOP_NOMBRE)
                        .AddWithValue("@FOP_SIGLA", pFormaPago.FOP_SIGLA)
                    End With
                    oSalida.Load(.ExecuteReader())
                End With
                clsConexion.Desconectar(oCn)
                Return oSalida
            Catch ex As Exception
                oSalida = Nothing
            Finally
                oCmd = Nothing
                oCn = Nothing
                oSalida = Nothing
            End Try

        End Function
    End Class

End Namespace