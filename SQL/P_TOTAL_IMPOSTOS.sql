USE [Teste]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_TOTAL_IMPOSTOS] 
AS
BEGIN
	
	SELECT	Cfop AS [CFOP]
			, SUM(ISNULL(BaseICMS,0)) AS [Valor Total da Base de ICMS]
			, SUM(ISNULL(ValorICMS,0)) AS [Valor Total do ICMS]
			, SUM(ISNULL(BaseIpi,0)) AS [Valor Total da Base de IPI]
			, SUM(ISNULL(ValorIpi,0)) AS [Valor Total do IPI]
	FROM NotaFiscalItem
	GROUP BY Cfop
	
END
