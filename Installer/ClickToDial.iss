#preproc ispp

#define BrandName "FairManager"
#define ApplicationName "Click-to-Dial"
#define ApplicationVersion GetFileVersion( "..\bin\dial.exe" )
#define ApplicationGuid "7811ec0b-a46e-4f34-af45-09923c5e9489"

[Setup]
AppId={#ApplicationGuid}
AppName={#BrandName} {#ApplicationName}
AppVersion={#ApplicationVersion}
AppPublisher="HARTWIG Communication & Events GmbH & Co. KG"
AppPublisherURL="https://fairmanager.de"

DefaultDirName={pf}\{#BrandName}\{#ApplicationName}
DefaultGroupName={#ApplicationName}

OutputBaseFilename=click-to-dial_{#ApplicationVersion}-x86

UninstallDisplayIcon={app}\dial.exe
UninstallDisplayName={#ApplicationName} {#ApplicationVersion}
SignedUninstaller=yes
SignedUninstallerDir=Uninstaller

AlwaysRestart=no
AllowNetworkDrive=no
AllowUNCPath=no
AllowRootDirectory=no

[Components]
Name: "main"; Description: "{#ApplicationName}"; Types: full compact custom; Flags: fixed
Name: "tapi"; Description: "MiVoice Office 400 First-party TAPI Service Provider"; Types: full

[Files]
Source: "..\bin\dial.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: main
Source: "..\bin\dial.exe.config"; DestDir: "{app}"; Components: main
Source: "..\bin\dial.exe.log4net"; DestDir: "{app}"; Components: main

Source: "..\bin\ITapi3.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: main
Source: "..\bin\ITapi3.xml"; DestDir: "{app}"; Components: main

Source: "..\bin\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: main
Source: "..\bin\log4net.xml"; DestDir: "{app}"; Components: main

Source: "..\bin\setup.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: main
Source: "..\bin\setup.exe.config"; DestDir: "{app}"; Components: main

Source: ".\Components\mivoice_office_400_and_intelligate_1st_party_tsp_serial_1.4.1.exe"; DestDir: "{app}"; Components: tapi

[Run]
Filename: "{app}\setup.exe"; Description: "Register protocol handler"; StatusMsg: "Registering protocol handler..."; Flags: postinstall runascurrentuser runhidden; Components: main

Filename: "{app}\mivoice_office_400_and_intelligate_1st_party_tsp_serial_1.4.1.exe"; StatusMsg: "Installing TAPI provider..."; Flags: hidewizard waituntilterminated; Components: tapi

[UninstallRun]
Filename: {app}\setup.exe; Parameters: "--uninstall"; StatusMsg: "Registering protocol handler..."; Components: main
