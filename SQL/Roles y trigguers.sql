--Roles y triggueers

-- Rol Administrador: acceso total
CREATE ROLE rol_Gerente;
GO

-- Rol Supervisor: lectura total, sin eliminar
CREATE ROLE rol_Supervisor;
GO


--permisos administrdor

GRANT SELECT, INSERT, UPDATE, DELETE ON Categoria       TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Productos       TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Mangueras       TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Islas           TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Empleado        TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Empleado_Isla   TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Isla_Manguera   TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Ventas          TO rol_Gerente;
GRANT SELECT, INSERT, UPDATE, DELETE ON Detalle_Venta   TO rol_Gerente;


-- permisos supervisor
-- (puede ver todo y actualizar, pero NO eliminar)

GRANT SELECT, INSERT, UPDATE ON Categoria       TO rol_Supervisor;
GRANT SELECT, INSERT, UPDATE ON Productos       TO rol_Supervisor;
GRANT SELECT, UPDATE ON Mangueras       TO rol_Supervisor;
GRANT SELECT, UPDATE ON Islas           TO rol_Supervisor;
GRANT SELECT ON Empleado        TO rol_Supervisor;
GRANT SELECT, INSERT, UPDATE ON Ventas          TO rol_Supervisor;
GRANT SELECT, INSERT, UPDATE ON Detalle_Venta   TO rol_Supervisor;
GO

-- asignar usuario ejemplos

-- EXEC sp_addrolemember 'rol_Administrador', 'usuario_admin';
-- EXEC sp_addrolemember 'rol_Despachador',   'usuario_despachador';
-- EXEC sp_addrolemember 'rol_Supervisor',    'usuario_supervisor';

-- Calcula galones (FNL - INL) y total (galones * precio) al insertar
CREATE TRIGGER trg_Calcular_Detalle
ON Detalle_Venta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dv
    SET 
        dv.Galones = i.LecturaFNL - i.LecturaINL,
        dv.Total   = (i.LecturaFNL - i.LecturaINL) * i.Precio
    FROM Detalle_Venta dv
    INNER JOIN inserted i ON dv.id_Detalle = i.id_Detalle;
END;
GO

-- Actualiza la LecturaINL de la manguera con el último valor FNL registrado
CREATE TRIGGER trg_Actualizar_Lectura_Manguera
ON Detalle_Venta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE m
    SET m.LecturaINL = i.LecturaFNL
    FROM Mangueras m
    INNER JOIN inserted i ON m.id_Manguera = i.id_Manguera;
END;
GO

-- Trigger que registra cuando se elimina un empleado
CREATE TRIGGER trg_Auditoria_Eliminar_Empleado
ON Empleado
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Empleado (id_Empleado, Nombre, Cedula)
    SELECT id_Empleado, Nombre, Cedula
    FROM deleted;
END;
GO

-- Impide modificar una venta que ya está en estado 'Cerrada'
-- Primero agrega la columna Estado a Ventas si no la tiene:
ALTER TABLE Ventas ADD Estado VARCHAR(20) DEFAULT 'Abierta';
GO

CREATE TRIGGER trg_Proteger_Venta_Cerrada
ON Ventas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM deleted d
        WHERE d.Estado = 'Cerrada'
    )
    BEGIN
        RAISERROR('No se puede modificar una venta que ya está cerrada.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- Evita registrar una lectura final menor a la inicial (error de digitación)
CREATE TRIGGER trg_Validar_Lecturas
ON Detalle_Venta
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM inserted
        WHERE LecturaFNL <= LecturaINL
    )
    BEGIN
        RAISERROR('La lectura final debe ser mayor que la lectura inicial.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

