# Mars Explorer Decompilation Project
## About
My amatuer attempt at decompiling Mars Explorer so that it can be improved to run on newer machines and other platforms (as well as ease of modding to support new 3rd party multiplayer and whirlds)
<hr>
<h1>building</h1>
Pretty much all of the game in terms of functionality has compilable source now, but the runtime code is still a work in progress.<br>
Building the project requires the mono mcs compiler and libraries, a c compiler*, and python.<br>
1. configure the project: <pre>python waf configure [--mono_home=...]</pre>
2. build the project: <pre>python waf build</pre>
If the build is successful, you should be able to find a bunch of dlls in the directory marsxplr_build\Mars Explorer_Data.<br>
To test the build, can copy the contents of the built Mars Explorer_Data dir into the Mars Explorer_Data dir of your own personal Mars Explorer install in place of the original files.<br>
<br>
*: c compiler is only required if you intend to build with the option --self_hosted=yes<br>
