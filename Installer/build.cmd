@ECHO OFF
REM Build all projects
CD ..
CALL build.cmd

REM Make installer
CD Installer
"C:\Program Files (x86)\NSIS\makensis.exe" "%~dp0\ClickToDial.nsi"
