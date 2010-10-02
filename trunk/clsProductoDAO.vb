Imports System.Data.SqlClient
Imports System.Data


Public Class clsProductoDAO

    Public Shared Function getProducto(ByVal PRO_ID As Integer, ByVal PRO_NOMBRE As String, _
                                       ByVal PRO_CODIGO As String, ByVal PRO_MARCA As String, _
                                       ByVal RUB_ID As Integer, ByVal FAM_ID As Integer) As DataTable
        Dim strSql As String = "STR_CONSULTA_PRODUCTO"
        Dim Conexion As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
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
                        .AddWithValue("@PRO_ID", PRO_ID)
                        .AddWithValue("@PRO_NOMBRE", PRO_NOMBRE)
                        .AddWithValue("@PRO_CODIGO", PRO_CODIGO)
                        .AddWithValue("@PRO_MARCA", PRO_MARCA)
                        .AddWithValue("@RUB_ID", RUB_ID)
                        .AddWithValue("@FAM_ID", FAM_ID)
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

    Public Shared Function InsertaProducto(ByVal producto As clsProducto) As Integer
        Dim strSql As String = "STR_NUEVO_PRODUCTO"
        Dim Conexion As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
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
                        .Add("@PRO_ID", SqlDbType.Int).Direction = ParameterDirection.InputOutput
                        .AddWithValue("@PRO_CODIGO", producto.Codigo)
                        .AddWithValue("@PRO_NOMBRE", producto.Nombre)
                        .AddWithValue("@PRO_DESCRIPCION", producto.Descripcion)
                        .AddWithValue("@PRO_MARCA", producto.Marca)
                        .AddWithValue("@PRO_PRECIOCOSTO", producto.PcioCompra)
                        .AddWithValue("@PRO_PRECIOCOMPRA", producto.PcioCosto)
                        .AddWithValue("@PRO_IVA ", producto.Iva)
                        .AddWithValue("@PRO_IMPUESTOINTERNO", producto.ImpuestoInterno)
                        .AddWithValue("@PRO_IDPADRE", producto.IdPadre)
                        .AddWithValue("@PRO_MINIMO", producto.Minimo)
                        .AddWithValue("@PRO_MAXIMO", producto.Maximo)
                        .AddWithValue("@PRO_ALERTA", producto.Alerta)
                        .AddWithValue("@UNC_ID", producto.Unic_Id)
                        .AddWithValue("@UNV_ID", producto.Univ_Id)
                        .AddWithValue("@RUB_ID", producto.Rub_Id)
                        .AddWithValue("@FAM_ID", producto.Fam_Id)
                        .AddWithValue("@PRO_ESTADO", producto.Estado)
                    End With
                    .ExecuteNonQuery()
                End With
                salida = cmd.Parameters.Item("@PRO_ID").Value
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

    End Function

    Public Shared Function ModificaProducto(ByVal producto As clsProducto) As Integer
        Dim strSql As String = "STR_MODIFICA_PRODUCTO"
        Dim Conexion As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
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
                        .AddWithValue("@PRO_ID", producto.ID)
                        .AddWithValue("@PRO_CODIGO", producto.Codigo)
                        .AddWithValue("@PRO_NOMBRE", producto.Nombre)
                        .AddWithValue("@PRO_DESCRIPCION", producto.Descripcion)
                        .AddWithValue("@PRO_MARCA", producto.Marca)
                        .AddWithValue("@PRO_PRECIOCOSTO", producto.PcioCompra)
                        .AddWithValue("@PRO_PRECIOCOMPRA", producto.PcioCosto)
                        .AddWithValue("@PRO_IVA ", producto.Iva)
                        .AddWithValue("@PRO_IMPUESTOINTERNO", producto.ImpuestoInterno)
                        .AddWithValue("@PRO_IDPADRE", producto.IdPadre)
                        .AddWithValue("@PRO_MINIMO", producto.Minimo)
                        .AddWithValue("@PRO_MAXIMO", producto.Maximo)
                        .AddWithValue("@PRO_ALERTA", producto.Alerta)
                        .AddWithValue("@UNC_ID", producto.Unic_Id)
                        .AddWithValue("@UNV_ID", producto.Univ_Id)
                        .AddWithValue("@RUB_ID", producto.Rub_Id)
                        .AddWithValue("@FAM_ID", producto.Fam_Id)
                        .AddWithValue("@PRO_ESTADO", producto.Estado)
                        .Add("@SALIDA", SqlDbType.Int).Direction = ParameterDirection.Output
                    End With
                    .ExecuteNonQuery()
                End With
                salida = cmd.Parameters.Item("@SALIDA").Value
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

    End Function


    Public Shared Function getMarcas(ByVal PRO_MARCA As String) As DataTable
        Dim strSql As String = "STR_CONSULTA_PRODUCTO_MARCA"
        Dim Conexion As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion2").ConnectionString)
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
                        .AddWithValue("@PRO_MARCA", PRO_MARCA)
                    End With
                    salida.Load(.ExecuteReader())
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


    'Public Shared Function InsertaProducto(ByVal PRO_ID As Integer, ByVal PRO_NOMBRE As String, _
    '                               ByVal PRO_CODIGO As String, ByVal PRO_MARCA As String, _
    '                               ByVal RUB_ID As Integer, ByVal FAM_ID As Integer) As Integer

    '    Dim strSql As String = "STR_NUEVO_PRODUCTO"
    '    Dim Conexion As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion").ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim conecto As Boolean
    '    Dim salida As Integer

    '    Try
    '        Conexion.Open()
    '        conecto = True
    '    Catch ex As Exception
    '        conecto = False
    '    End Try

    '    Try
    '        If conecto Then
    '            With cmd
    '                .Connection = Conexion
    '                .CommandText = strSql
    '                .CommandType = CommandType.StoredProcedure
    '                With .Parameters
    '                    .AddWithValue("@PRO_ID", PRO_ID)
    '                    .AddWithValue("@PRO_NOMBRE", PRO_NOMBRE)
    '                    .AddWithValue("@PRO_CODIGO", PRO_CODIGO)
    '                    .AddWithValue("@PRO_MARCA", PRO_MARCA)
    '                    .AddWithValue("@RUB_ID", RUB_ID)
    '                    .AddWithValue("@FAM_ID", FAM_ID)
    '                End With
    '                .ExecuteNonQuery()
    '            End With
    '        Else
    '            salida = -1
    '        End If
    '    Catch ex As Exception
    '        salida = -1
    '    End Try

    'End Function

    'Public Shared Function ModificaProducto(ByVal PRO_ID As Integer, ByVal PRO_NOMBRE As String, _
    '                               ByVal PRO_CODIGO As String, ByVal PRO_MARCA As String, _
    '                               ByVal RUB_ID As Integer, ByVal FAM_ID As Integer) As Integer
    '    Dim strSql As String = "STR_MODIFICA_PRODUCTO"
    '    Dim Conexion As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("sqlConexion").ConnectionString)
    '    Dim cmd As New SqlCommand
    '    Dim conecto As Boolean
    '    Dim salida As Integer

    '    Try
    '        Conexion.Open()
    '        conecto = True
    '    Catch ex As Exception
    '        conecto = False
    '    End Try

    '    Try
    '        If conecto Then
    '            With cmd
    '                .Connection = Conexion
    '                .CommandText = strSql
    '                .CommandType = CommandType.StoredProcedure
    '                With .Parameters
    '                    .AddWithValue("")
    '                    .AddWithValue("")
    '                    .AddWithValue("")
    '                    .AddWithValue("")
    '                    .AddWithValue("")
    '                    .AddWithValue("")
    '                End With
    '                .ExecuteNonQuery()
    '            End With
    '        Else
    '            salida = -1
    '        End If
    '    Catch ex As Exception
    '        salida = -1
    '    End Try

    'End Function

End Class
