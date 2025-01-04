//RealToon URP - RT_URP_PROP
//MJQStudioWorks

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

//===============================================================================
//CBUF
//===============================================================================

CBUFFER_START(UnityPerMaterial)

	//== Others
		uniform float4 _MainTex_ST;

		uniform half4 _MainColor;
		uniform half _MaiColPo;
		uniform half _MVCOL;
		uniform half _MCIALO;
		uniform half _TexturePatternStyle;
		uniform half4 _HighlightColor;
		uniform half _HighlightColorPower;
	//==


	//== N_F_O_ON
		uniform float4 _OutlineWidthControl_ST;
		uniform half _OutlineWidth;
		uniform int _OutlineExtrudeMethod;
		uniform half3 _OutlineOffset;
		uniform half _OutlineZPostionInCamera;
		uniform half4 _OutlineColor;
		uniform half _MixMainTexToOutline;
		uniform half _NoisyOutlineIntensity;
		uniform half _DynamicNoisyOutline;
		uniform half _LightAffectOutlineColor;
		uniform half _OutlineWidthAffectedByViewDistance;
		uniform half _FarDistanceMaxWidth;
		uniform half _VertexColorBlueAffectOutlineWitdh;
	//==


	//==  N_F_O_SSOL
		uniform float _DepthThreshold;
	//==


	//== N_F_MC_ON
		uniform half _MCapIntensity;

		uniform float4 _MCap_ST;

		uniform half _SPECMODE;
		uniform half _SPECIN;

		uniform float4 _MCapMask_ST;
	//==


	//== Transparency
		uniform float4 _MaskTransparency_ST;
		uniform half _Opacity;
		uniform half _TransparentThreshold;
	//==


	//== N_F_CO_ON
		uniform half _Cutout;
		uniform half _AlphaBaseCutout;
		uniform half _UseSecondaryCutout;
		
		uniform half _Glow_Edge_Width;
		uniform half4 _Glow_Color;

		uniform float4 _SecondaryCutout_ST;
	//==


	//== N_F_NM_ON
		uniform float4 _NormalMap_ST;

		uniform half _NormalMapIntensity;
	//==


	//== N_F_CA_ON
		uniform half _Saturation;
	//== 


	//== N_F_SL_ON
		uniform half _SelfLitIntensity;
		uniform half4 _SelfLitColor;
		uniform half _SelfLitPower;
		uniform half _TEXMCOLINT;
		uniform half _SelfLitHighContrast;

		uniform float4 _MaskSelfLit_ST;
	//==


	//== N_F_GLO_ON
		uniform half _GlossIntensity;
		uniform half _Glossiness;
		uniform half _GlossSoftness;
		uniform half4 _GlossColor;
		uniform half _GlossColorPower;

		uniform float4 _MaskGloss_ST;
	//==


	//== N_F_GLO_ON -> N_F_GLOT_ON
		uniform float4 _GlossTexture_ST;

		uniform half _GlossTextureSoftness;
		uniform half _PSGLOTEX;
		uniform half _GlossTextureRotate;
		uniform half _GlossTextureFollowObjectRotation;
		uniform half _GlossTextureFollowLight;
	//==


	//== Others
		uniform half4 _OverallShadowColor;
		uniform half _OverallShadowColorPower;

		uniform half _SelfShadowShadowTAtViewDirection;

		uniform half _ShadowHardness;
		uniform half _SelfShadowRealtimeShadowIntensity;
	//==


	//== N_F_SS_ON
		uniform half _SelfShadowThreshold;
		uniform half _VertexColorGreenControlSelfShadowThreshold;
		uniform half _SelfShadowHardness;
		uniform half _LigIgnoYNorDir;
		uniform half _SelfShadowAffectedByLightShadowStrength;
	//==


	//== N_F_SS_ON -> Transparency
		uniform half _SelfShadowIntensity;
		uniform half4 _SelfShadowColor;
		uniform half _SelfShadowColorPower;
	//==


	//== Others
		uniform half4 _SelfShadowRealTimeShadowColor;
		uniform half _SelfShadowRealTimeShadowColorPower;
	//==


	//== N_F_SON_ON
		uniform half _SmoothObjectNormal;
		uniform half _VertexColorRedControlSmoothObjectNormal;
		uniform float4 _XYZPosition;
		uniform half _ShowNormal;
	//==


	//== N_F_SCT_ON
		uniform float4 _ShadowColorTexture_ST;

		uniform half _ShadowColorTexturePower;
	//==


	//== N_F_ST_ON
		uniform half _ShadowTIntensity;

		uniform float4 _ShadowT_ST;

		uniform half _ShadowTLightThreshold;
		uniform half _ShadowTShadowThreshold;
		uniform half4 _ShadowTColor;
		uniform half _ShadowTColorPower;
		uniform half _ShadowTHardness;
		uniform half _STIL;
		uniform half _ShowInAmbientLightShadowIntensity;
		uniform half _ShowInAmbientLightShadowThreshold;
		uniform half _LightFalloffAffectShadowT;
	//==


	//==  N_F_PT_ON
		uniform float4 _PTexture_ST;
		uniform half4 _PTCol;
		uniform half _PTexturePower;
	//==


	//==  N_F_RELGI_ON
		uniform half _GIFlatShade;
		uniform half _GIShadeThreshold;
		uniform half _EnvironmentalLightingIntensity;
	//==
		

	//== Others
		uniform half _LightAffectShadow;
		uniform half _LightIntensity;
		uniform half _DirectionalLightIntensity;
		uniform half _PointSpotlightIntensity;
		uniform half _LightFalloffSoftness;
	//==


	//== N_F_CLD_ON
		uniform half _CustomLightDirectionIntensity;
		uniform half4 _CustomLightDirection;
		uniform half _CustomLightDirectionFollowObjectRotation;
	//==


	//== N_F_R_ON
		uniform half _ReflectionIntensity;
		uniform half _ReflectionRoughtness;
		uniform half _RefMetallic;

		uniform float4 _MaskReflection_ST;
	//==


	//== N_F_FR_ON
		float4 _FReflection_ST;
	//==


	//== N_F_RL_ON
		uniform half _RimLigInt;
		uniform half _RimLightUnfill;
		uniform half _RimLightSoftness;
		uniform half _LightAffectRimLightColor;
		uniform half4 _RimLightColor;
		uniform half _RimLightColorPower;
		uniform half _RimLightInLight;
	//==


	//== N_F_NFD_ON
		uniform half _MinFadDistance;
		uniform half _MaxFadDistance;
	//==


	//== N_F_TP_ON
		uniform float _TriPlaTile;
		uniform float _TriPlaBlend;
	//==


	//== N_F_PA_ON
		uniform half _PresAdju;
		uniform half _ClipAdju;
		uniform float _PASize;
		uniform float _PASmooTrans;
		uniform float _PADist;
	//==


	//== N_F_SE_ON
		uniform float _NoiseSize;
		uniform float _TrailSize;

	//==


	//== Others
		uniform half4 _SSAOColor;

		uniform half _ReduSha;
		uniform sampler3D _DitherMaskLOD;

		float _SkinMatrixIndex;
		float _ComputeMeshIndex;
		float4 _PrevPosition;
		float4 _ObjPosi;
		float3 _ObjectForward;
		float3 _ObjectRight;
	//==

