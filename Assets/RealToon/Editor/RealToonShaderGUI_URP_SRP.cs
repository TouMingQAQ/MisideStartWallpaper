//RealToonGUI URP
//MJQStudioWorks
//©2024

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;

namespace RealToon.GUIInspector
{

    public class RealToonShaderGUI_URP_SRP : ShaderGUI
    {

        #region foldout bools variable

        static bool ShowTextureColor;
        static bool ShowNormalMap;
        static bool ShowTransparency;
        static bool ShowMatCap;
        static bool ShowCutout;
        static bool ShowColorAdjustment;
        static bool ShowOutline;
        static bool ShowSelfLit;
        static bool ShowGloss;
        static bool ShowShadow;
        static bool ShowLighting;
        static bool ShowReflection;
        static bool ShowRimLight;
        static bool ShowSeeThrough;
        static bool NearFadeDithering;
        static bool Triplanar;
        static bool ShowPerspecAdju;
        static bool ShowSmeEff;
        //static bool ShowTessellation; In Progress
        static bool ShowDisableEnable;
        static bool ShowSettings;
        //static bool ShowFReflection; //remove later
        //static bool ShowLightmapping; //remove later
        //static bool ShowDepth; //remove later

        static bool ShowUI = true;

        string LightBlendString = "Anime/Cartoon";
        static string ShowUIString = "Hide Settings";


        #endregion

        #region Variables

string realtoon_version = "5.0.10";
string shader_type = "Default";
string srp_mode = "URP";
bool del_skw = false;
static bool aruskw = false;

static bool UseSSOL = true;
static string UseSSOLStat = "Use Screen Space Outline";
static string OLType = "Traditional";

static bool remoout = true;
static string remooutstat = "Remove Outline";

static bool twofourfive_target = false;
static string twofourfive_target_string = "Change shader compilation target to 4.5";

static bool dots_lbs_cd = false;
static string dots_lbs_cd_string = "DOTS|HR - Use Compute Deformation";

static bool add_st = true;
static string add_st_string = "Add 'See Through' feature";

        Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/RealToon/Editor/RT_GUI_Img.png", typeof(Texture2D));

        #endregion

        #region Material Properties Variables

        MaterialProperty _Culling = null;
        MaterialProperty _TRANSMODE = null;

        MaterialProperty _MainTex = null;
        MaterialProperty _TexturePatternStyle = null;
        MaterialProperty _MainColor = null;
        MaterialProperty _MaiColPo = null;
        MaterialProperty _MVCOL = null;
        MaterialProperty _MCIALO = null;

        MaterialProperty _MCapIntensity = null;
        MaterialProperty _MCap = null;
        MaterialProperty _SPECMODE = null;
        MaterialProperty _SPECIN = null;
        MaterialProperty _MCapMask = null;

        MaterialProperty _Cutout = null;
        MaterialProperty _UseSecondaryCutout = null;
        MaterialProperty _SecondaryCutout = null;
        MaterialProperty _AlphaBaseCutout = null;
        MaterialProperty _N_F_SCO = null;
        MaterialProperty _N_F_COEDGL = null;
        MaterialProperty _Glow_Color = null;
        MaterialProperty _Glow_Edge_Width = null;

        MaterialProperty _Opacity = null;
        MaterialProperty _TransparentThreshold = null;
        MaterialProperty _MaskTransparency = null;
        MaterialProperty _BleModSour = null;
        MaterialProperty _BleModDest = null;

        MaterialProperty _SimTrans = null;
        MaterialProperty _TransAffSha = null;

        MaterialProperty _NormalMap = null;
        MaterialProperty _NormalMapIntensity = null;

        MaterialProperty _Saturation = null;

        MaterialProperty _OutlineWidth = null;
        MaterialProperty _OutlineWidthControl = null;
        MaterialProperty _OutlineExtrudeMethod = null;
        MaterialProperty _OutlineOffset = null;
        MaterialProperty _OutlineZPostionInCamera = null;
        MaterialProperty _DoubleSidedOutline = null;
        MaterialProperty _OutlineColor = null;
        MaterialProperty _MixMainTexToOutline = null;
        MaterialProperty _NoisyOutlineIntensity = null;
        MaterialProperty _DynamicNoisyOutline = null;
        MaterialProperty _LightAffectOutlineColor = null;
        MaterialProperty _OutlineWidthAffectedByViewDistance = null;
        MaterialProperty _FarDistanceMaxWidth = null;
        MaterialProperty _VertexColorBlueAffectOutlineWitdh = null;

        MaterialProperty _N_F_MSSOLTFO = null;
        MaterialProperty _DepthThreshold = null;

        MaterialProperty _SelfLitIntensity = null;
        MaterialProperty _SelfLitColor = null;
        MaterialProperty _SelfLitPower = null;
        MaterialProperty _TEXMCOLINT = null;
        MaterialProperty _SelfLitHighContrast = null;
        MaterialProperty _MaskSelfLit = null;

        MaterialProperty _GlossIntensity = null;
        MaterialProperty _Glossiness = null;
        MaterialProperty _GlossSoftness = null;
        MaterialProperty _GlossColor = null;
        MaterialProperty _GlossColorPower = null;
        MaterialProperty _MaskGloss = null;

        MaterialProperty _GlossTexture = null;
        MaterialProperty _GlossTextureSoftness = null;
        MaterialProperty _PSGLOTEX = null;
        MaterialProperty _GlossTextureRotate = null;
        MaterialProperty _GlossTextureFollowObjectRotation = null;
        MaterialProperty _GlossTextureFollowLight = null;

        MaterialProperty _OverallShadowColor = null;
        MaterialProperty _OverallShadowColorPower = null;
        MaterialProperty _SelfShadowShadowTAtViewDirection = null;

        MaterialProperty _ReduSha = null;
        MaterialProperty _ShadowHardness = null;

        MaterialProperty _HighlightColor = null;
        MaterialProperty _HighlightColorPower = null;

        MaterialProperty _SelfShadowRealtimeShadowIntensity = null;
        MaterialProperty _SelfShadowThreshold = null;
        MaterialProperty _VertexColorGreenControlSelfShadowThreshold = null;
        MaterialProperty _SelfShadowHardness = null;
        MaterialProperty _SelfShadowRealTimeShadowColor = null;
        MaterialProperty _SelfShadowRealTimeShadowColorPower = null;
        MaterialProperty _LigIgnoYNorDir = null;
        MaterialProperty _SelfShadowAffectedByLightShadowStrength = null;

        MaterialProperty _SmoothObjectNormal = null;
        MaterialProperty _VertexColorRedControlSmoothObjectNormal = null;
        //MaterialProperty _XYZPosition = null;
        MaterialProperty _ShowNormal = null;

        MaterialProperty _ShadowColorTexture = null;
        MaterialProperty _ShadowColorTexturePower = null;

        MaterialProperty _ShadowTIntensity = null;
        MaterialProperty _ShadowT = null;
        MaterialProperty _ShadowTLightThreshold = null;
        MaterialProperty _ShadowTShadowThreshold = null;
        MaterialProperty _ShadowTColor = null;
        MaterialProperty _ShadowTColorPower = null;
        MaterialProperty _ShadowTHardness = null;
        MaterialProperty _STIL = null;
        MaterialProperty _N_F_STIS = null;
        MaterialProperty _N_F_STIAL = null;
        MaterialProperty _ShowInAmbientLightShadowIntensity = null;
        MaterialProperty _ShowInAmbientLightShadowThreshold = null;
        MaterialProperty _LightFalloffAffectShadowT = null;
        MaterialProperty _N_F_STSDFM = null;

        MaterialProperty _PTexture = null;
        MaterialProperty _PTCol = null;
        MaterialProperty _PTexturePower = null;

        MaterialProperty _RELG = null;
        MaterialProperty _EnvironmentalLightingIntensity = null;

        MaterialProperty _GIFlatShade = null;
        MaterialProperty _GIShadeThreshold = null;
        MaterialProperty _LightAffectShadow = null;
        MaterialProperty _LightIntensity = null;

        MaterialProperty _UseTLB = null;
        MaterialProperty _N_F_EAL = null;

        MaterialProperty _DirectionalLightIntensity = null;
        MaterialProperty _PointSpotlightIntensity = null;
        MaterialProperty _LightFalloffSoftness = null;

        MaterialProperty _CustomLightDirectionIntensity = null;
        MaterialProperty _CustomLightDirectionFollowObjectRotation = null;
        MaterialProperty _CustomLightDirection = null;

        MaterialProperty _ReflectionIntensity = null;
        MaterialProperty _ReflectionRoughtness = null;
        MaterialProperty _RefMetallic = null;
        MaterialProperty _MaskReflection = null;
        MaterialProperty _FReflection = null;

        MaterialProperty _RimLigInt = null;
        MaterialProperty _RimLightUnfill = null;
        MaterialProperty _RimLightColor = null;
        MaterialProperty _RimLightColorPower = null;
        MaterialProperty _RimLightSoftness = null;
        MaterialProperty _RimLightInLight = null;
        MaterialProperty _LightAffectRimLightColor = null;

        MaterialProperty _MinFadDistance = null;
        MaterialProperty _MaxFadDistance = null;

        MaterialProperty _TriPlaTile = null;
        MaterialProperty _TriPlaBlend = null;

        MaterialProperty _PresAdju = null;
        MaterialProperty _ClipAdju = null;
        MaterialProperty _PASize = null;
        MaterialProperty _PASmooTrans = null;
        MaterialProperty _PADist = null;

        //MaterialProperty _NoiseSize = null;
        //MaterialProperty _TrailSize = null;

        //MaterialProperty _TessellationSmoothness = null;
        //MaterialProperty _TessellationTransition = null;
        //MaterialProperty _TessellationNear = null;
        //MaterialProperty _TessellationFar = null;

        MaterialProperty _RefVal = null;
        MaterialProperty _Oper = null;
        MaterialProperty _Compa = null;

        MaterialProperty _N_F_ESSAO = null;
        MaterialProperty _SSAOColor = null;

        MaterialProperty _N_F_MC = null;
        MaterialProperty _N_F_NM = null;
        MaterialProperty _N_F_CO = null;
        MaterialProperty _N_F_O = null;
        MaterialProperty _N_F_CA = null;
        MaterialProperty _N_F_SL = null;
        MaterialProperty _N_F_GLO = null;
        MaterialProperty _N_F_GLOT = null;
        MaterialProperty _N_F_SS = null;
        MaterialProperty _N_F_SON = null;
        MaterialProperty _N_F_SCT = null;
        MaterialProperty _N_F_ST = null;
        MaterialProperty _N_F_PT = null;
        MaterialProperty _N_F_CLD = null;
        MaterialProperty _N_F_R = null;
        MaterialProperty _N_F_FR = null;
        MaterialProperty _N_F_RL = null;
        MaterialProperty _N_F_HDLS = null;
        MaterialProperty _N_F_HPSS = null;
        MaterialProperty _ZWrite = null;
        MaterialProperty _N_F_DCS = null;
        MaterialProperty _N_F_NLASOBF = null;
        MaterialProperty _N_F_RDC = null;
        MaterialProperty _N_F_DDMD = null;
        MaterialProperty _N_F_NFD = null;
        MaterialProperty _N_F_TP = null;
        MaterialProperty _N_F_PA = null;
        MaterialProperty _N_F_SE = null;

        MaterialProperty _N_F_OFLMB = null;

        #endregion

        #region List of Toggle Keywords

        enum SFKW
        {
            N_F_USETLB_ON,
            N_F_STIS_ON,
            N_F_STIAL_ON,
            N_F_EAL_ON,
            N_F_MC_ON,
            N_F_NM_ON,
            N_F_CO_ON,
            N_F_O_ON,
            N_F_CA_ON,
            N_F_SL_ON,
            N_F_GLO_ON,
            N_F_GLOT_ON,
            N_F_SS_ON,
            N_F_SON_ON,
            N_F_SCT_ON,
            N_F_ST_ON,
            N_F_PT_ON,
            N_F_RELGI_ON,
            N_F_CLD_ON,
            N_F_R_ON,
            N_F_FR_ON,
            N_F_RL_ON,
            N_F_HDLS_ON,
            N_F_HPSS_ON,
            N_F_DCS_ON,
            N_F_NLASOBF_ON,
            N_F_DNO_ON,
            N_F_TRANS_ON,
            N_F_TRANSAFFSHA_ON,
            N_F_OFLMB_ON,
            N_F_ESSAO_ON,
            N_F_RDC_ON,
            N_F_COEDGL_ON,
            N_F_DDMD_ON,
            N_F_SIMTRANS_ON,
            N_F_NFD_ON,
            N_F_TP_ON,
            N_F_PA_ON,
            N_F_SE_ON,
            N_F_SCO_ON,
            N_F_STSDFM_ON
        }

        #endregion

        #region TOTIPS

