SetCompressor /SOLID lzma

!define PRODUCT_NAME "Click-to-Dial"
!define PRODUCT_VERSION "4.0.0"
!define PRODUCT_PUBLISHER "FairManager"
!define PRODUCT_URL https://fairmanager.de/

!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

!include "MUI.nsh"
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE English

Name "${PRODUCT_NAME}"
OutFile "clicktodial-${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\${PRODUCT_PUBLISHER}\${PRODUCT_NAME}"
ShowInstDetails show
ShowUnInstDetails show

Section "Application" SECID_MAIN
	DetailPrint "Installing main application files..."
	ClearErrors

	SetOutPath "$INSTDIR"
	SetOverwrite on

	File "..\bin\dial.exe"
	File "..\bin\dial.exe.config"

	File "..\bin\ITapi3.dll"
	File "..\bin\ITapi3.xml"

	File "..\bin\log4net.config"
	File "..\bin\log4net.dll"
	File "..\bin\log4net.xml"

	File "..\bin\setup.exe"
	File "..\bin\setup.exe.config"
SectionEnd

Section "Run Setup" SECID_SERVICE
	DetailPrint "Running setup..."
	ClearErrors

	ExecWait '"$INSTDIR\setup.exe"'
SectionEnd

Section -Post
	WriteUninstaller "$INSTDIR\uninst.exe"
	WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
	WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"	
	WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
	WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_URL}"
	WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function un.onUninstSuccess
	HideWindow
	MessageBox MB_ICONINFORMATION|MB_OK "Application was successfully removed from your computer."
FunctionEnd

Function un.onInit
	MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove FairManager Click-to-Dial and all of its components?" IDYES +2
	Abort
FunctionEnd

Section Uninstall
	Delete "$INSTDIR\*"

	RMDir "$INSTDIR"
	RMDir "$INSTDIR\.."

	DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"

	SetAutoClose true
SectionEnd
