; Script untuk Inno Setup Compiler
; Mengemas berkas biner UCPPABD (Debug/Release mode), dependensi, dan skrip SQL setup.

#define MyAppName "Lucario Sistem Input Jadwal Pelajaran"
#define MyAppVersion "1.0"
#define MyAppPublisher "Lucario - Sistem Jadwal Pelajaran"
#define MyAppExeName "UCPPABD.exe"

[Setup]
; Gunakan GUID baru untuk project ini
AppId={{D37E8F12-E56B-4C0A-90D1-6F8AE2E5C9A9}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} v{#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Output disimpan ke subfolder Output di dalam project
OutputDir=d:\KULIAH\Lucario_SistemInputJadwalPelajaranOtomatis-master\Output
OutputBaseFilename=SistemJadwalPelajaran_Setup_v1.0
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Mengambil file executable utama
Source: "d:\KULIAH\Lucario_SistemInputJadwalPelajaranOtomatis-master\UCPPABD\bin\Debug\UCPPABD.exe"; DestDir: "{app}"; Flags: ignoreversion
; Mengambil semua DLL dependencies (Crystal Reports, ExcelDataReader, dll)
Source: "d:\KULIAH\Lucario_SistemInputJadwalPelajaranOtomatis-master\UCPPABD\bin\Debug\*.dll"; DestDir: "{app}"; Flags: ignoreversion
; Mengambil file konfigurasi awal jika ada
Source: "d:\KULIAH\Lucario_SistemInputJadwalPelajaranOtomatis-master\UCPPABD\bin\Debug\db_config.txt"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist
; Mengambil file database setup script SQL
Source: "d:\KULIAH\Lucario_SistemInputJadwalPelajaranOtomatis-master\database_setup.sql"; DestDir: "{app}\SQL"; Flags: ignoreversion
; Mengambil file Crystal Reports .rpt
Source: "d:\KULIAH\Lucario_SistemInputJadwalPelajaranOtomatis-master\UCPPABD\*.rpt"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
