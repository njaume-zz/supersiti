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

    'Public Function ValorNumerico(ByVal _char As Char) As String
    '    Dim str As String
    '    If (Asc(_char) < 48 Or Asc(_char) > 57) And Asc(_char) <> 8 Then
    '        _char = Trim(ChrW(Asc(0)))
    '    End If
    '    str = _char
    '    Return str
    'End Function

    Public Function buscar(ByVal txtval As String, ByVal car As Char) As Boolean
        Dim b As Integer
        For b = 1 To txtval.Length
            If Convert.ToChar(Mid(txtval, b, 1)) = car Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Function ValidaNumero(ByVal txt As TextBox, ByVal e As KeyPressEventArgs) As String
        Dim x As Char = e.KeyChar
        If x >= "0" And x <= "9" Then 'numero
            e.Handled = False
        Else
            If x = Convert.ToChar(13) Then 'enter
                e.Handled = True
            Else
                If x = Convert.ToChar(8) Then 'backspace
                    e.Handled = False
                Else
                    If buscar(txt.Text, x) = True And x = "." Then 'punto
                        e.Handled = False
                    Else
                        If txt.Text.Length = 0 And (x = "+" Or x = "-") Then 'suma o resta
                            e.Handled = False
                        Else
                            e.Handled = True
                        End If
                    End If
                End If
            End If
        End If
        If e.Handled Then
            ValidaNumero = txt.Text
        Else
            ValidaNumero = "0.00"
        End If
    End Function

    Public Function ValidarFecha(ByVal fecha As String) As String
        Dim dia As String
        Dim mes As String
        Dim anio As String

        dia = Left(fecha, 2)
        mes = Mid(fecha, 3, 4)
        anio = Right(fecha, 4)
        ValidarFecha = dia & "/" & mes & "/" & anio

        Return ValidarFecha
    End Function

    Public Function NombrePC() As String
        NombrePC = My.Computer.Name
    End Function

    Public Function ObtieneUsuario() As String
        Dim str As String
        
        Try
            str = frmVentas.TSSUsuario.Text

        Catch ex As Exception
            str = ""
            Manejador_Errores("Obtener Usuario", ex)
        End Try
        Return str
    End Function


    ''' <summary>
    ''' Controla el ingreso de caracter distinto de comilla simple o doble.
    ''' </summary>
    ''' <param name="pcKeyChar">Caracter a validar</param>
    ''' <returns>Caracter validado o cadena vacia si es comilla simple o doble</returns>
    ''' <remarks></remarks>
    Public Function ControlarCadena(ByVal pcKeyChar As Char) As String
        Try
            If (Asc(pcKeyChar) <> 34 And Asc(pcKeyChar) <> 39) Then
                Return pcKeyChar
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    ''' <summary>
    ''' Controla el ingreso de caracter numérico.
    ''' </summary>
    ''' <param name="pcKeyChar">Caracter a validar</param>
    ''' <returns>Caracter validado o cadena vacia si no es numérico</returns>
    ''' <remarks></remarks>
    Public Function ControlarEnteroPositivo(ByVal pcKeyChar As Char) As String
        Try
            If (Asc(pcKeyChar) > 47 And Asc(pcKeyChar) < 59) Or Asc(pcKeyChar) = 8 Then
                Return pcKeyChar
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Public Function ValidaNumerico(ByVal _char As Char) As Boolean
        Dim digitos() As Char = "0|1|2|3|4|5|6|7|8|9|.|,|-|\b"

        Return Array.IndexOf(digitos, _char) <> -1

    End Function
    ''' <summary>
    ''' Guarda un atributo de configuración de la aplicación dentro
    ''' del archivo de configuración.
    ''' </summary>
    ''' <param name="pszValor">Valor a asignar al atributo</param>
    ''' <param name="pszAtributo">Atributo</param>
    ''' <remarks></remarks>
    'Public Sub GuardarConfiguracion(ByVal pszValor As String, _
    '                                ByVal pszAtributo As String)
    '    Dim oDom As Xml.XmlDocument
    '    Dim oFSO As System.IO.FileInfo

    '    Try
    '        oFSO = New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\" & cXMLConfig)
    '        If (oFSO.Exists) Then
    '            oDom = New Xml.XmlDocument
    '            oDom.Load(My.Application.Info.DirectoryPath & "\" & cXMLConfig)

    '            'Verifico si el nodo existe y le actualizo el valor.
    '            If Not (oDom.SelectSingleNode(pszAtributo) Is Nothing) Then
    '                oDom.SelectSingleNode(pszAtributo).InnerText = pszValor
    '            Else
    '                MsgBox("No se encuentra el nodo que se está especificando (" & pszAtributo & ").", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Advertencia")
    '            End If

    '            'Grabo los cambios en el archivo de configuración.
    '            oDom.Save(gszPath & cXMLConfig)
    '        Else
    '            MsgBox("No se encuentra el archivo de configuración en la carpeta requerida (" & gszPath & cXMLConfig & ").", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Advertencia")
    '            Application.Exit()
    '        End If
    '    Catch ex As Exception
    '        Throw (ex)
    '    Finally
    '        oDom = Nothing
    '        oFSO = Nothing
    '    End Try
    'End Sub

    ''' <summary>
    ''' Genera un nuevo dataset, con la estructura del original y los datos que 
    ''' cumplen con el filtro indicado.
    ''' </summary>
    ''' <param name="poDs">DataSet a filtrar</param>
    ''' <param name="pszTabla">Nombre de la tabla en la que se filtraran los datos</param>
    ''' <param name="pszFiltro">Cadena que contiene el filtro a aplicar</param>
    ''' <returns>DataSet filtrado</returns>
    ''' <remarks></remarks>
    'Public Function FiltrarDts(ByVal poDs As DataSet, ByVal pszTabla As String, ByVal pszFiltro As String) As DataSet
    '    Dim oDs As DataSet

    '    Try
    '        oDs = New DataSet

    '        oDs.Merge(poDs.Tables(pszTabla).Select(pszFiltro))

    '        FiltrarDts = oDs
    '    Catch ex As Exception
    '        Throw (ex)
    '    Finally
    '        oDs = Nothing
    '    End Try
    'End Function

    ''' <summary>
    ''' Recupera un atributo de configuración de la aplicación dentro
    ''' del archivo de configuración.
    ''' </summary>
    ''' <param name="pszAtributo">Atributo</param>
    ''' <returns>Valor del atributo</returns>
    ''' <remarks></remarks>
    Public Function ObtenerConfiguracion(ByVal pszAtributo As String) As String
        Dim oDom As Xml.XmlDocument
        Dim oNodes As Xml.XmlNodeList
        Dim oFSO As System.IO.FileInfo
        Dim szValor As String = ""

        Try
            'Abro el archivo de configuración.
            oFSO = New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\" & cXMLConfig)
            If (oFSO.Exists) Then
                oDom = New Xml.XmlDocument
                oDom.Load(My.Application.Info.DirectoryPath & "\" & cXMLConfig)
                oNodes = oDom.SelectNodes("/Configuracion/" & pszAtributo)
                'Verifico si el nodo existe.
                If (oDom.SelectSingleNode("/Configuracion").LastChild.Name = pszAtributo) Then
                    szValor = Trim$(oNodes.Item("value").Value)
                    szValor = Trim$(oDom.SelectSingleNode("/Configuracion/" & pszAtributo).InnerXml)
                Else
                    MsgBox("No se encuentra el nodo que se está especificando (" & pszAtributo & ").", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Advertencia")
                End If
            Else
                MsgBox("No se encuentra el archivo de configuración en la carpeta requerida (" & gszPath & cXMLConfig & ").", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Advertencia")
                Application.Exit()
            End If

            ObtenerConfiguracion = szValor
        Catch ex As Exception
            Throw (ex)
        Finally
            oDom = Nothing
            oFSO = Nothing
        End Try
    End Function


    Public Function ObtenerConfiguracionDS(ByVal pszAtributo As String) As String
        Dim oFSO As System.IO.FileInfo
        Dim szValor As String = ""
        Dim ds As DataSet
        Try
            'Abro el archivo de configuración.
            oFSO = New System.IO.FileInfo(My.Application.Info.DirectoryPath & "\" & cXMLConfig)
            If (oFSO.Exists) Then
                ds = New DataSet
                ds.ReadXml(oFSO.FullName)
                'Tomo el valor de la conexion
                If ds.Tables(1).Rows(0).Item(1) Is Nothing Then
                    MsgBox("No se encontró la sección indicada en el archivo de configuración.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, ". : : ADVERTENCIA : : .")
                    Application.Exit()
                Else
                    szValor = ds.Tables(1).Rows(0).Item(1)
                End If

            Else
                MsgBox("No se encuentra el archivo de configuración en la carpeta requerida (" & gszPath & cXMLConfig & ").", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Advertencia")
                Application.Exit()
            End If

            ObtenerConfiguracionDS = szValor
        Catch ex As Exception
            Throw (ex)
        Finally
            oFSO = Nothing
        End Try
    End Function
End Module
