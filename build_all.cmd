@echo off

set "flags=/p:Configuration=Debug /p:Platform=x86"

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
msbuild /t:Clean %flags% "UnityScript.Lang\UnityScript.Lang.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%

echo ----------------
echo   BUILDING ALL
echo ----------------

msbuild %flags% "Boo.Lang\Boo.Lang.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "UnityEngine\UnityEngine.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "Assembly - CSharp - first pass\Assembly---CSharp---first-pass.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "Ionic.Zlib\Ionic.Zlib.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "TerrainControllerData\TerrainControllerData.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "UnityScript.Lang\UnityScript.Lang.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "Assembly - UnityScript\Assembly---UnityScript.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "Assembly - CSharp\Assembly---CSharp.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%
msbuild %flags% "Assembly - UnityScript - first pass\Assembly---UnityScript---first-pass.csproj"
if %errorlevel% neq 0 exit /b %errorlevel%

echo -----------------
echo   COPYING FILES
echo -----------------

if exist "marsxplr_build\Mars Explorer_Data\lib" rd /S /Q "marsxplr_build\Mars Explorer_Data\lib"
if %errorlevel% neq 0 exit /b %errorlevel%
if exist "marsxplr_build\Mars Explorer_Data" rd /S /Q "marsxplr_build\Mars Explorer_Data"
if %errorlevel% neq 0 exit /b %errorlevel%
if exist marsxplr_build rd /S /Q marsxplr_build
if %errorlevel% neq 0 exit /b %errorlevel%
md marsxplr_build
if %errorlevel% neq 0 exit /b %errorlevel%
md "marsxplr_build\Mars Explorer_Data"
if %errorlevel% neq 0 exit /b %errorlevel%
md "marsxplr_build\Mars Explorer_Data\lib"
if %errorlevel% neq 0 exit /b %errorlevel%

copy "Assembly - CSharp\bin\e36192721fc364533a8edf2aefd3b72c.dll" "marsxplr_build\Mars Explorer_Data\Assembly - CSharp.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "Assembly - CSharp - first pass\bin\26998b3a9cbf54825a27e5f2d3cc4df1.dll" "marsxplr_build\Mars Explorer_Data\Assembly - CSharp - first pass.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "Assembly - UnityScript\bin\58cc2f0ae478d40e7a89c7ba576c3586.dll" "marsxplr_build\Mars Explorer_Data\Assembly - UnityScript.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "Assembly - UnityScript - first pass\bin\50e2df949ee0745d0a011b02942f43d5.dll" "marsxplr_build\Mars Explorer_Data\Assembly - UnityScript - first pass.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "Ionic.Zlib\bin\Ionic.Zlib.dll" "marsxplr_build\Mars Explorer_Data\Ionic.Zlib.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "TerrainControllerData\bin\MonoDevelop DLLs.dll" "marsxplr_build\Mars Explorer_Data\TerrainControllerData.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "UnityEngine\bin\UnityEngine.dll" "marsxplr_build\Mars Explorer_Data\lib\UnityEngine.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "Boo.Lang\bin\Boo.Lang.dll" "marsxplr_build\Mars Explorer_Data\lib\Boo.Lang.dll"
if %errorlevel% neq 0 exit /b %errorlevel%
copy "UnityScript.Lang\bin\UnityScript.Lang.dll" "marsxplr_build\Mars Explorer_Data\lib\UnityScript.Lang.dll"
if %errorlevel% neq 0 exit /b %errorlevel%

echo -----------------
echo   BUILD SUCCESS
echo -----------------