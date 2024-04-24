Shader "Unlit/Warning Sign"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PulseMagnitude ("Pulse Magnitude", Float) = 1.0
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _PulseMagnitude;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float pulse = sin(_Time.y * _PulseMagnitude) * 0.75 + 1.75;
                float2 uvPulse = v.uv - float2(0.5, 0.5);
                uvPulse *= pulse;
                uvPulse += float2(0.5, 0.5);

                o.uv = TRANSFORM_TEX(uvPulse, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // float2 clampedUV = clamp(i.uv, 0.0, 1.0);

                // sample the texture
                float4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}
