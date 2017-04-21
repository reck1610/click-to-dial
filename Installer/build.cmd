@ECHO OFF
REM Clear all tempoary and build directories
CD ..
FOR /d /r . %%d in (bin obj) do @IF exist "%%d" RD /s/q "%%d"
CD Installer

CD ..
build.cmd

REM Make installer
"C:\Program Files (x86)\NSIS\makensis.exe" "%~dp0\ClickToDial.nsi"
