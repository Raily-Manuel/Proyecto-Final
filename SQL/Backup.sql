--Backup Completo

BACKUP DATABASE [GasolineraDB]
TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_FULL.bak'
WITH 
FORMAT,
INIT,
NAME = N'GasolineraDB Full Backup',
STATS = 10;
GO

--Backup Diferencial
BACKUP DATABASE [GasolineraDB]
TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_DIFF.bak'
WITH 
DIFFERENTIAL,
INIT,
NAME = N'GasolineraDB Differential Backup',
STATS = 10;
GO

--Backup de Registro de Transacciones
ALTER DATABASE [GasolineraDB]
SET RECOVERY FULL;
GO

BACKUP LOG [GasolineraDB]
TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_LOG.trn'
WITH 
INIT,
NAME = N'GasolineraDB Log Backup',
STATS = 10;
GO

--Verifica existencia del completo
RESTORE HEADERONLY
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_FULL.bak';
GO

--Verifica existencia del diferencial
RESTORE HEADERONLY
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_DIFF.bak';
GO

--Verifica existencia del log
RESTORE HEADERONLY
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_LOG.trn';
GO