# Mars Explorer Decompilation Project
## About
My amatuer attempt at decompiling Mars Explorer so that it can be improved to run on newer machines and other platforms (as well as ease of modding to support new 3rd party multiplayer and whirlds)
<hr>
<h1>building</h1>
Pretty much all of the game in terms of functionality has compilable source now, but the platform specific code is still a work in progress.<br>
I have setup a quick and dirty build system for all said targets in the single build_all.cmd script (and clean_all.cmd).<br>
At the moment the way the project is built is compiling each targets's respective .csproj using Command Line For Visual Studio 2008<br>
If the build is successful, you should be able to find a bunch of dlls in the directory marsxplr_build\Mars Explorer_Data.<br>
To test the build, can copy the dlls from that directory into the Mars Explorer_Data directory of your own personal Mars Explorer install in place of the original files.<br>