CBUFFER_END

//===============================================================================
//DOTS Instancing
//===============================================================================
#ifdef UNITY_DOTS_INSTANCING_ENABLED

UNITY_DOTS_INSTANCING_START(MaterialPropertyMetadata)

	UNITY_DOTS_INSTANCED_PROP(float4, _MainColor)
	UNITY_DOTS_INSTANCED_PROP(float, _MaiColPo)
	UNITY_DOTS_INSTANCED_PROP(float, _MVCOL)
	UNITY_DOTS_INSTANCED_PROP(float, _MCIALO)
	UNITY_DOTS_INSTANCED_PROP(float, _TexturePatternStyle)
	UNITY_DOTS_INSTANCED_PROP(float4, _HighlightColor)
	UNITY_DOTS_INSTANCED_PROP(float, _HighlightColorPower)

	UNITY_DOTS_INSTANCED_PROP(float, _OutlineWidth)
	UNITY_DOTS_INSTANCED_PROP(int, _OutlineExtrudeMethod)
	UNITY_DOTS_INSTANCED_PROP(float3, _OutlineOffset)
	UNITY_DOTS_INSTANCED_PROP(float, _OutlineZPostionInCamera)
	UNITY_DOTS_INSTANCED_PROP(float4, _OutlineColor)
	UNITY_DOTS_INSTANCED_PROP(float, _MixMainTexToOutline)
	UNITY_DOTS_INSTANCED_PROP(float, _NoisyOutlineIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _DynamicNoisyOutline)
	UNITY_DOTS_INSTANCED_PROP(float, _LightAffectOutlineColor)
	UNITY_DOTS_INSTANCED_PROP(float, _OutlineWidthAffectedByViewDistance)
	UNITY_DOTS_INSTANCED_PROP(float, _FarDistanceMaxWidth)
	UNITY_DOTS_INSTANCED_PROP(float, _VertexColorBlueAffectOutlineWitdh)

	UNITY_DOTS_INSTANCED_PROP(float, _DepthThreshold)

	UNITY_DOTS_INSTANCED_PROP(float, _MCapIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _SPECMODE)
	UNITY_DOTS_INSTANCED_PROP(float, _SPECIN)

	UNITY_DOTS_INSTANCED_PROP(float, _Opacity)
	UNITY_DOTS_INSTANCED_PROP(float, _TransparentThreshold)

	UNITY_DOTS_INSTANCED_PROP(float, _Cutout)
	UNITY_DOTS_INSTANCED_PROP(float, _AlphaBaseCutout)
	UNITY_DOTS_INSTANCED_PROP(float, _UseSecondaryCutout)

	UNITY_DOTS_INSTANCED_PROP(float4, _Glow_Color)
	UNITY_DOTS_INSTANCED_PROP(float, _Glow_Edge_Width)

	UNITY_DOTS_INSTANCED_PROP(float, _NormalMapIntensity)

	UNITY_DOTS_INSTANCED_PROP(float, _Saturation)

	UNITY_DOTS_INSTANCED_PROP(float, _SelfLitIntensity)
	UNITY_DOTS_INSTANCED_PROP(float4, _SelfLitColor)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfLitPower)
	UNITY_DOTS_INSTANCED_PROP(float, _TEXMCOLINT)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfLitHighContrast)

	UNITY_DOTS_INSTANCED_PROP(float, _GlossIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _Glossiness)
	UNITY_DOTS_INSTANCED_PROP(float, _GlossSoftness)
	UNITY_DOTS_INSTANCED_PROP(float4, _GlossColor)
	UNITY_DOTS_INSTANCED_PROP(float, _GlossColorPower)

	UNITY_DOTS_INSTANCED_PROP(float, _GlossTextureSoftness)
	UNITY_DOTS_INSTANCED_PROP(float, _PSGLOTEX)
	UNITY_DOTS_INSTANCED_PROP(float, _GlossTextureRotate)
	UNITY_DOTS_INSTANCED_PROP(float, _GlossTextureFollowObjectRotation)
	UNITY_DOTS_INSTANCED_PROP(float, _GlossTextureFollowLight)

	UNITY_DOTS_INSTANCED_PROP(float4, _OverallShadowColor)
	UNITY_DOTS_INSTANCED_PROP(float, _OverallShadowColorPower)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowShadowTAtViewDirection)
	UNITY_DOTS_INSTANCED_PROP(float, _ShadowHardness)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowRealtimeShadowIntensity)

	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowThreshold)
	UNITY_DOTS_INSTANCED_PROP(float, _VertexColorGreenControlSelfShadowThreshold)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowHardness)
	UNITY_DOTS_INSTANCED_PROP(float, _LigIgnoYNorDir)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowAffectedByLightShadowStrength)

	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowIntensity)
	UNITY_DOTS_INSTANCED_PROP(float4, _SelfShadowColor)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowColorPower)

	UNITY_DOTS_INSTANCED_PROP(float4, _SelfShadowRealTimeShadowColor)
	UNITY_DOTS_INSTANCED_PROP(float, _SelfShadowRealTimeShadowColorPower)

	UNITY_DOTS_INSTANCED_PROP(float, _SmoothObjectNormal)
	UNITY_DOTS_INSTANCED_PROP(float, _VertexColorRedControlSmoothObjectNormal)
	UNITY_DOTS_INSTANCED_PROP(float4, _XYZPosition)
	UNITY_DOTS_INSTANCED_PROP(float, _ShowNormal)

	UNITY_DOTS_INSTANCED_PROP(float, _ShadowColorTexturePower)

	UNITY_DOTS_INSTANCED_PROP(float, _ShadowTIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _ShadowTLightThreshold)
	UNITY_DOTS_INSTANCED_PROP(float, _ShadowTShadowThreshold)
	UNITY_DOTS_INSTANCED_PROP(float4, _ShadowTColor)
	UNITY_DOTS_INSTANCED_PROP(float, _ShadowTColorPower)
	UNITY_DOTS_INSTANCED_PROP(float, _ShadowTHardness)
	UNITY_DOTS_INSTANCED_PROP(float, _STIL)
	UNITY_DOTS_INSTANCED_PROP(float, _ShowInAmbientLightShadowIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _ShowInAmbientLightShadowThreshold)
	UNITY_DOTS_INSTANCED_PROP(float, _LightFalloffAffectShadowT)

	UNITY_DOTS_INSTANCED_PROP(float4, _PTCol)
	UNITY_DOTS_INSTANCED_PROP(float, _PTexturePower)

	UNITY_DOTS_INSTANCED_PROP(float, _GIFlatShade)
	UNITY_DOTS_INSTANCED_PROP(float, _GIShadeThreshold)
	UNITY_DOTS_INSTANCED_PROP(float, _EnvironmentalLightingIntensity)

	UNITY_DOTS_INSTANCED_PROP(float, _LightAffectShadow)
	UNITY_DOTS_INSTANCED_PROP(float, _LightIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _DirectionalLightIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _PointSpotlightIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _LightFalloffSoftness)

	UNITY_DOTS_INSTANCED_PROP(float, _CustomLightDirectionIntensity)
	UNITY_DOTS_INSTANCED_PROP(float4, _CustomLightDirection)
	UNITY_DOTS_INSTANCED_PROP(float, _CustomLightDirectionFollowObjectRotation)

	UNITY_DOTS_INSTANCED_PROP(float, _ReflectionIntensity)
	UNITY_DOTS_INSTANCED_PROP(float, _ReflectionRoughtness)
	UNITY_DOTS_INSTANCED_PROP(float, _RefMetallic)

	UNITY_DOTS_INSTANCED_PROP(float, _RimLigInt)
	UNITY_DOTS_INSTANCED_PROP(float, _RimLightUnfill)
	UNITY_DOTS_INSTANCED_PROP(float, _RimLightSoftness)
	UNITY_DOTS_INSTANCED_PROP(float, _LightAffectRimLightColor)
	UNITY_DOTS_INSTANCED_PROP(float4, _RimLightColor)
	UNITY_DOTS_INSTANCED_PROP(float, _RimLightColorPower)
	UNITY_DOTS_INSTANCED_PROP(float, _RimLightInLight)

	UNITY_DOTS_INSTANCED_PROP(float, _MinFadDistance)
	UNITY_DOTS_INSTANCED_PROP(float, _MaxFadDistance)

	UNITY_DOTS_INSTANCED_PROP(float, _TriPlaTile)
	UNITY_DOTS_INSTANCED_PROP(float, _TriPlaBlend)

	UNITY_DOTS_INSTANCED_PROP(float, _PresAdju)
	UNITY_DOTS_INSTANCED_PROP(float, _ClipAdju)
	UNITY_DOTS_INSTANCED_PROP(float, _PASize)
	UNITY_DOTS_INSTANCED_PROP(float, _PASmooTrans)
	UNITY_DOTS_INSTANCED_PROP(float, _PADist)

	UNITY_DOTS_INSTANCED_PROP(float4, _PrevPosition)
	UNITY_DOTS_INSTANCED_PROP(float4, _ObjPosi)
	UNITY_DOTS_INSTANCED_PROP(float, _NoiseSize)
	UNITY_DOTS_INSTANCED_PROP(float, _TrailSize)

	UNITY_DOTS_INSTANCED_PROP(float3, _ObjectForward);
	UNITY_DOTS_INSTANCED_PROP(float3, _ObjectRight);

	UNITY_DOTS_INSTANCED_PROP(float4, _SSAOColor)

	UNITY_DOTS_INSTANCED_PROP_OVERRIDE_SUPPORTED(float, _SkinMatrixIndex)
	UNITY_DOTS_INSTANCED_PROP_OVERRIDE_SUPPORTED(float, _ComputeMeshIndex)

