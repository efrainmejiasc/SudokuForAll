USE [SUDOKU]
GO
/****** Object:  StoredProcedure [dbo].[Sp_PutCliente]    Script Date: 17/07/2019 19:51:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_PutCliente]
(
  @FechaActivacionPrueba DATE,
  @Email 	VARCHAR(50),
  @EstatusEnvioNotificacion BIT,
  @Identidad UNIQUEIDENTIFIER
)
AS
BEGIN

SET NOCOUNT ON;
             
			DECLARE @Resultado INT;

			SET @Resultado = (SELECT Id FROM Cliente WHERE Email = @Email AND Identidad = @Identidad)
			IF (@Resultado >= 1)
			    Begin
                    UPDATE Cliente SET 
			        FechaActivacionPrueba = @FechaActivacionPrueba,
			        EstatusEnvioNotificacion = @EstatusEnvioNotificacion
			        WHERE Email = @Email  AND EstatusEnvioNotificacion = 0 AND Identidad = @Identidad
				End 
			ELSE
			    Begin 
				   SET @Resultado = 0
				End 

		SELECT @Resultado
END