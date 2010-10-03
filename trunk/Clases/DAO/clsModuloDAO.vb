Imports System.Data.SqlClient

Public Class clsModuloDAO

    Public Shared Function InsertaModulo(ByVal modulo As clsModulo) As Integer
        Dim strSQL As String = "STR_NUEVO_MODULO"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim salida As Integer

        Try
            cn = clsConexion.Conectar

            With cmd
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .Add("@MOD_ID", SqlDbType.Int).Direction = ParameterDirection.Output
                    .AddWithValue("@MOD_NOMBRE", modulo.Nombre)
                    .AddWithValue("@MOD_DESCRIPCION", modulo.Descripcion)
                    .AddWithValue("@MOD_TIPO", modulo.Tipo)
                    .AddWithValue("@MOD_URL", modulo.Url)
                    .AddWithValue("@MOD_ORDEN", modulo.Orden)
                    .AddWithValue("@MOD_IDPADRE", modulo.IdPadre)
                    .AddWithValue("@MOD_ESTADO", modulo.Estado)
                End With
                .ExecuteNonQuery()
            End With
            salida = cmd.Parameters.Item("@MOD_ID").Value

            clsConexion.Desconectar(cn)

        Catch ex As Exception
            Manejador_Errores("Inserta Modulo", ex)
            salida = -1
        Finally
            cmd = Nothing
            cn = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function ModificaModulo(ByVal modulo As clsModulo) As Integer
        Dim strSQL As String = "STR_MODIFICA_MODULO"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim salida As Integer

        Try
            cn = clsConexion.Conectar

            With cmd
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .AddWithValue("@MOD_ID", modulo.ID)
                    .AddWithValue("@MOD_NOMBRE", modulo.Nombre)
                    .AddWithValue("@MOD_DESCRIPCION", modulo.Descripcion)
                    .AddWithValue("@MOD_TIPO", modulo.Tipo)
                    .AddWithValue("@MOD_URL", modulo.Url)
                    .AddWithValue("@MOD_ORDEN", modulo.Orden)
                    .AddWithValue("@MOD_IDPADRE", modulo.IdPadre)
                    .AddWithValue("@MOD_ESTADO", modulo.Estado)
                End With
                .ExecuteNonQuery()
            End With
            salida = cmd.Parameters.Item("@MOD_ID").Value

            clsConexion.Desconectar(cn)

        Catch ex As Exception
            Manejador_Errores("Modifica Modulo", ex)
            salida = -1
        Finally
            cmd = Nothing
            cn = Nothing
        End Try
        Return salida
    End Function

    Public Shared Function getModulo(ByVal MOD_ID As Integer, ByVal MOD_NOMBRE As String, _
                                    ByVal MOD_DESCRIPCION As String, ByVal MOD_TIPO As String, _
                                    ByVal MOD_URL As String, ByVal MOD_ORDEN As Integer, _
                                    ByVal MOD_IDPADRE As Integer, ByVal MOD_ESTADO As Integer) As DataTable
        Dim strSQL As String = "STR_CONSULTA_MODULO"
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim salida As New DataTable

        Try
            cn = clsConexion.Conectar

            With cmd
                .Connection = cn
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSQL
                With .Parameters
                    .AddWithValue("@MOD_ID", MOD_ID)
                    .AddWithValue("@MOD_NOMBRE", MOD_NOMBRE)
                    .AddWithValue("@MOD_DESCRIPCION", MOD_DESCRIPCION)
                    .AddWithValue("@MOD_TIPO", MOD_TIPO)
                    .AddWithValue("@MOD_URL", MOD_URL)
                    .AddWithValue("@MOD_ORDEN", MOD_ORDEN)
                    .AddWithValue("@MOD_IDPADRE", MOD_IDPADRE)
                    .AddWithValue("@MOD_ESTADO", MOD_ESTADO)
                End With
                salida.Load(.ExecuteReader())
            End With

            clsConexion.Desconectar(cn)

        Catch ex As Exception
            Manejador_Errores("Modifica Modulo", ex)
            salida = Nothing
        Finally
            cmd = Nothing
            cn = Nothing
        End Try
        Return salida
    End Function
End Class
