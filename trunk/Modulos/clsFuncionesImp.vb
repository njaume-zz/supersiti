Public Class clsFuncionesImp
    Public Enum eTipoBase
        SQL = 0
        msAcces = 1
        MySQL = 2
    End Enum
    Private Structure tBase
    Nombre As String
    Tipo As eTipoBase
    Catalogo As String
    DataSource As String
    User As String
    Pass As String
    End Structure
    Private Structure tConfiguracion
    AuditarStock As Boolean
    SolicitarClaveModificacionDirecta As Boolean
    End Structure

    Private Structure tCopias
    RutaArchivoBase As String
    RutaDirectorioArchivoBase As String
    RutaDirectorioCopias As String
    End Structure

    Public Structure tUsuario
    idOperador As Long
    Detalle As String
    Descuento As Double
    Vendedor As Boolean
    Administrador As Boolean
    Programador As Boolean
    Otro    As Boolean
    PermiteCambioPrecio As Boolean
    PermiteListaDistribuidor As Boolean
    CajaHabilitada As String
    End Structure

    Private Structure tSistema
    Ruta As String
    RutaV As String
    RutaEnRed As String
    Base As tBase
    Configuracion As tConfiguracion
    ArchivoDeConfiguracionDelSistema As String
    Copias As tCopias
    Usuario As tUsuario
    End Structure
    Public SistemaPV As tSistemapv
    Public Sistema As tSistema
    'Public objError As Errores.clsError
    Public boolEsVenta As Boolean
    Public Const cFDinero = "#####0.00"

    Public Structure tParametro
    Id As String
    Valor As String
    End Structure
    Public Structure tParametros
    parametros() As String
    Cantidad As Integer
    item() As tParametro
    End Structure

    Public strRuta As String

    Public Enum eImpresionModos
        ModoWindows = 0
        ModoDOS = 1
        Tickeadora = 2
    End Enum
    Public Enum eImpresora
        EpsonTM = 0
        HASAR = 1
    End Enum

    Private Structure tDatosPV
    Detalle As String
    CUIT As String
    Caja As Long
    PtoVta  As Long
    SoloPresupuesto As Long
    End Structure

    Private Structure tRutas
    Sistema  As String      ' -> Ruta del Sistema
    Impresora As String     ' -> Puerto de la impresora de precios
    Copias As String        ' -> Ruta a donde descargara la copia
    End Structure
    Private Structure tFiscal
    Modelo As eImpresora
    Puerto As Integer
    Tipo As Integer
    PuntoDeVenta As Integer
    Utiliza As Boolean
    Cajon As Boolean
    End Structure
    Private Structure tImpresionConfig
    Modo As eImpresionModos
    Impresora As String
    End Structure
Private Type tImpresionComprobantes
    ImpresoraDefault As String
    MovimientoInterno As tImpresionConfig
    Comprobanteventa As tImpresionConfig
    Comprobanteventacopia As tImpresionConfig
    Caja As tImpresionConfig
    ForzarImpresionBasica As Boolean
End Type
Private Type tConf
    sinRed As Boolean       ' -> PV sin Servidor
    reintentaRed As Boolean ' -> si reintenta conexión con servidor
    terminal As Boolean     ' -> PC terminal de PV
    PideVendedor As Boolean ' -> PC terminal de PV
    PideHora As Boolean
    ValidacionPorTarjeta As Boolean
    CodigoEsId As Boolean
    Impresion As tImpresionComprobantes
    Pedidos As Boolean
End Type
Private Type tSistemapv
    Usuario As tUsuario     ' -> Tipo de Usuario-Vendedor
    Configuracion As tConf  ' -> Configuraciones
    DLL As String
    Rutas As tRutas         ' -> Rutas del Sistema
    ImpFiscal As tFiscal
    datosPV As tDatosPV
End Type

    'Public Sistema As tSistema
    'Public objError As Errores.clsError
    'Public objMensajes As ObjetosDeInterfaz.clsMensaje

    'Public Type tParametro
    '    Id As String
    '    Valor As String
    'End Type

    'Public Type tParametros
    '    parametros() As String
    '    Cantidad As Integer
    '    item() As tParametro
    'End Type


    Public Sub Main()
        Dim boolNoCerrar As Boolean
        On Error GoTo e
        ' Inicializacion de Variables
        If App.PrevInstance Then
            MsgBox("La Aplicación ya está en Abierta" & Chr(13) & "Se cerrará esta instancia", vbCritical, "Aplicación en Uso")
            End
        End If
        With SistemaPV
            .Configuracion.sinRed = False
            .DLL = "MarketSis_Pv"
            .ImpFiscal.Modelo = eImpresora.HASAR
            .ImpFiscal.Puerto = 1
            .Rutas.Sistema = App.Path & "\"
        End With
        objMensajes = New ObjetosDeInterfaz.clsMensaje
        Sistema_Configuracion()
        Error_Configurar()
        DatosDelSistema()

        If SistemaPV.Configuracion.PideHora Then frmSistema_FechaYHora.Show(vbModal)
        Logueo()

        SistemaPV.ImpFiscal.PuntoDeVenta = mdlFiscal.ObtenerPtoVta()

        boolNoCerrar = False
        If SistemaPV.Usuario.Vendedor Or SistemaPV.Usuario.Otro Then
            '  - Ver restricciones de fechas, tickets, etc
            Error_Configurar()
            Articulos_Refrescar()
            If Ventas_DiaInicializado Then
                boolNoCerrar = True
                Caja_Habilitada(True)
                frmVentas.Show()
            Else
                If MsgBox("Desea Realizar el Cierre del Día", vbQuestion + vbYesNo, "Cierre el Dia") = vbYes Then
                    frmCajas_CierreDiario.Show(vbModal)
                    If Ventas_DiaInicializado Then
                        boolNoCerrar = True
                        frmVentas.Show()
                    Else
                        MsgBox("No se ha realizado el Cierre Diario" & Chr(13) & "Consulte con su proveedor de sistemas", vbCritical, "Atención")
                    End If
                End If
            End If
        End If
        If Not boolNoCerrar Then Sistema_Cerrar()

        Exit Sub
e:

        MostrarError()
        MatarObjeto(objError)
        MatarObjeto(objMensajes)
        Sistema_Cerrar()
    End Sub

    '----------------------------------------------------------
    ' Articulos_refrescar :  Funcion publica
    ' - Traer Imagen [PV_Servidor -> PV]
    ' - Refrescar Formulario de Grilla (con Articulos de PV)
    '----------------------------------------------------------
    Public Function Articulos_Refrescar() As Boolean
        Dim objArticulos As clsArticulosC
        Dim objConsulta As clsConsultas
        Dim objInformes As Informes.clsInformes
        Dim rstArticulos As ADODB.Recordset
        Dim v As Integer, i As Integer
        On Error GoTo e

        'Screen.ActiveForm.Refresh
        objMensajes.Mensaje("Artículos", "Estableciendo Comunicación")
        objMensajes.Mostrar()
        objArticulos = New clsArticulosC
        If Not (objArticulos Is Nothing) Then
            objMensajes.Mensaje("Artículos", "Cargando Articulos")

            rstArticulos = objArticulos.Articulos
            MatarObjeto(objArticulos)

            ' -- URGENTE A COMPONENTE
            objConsulta = New clsConsultas
            objConsulta.Consultas_Almacenadas("EXEC SPD_CAJAS_ACTUALIZACION_FINALIZADA " & SistemaPV.datosPV.Caja & ", 1", "", "")
            MatarObjeto(objConsulta)

            objMensajes.Mensaje("Artículos", "Configurando Pantalla")
            With frmArticulos_Grilla
                .AdoGrilla.Recordset = rstArticulos
                .mGrilla.DataSource = .AdoGrilla
                With .mGrilla
                    .Tag = "Grilla Consulta Articulos para Facturación"
                    i = 4
                    For v = .FixedCols To .Cols - 1
                        If InStr(1, LCase(.ColKey(v)), "|lista_") > 0 Then
                            .ColFormat(v) = cFDinero
                            .TextMatrix(0, v) = Split(Split(.ColKey(v), "|")(1), "_")(1)
                            If Not SistemaPV.Usuario.PermiteListaDistribuidor Then .ColHidden(v) = Not (LCase(.TextMatrix(0, v)) = LCase("minorista"))
                            .ColPosition(v) = i
                            i = i + 1
                        Else
                            Select Case LCase(.ColKey(v))
                                Case "id"
                                    .ColHidden(v) = Not (SistemaPV.Configuracion.CodigoEsId)
                                Case "id_arti"
                                    .ColHidden(v) = (SistemaPV.Configuracion.CodigoEsId)
                                Case "detalle", "unidad", "empaque", "envase"

                                Case Else
                                    .ColHidden(v) = True
                            End Select
                        End If
                    Next v
                    .ColFormat(.ColIndex("id_arti")) = ""
                    objInformes = New clsInformes
                    objInformes.Grilla_Columnas.Tag, .object, False, SistemaPV.Rutas.Sistema & "usuario.ini"
                    MatarObjeto(objInformes)
                End With
            End With
            MatarObjeto(rstArticulos)
        End If
        MatarObjeto(objArticulos)
        objMensajes.Ocultar()
        Exit Function
