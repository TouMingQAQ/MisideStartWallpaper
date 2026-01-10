Shader "Hidden/TestImageEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _RadialScale ("Radial Scale", Float) = 1
        _LengthScale ("Length Scale", Float) = 1
        _ScaleSpeed ("Scale Speed", Float) = 0
        _RotateSpeed ("Rotate Speed", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "RenderQueue"="Transparent"
        }
        // No culling or depth
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            float4 _Center;
            float _RadialScale;
            float _LengthScale;
            float _ScaleSpeed;
            float _RotateSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float2 Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale)
            {
                float2 delta = UV - Center;
                float radius = length(delta) * 2 * RadialScale;
                float angle = atan2(delta.x, delta.y) * 1.0 / 6.28 * LengthScale;
                return float2(radius, angle);
            }

            float OutBox(float2 uv,float2 center,float radiu)
            {
                float col = 1;
                uv -= center;
                uv = abs(uv);
                uv -= float2(radiu, radiu);
                uv = saturate(uv);
                col = length(uv);
                col = step(col, 0);
                col = 1 - col;
                return col;
            }


            v2f vert(appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 polar = Unity_PolarCoordinates_float(i.uv, _Center.xy, _RadialScale, _LengthScale);
                float l = OutBox(i.uv, _Center, 0.3);
                return float4(l, l, l, 1);
            }
            ENDHLSL
        }
    }
}