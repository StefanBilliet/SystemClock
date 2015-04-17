cls
echo off
echo -------------------------------
echo Script that builds UltraGendaJS
echo -------------------------------
"%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" /v:n "pack.proj" /target:Pack /logger:FileLogger,Microsoft.Build.Engine;LogFile=Pack.log

pause