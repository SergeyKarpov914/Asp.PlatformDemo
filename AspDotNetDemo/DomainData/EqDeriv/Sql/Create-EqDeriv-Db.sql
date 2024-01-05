
SET NOCOUNT ON
GO

USE master
GO
if exists (select * from sysdatabases where name='EqDeriv')
		drop database EqDeriv
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE EqDeriv
  ON PRIMARY (NAME = N''EqDeriv'', FILENAME = N''' + @device_directory + N'eqderiv.mdf'')
  LOG ON (NAME = N''EqDeriv_log'',  FILENAME = N''' + @device_directory + N'eqderiv.ldf'')')
go

if CAST(SERVERPROPERTY('ProductMajorVersion') AS INT)<12 
BEGIN
  exec sp_dboption 'EqDeriv','trunc. log on chkpt.','true'
  exec sp_dboption 'EqDeriv','select into/bulkcopy','true'
END
ELSE ALTER DATABASE [EqDeriv] SET RECOVERY SIMPLE WITH NO_WAIT
GO

set quoted_identifier on
GO
