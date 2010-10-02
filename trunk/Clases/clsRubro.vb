﻿Public Class clsRubro

    Private _id As Integer
    Private _codigo As String
    Private _nombre As String
    Private _descripcion As String
    Private _estado As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal codigo As String, ByVal nombre As String, _
                    ByVal descripcion As String, ByVal estado As String)
        _id = id
        _codigo = codigo
        _nombre = nombre
        _descripcion = descripcion
        _estado = estado
    End Sub

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return _codigo
        End Get
        Set(ByVal value As String)
            _codigo = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _descripcion
        End Get
        Set(ByVal value As String)
            _descripcion = value
        End Set
    End Property

    Public Property Estado() As Integer
        Get
            Return _estado
        End Get
        Set(ByVal value As Integer)
            _estado = value
        End Set
    End Property

End Class
