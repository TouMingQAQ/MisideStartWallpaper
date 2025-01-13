# MisideStartWallpapaer

## Unity Version
- **version：** 6000.0.32f1  
- **link：** unityhub://6000.0.32f1/b2e806cf271c(Download UnityHub and open with a browser)

## Major plugins
1. **Final IK(Head and eye tracking)  [Asset store link](https://assetstore.unity.com/packages/tools/animation/final-ik-14290)**
2. **MagicCloth2(cloth simulation)  [Asset store link](https://assetstore.unity.com/packages/tools/physics/magica-cloth-2-242307)**
3. **RealToon(anime rendering)  [Asset store link](https://assetstore.unity.com/packages/vfx/shaders/realtoon-pro-anime-toon-shader-65518)**
4. **CSCore(decice audio sampling)  [Nuget](https://www.nuget.org/packages/CSCode)**
5. **Fmod(audio plaing) [官网](https://www.fmod.com/)**
6. **vFolders 2(editor extension) [Asset store link](https://assetstore.unity.com/packages/tools/utilities/vfolders-2-255470)**
7. **vInspector 2(editor extension) [Asset store link](https://assetstore.unity.com/packages/tools/utilities/vinspector-2-252297)**
8. **vHierarchy 2(editor extension) [Asset store link](https://assetstore.unity.com/packages/tools/utilities/vhierarchy-2-251320)**
9. **vTabs 2(editor extension) [Asset store link](https://assetstore.unity.com/packages/tools/utilities/vtabs-2-253396)**
10. **vFavorites 2(editor extension) [Asset store link](https://assetstore.unity.com/packages/tools/utilities/vfavorites-2-263643)**

## Config directory
C:\Users\{User}\AppData\LocalLow\MisideStart\MisideStartWallpaper\MiSideStartConfig.json
```json
{
  "StartAnimationRange": {
    "x": 0,
    "y": 5
  },
  "LookAtOffsetMultiplier": {
    "x": 3.0,
    "y": 3.0,
    "z": 3.0,
    "w": 3.0
  },
  "TargetFrameRate": 60,
  "LookAtState": 1,
  "MusicHead": true,
  "ClickCount": 2
}
```
StartAnimationRange:Range for the startup animation，if x is 0，y is 1, it always play the animation at index 0.(where is evil Mita?)
<br>LookAtOffsetMultiplier:Multipliers for head and eye tracking,x,y is for xy axis on the left side of the display, z,w is for xy axis on the right.
<br>TargetFrameRate:Target refresh rate, if gpu usage is too high, lower this value may help.
<br>LookAtState:Tracking mode. currently support 3 mode，0:No tracking, 1:Always enable mouse tracking, 2:Mouse tracking on holding left button.
<br>MusicHead:Switch for noding on music.
<br>ClickCount:Required clicks to trigger click animation.
### PS
After changes to config, restart to apply.


## Resources
- **Models and animations** Extracted from the original game[Steam store link](https://store.steampowered.com/app/2527500/_MiSide/)

## Usage tutorials

### WallpaperEngine
1. Download Steam and register an account. [link](https://store.steampowered.com/)
2. Purchase WallpaperEngine. [link](https://store.steampowered.com/app/431960/Wallpaper_Engine/)
3. Download and run WallpaperEngine.
4. Download Release package and unzip. [link](https://github.com/TouMingQAQ/MisideStartWallpaper/releases/download/0.0.1/MisideStartWallpapaer.zip)
5. Click wallpaper editor on the bottom-left corner. <br>![PixPin_2025-01-13_11-33-02](https://github.com/user-attachments/assets/5647d193-f797-4f00-99f3-e50e36d42dd5)
6. Click create wallpaper.<br>![PixPin_2025-01-13_11-34-09](https://github.com/user-attachments/assets/cceca657-4196-416b-a0cb-2a200def1c67)
7. Choose the unzipped MisideStartWallpaper.exe file.<br>![PixPin_2025-01-13_11-37-11](https://github.com/user-attachments/assets/3f079cbc-5c80-4591-a0c1-26ebb0e82962)
8. Apply wallpaper.<br>![PixPin_2025-01-13_11-37-58](https://github.com/user-attachments/assets/9e254048-0ef8-4dcc-b1e2-05cede487dee)
9. Preview as this image.<br>![PixPin_2025-01-13_11-38-12](https://github.com/user-attachments/assets/4fcbdefa-d5b5-410d-a17d-e3a19e50c089)
10. Close editor window, and you should find the new wallpaper in WallpapaerEngine.<br>![PixPin_2025-01-13_11-39-21](https://github.com/user-attachments/assets/bea5f994-9676-4678-a0e5-ef4c993f1e08)

### Lively Wallpaper(Free and open source alternative)
1. [Download Lively Wallpaper](https://github.com/rocksdanister/lively/releases)<br>![Screenshot 2025-01-13 134451](https://github.com/user-attachments/assets/bf2fd4e0-0d6d-489d-8131-2b643ef84785)
2. Click Add Wallpaper. <br>![Screenshot 2025-01-13 135141](https://github.com/user-attachments/assets/0cc550b3-aa6d-4f21-a690-66ddab0cd8b1)
3. Click Choose a file.<br>![image](https://github.com/user-attachments/assets/dfd7533f-cdd9-4a38-92f6-0bbfcccc650e)
4. Select all files.<br>![Screenshot 2025-01-13 134728](https://github.com/user-attachments/assets/79d18528-38c3-43e6-b4a5-f849b176d11b)
5. Click yes, if prompted again, continue clicking yes.<br>![Screenshot 2025-01-13 134806](https://github.com/user-attachments/assets/8bbc37f3-0885-40db-8765-e16d0dabfb7e)
6. Select the newly added wallpaper and enjoy.<br>![image](https://github.com/user-attachments/assets/a8264aff-f553-4c38-9eb2-45861d6666c3)

## 0.0.2 update roadmap
- [ ] Adding a click animation switch.
- [ ] Adding a blink sound.
- [ ] Customize resolution.
- [ ] Change music noding trigger threshold.
- [ ] Adding More Mita.

## Features may add on future updates
- [ ] Clothes changing
- [ ] More startup animations
- [ ] Custom background image
- [ ] Adding support for android(unlikely in short-term)

## Other related links
1. [Demo video](https://www.bilibili.com/video/BV1XZcNeaEsd/)
2. [Usage tutorial](https://www.bilibili.com/video/BV1qJc1eDEiU/)
---

### 注意
请确保所有插件均已正确安装，并与指定的 Unity 版本兼容，以确保功能正常。
