![MisideStartWallpaper](https://socialify.git.ci/TouMingQAQ/MisideStartWallpaper/image?description=1&font=Raleway&forks=1&issues=1&language=1&name=1&owner=1&pattern=Circuit%20Board&pulls=1&stargazers=1&theme=Auto)

# Miside Start Wallpaper

- 一款基于 Unity 对于 Miside 主菜单页面的壁纸实现 

## Unity 版本
- **版本：** 6000.0.32f1  
- **链接：** unityhub://6000.0.32f1/b2e806cf271c(先下载UnityHub,然后浏览器输入)

## 主要插件
1. **Final IK (头部和眼球跟踪)  [商店地址](https://assetstore.unity.com/packages/tools/animation/final-ik-14290)**
2. **MagicCloth2 (布料模拟)  [商店地址](https://assetstore.unity.com/packages/tools/physics/magica-cloth-2-242307)**
3. **RealToon (三渲二)  [商店地址](https://assetstore.unity.com/packages/vfx/shaders/realtoon-pro-anime-toon-shader-65518)**
4. **CSCore (设备音频采样)  [Nuget](https://www.nuget.org/packages/CSCode)**
5. **Fmod (音频播放中间件) [官网](https://www.fmod.com/)**
6. **vFolders 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vfolders-2-255470)**
7. **vInspector 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vinspector-2-252297)**
8. **vHierarchy 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vhierarchy-2-251320)**
9. **vTabs 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vtabs-2-253396)**
10. **vFavorites 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vfavorites-2-263643)**

## 配置路径
Root\MisideStartWallpaper_Data\StreamingAssets\MiSideStartConfig.json
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
  "Resolution": {
    "x": 1600,
    "y": 900
  },
  "LookAtState": 1,
  "MusicHead": true,
  "MusicMinEnergy": 0.0125,
  "ClickCount": 2,
  "PlaySoundOnClick": true
}
```
> [!NOTE]
>StartAnimationRange : 开场动画的范围，假如x为0，y为1则必定播放下标为0的动画(邪恶米塔在哪呢？)
> 
>LookAtOffsetMultiplier : 视野跟踪的倍率，x,y是屏幕左侧XY轴的倍率，z,w是屏幕右侧XY轴的倍率。
> 
>TargetFrameRate : 壁纸目标刷新率，如果出现GPU占用过高，可以尝试调低此数值看看。
> 
>Resolution : 分辨率设置。
> 
>LookAtState : 视野跟踪模式，目前有三种模式，0:无任何跟踪、1：始终跟踪鼠标、2：只有当鼠标左键按下时才会跟踪。
> 
>MusicHead : 跟随音乐点头的开关。
> 
>MusicMinEnergy : 点头触发的最小值。
> 
>ClickCount : 短时间内触发点击动画需要的点击次数。(小于等于0时不触发)
> 
>PlaySoundOnClick : 点击音频是否播放

> [!TIP]
> 修改配置表后需要重启壁纸来应用配置表

## 资源
- **模型和动画：** 原游戏解包。[游戏商店链接](https://store.steampowered.com/app/2527500/_MiSide/)

## TODO

### 0.0.2
- [x] 点击动画开关
- [x] 添加眨眼音效
- [x] 分辨率自定义
- [x] 音乐点头触发阈值自定义
- [x] 修复已知BUG

### 0.0.3
- [ ] 更多的米塔

### Future
- [ ] 换装
- [ ] 更多的开屏动画
- [ ] 自定义背景
- [ ] 安卓壁纸（短时间没希望）

## 使用教程

### WallpaperEngine壁纸引擎
1. 下载Steam并注册账号。[官网](https://store.steampowered.com/)
2. 购买WallpaperEngine壁纸引擎。[商店链接](https://store.steampowered.com/app/431960/Wallpaper_Engine/)
3. 下载并打开WallpaperEngine。
4. 下载Release程序包并解压。 [下载链接](https://github.com/TouMingQAQ/MisideStartWallpaper/releases/download/0.0.1/MisideStartWallpapaer.zip)
5. 点击左下角壁纸编辑器。 <br>![PixPin_2025-01-13_11-33-02](https://github.com/user-attachments/assets/5647d193-f797-4f00-99f3-e50e36d42dd5)
6. 点击创建壁纸。<br>![PixPin_2025-01-13_11-34-09](https://github.com/user-attachments/assets/cceca657-4196-416b-a0cb-2a200def1c67)
7. 在文件选择框里选择刚刚下载并解压的程序包里的MisideStartWallpaper.exe文件。<br>![PixPin_2025-01-13_11-37-11](https://github.com/user-attachments/assets/3f079cbc-5c80-4591-a0c1-26ebb0e82962)
8. 应用壁纸。<br>![PixPin_2025-01-13_11-37-58](https://github.com/user-attachments/assets/9e254048-0ef8-4dcc-b1e2-05cede487dee)
9. 预览如图。<br>![PixPin_2025-01-13_11-38-12](https://github.com/user-attachments/assets/4fcbdefa-d5b5-410d-a17d-e3a19e50c089)
10. 关闭编辑器界面，然后就能在WallpapaerEngine中找到设置好的壁纸啦。<br>![PixPin_2025-01-13_11-39-21](https://github.com/user-attachments/assets/bea5f994-9676-4678-a0e5-ef4c993f1e08)

### Lively Wallpaper(免费开源替代)
1. [下载Lively Wallpaper](https://github.com/rocksdanister/lively/releases)<br>![Screenshot 2025-01-13 134451](https://github.com/user-attachments/assets/bf2fd4e0-0d6d-489d-8131-2b643ef84785)
2. 点击Add Wallpaper。<br>![Screenshot 2025-01-13 135141](https://github.com/user-attachments/assets/0cc550b3-aa6d-4f21-a690-66ddab0cd8b1)
3. 点击Choose a file。<br>![image](https://github.com/user-attachments/assets/dfd7533f-cdd9-4a38-92f6-0bbfcccc650e)
4. 选中所有文件。<br>![Screenshot 2025-01-13 134728](https://github.com/user-attachments/assets/79d18528-38c3-43e6-b4a5-f849b176d11b)
5. 点击确定，如果有弹窗提示，继续点击确定<br>![Screenshot 2025-01-13 134806](https://github.com/user-attachments/assets/8bbc37f3-0885-40db-8765-e16d0dabfb7e)
6. 选择壁纸，然后就可以使用啦。<br>![image](https://github.com/user-attachments/assets/a8264aff-f553-4c38-9eb2-45861d6666c3)



## 其他相关链接
1. [演示视频](https://www.bilibili.com/video/BV1XZcNeaEsd/)
2. [使用教程](https://www.bilibili.com/video/BV1qJc1eDEiU/)
---

> [!TIP]
> 请确保所有插件均已正确安装，并与指定的 Unity 版本兼容，以确保功能正常。
