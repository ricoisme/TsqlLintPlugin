IF type_id('[dbo].[ut_UpdatedWallet]') IS NOT NULL
    EXEC('DROP TYPE [dbo].[ut_UpdatedWallet]');     
GO
CREATE TYPE [dbo].[ut_UpdatedWallet] AS TABLE(
	WalletId uniqueidentifier NOT NULL index idx1,
	Balance decimal(18,2) NOT NULL 
)
with(memory_optimized=on)
GO

