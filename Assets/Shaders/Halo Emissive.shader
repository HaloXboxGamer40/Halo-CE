Shader "Halo Combat Evolved/Halo Emissive"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionStrength ("Emission Strength", Range(0,10)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode"="ForwardBase" }
        LOD 200
        Cull Off
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows lightmap
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        half _Glossiness;
        half _Metallic;
        fixed4 _EmissionColor;
        half _EmissionStrength;
        
        struct Input
        {
            float2 uv_MainTex;
            float2 uv2_Lightmap; // Lightmap UVs
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            
            // Apply baked lighting
            fixed3 bakedLighting = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, IN.uv2_Lightmap));
            o.Emission = bakedLighting + (_EmissionColor.rgb * _EmissionStrength);
        }
        ENDCG
    }
    FallBack "Diffuse"
}