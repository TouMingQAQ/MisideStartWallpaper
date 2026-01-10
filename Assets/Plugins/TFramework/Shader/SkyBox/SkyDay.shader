Shader "TShader/SkyDay"
{
    Properties
    {
        //太阳
        [HDR]_SunColor("Sun Color", Color) = (1,1,1,1)
        _SunSetColor("Sun Set Color", Color) = (1,1,1,1)
        _SunIntensity("Sun Intensity", Range(0, 100)) = 20
        _SunRadius("Sun Radius", Float) = 0.1

        //月亮
        [HDR]_MoonColor("Moon Color", Color) = (1,1,1,1)
        _MoonRadius("Moon Radius", Float) = 0.1
        _MoonIntensity("Moon Intensity", Range(0, 100)) = 20
        _MoonOffset("Moon Offset", Range(-1,1)) = 0.2

        //天空
        _SkyDayTopColor("Sky Day Top Color", Color) = (1,1,1,1)
        _SkyDayBottomColor("Sky Day Bottom Color", Color) = (0.5,0.5,0.5,1)

        _SkyNightColor("Sky Night Top Color", Color) = (0.2,0.2,0.2,1)
        _SkyNightBottomColor("Sky Night Bottom Color", Color) = (0,0,0,1)

        //地平线
        _HorizonIntensity("Horizon Intensity", Range(0, 10)) = 1
        _HorizonOffset("Horizon Offset", Range(-1, 1)) = 0
        _HorizonDayColor("Horizon Day Color", Color) = (1,1,1,1)
        _HorizonNightColor("Horizon Night Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };


            half4 _SunColor;
            half4 _SunSetColor;
            float _SunRadius;
            float _SunIntensity;

            half4 _MoonColor;
            float _MoonRadius;
            float _MoonOffset;
            float _MoonIntensity;

            half4 _SkyDayTopColor;
            half4 _SkyDayBottomColor;
            half4 _SkyNightTopColor;
            half4 _SkyNightBottomColor;

            float _HorizonIntensity;
            float _HorizonOffset;
            half4 _HorizonDayColor;
            half4 _HorizonNightColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                Light mainLight = GetMainLight();
                //水平天空UV 用于云层，星空等
                float2 skyUV = i.uv.xz / i.uv.y;
                //太阳
                float sun = distance(i.uv.xyz, float4(mainLight.direction, 0));
                sun = saturate((1 - sun / _SunRadius) * _SunIntensity);

                float moon = distance(i.uv.xyz, float4(-mainLight.direction, 0));
                moon = saturate((1 - moon / _MoonRadius) * _MoonIntensity);
                float moonMask = distance(float3(i.uv.x + _MoonOffset, i.uv.yz), float4(-mainLight.direction, 0));
                moonMask = saturate((1 - moonMask / _MoonRadius * 0.9) * _MoonIntensity);
                moon -= moonMask;
                moon = saturate(moon);

                //太阳月亮合并
                float3 sunAndMoon = sun * _SunColor.rgb + moon * _MoonColor.rgb;

                //地平线
                float horizon = abs(i.uv.y * _HorizonIntensity - _HorizonOffset);
                float sunSet = saturate((1 - horizon) * saturate(mainLight.direction.y * 2));
                float3 sunSetColor = sunSet * _SunSetColor.rgb;
                float3 horizonGlow = saturate((1 - horizon * 5) * saturate(mainLight.direction.y * 5)) *
                    _HorizonDayColor.rgb;
                float3 horizonGlowNight = saturate((1 - horizon * 5) * saturate(-mainLight.direction.y * 5)) *
                    _HorizonNightColor.rgb;
                horizonGlow += horizonGlowNight;
                float sunT = distance(i.uv.xyz, float4(mainLight.direction, 0));
                sunT = saturate(sunT);
                horizonGlow *= (1 - sunT);
                sunSetColor *= (1 - sunT);
                horizonGlow += sunSetColor;

                
                float3 skyDayColor = lerp(_SkyDayBottomColor.rgb, _SkyDayTopColor.rgb, saturate(horizon));
                float3 skyNightColor = lerp(_SkyNightBottomColor.rgb, _SkyNightTopColor.rgb, saturate(horizon));
                float3 skyColor = lerp(skyNightColor, skyDayColor, saturate(mainLight.direction.y));


                skyColor += sunAndMoon;
                skyColor += horizonGlow;
                return float4(skyColor, 1);
            }
            ENDHLSL
        }
    }
}