UNITY_DOTS_INSTANCING_END(MaterialPropertyMetadata)



static float4 unity_DOTS_Sampled_MainColor;
static float unity_DOTS_Sampled_MaiColPo;
static float unity_DOTS_Sampled_MVCOL;
static float unity_DOTS_Sampled_MCIALO;
static float unity_DOTS_Sampled_TexturePatternStyle;
static float4 unity_DOTS_Sampled_HighlightColor;
static float unity_DOTS_Sampled_HighlightColorPower;

static float unity_DOTS_Sampled_OutlineWidth;
static int unity_DOTS_Sampled_OutlineExtrudeMethod;
static float3 unity_DOTS_Sampled_OutlineOffset;
static float unity_DOTS_Sampled_OutlineZPostionInCamera;
static float4 unity_DOTS_Sampled_OutlineColor;
static float unity_DOTS_Sampled_MixMainTexToOutline;
static float unity_DOTS_Sampled_NoisyOutlineIntensity;
static float unity_DOTS_Sampled_DynamicNoisyOutline;
static float unity_DOTS_Sampled_LightAffectOutlineColor;
static float unity_DOTS_Sampled_OutlineWidthAffectedByViewDistance;
static float unity_DOTS_Sampled_FarDistanceMaxWidth;
static float unity_DOTS_Sampled_VertexColorBlueAffectOutlineWitdh;

static float unity_DOTS_Sampled_DepthThreshold;

static float unity_DOTS_Sampled_MCapIntensity;
static float unity_DOTS_Sampled_SPECMODE;
static float unity_DOTS_Sampled_SPECIN;