e:
        objError.Capturar("Articulos_Refrescar", Err)
        objMensajes.Ocultar()
        MatarObjeto(objArticulos)
        MatarObjeto(objConsulta)
        MatarObjeto(rstArticulos)
        MatarObjeto(objInformes)
        objError.Generar()
    End Function

    Public Function Grilla(ByVal strProc As String, ByVal boolEnServidor As Boolean, ByVal strTitulo As String) As Object
        Dim strSubtitulo As String, strObjeto As String
        Dim objconsultas As Informes.clsInformes, objDatos As Object
        Dim rstConsulta As ADODB.Recordset
        Dim varDatos() As String
        On Error GoTo e

        ' /- Trae los datos
        'strObjeto = Sistema.DLL & ".clsConsultas"


        objDatos = New clsConsultas
        rstConsulta = objDatos.Consultas_Almacenadas(strProc, strTitulo, strSubtitulo)
        If Not (objDatos Is Nothing) Then objDatos = Nothing

        ' /- Muestra los datos
        objconsultas = New clsInformes
        varDatos = objconsultas.Grilla(rstConsulta, strTitulo, strSubtitulo, SistemaPV.Rutas.Sistema & "usuario.ini")
        If Not (objconsultas Is Nothing) Then objconsultas = Nothing
        If Not (rstConsulta Is Nothing) Then rstConsulta = Nothing
        Grilla = varDatos
        Exit Function
e:
        ReDim varDatos(0)
        MostrarError()
        If Not (objconsultas Is Nothing) Then objconsultas = Nothing
        If Not (objDatos Is Nothing) Then objDatos = Nothing
        Grilla = varDatos
    End Function
    Public Function GrillaRst(ByVal strTitulo As String, ByVal strSubtitulo As String, ByVal rstDatosParaConsulta As ADODB.Recordset) As Object
        Dim objconsultas As Informes.clsInformes
        Dim varDatos() As String
        On Error GoTo e

        ' /- Muestra los datos
        objconsultas = New clsInformes
        varDatos = objconsultas.Grilla(rstDatosParaConsulta, strTitulo, strSubtitulo, SistemaPV.Rutas.Sistema & "usuario.ini")
        MatarObjeto(objconsultas)
        GrillaRst = varDatos
        Exit Function
e:
        ReDim varDatos(0)
        MostrarError()
        MatarObjeto(objconsultas)
        GrillaRst = varDatos
    End Function


    Public Sub Logueo()
        'Dim boolSigue As Boolean, boolReintenta As Boolean

        SistemaPV.Usuario = Usuario_Solicitar("Inicio de Sistema", SistemaPV.Configuracion.ValidacionPorTarjeta, SistemaPV.Usuario)
        If SistemaPV.Usuario.idOperador > 0 Then
            If SistemaPV.Usuario.Otro And SistemaPV.Usuario.idOperador = 0 And Not SistemaPV.Usuario.Administrador Then
                If MsgBox("Los Datos ingresados son Correctos" & Chr(13) & "¿Está seguro de Ingresar como dicho usuario?", vbYesNo + vbQuestion, "A T E N C I O N") = vbYes Then
                    frmSistema_LoginFiscal.Show(vbModal)
                Else
                    'boolSigue = False
                End If
            ElseIf SistemaPV.Usuario.Administrador Then
                'If MsgBox("Los Datos ingresados son Correctos" & Chr(13) & "¿Desea establecer los parámetros de comunicación?", vbYesNo + vbQuestion, "Administrador") = vbYes Then frmRed.Show vbModal
                'boolSigue = False
                'boolReintenta = True
            ElseIf SistemaPV.Usuario.Programador Then
                If MsgBox("Los Datos ingresados son Correctos" & Chr(13) & "¿Desea establecer los parámetros del Sistema?", vbYesNo + vbQuestion, "Administrador") = vbYes Then Configuracion()
                'boolSigue = False
                'boolReintenta = True
            Else
                'boolSigue = True
            End If
        End If

    End Sub

    Private Sub DatosDelSistema()
        Dim strRutaApp As String
        ' - Traemos la Ruta, la ruta de impresion y si imprime en red o no
        strRutaApp = LCase(Trim(App.Path))
        If Right(strRutaApp, 1) <> "\" Then strRutaApp = strRutaApp & "\"
        With SistemaPV
            .Rutas.Sistema = Trim(GetKeyVal(strRutaApp & "datos.ini", "Rutas", "Sistema"))
            If Not (.Rutas.Sistema = "") Then
                With .Rutas
                    If Right(.Sistema, 1) <> "\" Then .Sistema = .Sistema & "\"
                    .Sistema = Trim(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Rutas", "Sistema"))
                    .Copias = Trim(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Rutas", "Copias"))
                    .Impresora = Trim(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Rutas", "Impresora"))
                End With
                With .Configuracion
                    With .Impresion
                        .ImpresoraDefault = Printer.DeviceName
                        .Comprobanteventa.Impresora = Trim(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Venta - Impresora"))
                        .Comprobanteventa.Modo = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Venta - modo"))
                        .MovimientoInterno.Impresora = Trim(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Mov. Interno - Impresora"))
                        .MovimientoInterno.Modo = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Mov. Interno - modo"))
                        .Caja.Impresora = Trim(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Caja - Impresora"))
                        .Caja.Modo = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Caja - modo"))
                        .ForzarImpresionBasica = (Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Forzar Impresion Basica")) = 1)
                        .Comprobanteventacopia.Impresora = Trim(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Venta Copia- Impresora"))
                        .Comprobanteventacopia.Modo = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresion de Comprobantes", "Comprobante de Venta Copia- modo"))
                    End With
                    .sinRed = (Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "sinRed")) = 1)
                    .reintentaRed = Not SistemaPV.Configuracion.sinRed ' (Val(GetKeyVal(strRutaApp & "datos.ini", "datos del sistema", "sinRed")) = 1)
                    .PideVendedor = (Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "PideVendedor")) = 1)
                    .PideHora = (Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "PideHora")) = 1)
                    .ValidacionPorTarjeta = (Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "Validacion por tarjeta")) = 1)
                    .CodigoEsId = (Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "Datos_es_Id")) = 1)
                    .Pedidos = (Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "Tipo")) = 1)
                End With
                With .ImpFiscal
                    .Puerto = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresora Fiscal", "Puerto"))
                    .Modelo = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresora Fiscal", "Modelo"))
                    .Tipo = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Impresora Fiscal", "Tipo"))
                    .Utiliza = (.Puerto < 50)
                End With
                With .datosPV
                    .Caja = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "CajaNumero"))
                    .PtoVta = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "PtoVta"))
                    .SoloPresupuesto = Val(GetKeyVal(SistemaPV.Rutas.Sistema & "datos.ini", "Configuracion", "SoloPresupuesto"))
                End With
                If .Usuario.Otro Then .Usuario.Otro = (Val(GetKeyVal(.Rutas.Sistema & "datos.ini", "Configuracion", "Reparada")) = 1)
            Else
                MsgBox("Ruta de datos Incorrecta", vbCritical, "DATOS.INI")
            End If
        End With
    End Sub
    Private Sub DatosDelSistema_Grabar()
        Dim strRutaApp As String
        ' - Traemos la Ruta, la ruta de impresion y si imprime en red o no
        strRutaApp = LCase(Trim(App.Path))
        If Right(strRutaApp, 1) <> "\" Then strRutaApp = strRutaApp & "\"
        If Trim(SistemaPV.Rutas.Sistema) <> "" Then
            AddToINI(strRutaApp & "datos.ini", "datos del sistema", "PideVendedor", Abs(CInt(SistemaPV.Configuracion.PideVendedor)))
        Else
            MsgBox("Ruta de datos Incorrecta", vbCritical, "DATOS.INI")
        End If
        Exit Sub
    End Sub

    Public Function Configuracion()
        Dim v As Integer
        With frmSistema_Configuracion
            With .cmbOpciones
                .item(v).AddItem("Epson TM")
                .item(v).AddItem("Hasar 615F")
                For v = 1 To .Count - 1
                    .item(v).Clear()
                    .item(v).AddItem("NO")
                    .item(v).AddItem("SI")
                    .item(v).ListIndex = 0
                Next v
            End With
            .UpDown1(0).Value = SistemaPV.datosPV.PtoVta
            .UpDown1(1).Value = SistemaPV.datosPV.Caja
            .UpDown1(2).Value = SistemaPV.ImpFiscal.Puerto
            .txtDatos(0).Text = SistemaPV.datosPV.PtoVta
            .txtDatos(1).Text = SistemaPV.datosPV.Caja
            .txtDatos(6).Text = SistemaPV.ImpFiscal.Puerto
            .txtDatos(2).Text = SistemaPV.Rutas.Sistema
            .txtDatos(3).Text = SistemaPV.Rutas.Copias
            .txtDatos(3).Text = ""
            .txtDatos(4).Text = SistemaPV.Rutas.Impresora
            .cmbOpciones(0).ListIndex = Abs(Val(SistemaPV.ImpFiscal.Modelo))
            .cmbOpciones(1).ListIndex = Abs(CInt(SistemaPV.Configuracion.sinRed))
            .cmbOpciones(2).ListIndex = Abs(CInt(SistemaPV.Usuario.Otro))
            .Show(vbModal)
            If Val(.Tag) = 1 Then
                ' AddToINI Sistema.Rutas.Sistema & "datos.ini", "", "", ""
            End If
            Unload(frmSistema_Configuracion)
        End With
    End Function




    Private Sub Error_Configurar()
        objError = New Errores.clsError
        With objError
            .MomentoDelRegistro = al_Mostrar
            .Plataforma = Win32
            .Sistema_Nombre = App.Title
            .Usuario = SistemaPV.Usuario.Detalle
            .RutaArchivoLog = SistemaPV.Rutas.Sistema & "errores.log"
            .Limpiar()
        End With
    End Sub

    Public Sub Sistema_Cerrar()
        DatosDelSistema_Grabar()
        MatarObjeto(objError)
        MatarObjeto(objMensajes)
        End
    End Sub

    Public Function Ventas_DiaInicializado() As Boolean
        Dim objVentas As clsCajas
        Dim boolOK As Boolean
        On Error GoTo e

        DoEvents()

        objVentas = New clsCajas
        boolOK = objVentas.DiaInicializado(SistemaPV.datosPV.Caja, SistemaPV.Usuario.idOperador)
        MatarObjeto(objVentas)
        Ventas_DiaInicializado = boolOK
        If Not boolOK Then MsgBox("Existe Comprobantes con fecha distinta a la del día", vbCritical, "Cierre del Día")


        Exit Function
