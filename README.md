	Update Database guide:
tools -> nuget package manager -> package manager controll
default project: dropbox set to repository (project with dbcontext)

type each lines below:


Add-Migration InitialCreate -Context DiamondShopContext
Update-Database
