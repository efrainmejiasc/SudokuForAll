/*
   jueves, 4 de julio de 20196:48:33
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
CREATE TABLE dbo.PagoCliente
	(
	Id int NOT NULL IDENTITY (1, 1),
	IdCliente int NULL,
	FechaPago datetime NULL,
	FechaVencimiento datetime NULL,
	MontoPago float(53) NULL,
	Impuesto float(53) NULL,
	MontoTotal float(53) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PagoCliente ADD CONSTRAINT
	PK_PagoCliente PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PagoCliente SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PagoCliente', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PagoCliente', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PagoCliente', 'Object', 'CONTROL') as Contr_Per 