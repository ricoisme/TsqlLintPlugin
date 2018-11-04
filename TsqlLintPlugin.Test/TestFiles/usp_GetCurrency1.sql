IF OBJECT_ID('usp_GetCurrency') IS NULL
BEGIN
    EXEC('CREATE PROCEDURE [dbo].[usp_GetCurrency] AS');
END
GO

create or alter procedure sp_GetCurrency(@playerid uniqueidentifier)
as
set nocount on;
select Currency
from dbo.Player
where PlayerId=@playerid