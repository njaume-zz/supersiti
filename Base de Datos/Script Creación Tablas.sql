
-- CREATE DATABASE DES_SUPER
--	USE DES_SUPER
-- V:Ventas, C:Comercializaci�n, A:Tablas auxiliares
-- S:Sistema
-- =============================================================================================
--                      USUARIO, MODULOS Y ROLES DEL SISTEMA
-- =============================================================================================


CREATE TABLE [dbo].[S_MODULO](
	[MOD_ID] [INT] IDENTITY (1,1) NOT NULL,
	[MOD_NOMBRE] [VARCHAR](100) NOT NULL,
	[MOD_DESCRIPCION] [VARCHAR](250),
	[MOD_TIPO] [VARCHAR] (100),
	[MOD_URL] [VARCHAR](250),
	[MOD_ORDEN] [INT],
	[MOD_IDPADRE] [INT],
	[MOD_ESTADO] [INT],
	[MOD_FECHAALTA] [DATETIME],
	PRIMARY KEY ([MOD_ID])
)
go
CREATE TABLE [dbo].[S_ROL](
	[ROL_ID] [INT] IDENTITY (1,1) NOT NULL,
	[ROL_NOMBRE] [VARCHAR] (100),
	[ROL_ESTADO] [INT],
	[ROL_FECHAALTA] [DATETIME],
	PRIMARY KEY ([ROL_ID])
)
go
CREATE TABLE [dbo].[S_USUARIO](
	[USU_ID] [INT] IDENTITY (1,1) NOT NULL,
	[USU_NOMBRE] [VARCHAR] (100) NOT NULL,
	[USU_PASSWORD] [VARCHAR] (250) NOT NULL,
	[USU_MAIL] [VARCHAR](200),
	[USU_ESTADO] [INT],
	[USU_FECHAALTA] [DATETIME],
	[ROL_ID] [INT],
	PRIMARY KEY ([USU_ID]),
	CONSTRAINT [FK_USUARIO_ROL] FOREIGN KEY ([ROL_ID]) REFERENCES [S_ROL] ([ROL_ID])
)
go
CREATE TABLE [dbo].[S_ROL_MODULO](
	[ROM_ID] [INT] IDENTITY (1,1) NOT NULL,
	[ROM_ESTADO] [INT],
	[ROL_ID] [INT],
	[MOD_ID] [INT],
	PRIMARY KEY ([ROM_ID]),
	CONSTRAINT [FK_ROL_MODULO_ROLES] FOREIGN KEY ([ROL_ID]) REFERENCES [S_ROL] ([ROL_ID]),
	CONSTRAINT [FK_ROL_MODULO_MODULOS] FOREIGN KEY ([MOD_ID]) REFERENCES [S_MODULO] ([MOD_ID])
)
go
-- ====================================================================================
--                                  INICIO DE SCRIPT PARA CAJAS
-- ====================================================================================
-- ESTADO DE CAJA CONTIENE LOS ESTADOS APERTURA, CIERRE X, CIERRE Z
CREATE TABLE [dbo].[V_CAJA_TIPO_MOVIMIENTO](
	[CAE_ID] [INT] IDENTITY(1,1) NOT NULL,
	[CAE_NOMBRE] [VARCHAR] (80) NOT NULL,
	[CAE_ESTADO] [INT],
	[CAE_FECHAALTA] [DATETIME],
	PRIMARY KEY ([CAE_ID])
)
go

CREATE TABLE [dbo].[V_CAJA](
	[CAJ_ID] [INT] IDENTITY (1,1) NOT NULL,
	[CAJ_NUMERO] [INT] NOT NULL,
	[CAJ_ESTADO] [INT],
	[CAJ_FECHAALTA] [DATETIME],
	PRIMARY KEY ([CAJ_ID])
)
go
CREATE TABLE [dbo].[V_CAJA_MOVIMIENTOS](
	[CAM_ID] [INT] IDENTITY (1,1) NOT NULL,
	[CAM_IMPORTE] [NUMERIC] (18,2),
	[CAE_ID] [INT], -- CAJA ESTADO (ABIERTA, CERRADA, CIERRE Z, ETC)
	[CAM_FECHA] [DATETIME],
	[USU_ID] [INT],
	[CAM_ESTADO] [INT],
	[CAM_FECHAALTA][DATETIME],
	PRIMARY KEY ([CAM_ID]),
	CONSTRAINT [FK_CAJA_TIPO_MOVIMIENTOS] FOREIGN KEY ([CAE_ID]) REFERENCES [V_CAJA_TIPO_MOVIMIENTO] ([CAE_ID]),
	CONSTRAINT [FK_CAJA_MOVIMIENTOS_USUARIO] FOREIGN KEY ([USU_ID]) REFERENCES [S_USUARIO] ([USU_ID])
)
go

