USE [SUDOKU]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetResultToIntro]    Script Date: 27/07/2019 6:01:02 ******/
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
			    SET @Horas = (SELECT DATEDIFF(HOUR,FechaActivacionPrueba, GETDATE()) FROM Cliente WHERE Email = @Email AND EstatusEnvioNotificacion = 1 AND Estatus = 0);
			              IF (@Horas <= 60)
				              Begin
				                   SET @Resultado = (SELECT Id FROM Cliente WHERE Email = @Email AND EstatusEnvioNotificacion = 0)
								         IF(@Resultado > 0)
										      Begin
											        SET @Resultado = 1 --TIEMPO DE PRUEBA VALIDO NO ACTIVADO
											  End  
								    SET @Resultado = (SELECT Id FROM Cliente WHERE Email = @Email AND EstatusEnvioNotificacion = 1)
									       IF(@Resultado > 0)
									          Begin
											        SET @Resultado = 2 --TIEMPO DE PRUEBA VALIDO ACTIVADO
											  End 
				              End
						  ELSE
						      Begin
						        SET @Resultado = 3 --Tiempo de prueba Expirado

						         SET @Resultado = (SELECT Id FROM Cliente WHERE Email = @Email AND EstatusEnvioNotificacion = 1 AND Estatus = 0 AND Password IS NOT NULL)
								         IF(@Resultado > 0)
										      Begin
											        SET @Resultado = 4 -- CLIENTE REGISTRADO NO ACTIVADO
											  End  
							    SET @Resultado = (SELECT Id FROM Cliente WHERE Email = @Email AND EstatusEnvioNotificacion = 1 AND Estatus = 1 AND Password IS NOT NULL)
								         IF(@Resultado > 0)
										      Begin
											        SET @Resultado = 5 --CLIENTE REGISTRADO ACTIVADO
											  End  	
							   End
	      End
       ELSE
	      Begin
			 SET @Resultado = 6 -- EMAIL NO REGISTRADO
		   End

	     SELECT @Resultado
   END;
