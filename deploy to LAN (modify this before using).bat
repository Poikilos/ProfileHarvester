REM @echo off
SET LAN_PROGRAM_FILES=\\FCAFILES\Public\Computers\Projects
IF NOT EXIST "%LAN_PROGRAM_FILES%\ProfileHarvester" md "%LAN_PROGRAM_FILES%\ProfileHarvester"
REM IF NOT EXIST "%LAN_PROGRAM_FILES%\ProfileHarvester\bin" md "%LAN_PROGRAM_FILES%\ProfileHarvester\bin"
copy bin\Release\ProfileHarvester.exe "%LAN_PROGRAM_FILES%\ProfileHarvester\"
REM copy "PACKAGE\bin\ProfileHarvester - Shortcut.lnk" "%LAN_PROGRAM_FILES%\ProfileHarvester\bin\"
copy "PACKAGE\bin\ProfileHarvester-from-P.bat" "%LAN_PROGRAM_FILES%\ProfileHarvester\"
pause