e:
        objError.Mostrar(Err)
        MatarObjeto(objVentas)
    End Function
    Public Function Caja_SolicitarExtraccion() As Boolean
        '    Dim objVentas As MarketSis_Pv.clsCajas
        '    Dim boolOK As Boolean
        '    On Error GoTo e:
        '
        '    Set objVentas = New clsCajas
        '    boolOK = objVentas.SolicitarExtraccion()
        '    MatarObjeto objVentas
        '    Caja_SolicitarExtraccion = boolOK
        '    If Not boolOK Then MsgBox "Solicite Extracción de Valores de Caja", vbCritical, "Valores en Caja"
        '
        '
        '    Exit Function
        'e:
        '    objError.Mostrar Err
        '    MatarObjeto objVentas
    End Function
    Public Function Caja_Habilitada(ByVal boolMostrarMensaje As Boolean) As Boolean
        Dim objVentas As clsCajas
        Dim boolOK As Boolean
        Dim strMensaje As String
        On Error GoTo e

        SistemaPV.Usuario.CajaHabilitada = "Sin Comprobar"
        objVentas = New clsCajas
        strMensaje = objVentas.CajaHabilitada(SistemaPV.datosPV.Caja, SistemaPV.Usuario.idOperador)
        MatarObjeto(objVentas)

        If Not (strMensaje = "") Then
            boolOK = False
            If boolMostrarMensaje Then MsgBox(strMensaje, vbCritical, "Caja Habilitada")
            SistemaPV.Usuario.CajaHabilitada = strMensaje
        Else
            boolOK = True
            SistemaPV.Usuario.CajaHabilitada = ""
        End If
        Caja_Habilitada = boolOK
        Exit Function
e:
        objError.Mostrar(Err)
        MatarObjeto(objVentas)
    End Function
    Public Sub Caja_ActualizarDatos(ByVal intTipo As Integer)
        Dim objVentas As clsConsultas

        Dim rstdatos As ADODB.Recordset
        Dim boolOK As Boolean
        On Error GoTo e

        objVentas = New clsConsultas
        ' ------- URGENTE PASAR A NEGOCIOS
        rstdatos = objVentas.Consultas_Almacenadas("SELECT  dbo.Cajas.pos_id, dbo.Cajas.pos_posicion, dbo.Actualizaciones.act_tipo, dbo.Actualizaciones.act_estados FROM         dbo.Actualizaciones CROSS JOIN dbo.Cajas WHERE     (CAST(SUBSTRING(dbo.Actualizaciones.act_estados, dbo.Cajas.pos_posicion, 1) AS int) IN (1, 2, 3)) AND (pos_id = " & SistemaPV.datosPV.Caja & ") AND (act_Tipo =" & intTipo & ")", "", "")
        MatarObjeto(objVentas)
        If Not (rstdatos Is Nothing) Then
            With rstdatos
                boolOK = Not (.RecordCount = 0)
                .Close()
            End With
        End If
        MatarObjeto(rstdatos)
        If boolOK Then
            If MsgBox("Existen actualizaciones pendientes" & Chr(13) & "¿Desea Ejecutarlas?", vbYesNo + vbQuestion, "Pendientes") = vbYes Then Articulos_Refrescar()
        End If

        Exit Sub
