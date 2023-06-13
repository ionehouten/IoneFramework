BACA AKU
ATAU
AKU YANG BACA

RD /S /Q "$(TargetDir)de\"

RD /S /Q "$(TargetDir)es\"

RD /S /Q "$(TargetDir)ja\"

RD /S /Q "$(TargetDir)ru\"

xcopy "$(TargetDir)$(TargetFileName)" "$(SolutionDir)Deploy\" /Y /I /R
xcopy "$(TargetDir)$(TargetFileName).config" "$(SolutionDir)Deploy\" /Y /I /R
xcopy "$(TargetDir)$(TargetFileName)" "C:\Program Files (x86)\Ione\Framework\" /Y /I /R