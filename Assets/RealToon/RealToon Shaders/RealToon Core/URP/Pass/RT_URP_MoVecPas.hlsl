//RealToon HDRP - MoVecPas
//MJQStudioWorks

//=========================

struct Attributes
{
	float4 position             : POSITION;
	float3 normalOS             : NORMAL;
	float4 tangentOS			: TANGENT;
	float2 uv                   : TEXCOORD0;
	float3 positionOld          : TEXCOORD4;
	#if _ADD_PRECOMPUTED_VELOCITY
		float3 alembicMotionVector  : TEXCOORD5;
	#endif

	#ifndef	N_F_DDMD_ON
float4 weights : BLENDWEIGHTS;//DOTS_LiBleSki_MV
uint4 indices : BLENDINDICES;//DOTS_LiBleSki_MV
//uint vertexID : SV_VertexID;//DOTS_CompDef_MV
	#endif

	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
	float4 positionCS                 : SV_POSITION;
	float4 positionCSNoJitter         : POSITION_CS_NO_JITTER;
	float4 previousPositionCSNoJitter : PREV_POSITION_CS_NO_JITTER;
	float2 uv                         : TEXCOORD0;
	float3 positionWS				  : TEXCOORD1;
	float3 normalWS					  : TEXCOORD2;

	UNITY_VERTEX_INPUT_INSTANCE_ID
	UNITY_VERTEX_OUTPUT_STEREO
};

Varyings vert(Attributes input)
{
	Varyings output = (Varyings)0;

	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_TRANSFER_INSTANCE_ID(input, output);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);


	#if defined(UNITY_DOTS_INSTANCING_ENABLED)

		#ifdef	N_F_DDMD_ON

			float4 _LBS_CD_Position = input.position;
			float3 _LBS_CD_Normal = input.normalOS;
			//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed

		#else

			float4 _LBS_CD_Position = 0;
			float3 _LBS_CD_Normal = 0;
			float4 _LBS_CD_Tangent = 0;

DOTS_LiBleSki(input.indices, input.weights, input.position.xyz, input.normalOS.xyz, input.tangentOS.xyz, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_LiBleSki_MV
//DOTS_CompDef(input.vertexID, (float3)_LBS_CD_Position, _LBS_CD_Normal, (float3)_LBS_CD_Tangent);//DOTS_CompDef_MV
			_LBS_CD_Position.w = 1.0;

		#endif

	#else
		float4 _LBS_CD_Position = input.position;
		float3 _LBS_CD_Normal = input.normalOS;
		//float4 _LBS_CD_Tangent = input.tangentOS; //not currently needed
	#endif


	const VertexPositionInputs vertexInput = GetVertexPositionInputs(_LBS_CD_Position.xyz);

	//RT_SE
	#if N_F_SE_ON
		input.position = RT_SE(vertexInput.positionWS, input.position);
		_LBS_CD_Position = input.position;
	#endif
	//==
		
	//RT_PA
	#if N_F_PA_ON
		output.positionCS = mul(RT_PA(vertexInput.positionWS), float4(_LBS_CD_Position.xyz,1.0) );
	#else
		output.positionCS = vertexInput.positionCS;
	#endif
	//==
		
    output.uv = TRANSFORM_TEX(input.uv, _MainTex);
    output.positionWS = TransformObjectToWorld(_LBS_CD_Position.xyz);
    output.normalWS = TransformObjectToWorldDir(_LBS_CD_Normal);
	output.positionCSNoJitter = mul(_NonJitteredViewProjMatrix, mul(UNITY_MATRIX_M, _LBS_CD_Position));
		
	float4 prevPos = (unity_MotionVectorsParams.x == 1) ? float4(input.positionOld, 1) : _LBS_CD_Position;
	#if _ADD_PRECOMPUTED_VELOCITY
		prevPos = prevPos - float4(input.alembicMotionVector, 0);
	#endif

	output.previousPositionCSNoJitter = mul(_PrevViewProjMatrix, mul(UNITY_PREV_MATRIX_M, prevPos));

	ApplyMotionVectorZBias(output.positionCS);

	return output;
}

float4 frag(Varyings input) : SV_Target
{
	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

	#ifdef N_F_TP_ON
		half4 _MainTex_var = RT_Tripl_Default(_MainTex, sampler_MainTex, input.positionWS, input.normalWS);
	#else
		half4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(input.uv, _MainTex));
	#endif

	//RT_CO
	RT_CO(input.uv, _MainTex_var, input.positionWS, input.normalWS, input.positionCS.xy);
	//==
		
		
	//RT_NFD
	#ifdef N_F_NFD_ON
		RT_NFD(input.positionCS.xy);
	#endif
	//==
		
		
	#if defined(LOD_FADE_CROSSFADE)
		LODFadeCrossFade(input.positionCS);
	#endif

	return float4(CalcNdcMotionVectorFromCsPositions(input.positionCSNoJitter, input.previousPositionCSNoJitter), 0, 0);
}

//