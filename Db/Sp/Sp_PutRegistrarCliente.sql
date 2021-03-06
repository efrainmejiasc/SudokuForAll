USE [SUDOKU]
GO
/****** Object:  StoredProcedure [dbo].[Sp_PutRegistrarCliente]    Script Date: 10/09/2019 18:53:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_PutRegistrarCliente]
(
  @Nombre	VARCHAR(50),
  @Apellido	VARCHAR(50),
  @Email 	VARCHAR(50),
  @Password VARCHAR(50),
  @FechaRegistro DATE,
  @Estatus BIT
)
AS
BEGIN

SET NOCOUNT ON;
       
	   DECLARE @Resultado INT;
	   DECLARE @Id INT;

	   SET @Id = (SELECT Id FROM Cliente WHERE Email = @Email)
	     IF (@Id >= 1)
		    Begin

                UPDATE Cliente SET 
			    Nombre = @Nombre,
			    Apellido = @Apellido,
			    Password = @Password,
			    FechaRegistro = @FechaRegistro,
			    Estatus = @Estatus,
				EstatusEnvioNotificacion = 1
			    WHERE Email = @Email  AND Estatus = 0  

			    SET @Resultado = (@Id) --> Registro Exitoso
            End
         ELSE
		    Begin
		      	SET @Resultado = 0 --> Registro Fallido
			End

      SELECT @Resultado

END
