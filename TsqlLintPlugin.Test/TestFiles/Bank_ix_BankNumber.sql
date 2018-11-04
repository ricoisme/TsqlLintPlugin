﻿IF NOT EXISTS (
SELECT *
FROM SYS.INDEXES
WHERE NAME='ix_BankNumber' AND OBJECT_ID = OBJECT_ID('dbo.Bank')
)
BEGIN
  CREATE CLUSTERED INDEX ix_BankNumber ON dbo.Bank(BankNumber)
  WITH(DATA_COMPRESSION=ROW,MAXDOP=8,ONLINE= ON)
END
/*
IF EXISTS (
SELECT *
FROM SYS.INDEXES
WHERE NAME='ix_BankNumber' AND OBJECT_ID = OBJECT_ID('dbo.Bank')
)
BEGIN
    ALTER INDEX ix_BankNumber on dbo.Bank REBUILD 
    WITH (MAXDOP=8,ONLINE = ON(WAIT_AT_LOW_PRIORITY 
		(MAX_DURATION = 1 MINUTES , ABORT_AFTER_WAIT = SELF )))
END
*/