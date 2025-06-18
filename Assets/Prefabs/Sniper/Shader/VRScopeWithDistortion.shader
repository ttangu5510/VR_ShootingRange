// Shader Graph 대신 HLSL Unlit Shader 예제 (볼록렌즈 왜곡 포함)
Shader "Custom/VRScopeWithDistortion"
{

    Properties
    {
        _MainTex("Render Texture", 2D) = "white" {}
        _DistortionStrength("Distortion Strength", Range(0, 0.5)) = 0.3
    }

    SubShader
    {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
Blend SrcAlpha OneMinusSrcAlpha
ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _DistortionStrength;

            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);

                float2 center = float2(0.5, 0.5);
                float2 offset = i.uv - center;
                float dist = length(offset);

                // 볼록 렌즈 왜곡 적용
                float2 distortedUV = i.uv + normalize(offset) * pow(dist, 2.0) * _DistortionStrength;

                // 원형 마스크
                float mask = step(dist, 0.5);

                fixed4 col = tex2D(_MainTex, distortedUV);
                col.rgb *= mask;
                col.a = mask;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Texture"
}
