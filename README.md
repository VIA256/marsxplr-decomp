# Mars Explorer Decompilation Project
## About
My amatuer attempt at decompiling Mars Explorer so that it can be improved to run on newer machines and other platforms (as well as ease of modding to support new 3rd party multiplayer and whirlds)
<hr>
<h1>building</h1>
Pretty much all of the game in terms of functionality has compilable source now, but the platform specific code is still a work in progress.<br>
For the time being, building the project requires visual studio 2008 and dotnet 3.5. inconvenient as it is, its necessary for as long as we rely on the original unity player runtime.<br>
If the build is successful, you should be able to find a bunch of dlls in the directory Game\marsxplr_build\Mars Explorer_Data.<br>
To test the build, can copy the contents of the built Mars Explorer_Data dir into the Mars Explorer_Data dir of your own personal Mars Explorer install in place of the original files.<br>
