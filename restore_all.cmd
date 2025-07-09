@echo off

echo -------------------
echo   RESTORING BUILDS
echo -------------------

msbuild /t:Restore "Assembly - CSharp\Assembly---CSharp.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "Assembly - CSharp - first pass\Assembly---CSharp---first-pass.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "Assembly - UnityScript\Assembly---UnityScript.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "Assembly - UnityScript - first pass\Assembly---UnityScript---first-pass.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "Ionic.Zlib\Ionic.Zlib.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "TerrainControllerData\TerrainControllerData.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "UnityEngine\UnityEngine.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "Boo.Lang\Boo.Lang.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "UnityScript.Lang\UnityScript.Lang.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Restore "UnityDomainLoad\UnityDomainLoad.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%