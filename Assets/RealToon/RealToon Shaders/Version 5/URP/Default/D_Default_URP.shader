//RealToon V5.0.10 (URP)
//MJQStudioWorks
//�2024

Shader "Universal Render Pipeline/RealToon/Version 5/Default/Default"
{
    Properties
    {

		[Enum(UnityEngine.Rendering.CullMode)] _Culling("Culling", int) = 2

		[Toggle(N_F_TRANS_ON)] _TRANSMODE("Transparent Mode", Float) = 0.0

        _MainTex ("Texture", 2D) = "white" {}
        [ToggleOff] _TexturePatternStyle ("Texture Pattern Style", Float ) = 0.0
        [HDR] _MainColor ("Main Color", Color) = (1.0,1.0,1.0,1.0)
		_MaiColPo("Main Color Power", Float) = 0.8

		[ToggleOff] _MVCOL ("Mix Vertex Color", Float ) = 0.0

		[ToggleOff] _MCIALO ("Main Color In Ambient Light Only", Float ) = 0.0

		[HDR] _HighlightColor ("Highlight Color", Color) = (1.0,1.0,1.0,1.0)
        _HighlightColorPower ("Highlight Color Power", Float ) = 1.0

		_MCapIntensity ("Intensity", Range(0, 1)) = 1.0
		_MCap ("MatCap", 2D) = "white" {}
		[ToggleOff] _SPECMODE ("Specular Mode", Float ) = 0.0
		_SPECIN ("Specular Power", Float ) = 1
		_MCapMask ("Mask MatCap", 2D) = "white" {}

        _Cutout ("Cutout", Range(0, 1)) = 0.0
		[ToggleOff] _AlphaBaseCutout ("Alpha Base Cutout", Float ) = 1.0
		[Toggle(N_F_SCO_ON)] _N_F_SCO ("Soft Cutout", Float ) = 0.0
        [ToggleOff] _UseSecondaryCutout ("Use Secondary Cutout Only", Float ) = 0.0
        _SecondaryCutout ("Secondary Cutout", 2D) = "white" {}

		[Toggle(N_F_COEDGL_ON)] _N_F_COEDGL("Enable Glow Edge", Float) = 0.0
		[HDR] _Glow_Color("Glow Color", Color) = (1.0,1.0,1.0,1.0)
		_Glow_Edge_Width("Glow Edge Width", Float) = 1.0

		[Toggle(N_F_SIMTRANS_ON)] _SimTrans("Simple Transparency Mode", Float) = 0.0
		_Opacity("Opacity", Range(0, 1)) = 1.0
		_TransparentThreshold("Transparent Threshold", Float) = 0.0

		[Enum(UnityEngine.Rendering.BlendMode)] _BleModSour("Blend - Source", int) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _BleModDest("Blend - Destination", int) = 0

		_MaskTransparency("Mask Transparency", 2D) = "black" {}

		[Toggle(N_F_TRANSAFFSHA_ON)] _TransAffSha("Affect Shadow", Float) = 1.0

        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalMapIntensity ("Normal Map Intensity", Float ) = 1.0

        _Saturation ("Saturation", Range(0, 2)) = 1.0

        _OutlineWidth ("Width", Float ) = 0.5
        _OutlineWidthControl ("Width Control", 2D) = "white" {}

		[Enum(Normal,0,Origin,1)] _OutlineExtrudeMethod("Outline Extrude Method", int) = 0

		_OutlineOffset ("Outline Offset", Vector) = (0,0,0)
		_OutlineZPostionInCamera ("Outline Z Position In Camera", Float) = 0.0

		[Enum(Off,1,On,0)] _DoubleSidedOutline("Double Sided Outline", int) = 1

        [HDR] _OutlineColor ("Color", Color) = (0.0,0.0,0.0,1.0)

		[ToggleOff] _MixMainTexToOutline ("Mix Main Texture To Outline", Float ) = 0.0

        _NoisyOutlineIntensity ("Noisy Outline Intensity", Range(0, 1)) = 0.0
		[Toggle(N_F_DNO_ON)] _DynamicNoisyOutline ("Dynamic Noisy Outline", Float ) = 0.0
        [ToggleOff] _LightAffectOutlineColor ("Light Affect Outline Color", Float ) = 0.0

        [ToggleOff] _OutlineWidthAffectedByViewDistance ("Outline Width Affected By View Distance", Float ) = 0.0
		_FarDistanceMaxWidth ("Far Distance Max Width", Float ) = 10.0

        [ToggleOff] _VertexColorBlueAffectOutlineWitdh ("Vertex Color Blue Affect Outline Witdh", Float ) = 0.0

		_DepthThreshold("Depth Threshold", Float) = 900.0

		[Toggle(N_F_O_MOTTSO_ON)] _N_F_MSSOLTFO("Mix Outline To The Shader Output", Float) = 0.0

        _SelfLitIntensity ("Intensity", Range(0, 1)) = 0.0
        [HDR] _SelfLitColor ("Color", Color) = (1.0,1.0,1.0,1.0)
        _SelfLitPower ("Power", Float ) = 2.0
		_TEXMCOLINT ("Texture and Main Color Intensity", Float ) = 1.0
        [ToggleOff] _SelfLitHighContrast ("High Contrast", Float ) = 1.0
        _MaskSelfLit ("Mask Self Lit", 2D) = "white" {}

        _GlossIntensity ("Gloss Intensity", Range(0, 1)) = 1.0
        _Glossiness ("Glossiness", Range(0, 1)) = 0.6
        _GlossSoftness ("Softness", Range(0, 1)) = 0.0
        [HDR] _GlossColor ("Color", Color) = (1.0,1.0,1.0,1.0)
        _GlossColorPower ("Color Power", Float ) = 10.0
        _MaskGloss ("Mask Gloss", 2D) = "white" {}

        _GlossTexture ("Gloss Texture", 2D) = "black" {}
        _GlossTextureSoftness ("Softness", Float ) = 0.0
		[ToggleOff] _PSGLOTEX ("Pattern Style", Float ) = 0.0
        _GlossTextureRotate ("Rotate", Float ) = 0.0
        [ToggleOff] _GlossTextureFollowObjectRotation ("Follow Object Rotation", Float ) = 0.0
        _GlossTextureFollowLight ("Follow Light", Range(0, 1)) = 0.0

        [HDR] _OverallShadowColor ("Overall Shadow Color", Color) = (0.0,0.0,0.0,1.0)
        _OverallShadowColorPower ("Overall Shadow Color Power", Float ) = 1.0

        [ToggleOff] _SelfShadowShadowTAtViewDirection ("Self Shadow & ShadowT At View Direction", Float ) = 0.0

		_ReduSha ("Reduce Shadow", Float ) = 0.5

		_ShadowHardness ("Shadow Hardness", Range(0, 1)) = 0.0

        _SelfShadowRealtimeShadowIntensity ("Self Shadow & Realtime Shadow Intensity", Range(0, 1)) = 1.0
        _SelfShadowThreshold ("Threshold", Range(0, 1)) = 0.930
        [ToggleOff] _VertexColorGreenControlSelfShadowThreshold ("Vertex Color Green Control Self Shadow Threshold", Float ) = 0.0
        _SelfShadowHardness ("Hardness", Range(0, 1)) = 1.0
        [HDR] _SelfShadowRealTimeShadowColor ("Self Shadow & Real Time Shadow Color", Color) = (1.0,1.0,1.0,1.0)
        _SelfShadowRealTimeShadowColorPower ("Self Shadow & Real Time Shadow Color Power", Float ) = 1.0
		[ToggleOff] _LigIgnoYNorDir ("Light Ignore Y Normal Direction", Float) = 0
		[ToggleOff] _SelfShadowAffectedByLightShadowStrength ("Self Shadow Affected By Light Shadow Strength", Float ) = 0.0

        _SmoothObjectNormal ("Smooth Object Normal", Range(0, 1)) = 0.0
        [ToggleOff] _VertexColorRedControlSmoothObjectNormal ("Vertex Color Red Control Smooth Object Normal", Float ) = 0.0
        [HideInInspector] _XYZPosition ("XYZ Position", Vector) = (0.0,0.0,0.0,0.0)
        [ToggleOff] _ShowNormal ("Show Normal", Float ) = 0.0

        _ShadowColorTexture ("Shadow Color Texture", 2D) = "white" {}
        _ShadowColorTexturePower ("Power", Float ) = 0.0

        _ShadowTIntensity ("ShadowT Intensity", Range(0, 1)) = 1.0
        _ShadowT ("ShadowT", 2D) = "white" {}
        _ShadowTLightThreshold ("Light Threshold", Float ) = 50.0
        _ShadowTShadowThreshold ("Shadow Threshold", Float ) = 0.0
		_ShadowTHardness ("Hardness", Range(0, 1)) = 1.0
        [HDR] _ShadowTColor ("Color", Color) = (1.0,1.0,1.0,1.0)
        _ShadowTColorPower ("Color Power", Float ) = 1.0
		[Toggle(N_F_STSDFM_ON)] _N_F_STSDFM ("SDF Mode", Float ) = 0.0

		[ToggleOff] _STIL ("Ignore Light", Float ) = 0.0

		[Toggle(N_F_STIS_ON)] _N_F_STIS ("Show In Shadow", Float ) = 0.0

		[Toggle(N_F_STIAL_ON )] _N_F_STIAL ("Show In Ambient Light", Float ) = 0.0
        _ShowInAmbientLightShadowIntensity ("Show In Ambient Light & Shadow Intensity", Range(0, 1)) = 1.0
        _ShowInAmbientLightShadowThreshold ("Show In Ambient Light & Shadow Threshold", Float ) = 0.4

        [ToggleOff] _LightFalloffAffectShadowT ("Light Falloff Affect ShadowT", Float ) = 0.0

        _PTexture ("PTexture", 2D) = "white" {}
		_PTCol("Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _PTexturePower ("Power", Float ) = 1.0

		[Toggle(N_F_RELGI_ON)] _RELG ("Receive Environmental Lighting and GI", Float ) = 1.0
        _EnvironmentalLightingIntensity ("Environmental Lighting Intensity", Float ) = 1.0

        [ToggleOff] _GIFlatShade ("GI Flat Shade", Float ) = 0.0
        _GIShadeThreshold ("GI Shade Threshold", Range(0, 1)) = 0.0

        [ToggleOff] _LightAffectShadow ("Light Affect Shadow", Float ) = 0.0
        _LightIntensity ("Light Intensity", Float ) = 1.0

		[Toggle(N_F_USETLB_ON)] _UseTLB ("Use Traditional Light Blend", Float ) = 0.0
		[Toggle(N_F_EAL_ON)] _N_F_EAL ("Enable Additional Lights", Float ) = 1.0

		_DirectionalLightIntensity ("Directional Light Intensity", Float ) = 0.0
		_PointSpotlightIntensity ("Point and Spot Light Intensity", Float ) = 0.0
		_LightFalloffSoftness ("Light Falloff Softness", Range(0, 1)) = 1.0

        _CustomLightDirectionIntensity ("Intensity", Range(0, 1)) = 0.0
        [ToggleOff] _CustomLightDirectionFollowObjectRotation ("Follow Object Rotation", Float ) = 0.0
        _CustomLightDirection ("Custom Light Direction", Vector) = (0.0,0.0,10.0,0.0)

        _ReflectionIntensity ("Intensity", Range(0, 1)) = 0.0
        _ReflectionRoughtness ("Roughness", Float ) = 0.0
		_RefMetallic ("Metallic", Range(0, 1) ) = 0.0

        _MaskReflection ("Mask Reflection", 2D) = "white" {}

        _FReflection ("FReflection", 2D) = "black" {}

		_RimLigInt("Rim Light Intensity", Range(0, 1)) = 1.0
        _RimLightUnfill ("Unfill", Float ) = 1.5
        [HDR] _RimLightColor ("Color", Color) = (1.0,1.0,1.0,1.0)
        _RimLightColorPower ("Color Power", Float ) = 10.0
        _RimLightSoftness ("Softness", Range(0, 1)) = 1.0
        [ToggleOff] _RimLightInLight ("Rim Light In Light", Float ) = 1.0
        [ToggleOff] _LightAffectRimLightColor ("Light Affect Rim Light Color", Float ) = 0.0

		_MinFadDistance("Min Distance", Float) = 0.0
		_MaxFadDistance("Max Distance", Float) = 2.0

		_TriPlaTile("Tile", Float) = 1.0
        _TriPlaBlend("Blend", Float) = 4.0

		_PresAdju("Prespective", Float) = 1.0
		_ClipAdju("Clip", Float) = 0.0
		_PASize("Close-Up Size", Float) = 0.5
		_PASmooTrans("Close-Up Size Smooth Transition", Float) = 1
        _PADist("Close-Up Size Distance", Float) = 0

		[HideInInspector]_NoiseSize("Noise Size", Float) = 100
		[HideInInspector]_TrailSize("Trail Size", Float) = 1.5

		_RefVal ("ID", int ) = 0
        [Enum(Blank,8,A,0,B,2)] _Oper("Set 1", int) = 0
        [Enum(Blank,8,None,4,A,6,B,7)] _Compa("Set 2", int) = 4

		[Toggle(N_F_MC_ON)] _N_F_MC ("MatCap", Float ) = 0.0
		[Toggle(N_F_NM_ON)] _N_F_NM ("Normal Map", Float ) = 0.0
		[Toggle(N_F_CO_ON)] _N_F_CO ("Cutout", Float ) = 0.0
		[Toggle(N_F_O_ON)] _N_F_O ("Outline", Float ) = 1.0
		[Toggle(N_F_CA_ON)] _N_F_CA ("Color Adjustment", Float ) = 0.0
		[Toggle(N_F_SL_ON)] _N_F_SL ("Self Lit", Float ) = 0.0
		[Toggle(N_F_GLO_ON)] _N_F_GLO ("Gloss", Float ) = 0.0
		[Toggle(N_F_GLOT_ON)] _N_F_GLOT ("Gloss Texture", Float ) = 0.0
		[Toggle(N_F_SS_ON)] _N_F_SS ("Self Shadow", Float ) = 1.0
		[Toggle(N_F_SON_ON)] _N_F_SON ("Smooth Object Normal", Float ) = 0.0
		[Toggle(N_F_SCT_ON)] _N_F_SCT ("Shadow Color Texture", Float ) = 0.0
		[Toggle(N_F_ST_ON)] _N_F_ST ("ShadowT", Float ) = 0.0
		[Toggle(N_F_PT_ON)] _N_F_PT ("PTexture", Float ) = 0.0
		[Toggle(N_F_CLD_ON)] _N_F_CLD ("Custom Light Direction", Float ) = 0.0
		[Toggle(N_F_R_ON)] _N_F_R ("Relfection", Float ) = 0.0
		[Toggle(N_F_FR_ON)] _N_F_FR ("FRelfection", Float ) = 0.0
		[Toggle(N_F_RL_ON)] _N_F_RL ("Rim Light", Float ) = 0.0
		[Toggle(N_F_NFD_ON)] _N_F_NFD ("Near Fade Dithering", Float) = 0.0
		[Toggle(N_F_TP_ON)] _N_F_TP ("Triplanar", Float ) = 0.0
		[Toggle(N_F_PA_ON)] _N_F_PA ("Perspective Adjustment", Float ) = 0.0
		[Toggle(N_F_SE_ON)] _N_F_SE ("Smear Effect", Float ) = 0.0

		[Toggle(N_F_HDLS_ON)] _N_F_HDLS ("Hide Directional Light Shadow", Float ) = 0.0
		[Toggle(N_F_HPSS_ON)] _N_F_HPSS ("Hide Point & Spot Light Shadow", Float ) = 0.0

		[Toggle(N_F_DCS_ON)] _N_F_DCS ("Disable Cast Shadow", Float ) = 0.0
		[Toggle(N_F_NLASOBF_ON)] _N_F_NLASOBF ("No Light and Shadow On BackFace", Float ) = 0.0

		[Toggle(N_F_ESSAO_ON)] _N_F_ESSAO("Enable Screen Space Ambient Occlusion", Float) = 0.0
		[HDR] _SSAOColor("Ambient Occlusion Color", Color) = (0.0, 0.0, 0.0, 0.0)

		[Toggle(N_F_RDC_ON)] _N_F_RDC("Receive Decal", Float) = 1.0
		[Toggle(N_F_OFLMB_ON)] _N_F_OFLMB("Optimize for [Light Mode: Baked]", Float) = 0.0

		[Toggle(N_F_DDMD_ON)] _N_F_DDMD("Disable DOTS Mesh Deformation", Float) = 1.0

		[Enum(On, 1, Off, 0)] _ZWrite("ZWrite", int) = 1

		//Others
		[HideInInspector]_SkinMatrixIndex("Skin Matrix Index Offset", Float) = 0
		[HideInInspector]_ComputeMeshIndex("Compute Mesh Buffer Index Offset", Float) = 0
		[HideInInspector]_PrevPosition("Prev Position", Vector) = (0, 0, 0, 0)
		[HideInInspector]_ObjPosi ("Object Position", Vector) = (0, 0, 0, 0)
		[HideInInspector]_ObjectForward("Object Forward", Vector) = (0, 0, 0, 0)
		[HideInInspector]_ObjectRight("Object Right", Vector) = (0, 0, 0, 0)


    }

SubShader
{

    Tags
	{
		"Queue" = "Geometry" 
		"RenderType" = "Opaque"
		"RenderPipeline" = "UniversalPipeline"
		"UniversalMaterialType" = "Lit"
		"IgnoreProjector" = "True"
	}
    LOD 300

	Pass {

Name"Outline"
Tags{"LightMode"="SRPDefaultUnlit"}
//OL_NRE

Cull [_DoubleSidedOutline]//OL_RCUL
Blend[_BleModSour][_BleModDest]

		Stencil {
/*//O_ST
			Ref[_RefVal]
			Comp [_Compa]
			Pass [_Oper]
			Fail [_Oper]
//O_ST_En*/

Pass Invert//O_PI
		}

        HLSLPROGRAM

        #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch
#pragma target 2.0 //targetol

		#pragma multi_compile _ _ADDITIONAL_LIGHTS
		#pragma multi_compile _ _FORWARD_PLUS

		#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
		#pragma multi_compile_fragment _ _LIGHT_LAYERS
        #pragma multi_compile_fog
        #pragma multi_compile_instancing
		#pragma instancing_options renderinglayer

		#pragma multi_compile _ DOTS_INSTANCING_ON
		#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE

        #pragma vertex LitPassVertex
        #pragma fragment LitPassFragment

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		#pragma shader_feature_local_fragment N_F_TRANS_ON
		#pragma shader_feature_local_fragment N_F_SIMTRANS_ON
		#pragma shader_feature_local_fragment N_F_CO_ON
		#pragma shader_feature_local_fragment N_F_EAL_ON
		#pragma shader_feature_local N_F_O_ON
		#pragma shader_feature_local_vertex N_F_DNO_ON
		#pragma shader_feature_local_vertex N_F_DDMD_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON
		#pragma shader_feature_local_fragment N_F_TP_ON
		#pragma shader_feature_local_vertex N_F_PA_ON
		#pragma shader_feature_local_vertex N_F_SE_ON
		#pragma shader_feature_local_fragment N_F_SCO_ON

		struct Attributes
        {

            float4 positionOS   : POSITION;
            float3 normalOS     : NORMAL;
            float2 uv           : TEXCOORD0;
			float4 vertexColor	: COLOR;
			float2 uvLM         : TEXCOORD1;
		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#ifndef	N_F_DDMD_ON
			float4 tangentOS    : TANGENT;
float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_OL
uint4 indices : BLENDINDICES;//DOTS_LiBleSki_OL
//uint vertexID : SV_VertexID;//DOTS_CompDef_OL
		#endif

		#endif
            UNITY_VERTEX_INPUT_INSTANCE_ID

        };

        struct Varyings
        {

            float2 uv                       : TEXCOORD0;
            float4 positionWSAndFogFactor   : TEXCOORD2; 
			float4 projPos					: TEXCOORD7;
			float4 posWorld					: TEXCOORD8;
			float3 normalWS					: TEXCOORD9;
			float4 vertexColor				: COLOR;
            float4 positionCS               : SV_POSITION;
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO

        };


		Varyings LitPassVertex(Attributes input)
        {

			Varyings output = (Varyings)0;

			UNITY_SETUP_INSTANCE_ID (input);
			UNITY_TRANSFER_INSTANCE_ID(input, output);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

            output.uv = input.uv;
            output.vertexColor = input.vertexColor;

		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

			#ifdef	N_F_DDMD_ON

				float4 _LBS_CD_Position = input.positionOS;
				float3 _LBS_CD_Normal = input.normalOS;
				//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed

			#else

				float4 _LBS_CD_Position = 0;
				float3 _LBS_CD_Normal = 0;
				float4 _LBS_CD_Tangent = 0;

DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_OL
//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_OL

			#endif
			
		#else
			float4 _LBS_CD_Position = input.positionOS;
			float3 _LBS_CD_Normal = input.normalOS;
			//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed
		#endif

			VertexPositionInputs vertexInput = GetVertexPositionInputs(_LBS_CD_Position.xyz);

			output.normalWS = TransformObjectToWorldNormal(input.normalOS);
			output.posWorld = float4(vertexInput.positionWS, 1.0);

			float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0.0,0.0,0.0,1.0) );


			//RT_SE
			#if N_F_SE_ON
				input.positionOS = RT_SE(vertexInput.positionWS, input.positionOS);
				_LBS_CD_Position = input.positionOS;
			#endif
			//==


			half RTD_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);

			//**
			half RTD_OL_VCRAOW_OO;
			if (!_VertexColorBlueAffectOutlineWitdh)
			{
				RTD_OL_VCRAOW_OO = _OutlineWidth;
			}
			else
			{
				RTD_OL_VCRAOW_OO = _OutlineWidth * (1.0 - output.vertexColor.b);
			}
			//**


			//**
			half RTD_OL_OLWABVD_OO;
			if (!_OutlineWidthAffectedByViewDistance)
			{
				RTD_OL_OLWABVD_OO = RTD_OL_VCRAOW_OO;
			}
			else
			{
				RTD_OL_OLWABVD_OO = clamp(RTD_OL_VCRAOW_OO * RTD_OB_VP_CAL, RTD_OL_VCRAOW_OO, _FarDistanceMaxWidth);
			}
			//**


			#if defined(SHADER_API_GLES) 
				half4 _OutlineWidthControl_var = tex2Dlod(_OutlineWidthControl, float4(TRANSFORM_TEX(output.uv, _OutlineWidthControl), 0.0, 0)); //replace soon
			#else
				half4 _OutlineWidthControl_var = SAMPLE_TEXTURE2D_LOD(_OutlineWidthControl, sampler_OutlineWidthControl, TRANSFORM_TEX(output.uv, _OutlineWidthControl), 0.0);
			#endif

			#if N_F_DNO_ON

				float4 _3726 = _Time;
				float _8530_ang = _3726.g;
				float _8530_spd = 0.002;
				float _8530_cos = cos(_8530_spd * _8530_ang);
				float _8530_sin = sin(_8530_spd * _8530_ang);
				float2 _8530_piv = float2(0.5, 0.5);
				half2 _8530 = (mul(output.uv - _8530_piv, float2x2(_8530_cos, -_8530_sin, _8530_sin, _8530_cos)) + _8530_piv);

				half2 RTD_OL_DNOL_OO = _8530;

			#else

				half2 RTD_OL_DNOL_OO = output.uv;

			#endif


			half2 _8743 = RTD_OL_DNOL_OO;
            float2 _1283_skew = _8743 + 0.2127+_8743.x*0.3713*_8743.y;
            float2 _1283_rnd = 4.789*sin(489.123*(_1283_skew));
            half _1283 = frac(_1283_rnd.x*_1283_rnd.y*(1+_1283_skew.x));


			//**
			float3 _OEM = float3(0.0, 0.0, 0.0);
			if (!_OutlineExtrudeMethod)
			{
				_OEM = _LBS_CD_Normal;
			}
			else
			{
				_OEM = normalize(_LBS_CD_Position.xyz);
			}
			//**


			half RTD_OL = ( RTD_OL_OLWABVD_OO*0.01 ) * _OutlineWidthControl_var.r * lerp(1.0,_1283,_NoisyOutlineIntensity);


			//RT_PA
			#if N_F_PA_ON
				output.positionCS = mul(RT_PA(vertexInput.positionWS), float4( (_LBS_CD_Position.xyz + _OutlineOffset.xyz * 0.01) + _OEM * RTD_OL, 1.0) );
			#else
				output.positionCS = mul(GetWorldToHClipMatrix(), mul(GetObjectToWorldMatrix(), float4( (_LBS_CD_Position.xyz + _OutlineOffset.xyz * 0.01) + _OEM * RTD_OL,1.0) ) );
			#endif
			//==


			#if defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)
				output.positionCS.z += _OutlineZPostionInCamera * 0.0005;
			#else
				output.positionCS.z -= _OutlineZPostionInCamera * 0.0005;
			#endif

			float fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
			output.positionWSAndFogFactor = float4(vertexInput.positionWS, fogFactor);
			output.projPos = ComputeScreenPos (output.positionCS);

            return output;

        }

        void LitPassFragment(
			Varyings input
		    , out half4 outColor : SV_Target0
		#ifdef _WRITE_RENDERING_LAYERS
			, out float4 outRenderingLayers : SV_Target1
		#endif
		)
        {

			UNITY_SETUP_INSTANCE_ID (input);
			UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

            float3 positionWS = input.positionWSAndFogFactor.xyz;
			float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0.0,0.0,0.0,1.0) );
            float2 sceneUVs = (input.projPos.xy / input.projPos.w);
			half RTD_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);
			half2 RTD_VD_Cal = (float2((sceneUVs.x * 2.0 - 1.0)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2.0 - 1.0).rg*RTD_OB_VP_CAL);

			#ifdef LOD_FADE_CROSSFADE
				LODFadeCrossFade(input.positionCS);
			#endif

			half3 color = (half3)1.0;

			//**
			half2 RTD_TC_TP_OO;
			if (!_TexturePatternStyle)
			{
				RTD_TC_TP_OO = input.uv;
			}
			else
			{
				RTD_TC_TP_OO = RTD_VD_Cal;
			}
			//**


			#ifdef N_F_TP_ON
				half4 _MainTex_var = RT_Tripl_Default(_MainTex, sampler_MainTex, positionWS, input.normalWS);
			#else
				half4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(RTD_TC_TP_OO, _MainTex));
			#endif


			//RT_TRANS_CO
			half RTD_TRAN_OPA_Sli;
			half RTD_CO;
			half3 GLO_OUT;
			RT_TRANS_CO(input.uv, _MainTex_var, RTD_TRAN_OPA_Sli, RTD_CO, positionWS, input.normalWS, input.positionCS.xy, GLO_OUT);

			#if N_F_TRANS_ON
				#ifndef N_F_CO_ON
					clip(RTD_TRAN_OPA_Sli - 0.5);
				#endif
			#endif
			//


			//========/
			//========/
			Light mainLight = GetMainLight();

			#ifndef N_F_OFLMB_ON
				half3 lightColor = mainLight.color.rgb;
			#else
				half3 lightColor = (half3)1.0;
			#endif
			//========/
			//========/


			uint meshRenderingLayers = GetMeshRenderingLayer();


			//========/
			//========/
			#ifndef N_F_OFLMB_ON
				#ifdef _ADDITIONAL_LIGHTS
					#if N_F_EAL_ON

						uint pixelLightCount = GetAdditionalLightsCount();

						#if USE_FORWARD_PLUS

							InputData inputData = (InputData)0;
							inputData.positionWS = positionWS;
							inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);

							for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
							{
								Light light = GetAdditionalLight(lightIndex, input.posWorld.xyz, (float4)1.0);

						#ifdef _LIGHT_LAYERS
								if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
						#endif
								{
									lightColor += light.color * light.distanceAttenuation;
								}
							}
						#endif

						LIGHT_LOOP_BEGIN(pixelLightCount)
							Light light = GetAdditionalLight(lightIndex, positionWS);

							#ifdef _LIGHT_LAYERS
								if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
							#endif
							{
								lightColor += light.color * light.distanceAttenuation;
							}
						LIGHT_LOOP_END

					#endif
				#endif
			#endif
			//========/
			//========/


            float fogFactor = input.positionWSAndFogFactor.w;


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_OutlineColor = float4(LinearToGamma22(_OutlineColor.rgb), _OutlineColor.a);
			#endif
			//


			//**
			half3 RTD_MMTTO_OO;
			if (!_MixMainTexToOutline)
			{
				RTD_MMTTO_OO = _OutlineColor.rgb;
			}
			else
			{

				RTD_MMTTO_OO = _OutlineColor.rgb * _MainTex_var.rgb;
			}
			//**


			//**
			half3 RTD_OL_LAOC_OO;
			if (!_LightAffectOutlineColor)
			{
				RTD_OL_LAOC_OO = RTD_MMTTO_OO;
			}
			else
			{

				RTD_OL_LAOC_OO = lerp(half3(0.0, 0.0, 0.0), RTD_MMTTO_OO, lightColor.rgb);
			}
			//**


			half3 finalRGBA = RTD_OL_LAOC_OO;


			//RT_NFD
			#ifdef N_F_NFD_ON
				RT_NFD(input.positionCS.xy);
			#endif
			//==


			color = MixFog(finalRGBA, fogFactor);

			#if defined(N_F_TRANS_ON) & !defined(N_F_CO_ON)
				outColor = half4(color, RTD_TRAN_OPA_Sli);
			#else
				outColor = half4(color, 1.0);
			#endif

			#ifdef _WRITE_RENDERING_LAYERS
				uint renderingLayers = GetMeshRenderingLayer();
				outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
			#endif

        }

		ENDHLSL
    }

    Pass
    {

        Name "ForwardLit"
        Tags{"LightMode" = "UniversalForward"}

        Cull [_Culling]
		Blend [_BleModSour] [_BleModDest]
		ZWrite[_ZWrite]

/*//F_ST
		Stencil {
            Ref[_RefVal]
            Comp [_Compa]
            Pass [_Oper]
            Fail [_Oper]
        }
//F_ST_En*/

        HLSLPROGRAM

        #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch
#pragma target 2.0 //targetfl

		#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
		#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
		#pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
		#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
		#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
		#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
		#pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH
		#pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
		#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
		#pragma multi_compile_fragment _ _LIGHT_COOKIES
		#pragma multi_compile _ _LIGHT_LAYERS
		#pragma multi_compile _ _FORWARD_PLUS

		#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ProbeVolumeVariants.hlsl"

		#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
		#pragma multi_compile _ SHADOWS_SHADOWMASK
		#pragma multi_compile _ DIRLIGHTMAP_COMBINED
		#pragma multi_compile _ LIGHTMAP_ON
		#pragma multi_compile _ DYNAMICLIGHTMAP_ON
        #pragma multi_compile _ USE_LEGACY_LIGHTMAPS
		#pragma multi_compile _ LOD_FADE_CROSSFADE

        #pragma multi_compile_fog
        #pragma multi_compile_instancing
		#pragma instancing_options renderinglayer

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

        #pragma vertex LitPassVertex
        #pragma fragment LitPassFragment

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		#pragma shader_feature_local_fragment N_F_USETLB_ON
		#pragma shader_feature_local_fragment N_F_TRANS_ON
		#pragma shader_feature_local_fragment N_F_SIMTRANS_ON
		#pragma shader_feature_local_vertex N_F_DDMD_ON

		#pragma shader_feature_local_fragment N_F_O_ON
		#pragma shader_feature_local_fragment N_F_O_MOTTSO_ON
		#pragma shader_feature_local_fragment N_F_MC_ON
		#pragma shader_feature_local_fragment N_F_NM_ON
		#pragma shader_feature_local_fragment N_F_CO_ON
		#pragma shader_feature_local_fragment N_F_SL_ON
		#pragma shader_feature_local_fragment N_F_CA_ON
		#pragma shader_feature_local_fragment N_F_GLO_ON
		#pragma shader_feature_local_fragment N_F_GLOT_ON
		#pragma shader_feature_local_fragment N_F_SS_ON
		#pragma shader_feature_local_fragment N_F_SCT_ON
		#pragma shader_feature_local_fragment N_F_ST_ON
		#pragma shader_feature_local_fragment N_F_STIS_ON
		#pragma shader_feature_local_fragment N_F_STIAL_ON 
		#pragma shader_feature_local N_F_SON_ON
		#pragma shader_feature_local_fragment N_F_PT_ON
		#pragma shader_feature_local_fragment N_F_RELGI_ON
		#pragma shader_feature_local_fragment N_F_CLD_ON
		#pragma shader_feature_local_fragment N_F_R_ON
		#pragma shader_feature_local_fragment N_F_FR_ON
		#pragma shader_feature_local_fragment N_F_RL_ON
		#pragma shader_feature_local_fragment N_F_HDLS_ON
		#pragma shader_feature_local_fragment N_F_HPSS_ON
		#pragma shader_feature_local_fragment N_F_EAL_ON
		#pragma shader_feature_local_fragment N_F_NLASOBF_ON
		#pragma shader_feature_local_fragment N_F_OFLMB_ON
		#pragma shader_feature_local_fragment N_F_ESSAO_ON
		#pragma shader_feature_local_fragment N_F_RDC_ON
		#pragma shader_feature_local_fragment N_F_COEDGL_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON
		#pragma shader_feature_local_fragment N_F_TP_ON
		#pragma shader_feature_local_vertex N_F_PA_ON
		#pragma shader_feature_local_vertex N_F_SE_ON
		#pragma shader_feature_local_fragment N_F_SCO_ON
		#pragma shader_feature_local_fragment N_F_STSDFM_ON

		#define _EMISSION

        struct Attributes
        {

            float4 positionOS   : POSITION;
            float3 normalOS     : NORMAL;
            float4 tangentOS    : TANGENT;
            float2 uv           : TEXCOORD0;
			float2 lightmapUV   : TEXCOORD1;
			float4 vertexColor	: COLOR;
		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#ifndef	N_F_DDMD_ON
float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_FL
uint4 indices : BLENDINDICES;//DOTS_LiBleSki_FL
//uint vertexID : SV_VertexID;//DOTS_CompDef_FL
		#endif

		#endif
            UNITY_VERTEX_INPUT_INSTANCE_ID

        };

        struct Varyings
        {

            float2 uv						: TEXCOORD0;
			float2 lightmapUV				: TEXCOORD1;
            float4 positionWSAndFogFactor   : TEXCOORD2; 
            float3 normalWS                 : TEXCOORD3;
            float3 tangentWS                : TEXCOORD4;
            float3 bitangentWS              : TEXCOORD5;

#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            float4 shadowCoord              : TEXCOORD6; 
#endif
			float4 projPos					: TEXCOORD7;
			float4 posWorld					: TEXCOORD8;
			float3 smoNorm					: TEXCOORD9;
			float4 vertexColor				: COLOR;
            float4 positionCS               : SV_POSITION;
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO

        };

        Varyings LitPassVertex(Attributes input)
        {

            Varyings output = (Varyings)0;

			UNITY_SETUP_INSTANCE_ID (input);
			UNITY_TRANSFER_INSTANCE_ID(input, output);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

			#ifdef	N_F_DDMD_ON

				float4 _LBS_CD_Position = input.positionOS;
				float3 _LBS_CD_Normal = input.normalOS;
				float4 _LBS_CD_Tangent = input.tangentOS;

			#else

				float4 _LBS_CD_Position = 0;
				float3 _LBS_CD_Normal = 0;
				float4 _LBS_CD_Tangent = 0;

DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_FL
//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_FL

			#endif

		#else
			float4 _LBS_CD_Position = input.positionOS;
			float3 _LBS_CD_Normal = input.normalOS;
			float4 _LBS_CD_Tangent = input.tangentOS;
		#endif

            VertexPositionInputs vertexInput = GetVertexPositionInputs(_LBS_CD_Position.xyz);
			VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(_LBS_CD_Normal, _LBS_CD_Tangent);


			//RT_SE
			#if N_F_SE_ON
				input.positionOS = RT_SE(vertexInput.positionWS, input.positionOS);
				_LBS_CD_Position = input.positionOS;
			#endif
			//==


			//RT_PA
			#if N_F_PA_ON
				output.positionCS = mul(RT_PA(vertexInput.positionWS), float4(_LBS_CD_Position.xyz,1.0) );
			#else
				output.positionCS = vertexInput.positionCS;
			#endif
			//
			

			output.uv = input.uv;
            output.vertexColor = input.vertexColor;

            output.normalWS = vertexNormalInput.normalWS;
            output.tangentWS = vertexNormalInput.tangentWS;
            output.bitangentWS = vertexNormalInput.bitangentWS;

			float fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
            output.positionWSAndFogFactor = float4(vertexInput.positionWS, fogFactor);

			output.posWorld = float4(vertexInput.positionWS, 1.0);
			output.projPos = ComputeScreenPos (output.positionCS);

			OUTPUT_LIGHTMAP_UV(input.lightmapUV, unity_LightmapST, output.lightmapUV);


			//RT_SON
			#if N_F_SON_ON
				output.smoNorm = GetAbsolutePositionWS(vertexInput.positionWS);
			#endif
			//==


#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)

            output.shadowCoord = GetShadowCoord(vertexInput);
#endif

            return output;
        }

        void LitPassFragment(Varyings input
							, out half4 outColor : SV_Target0
							, float facing : VFACE
						#ifdef _WRITE_RENDERING_LAYERS
							, out float4 outRenderingLayers : SV_Target1
						#endif
		) 
        {

			UNITY_SETUP_INSTANCE_ID (input);
			UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

			//=========

			float4 shadow_mask = SAMPLE_SHADOWMASK(input.lightmapUV);

			#if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
				half4 shadowMask = shadow_mask;
			#elif !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1.0, 1.0, 1.0, 1.0);
			#endif

			//==========

			half3 color = (half3)0.0;
			float3 A_L_O = (float3)0.0;

			half isFrontFace = ( facing >= 0 ? 1 : 0 );
			float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0.0,0.0,0.0,1.0) );
			float2 sceneUVs = (input.projPos.xy / input.projPos.w);
			half RTD_OB_VP_CAL = distance(objPos.rgb, _WorldSpaceCameraPos);
			half2 RTD_VD_Cal = (float2((sceneUVs.x * 2.0 - 1.0) * (_ScreenParams.r / _ScreenParams.g), sceneUVs.y * 2.0 - 1.0).rg * RTD_OB_VP_CAL);

			float3 positionWS = input.positionWSAndFogFactor.xyz;
			input.normalWS = normalize(input.normalWS);
			float3x3 tangentTransform = float3x3( input.tangentWS, input.bitangentWS, input.normalWS);


			//RT_NM
			float3 normalLocal = RT_NM(input.uv, positionWS, tangentTransform, input.normalWS);
			//==


			float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - input.posWorld.xyz);
			float3 normalDirection = normalize(mul( normalLocal, tangentTransform ));
			float3 viewReflectDirection = reflect( -viewDirection, normalDirection );


			//**
			half2 RTD_TC_TP_OO;
			if (!_TexturePatternStyle)
			{
				RTD_TC_TP_OO = input.uv;
			}
			else
			{
				RTD_TC_TP_OO = RTD_VD_Cal;
			}
			//**


			#ifdef N_F_TP_ON
				half4 _MainTex_var = RT_Tripl_Default(_MainTex, sampler_MainTex, positionWS, normalDirection);
			#else
				half4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(RTD_TC_TP_OO, _MainTex));
			#endif

			#if defined(N_F_RDC_ON)
				#if defined(_DBUFFER) && (!defined(N_F_TRANS_ON) || defined(N_F_CO_ON))
					RT_DC(input.positionCS, _MainTex_var, input.normalWS);
				#endif
			#endif

			#ifdef LOD_FADE_CROSSFADE
				LODFadeCrossFade(input.positionCS);
			#endif


			//**
			half3 _RTD_MVCOL;
			if (!_MVCOL)
			{
				_RTD_MVCOL = (half3)1.0;
			}
			else
			{
				_RTD_MVCOL = input.vertexColor.rgb;
			}

			uint meshRenderingLayers = GetMeshRenderingLayer();
			//**


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_OverallShadowColor = float4(LinearToGamma22(_OverallShadowColor.rgb), _OverallShadowColor.a);
			#endif

			#ifndef N_F_OFLMB_ON
				half3 RTD_OSC = (_OverallShadowColor.rgb*_OverallShadowColorPower);
			#else
				half3 RTD_OSC = (half3)0.0;
			#endif
			//



			//RT_MCAP
			half3 MCapOutP = RT_MCAP(input.uv, normalDirection);
			//==


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_MainColor = float4(LinearToGamma22(_MainColor.rgb),_MainColor.a);
			#endif
			//


			half3 _MC_MCP = (_MainColor.rgb * _MaiColPo);


			//RT_SSAO
			half3 SSAmOc = RT_SSAO(input.positionCS);
			//==


			//RT_MCAP_SUB1
			half3 RTD_TEX_COL;
			half3 RTD_MCIALO_IL = RT_MCAP_SUB1(MCapOutP, _MainTex_var, _RTD_MVCOL, RTD_TEX_COL);
			//==


			//RT_TRANS_CO
			half RTD_TRAN_OPA_Sli;
			half RTD_CO;
			half3 GLO_OUT = (half3)0.0;
			RT_TRANS_CO(input.uv, _MainTex_var, RTD_TRAN_OPA_Sli, RTD_CO, positionWS, normalDirection, input.positionCS.xy, GLO_OUT);
			//==


			//RT_SON
			float3 RTD_SON_CHE_1;
			float3 RTD_SON = RT_SON(input.vertexColor, input.smoNorm, normalDirection, RTD_SON_CHE_1);
			//==


			//RT_RELGI
			float3 RTD_GI_FS_OO = RT_RELGI(RTD_SON);
			//==


			//RT_SCT
			half3 RTD_SCT = RT_SCT(input.uv, positionWS, normalDirection, RTD_MCIALO_IL);
			//==


			//RT_PT
			half3 RTD_PT_COL;
			half RTD_PT = RT_PT(RTD_VD_Cal, RTD_PT_COL);
			//==


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_SelfShadowRealTimeShadowColor = float4(LinearToGamma22(_SelfShadowRealTimeShadowColor.rgb), _SelfShadowRealTimeShadowColor.a);
			#endif

			#ifndef N_F_OFLMB_ON
				half3 ss_col = lerp( RTD_PT_COL, (_SelfShadowRealTimeShadowColor.rgb * _SelfShadowRealTimeShadowColorPower) * RTD_OSC * RTD_SCT, RTD_PT);
			#else
				half3 ss_col = (half3)0.0;
			#endif
			//


			//========/
			//========/
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				Light mainLight = GetMainLight(input.shadowCoord, positionWS, shadowMask);
			#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
				Light mainLight = GetMainLight(TransformWorldToShadowCoord(positionWS), positionWS, shadowMask);
			#else
				Light mainLight = GetMainLight();
			#endif

			half3 mainliCol = 0.0;
			half mainliSAtt = 0.0;
			float3 mainliDir = float3(0.0,0.0,0.0);

			#ifdef _LIGHT_LAYERS
				if (IsMatchingLightLayer(mainLight.layerMask, meshRenderingLayers))
			#endif
				{
					mainliCol = mainLight.color.rgb;
					mainliSAtt = mainLight.shadowAttenuation;
					mainliDir = mainLight.direction;
				}
			//========/
			//========/


			#ifndef N_F_OFLMB_ON
				#if N_F_NLASOBF_ON
					half3 lightColor = lerp( (float3)0.0 , mainliCol,isFrontFace);
				#else
					half3 lightColor = mainliCol;
				#endif
			#else
				half3 lightColor = (half3)0.0;
			#endif



			#ifndef N_F_OFLMB_ON
				#if N_F_HDLS_ON
					half attenuation = 1.0; 
				#else
					half dlshmin = lerp( 0.0, 0.6 ,_ShadowHardness);
					half dlshmax = lerp( 1.0, 0.6 ,_ShadowHardness);

					#if N_F_NLASOBF_ON
						half FB_Check = lerp( 1.0 , mainliSAtt ,isFrontFace);
					#else
						half FB_Check = mainliSAtt;
					#endif

					half attenuation = smoothstep(dlshmin,dlshmax,FB_Check);
				#endif
			#else
				half attenuation = 1.0;
			#endif



			#ifndef N_F_OFLMB_ON
				float3 lightDirection = mainliDir;
				float3 halfDirection = normalize(viewDirection + lightDirection);
			#else
				float3 lightDirection = (float3)0.0;
				float3 halfDirection = (float3)0.0;
			#endif



			//RT_CLD
			float3 RTD_CLD = RT_CLD(lightDirection);
			//==


			#ifndef N_F_OFLMB_ON


				//**
				half3 RTD_ST_SS_AVD_OO;
				if (!_SelfShadowShadowTAtViewDirection)
				{
					RTD_ST_SS_AVD_OO = RTD_CLD;
				}
				else
				{
					RTD_ST_SS_AVD_OO = viewDirection;
				}
				//**


				half RTD_NDOTL = 0.5 * dot(RTD_ST_SS_AVD_OO, float3(RTD_SON.x, RTD_SON.y * (1-_LigIgnoYNorDir), RTD_SON.z)) + 0.5;
				half RTD_LVLC = RTD_LVLC_F(lightColor.rgb);
				half3 lig_col_int = (_LightIntensity * lightColor.rgb);


				//**
				half3 RTD_LAS;
				if (!_LightAffectShadow)
				{
					RTD_LAS = ss_col * RTD_LVLC;
				}
				else
				{
					RTD_LAS = ss_col * lig_col_int;
				}
				//**


			#else
				half3 RTD_ST_SS_AVD_OO = (half3)0.0;
				half RTD_NDOTL = 0.0;
				half RTD_LVLC = 0.0;
				half3 lig_col_int = (half3)0.0;
				half3 RTD_LAS = (half3)0.0;
			#endif


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_HighlightColor = float4(LinearToGamma22(_HighlightColor.rgb), _HighlightColor.a);
			#endif

			#ifndef _N_F_OFLMB
				half3 RTD_HL = (_HighlightColor.rgb*_HighlightColorPower+_DirectionalLightIntensity);
			#else
				half3 RTD_HL = (half3)0.0;
			#endif
			//


			//**
			half3 RTD_MC_SM_TC_OO;
			if (!_SPECMODE)
			{
				RTD_MC_SM_TC_OO = RTD_TEX_COL * _MC_MCP;
			}
			else
			{
				RTD_MC_SM_TC_OO = RTD_TEX_COL + _MC_MCP;
			}
			//**


			//**
			half3 RTD_MCIALO_OO;
			if (!_MCIALO)
			{
				RTD_MCIALO_OO = RTD_TEX_COL;
			}
			else
			{
				RTD_MCIALO_OO = lerp(RTD_MC_SM_TC_OO, _MainTex_var.rgb * MCapOutP * _RTD_MVCOL * 0.7, clamp((RTD_LVLC * 1.0), 0.0, 1.0));
			}
			//**


			half3 RTD_MCIALO = RTD_MCIALO_OO;


			//RT_GLO
			half RTD_GLO;
			half3 RTD_GLO_COL;
			RT_GLO(input.uv, RTD_VD_Cal, halfDirection, normalDirection, viewDirection, positionWS, RTD_GLO, RTD_GLO_COL);
			half3 RTD_GLO_OTHERS = RTD_GLO;
			//**


			//RT_RL
			half3 RTD_RL_LARL_OO;
			half RTD_RL_MAIN;
			half RTD_RL_CHE_1 = RT_RL(viewDirection, normalDirection, lightColor, RTD_RL_LARL_OO, RTD_RL_MAIN);
			//**


			//RT_ST
			half3 RTD_SHAT_COL;
			half RTD_STIAL;
			half RTD_ST_IS;
			half3 RTD_ST_LAF;
			half RTD_ST = RT_ST(input.uv, positionWS, normalDirection, RTD_NDOTL, attenuation, lightDirection, RTD_LVLC, RTD_PT_COL, lig_col_int, RTD_SCT, RTD_OSC, RTD_PT, RTD_SHAT_COL, RTD_STIAL, RTD_ST_IS, RTD_ST_LAF);
			//**


			//RT_SS
			half RTD_SS = RT_SS(input.vertexColor, RTD_NDOTL, attenuation, _MainLightShadowData.x);
			//**


			//RT_RELGI_SUB1
			half ref_int_val;
			half3 RTD_SL_OFF_OTHERS = RT_RELGI_SUB1(input.lightmapUV, RTD_GI_FS_OO, RTD_SHAT_COL, RTD_MCIALO, RTD_STIAL, mainLight, normalDirection, positionWS, viewDirection, input.positionCS.xy );
			//==


			#ifndef N_F_OFLMB_ON
				half3 RTD_R_OFF_OTHERS = lerp( lerp( RTD_ST_LAF, RTD_LAS, RTD_ST_IS) , lerp( RTD_ST_LAF, lerp( lerp( RTD_MCIALO_IL * RTD_HL , RTD_GLO_COL , RTD_GLO_OTHERS) , RTD_RL_LARL_OO , RTD_RL_CHE_1 ) * lightColor.rgb, RTD_ST) , RTD_SS ) ;
			#else
				half3 RTD_R_OFF_OTHERS = (half3)0.0;
			#endif


			//RT_R
			half3 RTD_R = RT_R(input.uv, viewReflectDirection, viewDirection, normalDirection, RTD_TEX_COL, RTD_R_OFF_OTHERS, positionWS);
			//==


			//RT_SL
			half3 RTD_SL_CHE_1;
			half3 RTD_SL = RT_SL(input.uv, positionWS, normalDirection, RTD_SL_OFF_OTHERS, RTD_TEX_COL, RTD_R, RTD_SL_CHE_1);
			//==


			//RT_RL_SUB1
			half3 RTD_RL = RT_RL_SUB1(RTD_SL_CHE_1, RTD_RL_LARL_OO, RTD_RL_MAIN);
			//==


			half3 RTD_CA_OFF_OTHERS = (RTD_RL + RTD_SL);
			half3 main_light_output = RTD_CA_OFF_OTHERS;


			//========/
			//========/
			#ifndef N_F_OFLMB_ON

				#ifdef _ADDITIONAL_LIGHTS

					#if N_F_EAL_ON

						uint pixelLightCount = GetAdditionalLightsCount();

						    #if USE_FORWARD_PLUS

								InputData inputData = (InputData)0;
								inputData.positionWS = positionWS;
								inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);

								for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
								{
									Light light = GetAdditionalLight(lightIndex, input.posWorld.xyz, shadowMask);

								#ifdef _LIGHT_LAYERS
										if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								#endif
										{
											#if N_F_USETLB_ON
												A_L_O += RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex);
											#else
												A_L_O = max(RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex),A_L_O);
											#endif
										}
								}

							#endif

							LIGHT_LOOP_BEGIN(pixelLightCount)

								Light light = GetAdditionalLight(lightIndex, input.posWorld.xyz, shadowMask);

								#ifdef _LIGHT_LAYERS
									if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								#endif
								{
									#if N_F_USETLB_ON
										A_L_O += RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex);
									#else
										A_L_O = max(RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex),A_L_O);
									#endif
								}

							LIGHT_LOOP_END

					#endif

				#endif

			#endif
			//========/
			//========/


			#ifndef N_F_OFLMB_ON
				#if N_F_USETLB_ON
					color = main_light_output + A_L_O;
				#else
					color = max (main_light_output,A_L_O);
				#endif
			#else
				color = main_light_output;
			#endif


			#if N_F_TRANS_ON
				float Trans_Val = 1.0;
				#ifndef N_F_CO_ON
					Trans_Val = RTD_TRAN_OPA_Sli;
				#endif
			#else
				float Trans_Val = 1.0;
			#endif

			//RT_CA
			float3 RTD_CA = RT_CA(color * SSAmOc + GLO_OUT);
			//==