        string[] TOTIPS =
        {

        //Culling [0]
        "Controls which sides of polygons should be culled (not drawn).\n\n\nBack: Don’t render polygons that are facing away from the viewer.\n\nFront: Don’t render polygons that are facing towards the viewer, Used for turning objects inside-out.\n\nOff: Disables culling - all faces are drawn, This also called Double Sided." ,

        //Texture [1]
        "Main or base texture." , 

        //Texture Pattern Style [2]
        "Turn the 'Main/Base Texture' into pattern style." ,

        //Main Color [3]
        "Main or base color." ,

        //Mix Vertex Color [4]
        "Mix or show vertex color." ,

        //Main Color in Ambient Light Only [5]
        "Put the 'Main/Base Color' into ambient light." ,

        //Highlight Color [6]
        "Highlight color." ,

        //Highlight Color Power [7]
        "'Highlight Color' power or intensity." ,

        //Main Color Power [8]
        "'Main Color' power or intensity." ,

        //Blend - Source [9] [Transparent Mode]
        "Blending source.\n\n-Default Value: ScrAlpha" ,

        //Blend - Destination [10] [Transparent Mode]
        "Blending Destination.\n\n-Default Value: OneMinusScrAlpha" ,

        //Transparent Mode [11]
        "Setting the current mode from Opaque to Transparent.\n\nThis will allow you to use 'Fade Transparency' and 'Cutout' feature.",

        //Intensity [12] [MatCap]
        "MatCap intensity." ,

        //MatCap [13] [MatCap]
        "MatCap texture." ,

        //Specualar Mode [14] [MatCap]
        "Turn MatCap into specular." ,

        //Specular Power [15] [MatCap]
        "Specular intensity or power." ,

        //Mask MatCap [16] [MatCap]
        "Mask MatCap.\n\nUse a Black and White texture map.\nWhite means visible matcap while Black is not." ,

        //Cutout [17]
        "Cutout value or threshold." ,

        //Alpha Base Cutout [18] 
        "It will use the alpha/transparent channel of the 'Main/Base Texture' to cutout." ,

        //Use Secondary Cutout Only [19]
        "Use only the 'Secondary Cutout' to do the cutout." ,

        //Secondary Cutout [20]
        "Secondary texture cutout.\n\nUse a Black and White texture map.\nWhite means not cut out while Black is cutout." ,

        //Opacity [21]
        "Adjust the Transparency - Opacity of the object" ,

        //Transparent Threshold [22]
        "'Main/Base Texture' transparency threshold." ,

        //Mask Transparency [23]
        "Mask Transparency.\n\nWhite means opaque while Black means transparent." ,

        //Normal Map [24]
        "Normal Map." ,

        //Normal Map Intensity [25]
        "'Normal Map' intensity." ,

        //Saturation [26] [Color Adjustment]
        "Color saturation of the object." ,

        //Width [27] [Outline]
        "Outline main width." ,

        //Width Control [28] [Outline]
        "Controls the 'Outline Width' using texture Map.\n\nUse a Black and White texture map.\nWhite means 1 while Black means 0.\nThis will not work if the Outline main width value is 0." ,

        //Outline Extrude Method [29] [Outline]
        "Outline Extrude Methods.\n\nNormal - The outline extrusion will be based on normal direction.\n\nOrigin - The outline extrusion will be based on the center of the object." ,

        //Outline Offset [30] [Outline]
        "Outline XYZ position." ,

        //Double Sided Outline [31] [Outline]
        "Show the front side of the outline.\n\nUseful for plane object.\n'Outline Z Position In Camera' option is needed to be adjust to show the object." ,

        //Color [32] [Outline] [Outline]
        "Outline color." ,

        //Mix Main Texture To Outline [33] [Outline]
        "Mix 'Main/Base Texture' to oultine." ,

        //Noisy Outline Intensity [34] [Outline]
        "The power/intensity of the outline distortion or noise." ,

        //Dynamic Noisy Outline [35] [Outline]
        "Moving noisy or distort outline." ,

        //Light Affect Outline Color [36] [Outline]
        "Light (Brightness and Color) affect Outline color." ,

        //Outline Width Affected By View Distance [37] [Outline]
        "'Outline Width' affected by view distance." ,

        //Far Distance Max Width [38] [Outline]
        "The maximum 'Outline Width' limit when moving far from the object." ,

        //Vertex Color Blue Affect Outline Width [39] [Outline]
        "'Vertex Color Blue will affect the Outline Width.\n\nThis will not work if the Outline main width value is 0." ,

        //Intensity [40] [SelfLit]
        "How visible or strong the 'Self Lit' is." ,

        //Color [41] [SelfLit]
        "Self Lit color" ,

        //Power [42] [SelfLit]
        "'Self Lit Color' power or intensity." ,

        //Texture and Main Color Intensity [43] [SelfLit]
        "'Main/Base Texture' and 'Main/Base Color' intensity.\n\nAdjust this if the 'Main/Base Texture' and 'Main/Base Color' is too strong or too bright for Self Lit." ,

        //High Contrast [44] [SelfLit]
        "Turn Self Lit into high contrast colors and mix 'Base/Main Texture' twice." ,

        //Mask Self Lit [45] [SelfLit]
        "Mask Self Lit.\n\nUse a Black and White texture map.\nWhite means visible Self Lit while Black is not." ,

        //Gloss Intensity [46] [Gloss]
        "How visible or strong the 'Gloss' is." ,

        //Glossiness [47] [Gloss]
        "Glossiness." ,

        //Softness [48] [Gloss]
        "How soft the 'Gloss' is." ,

        //Color [49] [Gloss]
        "Gloss color" ,

        //Power [50] [Gloss]
        "'Gloss Color' power or intensity." ,

        //Mask Gloss [51] [Gloss]
        "Mask Gloss.\n\nWhite means visible Gloss while black is not." ,

        //Gloss Texture [52] [Gloss Texture]
        "A Black and White texture map to be used as gloss.\n\nWhite means gloss while Black is not." ,

        //Softness [53] [Gloss Texture]
        "The softness of the 'Gloss Texture'." ,

        //Pattern Style [54] [Gloss Texture]
        "Turn 'Gloss Texture' into pattern style." ,

        //Rotate [55] [Gloss Texture]
        "Rotate 'Gloss Texture'." ,

        //Follow Object Rotation [56] [Gloss Texture]
        "'Gloss Texture' will follow the object local rotation." ,

        //Follow Light [57] [Gloss Texture]
        "'Gloss Texture' will follow the light direction or position." ,

        //Overall Shadow Color [58]
        "Overall shadow color.\n\nThis will affect Realtime Shadow, Self Shadow/Shade and ShadowT." ,

        //Overall Shadow Color Power [59]
        "'Overall shadow Color' power or intensity." ,

        //Self Shadow & ShadowT At View Direction [60]
        "'Self Shadow' and 'ShadowT' follow your view or camera view direction." ,

        //Reduce Shadow (Point Light) [61]
        "The amount of reduce self cast shadow.\n\nThis option will only take effect when there's a Point Light." ,

        //Refresh Settings [62]
        "This will refresh and re-apply the settings properly.\n\nClick this if there are some problem, after you update, after material reset or re-import RealToon.",

        //Reduce Shadow [63]
        "The amount of reduce self cast shadow.\n\nThis option will only take effect when there's a 'Directional Light', 'Point' or 'Spot Light'." ,

        //Shadow Hardness [64] [RealTime Shadow]
        "Real time shadow hardness" ,

        //Threshold [65] [Self Shadow]
        "The amount of 'Self Shadow/Shade' on the object." ,

        //Vertex Color Green Control Self Shadow Threshold [66]
        "Controls 'Self Shadow Threshold' by using vertex color Green." ,

        //Hardness [67] [Self Shadow]
        "'Self Shadow/Shade' hardness." ,

        //Self Shadow & Real Time Shadow Color [68]
        "'Self Shadow and Real Time Shadow Color'.\n\nBefore you set/change this, Set 'Overall Shadow Color' to White." ,

        //Self Shadow & Real Time Shadow Color Power [69]
        "'Self Shadow and Real Time Shadow Color' power or intensity." ,

        //Self Shadow Affected By Light Shadow Strength [70]
        "Light shadow strength will affect self shadow visibility." ,

        //Smooth Object Normal [71]
        "The amount of smooth object normal." ,

        //Vertex Color Red Control Smooth Object Normal [72]
        "'Vertex color Red controls the amount of smooth object normal." ,

        //XYZ Position [73] [Smooth Object Normal]
        "Normal's XYZ positions." ,

        //Affect Shadow [74]
        "Transparency affect shadow." ,

        //Show Normal [75] [Smooth Object Normal]
        "Show the normal of the object." ,

        //Shadow Color Texture [76]
        "A texture to color shadow.\n\nThis includes (RealTime Shadow, Self Shadow/Shade and ShadowT.\nYou can also use your 'Main/Base Texture' and adjust 'Power' to make it dark." ,

        //Power [77] [Shadow Color Texture]
        "How strong or dark the 'Shadow Color Texture'." ,

        //Intensity [78] [ShadowT]
        "How visitble or strong the 'ShadowT' is." ,

        //ShadowT [79]
        "ShadowT or Shadow Texture, shadows in texture form.\n\nUse Black or Gray and White Flat, Gradient and Smooth texture map.\nGray and White affected by light while Black is not.\n\nFor more info and how to use and make ShadowT texture maps, see 'Video Tutorials' and 'User Guide.pdf' at the bottom of this RealToon inspector.",

        //Light Threshold [80] [ShadowT]
        "The amount of light." ,

        //Shadow Threshold [81] [ShadowT]
        "The amount of ShadowT." ,

        //Hardness [82] [ShadowT]
        "'ShadowT' hardness." ,

        //Show In Shadow [83] [ShadowT]
        "Show 'ShadowT' in shadow.\n\nThis will only be visible if realtime shadow and self shadow/shade color is not Black." ,

        //Show In Ambient Light [84] [ShadowT]
        "Show 'ShadowT' in Ambient Light.\n\nThis will only be visible if there's an Ambient Light present or GI." ,

        //Show In Ambient Light & Shadow Intensity [85] [ShadowT]
        "'ShadowT' intensity or visibility in shadow and ambient light." ,

        //Show In Ambient Light & Shadow Threshold [86] [ShadowT]
        "'ShadowT' threshold in Ambient Light and shadow." ,

        //Light Falloff Affect ShadowT [87]
        "'Point light' and 'Spot Light' light falloff affect 'ShadowT'." ,

        //PTexture [88]
        "A Black and White texture to be used as pattern for shadow.\n\nBlack means pattern while White is nothing.\nThis will not be visible if the shadow color is Black." ,

        //Power [89] [PTexture]
        "How strong or dark the pattern is." ,

        //Receive Environmental Ligthing and GI [90] [Lighting]
        "Turn on or off receive 'Environmental Ligthing' or 'GI'." ,

        //Environmental Ligthing Intensity [91] [Lighting]
        "Ambient Light, GI or Environmental Ligthing intensity on the object." ,

        //GI Flat Shade [92] [Lighting]
        "Turn GI or SH lighting shade into flat shade." ,

        //GI Shade Threshold [93] [Lighting]
        "The amount of GI Shade on the object." ,

        //Light affect Shadow [94] [Lighting]
        "Light intensity, color and light falloff affect shadows.\n\nThis will affect (RealTime shadow, Self Shadow and ShadowT)." ,

        //Directional Light Intensity [95] [Lighting]
        "Directional Light intensity received on the object." ,

        //Point and Spot Light Intensity [96] [Lighting]
        "Point and Spot light intensity received on the object." ,

        //Light Falloff Softness [97] [Lighting]
        "How soft is the point and spot light light falloff." ,

        //Intensity [98] [Custom Light Direction]
        "The amount of custom light direction." ,

        //Custom Light Direction [99] [Custom Light Direction]
        "XYZ light direction." ,

        //Follow Object Rotation [100] [Custom Light Direction]
        "'Custom Light Direction' follow object rotation." ,

        //Intensity [101] [Reflection]
        "The amount reflection visibility." ,

        //Roughness [102] [Reflection]
        "'Reflection' roughness." ,
        
        //Metallic [103] [Reflection]
        "The amount of reflection metallic look." ,
        
        //Mask Reflection [104]
        "Mask Reflection.\n\nWhite means visible relfection while Black means reflection not visible." ,

        //FReflection [105]
        "A texture or image to be used as reflection." ,

        //Unfill [106] [Rim Light]
        "Unfill the 'Rim Light' on the object." ,

        //Softness [107] [Rim Light]
        "'Rim Light' softness." ,

        //Light Affect Rim Light [108] [Rim Light]
        "Light (Brightness and Color) affect 'Rim Light'." ,

        //Color [109] [Rim Light]
        "'Rim Light' color." ,

        //Color Power [110] [Rim Light]
        "'Rim Light Color' power or intensity." ,

        //Rim Light In Light [111]
        "'Rim Light' will be visible in light only." ,

        //ID [112] [See Through]
        "ID or reference value.\n\n-Default Value: 0" ,

        //Set A [113] [See Through]
        "'A' The see through object while 'B' is the object to be seen through 'A'.\n\n-Default Value: A" ,

        //Set B [114] [See Through]
        "'A' The see through object while 'B' is the object to be seen through 'A'.\n\n-Default Value: None" ,

        //No Light and Shadow On Backface [115]
        "No light and shadow will be visible on a back of a plane/flat object or face.\n\nThis will only be take effect or visible if 'Culling' is turned 'Off' or 'Front'." ,

        //Change Shader Compilation Target To 2.0/4.5. [116]
        "This will change the Shader Compilation Target of the RealToon Shader file to '2.0' or '4.5'.\n\n*If the shader compilation target is changed to 4.5, the shader will support DOTS/DOTS Hybrid Renderer, GPU Resident and Tessellation.",

        //Hide Directional Light Shadow [117]
        "Hide received 'Directional Light' shadows on the object." ,

        //Hide Point & Spot Light Shadow [118]
        "Hide received 'Point and Spot Light' shadows on the object." ,

        //Disable Cast Shadow [119]
        "Disable object cast shadow." ,

        //ZWrite [120]
        "Turn on or off ZWrite." ,

        //Automatic Remove Unused Shader Keywords [121]
        "Remove unused shader keywords automatically in all materials with Realtoon Shader. This will take effect once this enabled and when the RealToon Inspector shown. Disable this if you experience too slow Inspector.\n\n(Warning: This will also remove stored previous shaders shader keywords.)",

        //Color[122] [PTexture]
        "'PTexture' color." ,

        //Outline Z Position In Camera [123] [Outline]
        "Adjust the outline Z position in camera space." ,

        //RealTime Shadow Intensity [124] [RealTime Shadow]
        "Adjust the realtime shadow intensity." ,

        //Rim Light Intensity [125] [RimLight]
        "'Rim Light' intensity.",

        //Self Shadow & RealTime Shadow Intensity [126]
        "Adjust the 'Self Shadow' and realtime shadow intensity." ,

        //Self Shadow Color [127] [Shadow]
        "'Self Shadow' color." ,

        //Self Shadow Color Power [128] [Shadow]
        "'Self Shadow' color power or intensity." ,

        //Color [129] [ShadowT]
        "'ShadowT' color." ,

        //Color Power [130] [ShadowT]
        "'ShadowT' color power or intensity.",

        //Ignore Light [131] [ShadowT]
        "'ShadowT' ignore direction light or light position.",

        //Light Intensity [132] [Lighting]
        "How strong is the Light in the shadow.",

        //Enable Additional Lights [133] [Lighting]
        "Enable additional lights like Point and Spot lights.",

        //Use Traditional Light Blend [134] [Lighting]
        "Use traditional light blend.\n\nIf enabled light blending will be in add mode, if not enabled the light blending will based on high or maximum light intensity and the blending will be similar to Anime or Cartoon.",

        //Remove Outline/Add Outline (On Shader) [135]
        "This will remove the Outline feature completely on the shader file or Add back the Outline feature on the shader file.\n\nThis is not per material.",

        //Video Tutorials [136]
        "RealToon's video tutorial playlist.",

        //RealToon (User Guide).pdf [137]
        "RealToon's user guide or documentation.",

        //Hide/Show UI [138]
        "This will hide or show RealToon's Inspector UI.\n\nThis is global and not per material.",

        //Depth Threshold [139] [outline]
        "This will adjust the depth based outline threshold.",

        //Mix Outline To The Shader Output [140] [outline]
        "This will mix the outline to the shader output",

        //Optimize for [Light Mode:Baked] [141]
        "If enabled, it will disable all realtime features on the shader and optimize it for [Light Mode:Baked].\n\nDisable or uncheck this for [Light Mode: RealTime or Mixed] use.",

        //Use Screen Space Outline/Use Traditional Outline [142] [outline]
        "This will enable you to use 'Screen Space Outline' or 'Traditional Outline'.\n\n'Depth Texture' needs to be turn 'On' if you use the 'Screen Space Outline'.\n\nThis is not per material.",

        //Use Linear Blend Skinning/Compute Deformation [143]
        "This will enable you to use 'Linear Blend Skinning' or 'Compute Deformation'.\n\nThis will modify the RealToon shader file.",

        //Light Ignore Y Normal Direcion [144]
        "Light will ignore Object Normal Y direction.",

        //Enable Screen Space Ambient Occlusion [145]
        "Enable SSAO or Screen Space Ambient Occlusion." ,

        //Ambient Occlusion Color [146]
        "Ambient Occlusion color or tint.",

        //Receive Decal [147]
        "The object will Receive Decal.",

        //Glow Color [148]
        "Glow edge color.",

        //Glow Edge Width [149]
        "The width of the glow.",

        //Simple Transparency Mode[150]
        "Common simple transparency.\nOnly 'Opacity', 'Blend Modes' and 'Affect Shadow' are available.\n\n'Transparent Threshold' and 'Mask Transparency' not available on this mode.",

        //Disable DOTS Mesh Deformation[151]
        "Disable DOTS Mesh Deformation: 'Linear Blend Skinning and Compute Deformation'.\n\n*For Static Objects, enabled this.",

        //Near Fade Dithering - Min Distance[152]
        "The minimum near distance.",

        //Near Fade Dithering - Max Distance[153]
        "The maximum near distance.",

        //Soft Cutout [154]
        "Dithering/Dot style cutout.\n\nFor a soft edge cutout.",

        //Tile (Triplanar) [155]
        "Tiling scale of the texture.",

        //Blend (Triplanar) [156]
        "Blending of the triplanar texture.",

        //Perspective (Perspective Adjustment) [157]
        "This will change the perspective of an object to 2D or 3D or FOV stretch look.\nFor 2d toon/anime look, set it to 0.5 or 0.",

        //Clip (Perspective Adjustment) [158]
        "This will change the clipping on the object.\nChange this if the object is overlapping front or back.\nHigher value will slice the object.",

        //Close-Up Size (Perspective Adjustment) [159]
        "This will adjust the size of the object when the camera is closer.",

        //Close-Up Size Smooth Transition (Perspective Adjustment) [160]
        "How smooth the transition of the sizing.",

        //Close-Up Size Distance (Perspective Adjustment) [161]
        "Distance transition from the camera to the object.",

        //SDF Mode (ShadowT) [162]
        "SDF Style Shadowing.\nNote: This only affect 3D Space X & Z axis.",

        //Add/Remove 'See Through' feature [163]
        "This will add or remove 'See Through' feature on the RealToon Shader.\n\nUse this if you don't need the 'See Through' feature.\n\nThis will modify the RealToon shader file."

        };

        #endregion

        #region TOTIPS for EnDisFeatures

        string[] TOTIPSEDF =
        {
        //MatCap [0]
        "MatCap or Material Capture.",

        //Normal Map [1]
        "Normal Map.",

        //Outline [2]
        "Outline.",

        //Cutout [3]
        "Cutout.",

        //Color Adjustment [4]
        "Adjust the color of the object.",

        //SelfLit [5]
        "Own light or Emission.",

        //Gloss [6]
        "Gloss.",

        //Gloss Texture [7]
        "Gloss in texture form.\n\nUse a Black and White texture map.\nWhite means gloss while Black is not.",

        //Self Shadow [8]
        "Self Shadow or Shade.",

        //Smooth Object Normal [9]
        "Smooth object normal or ignore object normal.",

        //Shadow Color Texture [10]
        "Color shadow using texture.",

        //ShadowT [11]
        "ShadowT or Shadow Texture, shadows in texture form.\n\nUse Black or Gray and White Flat, Gradient and Smooth texture map.\nGray and White affected by light while Black is not.\n\nFor more info and how to use and make ShadowT texture maps, see 'Video Tutorials' and 'User Guide.pdf' at the bottom of this RealToon inspector.",

        //PTexture [12]
        "PTexture or Pattern Texture.\n\nA Black and White texture to be used as pattern for shadow.\n\nBlack means pattern while White is nothing.\nThis will not be visible if the shadow color is Black.",

        //Custom Light Direction [13]
        "Custom light direction.",

        //Reflection [14]
        "Reflection.",

        //FReflection [15]
        "FReflection or Fake Reflection.\n\nUse any texture or image as reflection.",

        //Rim Light [16]
        "Rim light or fresnel effect.",

        //Near Fade Dithering [17]
        "Object fades when the camera near.",

        //Triplanar [18]
        "For a uniform texture scale and tiles.\n\nUseful for static objects and environment.",

        //Perspective Adjustment [19]
        "This will adjust the perspective of your object to look 2D Toon/Anime or Default 3D.",

        //Smear Effect [20]
        "Trail lines or Line noise effect when an object move fast, like the Anime/Cartoon."

        };

