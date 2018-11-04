CREATE FUNCTION dbo.uVf_GetBalance
(   
   @playerid uniqueidentifier
)
RETURNS int
AS
Begin
  return (select Balance from dbo.Wallet where PlayerId=@playerid )
End;
  

GO
