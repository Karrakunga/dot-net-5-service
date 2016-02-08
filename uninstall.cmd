set service_name=%1
if "%service_name%"=="" (set service_name=dot-net-5-service)

sc stop %service_name%
sc delete %service_name%