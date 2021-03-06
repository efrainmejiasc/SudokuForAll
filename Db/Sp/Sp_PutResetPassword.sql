USE [SUDOKU]
GO
/****** Object:  StoredProcedure [dbo].[Sp_PutResetPassword]    Script Date: 21/07/2019 15:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_PutResetPassword]
(
  @Email 	VARCHAR(50),
  @Codigo VARCHAR(10),
  @Estatus BIT

)
AS
BEGIN

SET NOCOUNT ON;
             
			DECLARE @Resultado INT;
			SET @Resultado = (SELECT MAX (Id)  FROM ResetPassword  WHERE Email = Email AND Codigo = @Codigo AND Estatus = 0)
			IF (@Resultado > = 1)
			  Begin
			     UPDATE ResetPassword SET Estatus = @Estatus WHERE Email = @Email AND Codigo = @Codigo AND Estatus = 0
			   End
			ELSE
			   Begin
			     SET @Resultado = 0;
			   End

		    SELECT @Resultado
END