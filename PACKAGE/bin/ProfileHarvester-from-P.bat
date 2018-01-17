set IEDUCG_OUT_NAME=IEDUCG-out.txt
set IEDUCG_ERR_NAME=IEDUCG-err.txt
del err.txt
del out.txt
del %IEDUCG_OUT_NAME%
del %IEDUCG_ERR_NAME%
del c:\tmp\err.txt
del c:\tmp\out.txt
del c:\tmp\%IEDUCG_OUT_NAME%
del c:\tmp\%IEDUCG_ERR_NAME%
net use G: \\FCAFILES\main
net use P: \\FCAFILES\public
net use T: \\FCAFILES\student
C:
cd\
del err.txt
del out.txt
del %IEDUCG_OUT_NAME%
del %IEDUCG_ERR_NAME%
"P:\Computers\Projects\ProfileHarvester\ProfileHarvester.exe" 1>C:\%IEDUCG_OUT_NAME% 2>C:\%IEDUCG_ERR_NAME%
rename %IEDUCG_OUT_NAME% %computername%-out.txt
move /y %computername%-out.txt P:\Computers\Projects\ProfileHarvester\
rename %IEDUCG_ERR_NAME% %computername%-err.txt
move /y %computername%-err.txt P:\Computers\Projects\ProfileHarvester\
net use /d G:
net use /d P:
net use /d T:
