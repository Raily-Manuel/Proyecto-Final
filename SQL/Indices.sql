-- ============================================================
-- GASOLINERODB - ÍNDICES
-- ============================================================
 
-- CATEGORIA
CREATE NONCLUSTERED INDEX IX_Categoria_Estado  ON Categoria(Estado);
CREATE NONCLUSTERED INDEX IX_Categoria_Nombre  ON Categoria(NomCategoria);
 
-- PRODUCTOS
CREATE NONCLUSTERED INDEX IX_Productos_id_Categoria ON Productos(id_Categoria);
CREATE NONCLUSTERED INDEX IX_Productos_Estado       ON Productos(Estado);
CREATE NONCLUSTERED INDEX IX_Productos_Nombre       ON Productos(NomProducto);
 
-- MANGUERAS
CREATE NONCLUSTERED INDEX IX_Mangueras_id_Producto ON Mangueras(id_Producto);
CREATE NONCLUSTERED INDEX IX_Mangueras_Estado      ON Mangueras(Estado);
 
-- ISLAS
CREATE NONCLUSTERED INDEX IX_Islas_Estado ON Islas(Estado);
 
-- EMPLEADO
CREATE NONCLUSTERED INDEX IX_Empleado_Turno  ON Empleado(Turno);
CREATE NONCLUSTERED INDEX IX_Empleado_Cargo  ON Empleado(Cargo);
CREATE NONCLUSTERED INDEX IX_Empleado_Estado ON Empleado(Estado);
 
-- EMPLEADO_ISLA
CREATE NONCLUSTERED INDEX IX_Empleado_Isla_id_Empleado   ON Empleado_Isla(id_Empleado);
CREATE NONCLUSTERED INDEX IX_Empleado_Isla_id_Isla        ON Empleado_Isla(id_Isla);
CREATE NONCLUSTERED INDEX IX_Empleado_Isla_Empleado_Isla  ON Empleado_Isla(id_Empleado, id_Isla);
 
-- ISLA_MANGUERA
CREATE NONCLUSTERED INDEX IX_Isla_Manguera_id_Manguera   ON Isla_Manguera(id_Manguera);
CREATE NONCLUSTERED INDEX IX_Isla_Manguera_id_Isla        ON Isla_Manguera(id_Isla);
CREATE NONCLUSTERED INDEX IX_Isla_Manguera_Manguera_Isla  ON Isla_Manguera(id_Manguera, id_Isla);