e:
        objError.Mostrar(Err)
        MatarObjeto(objVentas)
    End Sub
    '--------------------------------------------------------------
    ' TipoComprobante (0 - vta / 1 - movinterno)
    '--------------------------------------------------------------
    Public Sub Comprobantes_Imprimir( _
        ByVal TipoComprobante As Integer, _
        ByVal dtFecha As Date, _
        ByVal strComprobante As String, _
        ByVal strNumero As String, _
        ByVal strEncabezado1 As String, _
        ByVal strEncabezado2 As String, _
        ByVal rstItems As ADODB.Recordset, _
        ByVal strObservacion As String, _
        ByVal intCopias As Integer)
        Dim boolTickeadora As Boolean
        On Error GoTo e

        Dim intAncho As Integer, v As Integer, i As Integer, j As Integer
        Dim strLinea As String, strImpresora As String
        Dim dblTotal(1) As Double

        objMensajes.Mensaje("Impresión", "Aguarde un instante mientras se imprime el comprobante")
        objMensajes.Mostrar()


        If intCopias <= 0 Then intCopias = 1
        intAncho = 0
        If Not (rstItems Is Nothing) Then
            With rstItems
                If .RecordCount > 0 Then
                    intAncho = 40
                    .MoveFirst()
                End If
            End With
        End If

        If (intAncho = 0) Then
            Err.Raise(1, "Validaciones", "No existen items a imprimir")
        Else

            Select Case TipoComprobante
                Case 0 ' vta
                    strImpresora = SistemaPV.Configuracion.Impresion.Comprobanteventa.Impresora
                    boolTickeadora = (SistemaPV.Configuracion.Impresion.Comprobanteventa.Modo = Tickeadora)
                Case 1 ' movinterno
                    strImpresora = SistemaPV.Configuracion.Impresion.MovimientoInterno.Impresora
                    boolTickeadora = (SistemaPV.Configuracion.Impresion.MovimientoInterno.Modo = Tickeadora)
                Case 2 ' Copia!
                    strImpresora = SistemaPV.Configuracion.Impresion.Comprobanteventacopia.Impresora
                    boolTickeadora = (SistemaPV.Configuracion.Impresion.Comprobanteventacopia.Modo = Tickeadora)
            End Select
            strImpresora = LCase(Trim(strImpresora))
            If strImpresora = "" Then
                strImpresora = "tickeadora"
                boolTickeadora = True
            End If

            Imprimir_ElegirImpresora(strImpresora)
            For j = 1 To intCopias
                With Printer
                    If boolTickeadora Then
                        .FontName = "16 cpi"
                        .FontSize = 9
                        If Not SistemaPV.Configuracion.Impresion.ForzarImpresionBasica Then
                            .FontName = "16 cpi"
                            .FontSize = 18
                            Printer.Print(UCase(strComprobante))
                            .FontSize = 9
                        Else
                            Printer.Print(UCase(strComprobante))
                        End If
                    Else
                        .FontName = "Courier New"
                        .FontSize = 10
                        .FontBold = True
                        Printer.Print(UCase(strComprobante))
                        .FontSize = 14
                        .FontBold = False
                    End If
                    strLinea = Left("Nº: " & strNumero, 18)
                strLinea = strLinea & String(20 - Len(strLinea), " ") & " Fecha:" & Format(dtFecha, "short date")
                    Printer.Print(Left(strLinea, intAncho))
                    If Trim(strEncabezado1) <> "" Then Printer.Print(Left(strEncabezado1, intAncho))
                    If Trim(strEncabezado2) <> "" Then Printer.Print(Left(strEncabezado2, intAncho))
                    Printer.Print("")
                    If Not SistemaPV.Configuracion.Impresion.ForzarImpresionBasica Then
                        .FontSize = 12
                        .FontBold = False
                    End If
                Printer.Print String(intAncho, "-")
                    v = 0

                    ' - Impresión
                    ' - Depende del Comprobante
                    With rstItems
                        If .RecordCount > 0 Then .MoveFirst()
                        Do While Not .EOF
                            v = v + 1
                            Select Case TipoComprobante
                                Case 1 ' movinternos
                                    If v = 1 Then
                                        strLinea = "Codigo    Detalle Producto"
                                        strLinea = strLinea & String(30 - Len(strLinea), " ") & "  Cantidad"
                                        Printer.Print(strLinea)
                                        Printer.Print String(intAncho, "-")
                                    End If
                                    strLinea = Left(Val(Left(Trim("" & !Id_arti), 3)) & "." & Val(Right(Trim("" & !Id_arti), 6)), 10)
                                    strLinea = strLinea & String(10 - Len(strLinea), " ") & Trim(Left(Trim("" & !Detalle), 20))
                                    strLinea = strLinea & String(30 - Len(strLinea), " ")
                                    strLinea = strLinea & String(8 - Len(Left(Format(Val("" & !Cantidad), cFDinero), 8)), " ") & Format(Val("" & !Cantidad), cFDinero)
                                    Printer.Print(strLinea)
                                    If v = .RecordCount Then
                                        Printer.Print String(intAncho, "-")
                                        Printer.Print(" Cantidad de ITEMS : " & v)
                                        Printer.Print(" - No válido como Factura - ")
                                    End If


                                Case 0 ' vta
                                    dblTotal(0) = 0
                                    If v = 1 Then
                                        dblTotal(0) = 1
                                        strLinea = "Detalle Producto"
                                    strLinea = strLinea & String(27 - Len(strLinea), " ") & String(8, " ") & "Total"
                                        Printer.Print(strLinea)
                                    Printer.Print String(intAncho, "-")
                                    End If
                                    dblTotal(0) = Val("" & !Cantidad) * Val("" & !final)
                                    dblTotal(1) = dblTotal(1) + dblTotal(0)
                                    ' si tiene totales detalla los precios

                                    strLinea = Trim(Left(Trim("" & !Detalleticket), 27))
                                strLinea = strLinea & String(27 - Len(strLinea), " ")
                                strLinea = strLinea & String(11 - Len(Left(Format(dblTotal(0), cFDinero), 11)), " ") & "$ " & Format(dblTotal(0), cFDinero)
                                    Printer.Print(strLinea)

                                    If Val("" & !Cantidad) <> 1 Then
                                        ' Space(3) & "--> " &
                                        strLinea = Format(Val("" & !Cantidad), cFDinero & "0") & " x $" & Format(Val("" & !final), cFDinero)  ' & " = $" & Format(dblTotal(0), cFDinero)
                                        Printer.Print(Trim(strLinea))
                                    End If



                                    Printer.Print("")
                                    If v = .RecordCount Then
                                    Printer.Print String(intAncho, "-")
                                        strLinea = "TOTAL  : $ " & Format(dblTotal(1), cFDinero)
                                    Printer.Print String(intAncho - Len(strLinea), " ") & strLinea
                                    Printer.Print String(intAncho, "-")
                                        Printer.Print(" Cantidad de ITEMS : " & v)

                                    End If
                                Case 2 ' vta Copia
                                    If v = 1 Then
                                        dblTotal(0) = 1
                                        strLinea = "Detalle Producto"
                                    strLinea = strLinea & String(27 - Len(strLinea), " ") & String(8, " ") & "Cant."
                                        Printer.Print(strLinea)
                                    Printer.Print String(intAncho, "-")
                                    End If

                                    strLinea = Trim(Left(Trim("" & !Detalleticket), 27))
                                strLinea = strLinea & String(27 - Len(strLinea), " ")
                                strLinea = strLinea & String(11 - Len(Left(Format(Val("" & !Cantidad), cFDinero), 13)), " ") & Format(Val("" & !Cantidad), cFDinero)
                                    Printer.Print(strLinea)

                                    'If Val("" & !Cantidad) <> 1 Then
                                    '    ' Space(3) & "--> " &
                                    '    strLinea = Format(Val("" & !Cantidad), cFDinero & "0") & " x $" & Format(Val("" & !final), cFDinero)  ' & " = $" & Format(dblTotal(0), cFDinero)
                                    '    Printer.Print Trim(strLinea)
                                    'End If

                                    Printer.Print("")

                                    If v = .RecordCount Then
                                        'Printer.Print String(intAncho, "-")
                                        'strLinea = "TOTAL  : $ " & Format(dblTotal(1), cFDinero)
                                        'Printer.Print String(intAncho - Len(strLinea), " ") & strLinea
                                    Printer.Print String(intAncho, "-")
                                        Printer.Print(" Cantidad de ITEMS : " & v)

                                    End If
                            End Select
                            DoEvents()
                            .MoveNext()
                        Loop
                        .MoveFirst()
                    End With

                    ' - observacion
                    If strObservacion <> "" Then
                    Printer.Print String(intAncho, "-")
                        strLinea = strObservacion
                        Do While strLinea <> ""
                            strLinea = Trim(strLinea)
                            If Len(strLinea) > (intAncho - 3) Then
                                Printer.Print(Left(strLinea, intAncho - 3))
                                strLinea = Trim(Right(strLinea, Len(strLinea) - intAncho + 3))
                            Else
                                Printer.Print(strLinea)
                                strLinea = ""
                            End If
                        Loop
                    End If
                    ' - Pie de Comprobante
                Printer.Print String(intAncho, "-")
                Printer.Print "Impresión: " & Date & " - " & Time
                    Printer.Print(" (" & SistemaPV.datosPV.PtoVta & " - " & SistemaPV.datosPV.Caja & " -> " & SistemaPV.Usuario.Detalle & ")")
                    Printer.Print("")
                    If boolTickeadora Then
                        If Not SistemaPV.Configuracion.Impresion.ForzarImpresionBasica Then
                            .FontBold = True
                            .Font.Size = 10
                            .Font.Name = "control"
                            Printer.Print("p") 'Full cut
                        End If
                    End If
                    .EndDoc()
                    DoEvents()
                End With
            Next j
            objMensajes.Ocultar()
        End If
        Imprimir_ElegirImpresora("")

        ' -----------------------------------------
        ' Comprobante
        ' Numero :              - Fecha
        ' Encabezado1
        ' Encabezado2
        ' -----------------------------------------
        ' ITEMS
        ' -----------------------------------------
        ' observacion
        ' -----------------------------------------
        ' Fecha Impresión:
        ' (punto de venta - Usuario)


        Exit Sub
