cls

SET DIR=%~dp0%

"%PROGRAMFILES(X86)%\MSBuild\12.0\Bin\msbuild.exe" /target:UpdateNuGet /maxcpucount:4 /v:d "%DIR%/restore_packages.proj" /logger:FileLogger,Microsoft.Build.Engine;LogFile=%DIR%/UpdateNuGet.log
pause