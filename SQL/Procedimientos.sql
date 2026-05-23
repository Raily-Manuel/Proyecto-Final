--PROCEDIMIENTOS ALMACENADOS
use GasolineraDB

--MOSTRAR DATOS DE ISLA EN EL DGV
CREATE OR ALTER PROC sp_Hoja_Detalle
(
    @Fecha DATE,
    @Turno VARCHAR(20)
)
AS
BEGIN

    SELECT
        m.id_Manguera AS [No. Mg.],
        p.NomProducto AS Producto,
        p.Precio_Litro AS Precio,

        MIN(dv.LecturaINL) AS [L.Inicial],
        MAX(dv.LecturaFNL) AS [L.Final],

        ISNULL(SUM(dv.Galones), 0) AS Galones,
        ISNULL(SUM(dv.Total), 0) AS Importe

    FROM Mangueras m

    INNER JOIN Productos p
        ON m.id_Producto = p.id_Producto

    LEFT JOIN Detalle_Venta dv
        ON m.id_Manguera = dv.id_Manguera

    LEFT JOIN Ventas v
        ON dv.id_Venta = v.id_Venta
        AND v.Fecha = @Fecha
        AND(
            (@Turno = 'Matutino' AND v.Hora >= '06:00' AND v.Hora < '14:00')
            OR (@Turno = 'Vespertino' AND v.Hora >= '14:00' AND v.Hora < '22:00')
            OR (@Turno = 'Nocturno' AND (v.Hora >= '22:00' OR v.Hora < '06:00'))
        )

    GROUP BY
        m.id_Manguera,
        p.NomProducto,
        p.Precio_Litro

    ORDER BY m.id_Manguera;


    /* TOTALES GENERALES */

    SELECT
        ISNULL(SUM(dv.Galones), 0) AS Total_Galones,
        ISNULL(SUM(dv.Total), 0) AS Total_Ventas

    FROM Detalle_Venta dv

    INNER JOIN Ventas v
        ON dv.id_Venta = v.id_Venta

    WHERE v.Fecha = @Fecha
    AND(
        (@Turno = 'Matutino' AND v.Hora >= '06:00' AND v.Hora < '14:00')
        OR (@Turno = 'Vespertino' AND v.Hora >= '14:00' AND v.Hora < '22:00')
        OR (@Turno = 'Nocturno' AND (v.Hora >= '22:00' OR v.Hora < '06:00'))
    );

END

exec sp_Mostrar_Datos_Isla @id_Isla = 4, @Turno = 'Nocturno', @Fecha = '2026-05-03';



--HOJA DE DETALLE (PROCEDIMIENTO ESTANDAR)
create or alter proc sp_Hoja_Detalle
	@Fecha date,
	@Turno varchar(20)
as
begin
	select
		m.id_Manguera as [No. Mg.],
		p.NomProducto as Producto,
		p.Precio_Litro,
		m.LecturaINL as [L.Inicial],
		m.LecturaFNL as [L.Final],

		isnull(sum(dv.Galones), 0) as Galones,
		isnull(sum(dv.Total), 0) as Importe

	from Mangueras m
	inner join Productos p on m.id_Producto = p.id_Producto
	inner join Detalle_Venta dv on m.id_Manguera = dv.id_Manguera
	inner join Ventas v on dv.id_Venta = v.id_Venta

	where v.Fecha = @Fecha and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)

group by m.id_Manguera, p.NomProducto, p.Precio_Litro, m.LecturaINL, m.LecturaFNL
order by m.id_Manguera

/*TOTALES GENERALES*/
	select
		isnull(sum(dv.Galones), 0) as Total_Galones,
		isnull(sum(dv.Total), 0) as Total_Ventas

	from Detalle_Venta dv
	inner join Ventas v on dv.id_Venta = v.id_Venta

	where v.Fecha = @Fecha and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)
end

