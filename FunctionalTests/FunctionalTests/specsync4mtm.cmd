@REM Excutes the "latest" SypcSync.MTM version from the NuGet packages folder from the project directory

@pushd %~dp0

@cd /D ..\packages\SpecSync.MTM.*\tools
@set TOOLPATH=%cd%\SpecSync4MTM.exe

@cd /D %~dp0

%TOOLPATH% %*

@popd