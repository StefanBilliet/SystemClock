SET DIR=%~dp0%

"%PROGRAMFILES(X86)%\MSBuild\12.0\Bin\msbuild.exe" /target:Build /v:d "build.proj" /logger:FileLogger,Microsoft.Build.Engine;LogFile=Build.log
pause