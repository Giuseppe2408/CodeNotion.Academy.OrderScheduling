::dotnet tool install --global dotnet-ef
::dotnet tool update --global dotnet-ef

:loop
set /p id="Migration className: "
cd CodeNotion.Academy.OrderScheduling
dotnet-ef --startup-project ..\CodeNotion.Academy.OrderScheduling migrations add %id% --context OrderDbContext
PAUSE
goto :loop