e:
        Err.Clear()
        Imprimir_ElegirImpresora("")
        objMensajes.Ocultar()
    End Sub


    'Public Sub Comprobantes_Imprimir( _
    '    ByVal TipoComprobante As Integer, _
    '    ByVal dtFecha As Date, _
    '    ByVal strComprobante As String, _
    '    ByVal strNumero As String, _
    '    ByVal strEncabezado1 As String, _
    '    ByVal strEncabezado2 As String, _
    '    ByVal rstItems As ADODB.Recordset, _
    '    ByVal strObservacion As String, _
    '    ByVal intCopias As Integer)
    '    Dim boolTickeadora As Boolean
    '    On Error GoTo e:
    '
    '    Dim intAncho As Integer, v As Integer, i As Integer, j As Integer
    '    Dim strLinea As String, strImpresora As String
    '    Dim dblTotal(1) As Double
    '
    '    objMensajes.Mensaje  "Impresión", "Aguarde un instante mientras se imprime el comprobante"
    '    objMensajes.Mostrar
    '
    '
    '    If intCopias <= 0 Then intCopias = 1
    '    intAncho = 0
    '    If Not (rstItems Is Nothing) Then
    '        With rstItems
    '            If .RecordCount > 0 Then
    '                intAncho = 40
    '                .MoveFirst
    '            End If
    '        End With
    '    End If
    '
    '    If (intAncho = 0) Then
    '        Err.Raise 1, "Validaciones", "No existen items a imprimir"
    '    Else
    '
    '        Select Case TipoComprobante
    '            Case 0 ' vta
    '                strImpresora = Sistema.Configuracion.Impresion.Comprobanteventa.Impresora
    '                boolTickeadora = (Sistema.Configuracion.Impresion.Comprobanteventa.Modo = Tickeadora)
    '            Case 1 ' movinterno
    '                strImpresora = Sistema.Configuracion.Impresion.MovimientoInterno.Impresora
    '                boolTickeadora = (Sistema.Configuracion.Impresion.MovimientoInterno.Modo = Tickeadora)
    '        End Select
    '        strImpresora = LCase(Trim(strImpresora))
    '        If strImpresora = "" Then
    '            strImpresora = "tickeadora"
    '            boolTickeadora = True
    '        End If
    '
    '        Imprimir_ElegirImpresora strImpresora
    '        For j = 1 To intCopias
    '            With Printer
    '                If boolTickeadora Then
    '                    .FontName = "16 cpi"
    '                    .FontSize = 18
    '                    Printer.Print UCase(strComprobante)
    '                    .FontSize = 9
    '                Else
    '                    .FontName = "Courier New"
    '                    .FontSize = 10
    '                    .FontBold = True
    '                    Printer.Print UCase(strComprobante)
    '                    .FontSize = 14
    '                End If
    '                .FontBold = False
    '                strLinea = Left("Nº: " & strNumero, 18)
    '                strLinea = strLinea & String(20 - Len(strLinea), " ") & " Fecha:" & Format(dtFecha, "short date")
    '                Printer.Print Left(strLinea, intAncho)
    '                If Trim(strEncabezado1) <> "" Then Printer.Print Left(strEncabezado1, intAncho)
    '                If Trim(strEncabezado2) <> "" Then Printer.Print Left(strEncabezado2, intAncho)
    '                Printer.Print
    '                .FontSize = 12
    '                .FontBold = False
    '                Printer.Print String(intAncho, "-")
    '                v = 0
    '
    '                ' - Impresión
    '                ' - Depende del Comprobante
    '                With rstItems
    '                    If .RecordCount > 0 Then .MoveFirst
    '                    Do While Not .EOF
    '                        v = v + 1
    '                        Select Case TipoComprobante
    '                            Case 1 ' movinternos
    '                                    If v = 1 Then
    '                                        strLinea = "Codigo    Detalle Producto"
    '                                        strLinea = strLinea & String(30 - Len(strLinea), " ") & "  Cantidad"
    '                                        Printer.Print strLinea
    '                                        Printer.Print String(intAncho, "-")
    '                                    End If
    '                                    strLinea = Left(Val(Left(Trim("" & !id_Arti), 3)) & "." & Val(Right(Trim("" & !id_Arti), 6)), 10)
    '                                    strLinea = strLinea & String(10 - Len(strLinea), " ") & Trim(Left(Trim("" & !Detalle), 20))
    '                                    strLinea = strLinea & String(30 - Len(strLinea), " ")
    '                                    strLinea = strLinea & String(8 - Len(Left(Format(Val("" & !cantidad), cFDinero), 8)), " ") & Format(Val("" & !cantidad), cFDinero)
    '                                    Printer.Print strLinea
    '                                    If v = .RecordCount Then
    '                                        Printer.Print String(intAncho, "-")
    '                                        Printer.Print " Cantidad de ITEMS : " & v
    '                                        Printer.Print " «« No válido como Factura »» "
    '                                    End If
    '
    '
    '                            Case 0 ' vta
    '                                dblTotal(0) = 0
    '                                If v = 1 Then
    '                                    dblTotal(0) = 1
    '                                    strLinea = "Detalle Producto"
    '                                    strLinea = strLinea & String(27 - Len(strLinea), " ") & String(8, " ") & "Total"
    '                                    Printer.Print strLinea
    '                                    Printer.Print String(intAncho, "-")
    '                                End If
    '                                dblTotal(0) = Val("" & !cantidad) * Val("" & !final)
    '                                dblTotal(1) = dblTotal(1) + dblTotal(0)
    '                                ' si tiene totales detalla los precios
    '
    '                                strLinea = Trim(Left(Trim("" & !Detalleticket), 27))
    '                                strLinea = strLinea & String(27 - Len(strLinea), " ")
    '                                strLinea = strLinea & String(11 - Len(Left(Format(dblTotal(0), cFDinero), 11)), " ") & "$ " & Format(dblTotal(0), cFDinero)
    '                                Printer.Print strLinea
    '
    '                                If Val("" & !cantidad) <> 1 Then
    '                                    ' Space(3) & "--> " &
    '                                    strLinea = Format(Val("" & !cantidad), cFDinero & "0") & " x $" & Format(Val("" & !final), cFDinero)  ' & " = $" & Format(dblTotal(0), cFDinero)
    '                                    Printer.Print strLinea
    '                                End If
    '
    '
    '
    '                                Printer.Print
    '                                If v = .RecordCount Then
    '                                    Printer.Print String(intAncho, "-")
    '                                    strLinea = "TOTAL  : $ " & Format(dblTotal(1), cFDinero)
    '                                    Printer.Print String(intAncho - Len(strLinea), " ") & strLinea
    '                                    Printer.Print String(intAncho, "-")
    '                                    Printer.Print " Cantidad de ITEMS : " & v
    '
    '                                End If
    '                        End Select
    '                        DoEvents
    '                        .MoveNext
    '                    Loop
    '                    .MoveFirst
    '                End With
    '
    '                ' - observacion
    '                If strObservacion <> "" Then
    '                    Printer.Print String(intAncho, "-")
    '                    strLinea = strObservacion
    '                    Do While strLinea <> ""
    '                        strLinea = Trim(strLinea)
    '                        If Len(strLinea) > (intAncho - 3) Then
    '                            Printer.Print Left(strLinea, intAncho - 3)
    '                            strLinea = Trim(Right(strLinea, Len(strLinea) - intAncho + 3))
    '                        Else
    '                            Printer.Print strLinea
    '                            strLinea = ""
    '                        End If
    '                    Loop
    '                End If
    '
    '                ' - Pie de Comprobante
    '                Printer.Print String(intAncho, "-")
    '                Printer.Print "Impresión: " & Date & " - " & Time
    '                Printer.Print " (" & Sistema.datosPV.PtoVta & " - " & Sistema.datosPV.Caja & " -> " & Sistema.usuario.Detalle & ")"
    '                Printer.Print ""
    '                If boolTickeadora Then
    '                    .FontBold = True
    '                    .Font.Size = 10
    '                    .Font.Name = "control"
    '                    Printer.Print "p" 'Full cut
    '                End If
    '                .EndDoc
    '                DoEvents
    '            End With
    '        Next j
    '        objMensajes.Ocultar
    '    End If
    '    Imprimir_ElegirImpresora ""
    '
    '    ' -----------------------------------------
    '    ' Comprobante
    '    ' Numero :              - Fecha
    '    ' Encabezado1
    '    ' Encabezado2
    '    ' -----------------------------------------
    '    ' ITEMS
    '    ' -----------------------------------------
    '    ' observacion
    '    ' -----------------------------------------
    '    ' Fecha Impresión:
    '    ' (punto de venta - Usuario)
    '
    '
    '    Exit Sub
    'e:
    '    objError.Capturar "Comprobantes_Imprimir", Err
    '    objMensajes.Ocultar
    '    objError.Generar
    '    Resume Next
    '    Imprimir_ElegirImpresora ""
    'End Sub


    Public Function Usuario_Solicitar( _
        ByVal strMotivo As String, _
        ByVal boolPorTarjeta As Boolean, _
        ByRef UsuarioActual As tUsuario) As tUsuario

        Dim objUsuario As clsConsultas
        Dim rstUsuario As ADODB.Recordset
        Dim boolContinuar As Boolean
        Dim strUsuario(1) As String
        Dim Devolver As tUsuario
        Dim intError As Integer

        ' Limpieza de variables
        With Devolver
            .idOperador = 0
            .Detalle = ""
            .Administrador = False
            .Programador = False
            .Otro = False
            .Vendedor = False
            .Descuento = 0
            .PermiteCambioPrecio = False
            .PermiteListaDistribuidor = False
        End With
        Usuario_Solicitar = Devolver
        Erase strUsuario


        ' formulario de ingreso
        With frmUsuario_ClavePedir
            .Motivo = strMotivo
            .MetodoXTarjeta = boolPorTarjeta
            .Validar = False
            Do
                .Show(vbModal)
                boolContinuar = True
                If .Validar Then
                    strUsuario(0) = Trim(.Usuario)
                    strUsuario(1) = Trim(.Clave)
                    If .MetodoXTarjeta Then strUsuario(0) = "portarjeta"

                    ' Bloque de Peticion de Usuario
                    'On Error Resume Next
                    objUsuario = New clsConsultas
                    rstUsuario = objUsuario.Usuario(strUsuario(0), strUsuario(1))
                    intError = Err.Number
                    If intError <> 0 Then
                        objError.Capturar("Usuario (Intento " & .Errores + 1 & ")", Err)
                        On Error GoTo e
                        If (intError = 100) And (.Errores < 2) Then
                            .DatosConError = True
                            objError.Limpiar()
                        Else
                            objError.Generar()
                        End If
                    End If
                    MatarObjeto(objUsuario)
                    On Error GoTo e

                    If Not (rstUsuario Is Nothing) Then
                        With rstUsuario
                            If Not .EOF Then
                                Devolver.idOperador = Val("" & !id_vendedor)
                                Devolver.Detalle = Trim("" & !Detalle)
                                Devolver.Administrador = !esAdministrador
                                Devolver.Programador = !esProgramador
                                Devolver.Otro = (!esotro) Or Devolver.Otro
                                Devolver.Vendedor = (Val("" & !id_vendedor) <> 0)
                                Devolver.Descuento = Val("" & !dto)

                                Devolver.PermiteCambioPrecio = !PermiteCambioPrecio

                                Devolver.PermiteListaDistribuidor = !PermiteListaDistribuidor

                                boolContinuar = False
                            End If
                            .Close()
                        End With
                        MatarObjeto(rstUsuario)
                    End If

                Else
                    boolContinuar = False
                End If
            Loop While boolContinuar
        End With
        Unload(frmUsuario_ClavePedir)
        Usuario_Solicitar = Devolver
        Exit Function
e:
        MostrarError()
        MatarObjeto(objUsuario)
        MatarObjeto(rstUsuario)
        MatarObjeto(frmUsuario_ClavePedir)
    End Function


    Public Sub Imprimir_ElegirImpresora(ByVal strImpresora As String)
        Dim prPrinter As Printer
        Dim strBuscar As String
        On Error GoTo e
        ' strImpresora = "tickeadora"
        If Not (strImpresora = "") Then
            strBuscar = LCase(strImpresora)
            SistemaPV.Configuracion.Impresion.ImpresoraDefault = Printer.DeviceName
        Else
            strBuscar = LCase(SistemaPV.Configuracion.Impresion.ImpresoraDefault)
        End If

        prPrinter = Printer
        For Each prPrinter In Printers
            If LCase(prPrinter.DeviceName) = strBuscar Then Exit For
        Next
        If Not (prPrinter Is Nothing) And (prPrinter.DeviceName <> Printer.DeviceName) Then Printer = prPrinter
        Exit Sub