CREATE TABLE [dbo].[V_CAJA_CIERRES](
	[CAC_ID] [INT] IDENTITY (1,1) NOT NULL,
	[CAC_EFECTIVO] [NUMERIC] (18,2),
	[CAC_BONO] [NUMERIC] (18,2),
	[CAC_TARJETA] [NUMERIC] (18,2),
	[CAC_CREDITO] [NUMERIC] (18,2),
	[CAC_ESTADO] [INT],
	[CAC_FECHAALTA] [DATETIME],
	PRIMARY KEY ([CAC_ID])	
)
go

CREATE TABLE [dbo].[C_TIPO_COMRPOBANTE](
	[CTC_ID] [INT] IDENTITY (1,1) NOT NULL,
	[CTC_CODIGO] [VARCHAR](2) NOT NULL ,
	[CTC_DESCRIPCION] [VARCHAR](100) NOT NULL,
	[CTC_SIGLA] [VARCHAR] (10) NOT NULL,
	[CTC_LETRA] [VARCHAR] (5) NOT NULL,
	[CTC_SIGNO] [CHAR] (1),
	PRIMARY KEY ([CTC_ID])
)

GO

CREATE TABLE [dbo].[C_FORMAPAGO](
	[FOP_ID] [INT] IDENTITY (1,1) NOT NULL,
	[FOP_NOMBRE] [VARCHAR] (100) NOT NULL,
	[FOP_DESCRIPCION] [VARCHAR] (250),
	[FOP_SIGLA] [VARCHAR] (10),
	[FOP_ESTADO] [INT],
	[FOP_FECHAALTA] [DATETIME],
	PRIMARY KEY ([FOP_ID]),
 )

CREATE TABLE [dbo].[V_COMPROBANTE](
	[COM_ID] [INT] IDENTITY(1,1) NOT NULL,
	[COM_PTOVTA] [VARCHAR](4) NOT NULL, -- DE ESTO DEPENDE COMO TOMAMOS LAS CAJAS
	[COM_NROEMITIDO] [VARCHAR](8) NOT NULL,
	[COM_FECHA] [DATETIME] NOT NULL,
	[COM_IMPORTEGRAVADO][NUMERIC] (18,2), -- ES EL CALCULO DEL PORCENTAJE DE IVA, SOBRE EL IMPORTE TOTAL
	[COM_IMPORTENOGRAVADO][NUMERIC](18,2), -- ES EL RESTO ENTRE TOTAL MENOS EL GRAVADO
	[COM_IVAFACTURADO] [NUMERIC] (18,2), -- PORCENTAJE DE IVA FACTURADO.
	[COM_TOTALFACTRADO] [NUMERIC] (18,2),
	[COM_IMPRESO] [CHAR](1),	-- DETERMINA SI SE IMPRIMI� EL COMPROBANTE (S/N)
	[COM_USUNOMBRE] [VARCHAR] (100),
	[COM_CLIRAZONSOCIAL] [VARCHAR](250),
	[COM_CLIDOMICILIO]  [VARCHAR] (250),
	[COM_CLITELEFONO] [VARCHAR] (250),
	[COM_CLIDOMICILIOENTREGA] [VARCHAR] (500),
	[COM_CLICUIT] [VARCHAR] (20),
	[COM_CLIINGRESOBRUTO] [VARCHAR] (30),
	[COM_RAZONSOCIALPROPIO] [VARCHAR](250),
	[COM_DOMICILIOPROPIO]  [VARCHAR] (250),
	[COM_TELEFONOPROPIO] [VARCHAR] (250),
	[COM_CUITPROPIO] [VARCHAR] (20),
	[COM_INGRESOBRUTOPROPIO] [VARCHAR] (30),
	[CTC_ID] [INT], --TIPO DE COMPROBANTE
	[CAJ_ID] [INT],
	[FOP_ID] [INT],-- FORMA DE PAGO
	PRIMARY KEY ([COM_ID]),
	CONSTRAINT [FK_COMPROBANTE_TIPO] FOREIGN KEY ([CTC_ID]) REFERENCES (V_TIPO_COMPROBANTE) ([CTC_ID]),
	CONSTRAINT [FK_COMPROBANTE_CAJA] FOREIGN KEY ([CAJ_ID]) REFERENCES (V_CAJA) ([CAJ_ID]),
	CONSTRAINT [FK_COMPROBANTE_FORMAPAGO] FOREIGN KEY ([FOP_ID]) REFERENCES (V_FORMAPAGO) ([FOP_ID])

)