//SSOL_NU
//SSOL
//#ifdef UNITY_COLORSPACE_GAMMA//SSOL
//_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL
//#endif//SSOL
//#if N_F_O_ON//SSOL
//float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL
//#if N_F_O_MOTTSO_ON//SSOL
//float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL
//#else//SSOL
//float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL
//#endif//SSOL
//#else//SSOL
float3 Init_FO=RTD_CA*RTD_SON_CHE_1;
//#endif//SSOL


			//RT_NFD
			#ifdef N_F_NFD_ON
				RT_NFD(input.positionCS.xy);
			#endif
			//==


			float fogFactor = input.positionWSAndFogFactor.w;

			color = MixFog(Init_FO, fogFactor);
			outColor = half4( color , Trans_Val);

			#ifdef _WRITE_RENDERING_LAYERS
				uint renderingLayers = GetMeshRenderingLayer();
				outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
			#endif

        }

        ENDHLSL
			 
    }

    Pass
    {
        Name "ShadowCaster"
        Tags{"LightMode" = "ShadowCaster"}

        Cull [_Culling]
		ZWrite On
		ZTest LEqual
        ColorMask 0

        HLSLPROGRAM
        #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch 
#pragma target 2.0 //targetsc

        #pragma multi_compile_instancing

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl" 

		#pragma shader_feature_local_fragment N_F_TRANS_ON
		#pragma shader_feature_local_fragment N_F_SIMTRANS_ON
		#pragma shader_feature_local_vertex N_F_DDMD_ON

		#pragma shader_feature_local_fragment N_F_TRANSAFFSHA_ON
		#pragma shader_feature_local_fragment N_F_CO_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON
		#pragma shader_feature_local_fragment N_F_TP_ON
		#pragma shader_feature_local_fragment N_F_SCO_ON
		#pragma shader_feature_local_vertex N_F_SE_ON

		#pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW
		#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE

        #pragma vertex ShadowPassVertex
        #pragma fragment ShadowPassFragment

		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		float3 _LightDirection;
		float3 _LightPosition;

		struct Attributes
		{

			float4 positionOS   : POSITION;
			float3 normalOS     : NORMAL;
			float2 texcoord     : TEXCOORD0;
		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#ifndef	N_F_DDMD_ON
			float4 tangentOS    : TANGENT;
float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_SC
uint4 indices : BLENDINDICES;//DOTS_LiBleSki_SC
//uint vertexID : SV_VertexID;//DOTS_CompDef_SC
		#endif

		#endif
			UNITY_VERTEX_INPUT_INSTANCE_ID

		};

		struct Varyings
		{

			float2 uv           : TEXCOORD0;
			float4 projPos		: TEXCOORD1;
			float3 normalWS		: TEXCOORD2;
			float3 positionWS	: TEXCOORD3;
			float4 positionCS   : SV_POSITION;
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO

		};

		float4 GetShadowPositionHClip(Attributes input, float3 positionWS, float3 normalWS)
		{

			#if _CASTING_PUNCTUAL_LIGHT_SHADOW
				float3 lightDirectionWS = normalize(_LightPosition - positionWS);
			#else
				float3 lightDirectionWS = _LightDirection;
			#endif

			float invNdotL = 1.0 - saturate(dot(lightDirectionWS, normalWS));
			float scale = invNdotL * _ShadowBias.y;

			positionWS = lightDirectionWS * _ShadowBias.xxx + positionWS;
			positionWS = normalWS * scale.xxx + positionWS;
			float4 positionCS = TransformWorldToHClip( positionWS );

			#if UNITY_REVERSED_Z
				positionCS.z = min(positionCS.z, UNITY_NEAR_CLIP_VALUE) + -_ReduSha * 0.01;
			#else
				positionCS.z = max(positionCS.z, UNITY_NEAR_CLIP_VALUE) + _ReduSha * 0.01;
			#endif


			return positionCS;

		}

		Varyings ShadowPassVertex(Attributes input)
		{

			Varyings output = (Varyings)0;
			UNITY_SETUP_INSTANCE_ID (input);
			UNITY_TRANSFER_INSTANCE_ID(input, output);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

			output.uv = input.texcoord;

		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

			#ifdef	N_F_DDMD_ON

				float4 _LBS_CD_Position = input.positionOS;
				float3 _LBS_CD_Normal = input.normalOS;
				//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed

			#else

				float4 _LBS_CD_Position = 0;
				float3 _LBS_CD_Normal = 0;
				float4 _LBS_CD_Tangent = 0;

DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_SC
//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_SC

			#endif

		#else
			float4 _LBS_CD_Position = input.positionOS;
			float3 _LBS_CD_Normal = input.normalOS;
			//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed
		#endif

			VertexPositionInputs vertexInput = GetVertexPositionInputs(_LBS_CD_Position.xyz);


			//RT_SE
			#if N_F_SE_ON
				input.positionOS = RT_SE(vertexInput.positionWS, input.positionOS);
				_LBS_CD_Position = input.positionOS;
			#endif
			//==


            //output.positionCS = vertexInput.positionCS; //might remove
			output.positionWS = TransformObjectToWorld(_LBS_CD_Position.xyz);
			output.normalWS = TransformObjectToWorldDir(_LBS_CD_Normal);
            output.projPos = ComputeScreenPos (output.positionCS);
			output.positionCS = GetShadowPositionHClip(input, output.positionWS, output.normalWS);

			return output;

		}

		half4 ShadowPassFragment(Varyings input) : SV_TARGET
		{

			UNITY_SETUP_INSTANCE_ID (input);

			float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0.0,0.0,0.0,1.0) );
            float2 sceneUVs = (input.projPos.xy / input.projPos.w);
			half RTD_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);
			half2 RTD_VD_Cal = (float2((sceneUVs.x * 2.0 - 1.0)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2.0 - 1.0).rg*RTD_OB_VP_CAL);


			//**
			half2 _TexturePatternStyle_var;
			if (!_TexturePatternStyle)
			{
				_TexturePatternStyle_var = input.uv;
			}
			else
			{
				_TexturePatternStyle_var = RTD_VD_Cal;
			}
			//**


			#ifdef N_F_TP_ON
				half4 _MainTex_var = RT_Tripl_Default(_MainTex, sampler_MainTex, input.positionWS, input.normalWS);
			#else
				half4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(_TexturePatternStyle_var, _MainTex));
			#endif


			#if N_F_TRANSAFFSHA_ON


				//RT_TRANS_CO
				half RTD_TRAN_OPA_Sli;
				half RTD_CO;
				half3 GLO_OUT;
				RT_TRANS_CO(input.uv, _MainTex_var, RTD_TRAN_OPA_Sli, RTD_CO, input.positionWS, input.normalWS, input.positionCS.xy, GLO_OUT);

				#if N_F_TRANS_ON
					#ifndef N_F_CO_ON
						float dither = tex3D(_DitherMaskLOD, float3(input.positionCS.xy * 0.25, RTD_TRAN_OPA_Sli * 0.99)).a;
						clip(saturate((0.74 > 0.5 ? (1.0 - (1.0 - 2.0 * (0.74 - 0.5)) * (1.0 - dither)) : (2.0 * 0.74 * dither))) - 0.5);
					#endif
				#endif
				//==


			#endif


			//RT_NFD
			#ifdef N_F_NFD_ON
				RT_NFD(input.positionCS.xy);
			#endif
			//==


			#ifdef LOD_FADE_CROSSFADE
				LODFadeCrossFade(input.positionCS);
			#endif

			return 0;
		}

        ENDHLSL
    }

	Pass
    {

        Name "GBuffer"
		Tags{"LightMode" = "UniversalGBuffer"}

		Cull [_Culling]
		ZWrite[_ZWrite]
		ZTest LEqual

/*//G_ST
		Stencil {
            Ref[_RefVal]
            Comp [_Compa]
            Pass [_Oper]
            Fail [_Oper]
        }
//G_ST_En*/

        HLSLPROGRAM

        #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch
		#pragma target 4.5

		#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
		#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
		#pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
		#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
		#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
		#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
		#pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH
		#pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
		#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
		#pragma multi_compile_fragment _ _LIGHT_COOKIES
		#pragma multi_compile _ _LIGHT_LAYERS
		#pragma multi_compile_fragment _ _RENDER_PASS_ENABLED

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ProbeVolumeVariants.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"

		#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
		#pragma multi_compile _ SHADOWS_SHADOWMASK
		#pragma multi_compile _ DIRLIGHTMAP_COMBINED
		#pragma multi_compile _ LIGHTMAP_ON
		#pragma multi_compile _ DYNAMICLIGHTMAP_ON
        #pragma multi_compile _ USE_LEGACY_LIGHTMAPS
		#pragma multi_compile _ LOD_FADE_CROSSFADE
		#pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT

        #pragma multi_compile_fog
        #pragma multi_compile_instancing
		#pragma instancing_options renderinglayer

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

        #pragma vertex LitPassVertex
        #pragma fragment LitPassFragment

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		#pragma shader_feature_local_fragment N_F_USETLB_ON
		#pragma shader_feature_local_fragment N_F_TRANS_ON
		#pragma shader_feature_local_fragment N_F_SIMTRANS_ON
		#pragma shader_feature_local_vertex N_F_DDMD_ON

		#pragma shader_feature_local_fragment N_F_O_ON
		#pragma shader_feature_local_fragment N_F_O_MOTTSO_ON
		#pragma shader_feature_local_fragment N_F_MC_ON
		#pragma shader_feature_local_fragment N_F_NM_ON
		#pragma shader_feature_local_fragment N_F_CO_ON
		#pragma shader_feature_local_fragment N_F_SL_ON
		#pragma shader_feature_local_fragment N_F_CA_ON
		#pragma shader_feature_local_fragment N_F_GLO_ON
		#pragma shader_feature_local_fragment N_F_GLOT_ON
		#pragma shader_feature_local_fragment N_F_SS_ON
		#pragma shader_feature_local_fragment N_F_SCT_ON
		#pragma shader_feature_local_fragment N_F_ST_ON
		#pragma shader_feature_local_fragment N_F_STIS_ON
		#pragma shader_feature_local_fragment N_F_STIAL_ON 
		#pragma shader_feature_local N_F_SON_ON
		#pragma shader_feature_local_fragment N_F_PT_ON
		#pragma shader_feature_local_fragment N_F_RELGI_ON
		#pragma shader_feature_local_fragment N_F_CLD_ON
		#pragma shader_feature_local_fragment N_F_R_ON
		#pragma shader_feature_local_fragment N_F_FR_ON
		#pragma shader_feature_local_fragment N_F_RL_ON
		#pragma shader_feature_local_fragment N_F_HDLS_ON
		#pragma shader_feature_local_fragment N_F_HPSS_ON
		#pragma shader_feature_local_fragment N_F_EAL_ON
		#pragma shader_feature_local_fragment N_F_NLASOBF_ON
		#pragma shader_feature_local_fragment N_F_OFLMB_ON
		#pragma shader_feature_local_fragment N_F_ESSAO_ON
		#pragma shader_feature_local_fragment N_F_RDC_ON
		#pragma shader_feature_local_fragment N_F_COEDGL_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON
		#pragma shader_feature_local_fragment N_F_TP_ON
		#pragma shader_feature_local_vertex N_F_PA_ON
		#pragma shader_feature_local_vertex N_F_SE_ON
		#pragma shader_feature_local_fragment N_F_SCO_ON
		#pragma shader_feature_local_fragment N_F_STSDFM_ON

		#define _EMISSION

        struct Attributes
        {

            float4 positionOS   : POSITION;
            float3 normalOS     : NORMAL;
            float4 tangentOS    : TANGENT;
            float2 uv           : TEXCOORD0;
			float2 lightmapUV   : TEXCOORD1;
			float4 vertexColor	: COLOR;
		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#ifndef	N_F_DDMD_ON
float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_GB
uint4 indices : BLENDINDICES;//DOTS_LiBleSki_GB
//uint vertexID : SV_VertexID;//DOTS_CompDef_GB
		#endif

		#endif
            UNITY_VERTEX_INPUT_INSTANCE_ID

        };

        struct Varyings
        {

            float2 uv						: TEXCOORD0;
			float2 lightmapUV				: TEXCOORD1;
            float4 positionWSAndFogFactor   : TEXCOORD2; 
            float3 normalWS					: TEXCOORD3;
            float3 tangentWS                : TEXCOORD4;
            float3 bitangentWS              : TEXCOORD5;

#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            float4 shadowCoord              : TEXCOORD6; 
#endif
			float4 projPos					: TEXCOORD7;
			float4 posWorld					: TEXCOORD8;
			float3 smoNorm					: TEXCOORD9;
			float4 vertexColor				: COLOR;
            float4 positionCS               : SV_POSITION;
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO

        };

        Varyings LitPassVertex(Attributes input)
        {

            Varyings output = (Varyings)0;

			UNITY_SETUP_INSTANCE_ID (input);
			UNITY_TRANSFER_INSTANCE_ID(input, output);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

			#ifdef	N_F_DDMD_ON

				float4 _LBS_CD_Position = input.positionOS;
				float3 _LBS_CD_Normal = input.normalOS;
				float4 _LBS_CD_Tangent = input.tangentOS;

			#else

				float4 _LBS_CD_Position = 0;
				float3 _LBS_CD_Normal = 0;
				float4 _LBS_CD_Tangent = 0;

DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_GB
//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_GB

			#endif

		#else
			float4 _LBS_CD_Position = input.positionOS;
			float3 _LBS_CD_Normal = input.normalOS;
			float4 _LBS_CD_Tangent = input.tangentOS;
		#endif

            VertexPositionInputs vertexInput = GetVertexPositionInputs(_LBS_CD_Position.xyz);
			VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(_LBS_CD_Normal, _LBS_CD_Tangent);


			//RT_SE
			#if N_F_SE_ON
				input.positionOS = RT_SE(vertexInput.positionWS, input.positionOS);
				_LBS_CD_Position = input.positionOS;
			#endif
			//==


			//RT_PA
			#if N_F_PA_ON
				output.positionCS = mul(RT_PA(vertexInput.positionWS), float4(_LBS_CD_Position.xyz,1.0) );
			#else
				output.positionCS = vertexInput.positionCS;
			#endif
			//==


			output.uv = input.uv;
            output.vertexColor = input.vertexColor;

			output.normalWS = vertexNormalInput.normalWS;
            output.tangentWS = vertexNormalInput.tangentWS;
            output.bitangentWS = vertexNormalInput.bitangentWS;

			float fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
            output.positionWSAndFogFactor = float4(vertexInput.positionWS, fogFactor);
				
			output.posWorld = float4(vertexInput.positionWS, 1.0);
			output.projPos = ComputeScreenPos (output.positionCS);

			OUTPUT_LIGHTMAP_UV(input.lightmapUV, unity_LightmapST, output.lightmapUV);


			//RT_SON
			#if N_F_SON_ON
				output.smoNorm = GetAbsolutePositionWS(vertexInput.positionWS);
			#endif
			//==


#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)

            output.shadowCoord = GetShadowCoord(vertexInput);
#endif

            return output;
        }

        void LitPassFragment(
							Varyings input
							, out half4 GBuffer0 : SV_Target0
							, out half4 GBuffer1 : SV_Target1
							, out half4 GBuffer2 : SV_Target2
							, out half4 GBuffer3 : SV_Target3
							, out half4 GBuffer4 : SV_Target4
							, out half4 Shadow_Mask : SV_Target6
							, float facing : VFACE
						#ifdef _WRITE_RENDERING_LAYERS
							, out float4 outRenderingLayers : SV_Target5
						#endif
		) 
        {

			UNITY_SETUP_INSTANCE_ID (input);
			UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);


			//RT_SSAO
			half SSAmOc_GOF;
			#if N_F_ESSAO_ON
				SSAmOc_GOF = 1;
			#else
				SSAmOc_GOF = 0;
			#endif
			//==
			

			GBuffer0 = half4(0.0,0.0,0.0,1);
			GBuffer1 = half4(1.0,1.0,1.0,SSAmOc_GOF);
			GBuffer4 = half4(0.0,0.0,0.0,0.0);
			Shadow_Mask = half4(0.0,0.0,0.0,0.0);

			float3 positionWS = input.positionWSAndFogFactor.xyz;

			//=========
			float4 shadow_mask = SAMPLE_SHADOWMASK(input.lightmapUV);

			#if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
				half4 shadowMask = shadow_mask;
				Shadow_Mask = shadowMask;
			#elif !defined (LIGHTMAP_ON)
				half4 shadowMask = unity_ProbesOcclusion;
			#else
				half4 shadowMask = half4(1.0, 1.0, 1.0, 1.0);
				Shadow_Mask = shadowMask;
			#endif
			//==========

			half3 color = (half3)0.0;
			float3 A_L_O = (float3)0.0;
			//float3 baked_GI = (float3)1.0; // Remove

			half isFrontFace = ( facing >= 0 ? 1 : 0 );
			float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0.0,0.0,0.0,1.0) );
			float2 sceneUVs = (input.projPos.xy / input.projPos.w);
			half RTD_OB_VP_CAL = distance(objPos.rgb, _WorldSpaceCameraPos);
			half2 RTD_VD_Cal = (float2((sceneUVs.x * 2.0 - 1.0) * (_ScreenParams.r / _ScreenParams.g), sceneUVs.y * 2.0 - 1.0).rg * RTD_OB_VP_CAL);


			//**
			half2 RTD_TC_TP_OO;
			if (!_TexturePatternStyle)
			{
				RTD_TC_TP_OO = input.uv;
			}
			else
			{
				RTD_TC_TP_OO = RTD_VD_Cal;
			}
			//**


			input.normalWS = normalize(input.normalWS);
			float3x3 tangentTransform = float3x3( input.tangentWS, input.bitangentWS, input.normalWS);


			//RT_NM
			float3 normalLocal = RT_NM(input.uv, positionWS, tangentTransform, input.normalWS);
			//==


			float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - input.posWorld.xyz);
			float3 normalDirection = normalize(mul( normalLocal, tangentTransform ));
			
			#if defined(_GBUFFER_NORMALS_OCT)
				float3 normalWS = normalize(mul( normalLocal, tangentTransform ));
				float2 octNormalWS = PackNormalOctQuadEncode(normalWS);
				float2 remappedOctNormalWS = saturate(octNormalWS * 0.5 + 0.5);
				half3 packedNormalWS = PackFloat2To888(remappedOctNormalWS);
				GBuffer2 = half4(packedNormalWS,0.0);
			#else
				GBuffer2 = half4(normalDirection,0.0);
			#endif

			float3 viewReflectDirection = reflect( -viewDirection, normalDirection );

			#ifdef N_F_TP_ON
				half4 _MainTex_var = RT_Tripl_Default(_MainTex, sampler_MainTex, positionWS, normalDirection);
			#else
				half4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(RTD_TC_TP_OO, _MainTex));
			#endif

			#if defined(N_F_RDC_ON)
				#if defined(_DBUFFER) && (!defined(N_F_TRANS_ON) || defined(N_F_CO_ON))
					RT_DC(input.positionCS, _MainTex_var, input.normalWS);
				#endif
			#endif

			#ifdef LOD_FADE_CROSSFADE
				LODFadeCrossFade(input.positionCS);
			#endif

			half3 _RTD_MVCOL;
			if (!_MVCOL)
			{
				_RTD_MVCOL = (half3)1.0;
			}
			else
			{
				_RTD_MVCOL = input.vertexColor.rgb;
			}

			uint meshRenderingLayers = GetMeshRenderingLayer();


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_OverallShadowColor = float4(LinearToGamma22(_OverallShadowColor.rgb), _OverallShadowColor.a);
			#endif

			#ifndef N_F_OFLMB_ON
				half3 RTD_OSC = (_OverallShadowColor.rgb*_OverallShadowColorPower);
			#else
				half3 RTD_OSC = (half3)0.0;
			#endif
			//


			//RT_MCAP
			half3 MCapOutP = RT_MCAP(input.uv, normalDirection);
			//==


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_MainColor = float4(LinearToGamma22(_MainColor.rgb),_MainColor.a);
			#endif
			//
			half3 _MC_MCP = (_MainColor.rgb * _MaiColPo);


			//RT_MCAP_SUB1
			half3 RTD_TEX_COL;
			half3 RTD_MCIALO_IL = RT_MCAP_SUB1(MCapOutP, _MainTex_var, _RTD_MVCOL, RTD_TEX_COL);
			//==


			//RT_TRANS_CO
			half RTD_TRAN_OPA_Sli;
			half RTD_CO;
			half3 GLO_OUT = (half3)0.0;
			RT_TRANS_CO(input.uv, _MainTex_var, RTD_TRAN_OPA_Sli, RTD_CO, positionWS, normalDirection, input.positionCS.xy, GLO_OUT);
			//==


			//RT_SON
			float3 RTD_SON_CHE_1;
			float3 RTD_SON = RT_SON(input.vertexColor, input.smoNorm, normalDirection, RTD_SON_CHE_1);
			//==


			//RT_RELGI
			float3 RTD_GI_FS_OO = RT_RELGI(RTD_SON);
			//==


			//RT_SCT
			half3 RTD_SCT = RT_SCT(input.uv, positionWS, normalDirection, RTD_MCIALO_IL);
			//==


			//RT_PT
			half3 RTD_PT_COL;
			half RTD_PT = RT_PT(RTD_VD_Cal, RTD_PT_COL);
			//==


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_SelfShadowRealTimeShadowColor = float4(LinearToGamma22(_SelfShadowRealTimeShadowColor.rgb), _SelfShadowRealTimeShadowColor.a);
			#endif

			#ifndef N_F_OFLMB_ON
				half3 ss_col = lerp( RTD_PT_COL, (_SelfShadowRealTimeShadowColor.rgb * _SelfShadowRealTimeShadowColorPower) * RTD_OSC * RTD_SCT, RTD_PT);
			#else
				half3 ss_col = (half3)0.0;
			#endif
			//


			//========/
			//========/
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				Light mainLight = GetMainLight(input.shadowCoord, positionWS, shadowMask);
			#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
				Light mainLight = GetMainLight(TransformWorldToShadowCoord(positionWS), positionWS, shadowMask);
			#else
				Light mainLight = GetMainLight();
			#endif

			half3 mainliCol = 0.0;
			half mainliSAtt = 0.0;
			float3 mainliDir = float3(0.0,0.0,0.0);

			#ifdef _LIGHT_LAYERS
				if (IsMatchingLightLayer(mainLight.layerMask, meshRenderingLayers))
			#endif
				{
					mainliCol = mainLight.color.rgb;
					mainliSAtt = mainLight.shadowAttenuation;
					mainliDir = mainLight.direction;
				}
			//========/
			//========/


			#ifndef N_F_OFLMB_ON
				#if N_F_NLASOBF_ON
					half3 lightColor = lerp( (float3)0.0 , mainliCol,isFrontFace);
				#else
					half3 lightColor = mainliCol;
				#endif
			#else
				half3 lightColor = (half3)0.0;
			#endif



			#ifndef N_F_OFLMB_ON
				#if N_F_HDLS_ON
					half attenuation = 1.0; 
				#else
					half dlshmin = lerp( 0.0, 0.6 ,_ShadowHardness);
					half dlshmax = lerp( 1.0, 0.6 ,_ShadowHardness);

					#if N_F_NLASOBF_ON
						half FB_Check = lerp( 1.0 , mainliSAtt ,isFrontFace);
					#else
						half FB_Check = mainliSAtt;
					#endif

					half attenuation = smoothstep(dlshmin,dlshmax,FB_Check);
				#endif
			#else
				half attenuation = 1.0;
			#endif



			#ifndef N_F_OFLMB_ON
				float3 lightDirection = mainliDir;
				float3 halfDirection = normalize(viewDirection + lightDirection);
			#else
				float3 lightDirection = (float3)0.0;
				float3 halfDirection = (float3)0.0;
			#endif



			//RT_CLD
			float3 RTD_CLD = RT_CLD(lightDirection);
			//==


			#ifndef N_F_OFLMB_ON

				//**
				half3 RTD_ST_SS_AVD_OO;
				if (!_SelfShadowShadowTAtViewDirection)
				{
					RTD_ST_SS_AVD_OO = RTD_CLD;
				}
				else
				{
					RTD_ST_SS_AVD_OO = viewDirection;
				}
				//**


				half RTD_NDOTL = 0.5 * dot(RTD_ST_SS_AVD_OO, float3(RTD_SON.x, RTD_SON.y * (1-_LigIgnoYNorDir), RTD_SON.z)) + 0.5;
				half RTD_LVLC = RTD_LVLC_F(lightColor.rgb);
				half3 lig_col_int = (_LightIntensity * lightColor.rgb);

				//**
				half3 RTD_LAS;
				if (!_LightAffectShadow)
				{
					RTD_LAS = ss_col * RTD_LVLC;
				}
				else
				{
					RTD_LAS = ss_col * lig_col_int;
				}
				//**


			#else
				half3 RTD_ST_SS_AVD_OO = (half3)0.0;
				half RTD_NDOTL = 0.0;
				half RTD_LVLC = 0.0;
				half3 lig_col_int = (half3)0.0;
				half3 RTD_LAS = (half3)0.0;
			#endif


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_HighlightColor = float4(LinearToGamma22(_HighlightColor.rgb), _HighlightColor.a);
			#endif

			#ifndef _N_F_OFLMB
				half3 RTD_HL = (_HighlightColor.rgb*_HighlightColorPower+_DirectionalLightIntensity);
			#else
				half3 RTD_HL = (half3)0.0;
			#endif
			//


			//**
			half3 RTD_MC_SM_TC_OO;
			if (!_SPECMODE)
			{
				RTD_MC_SM_TC_OO = RTD_TEX_COL * _MC_MCP;
			}
			else
			{
				RTD_MC_SM_TC_OO = RTD_TEX_COL + _MC_MCP;
			}
			//**


			//**
			half3 RTD_MCIALO_OO;
			if (!_MCIALO)
			{
				RTD_MCIALO_OO = RTD_TEX_COL;
			}
			else
			{
				RTD_MCIALO_OO = lerp(RTD_MC_SM_TC_OO, _MainTex_var.rgb * MCapOutP * _RTD_MVCOL * 0.7, clamp((RTD_LVLC * 1.0), 0.0, 1.0));
			}
			//**


			half3 RTD_MCIALO = RTD_MCIALO_OO;


			//RT_GLO
			half RTD_GLO;
			half3 RTD_GLO_COL;
			RT_GLO(input.uv, RTD_VD_Cal, halfDirection, normalDirection, viewDirection, positionWS, RTD_GLO, RTD_GLO_COL);
			half3 RTD_GLO_OTHERS = RTD_GLO;
			//==


			//RT_RL
			half3 RTD_RL_LARL_OO;
			half RTD_RL_MAIN;
			half RTD_RL_CHE_1 = RT_RL(viewDirection, normalDirection, lightColor, RTD_RL_LARL_OO, RTD_RL_MAIN);
			//==


			//RT_ST
			half3 RTD_SHAT_COL;
			half RTD_STIAL;
			half RTD_ST_IS;
			half3 RTD_ST_LAF;
			half RTD_ST = RT_ST(input.uv, positionWS, normalDirection, RTD_NDOTL, attenuation, lightDirection, RTD_LVLC, RTD_PT_COL, lig_col_int, RTD_SCT, RTD_OSC, RTD_PT, RTD_SHAT_COL, RTD_STIAL, RTD_ST_IS, RTD_ST_LAF);
			//==


			//RT_SS
			half RTD_SS = RT_SS(input.vertexColor, RTD_NDOTL, attenuation, _MainLightShadowData.x);
			//==


			//RT_RELGI_SUB1
			half ref_int_val;
			half3 RTD_SL_OFF_OTHERS = RT_RELGI_SUB1(input.lightmapUV, RTD_GI_FS_OO, RTD_SHAT_COL, RTD_MCIALO, RTD_STIAL, mainLight, normalDirection, positionWS, viewDirection, input.positionCS.xy );
			//==


			#ifndef N_F_OFLMB_ON
				half3 RTD_R_OFF_OTHERS = lerp( lerp( RTD_ST_LAF, RTD_LAS, RTD_ST_IS) , lerp( RTD_ST_LAF, lerp( lerp( RTD_MCIALO_IL * RTD_HL , RTD_GLO_COL , RTD_GLO_OTHERS) , RTD_RL_LARL_OO , RTD_RL_CHE_1 ) * lightColor.rgb, RTD_ST) , RTD_SS ) ;
			#else
				half3 RTD_R_OFF_OTHERS = (half3)0.0;
			#endif



			//RT_R
			half3 RTD_R = RT_R(input.uv, viewReflectDirection, viewDirection, normalDirection, RTD_TEX_COL, RTD_R_OFF_OTHERS, positionWS);
			//==


			//RT_SL
			half3 RTD_SL_CHE_1;
			half3 RTD_SL = RT_SL(input.uv, positionWS, normalDirection, RTD_SL_OFF_OTHERS, RTD_TEX_COL, RTD_R, RTD_SL_CHE_1);
			//==


			//RT_RL_SUB1
			half3 RTD_RL = RT_RL_SUB1(RTD_SL_CHE_1, RTD_RL_LARL_OO, RTD_RL_MAIN);
			//==


			half3 RTD_CA_OFF_OTHERS = (RTD_RL + RTD_SL);
			half3 main_light_output = RTD_CA_OFF_OTHERS;


			//========/
			//========/
			#ifndef N_F_OFLMB_ON

				#ifdef _ADDITIONAL_LIGHTS

					#if N_F_EAL_ON

						uint pixelLightCount = GetAdditionalLightsCount();

						    #if USE_FORWARD_PLUS

								InputData inputData = (InputData)0;
								inputData.positionWS = positionWS;
								inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);

								for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
								{
									Light light = GetAdditionalLight(lightIndex, input.posWorld.xyz, shadowMask);

							#ifdef _LIGHT_LAYERS
									if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
							#endif
									{
										#if N_F_USETLB_ON
											A_L_O += RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex);
										#else
											A_L_O = max(RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex),A_L_O);
										#endif
									}
								}
								#endif

							LIGHT_LOOP_BEGIN(pixelLightCount)
								Light light = GetAdditionalLight(lightIndex, input.posWorld.xyz, shadowMask);

								#ifdef _LIGHT_LAYERS
									if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								#endif
								{
									#if N_F_USETLB_ON
										A_L_O += RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex);
									#else
										A_L_O = max(RT_ADD_LI(light, viewDirection, viewReflectDirection, positionWS, ss_col, RTD_TEX_COL, _MC_MCP, _MainTex_var, MCapOutP, _RTD_MVCOL, RTD_VD_Cal, normalDirection, RTD_SON, RTD_PT_COL, RTD_SCT, RTD_OSC, RTD_PT, RTD_MCIALO_IL, input.uv, input.vertexColor, isFrontFace, lightIndex),A_L_O);
									#endif
								}
							LIGHT_LOOP_END

					#endif

				#endif

			#endif
			//========/
			//========/


			#ifndef N_F_OFLMB_ON
				#if N_F_USETLB_ON
					color = main_light_output + A_L_O;
				#else
					color = max (main_light_output,A_L_O);
				#endif
			#else
				color = main_light_output;
			#endif


			#if N_F_TRANS_ON
				float Trans_Val = 1.0;
				#ifndef N_F_CO_ON
					Trans_Val = RTD_TRAN_OPA_Sli;
				#endif
			#else
				float Trans_Val = 1.0;
			#endif

			//RT_CA
			float3 RTD_CA = RT_CA(color + GLO_OUT);

