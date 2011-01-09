﻿Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class clsConexion

    Private _base As String
    Private _server As String
    Private _usuario As String
    Private _password As String

    Public Shared Function Conectar() As SqlClient.SqlConnection
        Dim cadenaConexion As String
        'cadenaConexion = "Data Source=PICHITOS-PC;Initial Catalog=DES_SUPER;Integrated Security=True"
        'cadenaConexion = "Data Source=LOSDIBERT-PC\SQLEXPRESS;Initial Catalog=DES_SUPER;Integrated Security=True"
        'cadenaConexion = "Data Source=PICHITOSDESK-PC\SQLEXPRESS;Initial Catalog=DES_SUPER;Integrated Security=True"
        cadenaConexion = ObtenerConfiguracion("Conexion")

        Dim cn As SqlConnection = New SqlConnection(cadenaConexion)
        cn.Open()
        Return cn
    End Function

    Public Shared Sub Desconectar(ByRef cn As SqlClient.SqlConnection)
        cn.Close()
    End Sub

End Class
