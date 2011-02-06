Public Class clsFormaPago

#Region "Variables"
Private _fop_id As System.Int32
Private _fop_nombre As System.String
Private _fop_descripcion As System.String
Private _fop_sigla As System.String
Private _fop_estado As System.Int32
Private _fop_fechaalta As System.DateTime
#End Region
#Region "Propiedades"

Public Sub New()
End Sub


Public Property FOP_ID() As Int32
	Get
		Return _FOP_ID
	End Get
	Set (ByVal Value As Int32)
		_FOP_ID = Value
	End Set
End Property
Public Property FOP_NOMBRE() As String
	Get
		Return _FOP_NOMBRE
	End Get
	Set (ByVal Value As String)
		_FOP_NOMBRE = Value
	End Set
End Property
Public Property FOP_DESCRIPCION() As String
	Get
		Return _FOP_DESCRIPCION
	End Get
	Set (ByVal Value As String)
		_FOP_DESCRIPCION = Value
	End Set
End Property
Public Property FOP_SIGLA() As String
	Get
		Return _FOP_SIGLA
	End Get
	Set (ByVal Value As String)
		_FOP_SIGLA = Value
	End Set
End Property
Public Property FOP_ESTADO() As Int32
	Get
		Return _FOP_ESTADO
	End Get
	Set (ByVal Value As Int32)
		_FOP_ESTADO = Value
	End Set
End Property
Public Property FOP_FECHAALTA() As DateTime
	Get
		Return _FOP_FECHAALTA
	End Get
	Set (ByVal Value As DateTime)
		_FOP_FECHAALTA = Value
	End Set
End Property
#End Region
End Class