//SSOL_NU
//SSOL
//#ifdef UNITY_COLORSPACE_GAMMA//SSOL
//_OutlineColor=float4(LinearToGamma22(_OutlineColor.rgb),_OutlineColor.a);//SSOL
//#endif//SSOL
//#if N_F_O_ON//SSOL
//float3 SSOLi=(float3)EdgDet(sceneUVs.xy);//SSOL
//#if N_F_O_MOTTSO_ON//SSOL
//float3 Init_FO=((RTD_CA*RTD_SON_CHE_1))*lerp((float3)1.0,_OutlineColor.rgb,SSOLi);//SSOL
//#else//SSOL
//float3 Init_FO=lerp((RTD_CA*RTD_SON_CHE_1),_OutlineColor.rgb,SSOLi);//SSOL
//#endif//SSOL
//#else//SSOL
float3 Init_FO=RTD_CA*RTD_SON_CHE_1;
//#endif//SSOL


			//RT_NFD
			#ifdef N_F_NFD_ON
				RT_NFD(input.positionCS.xy);
			#endif
			//==


			float fogFactor = input.positionWSAndFogFactor.w;

			color = MixFog(Init_FO, fogFactor);
			GBuffer3 = half4(color, Trans_Val);

			#ifdef _WRITE_RENDERING_LAYERS
				uint renderingLayers = GetMeshRenderingLayer();
				outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
			#endif

			#if _RENDER_PASS_ENABLED
				GBuffer4 = input.positionCS.z;
			#endif

        }

        ENDHLSL

    }

	Pass
    {
        Name "DepthOnly"
        Tags{"LightMode" = "DepthOnly"}

        Cull [_Culling]
		ZWrite On
        ColorMask R

        HLSLPROGRAM

        #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch 
#pragma target 2.0 //targetdo

        #pragma vertex DepthOnlyVertex
        #pragma fragment DepthOnlyFragment

        #pragma multi_compile_instancing

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

		#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE

		#pragma shader_feature_local_vertex N_F_DDMD_ON
		#pragma shader_feature_local_vertex N_F_PA_ON
		#pragma shader_feature_local_vertex N_F_SE_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON

		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		struct Attributes
		{
			float4 position     : POSITION;
			float2 texcoord     : TEXCOORD0;
		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#ifndef	N_F_DDMD_ON
			float3 normalOS     : NORMAL;
			float4 tangentOS    : TANGENT;
float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DO
uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DO
//uint vertexID : SV_VertexID;//DOTS_CompDef_DO
		#endif

		#endif
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct Varyings
		{
			float2 uv           : TEXCOORD0;
			float4 projPos		: TEXCOORD1;
			float3 positionWS	: TEXCOORD2;
			float4 positionCS   : SV_POSITION;
			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO
		};

		Varyings DepthOnlyVertex(Attributes input)
		{
			Varyings output = (Varyings)0;
			UNITY_SETUP_INSTANCE_ID(input);
			UNITY_TRANSFER_INSTANCE_ID(input, output);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

			#ifdef	N_F_DDMD_ON

				float4 _LBS_CD_Position = input.position;
				//float3 _LBS_CD_Normal = input.normalOS; //not currently needed
				//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed

			#else

				float4 _LBS_CD_Position = 0;
				float3 _LBS_CD_Normal = 0;
				float4 _LBS_CD_Tangent = 0;

DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DO
//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DO

			#endif

		#else
			float4 _LBS_CD_Position = input.position;
			//float3 _LBS_CD_Normal = input.normalOS; //not currently needed
			//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed
		#endif
			
		    VertexPositionInputs vertexInput = GetVertexPositionInputs(_LBS_CD_Position.xyz);
			output.positionWS = TransformObjectToWorld(_LBS_CD_Position.xyz);

			//RT_SE
			#if N_F_SE_ON
				input.position = RT_SE(output.positionWS, input.position);
				_LBS_CD_Position = input.position;
			#endif
			//==


			//RT_PA
			#if N_F_PA_ON
				output.positionCS = mul(RT_PA(output.positionWS), float4(_LBS_CD_Position.xyz,1.0) );
			#else
				output.positionCS = TransformObjectToHClip(_LBS_CD_Position.xyz);
			#endif
			//


			return output;
		}

		half4 DepthOnlyFragment(Varyings input) : SV_TARGET
		{
			UNITY_SETUP_INSTANCE_ID(input);
			UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);


			//RT_NFD
			#ifdef N_F_NFD_ON
				RT_NFD(input.positionCS.xy);
			#endif
			//==


			#ifdef LOD_FADE_CROSSFADE
				LODFadeCrossFade(input.positionCS);
			#endif

			return input.positionCS.z;
		}


        ENDHLSL
    }

    Pass
    {
        Name "DepthNormals"
        Tags{"LightMode" = "DepthNormals"}

        ZWrite On
        Cull[_Culling]

        HLSLPROGRAM

		#pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch 
#pragma target 2.0 //targetdn

        #pragma vertex DepthNormalsVertex
        #pragma fragment DepthNormalsFragment


        #pragma shader_feature_local _NORMALMAP
		#pragma shader_feature_local _PARALLAXMAP
		#pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED
        #pragma shader_feature_local_fragment _ALPHATEST_ON
        #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

        #pragma multi_compile_instancing

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
		#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE

		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"

		#pragma shader_feature_local_fragment N_F_TRANS_ON
		#pragma shader_feature_local_fragment N_F_SIMTRANS_ON

		#pragma shader_feature_local_fragment N_F_TRANSAFFSHA_ON
		#pragma shader_feature_local_fragment N_F_CO_ON

		#pragma shader_feature_local_vertex N_F_DDMD_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON
		#pragma shader_feature_local_fragment N_F_TP_ON
		#pragma shader_feature_local_vertex N_F_PA_ON
		#pragma shader_feature_local_vertex N_F_SE_ON
		#pragma shader_feature_local_fragment N_F_SCO_ON

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		struct Attributes
		{
			float4 positionOS   : POSITION;
			float3 normal       : NORMAL;
			float4 tangentOS    : TANGENT;
			float2 texcoord     : TEXCOORD0;
		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#ifndef	N_F_DDMD_ON
float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_DN
uint4 indices : BLENDINDICES;//DOTS_LiBleSki_DN
//uint vertexID : SV_VertexID;//DOTS_CompDef_DN
		#endif

		#endif
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct Varyings
		{
			float4 positionCS   : SV_POSITION;
			float2 uv           : TEXCOORD1;
			float3 normalWS     : TEXCOORD2;
			float3 positionWS	: TEXCOORD3;
			float4 projPos		: TEXCOORD4;
			//float3 tangentWS    : TEXCOORD5;
			//float3 bitangentWS  : TEXCOORD6;

			UNITY_VERTEX_INPUT_INSTANCE_ID
			UNITY_VERTEX_OUTPUT_STEREO
		};

		Varyings DepthNormalsVertex(Attributes input)
		{
			Varyings output = (Varyings)0;
			UNITY_SETUP_INSTANCE_ID(input);
			UNITY_TRANSFER_INSTANCE_ID(input, output);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

		#if defined(UNITY_DOTS_INSTANCING_ENABLED)

			#ifdef	N_F_DDMD_ON

				float4 _LBS_CD_Position = input.positionOS;
				float3 _LBS_CD_Normal = input.normal;
				float4 _LBS_CD_Tangent = input.tangentOS;

			#else

				float4 _LBS_CD_Position = 0;
				float3 _LBS_CD_Normal = 0;
				float4 _LBS_CD_Tangent = 0;

DOTS_LiBleSki(input.indices, input.weights, input.positionOS.xyz, input.normal.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_DN
//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_DN

			#endif

		#else
			float4 _LBS_CD_Position = input.positionOS;
			float3 _LBS_CD_Normal = input.normal;
			float4 _LBS_CD_Tangent = input.tangentOS;
		#endif

			VertexPositionInputs vertexInput = GetVertexPositionInputs(_LBS_CD_Position.xyz);
			VertexNormalInputs normalInput = GetVertexNormalInputs(_LBS_CD_Normal, _LBS_CD_Tangent);
			
			output.positionWS = TransformObjectToWorld(_LBS_CD_Position.xyz);
			output.normalWS	  = NormalizeNormalPerVertex(normalInput.normalWS);
			//output.tangentWS = normalInput.tangentWS;
			//output.bitangentWS = normalInput.bitangentWS;
			output.uv = TRANSFORM_TEX(input.texcoord, _MainTex);


			//RT_SE
			#if N_F_SE_ON
				input.positionOS = RT_SE(output.positionWS, input.positionOS);
				_LBS_CD_Position = input.positionOS;
			#endif
			//==


			//RT_PA
			#if N_F_PA_ON
				output.positionCS = mul(RT_PA(output.positionWS), float4(_LBS_CD_Position.xyz,1.0) );
			#else
				output.positionCS = TransformObjectToHClip(_LBS_CD_Position.xyz);
			#endif
			//==


			return output;
		}

		void DepthNormalsFragment(
								Varyings input
								, out half4 outNormalWS : SV_Target0
							#ifdef _WRITE_RENDERING_LAYERS
								, out float4 outRenderingLayers : SV_Target1
							#endif
		)
		{
			UNITY_SETUP_INSTANCE_ID(input);
			UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

			float4 objPos = mul ( GetObjectToWorldMatrix(), float4(0.0,0.0,0.0,1.0) );
            float2 sceneUVs = (input.projPos.xy / input.projPos.w);
			half RTD_OB_VP_CAL = distance(objPos.rgb,_WorldSpaceCameraPos);
			half2 RTD_VD_Cal = (float2((sceneUVs.x * 2.0 - 1.0)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2.0 - 1.0).rg*RTD_OB_VP_CAL);
			//float3x3 tangentTransform = float3x3( input.tangentWS, input.bitangentWS, input.normalWS);


			//**
			half2 _TexturePatternStyle_var;
			if (!_TexturePatternStyle)
			{
				_TexturePatternStyle_var = input.uv;
			}
			else
			{
				_TexturePatternStyle_var = RTD_VD_Cal;
			}
			//**


			#ifdef N_F_TP_ON
				half4 _MainTex_var = RT_Tripl_Default(_MainTex, sampler_MainTex, input.positionWS, input.normalWS);
			#else
				half4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(_TexturePatternStyle_var, _MainTex));
			#endif

			#if N_F_TRANSAFFSHA_ON


				//RT_TRANS_CO
				half RTD_TRAN_OPA_Sli;
				half RTD_CO;
				half3 GLO_OUT;
				RT_TRANS_CO(input.uv, _MainTex_var, RTD_TRAN_OPA_Sli, RTD_CO, input.positionWS, input.normalWS, input.positionCS.xy, GLO_OUT);

				#if N_F_TRANS_ON
					#ifndef N_F_CO_ON
						float dither = tex3D(_DitherMaskLOD, float3(input.positionCS.xy * 0.25, RTD_TRAN_OPA_Sli * 0.99)).a;
						clip(saturate((0.74 > 0.5 ? (1.0 - (1.0 - 2.0 * (0.74 - 0.5)) * (1.0 - dither)) : (2.0 * 0.74 * dither))) - 0.5);
					#endif
				#endif
				//


			#endif


			//RT_NFD
			#ifdef N_F_NFD_ON
				RT_NFD(input.positionCS.xy);
			#endif
			//==


			//RT_NM
			//float3 normalLocal = RT_NM(input.uv, input.positionWS, tangentTransform, input.normalWS);
			//==


			#ifdef LOD_FADE_CROSSFADE
				LODFadeCrossFade(input.positionCS);
			#endif

			#if defined(_GBUFFER_NORMALS_OCT)
				float3 normalWS = normalize(input.normalWS);
				float2 octNormalWS = PackNormalOctQuadEncode(normalWS);
				float2 remappedOctNormalWS = saturate(octNormalWS * 0.5 + 0.5);
				half3 packedNormalWS = PackFloat2To888(remappedOctNormalWS);
				outNormalWS = half4(packedNormalWS, 0.0);
			#else
				float3 normalWS = NormalizeNormalPerPixel(input.normalWS);
				outNormalWS = half4(normalWS, 0.0);
			#endif

			#ifdef _WRITE_RENDERING_LAYERS
				uint renderingLayers = GetMeshRenderingLayer();
				outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
			#endif

		}

        ENDHLSL
    }

	Pass
	{
		Name "Meta"
		Tags{"LightMode" = "Meta"}

		Cull Off

		HLSLPROGRAM

		#pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch 
#pragma target 2.0 //targetm

		#pragma vertex UniversalVertexMeta
		#pragma fragment UniversalFragmentMeta

		#pragma shader_feature EDITOR_VISUALIZATION

		#pragma shader_feature_local_fragment N_F_SL_ON

		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"

		struct Attributes
		{
			float4 positionOS   : POSITION;
			float3 normalOS     : NORMAL;
			float2 uv0          : TEXCOORD0;
			float2 uv1          : TEXCOORD1;
			float2 uv2          : TEXCOORD2;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		//#ifdef _TANGENT_TO_WORLD
			//float4 tangentOS     : TANGENT;
		//#endif
		};

		struct Varyings
		{
			float4 positionCS   : SV_POSITION;
			float2 uv           : TEXCOORD0;

			//
			#ifdef EDITOR_VISUALIZATION
				float2 VizUV        : TEXCOORD1;
				float4 LightCoord   : TEXCOORD2;
			#endif
			//

			float3 normalWS		: TEXCOORD3;
			float3 positionWS	: TEXCOORD4;
		};

		Varyings UniversalVertexMeta(Attributes input)
		{

			//
			Varyings output = (Varyings)0;
			output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
			output.normalWS = TransformObjectToWorldDir(input.normalOS);
			output.positionCS = UnityMetaVertexPosition(input.positionOS.xyz, input.uv1, input.uv2);
			output.uv = TRANSFORM_TEX(input.uv0, _MainTex);
		#ifdef EDITOR_VISUALIZATION
			UnityEditorVizData(input.positionOS.xyz, input.uv0, input.uv1, input.uv2, output.VizUV, output.LightCoord);
		#endif

			return output;
			//
		}

		half4 UniversalFragmentMeta(Varyings input) : SV_Target
		{
			#ifdef N_F_TP_ON
				half4 _MainTex_var = RT_Tripl_Default(_MainTex, sampler_MainTex, input.positionWS, input.normalWS);
			#else
				half4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(input.uv, _MainTex));
			#endif


			//
			#ifdef UNITY_COLORSPACE_GAMMA
				_MainColor = float4(LinearToGamma22(_MainColor.rgb),_MainColor.a);
			#endif
			//


			half4 RTD_TEX_COL = _MainTex_var * (_MainColor * _MaiColPo);


			//RT_SL
			half3 RTD_SL_CHE_1;
			half3 RTD_SL = RT_SL(input.uv, input.positionWS, input.normalWS, (half3)0.0, RTD_TEX_COL.rgb, (half3)0.0, RTD_SL_CHE_1);
			//==


			MetaInput metaInput;
			metaInput.Albedo = RTD_TEX_COL.rgb;
			metaInput.Emission = RTD_SL;

			#ifdef EDITOR_VISUALIZATION
				metaInput.VizUV = fragIn.VizUV;
				metaInput.LightCoord = fragIn.LightCoord;
			#endif

			return MetaFragment(metaInput);
		}

		ENDHLSL
	}

	Pass
    {
        Name "MotionVectors"
        Tags { "LightMode" = "MotionVectors" }

        ColorMask RG

        HLSLPROGRAM

		#pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch 
		#pragma target 3.5

		#pragma vertex vert
		#pragma fragment frag

		#pragma shader_feature_local_fragment N_F_TRANS_ON
		#pragma shader_feature_local_fragment N_F_CO_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON
		#pragma shader_feature_local_fragment N_F_TP_ON
		#pragma shader_feature_local_fragment N_F_SCO_ON
		#pragma shader_feature_local_vertex N_F_DDMD_ON
		#pragma shader_feature_local_vertex N_F_PA_ON
		#pragma shader_feature_local_vertex N_F_SE_ON

        #pragma multi_compile _ LOD_FADE_CROSSFADE
        #pragma shader_feature_local_vertex _ADD_PRECOMPUTED_VELOCITY

		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#pragma multi_compile_instancing
		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

		#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityInput.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MotionVectorsCommon.hlsl"
		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl"

        ENDHLSL
    }

	Pass
    {
        Name "XRMotionVectors"
        Tags { "LightMode" = "XRMotionVectors" }

        ColorMask RGBA

        Stencil
        {
            WriteMask 1
            Ref 1
            Comp Always
            Pass Replace
        }

        HLSLPROGRAM

		#pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 xboxseries playstation switch 
		#pragma target 3.5

		#pragma vertex vert
		#pragma fragment frag

		#pragma shader_feature_local_fragment N_F_TRANS_ON
		#pragma shader_feature_local_fragment N_F_CO_ON
		#pragma shader_feature_local_fragment N_F_NFD_ON
		#pragma shader_feature_local_fragment N_F_TP_ON
		#pragma shader_feature_local_fragment N_F_SCO_ON
		#pragma shader_feature_local_vertex N_F_DDMD_ON
		#pragma shader_feature_local_vertex N_F_PA_ON
		#pragma shader_feature_local_vertex N_F_SE_ON

        #pragma multi_compile _ LOD_FADE_CROSSFADE
        #pragma shader_feature_local_vertex _ADD_PRECOMPUTED_VELOCITY
		#define APLICATION_SPACE_WARP_MOTION 1

		#include "Assets/RealToon/RealToon Shaders/RealToon Core/URP/RT_URP_Core.hlsl"

		#pragma multi_compile_instancing
		#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

		#include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityInput.hlsl"

		#if defined(LOD_FADE_CROSSFADE)
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
		#endif

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MotionVectorsCommon.hlsl"
		#include_with_pragmas "Assets/RealToon/RealToon Shaders/RealToon Core/URP/Pass/RT_URP_MoVecPas.hlsl"

        ENDHLSL
    }
}

FallBack "Hidden/InternalErrorShader"
CustomEditor "RealToon.GUIInspector.RealToonShaderGUI_URP_SRP"

}
