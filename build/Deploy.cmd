SET DIR=%~dp0%

"%PROGRAMFILES(X86)%\MSBuild\12.0\Bin\msbuild.exe" /target:Deploy /v:n "Frodo.proj" /logger:FileLogger,Microsoft.Build.Engine;LogFile=Deploy.log
pause