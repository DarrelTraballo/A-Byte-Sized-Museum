Shader "Unlit/UnlitShader"
{
    Properties
    {
        _Color ("Color", Color) = (0.5471698, 0, 0, 1)
        _Smoothness ("Smoothness", Range(10, 100)) = 50
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityStandardBRDF.cginc"
            #include "AutoLight.cginc"

            float4 _Color;
            float _Smoothness;

            struct MeshData
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;

                v.normal = normalize(v.normal);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(UnityObjectToWorldNormal(v.normal));
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                i.normal = normalize(i.normal);
                float3 lightDir = float3(0, 1, 0);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfVector = normalize(lightDir + viewDir);

                float3 lightColor = float3(1, 1, 1);

                float3 ambient = float3(0.0f, 0.0f, 0.1f);
                float3 diffuse = lightColor * _Color * max(0, dot(lightDir, i.normal));
                float3 specular = lightColor * pow(DotClamped(halfVector, i.normal), _Smoothness);

                float4 outCol = float4(saturate(ambient + diffuse), 1);
                return outCol;
            }
            ENDCG
        }
    }
}