e:
        Err.Clear()
    End Sub

    Public Sub Stock_VerOnlinea()
        Dim objServidor As clsConsultas
        Dim objInformes As clsInformes
        Dim rstdatos As ADODB.Recordset
        Dim strWhere As String
        On Error GoTo e
        If MsgBox("¿Desea Mostrar los productos con stock en 0 o negativos?", vbYesNo, "Stock Negativos") = vbNo Then strWhere = " WHERE ISNULL(STOCK, 0) > 0 "
        objMensajes.Mensaje("Consulta", "Aguarde mientras se obtienen los datos desde la casa central")
        objMensajes.Mostrar()



        objServidor = New clsConsultas

        rstdatos = objServidor.Consultas_Almacenadas("SELECT id_arti, detalle, stock FROM articulos " & strWhere & " ORDER BY detalle ASC", "", "")
        MatarObjeto(objServidor)
        objMensajes.Ocultar()
        If Not (rstdatos Is Nothing) Then
            objInformes = New clsInformes
            objInformes.Grilla(rstdatos, "Consulta Online para Pto de Vta", "Stock : " & Time, SistemaPV.Rutas.Sistema & "usuario.ini")
            MatarObjeto(objInformes)
        End If

        Exit Sub
e:
        objError.Capturar("Stock_VerOnlinea", Err)
        objMensajes.Ocultar()
        MatarObjeto(objInformes)
        objError.Generar()
    End Sub
    'Public Sub Parametros_Agregar(ByRef strParametros As String, ByVal strCampo As String, ByVal strValor As String)
    '    If Not (strValor = "") Then
    '        strParametros = Trim(strParametros) & strCampo & "=" & Trim(Replace(Replace(Replace(strValor, "|", ""), "=", ""), "'", "")) & "|"
    '    End If
    'End Sub


    'Public Function Parametros_Obtener(ByVal strParametros As String) As tParametros
    '    Dim T As tParametros
    '    Dim v As Integer
    '    ReDim Preserve T.item(0)
    '    With T
    '        .parametros = Split(strParametros, "|")
    '        .Cantidad = UBound(.parametros)
    '        For v = 0 To .Cantidad - 1
    '            If InStr(1, .parametros(v), "=") > 0 Then
    '                ReDim Preserve .item(UBound(.item) + 1)
    '                .item(UBound(.item) - 1).Id = Trim(Split(.parametros(v), "=")(0))
    '                .item(UBound(.item) - 1).Valor = (Trim(Split(.parametros(v), "=")(1)))
    '            End If
    '        Next v
    '    End With
    '    Parametros_Obtener = T
    'End Function

    'Public Function Parametros_ObtenerValor(parametros As tParametros, ByVal strParametroNombre As String) As tParametro
    '    Dim v As Integer
    '    Dim P As tParametro
    '    P.Id = ""
    '    With parametros
    '        For v = 0 To .Cantidad - 1
    '            If LCase(.item(v).Id) = LCase(strParametroNombre) Then
    '                P.Id = .item(v).Id
    '                P.Valor = .item(v).Valor
    '                v = .Cantidad + 1
    '            End If
    '        Next v
    '    End With
    '    Parametros_ObtenerValor = P
    'End Function

    Public Sub Movimientos_Internos(ByVal intOPcion As Integer)
        Dim frmBotones As Form
        On Error GoTo e
        Select Case intOPcion
            Case 5
                Dim intOpcionBoton As Integer
                Dim strCaptions(5, 1) As String
                intOpcionBoton = 0
                strCaptions(0, 0) = "Enviar" : strCaptions(0, 1) = 0
                strCaptions(1, 0) = "Recibir" : strCaptions(1, 1) = 1
                strCaptions(2, 0) = "Confirmar" : strCaptions(2, 1) = 2
                strCaptions(3, 0) = "Sincronizar" : strCaptions(3, 1) = 3
                strCaptions(4, 0) = "Salir" : strCaptions(4, 1) = 4
                frmBotones = New frmBotonera
                With frmBotones
                    .Configura("MOV. INTERNOS", strCaptions())
                    .OpcionElegida = 4
                    .Show(vbModal)
                    intOpcionBoton = .OpcionElegida
                End With
                Unload(frmBotones)
                MatarObjeto(frmBotones)
                Select Case intOpcionBoton
                    Case 0
                        Movimientos_Internos(Enviar)
                    Case 1
                        Movimientos_Internos(Recibir)
                    Case 2
                        Movimientos_Internos(ConfirmarMI)
                    Case 3
                        Movimientos_Internos(Sincronizar)
                End Select

            Case Else
                Dim strParametros As String
                strParametros = ""
                Parametros_Agregar(strParametros, "depositoId", SistemaPV.datosPV.PtoVta)
                Dim obj As New clsFunciones
                obj.Movimientos_Internos(intOPcion, strParametros)
                MatarObjeto(obj)
        End Select

        Exit Sub
e:
        MostrarError()
        If Not (frmBotones Is Nothing) Then frmBotones = Nothing
        MatarObjeto(obj)
    End Sub


    Private Function Pagos_Comprobante_Pide() As String
        Dim strPagos As String



        If strPagos = "" Then
            Parametros_Agregar(strPagos, "1", 0)
            Parametros_Agregar(strPagos, "2", 0)
            Parametros_Agregar(strPagos, "3", 0)
            Parametros_Agregar(strPagos, "4", 0)
            Parametros_Agregar(strPagos, "Tarjeta", ".")
        End If

        With frmVentas_Pagos
            .Total = 150.1
            .Pagos = strPagos
            .Show(vbModal)
            strPagos = ""
            If .OK Then strPagos = .Pagos
        End With
        Unload(frmVentas_Pagos)

        Pagos_Comprobante_Pide = strPagos
    End Function


    Public Function CierresFiscal(ByVal intTipo As Integer) As Boolean

        Dim boolOK As Boolean
        If intTipo = 1 Then
            boolOK = CierresFiscal_Formulario("", True)
        Else
            mdlFiscal.Cierre(intTipo)
            boolOK = True
        End If

        If boolOK Then
            If intTipo = 1 Then
                MsgBox("Cierre del Dia realizado correctamente" & Chr(13) & "Realice el CIERRE GENERAL", vbInformation, "Cierre Zeta")
            Else
                MsgBox("Cierre x Realizado Correctamente", vbInformation, "Cierre X")
            End If
        Else
            MsgBox("Cierre con errores", vbCritical, "Cierre Zeta")
        End If
        CierresFiscal = boolOK
    End Function
    Public Function Cajas_MenuOpciones(ByVal boolCierreDelDia As Boolean) As Boolean

        ' devuelve si limpia pantalla o no

        Load(frmCajas_Menu)
        With frmCajas_Menu
            .frameCaja.Visible = Not boolCierreDelDia
            .frameUsuario.Visible = boolCierreDelDia
            .Show(vbModal)
            Cajas_MenuOpciones = .ForzarLimpieza
        End With
        Unload(frmCajas_Menu)


    End Function

    Public Function Agregar_Movimiento() As Boolean

        Dim Usuario As tUsuario
        Dim strMensaje As String
        On Error GoTo e

        If Not SistemaPV.Usuario.PermiteCambioPrecio Then
            Usuario = Usuario_Solicitar("Agregar Movimiento", SistemaPV.Configuracion.ValidacionPorTarjeta, SistemaPV.Usuario)
        Else
            Usuario = SistemaPV.Usuario
        End If
        If (Not (Usuario.idOperador = 0)) And Usuario.PermiteCambioPrecio Then
            frmCajas_MovimientosAgregar.Show(vbModal)
        Else
            Err.Raise(1, "Agregar Movimiento", "El usuario no tiene permiso para agregar movimientos")
        End If


        Exit Function
e:

    End Function


    Public Function Cajas_ContarDinero(ByVal intTipo As Integer) As Boolean
        Dim Usuario As tUsuario
        Dim strMensaje As String
        On Error GoTo e


        Select Case intTipo
            Case 0

                If Not SistemaPV.Usuario.PermiteCambioPrecio Then
                    Usuario = Usuario_Solicitar("Retiro de Caja", SistemaPV.Configuracion.ValidacionPorTarjeta, SistemaPV.Usuario)
                Else
                    Usuario = SistemaPV.Usuario
                End If
                If (Not (Usuario.idOperador = 0)) And Usuario.PermiteCambioPrecio Then
                    Caja_ContarDinero_ArmaPantalla(intTipo)
                Else
                    Err.Raise(1, "Limpieza", "El usuario no tiene permiso para realizar retiros de caja")
                End If

            Case 2
                Caja_ContarDinero_ArmaPantalla(2)
            Case 1


                Dim objCaja As clsCajas
                objCaja = New clsCajas
                If objCaja.CajaConArqueo(SistemaPV.datosPV.Caja, SistemaPV.Usuario.idOperador) = "SI" Then strMensaje = "El usuario ya posee un arqueo de caja"
                MatarObjeto(objCaja)
                If strMensaje = "" Then
                    Caja_ContarDinero_ArmaPantalla(intTipo)
                Else
                    MsgBox("El usuario ya posee un arqueo de caja", vbCritical, "Atención")
                End If

        End Select

        Exit Function
