--PROCEDIMIENTOS ALMACENADOS

--MOSTRAR DATOS DE ISLA EN EL DGV
create or alter proc sp_Mostrar_Datos_Isla
	@id_Isla int,
	@Turno varchar(20),
	@Fecha DATE
as
begin
	select 
		m.id_Manguera as [No. Manguera],
		P.NomProducto as Producto,
		P.Precio_Litro as Precio,

		min(dv.LecturaINL) as [L.Inicial],
		max(dv.LecturaFNL) as [L.Final],

		isnull(sum(dv.Galones), 0) as Galones,
		isnull(sum(dv.Total), 0) as [Total Ventas]

	from Isla_Manguera im
	inner join Mangueras m on im.id_Manguera = m.id_Manguera
	inner join Productos p on m.id_Producto = p.id_Producto

	left join Detalle_Venta dv on m.id_Manguera = dv.id_Manguera
	left join Ventas v on dv.id_Venta = v.id_Venta
		and v.id_Isla = @id_Isla
		and v.Fecha = @Fecha
		and(
			(@Turno = 'Matutino' and v.Hora >= '6:00' and v.Hora < '14:00')
			or (@Turno = 'Vespertino' and v.Hora >= '14:00' and v.Hora < '22:00')
			or (@Turno = 'Nocturno' and (v.Hora >= '22:00' or v.Hora < '6:00'))
		)

	where im.id_Isla = @id_Isla
		
	group by m.id_Manguera, p.NomProducto, p.Precio_Litro
	order by m.id_Manguera
end

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