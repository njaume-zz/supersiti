USE DEv_SUPER

-- CARGA DE UNIDADES
exec str_nuevo_unidad 0,'Kilogramo','Kg.',1
exec str_nuevo_unidad 0,'Gramo','Gr.',1
exec str_nuevo_unidad 0,'Pack','Pack',1
exec str_nuevo_unidad 0,'Unidad','Uni.',1
exec str_nuevo_unidad 0,'Paquete','Paq.',1


-- CARGA DE RUBROS

exec str_nuevo_RUBRO 0,'Limpieza','Limpieza',1
exec str_nuevo_RUBRO 0,'Almacén','Almacén',1
exec str_nuevo_RUBRO 0,'Perfumeria','Perfumeria',1
exec str_nuevo_RUBRO 0,'Bazar','Bazar',1
exec str_nuevo_RUBRO 0,'Carnicería','Carnicería',1
exec str_nuevo_RUBRO 0,'Fiambreria','Fiambreria',1
exec str_nuevo_RUBRO 0,'Lacteos','Lacteos',1
exec str_nuevo_RUBRO 0,'Panificados','Panificados',1
exec str_nuevo_RUBRO 0,'Verdulería','Verdulería',1
exec str_nuevo_RUBRO 0,'Congelados','Congelados',1
exec str_nuevo_RUBRO 0,'Pastas','Pastas',1
exec str_nuevo_RUBRO 0,'Bebidas','Bebidas',1


-- CARGA DE IMPUESTO AL VALOR AGREGADO (IVA)

exec str_nuevo_impuestoiva 0,'10.50',10.5,2,1
exec str_nuevo_impuestoiva 0,'21',21,1,1
exec str_nuevo_impuestoiva 0,'27',27,3,1

-- CARGA DE FAMILIAS

exec str_nuevo_familia 0,'Alcohol','Alcohol',1,1
exec str_nuevo_familia 0,'Alfombras para baño','Alfombras para baño',1,1
exec str_nuevo_familia 0,'Aprestos para ropa','Aprestos para ropa',1,1
exec str_nuevo_familia 0,'Bolsas','Bolsas',1,1
exec str_nuevo_familia 0,'Broches para ropa','Broches para ropa',1,1
exec str_nuevo_familia 0,'Cepillos','Cepillos',1,1
exec str_nuevo_familia 0,'Ceras','Ceras',1,1
exec str_nuevo_familia 0,'Desinfectantes','Desinfectantes',1,1
exec str_nuevo_familia 0,'Desodorantes','Desodorantes',1,1
exec str_nuevo_familia 0,'Detergentes','Detergentes',1,1
exec str_nuevo_familia 0,'Escarbadientes','Escarbadientes',1,1
exec str_nuevo_familia 0,'Escobas','Escobas',1,1
exec str_nuevo_familia 0,'Escobillones','Escobillones',1,1
exec str_nuevo_familia 0,'Esponjas','Esponjas',1,1
exec str_nuevo_familia 0,'Film para alimentos','Film para alimentos',1,1
exec str_nuevo_familia 0,'Guantes','Guantes',1,1
exec str_nuevo_familia 0,'Insecticidas','Insecticidas',1,1
exec str_nuevo_familia 0,'Jabones en pan','Jabones en pan',1,1
exec str_nuevo_familia 0,'Jabones en polvo','Jabones en polvo',1,1
exec str_nuevo_familia 0,'Jabones líquidos','Jabones líquidos',1,1
exec str_nuevo_familia 0,'Lana de acero','Lana de acero',1,1
exec str_nuevo_familia 0,'Lavandinas','Lavandinas',1,1
exec str_nuevo_familia 0,'Limpiadores','Limpiadores',1,1
exec str_nuevo_familia 0,'Palitas y recogedores','Palitas y recogedores',1,1
exec str_nuevo_familia 0,'Pañales','Pañales',1,1
exec str_nuevo_familia 0,'Paños','Paños',1,1
exec str_nuevo_familia 0,'Papel de alumnio','Papel de alumnio',1,1
exec str_nuevo_familia 0,'Papel de cocina','Papel de cocina',1,1
exec str_nuevo_familia 0,'Papel de uso higiénico','Papel de uso higiénico',1,1
exec str_nuevo_familia 0,'Plumeros','Plumeros',1,1
exec str_nuevo_familia 0,'Quitamanchas','Quitamanchas',1,1
exec str_nuevo_familia 0,'Repasadores','Repasadores',1,1
exec str_nuevo_familia 0,'Secadores','Secadores',1,1
exec str_nuevo_familia 0,'Servilletas de papel','Servilletas de papel',1,1
exec str_nuevo_familia 0,'Sopapas','Sopapas',1,1
exec str_nuevo_familia 0,'Suavizantes para ropa','Suavizantes para ropa',1,1
exec str_nuevo_familia 0,'','',1,1
exec str_nuevo_familia 0,'Aceites','Aceites',2,1
exec str_nuevo_familia 0,'Aderezos','Aderezos',2,1
exec str_nuevo_familia 0,'Arroz y legumbres','Arroz y legumbres',2,1
exec str_nuevo_familia 0,'Azúcar y edulcorantes','Azúcar y edulcorantes',2,1
exec str_nuevo_familia 0,'Conservas','Conservas',2,1
exec str_nuevo_familia 0,'Comidas rápidas','Comidas rápidas',2,1
exec str_nuevo_familia 0,'Dulces y mermeladas','Dulces y mermeladas',2,1
exec str_nuevo_familia 0,'Harina y féculas','Harina y féculas',2,1
exec str_nuevo_familia 0,'Infusiones','Infusiones',2,1
exec str_nuevo_familia 0,'Pastas secas','Pastas secas',2,1
exec str_nuevo_familia 0,'Postres, flanes, bizcochuelos y budines','Postres, flanes, bizcochuelos y budines',2,1
exec str_nuevo_familia 0,'Repostería','Repostería',2,1
exec str_nuevo_familia 0,'Sales','Sales',2,1
exec str_nuevo_familia 0,'Sopas y caldos','Sopas y caldos',2,1

