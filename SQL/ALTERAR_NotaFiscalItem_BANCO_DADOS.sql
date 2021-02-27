USE [Teste]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


ALTER TABLE [dbo].[NotaFiscalItem] ADD 	
	[BaseIpi] [decimal](18, 5) NULL,
	[AliquotaIpi] [decimal](18, 5) NULL,
	[ValorIpi] [decimal](18, 5) NULL

GO
