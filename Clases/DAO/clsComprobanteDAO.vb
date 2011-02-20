' **** Capa de datos generada automáticamente por GenexPichita
' **** Maximiliano Adad - Carolina Di Bert **** 

Imports System.Data.SqlClient
Imports System.Data
Public Class clsComprobanteDAO

    Public Shared Function Insertar(ByVal pComprobante As clsComprobante) As Integer
        Dim strStore As String = "STR_NUEVO_COMPROBANTE_VENTA"
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
                .CommandText = strStore
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@COM_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@COM_PTOVTA", pComprobante.COM_PTOVTA)
                    .AddWithValue("@COM_NROEMITIDO", pComprobante.COM_NROEMITIDO)
                    .AddWithValue("@COM_FECHA", pComprobante.COM_FECHA)
                    .AddWithValue("@COM_IMPORTEGRAVADO", pComprobante.COM_IMPORTEGRAVADO)
                    .AddWithValue("@COM_IMPORTENOGRAVADO", pComprobante.COM_IMPORTENOGRAVADO)
                    .AddWithValue("@COM_IVAFACTURADO", pComprobante.COM_IVAFACTURADO)
                    .AddWithValue("@COM_TOTALFACTRADO", pComprobante.COM_TOTALFACTRADO)
                    .AddWithValue("@COM_IMPRESO", pComprobante.COM_IMPRESO)
                    .AddWithValue("@COM_USUNOMBRE", pComprobante.COM_USUNOMBRE)
                    .AddWithValue("@COM_CLIRAZONSOCIAL", pComprobante.COM_CLIRAZONSOCIAL)
                    .AddWithValue("@COM_CLIDOMICILIO", pComprobante.COM_CLIDOMICILIO)
                    .AddWithValue("@COM_CLITELEFONO", pComprobante.COM_CLITELEFONO)
                    .AddWithValue("@COM_CLIDOMICILIOENTREGA", pComprobante.COM_CLIDOMICILIOENTREGA)
                    .AddWithValue("@COM_CLICUIT", pComprobante.COM_CLICUIT)
                    .AddWithValue("@COM_CLIINGRESOBRUTO", pComprobante.COM_CLIINGRESOBRUTO)
                    .AddWithValue("@COM_RAZONSOCIALPROPIO", pComprobante.COM_RAZONSOCIALPROPIO)
                    .AddWithValue("@COM_DOMICILIOPROPIO", pComprobante.COM_DOMICILIOPROPIO)
                    .AddWithValue("@COM_TELEFONOPROPIO", pComprobante.COM_TELEFONOPROPIO)
                    .AddWithValue("@COM_CUITPROPIO", pComprobante.COM_CUITPROPIO)
                    .AddWithValue("@COM_INGRESOBRUTOPROPIO", pComprobante.COM_INGRESOBRUTOPROPIO)
                    .AddWithValue("@CTC_ID", pComprobante.CTC_ID)
                    .AddWithValue("@CAJ_ID", pComprobante.CAJ_ID)
                    .AddWithValue("@FOP_ID", pComprobante.FOP_ID)

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
    ' **** Capa de datos generada automáticamente por GenexPichita
    ' **** Maximiliano Adad - Carolina Di Bert **** 

    Public Shared Function Modificar(ByVal pComprobante As clsComprobante) As Integer
        Dim strStore As String = "STR_MODIFICA_Comprobante"
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
                .CommandText = "STR_MODIFICA_Comprobante"
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@COM_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@COM_PTOVTA", pComprobante.COM_PTOVTA)
                    .AddWithValue("@COM_NROEMITIDO", pComprobante.COM_NROEMITIDO)
                    .AddWithValue("@COM_FECHA", pComprobante.COM_FECHA)
                    .AddWithValue("@COM_IMPORTEGRAVADO", pComprobante.COM_IMPORTEGRAVADO)
                    .AddWithValue("@COM_IMPORTENOGRAVADO", pComprobante.COM_IMPORTENOGRAVADO)
                    .AddWithValue("@COM_IVAFACTURADO", pComprobante.COM_IVAFACTURADO)
                    .AddWithValue("@COM_TOTALFACTRADO", pComprobante.COM_TOTALFACTRADO)
                    .AddWithValue("@COM_IMPRESO", pComprobante.COM_IMPRESO)
                    .AddWithValue("@COM_USUNOMBRE", pComprobante.COM_USUNOMBRE)
                    .AddWithValue("@COM_CLIRAZONSOCIAL", pComprobante.COM_CLIRAZONSOCIAL)
                    .AddWithValue("@COM_CLIDOMICILIO", pComprobante.COM_CLIDOMICILIO)
                    .AddWithValue("@COM_CLITELEFONO", pComprobante.COM_CLITELEFONO)
                    .AddWithValue("@COM_CLIDOMICILIOENTREGA", pComprobante.COM_CLIDOMICILIOENTREGA)
                    .AddWithValue("@COM_CLICUIT", pComprobante.COM_CLICUIT)
                    .AddWithValue("@COM_CLIINGRESOBRUTO", pComprobante.COM_CLIINGRESOBRUTO)
                    .AddWithValue("@COM_RAZONSOCIALPROPIO", pComprobante.COM_RAZONSOCIALPROPIO)
                    .AddWithValue("@COM_DOMICILIOPROPIO", pComprobante.COM_DOMICILIOPROPIO)
                    .AddWithValue("@COM_TELEFONOPROPIO", pComprobante.COM_TELEFONOPROPIO)
                    .AddWithValue("@COM_CUITPROPIO", pComprobante.COM_CUITPROPIO)
                    .AddWithValue("@COM_INGRESOBRUTOPROPIO", pComprobante.COM_INGRESOBRUTOPROPIO)
                    .AddWithValue("@CTC_ID", pComprobante.CTC_ID)
                    .AddWithValue("@CAJ_ID", pComprobante.CAJ_ID)
                    .AddWithValue("@FOP_ID", pComprobante.FOP_ID)

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
    ' **** Capa de datos generada automáticamente por GenexPichita
    ' **** Maximiliano Adad - Carolina Di Bert **** 

    Public Shared Function GetTable(ByVal pComprobante As clsComprobante) As Integer
        Dim strStore As String = "STR_CONSULTA_Comprobante"
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
                .CommandText = "STR_CONSULTA_Comprobante"
                .CommandType = CommandType.StoredProcedure
                With .Parameters
                    .Add("@COM_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@COM_PTOVTA", pComprobante.COM_PTOVTA)
                    .AddWithValue("@COM_NROEMITIDO", pComprobante.COM_NROEMITIDO)
                    .AddWithValue("@COM_FECHA", pComprobante.COM_FECHA)
                    .AddWithValue("@COM_IMPORTEGRAVADO", pComprobante.COM_IMPORTEGRAVADO)
                    .AddWithValue("@COM_IMPORTENOGRAVADO", pComprobante.COM_IMPORTENOGRAVADO)
                    .AddWithValue("@COM_IVAFACTURADO", pComprobante.COM_IVAFACTURADO)
                    .AddWithValue("@COM_TOTALFACTRADO", pComprobante.COM_TOTALFACTRADO)
                    .AddWithValue("@COM_IMPRESO", pComprobante.COM_IMPRESO)
                    .AddWithValue("@COM_USUNOMBRE", pComprobante.COM_USUNOMBRE)
                    .AddWithValue("@COM_CLIRAZONSOCIAL", pComprobante.COM_CLIRAZONSOCIAL)
                    .AddWithValue("@COM_CLIDOMICILIO", pComprobante.COM_CLIDOMICILIO)
                    .AddWithValue("@COM_CLITELEFONO", pComprobante.COM_CLITELEFONO)
                    .AddWithValue("@COM_CLIDOMICILIOENTREGA", pComprobante.COM_CLIDOMICILIOENTREGA)
                    .AddWithValue("@COM_CLICUIT", pComprobante.COM_CLICUIT)
                    .AddWithValue("@COM_CLIINGRESOBRUTO", pComprobante.COM_CLIINGRESOBRUTO)
                    .AddWithValue("@COM_RAZONSOCIALPROPIO", pComprobante.COM_RAZONSOCIALPROPIO)
                    .AddWithValue("@COM_DOMICILIOPROPIO", pComprobante.COM_DOMICILIOPROPIO)
                    .AddWithValue("@COM_TELEFONOPROPIO", pComprobante.COM_TELEFONOPROPIO)
                    .AddWithValue("@COM_CUITPROPIO", pComprobante.COM_CUITPROPIO)
                    .AddWithValue("@COM_INGRESOBRUTOPROPIO", pComprobante.COM_INGRESOBRUTOPROPIO)
                    .AddWithValue("@CTC_ID", pComprobante.CTC_ID)
                    .AddWithValue("@CAJ_ID", pComprobante.CAJ_ID)
                    .AddWithValue("@FOP_ID", pComprobante.FOP_ID)

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

    Public Shared Function ObtieneNroComprobante() As Integer
        Dim oDt As DataTable
        Dim strStore As String = "STR_CONSULTA_UltimoComprobante"
        Dim oCn As SqlConnection
        Dim oCmd As New SqlCommand
        Dim conecto As Boolean

        Try
            Try
                oCn = clsConexion.Conectar
                conecto = True
            Catch ex As Exception
                conecto = False
            End Try

            If conecto Then

                With oCmd
                    .Connection = oCn
                    .CommandText = strStore
                    .CommandType = CommandType.StoredProcedure
                    oDt.Load(.ExecuteReader)
                End With

                If oDt.Rows.Count > 0 Then
                    ObtieneNroComprobante = oDt.Rows(0).Item("COM_NroComprobnte") + 1
                Else
                    ObtieneNroComprobante = 0
                End If
            End If
        Catch ex As Exception
            Funciones.LogError(ex, "ObtieneNroComprobante", Funciones.ObtieneUsuario)
        Finally
            oDt = Nothing
        End Try
    End Function
End Class
