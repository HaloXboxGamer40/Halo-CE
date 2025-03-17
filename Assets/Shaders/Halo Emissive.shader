Shader "Halo Combat Evolved/Halo Emissive"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _EmissionMap ("Emission", 2D) = "black" {}
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionStrength ("Emission Strength", Range(0,10)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode"="ForwardBase" }
        LOD 200
        Cull Off
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _EmissionMap;
        fixed4 _Color;
        fixed4 _EmissionColor;
        half _Glossiness;
        half _Metallic;
        half _EmissionStrength;
        
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_EmissionMap;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            
            // Emission-based lighting
            fixed4 emissionTex = tex2D(_EmissionMap, IN.uv_EmissionMap);
            fixed3 finalEmission = emissionTex.rgb * _EmissionColor.rgb * _EmissionStrength;
            
            if (dot(finalEmission, finalEmission) > 0.01) // If emitting, contribute to lighting
            {
                o.Emission = finalEmission;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}

// To achieve real-time lighting, attach a C# script that creates a dynamic point light at the object's position when this material is applied.