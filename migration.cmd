@echo off
chdir /d .\src\Ziv.CodeExample.Web
set /p migration="Enter migration name: "
dotnet ef migrations add %migration% --project ../Ziv.CodeExample.Database
dotnet ef database update
