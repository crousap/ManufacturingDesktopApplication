USE [master]
RESTORE DATABASE [manufacturing] 
FROM DISK = N'/tmp/Manufacturing_db_backup.bak' 
WITH FILE = 1,  
MOVE N'test_db'TO N'/var/opt/mssql/data/manufacturing.mdf',  
MOVE N'test_db_log'TO N'/var/opt/mssql/data/manufacturing.ldf',  
NOUNLOAD, STATS = 5
GO