exec str_nuevo_familia 0,'Desodorantes y perfumes','Desodorantes y perfumes',3,1
exec str_nuevo_familia 0,'Dentífricos','Dentífricos',3,1
exec str_nuevo_familia 0,'Cepillos de dientes','Cepillos de dientes',3,1
exec str_nuevo_familia 0,'Algodón','Algodón',3,1
exec str_nuevo_familia 0,'Alcohol fino','Alcohol fino',3,1
exec str_nuevo_familia 0,'Pañales','Pañales',3,1
exec str_nuevo_familia 0,'Jabón de tocador','Jabón de tocador',3,1
exec str_nuevo_familia 0,'Cremas','Cremas',3,1
exec str_nuevo_familia 0,'Shampoo y acondicionadores','Shampoo y acondicionadores',3,1

exec str_nuevo_familia 0,'Abrelatas','Abrelatas',4,1
exec str_nuevo_familia 0,'Bowls','Bowls',4,1
exec str_nuevo_familia 0,'Manga decoradora','Manga decoradora',4,1

exec str_nuevo_familia 0,'','',5,1

exec str_nuevo_familia 0,'Fiambres y envasados','Fiambres y envasados',6,1
exec str_nuevo_familia 0,'Quesos','Quesos',6,1
exec str_nuevo_familia 0,'Tapas y pascualinas','Tapas y pascualinas',6,1

exec str_nuevo_familia 0,'Crema de leche','Crema de leche',7,1
exec str_nuevo_familia 0,'Leches','Leches',7,1
exec str_nuevo_familia 0,'Mantecas','Mantecas',7,1
exec str_nuevo_familia 0,'Postres frescos','Postres frescos',7,1
exec str_nuevo_familia 0,'Queso Untable','Queso Untable',7,1
exec str_nuevo_familia 0,'Yogures','Yogures',7,1

exec str_nuevo_familia 0,'Galletitas','Galletitas',8,1
exec str_nuevo_familia 0,'Pan dulce','Pan dulce',8,1
exec str_nuevo_familia 0,'Pan rallado y rebozador','Pan rallado y rebozador',8,1
exec str_nuevo_familia 0,'Panes','Panes',8,1

exec str_nuevo_familia 0,'Frutas','Frutas',9,1
exec str_nuevo_familia 0,'Verduras y hortalizas','Verduras y hortalizas',9,1

exec str_nuevo_familia 0,'Comidas rápidas','Comidas rápidas',10,1
exec str_nuevo_familia 0,'Helados','Helados',10,1
exec str_nuevo_familia 0,'Verduras y hortalizas','Verduras y hortalizas',10,1

exec str_nuevo_familia 0,'','',11,1

exec str_nuevo_familia 0,'Aperitivos','Aperitivos',12,1
exec str_nuevo_familia 0,'Cervezas','Cervezas',12,1
exec str_nuevo_familia 0,'Espumantes','Espumantes',12,1
exec str_nuevo_familia 0,'Gaseosas','Gaseosas',12,1
exec str_nuevo_familia 0,'Licores','Licores',12,1
exec str_nuevo_familia 0,'Sidras y frizantes','Sidras y frizantes',12,1
exec str_nuevo_familia 0,'Vinos','Vinos',12,1
exec str_nuevo_familia 0,'Wiskies','Wiskies',12,1


