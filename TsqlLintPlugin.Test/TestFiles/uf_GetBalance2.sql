ALTER FUNCTION dbo.u4f_GetBalance
(   
   @playerid uniqueidentifier
)
RETURNS decimal
AS
Begin
  return (select Balance from dbo.Wallet where PlayerId=@playerid );
End;
  

GO
