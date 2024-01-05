
SET NOCOUNT ON
GO

USE master
GO
if exists (select * from sysdatabases where name='Hbc')
		drop database Hbc
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE Hbc
  ON PRIMARY (NAME = N''Hbc'', FILENAME = N''' + @device_directory + N'hbc.mdf'')
  LOG ON (NAME = N''Hbc_log'',  FILENAME = N''' + @device_directory + N'hbc.ldf'')')
go

if CAST(SERVERPROPERTY('ProductMajorVersion') AS INT)<12 
BEGIN
  exec sp_dboption 'Hbc','trunc. log on chkpt.','true'
  exec sp_dboption 'Hbc','select into/bulkcopy','true'
END
ELSE ALTER DATABASE [Hbc] SET RECOVERY SIMPLE WITH NO_WAIT
GO

set quoted_identifier on
GO
