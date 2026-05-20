CREATE DATABASE GasolineraDB;
use GasolineraDB

drop database GasolineraDB

-- CATEGORIA
CREATE TABLE Categoria (
    id_Categoria INT IDENTITY(1,1) PRIMARY KEY,
    NomCategoria VARCHAR(100) NOT NULL,
    Estado VARCHAR(20) DEFAULT 'Activo'
);
GO

-- PRODUCTOS
CREATE TABLE Productos (
    id_Producto INT IDENTITY(1,1) PRIMARY KEY,
    NomProducto VARCHAR(100) NOT NULL,
    id_Categoria INT,
    Precio_Litro DECIMAL(10,2),
    Galones DECIMAL(10,2),
    Estado VARCHAR(20) DEFAULT 'Activo',

    FOREIGN KEY (id_Categoria) REFERENCES Categoria(id_Categoria)
);
GO

-- MANGUERAS
CREATE TABLE Mangueras (
    id_Manguera INT IDENTITY(1,1) PRIMARY KEY,
    id_Producto INT,
    LecturaINL DECIMAL(10,2),
    LecturaFNL DECIMAL(10,2),
    Estado VARCHAR(20) DEFAULT 'Activo',

    FOREIGN KEY (id_Producto) REFERENCES Productos(id_Producto)
);
GO  

-- ISLAS
CREATE TABLE Islas (
    id_Isla INT IDENTITY(1,1) PRIMARY KEY,
    Estado VARCHAR(20) DEFAULT 'Activo',
);
GO

-- EMPLEADO
CREATE TABLE Empleado (
    id_Empleado INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100),
    Edad INT,
    Cargo VARCHAR(50),
    Contraseña VARCHAR(8),
    Turno VARCHAR(50) null,
    Cedula VARCHAR(20),
    Telefono VARCHAR(15),
    Correo VARCHAR(100),
    Direccion VARCHAR(200),
    Estado VARCHAR(20) DEFAULT 'Activo',
);
GO

--EMPLEADO_ISLA
create table Empleado_Isla(
    id_Emp_isla INT IDENTITY(1,1) PRIMARY KEY,
    id_Empleado int,
    id_Isla int

    FOREIGN KEY (id_Empleado) REFERENCES Empleado(id_Empleado),
    FOREIGN KEY (id_Isla) REFERENCES Islas(id_Isla)
)

--ISLA_MANGUERA
create table Isla_Manguera(
    id_Isla_Manguera INT IDENTITY(1,1) PRIMARY KEY,
    id_Manguera int,
    id_Isla int

    FOREIGN KEY (id_Manguera) REFERENCES Mangueras(id_Manguera),
    FOREIGN KEY (id_Isla) REFERENCES Islas(id_Isla)
)

--VENTAS
create table Ventas(
id_Venta int primary key identity (1,1),
id_Isla int,
Fecha date,
Hora time(0),

foreign key (id_Isla) references Islas(id_Isla)
)

--Reporte guardar
CREATE TABLE ReporteCuadre
(
    id_Reporte INT PRIMARY KEY IDENTITY(1,1),
    id_Isla INT,
    Turno VARCHAR(20),
    Fecha DATE,
    Total DECIMAL(18,2)
);

--DETALLE_VENTA
create table Detalle_Venta(
id_Detalle int primary key identity(1,1),
id_Venta int,
id_Empleado int,
id_Manguera int,
id_Producto int,
Precio decimal(6,2),
LecturaINL decimal(10,2),
LecturaFNL decimal(10,2),
Galones decimal(10,2),
Total decimal(10,2),

foreign key (id_Venta) references Ventas(id_Venta),
FOREIGN KEY (id_Empleado) REFERENCES Empleado(id_Empleado),
FOREIGN KEY (id_Manguera) REFERENCES Mangueras(id_Manguera),
FOREIGN KEY (id_Producto) REFERENCES Productos(id_Producto)
)



select * from Categoria
select * from Productos
select * from Mangueras
select * from Islas
select * from Empleado
select * from Empleado_isla
select * from Isla_Manguera
select * from Ventas
select * from ReporteCuadre