static float unity_DOTS_Sampled_Opacity;
static float unity_DOTS_Sampled_TransparentThreshold;

static float unity_DOTS_Sampled_Cutout;
static float unity_DOTS_Sampled_AlphaBaseCutout;
static float unity_DOTS_Sampled_UseSecondaryCutout;

static float4 unity_DOTS_Sampled_Glow_Color;
static float unity_DOTS_Sampled_Glow_Edge_Width;

static float unity_DOTS_Sampled_NormalMapIntensity;

static float unity_DOTS_Sampled_Saturation;

static float unity_DOTS_Sampled_SelfLitIntensity;
static float4 unity_DOTS_Sampled_SelfLitColor;
static float unity_DOTS_Sampled_SelfLitPower;
static float unity_DOTS_Sampled_TEXMCOLINT;
static float unity_DOTS_Sampled_SelfLitHighContrast;

static float unity_DOTS_Sampled_GlossIntensity;
static float unity_DOTS_Sampled_Glossiness;
static float unity_DOTS_Sampled_GlossSoftness;
static float4 unity_DOTS_Sampled_GlossColor;
static float unity_DOTS_Sampled_GlossColorPower;

static float unity_DOTS_Sampled_GlossTextureSoftness;
static float unity_DOTS_Sampled_PSGLOTEX;
static float unity_DOTS_Sampled_GlossTextureRotate;
static float unity_DOTS_Sampled_GlossTextureFollowObjectRotation;
static float unity_DOTS_Sampled_GlossTextureFollowLight;

static float4 unity_DOTS_Sampled_OverallShadowColor;
static float unity_DOTS_Sampled_OverallShadowColorPower;
static float unity_DOTS_Sampled_SelfShadowShadowTAtViewDirection;
static float unity_DOTS_Sampled_ShadowHardness;
static float unity_DOTS_Sampled_SelfShadowRealtimeShadowIntensity;

static float unity_DOTS_Sampled_SelfShadowThreshold;
static float unity_DOTS_Sampled_VertexColorGreenControlSelfShadowThreshold;
static float unity_DOTS_Sampled_SelfShadowHardness;
static float unity_DOTS_Sampled_LigIgnoYNorDir;
static float unity_DOTS_Sampled_SelfShadowAffectedByLightShadowStrength;

static float unity_DOTS_Sampled_SelfShadowIntensity;
static float4 unity_DOTS_Sampled_SelfShadowColor;
static float unity_DOTS_Sampled_SelfShadowColorPower;

static float4 unity_DOTS_Sampled_SelfShadowRealTimeShadowColor;
static float unity_DOTS_Sampled_SelfShadowRealTimeShadowColorPower;

static float unity_DOTS_Sampled_SmoothObjectNormal;
static float unity_DOTS_Sampled_VertexColorRedControlSmoothObjectNormal;
static float4 unity_DOTS_Sampled_XYZPosition;
static float unity_DOTS_Sampled_ShowNormal;

static float unity_DOTS_Sampled_ShadowColorTexturePower;

static float unity_DOTS_Sampled_ShadowTIntensity;
static float unity_DOTS_Sampled_ShadowTLightThreshold;
static float unity_DOTS_Sampled_ShadowTShadowThreshold;
static float4 unity_DOTS_Sampled_ShadowTColor;
static float unity_DOTS_Sampled_ShadowTColorPower;
static float unity_DOTS_Sampled_ShadowTHardness;
static float unity_DOTS_Sampled_STIL;
static float unity_DOTS_Sampled_ShowInAmbientLightShadowIntensity;
static float unity_DOTS_Sampled_ShowInAmbientLightShadowThreshold;
static float unity_DOTS_Sampled_LightFalloffAffectShadowT;

static float4 unity_DOTS_Sampled_PTCol;
static float unity_DOTS_Sampled_PTexturePower;

static float unity_DOTS_Sampled_GIFlatShade;
static float unity_DOTS_Sampled_GIShadeThreshold;
static float unity_DOTS_Sampled_EnvironmentalLightingIntensity;

static float unity_DOTS_Sampled_LightAffectShadow;
static float unity_DOTS_Sampled_LightIntensity;
static float unity_DOTS_Sampled_DirectionalLightIntensity;
static float unity_DOTS_Sampled_PointSpotlightIntensity;
static float unity_DOTS_Sampled_LightFalloffSoftness;

static float unity_DOTS_Sampled_CustomLightDirectionIntensity;
static float4 unity_DOTS_Sampled_CustomLightDirection;
static float unity_DOTS_Sampled_CustomLightDirectionFollowObjectRotation;

static float unity_DOTS_Sampled_ReflectionIntensity;
static float unity_DOTS_Sampled_ReflectionRoughtness;
static float unity_DOTS_Sampled_RefMetallic;

static float unity_DOTS_Sampled_RimLigInt;
static float unity_DOTS_Sampled_RimLightUnfill;
static float unity_DOTS_Sampled_RimLightSoftness;
static float unity_DOTS_Sampled_LightAffectRimLightColor;
static float4 unity_DOTS_Sampled_RimLightColor;
static float unity_DOTS_Sampled_RimLightColorPower;
static float unity_DOTS_Sampled_RimLightInLight;

static float unity_DOTS_Sampled_MinFadDistance;
static float unity_DOTS_Sampled_MaxFadDistance;

static float unity_DOTS_Sampled_TriPlaTile;
static float unity_DOTS_Sampled_TriPlaBlend;

static float unity_DOTS_Sampled_PresAdju;
static float unity_DOTS_Sampled_ClipAdju;
static float unity_DOTS_Sampled_PASize;
static float unity_DOTS_Sampled_PASmooTrans;
static float unity_DOTS_Sampled_PADist;

static float4 unity_DOTS_Sampled_PrevPosition;
static float4 unity_DOTS_Sampled_ObjPosi;
static float unity_DOTS_Sampled_NoiseSize;
static float unity_DOTS_Sampled_TrailSize;

static float3 unity_DOTS_Sampled_ObjectForward;
static float3 unity_DOTS_Sampled_ObjectRight;

static float4 unity_DOTS_Sampled_SSAOColor;



