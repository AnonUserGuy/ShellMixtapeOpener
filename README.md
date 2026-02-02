# ShellMixtapeOpener
Very simple BepInEx mod for Bits & Bops that allows opening custom mixtapes on the credits menu in the same manner as opening them on the alpha disclaimer. 

Made because I needed to debug [BopCustomTextures](https://github.com/AnonUserGuy/BopCustomTextures) interacting with RiqMenu, but RiqMenu doesn't work on my computer so I needed a workaround.

## Installation
1. Install [BepInEx 5.x](https://docs.bepinex.dev/articles/user_guide/installation/index.html) in Bits & Bops.
2. Download `ShellMixtapeOpener.dll` from the latest [release](https://github.com/AnonUserGuy/ShellMixtapeOpener/releases/), and place it in ``<Bits & Bops Installation>/BepinEx/plugins/``.

## Usage
1. Navigate to the credits menu. (on the title screen, third option down.)
2. Press the keyboard combination **ctrl+M**.
3. Your OS's file explorer will appear. Select a .bop mixtape file to open it.
4. The mixtape will begin playing immediately, acting in the same manner as opening a custom mixtape on the alpha disclaimer.

## Building 
### Prerequisites
- Bits & Bops v1.6+
- Microsoft .NET SDK v4.7.2+
- Visual Studio 2022 (Optional)

### Steps
1. Clone this repository using ``git clone https://github.com/AnonUserGuy/ShellMixtapeOpener.git``.
2. From ``<Bits & Bops installation>/Bits & Bops_Data/Managed/``, copy ``Assembly-CSharp.dll`` and ``StandaloneFileBrowser.dll`` into ``ShellMixtapeOpener/lib/``.
3. Build
    - Using CLI:
      ```bash
      dotnet restore ShellMixtapeOpener.sln
      dotnet build ShellMixtapeOpener.sln
      ```
    - Using Visual Studio 2022:
       - Open ShellMixtapeOpener.sln with Visual Studio 2022.
       - Set build mode to "release".
       - Build project.
4. Copy ``ShellMixtapeOpener/ShellMixtapeOpener/bin/Release/net472/ShellMixtapeOpener.dll`` into ``<Bits & Bops Installation>/BepinEx/plugins/``.

### Configuration (Optional)
You can setup `ShellMixtapeOpener.csproj.user` file next to `ShellMixtapeOpener.csproj` with the `PostBuildCopyDestination` path set to automatically copy the new DLL after build:
```xml
<Project>
  <PropertyGroup>
    <PostBuildCopyDestination>&lt;Bits &amp; Bops Installation&gt;/BepInEx/plugins</PostBuildCopyDestination>
  </PropertyGroup>
</Project>
```
