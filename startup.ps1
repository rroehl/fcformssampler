$target=[System.EnvironmentVariableTarget]::Process
[System.Environment]::SetEnvironmentVariable("CORECLR_ENABLE_PROFILING","1",$target)
[System.Environment]::SetEnvironmentVariable("CORECLR_PROFILER","{846F5F1C-F9AE-4B07-969E-05C26BC060D8}",$target)
[System.Environment]::SetEnvironmentVariable("COR_ENABLE_PROFILING","1",$target)
[System.Environment]::SetEnvironmentVariable("COR_PROFILER","{846F5F1C-F9AE-4B07-969E-05C26BC060D8}",$target)
[System.Environment]::SetEnvironmentVariable("DD_ENV","dev1",$target) 
[System.Environment]::SetEnvironmentVariable("DD_VERSION","7.64.3",$target)
[System.Environment]::SetEnvironmentVariable("DD_LOGS_INJECTION","true",$target)
[System.Environment]::SetEnvironmentVariable("DD_RUNTIME_METRICS_ENABLED","true",$target)
Start-Process ".\bin\Debug\net8.0-windows\formsample.exe"