void SetupDOTSLitMaterialPropertyCaches()
{
	unity_DOTS_Sampled_MainColor                                            = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _MainColor);
	unity_DOTS_Sampled_MaiColPo                                             = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MaiColPo);
	unity_DOTS_Sampled_MVCOL                                                = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MVCOL);
	unity_DOTS_Sampled_MCIALO                                               = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MCIALO);
	unity_DOTS_Sampled_TexturePatternStyle                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _TexturePatternStyle);
	unity_DOTS_Sampled_HighlightColor                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _HighlightColor);
	unity_DOTS_Sampled_HighlightColorPower                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _HighlightColorPower);

	unity_DOTS_Sampled_OutlineWidth                                         = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _OutlineWidth);
	unity_DOTS_Sampled_OutlineExtrudeMethod                                 = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(int, _OutlineExtrudeMethod);
	unity_DOTS_Sampled_OutlineOffset                                        = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float3, _OutlineOffset);
	unity_DOTS_Sampled_OutlineZPostionInCamera                              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _OutlineZPostionInCamera);
	unity_DOTS_Sampled_OutlineColor                                         = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _OutlineColor);
	unity_DOTS_Sampled_MixMainTexToOutline                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MixMainTexToOutline);
	unity_DOTS_Sampled_NoisyOutlineIntensity                                = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _NoisyOutlineIntensity);
	unity_DOTS_Sampled_DynamicNoisyOutline                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _DynamicNoisyOutline);
	unity_DOTS_Sampled_LightAffectOutlineColor                              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _LightAffectOutlineColor);
	unity_DOTS_Sampled_OutlineWidthAffectedByViewDistance                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _OutlineWidthAffectedByViewDistance);
	unity_DOTS_Sampled_FarDistanceMaxWidth                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _FarDistanceMaxWidth);
	unity_DOTS_Sampled_VertexColorBlueAffectOutlineWitdh                    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _VertexColorBlueAffectOutlineWitdh);

	unity_DOTS_Sampled_DepthThreshold                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _DepthThreshold);

	unity_DOTS_Sampled_MCapIntensity                                        = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MCapIntensity);
	unity_DOTS_Sampled_SPECMODE                                             = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SPECMODE);
	unity_DOTS_Sampled_SPECIN                                               = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SPECIN);

	unity_DOTS_Sampled_Opacity                                              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _Opacity);
	unity_DOTS_Sampled_TransparentThreshold                                 = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _TransparentThreshold);

	unity_DOTS_Sampled_Cutout                                               = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _Cutout);
	unity_DOTS_Sampled_AlphaBaseCutout                                      = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _AlphaBaseCutout);
	unity_DOTS_Sampled_UseSecondaryCutout                                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _UseSecondaryCutout);

	unity_DOTS_Sampled_Glow_Color                                           = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _Glow_Color);
	unity_DOTS_Sampled_Glow_Edge_Width                                      = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _Glow_Edge_Width);

	unity_DOTS_Sampled_NormalMapIntensity                                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _NormalMapIntensity);

	unity_DOTS_Sampled_Saturation                                           = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _Saturation);

	unity_DOTS_Sampled_SelfLitIntensity                                     = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfLitIntensity);
	unity_DOTS_Sampled_SelfLitColor                                         = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _SelfLitColor);
	unity_DOTS_Sampled_SelfLitPower                                         = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfLitPower);
	unity_DOTS_Sampled_TEXMCOLINT                                           = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _TEXMCOLINT);
	unity_DOTS_Sampled_SelfLitHighContrast                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfLitHighContrast);

	unity_DOTS_Sampled_GlossIntensity                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GlossIntensity);
	unity_DOTS_Sampled_Glossiness                                           = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _Glossiness);
	unity_DOTS_Sampled_GlossSoftness                                        = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GlossSoftness);
	unity_DOTS_Sampled_GlossColor                                           = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _GlossColor);
	unity_DOTS_Sampled_GlossColorPower                                      = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GlossColorPower);

	unity_DOTS_Sampled_GlossTextureSoftness                                 = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GlossTextureSoftness);
	unity_DOTS_Sampled_PSGLOTEX                                             = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _PSGLOTEX);
	unity_DOTS_Sampled_GlossTextureRotate                                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GlossTextureRotate);
	unity_DOTS_Sampled_GlossTextureFollowObjectRotation                     = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GlossTextureFollowObjectRotation);
	unity_DOTS_Sampled_GlossTextureFollowLight                              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GlossTextureFollowLight);

	unity_DOTS_Sampled_OverallShadowColor                                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _OverallShadowColor);
	unity_DOTS_Sampled_OverallShadowColorPower                              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _OverallShadowColorPower);
	unity_DOTS_Sampled_SelfShadowShadowTAtViewDirection                     = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowShadowTAtViewDirection);
	unity_DOTS_Sampled_ShadowHardness                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShadowHardness);
	unity_DOTS_Sampled_SelfShadowRealtimeShadowIntensity                    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowRealtimeShadowIntensity);

	unity_DOTS_Sampled_SelfShadowThreshold                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowThreshold);
	unity_DOTS_Sampled_VertexColorGreenControlSelfShadowThreshold           = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _VertexColorGreenControlSelfShadowThreshold);
	unity_DOTS_Sampled_SelfShadowHardness                                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowHardness);
	unity_DOTS_Sampled_LigIgnoYNorDir                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _LigIgnoYNorDir);
	unity_DOTS_Sampled_SelfShadowAffectedByLightShadowStrength              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowAffectedByLightShadowStrength);

	unity_DOTS_Sampled_SelfShadowIntensity                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowIntensity);
	unity_DOTS_Sampled_SelfShadowColor                                      = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _SelfShadowColor);
	unity_DOTS_Sampled_SelfShadowColorPower                                 = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowColorPower);

	unity_DOTS_Sampled_SelfShadowRealTimeShadowColor                        = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _SelfShadowRealTimeShadowColor);
	unity_DOTS_Sampled_SelfShadowRealTimeShadowColorPower                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SelfShadowRealTimeShadowColorPower);

	unity_DOTS_Sampled_SmoothObjectNormal                                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _SmoothObjectNormal);
	unity_DOTS_Sampled_VertexColorRedControlSmoothObjectNormal              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _VertexColorRedControlSmoothObjectNormal);
	unity_DOTS_Sampled_XYZPosition										    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _XYZPosition);
	unity_DOTS_Sampled_ShowNormal										    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShowNormal);

	unity_DOTS_Sampled_ShadowColorTexturePower                              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShadowColorTexturePower);

	unity_DOTS_Sampled_ShadowTIntensity                                     = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShadowTIntensity);
	unity_DOTS_Sampled_ShadowTLightThreshold                                = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShadowTLightThreshold);
	unity_DOTS_Sampled_ShadowTShadowThreshold                               = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShadowTShadowThreshold);
	unity_DOTS_Sampled_ShadowTColor                                         = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _ShadowTColor);
	unity_DOTS_Sampled_ShadowTColorPower                                    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShadowTColorPower);
	unity_DOTS_Sampled_ShadowTHardness                                      = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShadowTHardness);
	unity_DOTS_Sampled_STIL													= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _STIL);
	unity_DOTS_Sampled_ShowInAmbientLightShadowIntensity                    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShowInAmbientLightShadowIntensity);
	unity_DOTS_Sampled_ShowInAmbientLightShadowThreshold                    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ShowInAmbientLightShadowThreshold);
	unity_DOTS_Sampled_LightFalloffAffectShadowT                            = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _LightFalloffAffectShadowT);

	unity_DOTS_Sampled_PTCol                                                = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _PTCol);
	unity_DOTS_Sampled_PTexturePower                                        = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _PTexturePower);

	unity_DOTS_Sampled_GIFlatShade											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GIFlatShade);
	unity_DOTS_Sampled_GIShadeThreshold                                     = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _GIShadeThreshold);
	unity_DOTS_Sampled_EnvironmentalLightingIntensity                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _EnvironmentalLightingIntensity);

	unity_DOTS_Sampled_LightAffectShadow                                    = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _LightAffectShadow);
	unity_DOTS_Sampled_LightIntensity                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _LightIntensity);
	unity_DOTS_Sampled_DirectionalLightIntensity                            = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _DirectionalLightIntensity);
	unity_DOTS_Sampled_PointSpotlightIntensity                              = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _PointSpotlightIntensity);
	unity_DOTS_Sampled_LightFalloffSoftness                                 = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _LightFalloffSoftness);

	unity_DOTS_Sampled_CustomLightDirectionIntensity                        = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _CustomLightDirectionIntensity);
	unity_DOTS_Sampled_CustomLightDirection                                 = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _CustomLightDirection);
	unity_DOTS_Sampled_CustomLightDirectionFollowObjectRotation             = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _CustomLightDirectionFollowObjectRotation);

	unity_DOTS_Sampled_ReflectionIntensity                                  = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ReflectionIntensity);
	unity_DOTS_Sampled_ReflectionRoughtness                                 = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ReflectionRoughtness);
	unity_DOTS_Sampled_RefMetallic                                          = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _RefMetallic);

	unity_DOTS_Sampled_RimLigInt                                            = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _RimLigInt);
	unity_DOTS_Sampled_RimLightUnfill                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _RimLightUnfill);
	unity_DOTS_Sampled_RimLightSoftness                                     = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _RimLightSoftness);
	unity_DOTS_Sampled_LightAffectRimLightColor                             = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _LightAffectRimLightColor);
	unity_DOTS_Sampled_RimLightColor                                        = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _RimLightColor);
	unity_DOTS_Sampled_RimLightColorPower                                   = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _RimLightColorPower);
	unity_DOTS_Sampled_RimLightInLight                                      = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _RimLightInLight);

	unity_DOTS_Sampled_MinFadDistance                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MinFadDistance);
	unity_DOTS_Sampled_MaxFadDistance                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MaxFadDistance);

	unity_DOTS_Sampled_MinFadDistance                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MinFadDistance);
	unity_DOTS_Sampled_MaxFadDistance                                       = UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _MaxFadDistance);

    unity_DOTS_Sampled_TriPlaTile											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _TriPlaTile);
    unity_DOTS_Sampled_TriPlaBlend											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _TriPlaBlend);

    unity_DOTS_Sampled_PresAdju												= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _PresAdju);
    unity_DOTS_Sampled_ClipAdju												= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _ClipAdju);
    unity_DOTS_Sampled_PASize												= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _PASize);
    unity_DOTS_Sampled_PASmooTrans											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _PASmooTrans);
    unity_DOTS_Sampled_PADist												= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _PADist);

    unity_DOTS_Sampled_PrevPosition											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _PrevPosition);
    unity_DOTS_Sampled_ObjPosi												= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _ObjPosi);
    unity_DOTS_Sampled_NoiseSize											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _NoiseSize);
    unity_DOTS_Sampled_TrailSize											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float, _TrailSize);

    unity_DOTS_Sampled_ObjectForward										= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float3, _ObjectForward);
    unity_DOTS_Sampled_ObjectRight											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float3, _ObjectRight);

	unity_DOTS_Sampled_SSAOColor											= UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4, _SSAOColor);

}



