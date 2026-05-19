USE GasolineraDB;
GO

-- ============================================================
-- REPORTE 2: COMPARATIVA DE ISLAS
-- ============================================================
-- Agrupa las ventas por isla en un rango de fechas.
-- Parámetros:
--   @FechaInicio  DATE  -- fecha de inicio del rango (NULL = desde el inicio)
--   @FechaFin     DATE  -- fecha de fin del rango    (NULL = hasta hoy)
-- ============================================================

CREATE OR ALTER PROC sp_Reporte_Comparativa_Islas
    @FechaInicio DATE = NULL,
    @FechaFin    DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- DETALLE POR ISLA
    SELECT
        v.id_Isla                                                   AS [Isla],
        COUNT(DISTINCT v.id_Venta)                                  AS [Ventas Registradas],
        COUNT(dv.id_Detalle)                                        AS [Transacciones],
        CAST(SUM(dv.Galones)            AS DECIMAL(10,2))           AS [Total Galones],
        CAST(SUM(dv.Total)              AS DECIMAL(10,2))           AS [Total Ventas RD$],
        CAST(AVG(dv.Total)              AS DECIMAL(10,2))           AS [Ticket Promedio RD$],
        CAST(MAX(dv.Total)              AS DECIMAL(10,2))           AS [Venta Maxima RD$]
    FROM Ventas v
    INNER JOIN Detalle_Venta dv ON v.id_Venta = dv.id_Venta
    WHERE
        (@FechaInicio IS NULL OR v.Fecha >= @FechaInicio)
        AND (@FechaFin IS NULL OR v.Fecha <= @FechaFin)
    GROUP BY v.id_Isla
    ORDER BY [Total Ventas RD$] DESC;

    -- TOTALES GENERALES
    SELECT
        COUNT(DISTINCT v.id_Venta)                                  AS [Total Ventas],
        COUNT(dv.id_Detalle)                                        AS [Total Transacciones],
        CAST(SUM(dv.Galones)            AS DECIMAL(10,2))           AS [Gran Total Galones],
        CAST(SUM(dv.Total)              AS DECIMAL(10,2))           AS [Gran Total RD$],
        CAST(AVG(dv.Total)              AS DECIMAL(10,2))           AS [Ticket Promedio General RD$]
    FROM Ventas v
    INNER JOIN Detalle_Venta dv ON v.id_Venta = dv.id_Venta
    WHERE
        (@FechaInicio IS NULL OR v.Fecha >= @FechaInicio)
        AND (@FechaFin IS NULL OR v.Fecha <= @FechaFin);
END
GO

-- ============================================================
-- EJEMPLOS DE USO - REPORTE 2
-- ============================================================

-- Historico completo (todos los dias registrados)
EXEC sp_Reporte_Comparativa_Islas;

-- Rango de fechas especifico
EXEC sp_Reporte_Comparativa_Islas
    @FechaInicio = '2026-05-01',
    @FechaFin    = '2026-05-03';

-- Un solo dia
EXEC sp_Reporte_Comparativa_Islas
    @FechaInicio = '2026-05-03',
    @FechaFin    = '2026-05-03';
GO


-- ============================================================
-- REPORTE 3: PRODUCTOS MAS VENDIDOS POR TURNO
-- ============================================================
-- Muestra, para cada turno, los combustibles ordenados
-- de mayor a menor por galones vendidos.
-- Parámetros:
--   @FechaInicio  DATE  -- fecha de inicio del rango (NULL = desde el inicio)
--   @FechaFin     DATE  -- fecha de fin del rango    (NULL = hasta hoy)
--   @Turno        VARCHAR(20) -- 'Matutino' | 'Vespertino' | 'Nocturno' | NULL = todos
-- ============================================================

