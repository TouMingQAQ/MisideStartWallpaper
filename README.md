
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
5. **~~Fmod (音频播放中间件) [官网](https://www.fmod.com/)~~**
6. **vFolders 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vfolders-2-255470)**
7. **vInspector 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vinspector-2-252297)**
8. **vHierarchy 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vhierarchy-2-251320)**
9. **vTabs 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vtabs-2-253396)**
10. **vFavorites 2 (编辑器拓展) [商店地址](https://assetstore.unity.com/packages/tools/utilities/vfavorites-2-263643)**

## 配置路径
.\MisideStartWallpaper_Data\StreamingAssets\MiSideStartConfig.json
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
  "ClickCount": 2,
  "PlaySoundOnClick": true,
  "WallpaperVersion": "0.0.2_03"
}
```
> [!NOTE]
>StartAnimationRange : 开场动画的范围，假如x为0，y为1则必定播放下标为0的动画(邪恶米塔在哪呢？)
> 
>LookAtOffsetMultiplier : 视野跟踪的倍率，x,y是屏幕左侧XY轴的倍率，z,w是屏幕右侧XY轴的倍率。
> 
>TargetFrameRate : 壁纸目标刷新率，如果出现GPU占用过高，可以尝试调低此数值看看。
> 
>LookAtState : 视野跟踪模式，目前有三种模式，0:无任何跟踪、1：始终跟踪鼠标、2：只有当鼠标左键按下时才会跟踪。
> 
>MusicHead : 跟随音乐点头的开关。
> 
>ClickCount : 短时间内触发点击动画需要的点击次数。(小于等于0时不触发)
>
>WallpaperVersion : 程序版本
>
>PlaySoundOnClick : 点击音频是否播放


.\MisideStartWallpaper_Data\StreamingAssets\MusicHeadConfig.json
```json
{
  "MusicHeadVersion": 1,
  "v1Info": {
    "NodMinEnergy": 0.0125
  },
  "v2Info": {
    "NodEnergyThreshold": 0.01,
    "EnergyDecayFactor": 0.325,
    "PeakDetectionThreshold": 0.975,
    "SmoothingFactor": 0.675
  }
}
```

>[!NOTE]
>**MusicHeadVersion** : 算法版本，V1为1，V2为2
>
>**v1Info** : V1参数
>
>**NodMinEnergy** : 触发的最小能量值
>
>**v2Info** : V2参数
>
>**NodEnergyThreshold** : 
>  - **说明**：点头动作检测的基础能量阈值。该参数是 V2 版本中用于判断当前音频信号能量是否足够高以触发点头动作的基准值。它是与 `EnergyDecayFactor` 和 `PeakDetectionThreshold` 配合使用的，决定了在何种情况下能量波动会被认为是有效的触发
>  - **作用**：当音频能量超过此阈值时，系统会开始分析能量变化。如果能量超过设定的阈值并且符合其他条件（如音乐信号检测），则会触发点头动作
>  - **调整建议**：若环境噪声较多，可提高该阈值以避免被小的能量波动触发；若希望检测到较为细微的动作，则可以适当降低此值
> 
>**EnergyDecayFactor** : 
>  - **说明**：该参数决定了系统如何计算历史能量的衰减，模拟能量随时间衰减的过程。它控制着当前能量与历史能量之间的权重关系，较高的值意味着历史能量在计算中占据较大比例，变化较慢；较低的值则意味着当前能量对平均能量的影响较大
>  - **计算公式**：`平均能量 = (前一时刻的能量 * EnergyDecayFactor) + (当前能量 * (1 - EnergyDecayFactor))`
>  - **作用**：通过这个因子，可以使得系统对短期内的能量波动反应更加敏感或稳定。例如：较低的衰减因子使得系统对短期能量波动更加敏感，适合快速变化的音频信号
>  - **调整建议**：如果音频信号变化较慢，可以提高此值以减少对短期波动的响应；如果需要更快速地响应当前音频的变化，建议降低此值
> 
>**PeakDetectionThreshold** : 
>  - **说明**：该参数用于判断能量的峰值是否显著高于平均能量。峰值检测用于捕捉到短时间内的能量峰值，这通常对应于音频信号的节奏或显著的音量变化。当当前能量超过这个阈值时，可能表示音频中有显著的节奏或变化，进而触发点头动作
>  - **作用**：该参数决定了触发点头动作的能量标准，较高的值要求能量波动更大才能触发点头。它有助于减少因背景噪声或其他无关因素导致的误触发
>  - **调整建议**：如果想要检测更多的节奏变化，可以适当降低此值，让系统更灵敏地响应较小的波动。如果希望更加稳定、减少误触发，可以提高此值
>
>**SmoothingFactor** : 
>  - **说明**：该参数是用于对能量值进行平滑处理的滤波因子。通过将当前能量与先前能量进行加权平均，它能够减少瞬时波动对能量判断的影响，产生一个更平滑的能量变化曲线。平滑滤波使得系统对快速变化的能量波动不那么敏感，从而提高稳定性
>  - **计算公式**：`平滑能量 = (SmoothingFactor * 前一时刻的能量) + ((1 - SmoothingFactor) * 当前能量)`
>  - **作用**：平滑滤波有助于消除因音频信号的瞬时波动（如噪声或突发音量）导致的错误检测。通过调整该因子，可以控制对突发音量变化的响应速度
>  - **调整建议**：如果音频信号较为平稳，可以增加此值，以获得更平滑的响应。如果音频变化较快，且希望系统更加敏感地响应，可以适当降低此值

>**修改配置表后需要重启壁纸来应用配置表**


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