exec sp_Hoja_Detalle @Turno = 'Matutino', @Fecha = '2026-05-03';

--CALCULO DE CUADRE (ESTO NO VA EN EL SISTEMA, ES SOLO PARA VER LA INFORMACION DE LOS CALCULOS MAS AGRUPADA Y ORGANIZADA)
create or alter proc sp_Calculo_Cuadre
	@id_Isla int,
	@Turno varchar(20),
	@Fecha date
as
begin
	select
		sum(dv.Galones) as Total_Galones,
		sum(dv.Total) as Total_Ventas,

		count(dv.id_Detalle) as Cantidad_Transacciones,

		cast(isnull(avg(dv.Total), 0) as decimal(10,2)) as [Promedio de Venta]

	from Ventas v
	inner join Detalle_Venta dv on v.id_Venta = dv.id_Venta

	where
		v.id_Isla = @id_Isla and v.Fecha = @Fecha
		and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)
end
exec sp_Calculo_Cuadre @id_Isla = 1, @Turno = 'Matutino', @Fecha = '2026-05-03';
select * from Detalle_Venta

--TOTAL DE GALONES
create or alter proc sp_Total_Galones
	@id_Isla int,
	@Turno varchar(20),
	@Fecha date
as
begin
	select
		sum(dv.Galones) as [Total de Galones]
	from Detalle_Venta dv inner join Ventas v on dv.id_Venta = v.id_Venta

	where
		v.id_Isla = @id_Isla and v.Fecha = @Fecha
		and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)
end
exec sp_Total_Galones @id_Isla = 1, @Turno = 'Matutino', @Fecha = '2026-05-03';


--TOTAL DE VENTAS
create or  alter proc sp_Total_Ventas
	@id_Isla int,
	@Turno varchar(20),
	@Fecha date
as
begin
	select
		sum(dv.Total) AS [Total de Ventas]
	from Detalle_Venta dv inner join Ventas v on dv.id_Venta = v.id_Venta

	where
		v.id_Isla = @id_Isla and v.Fecha = @Fecha
		and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)
end
exec sp_Total_Ventas @id_Isla = 1, @Turno = 'Matutino', @Fecha = '2026-05-03';


--CANTIDAD DE TRANSACCIONES
create or alter proc sp_Cantidad_Transacciones
	@id_Isla int,
	@Turno varchar(20),
	@Fecha date
as
begin
	select
		count(dv.id_Detalle) as [Cantidad de Transacciones]
	from Detalle_Venta dv inner join Ventas v on dv.id_Venta = v.id_Venta

	where
		v.id_Isla = @id_Isla and v.Fecha = @Fecha
		and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)
end
exec sp_Cantidad_Transacciones @id_Isla = 1, @Turno = 'Matutino', @Fecha = '2026-05-03';


--PROMEDIO DE VENTA
create or alter proc sp_Promedio_Venta
	@id_Isla int,
	@Turno varchar(20),
	@Fecha date
as
begin
	select
		cast(isnull(avg(dv.Total), 0) as decimal(10,2)) as [Promedio de Venta]

	from Ventas v
	inner join Detalle_Venta dv on v.id_Venta = dv.id_Venta

	where
		v.id_Isla = @id_Isla and v.Fecha = @Fecha
		and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)
end
exec sp_Promedio_Venta @id_Isla = 1, @Turno = 'Matutino', @Fecha = '2026-05-03';


--REPORTE (ESTANDAR)
create or alter proc sp_Reporte
	@id_Isla int,
	@Turno varchar(20),
	@Fecha date
