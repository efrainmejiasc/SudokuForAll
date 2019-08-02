USE [SUDOKU]
GO
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetResultToIntro]    Script Date: 02/08/2019 7:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetResultToIntro] (@Email VARCHAR(50)) AS

   BEGIN
       DECLARE @Resultado INT;
	   DECLARE @IdMail INT;
	   DECLARE @Horas INT;
	   DECLARE @Status BIT;

	   SET @IdMail = (SELECT Id FROM Cliente WHERE Email = @Email)
	   IF(@IdMail > 0)
		   Begin
			    SET @Horas = (SELECT DATEDIFF(HOUR,FechaActivacionPrueba, GETDATE()) FROM Cliente WHERE Email = @Email);
			              IF (@Horas <= 60)
				              Begin
				                     SET @Resultado = 1 -- TIEMPO DE PRUEBA VALIDO
				              End
						  ELSE
						       Begin
							         SET @Resultado = 2 -- TiEMPO EXPIRADO
									 SET @Status = (SELECT Id FROM Cliente WHERE Email = @Email AND Password IS NOT NULL AND Estatus = 1)
									 IF (@Status > 0)
									   Begin
									        SET @Resultado = 3 -- CUENTA ACTIVADA CLIENTE REGISTRADO
									   End
									 SET @Status = (SELECT Id FROM Cliente WHERE Email = @Email AND Password IS NULL AND Estatus = 0)
									 IF (@Status > 0)
									   Begin
									        SET @Resultado = 4 -- CUENTA DEBE COMPRAR Y REGISTRAR
									   End
									 SET @Status = (SELECT Id FROM Cliente WHERE Email = @Email AND Password IS NOT NULL AND Estatus = 0)
									 IF (@Status > 0)
									   Begin
									        SET @Resultado = 5 -- CUENTA NO ACTIVADA
									   End
                                     SET @Status = (SELECT Id FROM Cliente WHERE Email = @Email AND Password IS NULL AND FechaActivacionPrueba = '1900-01-01 00:00:00.000')
									 IF (@Status > 0)
									   Begin
									        DELETE FROM CLIENTE WHERE Email = @Email
									        SET @Resultado = 6 -- EMAIL NUNCA ACTIVO CUENTA
									   End
							   End
	      End
       ELSE
	      Begin
			 SET @Resultado = 6 -- EMAIL NO EXISTE
		   End

	     SELECT @Resultado
   END;