Shader "TFramework/TranslateEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShuttersSize("ShuttersSize", float) = 1
        _ShuttersStep("ShuttersStep", Range(0,1)) = 0
        _ShuttersRotate("ShuttersRotate", float) = 0
        _ShuttersBackGroundColor("ShuttersBackGroundColor", Color) = (0,0,0,1)
        [Enum(TFramework.UIEffect.EffectShader.TranslateType)] _TranslateType("Translate Type", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "RenderQueue"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Off
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _TranslateType;
            float _ShuttersSize;
            float _ShuttersStep;
            fixed4 _ShuttersBackGroundColor;
            float _ShuttersRotate;

            float Remap(float value, float from1, float to1, float from2, float to2)
            {
                return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
            }

            float2 RotatePoint2(float2 point2, float2 center, half radian)
            {
                float2 dir = float2(0, 0) - center;
                point2 += dir;

                float2 newP;
                float c = cos(radian);
                float s = sin(radian);
                newP.x = point2.x * c - point2.y * s;
                newP.y = point2.x * s + point2.y * c;

                newP += (dir * -1);
                return newP;
            }

            v2f vert(appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                switch (_TranslateType)
                {
                //百叶窗
                case 0:
                    half2 uv = i.uv;
                    half2 uv2 = i.uv;
                    uv2 = RotatePoint2(uv2, half2(0.5, 0.5), _ShuttersRotate);
                    uv.xy *= _ShuttersSize;
                    uv.x = frac(uv.x);
                    float r = distance(uv.x, 0.5);
                    r += uv2.x;
                    _ShuttersStep = Remap(_ShuttersStep, 0, 1, 0, 2);
                    r = step(_ShuttersStep, r);
                    col = lerp(col, _ShuttersBackGroundColor, r);
                    break;
                default:
                    break;
                }
                return col;
            }
            ENDHLSL
        }
    }
}