        #endregion

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            //This Material
            Material targetMat = materialEditor.target as Material;

            //Settings
            materialEditor.SetDefaultGUIWidths();

            //Content

            //Will remove soon if no use anymore
            #region Shader Name Switch

            //switch (targetMat.shader.name)
            //{
            //case "Universal Render Pipeline/RealToon/Version 5/Default/Default":
            //    shader_name = "default_d";
            //    shader_type = "URP - Default";
            //    break;
            //case "Universal Render Pipeline/RealToon/Version 5/Default/Fade Transparency":
            //    shader_name = "default_ft";
            //    shader_type = "URP - Fade Transperancy"; 
            //    break;
            //case "RealToon/Version 5/Default/Refraction": //Temporarily Removed
            //shader_name = "default_ref"; //Temporarily Removed
            //shader_type = "Refraction";
            //break;
            //case "RealToon/Version 5/Tessellation/Default": //Temporarily Removed
            //shader_name = "tessellation_d"; //Temporarily Removed
            //shader_type = "Tessellation - Default"; //Temporarily Removed
            //break; //Temporarily Removed
            //case "RealToon/Version 5/Tessellation/Fade Transparency": //Temporarily Removed
            //shader_name = "tessellation_ft"; //Temporarily Removed
            //shader_type = "Tessellation - Fade Transparency"; //Temporarily Removed
            //break; //Temporarily Removed
            //case "RealToon/Version 5/Tessellation/Refraction": //Temporarily Removed
            //shader_name = "tessellation_ref"; //Temporarily Removed
            //shader_type = "Tessellation - Refraction"; //Temporarily Removed
            //break; //Temporarily Removed
            //default:
            //    shader_name = string.Empty;
            //    shader_type = string.Empty;
            //    break;
            //}


            #endregion

            #region Material Properties

            _UseTLB = ShaderGUI.FindProperty("_UseTLB", properties);
            _Culling = ShaderGUI.FindProperty("_Culling", properties);
            _TRANSMODE = ShaderGUI.FindProperty("_TRANSMODE", properties);

            _MainTex = ShaderGUI.FindProperty("_MainTex", properties);
            _TexturePatternStyle = ShaderGUI.FindProperty("_TexturePatternStyle", properties);

            _MainColor = ShaderGUI.FindProperty("_MainColor", properties);
            _MaiColPo = ShaderGUI.FindProperty("_MaiColPo", properties);

            _MVCOL = ShaderGUI.FindProperty("_MVCOL", properties);
            _MCIALO = ShaderGUI.FindProperty("_MCIALO", properties);

            _MCapIntensity = ShaderGUI.FindProperty("_MCapIntensity", properties);
            _MCap = ShaderGUI.FindProperty("_MCap", properties);
            _SPECMODE = ShaderGUI.FindProperty("_SPECMODE", properties);
            _SPECIN = ShaderGUI.FindProperty("_SPECIN", properties);
            _MCapMask = ShaderGUI.FindProperty("_MCapMask", properties);

            _Cutout = ShaderGUI.FindProperty("_Cutout", properties);
            _UseSecondaryCutout = ShaderGUI.FindProperty("_UseSecondaryCutout", properties);
            _SecondaryCutout = ShaderGUI.FindProperty("_SecondaryCutout", properties);
            _AlphaBaseCutout = ShaderGUI.FindProperty("_AlphaBaseCutout", properties);
            _N_F_SCO = ShaderGUI.FindProperty("_N_F_SCO", properties);

            _N_F_COEDGL = ShaderGUI.FindProperty("_N_F_COEDGL", properties);
            _Glow_Color = ShaderGUI.FindProperty("_Glow_Color", properties);
            _Glow_Edge_Width = ShaderGUI.FindProperty("_Glow_Edge_Width", properties);

            _Opacity = ShaderGUI.FindProperty("_Opacity", properties);
            _TransparentThreshold = ShaderGUI.FindProperty("_TransparentThreshold", properties);
            _MaskTransparency = ShaderGUI.FindProperty("_MaskTransparency", properties);
            _BleModSour = ShaderGUI.FindProperty("_BleModSour", properties);
            _BleModDest = ShaderGUI.FindProperty("_BleModDest", properties);

            _SimTrans = ShaderGUI.FindProperty("_SimTrans", properties);
            _TransAffSha = ShaderGUI.FindProperty("_TransAffSha", properties);

            _NormalMap = ShaderGUI.FindProperty("_NormalMap", properties);
            _NormalMapIntensity = ShaderGUI.FindProperty("_NormalMapIntensity", properties);

            _Saturation = ShaderGUI.FindProperty("_Saturation", properties);

            _OutlineWidth = ShaderGUI.FindProperty("_OutlineWidth", properties);
            _OutlineWidthControl = ShaderGUI.FindProperty("_OutlineWidthControl", properties);
            _OutlineExtrudeMethod = ShaderGUI.FindProperty("_OutlineExtrudeMethod", properties);
            _OutlineOffset = ShaderGUI.FindProperty("_OutlineOffset", properties);
            _OutlineZPostionInCamera = ShaderGUI.FindProperty("_OutlineZPostionInCamera", properties);
            _DoubleSidedOutline = ShaderGUI.FindProperty("_DoubleSidedOutline", properties);
            _OutlineColor = ShaderGUI.FindProperty("_OutlineColor", properties);
            _MixMainTexToOutline = ShaderGUI.FindProperty("_MixMainTexToOutline", properties);
            _NoisyOutlineIntensity = ShaderGUI.FindProperty("_NoisyOutlineIntensity", properties);
            _DynamicNoisyOutline = ShaderGUI.FindProperty("_DynamicNoisyOutline", properties);
            _LightAffectOutlineColor = ShaderGUI.FindProperty("_LightAffectOutlineColor", properties);
            _OutlineWidthAffectedByViewDistance = ShaderGUI.FindProperty("_OutlineWidthAffectedByViewDistance", properties);
            _FarDistanceMaxWidth = ShaderGUI.FindProperty("_FarDistanceMaxWidth", properties);
            _VertexColorBlueAffectOutlineWitdh = ShaderGUI.FindProperty("_VertexColorBlueAffectOutlineWitdh", properties);

            _DepthThreshold = ShaderGUI.FindProperty("_DepthThreshold", properties);
            _N_F_MSSOLTFO = ShaderGUI.FindProperty("_N_F_MSSOLTFO", properties);

            _SelfLitIntensity = ShaderGUI.FindProperty("_SelfLitIntensity", properties);
            _SelfLitColor = ShaderGUI.FindProperty("_SelfLitColor", properties);
            _SelfLitPower = ShaderGUI.FindProperty("_SelfLitPower", properties);
            _TEXMCOLINT = ShaderGUI.FindProperty("_TEXMCOLINT", properties);
            _SelfLitHighContrast = ShaderGUI.FindProperty("_SelfLitHighContrast", properties);
            _MaskSelfLit = ShaderGUI.FindProperty("_MaskSelfLit", properties);

            _GlossIntensity = ShaderGUI.FindProperty("_GlossIntensity", properties);
            _Glossiness = ShaderGUI.FindProperty("_Glossiness", properties);
            _GlossSoftness = ShaderGUI.FindProperty("_GlossSoftness", properties);
            _GlossColor = ShaderGUI.FindProperty("_GlossColor", properties);
            _GlossColorPower = ShaderGUI.FindProperty("_GlossColorPower", properties);
            _MaskGloss = ShaderGUI.FindProperty("_MaskGloss", properties);

            _GlossTexture = ShaderGUI.FindProperty("_GlossTexture", properties);
            _GlossTextureSoftness = ShaderGUI.FindProperty("_GlossTextureSoftness", properties);
            _PSGLOTEX = ShaderGUI.FindProperty("_PSGLOTEX", properties);
            _GlossTextureRotate = ShaderGUI.FindProperty("_GlossTextureRotate", properties);
            _GlossTextureFollowObjectRotation = ShaderGUI.FindProperty("_GlossTextureFollowObjectRotation", properties);
            _GlossTextureFollowLight = ShaderGUI.FindProperty("_GlossTextureFollowLight", properties);

            _OverallShadowColor = ShaderGUI.FindProperty("_OverallShadowColor", properties);
            _OverallShadowColorPower = ShaderGUI.FindProperty("_OverallShadowColorPower", properties);
            _SelfShadowShadowTAtViewDirection = ShaderGUI.FindProperty("_SelfShadowShadowTAtViewDirection", properties);

            _HighlightColor = ShaderGUI.FindProperty("_HighlightColor", properties);
            _HighlightColorPower = ShaderGUI.FindProperty("_HighlightColorPower", properties);

            _SelfShadowThreshold = ShaderGUI.FindProperty("_SelfShadowThreshold", properties);
            _VertexColorGreenControlSelfShadowThreshold = ShaderGUI.FindProperty("_VertexColorGreenControlSelfShadowThreshold", properties);
            _SelfShadowHardness = ShaderGUI.FindProperty("_SelfShadowHardness", properties);

            _SelfShadowRealtimeShadowIntensity = ShaderGUI.FindProperty("_SelfShadowRealtimeShadowIntensity", properties);

            _SelfShadowRealTimeShadowColor = ShaderGUI.FindProperty("_SelfShadowRealTimeShadowColor", properties);
            _SelfShadowRealTimeShadowColorPower = ShaderGUI.FindProperty("_SelfShadowRealTimeShadowColorPower", properties);

            _LigIgnoYNorDir = ShaderGUI.FindProperty("_LigIgnoYNorDir", properties);
            _SelfShadowAffectedByLightShadowStrength = ShaderGUI.FindProperty("_SelfShadowAffectedByLightShadowStrength", properties);

            _SmoothObjectNormal = ShaderGUI.FindProperty("_SmoothObjectNormal", properties);
            _VertexColorRedControlSmoothObjectNormal = ShaderGUI.FindProperty("_VertexColorRedControlSmoothObjectNormal", properties);
            //_XYZPosition = ShaderGUI.FindProperty("_XYZPosition", properties);
            _ShowNormal = ShaderGUI.FindProperty("_ShowNormal", properties);

            _ShadowColorTexture = ShaderGUI.FindProperty("_ShadowColorTexture", properties);
            _ShadowColorTexturePower = ShaderGUI.FindProperty("_ShadowColorTexturePower", properties);

            _ShadowTIntensity = ShaderGUI.FindProperty("_ShadowTIntensity", properties);
            _ShadowT = ShaderGUI.FindProperty("_ShadowT", properties);
            _ShadowTLightThreshold = ShaderGUI.FindProperty("_ShadowTLightThreshold", properties);
            _ShadowTShadowThreshold = ShaderGUI.FindProperty("_ShadowTShadowThreshold", properties);
            _ShadowTColor = ShaderGUI.FindProperty("_ShadowTColor", properties);
            _ShadowTColorPower = ShaderGUI.FindProperty("_ShadowTColorPower", properties);
            _ShadowTHardness = ShaderGUI.FindProperty("_ShadowTHardness", properties);
            _STIL = ShaderGUI.FindProperty("_STIL", properties);
            _N_F_STIS = ShaderGUI.FindProperty("_N_F_STIS", properties);
            _N_F_STIAL = ShaderGUI.FindProperty("_N_F_STIAL", properties);
            _ShowInAmbientLightShadowIntensity = ShaderGUI.FindProperty("_ShowInAmbientLightShadowIntensity", properties);
            _ShowInAmbientLightShadowThreshold = ShaderGUI.FindProperty("_ShowInAmbientLightShadowThreshold", properties);

            _LightFalloffAffectShadowT = ShaderGUI.FindProperty("_LightFalloffAffectShadowT", properties);

            _N_F_STSDFM = ShaderGUI.FindProperty("_N_F_STSDFM", properties);

            _PTexture = ShaderGUI.FindProperty("_PTexture", properties);
            _PTCol = ShaderGUI.FindProperty("_PTCol", properties);
            _PTexturePower = ShaderGUI.FindProperty("_PTexturePower", properties);

            _EnvironmentalLightingIntensity = ShaderGUI.FindProperty("_EnvironmentalLightingIntensity", properties);
            _RELG = ShaderGUI.FindProperty("_RELG", properties);

            _GIFlatShade = ShaderGUI.FindProperty("_GIFlatShade", properties);
            _GIShadeThreshold = ShaderGUI.FindProperty("_GIShadeThreshold", properties);
            _LightAffectShadow = ShaderGUI.FindProperty("_LightAffectShadow", properties);
            _LightIntensity = ShaderGUI.FindProperty("_LightIntensity", properties);

            _N_F_EAL = ShaderGUI.FindProperty("_N_F_EAL", properties);

            _DirectionalLightIntensity = ShaderGUI.FindProperty("_DirectionalLightIntensity", properties);
            _PointSpotlightIntensity = ShaderGUI.FindProperty("_PointSpotlightIntensity", properties);
            _LightFalloffSoftness = ShaderGUI.FindProperty("_LightFalloffSoftness", properties);

            _ReduSha = ShaderGUI.FindProperty("_ReduSha", properties);
            _ShadowHardness = ShaderGUI.FindProperty("_ShadowHardness", properties);

            _CustomLightDirectionIntensity = ShaderGUI.FindProperty("_CustomLightDirectionIntensity", properties);
            _CustomLightDirectionFollowObjectRotation = ShaderGUI.FindProperty("_CustomLightDirectionFollowObjectRotation", properties);
            _CustomLightDirection = ShaderGUI.FindProperty("_CustomLightDirection", properties);

            _ReflectionIntensity = ShaderGUI.FindProperty("_ReflectionIntensity", properties);
            _ReflectionRoughtness = ShaderGUI.FindProperty("_ReflectionRoughtness", properties);
            _RefMetallic = ShaderGUI.FindProperty("_RefMetallic", properties);
            _MaskReflection = ShaderGUI.FindProperty("_MaskReflection", properties);
            _FReflection = ShaderGUI.FindProperty("_FReflection", properties);

            _RimLigInt = ShaderGUI.FindProperty("_RimLigInt", properties);
            _RimLightUnfill = ShaderGUI.FindProperty("_RimLightUnfill", properties);
            _RimLightColor = ShaderGUI.FindProperty("_RimLightColor", properties);
            _RimLightColorPower = ShaderGUI.FindProperty("_RimLightColorPower", properties);
            _RimLightSoftness = ShaderGUI.FindProperty("_RimLightSoftness", properties);
            _RimLightInLight = ShaderGUI.FindProperty("_RimLightInLight", properties);
            _LightAffectRimLightColor = ShaderGUI.FindProperty("_LightAffectRimLightColor", properties);

            _MinFadDistance = ShaderGUI.FindProperty("_MinFadDistance", properties);
            _MaxFadDistance = ShaderGUI.FindProperty("_MaxFadDistance", properties);

            _TriPlaTile = ShaderGUI.FindProperty("_TriPlaTile", properties);
            _TriPlaBlend = ShaderGUI.FindProperty("_TriPlaBlend", properties);

            _PresAdju = ShaderGUI.FindProperty("_PresAdju", properties);
            _ClipAdju = ShaderGUI.FindProperty("_ClipAdju", properties);
            _PASize = ShaderGUI.FindProperty("_PASize", properties);
            _PASmooTrans = ShaderGUI.FindProperty("_PASmooTrans", properties);
            _PADist = ShaderGUI.FindProperty("_PADist", properties);

            //_NoiseSize = ShaderGUI.FindProperty("_NoiseSize", properties);
            //_TrailSize = ShaderGUI.FindProperty("_TrailSize", properties);

            //if (shader_name == "tessellation_d" || shader_name == "tessellation_ft" || shader_name == "tessellation_ref")
            //{
            //    _TessellationSmoothness = ShaderGUI.FindProperty("_TessellationSmoothness", properties);
            //    _TessellationTransition = ShaderGUI.FindProperty("_TessellationTransition", properties);
            //    _TessellationNear = ShaderGUI.FindProperty("_TessellationNear", properties);
            //    _TessellationFar = ShaderGUI.FindProperty("_TessellationFar", properties);
            //}
            //else if (shader_name == "default_d" || shader_name == "default_ft" || shader_name == "default_ref")
            //{

            //    _TessellationSmoothness = null;
            //    _TessellationTransition = null;
            //    _TessellationNear = null;
            //    _TessellationFar = null;

            //}

            _RefVal = ShaderGUI.FindProperty("_RefVal", properties);
            _Oper = ShaderGUI.FindProperty("_Oper", properties);
            _Compa = ShaderGUI.FindProperty("_Compa", properties);

