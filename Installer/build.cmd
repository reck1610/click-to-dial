@ECHO OFF
REM Build all projects
CD ..
CALL build.cmd

REM Make installer
CD Installer
"C:\Program Files (x86)\NSIS\makensis.exe" "%~dp0\ClickToDial.nsi"

"C:\Program Files (x86)\Windows Kits\10\bin\x86\signtool.exe" sign /f "..\ClickToDial\fairmanager-production-codesign.pfx" /p "%FAIRMANAGER_CODE_SIGNING_PASSWORD%" *.exe
"C:\Program Files (x86)\Windows Kits\10\bin\x86\signtool.exe" timestamp /t http://timestamp.comodoca.com/authenticode *.exe