-- =====CREACION DE USUARIO, ROLES Y MODULOS =============

--Padre de los siguientes 3 parrafos
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','PRODUCTOS','',1,0,1

exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','PRODUCTOS|ALTAS','~/Comercializacion/ProductoABM.aspx',1,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','PRODUCTOS|MODIFICACIONES','~/Comercializacion/ProductoABM.aspx',2,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','PRODUCTOS|BAJAS','~/Comercializacion/ProductoABM.aspx',3,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','PRODUCTOS|LISTADOS','~/Comercializacion/Productos.aspx',4,1,1

exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','RUBROS|ALTAS','~/Comercializacion/RubroABM.aspx',1,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','RUBROS|MODIFICACIONES','~/Comercializacion/RubroABM.aspx',2,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','RUBROS|BAJAS','~/Comercializacion/RubroABM.aspx',3,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','RUBROS|LISTADOS','~/Comercializacion/Rubros.aspx',4,1,1

exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','FAMILIAS|ALTAS','~/Comercializacion/FamiliaABM.aspx',1,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','FAMILIAS|MODIFICACIONES','~/Comercializacion/FamiliaABM.aspx',2,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','FAMILIAS|BAJAS','~/Comercializacion/FamiliaABM.aspx',3,1,1
exec str_nuevo_modulo 0,'Comercializacion','Comercialización de Productos, junto a sus rubros y familias','FAMILIAS|LISTADOS','~/Comercializacion/Familias.aspx',4,1,1


-- Tomar el valor del modulo Gestión Usuarios para los siguientes permisos
exec str_nuevo_modulo 0,'Gestión Usuarios','Gestión de los usuarios del sistema','USUARIOS','',1,0,1
-- select * from s_modulo where mod_tipo = 'usuarios'
exec str_nuevo_modulo 0,'Gestión Usuarios','Gestión de los usuarios del sistema','USUARIOS|ALTAS','~/Usuario/UsuarioABM.aspx',1,14,1
exec str_nuevo_modulo 0,'Gestión Usuarios','Gestión de los usuarios del sistema','USUARIOS|MODIFICACIONES','~/Usuario/UsuarioABM.aspx',1,14,1
exec str_nuevo_modulo 0,'Gestión Usuarios','Gestión de los usuarios del sistema','USUARIOS|BAJAS','~/Usuario/UsuarioABM.aspx',1,14,1
exec str_nuevo_modulo 0,'Gestión Usuarios','Gestión de los usuarios del sistema','USUARIOS|LISTADOS','~/Usuario/Usuarios.aspx',1,14,1


exec str_nuevo_modulo 0,'Ventas','Gestión de Ventas','VENTAS','',1,0,1

exec str_nuevo_modulo 0,'Ventas','Gestión de Ventas','VENTAS|ALTAS','',1,19,1
exec str_nuevo_modulo 0,'Ventas','Gestión de Ventas','VENTAS|LISTADOS','',1,19,1
exec str_nuevo_modulo 0,'Ventas','Gestión de Ventas','VENTAS|MODIFICACIONES','',1,19,1

exec str_nuevo_modulo 0,'Cajas','Gestión de Cajas como Puntos de Ventas','CAJAS','',1,0,1

exec str_nuevo_modulo 0,'Cajas','Gestión de Cajas como Puntos de Ventas','CAJAS|APERTURA','',1,23,1
exec str_nuevo_modulo 0,'Cajas','Gestión de Cajas como Puntos de Ventas','CAJAS|CIERRE','',1,23,1
exec str_nuevo_modulo 0,'Cajas','Gestión de Cajas como Puntos de Ventas','CAJAS|CIERRE Z','',1,23,1
exec str_nuevo_modulo 0,'Cajas','Gestión de Cajas como Puntos de Ventas','CAJAS|RETIROS','',1,23,1


-- Ingreso de Roles
exec str_nuevo_rol 0,'Administrador',1
exec str_nuevo_rol 0,'Supervisor',1
exec str_nuevo_rol 0,'Cajero',1