            _N_F_MC = ShaderGUI.FindProperty("_N_F_MC", properties);
            _N_F_NM = ShaderGUI.FindProperty("_N_F_NM", properties);
            _N_F_CO = ShaderGUI.FindProperty("_N_F_CO", properties);
            _N_F_O = ShaderGUI.FindProperty("_N_F_O", properties);
            _N_F_CA = ShaderGUI.FindProperty("_N_F_CA", properties);
            _N_F_SL = ShaderGUI.FindProperty("_N_F_SL", properties);
            _N_F_GLO = ShaderGUI.FindProperty("_N_F_GLO", properties);
            _N_F_GLOT = ShaderGUI.FindProperty("_N_F_GLOT", properties);
            _N_F_SS = ShaderGUI.FindProperty("_N_F_SS", properties);
            _N_F_SON = ShaderGUI.FindProperty("_N_F_SON", properties);
            _N_F_SCT = ShaderGUI.FindProperty("_N_F_SCT", properties);
            _N_F_ST = ShaderGUI.FindProperty("_N_F_ST", properties);
            _N_F_PT = ShaderGUI.FindProperty("_N_F_PT", properties);
            _N_F_CLD = ShaderGUI.FindProperty("_N_F_CLD", properties);
            _N_F_R = ShaderGUI.FindProperty("_N_F_R", properties);
            _N_F_FR = ShaderGUI.FindProperty("_N_F_FR", properties);
            _N_F_RL = ShaderGUI.FindProperty("_N_F_RL", properties);
            _N_F_NFD = ShaderGUI.FindProperty("_N_F_NFD", properties);
            _N_F_TP = ShaderGUI.FindProperty("_N_F_TP", properties);
            _N_F_PA = ShaderGUI.FindProperty("_N_F_PA", properties);
            _N_F_SE = ShaderGUI.FindProperty("_N_F_SE", properties);

            _N_F_HDLS = ShaderGUI.FindProperty("_N_F_HDLS", properties);
            _N_F_HPSS = ShaderGUI.FindProperty("_N_F_HPSS", properties);

            _N_F_DCS = ShaderGUI.FindProperty("_N_F_DCS", properties);

            _N_F_HDLS = ShaderGUI.FindProperty("_N_F_HDLS", properties);
            _N_F_HPSS = ShaderGUI.FindProperty("_N_F_HPSS", properties);
            _N_F_DCS = ShaderGUI.FindProperty("_N_F_DCS", properties);
            _ZWrite = ShaderGUI.FindProperty("_ZWrite", properties);

            _N_F_NLASOBF = ShaderGUI.FindProperty("_N_F_NLASOBF", properties);

            _N_F_OFLMB = ShaderGUI.FindProperty("_N_F_OFLMB", properties);

            _N_F_ESSAO = ShaderGUI.FindProperty("_N_F_ESSAO", properties);
            _SSAOColor = ShaderGUI.FindProperty("_SSAOColor", properties);

            _N_F_RDC = ShaderGUI.FindProperty("_N_F_RDC", properties);

            _N_F_DDMD = ShaderGUI.FindProperty("_N_F_DDMD", properties);

            #endregion


            //UI

            #region UI

            //Header
            Rect r_header = EditorGUILayout.BeginVertical("HelpBox");
           
            Rect rect = new Rect(36, 11, t.width, t.height);
            GUI.DrawTexture(rect, t, ScaleMode.ScaleToFit);
            GUILayout.Space(32);

            EditorGUILayout.LabelField("(" + realtoon_version + " - " + srp_mode + " - " + shader_type + ")", EditorStyles.boldLabel);

            EditorGUILayout.EndVertical();


            if (ShowUI == true)
            {

                GUILayout.Space(20);

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                //Light Blend

                #region Light Blend

                Rect r_lightblend = EditorGUILayout.BeginVertical("HelpBox");
                EditorGUILayout.LabelField("Light Blend Style: " + LightBlendString);
                EditorGUILayout.EndVertical();

                switch ((int)_UseTLB.floatValue)
                {
                    case 0:
                        LightBlendString = "Anime/Cartoon";
                        break;
                    case 1:
                        LightBlendString = "Traditional";
                        break;
                    default:
                        break;
                }

                #endregion

                //Double Sided

                #region Culling

                Rect r_culling = EditorGUILayout.BeginVertical("HelpBox");
                materialEditor.ShaderProperty(_Culling, new GUIContent(_Culling.displayName, TOTIPS[0]));
                EditorGUILayout.EndVertical();

                #endregion

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                GUILayout.Space(20);

                //Render Queue

                #region Transparent Mode

                Rect r_renderqueue = EditorGUILayout.BeginVertical("HelpBox");

                EditorGUI.BeginChangeCheck();

                materialEditor.ShaderProperty(_TRANSMODE, new GUIContent(_TRANSMODE.displayName, TOTIPS[11]));

                if (EditorGUI.EndChangeCheck())
                {
                    foreach (Material m in materialEditor.targets)
                    {
                        switch (_TRANSMODE.floatValue)
                        {
                            case 0:

                                m.renderQueue = -1;
                                m.SetOverrideTag("RenderType", "Opaque");
                                m.SetInt("_BleModSour", 1);
                                m.SetInt("_BleModDest", 0);
                                shader_type = "Default";
                                break;

                            case 1:

                                m.SetInt("_BleModSour", 5);
                                m.SetInt("_BleModDest", 10);

                                if (m.IsKeywordEnabled("N_F_CO_ON") || m.GetFloat("_N_F_CO") == 1.0f)
                                {
                                    m.renderQueue = 2450;
                                    m.SetOverrideTag("RenderType", "TransparentCutout");
                                }
                                else
                                {
                                    m.renderQueue = 3000;
                                    m.SetOverrideTag("RenderType", "Transparent");
                                }

                                shader_type = "Fade Transperancy";
                                break;

                            default:
                                break;
                        }

                    }

                    materialEditor.PropertiesChanged();

                }

                EditorGUILayout.EndVertical();

                #endregion

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                GUILayout.Space(20);

                //Texture - Color

                #region Texture - Color

                Rect r_texturecolor = EditorGUILayout.BeginVertical("Button");

                ShowTextureColor = EditorGUILayout.Foldout(ShowTextureColor, "(Texture - Color)", true, EditorStyles.foldout);

                if (ShowTextureColor)
                {

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_MainTex, new GUIContent(_MainTex.displayName, TOTIPS[1]));

                    EditorGUI.BeginDisabledGroup(_MainTex.textureValue == null);
                    materialEditor.ShaderProperty(_TexturePatternStyle, new GUIContent(_TexturePatternStyle.displayName, TOTIPS[2]));
                    EditorGUI.EndDisabledGroup();

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    materialEditor.ShaderProperty(_MainColor, new GUIContent(_MainColor.displayName, TOTIPS[3]));
                    materialEditor.ShaderProperty(_MaiColPo, new GUIContent(_MaiColPo.displayName, TOTIPS[8]));

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    materialEditor.ShaderProperty(_MVCOL, new GUIContent(_MVCOL.displayName, TOTIPS[4]));

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    materialEditor.ShaderProperty(_MCIALO, new GUIContent(_MCIALO.displayName, TOTIPS[5]));

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    materialEditor.ShaderProperty(_HighlightColor, new GUIContent(_HighlightColor.displayName, TOTIPS[6]));
                    materialEditor.ShaderProperty(_HighlightColorPower, new GUIContent(_HighlightColorPower.displayName, TOTIPS[7]));

                    GUILayout.Space(10);

                }

                EditorGUILayout.EndVertical();

                #endregion

                //MatCap

                #region MatCap

