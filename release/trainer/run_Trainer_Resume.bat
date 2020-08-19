@echo off
set yamlName=agentF1
set runID=testrun_00
echo:

echo What is the name of the yaml file? [agentF1]
set /p yamlName=
echo:

echo What is the name of the run ID you wish to resume training? [testrun_00]
set /p runID=
echo:

python .\build\python-lua\__main__.py .\settings\rewardFunction.py

start cmd /k tensorboard --logdir=results --port=6006
timeout 5
start "" http://localhost:6006/

echo mlagents-learn .\settings\%yamlName%.yaml --env=.\build\ML_AutoRacer_Trainer --run-id=%runID% --resume
mlagents-learn .\settings\%yamlName%.yaml --env=.\build\ML_AutoRacer_Trainer --run-id=%runID% --resume

echo.
pause