-- Ingresos de Rol-Modulo
-- Ingreso de Administrador
exec str_nuevo_rol_modulo 0,1,1,1
exec str_nuevo_rol_modulo 0,1,1,2
exec str_nuevo_rol_modulo 0,1,1,3
exec str_nuevo_rol_modulo 0,1,1,4
exec str_nuevo_rol_modulo 0,1,1,5
exec str_nuevo_rol_modulo 0,1,1,6
exec str_nuevo_rol_modulo 0,1,1,7
exec str_nuevo_rol_modulo 0,1,1,8
exec str_nuevo_rol_modulo 0,1,1,9
exec str_nuevo_rol_modulo 0,1,1,10
exec str_nuevo_rol_modulo 0,1,1,11
exec str_nuevo_rol_modulo 0,1,1,12
exec str_nuevo_rol_modulo 0,1,1,13
exec str_nuevo_rol_modulo 0,1,1,14
exec str_nuevo_rol_modulo 0,1,1,15
exec str_nuevo_rol_modulo 0,1,1,16
exec str_nuevo_rol_modulo 0,1,1,17
exec str_nuevo_rol_modulo 0,1,1,18
exec str_nuevo_rol_modulo 0,1,1,19
exec str_nuevo_rol_modulo 0,1,1,20
exec str_nuevo_rol_modulo 0,1,1,21
exec str_nuevo_rol_modulo 0,1,1,22
exec str_nuevo_rol_modulo 0,1,1,23
exec str_nuevo_rol_modulo 0,1,1,24
exec str_nuevo_rol_modulo 0,1,1,25
exec str_nuevo_rol_modulo 0,1,1,26
exec str_nuevo_rol_modulo 0,1,1,27

-- Ingreso de supervisor
exec str_nuevo_rol_modulo 0,1,2,1
exec str_nuevo_rol_modulo 0,1,2,2
exec str_nuevo_rol_modulo 0,1,2,3
exec str_nuevo_rol_modulo 0,1,2,4
exec str_nuevo_rol_modulo 0,1,2,5
exec str_nuevo_rol_modulo 0,1,2,6
exec str_nuevo_rol_modulo 0,1,2,7
exec str_nuevo_rol_modulo 0,1,2,8
exec str_nuevo_rol_modulo 0,1,2,9
exec str_nuevo_rol_modulo 0,1,2,10
exec str_nuevo_rol_modulo 0,1,2,11
exec str_nuevo_rol_modulo 0,1,2,12
exec str_nuevo_rol_modulo 0,1,2,13
exec str_nuevo_rol_modulo 0,1,2,14
exec str_nuevo_rol_modulo 0,1,2,15
exec str_nuevo_rol_modulo 0,1,2,16
exec str_nuevo_rol_modulo 0,1,2,17
exec str_nuevo_rol_modulo 0,1,2,18
exec str_nuevo_rol_modulo 0,1,2,19
exec str_nuevo_rol_modulo 0,1,2,20
exec str_nuevo_rol_modulo 0,1,2,21
exec str_nuevo_rol_modulo 0,1,2,22
exec str_nuevo_rol_modulo 0,1,2,23
exec str_nuevo_rol_modulo 0,1,2,24
exec str_nuevo_rol_modulo 0,1,2,25
exec str_nuevo_rol_modulo 0,1,2,26
exec str_nuevo_rol_modulo 0,1,2,27

-- Ingreso de Cajero
exec str_nuevo_rol_modulo 0,1,3,1
exec str_nuevo_rol_modulo 0,0,3,2
exec str_nuevo_rol_modulo 0,0,3,3
exec str_nuevo_rol_modulo 0,0,3,4
exec str_nuevo_rol_modulo 0,1,3,5
exec str_nuevo_rol_modulo 0,0,3,6
exec str_nuevo_rol_modulo 0,0,3,7
exec str_nuevo_rol_modulo 0,0,3,8
exec str_nuevo_rol_modulo 0,1,3,9
exec str_nuevo_rol_modulo 0,0,3,10
exec str_nuevo_rol_modulo 0,0,3,11
exec str_nuevo_rol_modulo 0,0,3,12
exec str_nuevo_rol_modulo 0,1,3,13
exec str_nuevo_rol_modulo 0,0,3,14
exec str_nuevo_rol_modulo 0,0,3,15
exec str_nuevo_rol_modulo 0,0,3,16
exec str_nuevo_rol_modulo 0,0,3,17
exec str_nuevo_rol_modulo 0,0,3,18
exec str_nuevo_rol_modulo 0,1,3,19
exec str_nuevo_rol_modulo 0,1,3,20
exec str_nuevo_rol_modulo 0,1,3,21
exec str_nuevo_rol_modulo 0,0,3,22
exec str_nuevo_rol_modulo 0,1,3,23
exec str_nuevo_rol_modulo 0,1,3,24
exec str_nuevo_rol_modulo 0,1,3,25
exec str_nuevo_rol_modulo 0,0,3,26
exec str_nuevo_rol_modulo 0,0,3,27

-- Ingreso de usuarios

exec str_nuevo_usuario 0,'Admin','9874123','maxiadad@gmail.com',1,1