                if (_N_F_MC.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_matcap = EditorGUILayout.BeginVertical("Button");
                    ShowMatCap = EditorGUILayout.Foldout(ShowMatCap, "(MatCap)", true, EditorStyles.foldout);

                    if (ShowMatCap)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_MCapIntensity, new GUIContent(_MCapIntensity.displayName, TOTIPS[13]));
                        materialEditor.ShaderProperty(_MCap, _MCap.displayName);

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_SPECMODE, new GUIContent(_SPECMODE.displayName, TOTIPS[14]));
                        EditorGUI.BeginDisabledGroup(_SPECMODE.floatValue == 0);
                        materialEditor.ShaderProperty(_SPECIN, new GUIContent(_SPECIN.displayName, TOTIPS[15]));
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_MCapMask, new GUIContent(_MCapMask.displayName, TOTIPS[16]));

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();
                }

                #endregion

                //Cutout

                #region Cutout

                if (_TRANSMODE.floatValue == 1)
                {
                    if (_N_F_CO.floatValue == 1)
                    {
                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        EditorGUI.BeginDisabledGroup(_N_F_CO.floatValue == 0);

                        Rect r_cutout = EditorGUILayout.BeginVertical("Button");
                        ShowCutout = EditorGUILayout.Foldout(ShowCutout, "(Cutout)", true, EditorStyles.foldout);

                        if (ShowCutout)
                        {

                            GUILayout.Space(10);

                            materialEditor.ShaderProperty(_Cutout, new GUIContent(_Cutout.displayName, TOTIPS[17]));
                            materialEditor.ShaderProperty(_AlphaBaseCutout, new GUIContent(_AlphaBaseCutout.displayName, TOTIPS[18]));
                            materialEditor.ShaderProperty(_N_F_SCO, new GUIContent(_N_F_SCO.displayName, TOTIPS[154]));

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            materialEditor.ShaderProperty(_UseSecondaryCutout, new GUIContent(_UseSecondaryCutout.displayName, TOTIPS[19]));
                            materialEditor.ShaderProperty(_SecondaryCutout, new GUIContent(_SecondaryCutout.displayName, TOTIPS[20]));

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            materialEditor.ShaderProperty(_N_F_COEDGL, _N_F_COEDGL.displayName);
                            EditorGUI.BeginDisabledGroup(_N_F_COEDGL.floatValue == 0.0f);
                            materialEditor.ShaderProperty(_Glow_Color, new GUIContent(_Glow_Color.displayName, TOTIPS[148]));
                            materialEditor.ShaderProperty(_Glow_Edge_Width, new GUIContent(_Glow_Edge_Width.displayName, TOTIPS[149]));
                            EditorGUI.EndDisabledGroup();

                            GUILayout.Space(10);

                        }

                        EditorGUILayout.EndVertical();

                        EditorGUI.EndDisabledGroup();
                    }
                }

                #endregion

                //Transperancy

                #region Transperancy

                if (_TRANSMODE.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    EditorGUI.BeginDisabledGroup(_N_F_CO.floatValue == 1);

                    Rect r_transparency = EditorGUILayout.BeginVertical("Button");
                    ShowTransparency = EditorGUILayout.Foldout(ShowTransparency, "(Transparency)", true, EditorStyles.foldout);

                    if (ShowTransparency)
                    {

                        GUILayout.Space(10);
                        materialEditor.ShaderProperty(_SimTrans, new GUIContent(_SimTrans.displayName, TOTIPS[150]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_Opacity, new GUIContent(_Opacity.displayName, TOTIPS[21]));

                        EditorGUI.BeginDisabledGroup(_SimTrans.floatValue == 1);
                        materialEditor.ShaderProperty(_TransparentThreshold, new GUIContent(_TransparentThreshold.displayName, TOTIPS[22]));
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_BleModSour, new GUIContent(_BleModSour.displayName, TOTIPS[9]));
                        materialEditor.ShaderProperty(_BleModDest, new GUIContent(_BleModDest.displayName, TOTIPS[10]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_TransAffSha, new GUIContent(_TransAffSha.displayName, TOTIPS[74]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        EditorGUI.BeginDisabledGroup(_SimTrans.floatValue == 1);
                        materialEditor.ShaderProperty(_MaskTransparency, new GUIContent(_MaskTransparency.displayName, TOTIPS[23]));
                        EditorGUI.EndDisabledGroup();

                        GUILayout.Space(10);

                    }

                    EditorGUI.EndDisabledGroup();

                    EditorGUILayout.EndVertical();
                }

                #endregion

                //Normal Map

                #region Normal Map

                if (_N_F_NM.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_normalmap = EditorGUILayout.BeginVertical("Button");
                    ShowNormalMap = EditorGUILayout.Foldout(ShowNormalMap, "(Normal Map)", true, EditorStyles.foldout);

                    if (ShowNormalMap)
                    {
                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_NormalMap, new GUIContent(_NormalMap.displayName, TOTIPS[24]));

                        EditorGUI.BeginDisabledGroup(_NormalMap.textureValue == null);
                        materialEditor.ShaderProperty(_NormalMapIntensity, new GUIContent(_NormalMapIntensity.displayName, TOTIPS[25]));
                        EditorGUI.EndDisabledGroup();

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                }
                #endregion

                //Color Adjustment

                #region Color Adjustment

                if (_N_F_CA.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_cadjustment = EditorGUILayout.BeginVertical("Button");
                    ShowColorAdjustment = EditorGUILayout.Foldout(ShowColorAdjustment, "Color Adjustment", true, EditorStyles.foldout);

                    if (ShowColorAdjustment)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_Saturation, new GUIContent(_Saturation.displayName, TOTIPS[26]));

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical(); ;

                }

                #endregion

                //Outline

                #region Outline

                if (remoout == true)
                {

                    if (_N_F_O.floatValue == 1)
                    {

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        Rect r_outline = EditorGUILayout.BeginVertical("Button");
                        ShowOutline = EditorGUILayout.Foldout(ShowOutline, "(Outline - " + OLType + ")", true, EditorStyles.foldout);


                        if (ShowOutline)
                        {

                            GUILayout.Space(10);

                            EditorGUI.BeginDisabledGroup(_TRANSMODE.floatValue == 1 && _N_F_CO.floatValue == 0 && UseSSOL == false);
                            materialEditor.ShaderProperty(_OutlineWidth, new GUIContent(_OutlineWidth.displayName, TOTIPS[8]));
                            EditorGUI.EndDisabledGroup();

                            if (UseSSOL == true)
                            {

                                materialEditor.ShaderProperty(_OutlineWidthControl, new GUIContent(_OutlineWidthControl.displayName, TOTIPS[28]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                materialEditor.ShaderProperty(_OutlineExtrudeMethod, new GUIContent(_OutlineExtrudeMethod.displayName, TOTIPS[29]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_OutlineOffset, new GUIContent(_OutlineOffset.displayName, TOTIPS[30]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_OutlineZPostionInCamera, new GUIContent(_OutlineZPostionInCamera.displayName, TOTIPS[123]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_DoubleSidedOutline, new GUIContent(_DoubleSidedOutline.displayName, TOTIPS[31]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_OutlineColor, new GUIContent(_OutlineColor.displayName, TOTIPS[32]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_MixMainTexToOutline, new GUIContent(_MixMainTexToOutline.displayName, TOTIPS[33]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_NoisyOutlineIntensity, new GUIContent(_NoisyOutlineIntensity.displayName, TOTIPS[34]));
                                materialEditor.ShaderProperty(_DynamicNoisyOutline, new GUIContent(_DynamicNoisyOutline.displayName, TOTIPS[35]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_LightAffectOutlineColor, new GUIContent(_LightAffectOutlineColor.displayName, TOTIPS[36]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_OutlineWidthAffectedByViewDistance, new GUIContent(_OutlineWidthAffectedByViewDistance.displayName, TOTIPS[37]));
                                EditorGUI.BeginDisabledGroup(_OutlineWidthAffectedByViewDistance.floatValue == 0);
                                materialEditor.ShaderProperty(_FarDistanceMaxWidth, new GUIContent(_FarDistanceMaxWidth.displayName, TOTIPS[38]));
                                EditorGUI.EndDisabledGroup();

                                GUILayout.Space(10);

                                materialEditor.ShaderProperty(_VertexColorBlueAffectOutlineWitdh, new GUIContent(_VertexColorBlueAffectOutlineWitdh.displayName, TOTIPS[39]));

                            }
                            else
                            {
                                EditorGUI.BeginDisabledGroup(_TRANSMODE.floatValue == 1 && _N_F_CO.floatValue == 0);
                                materialEditor.ShaderProperty(_OutlineColor, new GUIContent(_OutlineColor.displayName, TOTIPS[28]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                materialEditor.ShaderProperty(_N_F_MSSOLTFO, new GUIContent(_N_F_MSSOLTFO.displayName, TOTIPS[140]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                materialEditor.ShaderProperty(_DepthThreshold, new GUIContent(_DepthThreshold.displayName, TOTIPS[122]));
                                EditorGUI.EndDisabledGroup();

                            }

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            if (GUILayout.Button(new GUIContent(UseSSOLStat, TOTIPS[142]), "Button"))
                            {
                                USSOL_OR_TOL();
                            }

                            GUILayout.Space(10);

                        }

                        EditorGUILayout.EndVertical();

                    }

                }

                #endregion

                //Self Lit

                #region SelfLit

                if (_N_F_SL.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_selflit = EditorGUILayout.BeginVertical("Button");
                    ShowSelfLit = EditorGUILayout.Foldout(ShowSelfLit, "(Self Lit)", true, EditorStyles.foldout);

                    if (ShowSelfLit)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_SelfLitIntensity, new GUIContent(_SelfLitIntensity.displayName, TOTIPS[40]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_SelfLitColor, new GUIContent(_SelfLitColor.displayName, TOTIPS[41]));
                        materialEditor.ShaderProperty(_SelfLitPower, new GUIContent(_SelfLitPower.displayName, TOTIPS[42]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_TEXMCOLINT, new GUIContent(_TEXMCOLINT.displayName, TOTIPS[43]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_SelfLitHighContrast, new GUIContent(_SelfLitHighContrast.displayName, TOTIPS[44]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_MaskSelfLit, new GUIContent(_MaskSelfLit.displayName, TOTIPS[45]));


                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                }
                #endregion

                //Gloss

                #region Gloss

                if (_N_F_OFLMB.floatValue == 0)
                {

                    if (_N_F_GLO.floatValue == 1)
                    {
                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        Rect r_gloss = EditorGUILayout.BeginVertical("Button");
                        ShowGloss = EditorGUILayout.Foldout(ShowGloss, "(Gloss)", true, EditorStyles.foldout);

                        if (ShowGloss)
                        {

                            GUILayout.Space(10);

                            materialEditor.ShaderProperty(_GlossIntensity, new GUIContent(_GlossIntensity.displayName, TOTIPS[46]));
                            EditorGUI.BeginDisabledGroup(_N_F_GLOT.floatValue == 1);
                            materialEditor.ShaderProperty(_Glossiness, new GUIContent(_Glossiness.displayName, TOTIPS[47]));
                            materialEditor.ShaderProperty(_GlossSoftness, new GUIContent(_GlossSoftness.displayName, TOTIPS[48]));
                            EditorGUI.EndDisabledGroup();

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            materialEditor.ShaderProperty(_GlossColor, new GUIContent(_GlossColor.displayName, TOTIPS[49]));
                            materialEditor.ShaderProperty(_GlossColorPower, new GUIContent(_GlossColorPower.displayName, TOTIPS[50]));

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); ;

                            materialEditor.ShaderProperty(_MaskGloss, new GUIContent(_MaskGloss.displayName, TOTIPS[51]));

                            GUILayout.Space(10);

                            //Gloss Texture

                            #region Gloss Texture

                            if (_N_F_GLOT.floatValue == 1)
                            {

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                Rect r_glosstexture = EditorGUILayout.BeginVertical("Button");
                                GUILayout.Label("Gloss Texture", EditorStyles.boldLabel);
                                EditorGUILayout.EndVertical();

                                if (_N_F_GLOT.floatValue == 1)
                                {

                                    GUILayout.Space(10);

                                    materialEditor.ShaderProperty(_GlossTexture, new GUIContent(_GlossTexture.displayName, TOTIPS[52]));

                                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                    EditorGUI.BeginDisabledGroup(_GlossTexture.textureValue == null);
                                    materialEditor.ShaderProperty(_GlossTextureSoftness, new GUIContent(_GlossTextureSoftness.displayName, TOTIPS[53]));

                                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                    materialEditor.ShaderProperty(_PSGLOTEX, new GUIContent(_PSGLOTEX.displayName, TOTIPS[54]));

                                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                    EditorGUI.BeginDisabledGroup(_PSGLOTEX.floatValue == 1);
                                    materialEditor.ShaderProperty(_GlossTextureRotate, new GUIContent(_GlossTextureRotate.displayName, TOTIPS[55]));
                                    materialEditor.ShaderProperty(_GlossTextureFollowObjectRotation, new GUIContent(_GlossTextureFollowObjectRotation.displayName, TOTIPS[56]));
                                    materialEditor.ShaderProperty(_GlossTextureFollowLight, new GUIContent(_GlossTextureFollowLight.displayName, TOTIPS[57]));
                                    EditorGUI.EndDisabledGroup();

                                    EditorGUI.EndDisabledGroup();

                                }

                                GUILayout.Space(10);

                            }
                            #endregion

                        }

                        EditorGUILayout.EndVertical();

                    }

                }

                #endregion

                //Shadow

                #region Shadow

                if (_N_F_OFLMB.floatValue == 0)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_shadow = EditorGUILayout.BeginVertical("Button");
                    ShowShadow = EditorGUILayout.Foldout(ShowShadow, "(Shadow)", true, EditorStyles.foldout);

                    if (ShowShadow)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_OverallShadowColor, new GUIContent(_OverallShadowColor.displayName, TOTIPS[58]));
                        materialEditor.ShaderProperty(_OverallShadowColorPower, new GUIContent(_OverallShadowColorPower.displayName, TOTIPS[59]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_SelfShadowShadowTAtViewDirection, new GUIContent(_SelfShadowShadowTAtViewDirection.displayName, TOTIPS[60]));
                        materialEditor.ShaderProperty(_LigIgnoYNorDir, new GUIContent(_LigIgnoYNorDir.displayName, TOTIPS[144]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_ReduSha, new GUIContent(_ReduSha.displayName, TOTIPS[63]));

                        if (_N_F_HDLS.floatValue == 0 || _N_F_HPSS.floatValue == 0)
                        {
                            GUILayout.Space(10);
                            materialEditor.ShaderProperty(_ShadowHardness, new GUIContent(_ShadowHardness.displayName, TOTIPS[64]));
                        }

                        switch ((int)_N_F_SS.floatValue)
                        {
                            case 0:
                                materialEditor.ShaderProperty(_SelfShadowRealtimeShadowIntensity, new GUIContent("Realtime Shadow Intensity", TOTIPS[124]));
                                break;
                            case 1:
                                materialEditor.ShaderProperty(_SelfShadowRealtimeShadowIntensity, new GUIContent(_SelfShadowRealtimeShadowIntensity.displayName, TOTIPS[126]));
                                break;
                            default:
                                break;
                        }

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_N_F_ESSAO, new GUIContent(_N_F_ESSAO.displayName, TOTIPS[145]));
                        EditorGUI.BeginDisabledGroup(_N_F_ESSAO.floatValue == 0.0f);
                        materialEditor.ShaderProperty(_SSAOColor, new GUIContent(_SSAOColor.displayName, TOTIPS[146]));
                        EditorGUI.EndDisabledGroup();


                        //Self Shadow

                        #region Self Shadow

                        if (_N_F_SS.floatValue == 1)
                        {

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            Rect r_selfshadow = EditorGUILayout.BeginVertical("Button");
                            GUILayout.Label("Self Shadow", EditorStyles.boldLabel);
                            EditorGUILayout.EndVertical();

                            if (_N_F_SS.floatValue == 1)
                            {

                                GUILayout.Space(10);

                                materialEditor.ShaderProperty(_SelfShadowThreshold, new GUIContent(_SelfShadowThreshold.displayName, TOTIPS[65]));

                                materialEditor.ShaderProperty(_VertexColorGreenControlSelfShadowThreshold, new GUIContent(_VertexColorGreenControlSelfShadowThreshold.displayName, TOTIPS[66]));

                                materialEditor.ShaderProperty(_SelfShadowHardness, new GUIContent(_SelfShadowHardness.displayName, TOTIPS[67]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                materialEditor.ShaderProperty(_SelfShadowRealTimeShadowColor, new GUIContent(_SelfShadowRealTimeShadowColor.displayName, TOTIPS[68]));
                                materialEditor.ShaderProperty(_SelfShadowRealTimeShadowColorPower, new GUIContent(_SelfShadowRealTimeShadowColorPower.displayName, TOTIPS[69]));

                                GUILayout.Space(10);
                                materialEditor.ShaderProperty(_SelfShadowAffectedByLightShadowStrength, new GUIContent(_SelfShadowAffectedByLightShadowStrength.displayName, TOTIPS[70]));

                            }

                            GUILayout.Space(10);

                        }
                        #endregion

                        //Smooth Object Normal

                        #region Smooth Object Normal

                        if (_N_F_SON.floatValue == 1)
                        {

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            Rect r_smoothobjectnormal = EditorGUILayout.BeginVertical("Button");
                            GUILayout.Label("Smooth Object Normal", EditorStyles.boldLabel);
                            EditorGUILayout.EndVertical();

                            if (_N_F_SON.floatValue == 1)
                            {

                                GUILayout.Space(10);

                                materialEditor.ShaderProperty(_SmoothObjectNormal, new GUIContent(_SmoothObjectNormal.displayName, TOTIPS[71]));

                                materialEditor.ShaderProperty(_VertexColorRedControlSmoothObjectNormal, new GUIContent(_VertexColorRedControlSmoothObjectNormal.displayName, TOTIPS[72]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                //materialEditor.ShaderProperty(_XYZPosition, new GUIContent(_XYZPosition.displayName, TOTIPS[73]));

                                materialEditor.ShaderProperty(_ShowNormal, new GUIContent(_ShowNormal.displayName, TOTIPS[75]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                EditorGUI.BeginDisabledGroup(true);
                                EditorGUILayout.TextArea("Add the 'Smooth Object Normal - Helper' component to your object for this to work.\n\nTo Add:\nClick your object then click 'Add Component'\nthen 'RealToon>Tool>Smooth Object Normal - Helper.", EditorStyles.label, GUILayout.ExpandWidth(true));
                                EditorGUI.EndDisabledGroup();

                            }

                            GUILayout.Space(10);

                        }

                        #endregion

                        //Shadow Color Texture

                        #region Shadow Color Texture

                        if (_N_F_SCT.floatValue == 1)
                        {
                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            Rect r_shadowcolortexture = EditorGUILayout.BeginVertical("Button");
                            GUILayout.Label("Shadow Color Texture", EditorStyles.boldLabel);
                            EditorGUILayout.EndVertical();

                            if (_N_F_SCT.floatValue == 1)
                            {

                                GUILayout.Space(10);

                                materialEditor.ShaderProperty(_ShadowColorTexture, new GUIContent(_ShadowColorTexture.displayName, TOTIPS[76]));
                                materialEditor.ShaderProperty(_ShadowColorTexturePower, new GUIContent(_ShadowColorTexturePower.displayName, TOTIPS[77]));
                            }

                            GUILayout.Space(10);

                        }

                        #endregion

                        //ShadowT

                        #region ShadowT

                        if (_N_F_ST.floatValue == 1)
                        {
                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            Rect r_shadowt = EditorGUILayout.BeginVertical("Button");
                            GUILayout.Label("ShadowT", EditorStyles.boldLabel);
                            EditorGUILayout.EndVertical();

                            if (_N_F_ST.floatValue == 1)
                            {
                                GUILayout.Space(10);

                                materialEditor.ShaderProperty(_ShadowTIntensity, new GUIContent(_ShadowTIntensity.displayName, TOTIPS[78]));
                                materialEditor.ShaderProperty(_ShadowT, new GUIContent(_ShadowT.displayName, TOTIPS[79]));
                                materialEditor.ShaderProperty(_ShadowTLightThreshold, new GUIContent(_ShadowTLightThreshold.displayName, TOTIPS[80]));

                                if (_N_F_STSDFM.floatValue == 0)
                                {
                                    materialEditor.ShaderProperty(_ShadowTShadowThreshold, new GUIContent(_ShadowTShadowThreshold.displayName, TOTIPS[81]));
                                }

                                materialEditor.ShaderProperty(_ShadowTHardness, new GUIContent(_ShadowTHardness.displayName, TOTIPS[82]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_ShadowTColor, new GUIContent(_ShadowTColor.displayName, TOTIPS[129]));
                                materialEditor.ShaderProperty(_ShadowTColorPower, new GUIContent(_ShadowTColorPower.displayName, TOTIPS[130]));

                                if (_N_F_STSDFM.floatValue == 0)
                                {
                                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                    materialEditor.ShaderProperty(_STIL, new GUIContent(_STIL.displayName, TOTIPS[131]));

                                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                    materialEditor.ShaderProperty(_N_F_STIS, new GUIContent(_N_F_STIS.displayName, TOTIPS[83]));
                                    materialEditor.ShaderProperty(_N_F_STIAL, new GUIContent(_N_F_STIAL.displayName, TOTIPS[84]));

                                    EditorGUI.BeginDisabledGroup(_N_F_STIAL.floatValue == 0 && _N_F_STIS.floatValue == 0);
                                    materialEditor.ShaderProperty(_ShowInAmbientLightShadowIntensity, new GUIContent(_ShowInAmbientLightShadowIntensity.displayName, TOTIPS[85]));
                                    EditorGUI.EndDisabledGroup();

                                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                    materialEditor.ShaderProperty(_ShowInAmbientLightShadowThreshold, new GUIContent(_ShowInAmbientLightShadowThreshold.displayName, TOTIPS[86]));

                                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                    materialEditor.ShaderProperty(_LightFalloffAffectShadowT, new GUIContent(_LightFalloffAffectShadowT.displayName, TOTIPS[87]));
                                }

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                                materialEditor.ShaderProperty(_N_F_STSDFM, new GUIContent(_N_F_STSDFM.displayName, TOTIPS[162]));

                                if (_N_F_STSDFM.floatValue == 1)
                                {
                                    GUILayout.Space(10);

                                    EditorGUI.BeginDisabledGroup(true);
                                    EditorGUILayout.TextArea("Add the 'ShadowT SDF Mode - Helper' component to your object for this to work.\n\nTo Add:\nClick your object then click 'Add Component'\nthen 'RealToon>Tool>ShadowT SDF Mode - Helper.", EditorStyles.label, GUILayout.ExpandWidth(true));
                                    EditorGUI.EndDisabledGroup();
                                }

                            }

                            GUILayout.Space(10);

                        }

                        #endregion

                        //Shadow PTexture

                        #region PTexture

                        if (_N_F_PT.floatValue == 1)
                        {
                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            Rect r_ptexture = EditorGUILayout.BeginVertical("Button");
                            GUILayout.Label("PTexture", EditorStyles.boldLabel);
                            EditorGUILayout.EndVertical();

                            GUILayout.Space(10);

                            if (_N_F_PT.floatValue == 1)
                            {
                                materialEditor.ShaderProperty(_PTexture, new GUIContent(_PTexture.displayName, TOTIPS[88]));
                                materialEditor.ShaderProperty(_PTexturePower, new GUIContent(_PTexturePower.displayName, TOTIPS[89]));

                                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                                materialEditor.ShaderProperty(_PTCol, new GUIContent(_PTCol.displayName, TOTIPS[122]));
                            }

                            GUILayout.Space(10);

                        }

                        #endregion

                    }

                    EditorGUILayout.EndVertical();

                }

                #endregion

                //Lighting

                #region Lighting

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                Rect r_lighting = EditorGUILayout.BeginVertical("Button");
                ShowLighting = EditorGUILayout.Foldout(ShowLighting, "(Lighting)", true, EditorStyles.foldout);

                if (ShowLighting)
                {

                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_RELG, new GUIContent(_RELG.displayName, TOTIPS[90]));
                    EditorGUI.BeginDisabledGroup(_RELG.floatValue == 0);
                    materialEditor.ShaderProperty(_EnvironmentalLightingIntensity, new GUIContent(_EnvironmentalLightingIntensity.displayName, TOTIPS[91]));

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    materialEditor.ShaderProperty(_GIFlatShade, new GUIContent(_GIFlatShade.displayName, TOTIPS[92]));
                    materialEditor.ShaderProperty(_GIShadeThreshold, new GUIContent(_GIShadeThreshold.displayName, TOTIPS[93]));
                    EditorGUI.EndDisabledGroup();

                    if (_N_F_OFLMB.floatValue == 0)
                    {

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_LightAffectShadow, new GUIContent(_LightAffectShadow.displayName, TOTIPS[94]));
                        EditorGUI.BeginDisabledGroup(_LightAffectShadow.floatValue == 0);
                        materialEditor.ShaderProperty(_LightIntensity, new GUIContent(_LightIntensity.displayName, TOTIPS[132]));
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                        materialEditor.ShaderProperty(_UseTLB, new GUIContent(_UseTLB.displayName, TOTIPS[134]));
                        materialEditor.ShaderProperty(_N_F_EAL, new GUIContent(_N_F_EAL.displayName, TOTIPS[133]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                        materialEditor.ShaderProperty(_DirectionalLightIntensity, new GUIContent(_DirectionalLightIntensity.displayName, TOTIPS[95]));
                        EditorGUI.BeginDisabledGroup(_N_F_EAL.floatValue == 0);
                        materialEditor.ShaderProperty(_PointSpotlightIntensity, new GUIContent(_PointSpotlightIntensity.displayName, TOTIPS[96]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                        materialEditor.ShaderProperty(_LightFalloffSoftness, new GUIContent(_LightFalloffSoftness.displayName, TOTIPS[97]));
                        EditorGUI.EndDisabledGroup();

                        GUILayout.Space(10);

                        //Custom Light Direction

                        #region Custom Light Direction

                        if (_N_F_CLD.floatValue == 1)
                        {

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            EditorGUI.BeginDisabledGroup(_N_F_CLD.floatValue == 0);

                            Rect r_customlightdirection = EditorGUILayout.BeginVertical("Button");
                            GUILayout.Label("Custom Light Direction", EditorStyles.boldLabel);
                            EditorGUILayout.EndVertical();

                            if (_N_F_CLD.floatValue == 1)
                            {

                                GUILayout.Space(10);

                                materialEditor.ShaderProperty(_CustomLightDirectionIntensity, new GUIContent(_CustomLightDirectionIntensity.displayName, TOTIPS[98]));
                                materialEditor.ShaderProperty(_CustomLightDirection, new GUIContent(_CustomLightDirection.displayName, TOTIPS[99]));
                                materialEditor.ShaderProperty(_CustomLightDirectionFollowObjectRotation, new GUIContent(_CustomLightDirectionFollowObjectRotation.displayName, TOTIPS[100]));

                            }

                            EditorGUI.EndDisabledGroup();

                            GUILayout.Space(10);

                        }

                        #endregion

                    }

                    GUILayout.Space(10);
                }

                EditorGUILayout.EndVertical();

                #endregion

                //Reflection

                #region Reflection

                if (_N_F_R.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_reflection = EditorGUILayout.BeginVertical("Button");
                    ShowReflection = EditorGUILayout.Foldout(ShowReflection, "(Reflection)", true, EditorStyles.foldout);

                    if (ShowReflection)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_ReflectionIntensity, new GUIContent(_ReflectionIntensity.displayName, TOTIPS[101]));
                        materialEditor.ShaderProperty(_ReflectionRoughtness, new GUIContent(_ReflectionRoughtness.displayName, TOTIPS[102]));
                        materialEditor.ShaderProperty(_RefMetallic, new GUIContent(_RefMetallic.displayName, TOTIPS[103]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_MaskReflection, new GUIContent(_MaskReflection.displayName, TOTIPS[104]));

                        GUILayout.Space(10);

                        //FReflection

                        #region FReflection

                        if (_N_F_FR.floatValue == 1)
                        {

                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                            EditorGUI.BeginDisabledGroup(_N_F_FR.floatValue == 0);

                            Rect r_freflection = EditorGUILayout.BeginVertical("Button");
                            GUILayout.Label("FReflection", EditorStyles.boldLabel);
                            EditorGUILayout.EndVertical();

                            materialEditor.ShaderProperty(_FReflection, new GUIContent(_FReflection.displayName, TOTIPS[105]));

                            EditorGUI.EndDisabledGroup();

                            GUILayout.Space(10);
                        }

                    }

                    #endregion

                    EditorGUILayout.EndVertical();
                }

                #endregion

                // Rim Light

                #region Rim Light

                if (_N_F_RL.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_rimlight = EditorGUILayout.BeginVertical("Button");
                    ShowRimLight = EditorGUILayout.Foldout(ShowRimLight, "(Rim Light)", true, EditorStyles.foldout);

                    if (ShowRimLight)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_RimLigInt, new GUIContent(_RimLigInt.displayName, TOTIPS[125]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_RimLightUnfill, new GUIContent(_RimLightUnfill.displayName, TOTIPS[106]));
                        materialEditor.ShaderProperty(_RimLightSoftness, new GUIContent(_RimLightSoftness.displayName, TOTIPS[107]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_LightAffectRimLightColor, new GUIContent(_LightAffectRimLightColor.displayName, TOTIPS[108]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_RimLightColor, new GUIContent(_RimLightColor.displayName, TOTIPS[109]));
                        materialEditor.ShaderProperty(_RimLightColorPower, new GUIContent(_RimLightColorPower.displayName, TOTIPS[110]));

                        if (_N_F_OFLMB.floatValue == 0)
                        {
                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                            materialEditor.ShaderProperty(_RimLightInLight, new GUIContent(_RimLightInLight.displayName, TOTIPS[111]));
                        }

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                }

                #endregion

                //Near Fade Dithering

                #region Near Fade Dithering

                if (_N_F_NFD.floatValue == 1)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_nerfaddithe = EditorGUILayout.BeginVertical("Button");
                    NearFadeDithering = EditorGUILayout.Foldout(NearFadeDithering, "(Near Fade Dithering)", true, EditorStyles.foldout);

                    if (NearFadeDithering)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_MinFadDistance, new GUIContent(_MinFadDistance.displayName, TOTIPS[152]));
                        materialEditor.ShaderProperty(_MaxFadDistance, new GUIContent(_MaxFadDistance.displayName, TOTIPS[153]));

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                }

                #endregion

                //Triplanar

                #region Triplanar

                if (_N_F_TP.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_tripla = EditorGUILayout.BeginVertical("Button");
                    Triplanar = EditorGUILayout.Foldout(Triplanar, "(Triplanar)", true, EditorStyles.foldout);

                    if (Triplanar)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_TriPlaTile, new GUIContent(_TriPlaTile.displayName, TOTIPS[155]));
                        materialEditor.ShaderProperty(_TriPlaBlend, new GUIContent(_TriPlaBlend.displayName, TOTIPS[156]));

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                }

                #endregion

                //Perspective Adjustment

                #region Perspective Adjustment

                if (_N_F_PA.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_perspecadju = EditorGUILayout.BeginVertical("Button");
                    ShowPerspecAdju = EditorGUILayout.Foldout(ShowPerspecAdju, "(Perspective Adjustment)", true, EditorStyles.foldout);

                    if (ShowPerspecAdju)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_PresAdju, new GUIContent(_PresAdju.displayName, TOTIPS[157]));
                        materialEditor.ShaderProperty(_ClipAdju, new GUIContent(_ClipAdju.displayName, TOTIPS[158]));

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        materialEditor.ShaderProperty(_PASize, new GUIContent(_PASize.displayName, TOTIPS[159]));
                        materialEditor.ShaderProperty(_PASmooTrans, new GUIContent(_PASmooTrans.displayName, TOTIPS[160]));
                        materialEditor.ShaderProperty(_PADist, new GUIContent(_PADist.displayName, TOTIPS[161]));

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                }

                #endregion

                //Smear Effect

                #region Smear Effect

                if (_N_F_SE.floatValue == 1)
                {

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_perspecadju = EditorGUILayout.BeginVertical("Button");
                    ShowSmeEff = EditorGUILayout.Foldout(ShowSmeEff, "(Smear Effect)", true, EditorStyles.foldout);

                    if (ShowSmeEff)
                    {

                        EditorGUI.BeginDisabledGroup(true);
                            EditorGUILayout.TextArea("Add the 'Smear Effect [Helper]' component to your object for this to work.\nAdjustable options are on the 'Smear Effect [Helper]' component.\n\nTo Add:\nClick your object then click 'Add Component'\nthen 'RealToon>Tool>Smear Effect [Helper].", EditorStyles.label, GUILayout.ExpandWidth(true));
                        EditorGUI.EndDisabledGroup();

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                }

                #endregion

                //Tessellation (In Progress)

                #region Tessellation

//if (shader_name == "tessellation_d" || shader_name == "tessellation_ft" || shader_name == "tessellation_ref")
//{

//    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

//    Rect r_tessellation = EditorGUILayout.BeginVertical("Button");
//    ShowTessellation = EditorGUILayout.Foldout(ShowTessellation, "(Tessellation)", true, EditorStyles.foldout);

//    if (ShowTessellation)
//    {

//        GUILayout.Space(10);

//        materialEditor.ShaderProperty(_TessellationSmoothness, _TessellationSmoothness.displayName);
//        materialEditor.ShaderProperty(_TessellationTransition, _TessellationTransition.displayName);
//        materialEditor.ShaderProperty(_TessellationNear, _TessellationNear.displayName);
//        materialEditor.ShaderProperty(_TessellationFar, _TessellationFar.displayName);

//    }

//    EditorGUILayout.EndVertical();
//}

                #endregion

                //See Through

                #region See Through

                if(add_st == false)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    Rect r_seethrough = EditorGUILayout.BeginVertical("Button");
                    ShowSeeThrough = EditorGUILayout.Foldout(ShowSeeThrough, "(See Through)", true, EditorStyles.foldout);

                    if (ShowSeeThrough)
                    {

                        GUILayout.Space(10);

                        materialEditor.ShaderProperty(_RefVal, new GUIContent(_RefVal.displayName, TOTIPS[112]));
                        materialEditor.ShaderProperty(_Oper, new GUIContent(_Oper.displayName, TOTIPS[113]));
                        materialEditor.ShaderProperty(_Compa, new GUIContent(_Compa.displayName, TOTIPS[114]));

                        GUILayout.Space(10);

                    }

                    EditorGUILayout.EndVertical();

                    GUILayout.Space(20);
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                #endregion


                //Disable/Enable Features

                #region Disable/Enable Features

                Rect r_disableenablefeature = EditorGUILayout.BeginVertical("Button");
                ShowDisableEnable = EditorGUILayout.Foldout(ShowDisableEnable, "(Disable/Enable Features)", true, EditorStyles.foldout);

                if (ShowDisableEnable)
                {

                    Rect r_mc = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_MC, new GUIContent(_N_F_MC.displayName, TOTIPSEDF[0]));
                    EditorGUILayout.EndVertical();

                    Rect r_nm = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_NM, new GUIContent(_N_F_NM.displayName, TOTIPSEDF[1]));
                    EditorGUILayout.EndVertical();

                    if (remoout == true)
                    {
                        Rect r_ou = EditorGUILayout.BeginVertical("HelpBox");

                        EditorGUI.BeginChangeCheck();

                        materialEditor.ShaderProperty(_N_F_O, new GUIContent(_N_F_O.displayName, TOTIPSEDF[2]));

                        if (EditorGUI.EndChangeCheck())
                        {
                            int f_deo_int = (int)_N_F_O.floatValue;
                            foreach (Material m in materialEditor.targets)
                            {
                                switch (f_deo_int)
                                {
                                    case 0:
                                        m.SetShaderPassEnabled("SRPDefaultUnlit", false);
                                        break;
                                    case 1:
                                        m.SetShaderPassEnabled("SRPDefaultUnlit", true);
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }

                        EditorGUILayout.EndVertical();
                    }

                    EditorGUI.BeginDisabledGroup(_TRANSMODE.floatValue == 0);

                    Rect r_co = EditorGUILayout.BeginVertical("HelpBox");

                    EditorGUI.BeginChangeCheck();

                    materialEditor.ShaderProperty(_N_F_CO, new GUIContent(_N_F_CO.displayName, TOTIPSEDF[3]));

                    if (EditorGUI.EndChangeCheck())
                    {
                        int f_co_int = (int)_N_F_CO.floatValue;
                        foreach (Material m in materialEditor.targets)
                        {
                            switch (f_co_int)
                            {
                                case 0:

                                    m.renderQueue = 3000;
                                    m.SetOverrideTag("RenderType", "Transparent");
                                    break;

                                case 1:

                                    m.renderQueue = 2450;
                                    m.SetOverrideTag("RenderType", "TransparentCutout");
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    EditorGUILayout.EndVertical();

                    EditorGUI.EndDisabledGroup();

                    Rect r_ca = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_CA, new GUIContent(_N_F_CA.displayName, TOTIPSEDF[4]));
                    EditorGUILayout.EndVertical();


                    EditorGUI.BeginChangeCheck();

                    Rect r_sl = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_SL, new GUIContent(_N_F_SL.displayName, TOTIPSEDF[5]));
                    EditorGUILayout.EndVertical();

                    if (EditorGUI.EndChangeCheck())
                    {
                        int f_sl_int = (int)_N_F_SL.floatValue;
                        foreach (Material m in materialEditor.targets)
                        {
                            switch (f_sl_int)
                            {
                                case 0:
                                    m.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
                                    break;
                                case 1:
                                    m.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }


                    if (_N_F_OFLMB.floatValue == 0)
                    {
                        Rect r_o = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_GLO, new GUIContent(_N_F_GLO.displayName, TOTIPSEDF[6]));
                        EditorGUILayout.EndVertical();

                        Rect r_glot = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_GLOT, new GUIContent(_N_F_GLOT.displayName, TOTIPSEDF[7]));
                        EditorGUILayout.EndVertical();
                    }


                    if (_N_F_OFLMB.floatValue == 0)
                    {
                        Rect r_ss = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_SS, new GUIContent(_N_F_SS.displayName, TOTIPSEDF[8]));
                        EditorGUILayout.EndVertical();
                    }


                    if (_N_F_OFLMB.floatValue == 0)
                    {
                        Rect r_son = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_SON, new GUIContent(_N_F_SON.displayName, TOTIPSEDF[9]));
                        EditorGUILayout.EndVertical();
                    }


                    if (_N_F_OFLMB.floatValue == 0)
                    {
                        Rect r_sct = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_SCT, new GUIContent(_N_F_SCT.displayName, TOTIPSEDF[10]));
                        EditorGUILayout.EndVertical();
                    }


                    if (_N_F_OFLMB.floatValue == 0)
                    {
                        Rect r_st = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_ST, new GUIContent(_N_F_ST.displayName, TOTIPSEDF[11]));
                        EditorGUILayout.EndVertical();
                    }


                    if (_N_F_OFLMB.floatValue == 0)
                    {
                        Rect r_pt = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_PT, new GUIContent(_N_F_PT.displayName, TOTIPSEDF[12]));
                        EditorGUILayout.EndVertical();
                    }


                    if (_N_F_OFLMB.floatValue == 0)
                    {
                        Rect r_cld = EditorGUILayout.BeginVertical("HelpBox");
                        materialEditor.ShaderProperty(_N_F_CLD, new GUIContent(_N_F_CLD.displayName, TOTIPSEDF[13]));
                        EditorGUILayout.EndVertical();
                    }


                    Rect r_r = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_R, new GUIContent(_N_F_R.displayName, TOTIPSEDF[14]));
                    EditorGUILayout.EndVertical();

                    Rect r_fr = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_FR, new GUIContent(_N_F_FR.displayName, TOTIPSEDF[15]));
                    EditorGUILayout.EndVertical();

                    Rect r_rl = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_RL, new GUIContent(_N_F_RL.displayName, TOTIPSEDF[16]));
                    EditorGUILayout.EndVertical();

                    Rect r_nfd = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_NFD, new GUIContent(_N_F_NFD.displayName, TOTIPSEDF[17]));
                    EditorGUILayout.EndVertical();

                    Rect r_tp = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_TP, new GUIContent(_N_F_TP.displayName, TOTIPSEDF[18]));
                    EditorGUILayout.EndVertical();

                    Rect r_pa = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_PA, new GUIContent(_N_F_PA.displayName, TOTIPSEDF[19]));
                    EditorGUILayout.EndVertical();

                    Rect r_se = EditorGUILayout.BeginVertical("HelpBox");
                    materialEditor.ShaderProperty(_N_F_SE, new GUIContent(_N_F_SE.displayName, TOTIPSEDF[20]));
                    EditorGUILayout.EndVertical();

                }

                EditorGUILayout.EndVertical();

                #endregion

                //Settings

                #region Settings

                GUILayout.Space(5);

                Rect r_showsettings = EditorGUILayout.BeginVertical("Button");
                ShowSettings = EditorGUILayout.Foldout(ShowSettings, "(Settings)", true, EditorStyles.foldout);

                if (ShowSettings)
                {

                    GUILayout.Space(10);

                    if (GUILayout.Button(new GUIContent(twofourfive_target_string, TOTIPS[116]), "Button"))
                    {
                        TWOFORFIVE();
                    }

                    if (twofourfive_target == true)
                    {
                        if (GUILayout.Button(new GUIContent(dots_lbs_cd_string, TOTIPS[143]), "Button"))
                        {
                            DOTSLBSCD();
                        }
                    }

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    if (GUILayout.Button(new GUIContent(add_st_string, TOTIPS[163]), "Button"))
                    {
                        ADD_ST();
                    }

                    GUILayout.Space(10);

                }

                EditorGUILayout.EndVertical();

                #endregion

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                if (_N_F_OFLMB.floatValue == 0)
                {
                    GUILayout.Space(10);

                    materialEditor.ShaderProperty(_N_F_HDLS, new GUIContent(_N_F_HDLS.displayName, TOTIPS[117]));
                    materialEditor.ShaderProperty(_N_F_HPSS, new GUIContent(_N_F_HPSS.displayName, TOTIPS[118]));

                    EditorGUI.BeginChangeCheck();

                    materialEditor.ShaderProperty(_N_F_DCS, new GUIContent(_N_F_DCS.displayName, TOTIPS[119]));

                    if (EditorGUI.EndChangeCheck())
                    {
                        int f_hcs_int = (int)_N_F_DCS.floatValue;
                        foreach (Material m in materialEditor.targets)
                        {
                            switch (f_hcs_int)
                            {
                                case 0:
                                    m.SetShaderPassEnabled("ShadowCaster", true);
                                    break;
                                case 1:
                                    m.SetShaderPassEnabled("ShadowCaster", false);
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                    materialEditor.ShaderProperty(_N_F_NLASOBF, new GUIContent(_N_F_NLASOBF.displayName, TOTIPS[115]));

                    GUILayout.Space(10);

                }

                materialEditor.ShaderProperty(_ZWrite, new GUIContent(_ZWrite.displayName, TOTIPS[120]));

                GUILayout.Space(10);

                materialEditor.RenderQueueField();

                GUILayout.Space(10);

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                GUILayout.Space(10);

                materialEditor.EnableInstancingField();

                materialEditor.ShaderProperty(_N_F_DDMD, new GUIContent(_N_F_DDMD.displayName, TOTIPS[151]));

                materialEditor.ShaderProperty(_N_F_RDC, new GUIContent(_N_F_RDC.displayName, TOTIPS[147]));
                materialEditor.ShaderProperty(_N_F_OFLMB, new GUIContent(_N_F_OFLMB.displayName, TOTIPS[141]));
                aruskw = EditorGUILayout.Toggle(new GUIContent("Automatic Remove Unused Shader Keywords (Global)", TOTIPS[121]), aruskw);

                GUILayout.Space(10);

            }

            #region Automatic Remove UorOSKW
            if (aruskw == true)
            {
                foreach (Material m1 in materialEditor.targets)
                {
                    for (int x = 0; x < m1.shaderKeywords.Length; x++)
                    {
                        if (m1.shaderKeywords[x] != String.Empty)
                        {
                            for (int y = 0; y < Enum.GetValues(typeof(SFKW)).Length; y++)
                            {
                                if (m1.shaderKeywords[x] == Enum.GetValues(typeof(SFKW)).GetValue(y).ToString())
                                {
                                    del_skw = false;
                                    break;
                                }
                                else
                                {
                                    del_skw = true;
                                }
                            }

                            if (del_skw == true)
                            {
                                m1.DisableKeyword(m1.shaderKeywords[x]);
                                del_skw = false;
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            #endregion

            //Footbar
            #region Footbar

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            Rect r_footbar = EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(new GUIContent("[" + remooutstat + " (On Shader)]", TOTIPS[135]), "Toolbar"))
            {
                REMO_OUTL();
            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("[Refresh Settings]", TOTIPS[62]), "Toolbar"))
            {
                foreach (Material m in materialEditor.targets)
                {
                    CheckingPropKeyWord(m);
                }

                Check_RE_OL();

                Debug.Log("You clicked [Refresh Settings]: RealToon on the material has been refresh and re-apply the settings properly.");

            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("[Video Tutorials]", TOTIPS[136]), "Toolbar"))
            {
                Application.OpenURL("www.youtube.com/playlist?list=PL0M1m9smMVPJ4qEkJnZObqJE5mU9uz6SY");
            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("[RealToon (User Guide).pdf]", TOTIPS[137]), "Toolbar"))
            {
                Application.OpenURL(Application.dataPath + "/RealToon/RealToon (User Guide).pdf");
            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("[" + ShowUIString + "(Global)]", TOTIPS[138]), "Toolbar"))
            {
                if (ShowUI == false)
                {
                    ShowUI = true;
                    ShowUIString = "Hide UI";
                }
                else
                {
                    ShowUI = false;
                    ShowUIString = "Show UI";
                }
            }

            EditorGUILayout.EndHorizontal();

            #endregion

            #endregion

        }

        //
        #region Checking

        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            if (oldShader.name != "Universal Render Pipeline/RealToon/Version 5/Default/Default")
            {

                if (oldShader.name == "HDRP/RealToon/Version 5/Default")
                {
                    material.SetFloat("_MaiColPo", material.GetFloat("_MaiColPo") + 0.65f);
                }

            }

            CheckingPropKeyWord(material);
        }

        #region CheckingPropKeyWord

        void CheckingPropKeyWord(Material material)
        {

            if (material.IsKeywordEnabled("N_F_TRANS_ON") || material.GetFloat("_TRANSMODE") == 1.0f)
            {

                if (material.IsKeywordEnabled("N_F_CO_ON") || material.GetFloat("_N_F_CO") == 1.0f)
                {

                    material.renderQueue = 2450;
                    material.SetOverrideTag("RenderType", "TransparentCutout");

                }
                else if (material.IsKeywordEnabled("N_F_TRANS_ON") || material.GetFloat("_TRANSMODE") == 1.0f)
                {
                    material.renderQueue = 3000;

                    material.EnableKeyword("N_F_TRANS_ON");
                    material.SetFloat("_TRANSMODE", 1.0f);
                    material.SetOverrideTag("RenderType", "Transparent");
                }

                shader_type = "Transparency";
            }
            else if (!material.IsKeywordEnabled("N_F_TRANS_ON") || material.GetFloat("_TRANSMODE") == 0.0f)
            {
                material.DisableKeyword("N_F_TRANS_ON");
                material.SetFloat("_TRANSMODE", 0.0f);

                shader_type = "Default";
            }

            if ((material.IsKeywordEnabled("N_F_TRANSAFFSHA") || material.GetFloat("_TransAffSha") == 1.0f))
            {
                material.EnableKeyword("N_F_TRANSAFFSHA");
                material.SetFloat("_TransAffSha", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_TRANSAFFSHA") || material.GetFloat("_TransAffSha") == 0.0f))
            {
                material.DisableKeyword("N_F_TRANSAFFSHA");
                material.SetFloat("_TransAffSha", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_OFLMB_ON") || material.GetFloat("_N_F_OFLMB") == 1.0f))
            {
                material.EnableKeyword("N_F_OFLMB_ON");
                material.SetFloat("_N_F_OFLMB", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_OFLMB_ON") || material.GetFloat("_N_F_OFLMB") == 0.0f))
            {
                material.DisableKeyword("N_F_OFLMB_ON");
                material.SetFloat("_N_F_OFLMB", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_SIMTRANS_ON") || material.GetFloat("_SimTrans") == 1.0f))
            {
                material.EnableKeyword("N_F_SIMTRANS_ON");
                material.SetFloat("_SimTrans", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_SIMTRANS_ON") || material.GetFloat("_SimTrans") == 0.0f))
            {
                material.DisableKeyword("N_F_SIMTRANS_ON");
                material.SetFloat("_SimTrans", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_DDMD_ON") || material.GetFloat("_N_F_DDMD") == 1.0f))
            {
                material.EnableKeyword("N_F_DDMD_ON");
                material.SetFloat("_N_F_DDMD", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_DDMD_ON") || material.GetFloat("_N_F_DDMD") == 0.0f))
            {
                material.DisableKeyword("N_F_DDMD_ON");
                material.SetFloat("_N_F_DDMD", 0.0f);
            }

            //======================================================================================================

            if ((material.IsKeywordEnabled("N_F_DNO_ON") || material.GetFloat("_DynamicNoisyOutline") == 1.0f))
            {
                material.EnableKeyword("N_F_DNO_ON");
                material.SetFloat("_DynamicNoisyOutline", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_DNO_ON") || material.GetFloat("_DynamicNoisyOutline") == 0.0f))
            {
                material.DisableKeyword("N_F_DNO_ON");
                material.SetFloat("_DynamicNoisyOutline", 0.0f);
            }

            //======================================================================================================

            if ((material.IsKeywordEnabled("N_F_COEDGL_ON") || material.GetFloat("_N_F_COEDGL") == 1.0f))
            {
                material.EnableKeyword("N_F_COEDGL_ON");
                material.SetFloat("_N_F_COEDGL", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_COEDGL_ON") || material.GetFloat("_N_F_COEDGL") == 0.0f))
            {
                material.DisableKeyword("N_F_COEDGL_ON");
                material.SetFloat("_N_F_COEDGL", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_MC_ON") || material.GetFloat("_N_F_MC") == 1.0f))
            {
                material.EnableKeyword("N_F_MC_ON");
                material.SetFloat("_N_F_MC", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_MC_ON") || material.GetFloat("_N_F_MC") == 0.0f))
            {
                material.DisableKeyword("N_F_MC_ON");
                material.SetFloat("_N_F_MC", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_NM_ON") || material.GetFloat("_N_F_NM") == 1.0f))
            {
                material.EnableKeyword("N_F_NM_ON");
                material.SetFloat("_N_F_NM", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_NM_ON") || material.GetFloat("_N_F_NM") == 0.0f))
            {
                material.DisableKeyword("N_F_NM_ON");
                material.SetFloat("_N_F_NM", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_CO_ON") || material.GetFloat("_N_F_CO") == 1.0f))
            {
                material.EnableKeyword("N_F_CO_ON");
                material.SetFloat("_N_F_CO", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_CO_ON") || material.GetFloat("_N_F_CO") == 0.0f))
            {
                material.DisableKeyword("N_F_CO_ON");
                material.SetFloat("_N_F_CO", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_SCO_ON") || material.GetFloat("_N_F_SCO") == 1.0f))
            {
                material.EnableKeyword("N_F_SCO_ON");
                material.SetFloat("_N_F_SCO", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_SCO_ON") || material.GetFloat("_N_F_SCO") == 0.0f))
            {
                material.DisableKeyword("N_F_SCO_ON");
                material.SetFloat("_N_F_SCO", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_O_ON") || material.GetFloat("_N_F_O") == 1.0f))
            {
                material.EnableKeyword("N_F_O_ON");
                material.SetShaderPassEnabled("SRPDefaultUnlit", true);
                material.SetFloat("_N_F_O", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_O_ON") || material.GetFloat("_N_F_O") == 0.0f))
            {
                material.DisableKeyword("N_F_O_ON");
                material.SetShaderPassEnabled("SRPDefaultUnlit", false);
                material.SetFloat("_N_F_O", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_CA_ON") || material.GetFloat("_N_F_CA") == 1.0f))
            {
                material.EnableKeyword("N_F_CA_ON");
                material.SetFloat("_N_F_CA", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_CA_ON") || material.GetFloat("_N_F_CA") == 0.0f))
            {
                material.DisableKeyword("N_F_CA_ON");
                material.SetFloat("_N_F_CA", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_SL_ON") || material.GetFloat("_N_F_SL") == 1.0f))
            {
                material.EnableKeyword("N_F_SL_ON");
                material.SetFloat("_N_F_SL", 1.0f);
                material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
            }
            else if ((!material.IsKeywordEnabled("N_F_SL_ON") || material.GetFloat("_N_F_SL") == 0.0f))
            {
                material.DisableKeyword("N_F_SL_ON");
                material.SetFloat("_N_F_SL", 0.0f);
                material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
            }

            if ((material.IsKeywordEnabled("N_F_GLO_ON") || material.GetFloat("_N_F_GLO") == 1.0f))
            {
                material.EnableKeyword("N_F_GLO_ON");
                material.SetFloat("_N_F_GLO", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_GLO_ON") || material.GetFloat("_N_F_GLO") == 0.0f))
            {
                material.DisableKeyword("N_F_GLO_ON");
                material.SetFloat("_N_F_GLO", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_GLOT_ON") || material.GetFloat("_N_F_GLOT") == 1.0f))
            {
                material.EnableKeyword("N_F_GLOT_ON");
                material.SetFloat("_N_F_GLOT", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_GLOT_ON") || material.GetFloat("_N_F_GLOT") == 0.0f))
            {
                material.DisableKeyword("N_F_GLOT_ON");
                material.SetFloat("_N_F_GLOT", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_SS_ON") || material.GetFloat("_N_F_SS") == 1.0f))
            {
                material.EnableKeyword("N_F_SS_ON");
                material.SetFloat("_N_F_SS", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_SS_ON") || material.GetFloat("_N_F_SS") == 0.0f))
            {
                material.DisableKeyword("N_F_SS_ON");
                material.SetFloat("_N_F_SS", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_SON_ON") || material.GetFloat("_N_F_SON") == 1.0f))
            {
                material.EnableKeyword("N_F_SON_ON");
                material.SetFloat("_N_F_SON", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_SON_ON") || material.GetFloat("_N_F_SON") == 0.0f))
            {
                material.DisableKeyword("N_F_SON_ON");
                material.SetFloat("_N_F_SON", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_SCT_ON") || material.GetFloat("_N_F_SCT") == 1.0f))
            {
                material.EnableKeyword("N_F_SCT_ON");
                material.SetFloat("_N_F_SCT", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_SCT_ON") || material.GetFloat("_N_F_SCT") == 0.0f))
            {
                material.DisableKeyword("N_F_SCT_ON");
                material.SetFloat("_N_F_SCT", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_ST_ON") || material.GetFloat("_N_F_ST") == 1.0f))
            {
                material.EnableKeyword("N_F_ST_ON");
                material.SetFloat("_N_F_ST", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_ST_ON") || material.GetFloat("_N_F_ST") == 0.0f))
            {
                material.DisableKeyword("N_F_ST_ON");
                material.SetFloat("_N_F_ST", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_PT_ON") || material.GetFloat("_N_F_PT") == 1.0f))
            {
                material.EnableKeyword("N_F_PT_ON");
                material.SetFloat("_N_F_PT", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_PT_ON") || material.GetFloat("_N_F_PT") == 0.0f))
            {
                material.DisableKeyword("N_F_PT_ON");
                material.SetFloat("_N_F_PT", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_CLD_ON") || material.GetFloat("_N_F_CLD") == 1.0f))
            {
                material.EnableKeyword("N_F_CLD_ON");
                material.SetFloat("_N_F_CLD", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_CLD_ON") || material.GetFloat("_N_F_CLD") == 0.0f))
            {
                material.DisableKeyword("N_F_CLD_ON");
                material.SetFloat("_N_F_CLD", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_R_ON") || material.GetFloat("_N_F_R") == 1.0f))
            {
                material.EnableKeyword("N_F_R_ON");
                material.SetFloat("_N_F_R", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_R_ON") || material.GetFloat("_N_F_R") == 0.0f))
            {
                material.DisableKeyword("N_F_R_ON");
                material.SetFloat("_N_F_R", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_FR_ON") || material.GetFloat("_N_F_FR") == 1.0f))
            {
                material.EnableKeyword("N_F_FR_ON");
                material.SetFloat("_N_F_FR", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_FR_ON") || material.GetFloat("_N_F_FR") == 0.0f))
            {
                material.DisableKeyword("N_F_FR_ON");
                material.SetFloat("_N_F_FR", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_RL_ON") || material.GetFloat("_N_F_RL") == 1.0f))
            {
                material.EnableKeyword("N_F_RL_ON");
                material.SetFloat("_N_F_RL", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_RL_ON") || material.GetFloat("_N_F_RL") == 0.0f))
            {
                material.DisableKeyword("N_F_RL_ON");
                material.SetFloat("_N_F_RL", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_NFD_ON") || material.GetFloat("_N_F_NFD") == 1.0f))
            {
                material.EnableKeyword("N_F_NFD_ON");
                material.SetFloat("_N_F_NFD", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_NFD_ON") || material.GetFloat("_N_F_NFD") == 0.0f))
            {
                material.DisableKeyword("N_F_NFD_ON");
                material.SetFloat("_N_F_NFD", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_TP_ON") || material.GetFloat("_N_F_TP") == 1.0f))
            {
                material.EnableKeyword("N_F_TP_ON");
                material.SetFloat("_N_F_TP", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_TP_ON") || material.GetFloat("_N_F_TP") == 0.0f))
            {
                material.DisableKeyword("N_F_TP_ON");
                material.SetFloat("_N_F_TP", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_STSDFM_ON") || material.GetFloat("_N_F_STSDFM") == 1.0f))
            {
                material.EnableKeyword("N_F_STSDFM_ON");
                material.SetFloat("_N_F_STSDFM", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_STSDFM_ON") || material.GetFloat("_N_F_STSDFM") == 0.0f))
            {
                material.DisableKeyword("N_F_STSDFM_ON");
                material.SetFloat("_N_F_STSDFM", 0.0f);
            }

            //======================================================================================================

            if ((material.IsKeywordEnabled("N_F_ESSAO_ON") || material.GetFloat("_N_F_ESSAO") == 1.0f))
            {
                material.EnableKeyword("N_F_ESSAO_ON");
                material.SetFloat("_N_F_ESSAO", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_ESSAO_ON") || material.GetFloat("_N_F_ESSAO") == 0.0f))
            {
                material.DisableKeyword("N_F_ESSAO_ON");
                material.SetFloat("_N_F_ESSAO", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_RELGI_ON") || material.GetFloat("_RELG") == 1.0f))
            {
                material.EnableKeyword("N_F_RELGI_ON");
                material.SetFloat("_RELG", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_RELGI_ON") || material.GetFloat("_RELG") == 0.0f))
            {
                material.DisableKeyword("N_F_RELGI_ON");
                material.SetFloat("_RELG", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_USETLB_ON") || material.GetFloat("_UseTLB") == 1.0f))
            {
                material.EnableKeyword("N_F_USETLB_ON");
                material.SetFloat("_UseTLB", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_USETLB_ON") || material.GetFloat("_UseTLB") == 0.0f))
            {
                material.DisableKeyword("N_F_USETLB_ON");
                material.SetFloat("_UseTLB", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_EAL_ON") || material.GetFloat("_N_F_EAL") == 1.0f))
            {
                material.EnableKeyword("N_F_EAL_ON");
                material.SetFloat("_N_F_EAL", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_EAL_ON") || material.GetFloat("_N_F_EAL") == 0.0f))
            {
                material.DisableKeyword("N_F_EAL_ON");
                material.SetFloat("_N_F_EAL", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_HDLS_ON") || material.GetFloat("_N_F_HDLS") == 1.0f))
            {
                material.EnableKeyword("N_F_HDLS_ON");
                material.SetFloat("_N_F_HDLS", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_HDLS_ON") || material.GetFloat("_N_F_HDLS") == 0.0f))
            {
                material.DisableKeyword("N_F_HDLS_ON");
                material.SetFloat("_N_F_HDLS", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_HPSS_ON") || material.GetFloat("_N_F_HPSS") == 1.0f))
            {
                material.EnableKeyword("N_F_HPSS_ON");
                material.SetFloat("_N_F_HPSS", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_HPSS_ON") || material.GetFloat("_N_F_HPSS") == 0.0f))
            {
                material.DisableKeyword("N_F_HPSS_ON");
                material.SetFloat("_N_F_HPSS", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_DCS_ON") || material.GetFloat("_N_F_DCS") == 1.0f))
            {
                material.EnableKeyword("N_F_DCS_ON");
                material.SetShaderPassEnabled("ShadowCaster", false);
                material.SetFloat("_N_F_DCS", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_DCS_ON") || material.GetFloat("_N_F_DCS") == 0.0f))
            {
                material.DisableKeyword("N_F_DCS_ON");
                material.SetShaderPassEnabled("ShadowCaster", true);
                material.SetFloat("_N_F_DCS", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_RDC_ON") || material.GetFloat("_N_F_RDC") == 1.0f))
            {
                material.EnableKeyword("N_F_RDC_ON");
                material.SetFloat("_N_F_RDC", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_RDC_ON") || material.GetFloat("_N_F_RDC") == 0.0f))
            {
                material.DisableKeyword("N_F_RDC_ON");
                material.SetFloat("_N_F_RDC", 0.0f);
            }

            if ((material.IsKeywordEnabled("N_F_NLASOBF_ON") || material.GetFloat("_N_F_NLASOBF") == 1.0f))
            {
                material.EnableKeyword("_N_F_NLASOBF");
                material.SetFloat("_N_F_NLASOBF", 1.0f);
            }
            else if ((!material.IsKeywordEnabled("N_F_NLASOBF_ON") || material.GetFloat("_N_F_NLASOBF") == 0.0f))
            {
                material.DisableKeyword("N_F_NLASOBF_ON");
                material.SetFloat("_N_F_NLASOBF", 0.0f);
            }

            #endregion

        }

        #endregion

        #region ChanLi
        static void ChanLi(string searchTXT, string TXTChange, string fileName)
        {

            if (System.IO.File.Exists(fileName))
            {
                string[] arrLine = System.IO.File.ReadAllLines(fileName);

                for (int i = 0; i < arrLine.Length; ++i)
                {
                    if (arrLine[i] == searchTXT)
                    {
                        arrLine[i] = TXTChange;
                        System.IO.File.WriteAllLines(fileName, arrLine);
                        break;
                    }
                }

            }
            else
            {
                Debug.Log("Can't enable do 'Use Screen Space Outline' or 'Use Traditional Outline' because '" + fileName + "' Does not exist or file not found.");
            }

        }
        #endregion

        #region ReaLi
        static bool ReaLi(string searchTXT, string fileName)
        {

            if (System.IO.File.Exists(fileName))
            {
                string[] arrLine = System.IO.File.ReadAllLines(fileName);

                for (int i = 0; i < arrLine.Length; ++i)
                {
                    if (arrLine[i] == searchTXT)
                    {
                        return true;
                    }
                }

            }
            else
            {
                Debug.Log("Can't read a line because '" + fileName + "' Does not exist or file not found.");
            }

            return false;

        }

        #endregion

        #region Check_RE_OL
        void Check_RE_OL()
        {
            if (ReaLi("//OL_RE", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader"))
            {
                remoout = true;
                REMO_OUTL();
            }
            else
            {
                remoout = false;
                REMO_OUTL();
            }
        }
        #endregion

        #region REMO_OUTL
        void REMO_OUTL()
        {
            if (remoout == true)
            {
                ChanLi("Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "SRPDefaultUnlit" + (char)34 + "}", "Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "remove" + (char)34 + "}", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("Cull [_DoubleSidedOutline]//OL_RCUL", "//Cull [_DoubleSidedOutline]//OL_RCUL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("#ifdef UNITY_COLORSPACE_GAMMA//SSOL", "//#ifdef UNITY_COLORSPACE_GAMMA//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL", "//_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#endif//SSOL", "//#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("#if N_F_O_ON//SSOL", "//#if N_F_O_ON//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL", "//float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#if N_F_O_MOTTSO_ON//SSOL", "//#if N_F_O_MOTTSO_ON//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL", "//float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#else//SSOL", "//#else//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL", "//float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#endif//SSOL", "//#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#else//SSOL", "//#else//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("#endif//SSOL", "//#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//OL_NRE", "//OL_RE", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//SSOL_U", "//SSOL_NU", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                //============================
                //============================

                ChanLi("static bool remoout = true;", "static bool remoout = false;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string remooutstat = " + (char)34 + "Remove Outline" + (char)34 + ";", "static string remooutstat = " + (char)34 + "Add Outline" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.Log("Outline feature removed on RealToon URP shader.");
            }
            else if (remoout == false)
            {
                ChanLi("Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "remove" + (char)34 + "}", "Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "SRPDefaultUnlit" + (char)34 + "}", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//Cull [_DoubleSidedOutline]//OL_RCUL", "Cull [_DoubleSidedOutline]//OL_RCUL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//OL_RE", "//OL_NRE", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                Check_SSOL_TOL();

                //============================
                //============================

                ChanLi("static bool remoout = false;", "static bool remoout = true;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string remooutstat = " + (char)34 + "Add Outline" + (char)34 + ";", "static string remooutstat = " + (char)34 + "Remove Outline" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.Log("Outline feature added on RealToon URP shader.");
            }
        }
        #endregion

        #region Check_SSOL_TOL
        void Check_SSOL_TOL()
        {
            if (ReaLi("//SSOL_U", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader"))
            {
                UseSSOL = true;
                USSOL_OR_TOL();
            }
            else
            {
                UseSSOL = false;
                USSOL_OR_TOL();
            }
        }
        #endregion

        #region USSOL_OR_TOL
        void USSOL_OR_TOL()
        {
            if (UseSSOL == true)
            {
                ChanLi("Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "SRPDefaultUnlit" + (char)34 + "}", "Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "remove" + (char)34 + "}", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("Cull [_DoubleSidedOutline]//OL_RCUL", "//Cull [_DoubleSidedOutline]//OL_RCUL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//#ifdef UNITY_COLORSPACE_GAMMA//SSOL", "#ifdef UNITY_COLORSPACE_GAMMA//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL", "_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//#endif//SSOL", "#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//#if N_F_O_ON//SSOL", "#if N_F_O_ON//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL", "float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//#if N_F_O_MOTTSO_ON//SSOL", "#if N_F_O_MOTTSO_ON//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL", "float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//#else//SSOL", "#else//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL", "float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//#endif//SSOL", "#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//#else//SSOL", "#else//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//#endif//SSOL", "#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//SSOL_NU", "//SSOL_U", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                //============================
                //============================

                ChanLi("static bool UseSSOL = true;", "static bool UseSSOL = false;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string OLType = " + (char)34 + "Traditional" + (char)34 + ";", "static string OLType = " + (char)34 + "Screen Space" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string UseSSOLStat = " + (char)34 + "Use Screen Space Outline" + (char)34 + ";", "static string UseSSOLStat = " + (char)34 + "Use Traditional Outline" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.Log("Screen Space Outline is now use.");
            }
            else if (UseSSOL == false)
            {
                ChanLi("Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "remove" + (char)34 + "}", "Tags{" + (char)34 + "LightMode" + (char)34 + "=" + (char)34 + "SRPDefaultUnlit" + (char)34 + "}", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//Cull [_DoubleSidedOutline]//OL_RCUL", "Cull [_DoubleSidedOutline]//OL_RCUL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("#ifdef UNITY_COLORSPACE_GAMMA//SSOL", "//#ifdef UNITY_COLORSPACE_GAMMA//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL", "//_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#endif//SSOL", "//#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("#if N_F_O_ON//SSOL", "//#if N_F_O_ON//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL", "//float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#if N_F_O_MOTTSO_ON//SSOL", "//#if N_F_O_MOTTSO_ON//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL", "//float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#else//SSOL", "//#else//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL", "//float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#endif//SSOL", "//#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#else//SSOL", "//#else//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("#endif//SSOL", "//#endif//SSOL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//SSOL_U", "//SSOL_NU", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                //============================
                //============================

                ChanLi("static bool UseSSOL = false;", "static bool UseSSOL = true;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string OLType = " + (char)34 + "Screen Space" + (char)34 + ";", "static string OLType = " + (char)34 + "Traditional" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string UseSSOLStat = " + (char)34 + "Use Traditional Outline" + (char)34 + ";", "static string UseSSOLStat = " + (char)34 + "Use Screen Space Outline" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.Log("Traditional Outline is now use.");
            }
        }
        #endregion

        #region TWOFORFIVE
        void TWOFORFIVE()
        {
            if (twofourfive_target == false)
            {
                ChanLi("static bool twofourfive_target = false;", "static bool twofourfive_target = true;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string twofourfive_target_string = " + (char)34 + "Change shader compilation target to 4.5" + (char)34 + ";", "static string twofourfive_target_string = " + (char)34 + "Change shader compilation target to 2.0" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");

                ChanLi("#pragma target 2.0 //targetol", "#pragma target 4.5 //targetol", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 2.0 //targetfl", "#pragma target 4.5 //targetfl", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 2.0 //targetsc", "#pragma target 4.5 //targetsc", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 2.0 //targetdo", "#pragma target 4.5 //targetdo", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 2.0 //targetdn", "#pragma target 4.5 //targetdn", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 2.0 //targetm", "#pragma target 4.5 //targetm", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.LogWarning("RealToon shader compilation target has been changed to 4.5, added support for DOTS and Tessellation.");
            }
            else if (twofourfive_target == true)
            {
                ChanLi("static bool twofourfive_target = true;", "static bool twofourfive_target = false;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string twofourfive_target_string = " + (char)34 + "Change shader compilation target to 2.0" + (char)34 + ";", "static string twofourfive_target_string = " + (char)34 + "Change shader compilation target to 4.5" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");

                ChanLi("#pragma target 4.5 //targetol", "#pragma target 2.0 //targetol", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 4.5 //targetfl", "#pragma target 2.0 //targetfl", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 4.5 //targetsc", "#pragma target 2.0 //targetsc", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 4.5 //targetdo", "#pragma target 2.0 //targetdo", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 4.5 //targetdn", "#pragma target 2.0 //targetdn", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("#pragma target 4.5 //targetm", "#pragma target 2.0 //targetm", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.LogWarning("RealToon shader compilation target has been changed to 2.0.");
            }
        }
        #endregion

        #region  DOTSLBSCD

        void DOTSLBSCD()
        {
            if (dots_lbs_cd == false)
            {

            ChanLi("static bool dots_lbs_cd = false;", "static bool dots_lbs_cd = true;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
            ChanLi("static string dots_lbs_cd_string = " + (char)34 + "DOTS|HR - Use Compute Deformation" + (char)34 + ";", "static string dots_lbs_cd_string = " + (char)34 + "DOTS|HR - Use Linear Blend Skinning" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");


            ChanLi("float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_OL", "//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("uint4 indices : BLENDINDICES;//DOTS_LiBleSki_OL", "//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//uint vertexID : SV_VertexID;//DOTS_CompDef_OL", "uint vertexID : SV_VertexID;//DOTS_CompDef_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_OL", "DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_OL", "//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


            ChanLi("float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_FL", "//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("uint4 indices : BLENDINDICES;//DOTS_LiBleSki_FL", "//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//uint vertexID : SV_VertexID;//DOTS_CompDef_FL", "uint vertexID : SV_VertexID;//DOTS_CompDef_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_FL", "DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_FL", "//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


            ChanLi("float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_GB", "//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("uint4 indices : BLENDINDICES;//DOTS_LiBleSki_GB", "//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//uint vertexID : SV_VertexID;//DOTS_CompDef_GB", "uint vertexID : SV_VertexID;//DOTS_CompDef_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_GB", "DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_GB", "//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


            ChanLi("float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_SC", "//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("uint4 indices : BLENDINDICES;//DOTS_LiBleSki_SC", "//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//uint vertexID : SV_VertexID;//DOTS_CompDef_SC", "uint vertexID : SV_VertexID;//DOTS_CompDef_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_SC", "DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_SC", "//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


            ChanLi("float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DO", "//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DO", "//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//uint vertexID : SV_VertexID;//DOTS_CompDef_DO", "uint vertexID : SV_VertexID;//DOTS_CompDef_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DO", "DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DO", "//DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


            ChanLi("float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DN", "//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DN", "//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//uint vertexID : SV_VertexID;//DOTS_CompDef_DN", "uint vertexID : SV_VertexID;//DOTS_CompDef_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DN", "DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            ChanLi("DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normal.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DN", "//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normal.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


            ChanLi("float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_MV", "//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
            ChanLi("uint4 indices : BLENDINDICES;//DOTS_LiBleSki_MV", "//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
            ChanLi("//uint vertexID : SV_VertexID;//DOTS_CompDef_MV", "uint vertexID : SV_VertexID;//DOTS_CompDef_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
            ChanLi("//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_MV", "DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
            ChanLi("DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_MV", "//DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");


            AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
            AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
            AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
            Debug.LogWarning("DOTS|HR - Compute Deformation is now use, This will enable you to use BlendShapes and other deformation.");

            }
            else if (dots_lbs_cd == true)
            {
                ChanLi("static bool dots_lbs_cd = true;", "static bool dots_lbs_cd = false;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string dots_lbs_cd_string = " + (char)34 + "DOTS|HR - Use Linear Blend Skinning" + (char)34 + ";", "static string dots_lbs_cd_string = " + (char)34 + "DOTS|HR - Use Compute Deformation" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");


                ChanLi("//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_OL", "float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_OL", "uint4 indices : BLENDINDICES;//DOTS_LiBleSki_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("uint vertexID : SV_VertexID;//DOTS_CompDef_OL", "//uint vertexID : SV_VertexID;//DOTS_CompDef_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_OL", "//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_OL", "DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_OL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


                ChanLi("//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_FL", "float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_FL", "uint4 indices : BLENDINDICES;//DOTS_LiBleSki_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("uint vertexID : SV_VertexID;//DOTS_CompDef_FL", "//uint vertexID : SV_VertexID;//DOTS_CompDef_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_FL", "//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_FL", "DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_FL", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


                ChanLi("//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_GB", "float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_GB", "uint4 indices : BLENDINDICES;//DOTS_LiBleSki_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("uint vertexID : SV_VertexID;//DOTS_CompDef_GB", "//uint vertexID : SV_VertexID;//DOTS_CompDef_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_GB", "//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_GB", "DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_GB", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


                ChanLi("//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_SC", "float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_SC", "uint4 indices : BLENDINDICES;//DOTS_LiBleSki_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("uint vertexID : SV_VertexID;//DOTS_CompDef_SC", "//uint vertexID : SV_VertexID;//DOTS_CompDef_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_SC", "//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_SC", "DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_SC", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


                ChanLi("//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DO", "float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DO", "uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("uint vertexID : SV_VertexID;//DOTS_CompDef_DO", "//uint vertexID : SV_VertexID;//DOTS_CompDef_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DO", "//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DO", "DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DO", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


                ChanLi("//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DN", "float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DN", "uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("uint vertexID : SV_VertexID;//DOTS_CompDef_DN", "//uint vertexID : SV_VertexID;//DOTS_CompDef_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DN", "//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normal.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DN", "DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normal.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DN", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");


                ChanLi("//float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_MV", "float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
                ChanLi("//uint4 indices : BLENDINDICES;//DOTS_LiBleSki_MV", "uint4 indices : BLENDINDICES;//DOTS_LiBleSki_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
                ChanLi("uint vertexID : SV_VertexID;//DOTS_CompDef_MV", "//uint vertexID : SV_VertexID;//DOTS_CompDef_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
                ChanLi("DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_MV", "//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
                ChanLi("//DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_MV", "DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_MV", "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");


                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.LogWarning("DOTS|HR - Linear Blending Skinning is now use.");
            }
        }

        #endregion

        #region ADD_ST
        void ADD_ST()
        {
            if (add_st == true)
            {
                ChanLi("static bool add_st = true;", "static bool add_st = false;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string add_st_string = " + (char)34 + "Add 'See Through' feature" + (char)34 + ";", "static string add_st_string = " + (char)34 + "Remove 'See Through' feature" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");

                ChanLi("/*//O_ST", "//O_ST/*", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("/*//F_ST", "//F_ST/*", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("/*//G_ST", "//G_ST/*", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("Pass Invert//O_PI", "//Pass Invert//O_PI", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.LogWarning("'See Through feature' has been added.");
            }
            else if (add_st == false)
            {
                ChanLi("static bool add_st = false;", "static bool add_st = true;", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("static string add_st_string = " + (char)34 + "Remove 'See Through' feature" + (char)34 + ";", "static string add_st_string = " + (char)34 + "Add 'See Through' feature" + (char)34 + ";", "Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                ChanLi("//Pass Invert//O_PI", "Pass Invert//O_PI", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                ChanLi("//O_ST/*", "/*//O_ST", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//F_ST/*", "/*//F_ST", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                ChanLi("//G_ST/*", "/*//G_ST", "Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");

                AssetDatabase.ImportAsset("Assets/RealToon/RealToon Shaders/Version 5/URP/Default/D_Default_URP.shader");
                AssetDatabase.ImportAsset("Assets/RealToon/Editor/RealToonShaderGUI_URP_SRP.cs");
                Debug.LogWarning("'See Through feature' has been remove.");
            }
        }
        #endregion
    }

}

#endif
