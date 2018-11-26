@ECHO OFF
SETLOCAL
SET SOLUTION_FILENAME=ClickToDial.sln
IF EXIST packages\NuGet.exe (
  SET EnableNuGetPackageRestore=true
  echo Installing NuGet pacakges...
  FOR /F "delims=" %%n in ('dir /b /s packages.config') DO (
    packages\NuGet.exe install "%%n" -o packages
  )
)

ECHO Building '%~dp0%SOLUTION_FILENAME%'...
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe" /Build "Release|x86" "%~dp0%SOLUTION_FILENAME%"
ENDLOCAL