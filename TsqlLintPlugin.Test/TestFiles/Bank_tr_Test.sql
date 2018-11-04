create trigger dbo.tr_TestAfter
on bank
AFTER INSERT, UPDATE   
AS RAISERROR ('Notify Customer Relations', 16, 10);  
GO 