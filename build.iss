#define MyAppName "SchoolWallpaperChanger"
#define MyAppVersion "1.3.1"
#define MyAppPublisher "AGG-Productions"
#define MyAppExeName "SchoolWallpaperChanger.exe"

[Setup]
AppId={{SchoolWallpaperChanger}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName=C:\Users\Public\{#MyAppName}
DisableDirPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
OutputDir=./
OutputBaseFilename=SchoolWallpaperChanger.Installer
SetupIconFile=.\Images\icon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce

[Files]
Source: ".\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\SchoolWallpaperChanger.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\SchoolWallpaperChanger.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\bin\Release\SchoolWallpaperChanger.pdb"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
