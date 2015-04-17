cls
echo off
echo -----------------------------------
echo Script that restores nuget packages
echo -----------------------------------
SET DIR=%~dp0%

"%PROGRAMFILES(X86)%\MSBuild\12.0\Bin\msbuild.exe" /v:n "%DIR%/restore_packages.proj" /target:RestorePackages /logger:FileLogger,Microsoft.Build.Engine;LogFile=%DIR%/RestorePackages.log
pause