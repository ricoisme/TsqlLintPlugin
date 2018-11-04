alter trigger dbo.tri_TestAfter
on bank
AFTER INSERT, UPDATE   
AS RAISERROR ('Notify Customer Relations', 16, 10);  
GO 