GO

CREATE TABLE [dbo].[V_COMPROBANTE_DETALLE](
	[COD_ID] [INT] IDENTITY (1,1),
	[COD_PROCODIGO] [VARCHAR] (50),
	[COD_PRONOMBRE] [VARCHAR](100),
	[COD_PROPCIOUNITARIO] [NUMERIC](18,2),
	[COD_PROCANTIDAD] [INT],
	[COD_PRECIOCANTIDAD] [NUMERIC] (18,2),
	[COD_IVA] [NUMERIC] (18,2), -- CUANTO ES EL VALOR DE IVA DEL PRODUCTO
	[COM_ID] [INT],
	PRIMARY KEY ([COD_ID]),
	CONSTRAINT [FK_COMPROBANTE_DETALLE_COMP] FOREIGN KEY ([COM_ID]) REFERENCES (V_COMPROBANTE) ([COM_ID])
)

/*CREATE TABLE AUD_AUDITORIA(
	
)
*/

-- ====================================================================================
--                    INICIO SCRIPT PARA COMERCIALIZACION
-- ====================================================================================
CREATE TABLE [dbo].[C_UNIDAD](
	[UNI_ID] [INT] IDENTITY (1,1) NOT NULL,
	[UNI_NOMBRE] [VARCHAR](100) NOT NULL,
	[UNI_SIGLA] [VARCHAR](10),
	[UNI_ESTADO] [INT],
	[UNI_FECHAALTA] [DATETIME],
	[UNI_FECHAMODIFICADA] [DATETIME],
	PRIMARY KEY ([UNI_ID])
)
go

CREATE TABLE [dbo].[C_RUBRO](
	[RUB_ID] [INT] IDENTITY (1,1) NOT NULL,
	[RUB_NOMBRE] [VARCHAR](100) NOT NULL,
	[RUB_DESCRIPCION] [VARCHAR](250),
	[RUB_ESTADO] [INT] DEFAULT 1,
	[RUB_FECHAALTA] [DATETIME] ,
	[RUB_FECHAMODIFICADA] [DATETIME],
	PRIMARY KEY ([RUB_ID])
)
go

CREATE TABLE [dbo].[C_FAMILIA](
	[FAM_ID] [INT] IDENTITY (1,1) NOT NULL,
	[FAM_NOMBRE] [VARCHAR](100) NOT NULL,
	[FAM_DESCRIPCION] [VARCHAR](250),
	[RUB_ID] [INT],
	[FAM_ESTADO] [INT] DEFAULT 1,
	[FAM_FECHAALTA] [DATETIME],
	[FAM_FECHAMODIFICADA] [DATETIME],
	PRIMARY KEY ([FAM_ID]),
	CONSTRAINT [PK_RUBRO_FAMILIA] FOREIGN KEY ([RUB_ID]) REFERENCES [C_RUBRO] ([RUB_ID])
)
go

CREATE TABLE [dbo].[C_PRODUCTO](
	[PRO_ID] [INT] IDENTITY (1,1) NOT NULL,
	[PRO_CODIGO] [VARCHAR](20),
	[PRO_CODIGO_BARRA] [VARCHAR](50),
	[PRO_CODIGO_PROVEEDOR] [VARCHAR](100), -- C�DIGO INTERNO DEL PROVEEDOR
	[PRO_NOMBRE] [VARCHAR](250) NOT NULL,
	[PRO_NOMBREETIQUETA] [VARCHAR](250) NOT NULL,
	[PRO_DESCRIPCION] [VARCHAR](500),
	[PRO_MARCA] [VARCHAR](100),
	[PRO_PRECIOCOSTO] [NUMERIC] (18,2),
	[PRO_PRECIOCOMPRA] [NUMERIC](18,2),
	[PRO_IVA] [NUMERIC] (18,2),
	[PRO_IMPUESTOINTERNO] [NUMERIC](18,2),
	[PRO_IDPADRE] [INT],
	[PRO_MINIMO] [INT],
	[PRO_MAXIMO] [INT],
	[PRO_ALERTA] [INT],	
	[PRO_PESABLE] [INT],
	[UNC_ID] [INT], -- UNIDAD DE COMPRA
	[UNV_ID] [INT], -- UNIDAD DE VENTA
	[FAM_ID] [INT], -- RUBRO
	[RUB_ID] [INT], -- FAMILIA
	[PRO_ESTADO] [INT] DEFAULT 1,
	PRIMARY KEY ([PRO_ID]),
	CONSTRAINT [PK_PRODUCTO_UNIDAD_COMPRA] FOREIGN KEY ([UNC_ID]) REFERENCES [C_UNIDAD] ([UNI_ID]),
	CONSTRAINT [PK_PRODUCTO_UNIDAD_VENTA] FOREIGN KEY ([UNV_ID]) REFERENCES [C_UNIDAD] ([UNI_ID]),
	CONSTRAINT [PK_PRODUCTO_RUBRO] FOREIGN KEY ([RUB_ID]) REFERENCES [C_RUBRO] ([RUB_ID]),
	CONSTRAINT [PK_PRODUCTO_FAMILIA] FOREIGN KEY ([FAM_ID]) REFERENCES [C_FAMILIA] ([FAM_ID])
)
go

