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

[Files]
Source: "..\bin\dial.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\dial.exe.config"; DestDir: "{app}";
Source: "..\bin\dial.exe.log4net"; DestDir: "{app}";

Source: "..\bin\ITapi3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\ITapi3.xml"; DestDir: "{app}";

Source: "..\bin\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\log4net.xml"; DestDir: "{app}";

Source: "..\bin\setup.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\setup.exe.config"; DestDir: "{app}";

[Run]
Filename: {app}\setup.exe; Description: "Register protocol handler"; StatusMsg: "Registering protocol handler..."; Flags: postinstall runascurrentuser runhidden

[UninstallRun]
Filename: {app}\setup.exe; Parameters: "--uninstall"; StatusMsg: "Registering protocol handler..."