#undef UNITY_SETUP_DOTS_MATERIAL_PROPERTY_CACHES
#define UNITY_SETUP_DOTS_MATERIAL_PROPERTY_CACHES() SetupDOTSLitMaterialPropertyCaches()

#define _MainColor										unity_DOTS_Sampled_MainColor
#define _MaiColPo										unity_DOTS_Sampled_MaiColPo
#define _MVCOL											unity_DOTS_Sampled_MVCOL
#define _MCIALO											unity_DOTS_Sampled_MCIALO
#define _TexturePatternStyle							unity_DOTS_Sampled_TexturePatternStyle
#define _HighlightColor									unity_DOTS_Sampled_HighlightColor
#define _HighlightColorPower							unity_DOTS_Sampled_HighlightColorPower

#define _OutlineWidth									unity_DOTS_Sampled_OutlineWidth
#define _OutlineExtrudeMethod							unity_DOTS_Sampled_OutlineExtrudeMethod
#define _OutlineOffset									unity_DOTS_Sampled_OutlineOffset
#define _OutlineZPostionInCamera						unity_DOTS_Sampled_OutlineZPostionInCamera
#define _OutlineColor									unity_DOTS_Sampled_OutlineColor
#define _MixMainTexToOutline							unity_DOTS_Sampled_MixMainTexToOutline
#define _NoisyOutlineIntensity							unity_DOTS_Sampled_NoisyOutlineIntensity
#define _DynamicNoisyOutline							unity_DOTS_Sampled_DynamicNoisyOutline
#define _LightAffectOutlineColor						unity_DOTS_Sampled_LightAffectOutlineColor
#define _OutlineWidthAffectedByViewDistance				unity_DOTS_Sampled_OutlineWidthAffectedByViewDistance
#define _FarDistanceMaxWidth							unity_DOTS_Sampled_FarDistanceMaxWidth
#define _VertexColorBlueAffectOutlineWitdh				unity_DOTS_Sampled_VertexColorBlueAffectOutlineWitdh