e:

        MatarObjeto(objCaja)
    End Function


    Private Sub Caja_ContarDinero_ArmaPantalla(ByVal intTipo As Integer)
        Dim rstdatos As ADODB.Recordset
        Dim objPagos As clsConsultas

        Dim lngLeftCajas As Long, lngTop(1) As Long, lngMasAlto As Long
        Dim v As Integer, j As Integer, pos As Integer, intMonedas As Integer, intPago(1) As Integer
        Dim strParametros As String
        Dim boolItems As Boolean

        On Error GoTo err_ed

        objMensajes.Mensaje("Configuración", "Aguarde un instante mientras se configura la pantalla ...")
        objMensajes.Mostrar()
        strParametros = ""
        Parametros_Agregar(strParametros, "TipoConsulta", "2")
        ' Obtengo las Monedas
        objPagos = New clsConsultas
        rstdatos = objPagos.FormasDePago(strParametros)
        MatarObjeto(objPagos)


        v = 0
        j = 0
        intMonedas = 0
        lngLeftCajas = 100
        With rstdatos
            If .RecordCount > 0 Then
                Load(frmCajas_Arqueo)
                .MoveFirst()
                Do While Not .EOF       ' Recorro las Monedas
                    ' Otra "Moneda" a contar
                    If intPago(0) <> Val("" & !pago_id) Or intPago(1) <> Val("" & !pago_plan) Then
                        intPago(0) = Val("" & !pago_id)
                        intPago(1) = Val("" & !pago_plan)
                        intMonedas = intMonedas + 1
                        boolItems = False
                        ' Verifica si es la única o tiene items
                        If Not .EOF Then
                            .MoveNext()
                            If Not .EOF Then boolItems = (intPago(0) = Val("" & !pago_id) And intPago(1) = Val("" & !pago_plan))
                            .MovePrevious()
                        End If
                        pos = 0

                    End If


                    If pos = 0 Then
                        ' genera los totales
                        If intMonedas > 1 Then
                            Load(frmCajas_Arqueo.L(intMonedas - 1))
                            Load(frmCajas_Arqueo.TT(intMonedas - 1))
                            lngTop(1) = frmCajas_Arqueo.TT(intMonedas - 2).Top + frmCajas_Arqueo.TT(intMonedas - 1).Height + 50
                        Else
                            lngTop(1) = frmCajas_Arqueo.TT(0).Top
                        End If

                        With frmCajas_Arqueo.L(intMonedas - 1)
                            .Tag = 0 ' Val("" & rstDatos.Fields("IdAdministrativa"))
                            .Caption = Trim("" & UCase(rstdatos.Fields("detalle")))
                            .Top = lngTop(1) '  + 50
                            .Visible = True
                        End With
                        With frmCajas_Arqueo.TT(intMonedas - 1)
                            .Text = Format(0, cFDinero)
                            .Top = lngTop(1)
                            .Visible = True
                            .Locked = boolItems
                            .Top = lngTop(1) '  + 50
                            ' lngTop(1) = lngTop(1) + .Height + 90
                        End With
                    End If

                    If boolItems Then
                        ' Si tiene que configurar el encabezado
                        If pos = 0 Then

                            lngTop(0) = frmCajas_Arqueo.T2(0).Top
                            If j > 0 Then
                                Load(frmCajas_Arqueo.lblTituloDeCaja(intMonedas - 1))
                                Load(frmCajas_Arqueo.lblColumnas(intMonedas - 1))
                                If Not (intMonedas = 1) Then lngLeftCajas = lngLeftCajas + frmCajas_Arqueo.lblTituloDeCaja(0).Width + 30
                            End If
                            With frmCajas_Arqueo.lblTituloDeCaja(frmCajas_Arqueo.lblTituloDeCaja.Count - 1)
                                .Left = lngLeftCajas
                                .Caption = UCase(Trim("" & rstdatos.Fields("Detalle")))
                                .Visible = True
                            End With
                            With frmCajas_Arqueo.lblColumnas(frmCajas_Arqueo.lblTituloDeCaja.Count - 1)
                                .Left = frmCajas_Arqueo.lblTituloDeCaja(frmCajas_Arqueo.lblTituloDeCaja.Count - 1).Left
                                .Visible = True
                            End With
                            pos = 1
                        End If


                        ' genera el txt del total
                        If j > 0 Then   ' si no es el Primero
                            Load(frmCajas_Arqueo.L1(j))  ' Valor
                            Load(frmCajas_Arqueo.T(j))   ' Cantidad
                            Load(frmCajas_Arqueo.T2(j))  ' Total
                        End If
                        ' Coloca EL Valor
                        With frmCajas_Arqueo.L1(j)
                            .Caption = Format(Val("" & rstdatos.Fields("mon_valor")), cFDinero)
                            .Top = lngTop(0) + 50
                            .Tag = Val("" & rstdatos.Fields("pago_id"))
                            .Left = lngLeftCajas
                            .Visible = True
                        End With
                        ' Coloca 0 en cantidad
                        With frmCajas_Arqueo.T(j)
                            .Text = Format(0, cFDinero)
                            .Top = lngTop(0)
                            .Tag = intMonedas - 1
                            .Left = frmCajas_Arqueo.L1(j).Left + frmCajas_Arqueo.L1(j).Width + 100
                            .Visible = True
                        End With
                        ' Coloca Total
                        With frmCajas_Arqueo.T2(j)
                            .Text = Format(0, cFDinero)
                            .Top = lngTop(0)
                            .Left = frmCajas_Arqueo.T(j).Left + frmCajas_Arqueo.T(j).Width + 100
                            .Visible = True
                            lngTop(0) = lngTop(0) + .Height + 75
                        End With
                        If lngMasAlto < lngTop(0) Then lngMasAlto = lngTop(0)
                        j = j + 1   ' -> Cantidad de texts
                    End If
                    .MoveNext()
                Loop

                MatarObjeto(rstdatos)
                With frmCajas_Arqueo
                    lngLeftCajas = lngLeftCajas + .lblTituloDeCaja(0).Width
                    If lngLeftCajas < .frameConfirma.Width Then lngLeftCajas = (.lblTituloDeCaja(0).Left * 2) + .frameConfirma.Width


                    .Frame2.Left = lngLeftCajas + 30
                    .Line1(0).X1 = .lblTituloDeCaja(0).Left
                    .Line1(1).X1 = .lblTituloDeCaja(0).Left

                    .Line1(0).X2 = lngLeftCajas + 30
                    .Line1(1).X2 = lngLeftCajas + 30

                    .frameConfirma.Left = (.Line1(0).X2 / 2) - (.frameConfirma.Width / 2)

                    .Width = .Frame2.Left + .Frame2.Width + 200

                    .frameConfirma.Top = lngMasAlto + 100

                    .Line1(0).Y1 = lngMasAlto + 30
                    .Line1(1).Y1 = .frameConfirma.Top + .frameConfirma.Height + 25
                    .Line1(0).Y2 = lngMasAlto + 30
                    .Line1(1).Y2 = .frameConfirma.Top + .frameConfirma.Height + 25

                    .Frame2.Height = .Line1(0).Y1 - .Frame2.Top + 50

                    If .Height < (.frameConfirma.Top + .frameConfirma.Height + .sb.Height + 100) Then .Height = (.frameConfirma.Top + .frameConfirma.Height + .sb.Height * 2 + 100)
                    With .lblInfo
                        .Width = frmCajas_Arqueo.Width
                        Select Case intTipo
                            Case 0
                                .Caption = "ALIVIO DE CAJA"
                                .Tag = intTipo
                                .ForeColor = vbBlack
                                .BackColor = &HE0E0E0
                            Case 1
                                .Caption = "ARQUEO DE CAJA"
                                .Tag = intTipo
                                .ForeColor = vbWhite
                                .BackColor = vbBlack
                            Case 2
                                .Caption = "INICIO DE CAJA"
                                .Tag = intTipo
                                .ForeColor = vbBlack
                                .BackColor = &HE0E0E0
                        End Select

                    End With
                    objMensajes.Ocultar()
                    .Show(vbModal)
                End With
            End If
            .Close()
        End With
        MatarObjeto(rstdatos)



        Exit Sub
