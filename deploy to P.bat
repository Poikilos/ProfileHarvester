REM @echo off
SET LAN_PROGRAM_FILES=P:\Computers\Projects
IF NOT EXIST "%LAN_PROGRAM_FILES%\ProfileHarvester" md "%LAN_PROGRAM_FILES%\ProfileHarvester"
IF NOT EXIST "%LAN_PROGRAM_FILES%\ProfileHarvester\bin" md "%LAN_PROGRAM_FILES%\ProfileHarvester\bin"
copy bin\Release\ProfileHarvester.exe "%LAN_PROGRAM_FILES%\ProfileHarvester\bin\"
REM copy "PACKAGE\bin\ProfileHarvester - Shortcut.lnk" "%LAN_PROGRAM_FILES%\ProfileHarvester\bin\"
copy "PACKAGE\bin\ProfileHarvester-from-P.bat" "%LAN_PROGRAM_FILES%\ProfileHarvester\bin\"
pause