#define _DepthThreshold									unity_DOTS_Sampled_DepthThreshold

#define _MCapIntensity									unity_DOTS_Sampled_MCapIntensity
#define _SPECMODE										unity_DOTS_Sampled_SPECMODE
#define _SPECIN											unity_DOTS_Sampled_SPECIN

#define _Opacity										unity_DOTS_Sampled_Opacity
#define _TransparentThreshold							unity_DOTS_Sampled_TransparentThreshold

#define _Cutout											unity_DOTS_Sampled_Cutout
#define _AlphaBaseCutout								unity_DOTS_Sampled_AlphaBaseCutout
#define _UseSecondaryCutout								unity_DOTS_Sampled_UseSecondaryCutout

#define _Glow_Color										unity_DOTS_Sampled_Glow_Color
#define _Glow_Edge_Width								unity_DOTS_Sampled_Glow_Edge_Width

#define _NormalMapIntensity								unity_DOTS_Sampled_NormalMapIntensity

#define _Saturation										unity_DOTS_Sampled_Saturation

#define _SelfLitIntensity								unity_DOTS_Sampled_SelfLitIntensity
#define _SelfLitColor									unity_DOTS_Sampled_SelfLitColor
#define _SelfLitPower									unity_DOTS_Sampled_SelfLitPower
#define _TEXMCOLINT										unity_DOTS_Sampled_TEXMCOLINT
#define _SelfLitHighContrast							unity_DOTS_Sampled_SelfLitHighContrast

#define _GlossIntensity									unity_DOTS_Sampled_GlossIntensity
#define _Glossiness										unity_DOTS_Sampled_Glossiness
#define _GlossSoftness									unity_DOTS_Sampled_GlossSoftness
#define _GlossColor										unity_DOTS_Sampled_GlossColor
#define _GlossColorPower								unity_DOTS_Sampled_GlossColorPower

#define _GlossTextureSoftness							unity_DOTS_Sampled_GlossTextureSoftness
#define _PSGLOTEX										unity_DOTS_Sampled_PSGLOTEX
#define _GlossTextureRotate								unity_DOTS_Sampled_GlossTextureRotate
#define _GlossTextureFollowObjectRotation				unity_DOTS_Sampled_GlossTextureFollowObjectRotation
#define _GlossTextureFollowLight						unity_DOTS_Sampled_GlossTextureFollowLight

#define _OverallShadowColor								unity_DOTS_Sampled_OverallShadowColor
#define _OverallShadowColorPower						unity_DOTS_Sampled_OverallShadowColorPower
#define _SelfShadowShadowTAtViewDirection				unity_DOTS_Sampled_SelfShadowShadowTAtViewDirection
#define _ShadowHardness									unity_DOTS_Sampled_ShadowHardness
#define _SelfShadowRealtimeShadowIntensity				unity_DOTS_Sampled_SelfShadowRealtimeShadowIntensity

#define _SelfShadowThreshold							unity_DOTS_Sampled_SelfShadowThreshold
#define _VertexColorGreenControlSelfShadowThreshold		unity_DOTS_Sampled_VertexColorGreenControlSelfShadowThreshold
#define _SelfShadowHardness								unity_DOTS_Sampled_SelfShadowHardness
#define _LigIgnoYNorDir									unity_DOTS_Sampled_LigIgnoYNorDir
#define _SelfShadowAffectedByLightShadowStrength        unity_DOTS_Sampled_SelfShadowAffectedByLightShadowStrength

#define _SelfShadowIntensity							unity_DOTS_Sampled_SelfShadowIntensity
#define _SelfShadowColor								unity_DOTS_Sampled_SelfShadowColor
#define _SelfShadowColorPower							unity_DOTS_Sampled_SelfShadowColorPower

#define _SelfShadowRealTimeShadowColor					unity_DOTS_Sampled_SelfShadowRealTimeShadowColor
#define _SelfShadowRealTimeShadowColorPower             unity_DOTS_Sampled_SelfShadowRealTimeShadowColorPower

#define _SmoothObjectNormal								unity_DOTS_Sampled_SmoothObjectNormal
#define _VertexColorRedControlSmoothObjectNormal        unity_DOTS_Sampled_VertexColorRedControlSmoothObjectNormal
#define _XYZPosition									unity_DOTS_Sampled_XYZPosition
#define _ShowNormal										unity_DOTS_Sampled_ShowNormal

#define _ShadowColorTexturePower						unity_DOTS_Sampled_ShadowColorTexturePower

#define _ShadowTIntensity								unity_DOTS_Sampled_ShadowTIntensity
#define _ShadowTLightThreshold							unity_DOTS_Sampled_ShadowTLightThreshold
#define _ShadowTShadowThreshold							unity_DOTS_Sampled_ShadowTShadowThreshold
#define _ShadowTColor									unity_DOTS_Sampled_ShadowTColor
#define _ShadowTColorPower								unity_DOTS_Sampled_ShadowTColorPower
#define _ShadowTHardness								unity_DOTS_Sampled_ShadowTHardness
#define _STIL											unity_DOTS_Sampled_STIL
#define _ShowInAmbientLightShadowIntensity              unity_DOTS_Sampled_ShowInAmbientLightShadowIntensity
#define _ShowInAmbientLightShadowThreshold              unity_DOTS_Sampled_ShowInAmbientLightShadowThreshold
#define _LightFalloffAffectShadowT						unity_DOTS_Sampled_LightFalloffAffectShadowT

