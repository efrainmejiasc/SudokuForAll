USE [SUDOKU]
GO
/****** Object:  StoredProcedure [dbo].[Sp_LoginCliente]    Script Date: 14/07/2019 10:31:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[Sp_LoginCliente] (@Password VARCHAR(50)) AS

   BEGIN
       DECLARE @Resultado INT;
	   DECLARE @Id INT;
	   DECLARE @Dias INT;
	   DECLARE @FechaVencimiento DATE;

	   SET @Id = (SELECT (A.Id) AS Id  FROM Cliente A INNER JOIN PagoCliente B ON A.Id = B.IdCliente WHERE  A.Password = @Password AND A.Estatus = 1  GROUP BY A.Id)
	   If (@Id >= 1)
	   Begin
	   SET @FechaVencimiento = (SELECT MAX(FechaVencimiento) AS FechaVencimiento FROM PagoCliente WHERE IdCliente = @Id)
	   IF (@Id > 0)
	      Begin
		        SET @Dias = (SELECT DATEDIFF(DAY, GETDATE(), @FechaVencimiento))
		         IF(@Dias = 0)
		            Begin
					     SET @Resultado  = 0 -- Expiro el pago
					End
                 ELSE IF (@Dias >= 1 AND @Dias <= 5)
				     Begin
					     SET @Resultado  = 1 -- Dias por expirar menos o igual a 5
					 End
				 ELSE IF (@Dias >= 6)
				     Begin
					     SET @Resultado  = 2 -- Dia Numero N
					 End
		  End
     End
	 ELSE
	 Begin
	     SET @Resultado = -1 
	 End
    SELECT @Resultado AS RESULTADO

   END;
