:: msbuild /t:pack /p:Configuration=Release
:: cd .\Cmas.Backend.Modules.Contracts.Datalayer.Couchdb\bin\Release\
:: nuget push Cmas.Backend.Modules.Contracts.Datalayer.Couchdb.1.0.4.nupkg -Source http://cm-ylng-msk-04/nuget/nuget

::cd .\Cmas.Backend.Modules.Contracts.Datalayer.Couchdb\bin\Debug\
::nuget push Cmas.Backend.Modules.Contracts.Datalayer.Couchdb.1.0.8.nupkg -Source http://cm-ylng-msk-04/nuget/nuget

set project_name=Cmas.Modules.CallOffOrders.Datalayer.Couchdb
set nupkg_out_dir=bin\Debug

::dotnet pack %project_name%\%project_name%.csproj --output %nupkg_out_dir% --include-source --configuration Release
dotnet nuget push %project_name%\%nupkg_out_dir%\*.nupkg --source http://cm-ylng-msk-04/nuget/nuget

pause