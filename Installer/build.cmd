@ECHO OFF

ECHO Clearing output folders...
CD ..
FOR /D /R . %%d in (bin obj) do @IF EXIST "%%d" RD /S /Q "%%d"

ECHO Building project...
CD Installer
IF EXIST Output RD /S /Q Output
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe" /Build "Release|x86" "%~dp0..\ClickToDial.sln"

ECHO Building installer...
"C:\Program Files (x86)\Inno Setup 5\ISCC.exe" "%~dp0ClickToDial.iss"

IF EXIST "../.certificates/PFX_PASSWORD" (
	SET /p FAIRMANAGER_CODE_SIGNING_PASSWORD=<"../.certificates/PFX_PASSWORD"

	ECHO Signing uninstaller...
	"C:\Program Files (x86)\Windows Kits\10\bin\x86\signtool.exe" sign /f "../.certificates/fairmanager-production-codesign.pfx" /p "%FAIRMANAGER_CODE_SIGNING_PASSWORD%" "Uninstaller/*"
	"C:\Program Files (x86)\Windows Kits\10\bin\x86\signtool.exe" timestamp /t http://timestamp.comodoca.com/authenticode "Uninstaller/*"
	ECHO If this is the first time the uninstaller was built, you now need to run the setup build again!

	ECHO Signing installer...
	"C:\Program Files (x86)\Windows Kits\10\bin\x86\signtool.exe" sign /f "../.certificates/fairmanager-production-codesign.pfx" /p "%FAIRMANAGER_CODE_SIGNING_PASSWORD%" "Output/*"
	"C:\Program Files (x86)\Windows Kits\10\bin\x86\signtool.exe" timestamp /t http://timestamp.comodoca.com/authenticode "Output/*"
	ECHO If the installer could not be signed, this might be due to the uninstaller not having been signed yet. Run the build again.
) ELSE ECHO No PFX password file found. Skipping signing.
