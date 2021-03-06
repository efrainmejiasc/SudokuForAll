USE [SUDOKU]
GO
/****** Object:  StoredProcedure [dbo].[Sp_PutActivarCliente]    Script Date: 18/07/2019 18:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_PutActivarCliente]
(
  @Email 	VARCHAR(50),
  @Password VARCHAR(50),
  @FechaActivacion DATE,
  @Estatus BIT,
  @Identidad UNIQUEIDENTIFIER
)
AS
BEGIN

SET NOCOUNT ON;
       
	   DECLARE @Resultado INT;
	   DECLARE @Id INT;

	   SET @Id = (SELECT Id FROM Cliente WHERE Email = @Email AND Password = @Password AND Estatus = 0 AND Identidad = @Identidad)
	     IF (@Id >= 1)
		    Begin

                UPDATE Cliente SET 
			    FechaActivacion= @FechaActivacion,
			    Estatus = @Estatus
			    WHERE Email = @Email AND Password = @Password 

			    SET @Resultado = 1  --> Acivacion Exitosa
            End
         ELSE
		    Begin
		      	SET @Resultado = 0 --> Activacion Fallida
			End

      SELECT @Resultado

END