as
begin
--ENCABEZADO
	select distinct top 1
		cast(getdate() as date) as [Fecha de Cuadre],
		format(getdate(), 'HH:mm') as [Hora de Cuadre],
		ei.id_Empleado as [No. Empleado],
		e.Nombre as [Nombre Empleado],
		@id_Isla as [No. Isla],
		@Turno   as Turno

	from Empleado_Isla ei
    inner join Empleado e on ei.id_Empleado = e.id_Empleado
    inner join Ventas v on v.id_Isla = ei.id_Isla
    inner join Detalle_Venta dv on dv.id_Venta = v.id_Venta
                                and dv.id_Empleado = ei.id_Empleado

	where
		ei.id_Isla = @id_Isla
		and(
            (@Turno = 'Matutino'   and v.Hora >= '06:00' and v.Hora < '14:00')
            or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
            or (@Turno = 'Nocturno'   and (v.Hora >= '22:00' or v.Hora < '06:00'))
        )

--DETALLE
	select
		dv.id_Manguera as [No. Manguera],
		p.NomProducto as Producto,
		cast(max (dv.Precio) as decimal(10,2)) as Precio,
		cast(sum (dv.Galones) as decimal(10,2)) as [Galones Vendidos],
		cast(sum (dv.Total) as decimal(10,2)) as [Total de Productos vendidos]

	from Detalle_Venta dv
	inner join Ventas v on dv.id_Venta = v.id_Venta
	inner join Productos p on dv.id_Producto = p.id_Producto
	inner join Isla_Manguera im on im.id_Isla_Manguera = dv.id_Manguera

	where
		v.id_Isla = @id_Isla and im.id_Isla = @id_Isla and v.Fecha = @Fecha
		and(
            (@Turno = 'Matutino'   and v.Hora >= '06:00' and v.Hora < '14:00')
            or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
            or (@Turno = 'Nocturno'   and (v.Hora >= '22:00' or v.Hora < '06:00'))
        )
	group by dv.id_Manguera, p.NomProducto, dv.Precio
	order by dv.id_Manguera

--RESUMEN
	select
        cast(sum(dv.Galones) as decimal(10,2))              as [Total Galones],
        cast(sum(dv.Total) as decimal(10,2))                as [Total Ventas],
        count(dv.id_Detalle)                                as [Cantidad Transacciones],
        cast(isnull(avg(dv.Total), 0) as decimal(10,2))    as [Promedio de Venta]

    from Detalle_Venta dv
    inner join Ventas v on dv.id_Venta = v.id_Venta

    where
        v.id_Isla = @id_Isla
        and v.Fecha = @Fecha
        and (
            (@Turno = 'Matutino'   and v.Hora >= '06:00' and v.Hora < '14:00')
            or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
            or (@Turno = 'Nocturno'   and (v.Hora >= '22:00' or v.Hora < '06:00'))
        )
end
exec sp_Reporte @id_Isla = 2, @Turno = 'Matutino', @Fecha = '2026-05-03';

--Validar login
CREATE OR ALTER PROC sp_Iniciar_Sesion
    @Nombre VARCHAR(30),
    @Clave VARCHAR(30)
AS
BEGIN
    SELECT 
        Nombre,
        Cargo,
        Turno
    FROM Empleado
    WHERE Nombre = @Nombre
    AND Clave = @Clave
END

--Guardar Reporte
CREATE OR ALTER PROC sp_GuardarReporte
    @id_Isla INT,
    @Turno VARCHAR(20),
    @Fecha DATE 
AS
BEGIN
    INSERT INTO ReporteCuadre
    (
        id_Isla,
        Turno,
        Fecha
    )
    VALUES
    (
        @id_Isla,
        @Turno,
        @Fecha
    )
END
GO

--Obtener parametros
CREATE OR ALTER PROC sp_ObtenerParametros
(
    @id_Isla INT,
    @Turno VARCHAR(20),
    @Fecha DATE
)
AS
BEGIN

    SELECT
        @id_Isla AS id_Isla,
        @Turno AS Turno,
        @Fecha AS Fecha;

END

