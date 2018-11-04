IF type_id('[dbo].[[utt_UpdatedWallet]]') IS NOT NULL
    EXEC('DROP TYPE [dbo].[[utt_UpdatedWallet]]');     
GO
CREATE TYPE [utt_UpdatedWallet] AS TABLE(
	WalletId uniqueidentifier NOT NULL index idx1,
	Balance decimal(18,2) NOT NULL 
)
with(memory_optimized=on)
GO