err_ed:
        Resume Next
        MsgBox(Err.Description)
        objError.Capturar("Caja_Arqueo", Err)
        objMensajes.Ocultar()
        MatarObjeto(rstdatos)
        objError.Generar()
    End Sub

    Private Function CierresFiscal_Formulario(ByVal strParametros As String, ByVal boolImprimir As Boolean) As Boolean
        Dim boolOK As Boolean
        Load(frmLibroVentas_CierreZeta)
        With frmLibroVentas_CierreZeta
            .OK = False
            If Not SistemaPV.ImpFiscal.Utiliza Then boolImprimir = False
            .Imprimir = boolImprimir
            If boolImprimir Then
                .Manual = False
            Else
                If Not strParametros = "" Then .Datos_Colocar(strParametros)
            End If
            .Show(vbModal)
            boolOK = .OK
        End With
        Unload(frmLibroVentas_CierreZeta)
        CierresFiscal_Formulario = boolOK
    End Function

    Public Sub Cajero_Cambio()
        Dim Usuario As tUsuario
        If frmVentas.Grilla_Vacia Then
            Usuario = Usuario_Solicitar("Cambio de Cajero", SistemaPV.Configuracion.ValidacionPorTarjeta, SistemaPV.Usuario)
            If Not (Usuario.idOperador = 0) Then
                Sistema.Usuario = Usuario
                SistemaPV.Usuario = Usuario
                frmVentas.UsuarioDatos()
            End If
        Else
            MsgBox("No puede Cambiar de Cajero mientras poseea datos en la pantalla", vbCritical, "Atención")
        End If

    End Sub

    'Public Sub ManejarError(ByVal strOrigen As String)
    '    strOrigen = Err.Source & " -> " & strOrigen
    '    Err.Raise Err.Number, strOrigen, Err.Description
    'End Sub

    '// Funcion Publica que recibe un Texto al que desglosa en una matriz de resultado
    '// la separación la hace a partir del mCaracter
    Public Function TextoPorComas(ByVal mTexto As String, ByVal mCaracter As String, ByVal mTextoPorComas As Object) As Boolean
        Dim v As Integer, i As Integer, j As Integer
        Dim mvalor As String
        Erase mTextoPorComas
        mTexto = Trim(mTexto)
        mCaracter = Left(Trim(mCaracter), 1)
        v = 0 ' // V = Caracter que voy revisando
        i = 1 ' // I = Posicion Ultima separacion
        j = 0 ' // j = Posicion dentro de la matriz de resultados
        For v = 1 To Len(mTexto)
            mvalor = ""
            If Mid(mTexto, v, 1) = mCaracter Or (v = Len(mTexto)) Then ' // Encuentra separador o llega al final
                If v = 1 Then
                    i = 2
                Else
                    If v = Len(mTexto) Then v = v + 1
                    mvalor = Trim(Mid(mTexto, i, v - i))
                    If mvalor <> "" Then
                        mTextoPorComas(j) = mvalor
                        ' // no habia encontrado otro entonces se encontro algo
                        If Not TextoPorComas Then TextoPorComas = True
                    End If
                    i = v + 1
                    j = j + 1
                End If
            End If
        Next v
    End Function


    Public Sub MatarObjeto(ByRef objObjeto As Object)
        On Error GoTo e
        If Not (objObjeto Is Nothing) Then objObjeto = Nothing
        Exit Sub
e:
        Err.Clear()

    End Sub


    'Sub Main()
    '    On Error GoTo e:
    '    Sistema.Ruta = App.Path & "\"
    '    Error_Configurar
    '    Sistema_Configuracion
    '    Error_Configurar
    '    Exit Sub
    'e:
    '    If Not (objError Is Nothing) Then
    '        objError.Capturar "Libreria", Err
    '        objError.Generar
    '        MatarObjeto objError
    '    Else
    '        Err.Raise 1, "Libreria", "Error al iniciliar la Librería"
    '    End If
    'End Sub
    Private Sub Sistema_Configuracion()
        On Error GoTo e
        Dim strRutaApp As String
        strRutaApp = LCase(Trim(App.Path))
        If Right(strRutaApp, 1) <> "\" Then strRutaApp = strRutaApp & "\"
        With Sistema
            .Ruta = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos del sistema", "Ruta"))
            If Right(.Ruta, 1) <> "\" Then .Ruta = .Ruta & "\"
            If Trim(.Ruta) <> "" Then
                With .Base
                    .Nombre = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos de Base De Datos", "Nombre"))
                    .Tipo = Val(GetKeyVal(strRutaApp & "datos.ini", "datos de Base De Datos", "Tipo"))
                    .Catalogo = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos de Base De Datos", "Catalog"))
                    .DataSource = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos de Base De Datos", "DataSource"))
                    .User = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos de Base De Datos", "UserId"))
                    .Pass = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos de Base De Datos", "Pass"))
                End With
                .Copias.RutaArchivoBase = Trim(GetKeyVal(strRutaApp & "datos.ini", "Copias", "Archivo Base"))
                .Copias.RutaDirectorioArchivoBase = Trim(GetKeyVal(strRutaApp & "datos.ini", "Copias", "Directorio Archivo Base"))
                .Copias.RutaDirectorioCopias = Trim(GetKeyVal(strRutaApp & "datos.ini", "Copias", "Directorio Copias"))
                .RutaV = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos del sistema", "RutaV"))
                .RutaEnRed = Trim(GetKeyVal(strRutaApp & "datos.ini", "datos del sistema", "dirección en red")) & .Base.Nombre
                .ArchivoDeConfiguracionDelSistema = Trim(GetKeyVal(strRutaApp & "datos.ini", "Datos del Sistema ", "Archivo Configuracion Sistema"))
                With .Configuracion
                    .AuditarStock = (Val(GetKeyVal(strRutaApp & "datos.ini", "configuracion", "AuditarStock")) = 1)
                    .SolicitarClaveModificacionDirecta = (Val(GetKeyVal(strRutaApp & "datos.ini", "configuracion", "Clave Mod. Directa de Stock")) = 1)
                End With
            Else
                Err.Raise(1, "Rutas", "La ruta del sistema no es válida")
            End If
        End With
        Exit Sub
e:
        objError.Capturar("Obtencion de Rutas", Err)
        objError.Generar()
    End Sub

    'Private Sub Error_Configurar()
    '    Set objError = New Errores.clsError
    '    With objError
    '        .MomentoDelRegistro = al_Capturar
    '        .Plataforma = Win32
    '        .Sistema_Nombre = App.Title
    '        .Usuario = ""
    '        .RutaArchivoLog = Sistema.Ruta & "errorNegocios.log"
    '        .Limpiar
    '    End With
    'End Sub






    'Public Function Imprimir_ElegirImpresora(ByVal strImpresora As String) As Printer
    '    Dim prPrinter As Printer
    '    Dim strBuscar As String
    '    On Error GoTo e:
    '
    '    If Not (strImpresora = "") Then
    '        strBuscar = LCase(strImpresora)
    '        'Sistema.Configuracion.Impresion.ImpresoraDefault = Printer.DeviceName
    '    Else
    '        strBuscar = Printer.DeviceName
    '    End If
    '
    '    Set prPrinter = Printer
    '    For Each prPrinter In Printers
    '        If LCase(prPrinter.DeviceName) = strBuscar Then Exit For
    '    Next
    '    If prPrinter Is Nothing Then
    '        Set prPrinter = Printer
    '    Else
    '        Set Printer = prPrinter
    '    End If
    '    Exit Function
    'e:
    '    objError.Capturar "Imprimir_ElegirImpresora", Err
    '    MatarObjeto prPrinter
    '    objError.Generar
    'End Function



    Public Function FiltroPorUsuario(ByVal intId As Integer)
        '    Dim objUsuarios As clsUsuarios
        '    Dim rstDatos As adodb.Recordset
        '    Dim strRubros As String
        '
        '    Set objUsuarios = New clsUsuarios
        '    Set rstDatos = objUsuarios.Consultar(intId)
        '    With rstDatos
        '        If Not (.RecordCount = 0) Then FiltroPorUsuario = Trim("" & !rubrospermitidos)
        '        .Close
        '    End With
        '    MatarObjeto objUsuarios
        '    MatarObjeto rstDatos

    End Function


    'Public Sub Parametros_Agregar(ByRef strParametros As String, ByVal strCampo As String, ByVal strValor As String)
    '    If Not (Trim(strValor) = "") Then
    '        strParametros = Trim(strParametros) & strCampo & "=" & Trim(Replace(Replace(Replace(strValor, "|", ""), "=", ""), "'", "")) & "|"
    '    End If
    'End Sub

    'Public Function Parametros_Obtener(ByVal strParametros As String) As tParametros
    '    Dim T As tParametros
    '    Dim v As Integer
    '    ReDim Preserve T.item(0)
    '    With T
    '        .parametros = Split(strParametros, "|")
    '        .Cantidad = UBound(.parametros)
    '        For v = 0 To .Cantidad - 1
    '            If InStr(1, .parametros(v), "=") > 0 Then
    '                ReDim Preserve .item(UBound(.item) + 1)
    '                .item(UBound(.item) - 1).Id = Trim(Split(.parametros(v), "=")(0))
    '                .item(UBound(.item) - 1).Valor = (Trim(Split(.parametros(v), "=")(1)))
    '            End If
    '        Next v
    '    End With
    '    Parametros_Obtener = T
    'End Function

    'Public Function Parametros_ObtenerValor(parametros As tParametros, ByVal strParametroNombre As String) As tParametro
    '    Dim v As Integer
    '    Dim p As tParametro
    '    p.Id = ""
    '    With parametros
    '        For v = 0 To .Cantidad - 1
    '            If LCase(.item(v).Id) = LCase(strParametroNombre) Then
    '                p.Id = .item(v).Id
    '                p.Valor = .item(v).Valor
    '                v = .Cantidad + 1
    '            End If
    '        Next v
    '    End With
    '    Parametros_ObtenerValor = p
    'End Function

End Class