CREATE OR ALTER PROC sp_Reporte_Productos_Por_Turno
    @FechaInicio DATE        = NULL,
    @FechaFin    DATE        = NULL,
    @Turno       VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- DETALLE POR TURNO Y PRODUCTO
    SELECT
        CASE
            WHEN v.Hora >= '06:00' AND v.Hora < '14:00' THEN 'Matutino'
            WHEN v.Hora >= '14:00' AND v.Hora < '22:00' THEN 'Vespertino'
            ELSE 'Nocturno'
        END                                                         AS [Turno],
        p.NomProducto                                               AS [Producto],
        c.NomCategoria                                              AS [Categoria],
        CAST(p.Precio_Litro         AS DECIMAL(10,2))               AS [Precio por Litro RD$],
        COUNT(dv.id_Detalle)                                        AS [Transacciones],
        CAST(SUM(dv.Galones)        AS DECIMAL(10,2))               AS [Galones Vendidos],
        CAST(SUM(dv.Total)          AS DECIMAL(10,2))               AS [Importe RD$],
        CAST(AVG(dv.Total)          AS DECIMAL(10,2))               AS [Promedio por Despacho RD$]
    FROM Detalle_Venta dv
    INNER JOIN Ventas    v ON dv.id_Venta    = v.id_Venta
    INNER JOIN Productos p ON dv.id_Producto = p.id_Producto
    INNER JOIN Categoria c ON p.id_Categoria = c.id_Categoria
    WHERE
        (@FechaInicio IS NULL OR v.Fecha >= @FechaInicio)
        AND (@FechaFin IS NULL OR v.Fecha <= @FechaFin)
        AND (
            @Turno IS NULL
            OR (@Turno = 'Matutino'   AND v.Hora >= '06:00' AND v.Hora <  '14:00')
            OR (@Turno = 'Vespertino' AND v.Hora >= '14:00' AND v.Hora <  '22:00')
            OR (@Turno = 'Nocturno'   AND (v.Hora >= '22:00' OR v.Hora < '06:00'))
        )
    GROUP BY
        CASE
            WHEN v.Hora >= '06:00' AND v.Hora < '14:00' THEN 'Matutino'
            WHEN v.Hora >= '14:00' AND v.Hora < '22:00' THEN 'Vespertino'
            ELSE 'Nocturno'
        END,
        p.NomProducto, c.NomCategoria, p.Precio_Litro
    ORDER BY [Turno], [Galones Vendidos] DESC;

    -- RESUMEN POR TURNO (totales)
    SELECT
        CASE
            WHEN v.Hora >= '06:00' AND v.Hora < '14:00' THEN 'Matutino'
            WHEN v.Hora >= '14:00' AND v.Hora < '22:00' THEN 'Vespertino'
            ELSE 'Nocturno'
        END                                                         AS [Turno],
        COUNT(dv.id_Detalle)                                        AS [Total Transacciones],
        CAST(SUM(dv.Galones)        AS DECIMAL(10,2))               AS [Total Galones],
        CAST(SUM(dv.Total)          AS DECIMAL(10,2))               AS [Total Ventas RD$]
    FROM Detalle_Venta dv
    INNER JOIN Ventas v ON dv.id_Venta = v.id_Venta
    WHERE
        (@FechaInicio IS NULL OR v.Fecha >= @FechaInicio)
        AND (@FechaFin IS NULL OR v.Fecha <= @FechaFin)
        AND (
            @Turno IS NULL
            OR (@Turno = 'Matutino'   AND v.Hora >= '06:00' AND v.Hora <  '14:00')
            OR (@Turno = 'Vespertino' AND v.Hora >= '14:00' AND v.Hora <  '22:00')
            OR (@Turno = 'Nocturno'   AND (v.Hora >= '22:00' OR v.Hora < '06:00'))
        )
    GROUP BY
        CASE
            WHEN v.Hora >= '06:00' AND v.Hora < '14:00' THEN 'Matutino'
            WHEN v.Hora >= '14:00' AND v.Hora < '22:00' THEN 'Vespertino'
            ELSE 'Nocturno'
        END
    ORDER BY [Total Ventas RD$] DESC;
END
GO

-- ============================================================
-- EJEMPLOS DE USO - REPORTE 3
-- ============================================================

-- Todos los turnos, historico completo
EXEC sp_Reporte_Productos_Por_Turno;

-- Todos los turnos en un rango de fechas
EXEC sp_Reporte_Productos_Por_Turno
    @FechaInicio = '2026-05-01',
    @FechaFin    = '2026-05-03';

-- Solo el turno matutino
EXEC sp_Reporte_Productos_Por_Turno
    @FechaInicio = '2026-05-01',
    @FechaFin    = '2026-05-03',
    @Turno       = 'Matutino';

-- Solo el turno vespertino
EXEC sp_Reporte_Productos_Por_Turno
    @FechaInicio = '2026-05-01',
    @FechaFin    = '2026-05-03',
    @Turno       = 'Vespertino';

-- Solo el turno nocturno
EXEC sp_Reporte_Productos_Por_Turno
    @FechaInicio = '2026-05-01',
    @FechaFin    = '2026-05-03',
    @Turno       = 'Nocturno';
GO