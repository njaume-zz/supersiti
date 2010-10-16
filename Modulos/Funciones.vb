Module Funciones

    Public Sub LogError(ByVal pex As Exception, ByVal pDescripcion As String, _
                              ByVal pUsuario As String)
        Dim oDocumento As System.Xml.XmlDocument
        Dim oNodo1 As System.Xml.XmlNode
        Dim oNodo2 As System.Xml.XmlNode

        Try
            oDocumento = New System.Xml.XmlDocument
            oDocumento.Load(My.Application.Info.DirectoryPath & "\log.xml")
            oNodo1 = oDocumento.CreateNode(Xml.XmlNodeType.Element, "Evento", "")
            oDocumento.SelectSingleNode("Eventos").AppendChild(oNodo1)
            oNodo2 = oDocumento.CreateNode(Xml.XmlNodeType.Element, "Excepcion", "")
            oNodo2.InnerText = pex.Message
            oNodo2 = oDocumento.CreateNode(Xml.XmlNodeType.Element, "Ubicacion", "")
            oNodo2.InnerText = pDescripcion
            oNodo2 = oDocumento.CreateNode(Xml.XmlNodeType.Element, "Fecha", "")
            oNodo2.InnerText = Now.ToString("dd/MM/yyyy HH:mm:ss")
            oNodo2 = oDocumento.CreateNode(Xml.XmlNodeType.Element, "Usuario", "")
            oNodo2.InnerText = Now.ToString(pUsuario)
            oDocumento.SelectSingleNode("Eventos").LastChild.AppendChild(oNodo2)
            oDocumento.Save(My.Application.Info.DirectoryPath & "\log.xml")
        Catch ex As Exception
            Throw ex
        Finally
            oDocumento = Nothing
            oNodo1 = Nothing
            oNodo2 = Nothing
        End Try

    End Sub

    Public Sub Manejador_Errores(ByVal Lugar As String, ByVal ex As Exception)
        MsgBox("Se ha producido un error en: " & Lugar & vbCrLf & _
               "Descripción del error: " & ex.ToString & vbCrLf, MsgBoxStyle.Critical, "ERROR CRITICO")
        'Grabar log de errores
    End Sub

    Public Function ValorNumerico(ByVal _char As Char) As String
        Dim str As String
        If (Asc(_char) < 48 Or Asc(_char) > 57) And Asc(_char) <> 8 Then
            _char = ChrW(Asc(0))
        End If
        str = _char
        Return str
    End Function

    Public Function ValidarFecha(ByVal fecha As Date) As Boolean

    End Function

    Public Function NombrePC() As String
        NombrePC = My.Computer.Name
    End Function
End Module
