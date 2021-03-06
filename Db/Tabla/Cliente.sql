/*
   viernes, 5 de julio de 20199:53:29
   Usuario: sa
   Servidor: EMCSERVERI7
   Base de datos: SUDOKU
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.Cliente.EstatusEnvio', N'Tmp_EstatusEnvioNotificacion', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Cliente.Tmp_EstatusEnvioNotificacion', N'EstatusEnvioNotificacion', 'COLUMN' 
GO
ALTER TABLE dbo.Cliente SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Cliente', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Cliente', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Cliente', 'Object', 'CONTROL') as Contr_Per 