--MostrarCuadre
CREATE OR ALTER PROC sp_MostrarCuadre
(
    @id_Isla INT,
    @Turno VARCHAR(20),
    @Fecha DATE
)
AS
BEGIN

    SELECT
        m.id_Manguera AS [No. Manguera],
        p.NomProducto AS Producto,
        p.Precio_Litro AS Precio,

        MIN(dv.LecturaINL) AS [L.Inicial],
        MAX(dv.LecturaFNL) AS [L.Final],

        ISNULL(SUM(dv.Galones),0) AS Galones,
        ISNULL(SUM(dv.Total),0) AS [Total Ventas]

    FROM Isla_Manguera im

    INNER JOIN Mangueras m
        ON im.id_Manguera = m.id_Manguera

    INNER JOIN Productos p
        ON m.id_Producto = p.id_Producto

    LEFT JOIN Detalle_Venta dv
        ON m.id_Manguera = dv.id_Manguera

    LEFT JOIN Ventas v
        ON dv.id_Venta = v.id_Venta
        AND v.id_Isla = @id_Isla
        AND v.Fecha = @Fecha
        AND(
            (@Turno = 'Matutino' AND v.Hora >= '06:00' AND v.Hora < '14:00')
            OR (@Turno = 'Vespertino' AND v.Hora >= '14:00' AND v.Hora < '22:00')
            OR (@Turno = 'Nocturno' AND (v.Hora >= '22:00' OR v.Hora < '06:00'))
        )

    WHERE im.id_Isla = @id_Isla

    GROUP BY
        m.id_Manguera,
        p.NomProducto,
        p.Precio_Litro

    ORDER BY m.id_Manguera;

END

--Mostrar Turnos
CREATE OR ALTER PROC sp_MostrarTurnos
AS
BEGIN

    SELECT 'Matutino' AS Turno
    UNION
    SELECT 'Vespertino'
    UNION
    SELECT 'Nocturno';

END

--Mostrar Turnos
CREATE OR ALTER PROC sp_MostrarDatosIsla
AS
BEGIN

    SELECT
        id_Isla,
        Estado
    FROM Islas

    ORDER BY id_Isla;

END

-- REPORTE 2: COMPARATIVA DE ISLAS
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

-- REPORTE 3: PRODUCTOS MAS VENDIDOS POR TURNO
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

-- ListarReportes desde aqui son nuevos
CREATE OR ALTER PROC sp_ListarReportes
AS
BEGIN
    SELECT 
        id_Reporte,
        id_Isla,
        Turno,
        Fecha
    FROM ReporteCuadre
    ORDER BY Fecha DESC, id_Reporte DESC;
END
GO

-- BuscarReportesPorFecha
CREATE OR ALTER PROC sp_BuscarReportesPorFecha
    @Fecha DATE
AS
BEGIN
    SELECT 
        id_Reporte,
        id_Isla,
        Turno,
        Fecha
    FROM ReporteCuadre
    WHERE Fecha = @Fecha
    ORDER BY id_Reporte DESC;
END
GO

-- MostrarHojaDetalle
CREATE OR ALTER PROC sp_MostrarHojaDetalle
AS
BEGIN
    SELECT
        id_Isla,
        Turno,
        Fecha
    FROM HojaDetalle
    ORDER BY Fecha DESC;
END
GO

-- BuscarHojaDetallePorFecha
CREATE OR ALTER PROC sp_MostrarHojaDetalle
AS
BEGIN
    SELECT
        hd.id_HojaD,
        hd.id_Detalle,
        hd.Turno,
        hd.Fecha
    FROM hoja_de_detalle hd
    ORDER BY hd.Fecha DESC;
END
GO

-- BuscarReportePorParametros
CREATE OR ALTER PROC sp_BuscarHojaDetallePorFecha
    @Fecha DATE
AS
BEGIN
    SELECT
        hd.id_HojaD,
        hd.id_Detalle,
        hd.Turno,
        hd.Fecha
    FROM hoja_de_detalle hd
    WHERE hd.Fecha = @Fecha
    ORDER BY hd.Fecha DESC;
END
GO
