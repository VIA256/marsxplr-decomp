# Mars Explorer Decompilation Project
## About
My amatuer attempt at decompiling Mars Explorer so that it can be improved to run on newer machines and other platforms
<hr>
<details closed>
<summary><h2>Assembly - UnityScript</h2></summary>
<h3>About</h3>
<ul>
  <li>(almost) all of the games compiled unityscripts</li>
  <li>Used ILSpy to decompile dll from marsxplr 2.22 win32 into C#</li>
  <li>Updated upd3 address (Lobby.cs)</li>
  <li>Updated game version from 2.22 to 2.3 (GameData.cs)</li>
  <li>changed max bots from 10 to 25</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>Assembly - UnityScript - first pass</h2></summary>
<h3>About</h3>
<ul>
  <li>some of the games compiled unityscripts</li>
  <li>used ILSpy to decompile</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>Assembly - CSharp</h2></summary>
<h3>About</h3>
<ul>
  <li>(almost) all of the games compiled C# scripts</li>
  <li>decompiled with ilspy</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>Assembly - CSharp - first pass</h2></summary>
<h3>About</h3>
<ul>
  <li>some of the games compiled C# scripts</li>
  <li>decompiled with ilspy</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>Mars Explorer</h2></summary>
<h3>About</h3>
<ul>
  <li>Mars Explorer 2.22 win32 game directory</li>
  <li>Libraries in here are referenced until they too will decompiled</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>marsxplr_exe</h2></summary>
<h3>About</h3>
<ul>
  <li>ghidra project for Mars Explorer.exe</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>Ionic.Zlib</h2></summary>
<h3>About</h3>
<ul>
  <li>Ionic.Zlib 1.8.4.24</li>
  <li>decomipled with ilspy</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>TerrainControllerData</h2></summary>
<h3>About</h3>
<ul>
  <li>dll made by aub for the sole purpose of making terrain calculations less annoying or something</li>
  <li>decomipled with ilspy</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>UnityEngine</h2></summary>
<h3>About</h3>
<ul>
  <li>Unity Engine functionality</li>
  <li>decomipled with ilspy</li>
</ul>
</details>
<hr>
<details closed>
<summary><h2>Boo.Lang</h2></summary>
<h3>About</h3>
<ul>
  <li>Boo programming language library</li>
  <li>part of old unity's functionality</li>
  <li>decomipled with ilspy</li>
</ul>
</details>
<hr>
<h1>building</h1>
Building mars explorer from source in its entirety hasnt yet been implemented, but all of the above targets are (ideally) buildable.<br>
I have setup a quick and dirty build system for all said targets in the single build_all.cmd script.<br>
You must run it in an environment with msbuild in the path (for example, i use x64 native tools command prompt for vs2019).<br>
If the build is successfull, you should be able to find a bunch of dlls in the directory marsxplr_build\Mars Explorer_Data.<br>
You can copy the dlls from that directory into the Mars Explorer_Data directory of your own personal Mars Explorer install.<br>
