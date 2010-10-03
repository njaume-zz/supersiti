Module Funciones


    Public Sub Manejador_Errores(ByVal Lugar As String, ByVal ex As Exception)
        MsgBox("Se ha producido un error en: " & Lugar & vbCrLf & _
               "Descripción del error: " & ex.ToString & vbCrLf, MsgBoxStyle.Critical, "ERROR CRITICO")
        'Grabar log de errores
    End Sub

    Public Function ValorNumerico(ByVal _char As Char) As String
        Dim str As String

        Return str
    End Function
End Module
