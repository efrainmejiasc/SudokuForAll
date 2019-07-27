USE [SUDOKU]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetResultToIntro]    Script Date: 27/07/2019 14:34:54 ******/
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
				                     SET @Status = (SELECT Id FROM Cliente WHERE Email = @Email AND Password IS NOT NULL AND EstatusEnvioNotificacion = 1)
									 IF (@Status > 0)
									    Begin
										  SET @Resultado = 1 -- TIEMPO PRUEBA VALIDO
										End
									 ELSE
									    Begin
										  SET @Resultado = 2 -- TIEMPO PRUEBA VALIDO CUENTA NO ACTIVADA
										End

				              End
						  ELSE
						       Begin
									 SET @Status = (SELECT Id FROM Cliente WHERE Email = @Email AND Password IS NOT NULL AND Estatus = 1)
									 IF (@Status > 0)
									   Begin
									        SET @Resultado = 3-- CUENTA ACTIVADA CLIENTE REGISTRADO
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
							   End
	      End
       ELSE
	      Begin
			 SET @Resultado = 6 -- EMAIL NO EXISTE
		   End

	     SELECT @Resultado
   END;
