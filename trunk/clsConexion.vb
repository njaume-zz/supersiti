﻿Imports System.Data.SqlClient

Public Class clsConexion

    Private _base As String
    Private _server As String
    Private _usuario As String
    Private _password As String

    Public Shared Function Conectar() As SqlClient.SqlConnection
        Dim cadenaConexion As String
        cadenaConexion = "Data Source=PICHITOS-PC\SQLEXPRESS;Initial Catalog=DES_SUPER;Integrated Security=True"
        Dim cn As SqlConnection = New SqlConnection(cadenaConexion)
        cn.Open()
        Return cn
    End Function

    Public Shared Sub Desconectar(ByRef cn As SqlClient.SqlConnection)
        cn.Close()
    End Sub

End Class
