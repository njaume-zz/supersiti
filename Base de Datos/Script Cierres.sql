CREATE PROCEDURE [dbo].[STR_RECUPERA_IMPORTES_VENTA](
	@CAJ_ID AS INT = 0
)
AS
/*
DECLARE @CAJ_ID AS INT
SET @CAJ_ID = 1
*/
BEGIN
	WITH CTE_FORMAPAGOS (IMPORTES, FORMAS, NOMBRE,CAJA)
		AS
		(
			SELECT CASE COM.FOP_ID
						WHEN 1 THEN SUM (COM_TOTALFACTRADO)
						WHEN 2 THEN SUM (COM_TOTALFACTRADO)
						WHEN 3 THEN SUM (COM_TOTALFACTRADO)
						WHEN 4 THEN SUM (COM_TOTALFACTRADO)
					END AS TOTAL,COM.FOP_ID,FOP.FOP_NOMBRE,COM.CAJ_ID
			FROM V_COMPROBANTE COM INNER JOIN V_FORMAPAGO FOP
				ON COM.FOP_ID = FOP.FOP_ID
			WHERE (@CAJ_ID = 0 OR (COM.CAJ_ID = @CAJ_ID))
			GROUP BY COM.FOP_ID,FOP.FOP_NOMBRE,COM.CAJ_ID
		)

	SELECT IMPORTES,NOMBRE,CAJA 
	FROM CTE_FORMAPAGOS 
END

GO 

CREATE PROCEDURE [dbo].[STR_RECUPERA_IMPORTES_CIERRE](
	@CAJ_ID AS INT = 0
)
AS

BEGIN 

WITH CTE_CIERRE (IMPORTES,TIPOIMPORTE,NOMBRE,CAJA)
	AS
	(
		SELECT CASE CAE.CAE_ID
					WHEN 1 THEN SUM(CAM.CAM_IMPORTE) --APERTURA
					WHEN 2 THEN SUM(CAM.CAM_IMPORTE) --RETIIRO
					WHEN 3 THEN SUM(CAM.CAM_IMPORTE) --CIERRA X
					WHEN 4 THEN SUM(CAM.CAM_IMPORTE) --CIERRE Z
				END TOTAL, CAE.CAE_ID,CAE.CAE_NOMBRE,CAM.CAJ_ID
		FROM V_CAJA_MOVIMIENTOS CAM INNER JOIN V_CAJA_TIPO_MOVIMIENTO CAE
					ON CAM.CAE_ID = CAE.CAE_ID 
		WHERE (@CAJ_ID = 0 OR (CAM.CAJ_ID = @CAJ_ID))
		GROUP BY CAE.CAE_ID,CAE.CAE_NOMBRE,CAM.CAJ_ID
	)
						
SELECT IMPORTES,NOMBRE,CAJA
FROM CTE_CIERRE  

END

GO