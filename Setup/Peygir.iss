#define MyAppName "Peygir"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Amir Saniyan"
#define MyAppURL "http://peygir.codeplex.com/"
#define MyAppExeName "Peygir.exe"

[Setup]
AppId={{AC61F0D0-E31E-4B9A-8624-E99E19D2453F}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}\{#MyAppVersion}
DefaultGroupName={#MyAppName} {#MyAppVersion}
LicenseFile=..\License.txt
OutputBaseFilename={#MyAppName}-{#MyAppVersion}
SetupIconFile=..\Peygir\App.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\bin\Release\Peygir.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Peygir.Logic.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Peygir.Presentation.UserControls.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Peygir.Presentation.Forms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Peygir.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\Peygir.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\PeygirDatabase.mdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\fa\Peygir.Presentation.Forms.resources.dll"; DestDir: "{app}\fa"; Flags: ignoreversion
Source: "..\bin\Release\fa\Peygir.Presentation.UserControls.resources.dll"; DestDir: "{app}\fa"; Flags: ignoreversion
Source: "..\bin\Release\Help\Help.htm"; DestDir: "{app}\Help"; Flags: ignoreversion
Source: "..\bin\Release\Help\Images\Screenshot0.png"; DestDir: "{app}\Help\Images"; Flags: ignoreversion
Source: "..\bin\Release\Help\Images\Screenshot1.png"; DestDir: "{app}\Help\Images"; Flags: ignoreversion
Source: "..\bin\Release\Help\Images\Screenshot2.png"; DestDir: "{app}\Help\Images"; Flags: ignoreversion
Source: "..\bin\Release\Help\Images\Screenshot3.png"; DestDir: "{app}\Help\Images"; Flags: ignoreversion
Source: "..\bin\Release\Help\Images\Screenshot4.png"; DestDir: "{app}\Help\Images"; Flags: ignoreversion
Source: "..\bin\Release\Help\Images\Screenshot5.png"; DestDir: "{app}\Help\Images"; Flags: ignoreversion
Source: "..\bin\Release\Help\Images\Screenshot6.png"; DestDir: "{app}\Help\Images"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Help"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