#define _PTCol											unity_DOTS_Sampled_PTCol
#define _PTexturePower									unity_DOTS_Sampled_PTexturePower

#define _GIFlatShade									unity_DOTS_Sampled_GIFlatShade
#define _GIShadeThreshold								unity_DOTS_Sampled_GIShadeThreshold
#define _EnvironmentalLightingIntensity					unity_DOTS_Sampled_EnvironmentalLightingIntensity

#define _LightAffectShadow								unity_DOTS_Sampled_LightAffectShadow
#define _LightIntensity									unity_DOTS_Sampled_LightIntensity
#define _DirectionalLightIntensity						unity_DOTS_Sampled_DirectionalLightIntensity
#define _PointSpotlightIntensity						unity_DOTS_Sampled_PointSpotlightIntensity
#define _LightFalloffSoftness							unity_DOTS_Sampled_LightFalloffSoftness

#define _CustomLightDirectionIntensity					unity_DOTS_Sampled_CustomLightDirectionIntensity
#define _CustomLightDirection							unity_DOTS_Sampled_CustomLightDirection
#define _CustomLightDirectionFollowObjectRotation       unity_DOTS_Sampled_CustomLightDirectionFollowObjectRotation

#define _ReflectionIntensity							unity_DOTS_Sampled_ReflectionIntensity
#define _ReflectionRoughtness							unity_DOTS_Sampled_ReflectionRoughtness
#define _RefMetallic									unity_DOTS_Sampled_RefMetallic

#define _RimLigInt										unity_DOTS_Sampled_RimLigInt
#define _RimLightUnfill									unity_DOTS_Sampled_RimLightUnfill
#define _RimLightSoftness								unity_DOTS_Sampled_RimLightSoftness
#define _LightAffectRimLightColor						unity_DOTS_Sampled_LightAffectRimLightColor
#define _RimLightColor									unity_DOTS_Sampled_RimLightColor
#define _RimLightColorPower								unity_DOTS_Sampled_RimLightColorPower
#define _RimLightInLight								unity_DOTS_Sampled_RimLightInLight

#define _MinFadDistance									unity_DOTS_Sampled_MinFadDistance
#define _MaxFadDistance									unity_DOTS_Sampled_MaxFadDistance

#define _TriPlaTile                                     unity_DOTS_Sampled_TriPlaTile
#define _TriPlaBlend                                    unity_DOTS_Sampled_TriPlaBlend

#define _PresAdju										unity_DOTS_Sampled_PresAdju
#define _ClipAdju										unity_DOTS_Sampled_ClipAdju
#define _PASize                                         unity_DOTS_Sampled_PASize
#define _PASmooTrans                                    unity_DOTS_Sampled_PASmooTrans
#define _PADist                                         unity_DOTS_Sampled_PADist

#define _PrevPosition									unity_DOTS_Sampled_PrevPosition
#define _ObjPosi										unity_DOTS_Sampled_ObjPosi
#define _NoiseSize										unity_DOTS_Sampled_NoiseSize
#define _TrailSize										unity_DOTS_Sampled_TrailSize

#define _ObjectForward									unity_DOTS_Sampled_ObjectForward
#define _ObjectRight									unity_DOTS_Sampled_ObjectRight

#define _SSAOColor                                      unity_DOTS_Sampled_SSAOColor

//=========



#define UNITY_ACCESS_HYBRID_INSTANCED_PROP(var, type) UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(type, var)
#elif defined(UNITY_INSTANCING_ENABLED)

UNITY_INSTANCING_BUFFER_START(DOTSMESDEF)
	UNITY_DEFINE_INSTANCED_PROP(float, _SkinMatrixIndex)
	UNITY_DEFINE_INSTANCED_PROP(float, _ComputeMeshIndex)
UNITY_INSTANCING_BUFFER_END(DOTSMESDEF)

#define UNITY_ACCESS_HYBRID_INSTANCED_PROP(var, type) UNITY_ACCESS_INSTANCED_PROP(DOTSMESDEF, var)
#else
#define UNITY_ACCESS_HYBRID_INSTANCED_PROP(var, type) var

//=========

#endif

//===============================================================================
//Non CBUF
//===============================================================================

TEXTURE2D(_MainTex);
SAMPLER(sampler_MainTex);

TEXTURE2D(_MaskTransparency);
SAMPLER(sampler_MaskTransparency);

TEXTURE2D(_OutlineWidthControl);
SAMPLER(sampler_OutlineWidthControl);

#if N_F_MC_ON
	TEXTURE2D(_MCap);
	SAMPLER(sampler_MCap);

	TEXTURE2D(_MCapMask);
	SAMPLER(sampler_MCapMask);
#endif

#if N_F_CO_ON
	TEXTURE2D(_SecondaryCutout);
	SAMPLER(sampler_SecondaryCutout);
#endif

#if N_F_NM_ON
	TEXTURE2D(_NormalMap);
	SAMPLER(sampler_NormalMap);
#endif

#if N_F_SL_ON
	TEXTURE2D(_MaskSelfLit);
	SAMPLER(sampler_MaskSelfLit);
#endif

#if N_F_GLO_ON
	TEXTURE2D(_MaskGloss);
	SAMPLER(sampler_MaskGloss);
#endif

#if N_F_GLO_ON
	#if N_F_GLOT_ON
		TEXTURE2D(_GlossTexture);
		SAMPLER(sampler_GlossTexture);
	#endif
#endif

#if N_F_SCT_ON
	TEXTURE2D(_ShadowColorTexture);
	SAMPLER(sampler_ShadowColorTexture);
#endif

#if N_F_ST_ON
	TEXTURE2D(_ShadowT);
	SAMPLER(sampler_ShadowT);
#endif

#if N_F_PT_ON
	TEXTURE2D(_PTexture);
	SAMPLER(sampler_PTexture);
#endif

#if N_F_R_ON
	TEXTURE2D(_MaskReflection);
	SAMPLER(sampler_MaskReflection);
#endif

#if N_F_R_ON
	#if N_F_FR_ON
		TEXTURE2D(_FReflection);
		SAMPLER(sampler_FReflection);
	#endif
#endif