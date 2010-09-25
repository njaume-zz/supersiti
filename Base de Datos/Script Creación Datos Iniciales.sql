--USE DES_SUPER

-- CARGA DE UNIDADES
exec str_nuevo_unidad 0,'Kilogramo','Kg.',1
exec str_nuevo_unidad 0,'Gramo','Gr.',1
exec str_nuevo_unidad 0,'Pack','Pack',1
exec str_nuevo_unidad 0,'Unidad','Uni.',1
exec str_nuevo_unidad 0,'Paquete','Paq.',1


-- CARGA DE IMPUESTO AL VALOR AGREGADO (IVA)

exec str_nuevo_impuestoiva 0,'10.50',10.5,2,1
exec str_nuevo_impuestoiva 0,'21',21,1,1
exec str_nuevo_impuestoiva 0,'27',27,3,1

-- CARGA DE FAMILIAS
exec str_nuevo_familia 0,'Ceras','Ceras para piso y otros tipos de materiales',1

-- CARGA DE RUBROS