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

CD "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\"
ECHO Building...
MSBuild.exe "%~dp0%SOLUTION_FILENAME%" /t:Rebuild /p:Configuration=Release /p:Platform="x86"
ENDLOCAL