CREATE TABLE [dbo].[C_TIPOS_PRECIOS](
	[TPR_ID] [INT] IDENTITY (1,1) NOT NULL,
	[TPR_CODIGO] [VARCHAR] (10) NOT NULL,
	[TPR_NOMBRE] [VARCHAR] (100) NOT NULL,
	[TPR_DESCRIPCION] [VARCHAR](250),
	[TPR_ESTADO] [INT] DEFAULT 1,
	[TPR_FECHAALTA] [DATETIME] DEFAULT GETDATE(),
	PRIMARY KEY ([TPR_ID])
)

GO

CREATE TABLE [dbo].[C_LISTA_PRECIO](
	[LPR_ID] [INT] IDENTITY (1,1) NOT NULL,
	[PRO_ID] [INT] NOT NULL,
	[TPR_ID] [INT] NOT NULL,
	[LPR_PRECIO] [NUMERIC] (18,2),
	[LPR_PRECIOXCANTIDAD] [NUMERIC] (18,2),
	[LPR_ESTADO] [INT] DEFAULT 1,
	[LPR_FECHAALTA] [DATETIME] DEFAULT GETDATE(),
	PRIMARY KEY ([LPR_ID]),
	CONSTRAINT [FK_LISTA_PRECIO_PRODUCTO] FOREIGN KEY ([PRO_ID]) REFERENCES [C_PRODUCTO] ([PRO_ID]),
	CONSTRAINT [FK_LISTA_PRECIO_TIPOLISTA] FOREIGN KEY ([TPR_ID]) REFERENCES [C_TIPOS_PRECIOS] ([TPR_ID])
)

GO

/*
	========================= TABLAS AUXILIARES ==========================
*/
--IMPUESTO IVA SER� EL PORCENTAJE DE IVA, CATEGOR�A IVA SER�, POR EJEMPLO, RESPONSABLE INSCRIPTO
CREATE TABLE [dbo].[A_IMPUESTOIVA](
	[IVA_ID] [INT] IDENTITY (1,1) NOT NULL,
	[IVA_NOMBRE] [VARCHAR](250) NOT NULL,
	[IVA_TASA] [NUMERIC](18,2), -- PORCENTAJE DE IVA 21, 10.5, 27
	[IVA_ORDEN] [INT],
	[IVA_FECHAALTA] [DATETIME],
	[IVA_ESTADO] [INT] DEFAULT 1,
	PRIMARY KEY ([IVA_ID])
)
go



CREATE TABLE [dbo].[A_COMPROBANTES_VENTA] (
    [COV_ID] [INT] IDENTITY (1,1) NOT NULL,
	[COV_CODIGO] [INT],
    [COV_DESCRIPCION] [VARCHAR](30),
    [COV_SIGLA] [VARCHAR] (10),
    [COV_Letra] [VARCHAR] (1),
    [COV_Signo] [VARCHAR] (1),
    [COV_UltimoNro] [VARCHAR](13),  -- ultimo numero entregado al cliente.
    PRIMARY KEY ([COV_ID])
)

 go

CREATE TABLE [dbo].[A_PUNTO_VENTA] (
  [PTV_ID] [INT] IDENTITY(1,1) NOT NULL,
  [PTV_CODIGO] [VARCHAR](4),
  [PTV_NOMBRE] [VARCHAR](50),
  [PTV_DESCRIPCION] [VARCHAR](250),
  [PTV_SIGLA] [VARCHAR](8),
  PRIMARY KEY ([PTV_ID])
)

go

CREATE TABLE [dbo].[A_SITUACION_IMPOSITIVA] (
  [SII_ID] [INT] identity (1,1 )NOT NULL ,
  [SII_CLAVE] [varchar](2) NOT NULL,
  [SII_DESCRIPCION][varchar](45) NOT NULL,
  [SII_SIGLA] [varchar](5) NOT NULL,
  PRIMARY KEY  ([SII_ID])
)

 go
