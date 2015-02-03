@echo off
set ildasmFolder=%~1
set ilasmFolder=%~2
set inputFolder=%~3
set inputFilename=%~4
set debug=%~5

mkdir tempSigning
"%ildasmFolder%\sn.exe" -k tempSigning\keyPair.snk
echo Key Written
"%ildasmFolder%\ildasm.exe" "%inputFolder%\%inputFilename%" /out:"tempSigning\%inputFilename%.il"
echo Disassembled
"%ilasmFolder%\ilasm.exe" "tempSigning\%inputFilename%.il" /dll /key=tempSigning\keyPair.snk /output="tempSigning\%inputFilename%"
echo Assembled
move /Y "tempSigning\%inputFilename%" "%inputFolder%\%inputFilename%"
echo Overwritten

IF "%debug%"=="" rmdir /s /q tempSigning