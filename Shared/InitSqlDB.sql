USE master
GO

IF (NOT EXISTS(SELECT NULL 
        FROM master.dbo.sysdatabases 
        WHERE name = 'NServiceBusHost')
    )
BEGIN
    CREATE DATABASE NServiceBusHost;
END

GO

USE NServiceBusHost;
GO