set service_name=%1
if "%service_name%"=="" (set service_name=dot-net-5-service)

call dnu restore
call dnu publish src\dot-net-5-service --out publish-output --runtime active --no-source

sc stop %service_name%
sc delete %service_name%

sc create %service_name% binPath= "\"%~dp0publish-output\approot\run.cmd\" --windows-service"
sc start %service_name%