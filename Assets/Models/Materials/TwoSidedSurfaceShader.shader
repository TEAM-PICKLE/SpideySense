Shader "Custom/TwoSidedStandard"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}   // Albedo texture
        _Color ("Color", Color) = (1,1,1,1)          // Tint color
        _Glossiness ("Smoothness", Range(0,1)) = 0.5 // Smoothness/Glossiness
        _Metallic ("Metallic", Range(0,1)) = 0.0     // Metallic factor
        _NormalMap ("Normal Map", 2D) = "bump" {}    // Normal map
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Cull Off // Render both sides

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _NormalMap;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float3 viewDir;
            float3 worldNormal;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 albedoColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = albedoColor.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = albedoColor.a;

            // Proper handling of normals for both sides
            fixed3 normalDirection = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));

            #ifdef UNITY_PASS_FORWARDBASE
            if (dot(IN.viewDir, IN.worldNormal) < 0)
            {
                // Flip normals for backface
                normalDirection.z = -normalDirection.z;
            }
            #endif

            o.Normal = normalDirection;
        }
        ENDCG
    }

    FallBack "Diffuse"
}