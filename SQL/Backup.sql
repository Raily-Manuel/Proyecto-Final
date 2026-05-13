-- Backup Completo
BACKUP DATABASE [GasolineraDB]
TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_FULL.bak'
WITH 
FORMAT,
INIT,
NAME = N'GasolineraDB Full Backup',
STATS = 10;
GO


-- Backup Diferencial
BACKUP DATABASE [GasolineraDB]
TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_DIFF.bak'
WITH 
DIFFERENTIAL,
INIT,
NAME = N'GasolineraDB Differential Backup',
STATS = 10;
GO


-- Cambiar A Recovery Full
ALTER DATABASE [GasolineraDB]
SET RECOVERY FULL;
GO


-- Backup Del Log
BACKUP LOG [GasolineraDB]
TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_LOG.trn'
WITH 
INIT,
NAME = N'GasolineraDB Log Backup',
STATS = 10;
GO


-- Verificar Backup Completo
RESTORE HEADERONLY
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_FULL.bak';
GO


-- Verificar Backup Diferencial
RESTORE HEADERONLY
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_DIFF.bak';
GO


-- Verificar Backup Log
RESTORE HEADERONLY
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_LOG.trn';
GO


-- Cerrar conexiones
ALTER DATABASE [GasolineraDB]
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO

-- Restaurar Backup Completo
RESTORE DATABASE [GasolineraDB]
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_FULL.bak'
WITH 
REPLACE,
NORECOVERY;
GO


-- Restaurar Backup Diferencial
RESTORE DATABASE [GasolineraDB]
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_DIFF.bak'
WITH NORECOVERY;
GO


-- Restaurar Backup Log
RESTORE LOG [GasolineraDB]
FROM DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\Backup\GasolineraDB_LOG.trn'
WITH RECOVERY;
GO


-- Volver Multiusuario
ALTER DATABASE [GasolineraDB]
SET MULTI_USER;
GO

RESTORE DATABASE [GasolineraDB]
WITH RECOVERY;
GO
