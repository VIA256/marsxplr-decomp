@echo off

set "flags=/p:Configuration=Debug /p:Platform=Win32"

echo -------------------
echo   CLEANING BUILDS
echo -------------------

msbuild /t:Clean %flags% "Assembly - CSharp\Assembly---CSharp.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "Assembly - CSharp - first pass\Assembly---CSharp---first-pass.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "Assembly - UnityScript\Assembly---UnityScript.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "Assembly - UnityScript - first pass\Assembly---UnityScript---first-pass.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "Ionic.Zlib\Ionic.Zlib.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "TerrainControllerData\TerrainControllerData.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "UnityEngine\UnityEngine.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "Boo.Lang\Boo.Lang.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild /t:Clean %flags% "UnityDomainLoad\UnityDomainLoad.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
