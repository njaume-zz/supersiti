Imports System.Globalization
Imports System.Threading
Imports CapaDatos.CapaDatos
Imports System.Configuration

Module Funciones

    Public Sub InicializarConfiguracion()
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-AR")
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
    End Sub

    Public Sub LogError(ByVal pex As Exception, ByVal pDescripcion As String, _
                              ByVal pUsuario As String)
        Dim oDocumento As System.Xml.XmlDocument
        Dim oNodo1 As System.Xml.XmlNode
        Dim oNodo2 As System.Xml.XmlNode
        Dim oFSO As System.IO.FileInfo
        Dim szRuta As String
        Try
            oDocumento = New System.Xml.XmlDocument
            szRuta = System.IO.Path.Combine(My.Application.Info.DirectoryPath, "log.xml")
            oFSO = New System.IO.FileInfo(szRuta)
            If Not (oFSO.Exists) Then
                CrearLogVacio()
            End If
            oDocumento.Load(szRuta)
            oNodo1 = oDocumento.CreateElement("Eventos")
            oDocumento.SelectSingleNode("XMLErrores").AppendChild(oNodo1)

            oNodo1 = oDocumento.CreateElement("Excepcion")
            oNodo1.InnerText = pex.Message.ToString
            oDocumento.SelectSingleNode("XMLErrores/Eventos").AppendChild(oNodo1)

            oNodo1 = oDocumento.CreateElement("Ubicacion")
            oNodo1.InnerText = pDescripcion
            oDocumento.SelectSingleNode("XMLErrores/Eventos").AppendChild(oNodo1)

            oNodo1 = oDocumento.CreateElement("Fecha")
            oNodo1.InnerText = Now.ToString("dd/MM/yyyy HH:mm:ss")
            oDocumento.SelectSingleNode("XMLErrores/Eventos").AppendChild(oNodo1)

            oNodo1 = oDocumento.CreateElement("Usuario")
            oNodo1.InnerText = Now.ToString(pUsuario)
            oDocumento.SelectSingleNode("XMLErrores/Eventos").AppendChild(oNodo1)

            oDocumento.SelectSingleNode("XMLErrores").AppendChild(oNodo1)
            oDocumento.Save(My.Application.Info.DirectoryPath & "\log.xml")

        Catch ex As Exception
            'Throw ex
        Finally
            oDocumento = Nothing
            oNodo1 = Nothing
            oNodo2 = Nothing
        End Try

    End Sub

    Private Sub CrearLogVacio()
        Dim strXML As String
        Dim oXML As New Xml.XmlDocument
        'Dim oElemento As Xml.XmlElement

        strXML = "<XMLErrores>" & vbNewLine & _
                 "  <Eventos>" & vbNewLine & _
                 "      <Excepcion></Excepcion>" & vbNewLine & _
                 "      <Ubicacion></Ubicacion>" & vbNewLine & _
                 "      <Fecha></Fecha>" & vbNewLine & _
                 "      <Usuario></Usuario>" & vbNewLine & _
                 "  </Eventos>" & vbNewLine & _
                 "</XMLErrores>"
        oXML.LoadXml(strXML)
        oXML.Save(System.IO.Path.Combine(My.Application.Info.DirectoryPath, "log.xml"))
    End Sub

    Public Sub Manejador_Errores(ByVal Lugar As String, ByVal ex As Exception)
        MsgBox("Se ha producido un error en: " & Lugar & vbCrLf & _
               "Descripción del error: " & ex.ToString & vbCrLf, MsgBoxStyle.Critical, "ERROR CRITICO")
        'Grabar log de errores
    End Sub

    Public Function AutorizarMovimientos(ByVal pszUsuario As String, ByVal pszPass As String) As Boolean
        Dim oDt = New DataTable

        oDt = frmVentas.ol_dtUsr
        If oDt Is Nothing Then oDt = clsUsuarioDAO.ValidaUsuario(pszUsuario, pszPass)

        oDt.Select("ROL_NOMBRE = 'Admnistrador' OR ROL_NOMBRE = 'Supervisor'")
        If oDt.Rows.Count > 0 Then
            AutorizarMovimientos = True
        Else
            AutorizarMovimientos = False
        End If
    End Function
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

    Public Function ObtenerPuntoVenta() As String
        Dim str As String
        Try
            str = frmVentas.TSSPtoVta.Text
        Catch ex As Exception
            str = ""
            Manejador_Errores("Obtener Punto de Venta", ex)
        End Try
        Return str
    End Function

    ''' <summary>
    ''' Convierto a decimal, tomando la configuración local y 
    ''' poder determinar el separador decimal para una
    ''' correcta conversion
    ''' </summary>
    ''' <param name="pszValor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertirDecimal(ByVal pszValor As String) As Decimal
        Dim oSeparadorDecimal As Char
        Dim oSeparadorStr As Char
        Dim wdSalida As Decimal
        Dim wiIndexComa, wiIndexPunto As Integer
        'Obtenego el separador decimal del sistema
        oSeparadorDecimal = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator()
        'Obtengo el separador decimal del string recuperado
        'Se debe tener en cuenta que se cuentan 2 posiciones (Ej: "34." ), ya que se
        'quitó el signo negativo (-)
        'pszValor = Replace(pszValor, "-", "")
        wiIndexComa = pszValor.ToString.IndexOf(",")
        wiIndexPunto = pszValor.ToString.IndexOf(".")
        If pszValor <> 0 Then
            If wiIndexComa = -1 Then
                If wiIndexPunto = -1 Then
                    oSeparadorStr = pszValor.ToString.Substring(0, 0)
                Else
                    oSeparadorStr = pszValor.ToString.Substring(wiIndexPunto, 1)
                End If
            Else
                oSeparadorStr = pszValor.ToString.Substring(wiIndexComa, 1)
            End If
        End If
        wdSalida = Convert.ToDecimal(Replace(pszValor, oSeparadorStr, oSeparadorDecimal))

        Return wdSalida
    End Function

    ''' <summary>
    ''' Función que convierte un String a Formato de Moneda. Previamente se debe convertir
    ''' a decimal para que acepte los decimales despues de la coma.
    ''' </summary>
    ''' <param name="pstrValor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FormatoMoneda(ByVal pstrValor As String) As String
        pstrValor = ConvertirDecimal(CDbl(pstrValor))
        FormatoMoneda = Microsoft.VisualBasic.Mid(FormatCurrency(pstrValor, 2), 3)
    End Function

    ''' <summary>
    ''' Función que permite formatear el número de comprobante para visualizarlo correctamente.
    ''' </summary>
    ''' <param name="pstrValor">string</param>
    ''' <returns>string</returns>
    ''' <remarks>madad</remarks>
    Public Function FormatoNroComprobante(ByVal pstrValor As String) As String
        Select Case Len(pstrValor)
            Case 1
                FormatoNroComprobante = "0000000" & pstrValor
            Case 2
                FormatoNroComprobante = "000000" & pstrValor
            Case 3
                FormatoNroComprobante = "00000" & pstrValor
            Case 4
                FormatoNroComprobante = "0000" & pstrValor
            Case 5
                FormatoNroComprobante = "000" & pstrValor
            Case 6
                FormatoNroComprobante = "00" & pstrValor
            Case 7
                FormatoNroComprobante = "0" & pstrValor
            Case Else
                FormatoNroComprobante = pstrValor
        End Select
    End Function

    Public Function CompletaCeros(ByVal pszValor As String, ByVal piCantidad As Integer) As String
        Dim i As Integer
        Dim wszCompletar As String = ""

        For i = 1 To piCantidad - pszValor.Length
            wszCompletar = wszCompletar & "0"
        Next

        CompletaCeros = wszCompletar & pszValor.Replace(".", "").Replace(",", "")
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

    Public Function ValidaDecimal(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Text As TextBox) As Integer

        Dim dig As Integer = Len(Text.Text & e.KeyChar)
        Dim a, esDecimal, NumDecimales As Integer
        Dim esDec As Boolean
        ' se verifica si es un digito o un punto 
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
            Return a
        Else
            e.Handled = True
        End If
        ' se verifica que el primer digito ingresado no sea un punto al seleccionar
        If Text.SelectedText <> "" Then
            If e.KeyChar = "." Then
                e.Handled = True
                Return a
            End If
        End If
        If dig = 1 And e.KeyChar = "." Then
            e.Handled = True
            Return a
        End If
        'aqui se hace la verificacion cuando es seleccionado el valor del texto
        'y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        If Text.SelectedText = "" Then
            ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
            For a = 0 To dig - 1
                Dim car As String = CStr(Text.Text & e.KeyChar)
                If car.Substring(a, 1) = "." Then
                    esDecimal = esDecimal + 1
                    esDec = True
                End If
                If esDec = True Then
                    NumDecimales = NumDecimales + 1
                End If
                ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
                If NumDecimales >= 4 Or esDecimal >= 2 Then
                    e.Handled = True
                End If
            Next
        End If
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
        Dim szRuta As String
        Try

            szRuta = System.IO.Path.Combine(My.Settings.XMLConfig, cXMLConfig)
            'Abro el archivo de configuración.
            oFSO = New System.IO.FileInfo(szRuta)
            If (oFSO.Exists) Then
                oDom = New System.Xml.XmlDocument
                oDom.Load(szRuta)

                'Verifico si el nodo existe.
                If Not (oDom.SelectSingleNode("/Configuracion/" & pszAtributo) Is Nothing) Then
                    szValor = Trim$(oDom.SelectSingleNode("/Configuracion/" & pszAtributo).InnerText)
                Else
                    MsgBox("No se encuentra el nodo que se está especificando (" & pszAtributo & ").", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Advertencia")
                    ObtenerConfiguracion = Nothing
                    Exit Function
                End If
            Else
                MsgBox("No se encuentra el archivo de configuración en la carpeta requerida (" & szRuta & cXMLConfig & ").", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Advertencia")
                'Application.Exit()
                ObtenerConfiguracion = Nothing
                Exit Function
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
