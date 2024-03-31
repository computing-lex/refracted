Shader "Custom/PlanetGen"
{
    Properties
    {
        _Color("Albedo", Color) = (1, 1, 1, 1)
        _MainTex("Main Texture", 2D) = "white" {}

        _Glossiness("Glossiness", Range(0, 1)) = 0.5
        [Gamma] _Metallic("Metallic", Range(0, 1)) = 0

        _BumpScale("Normal Map Scale", Float) = 1
        _BumpMap("Normal Map", 2D) = "bump" {}

        _MapScale("Texture Scale", Float) = 1

        _FresnelColor("Fresnel Color", Color) = (1,1,1,1)
		_FresnelBias("Fresnel Bias", Float) = 0
		_FresnelScale("Fresnel Scale", Float) = 1
		_FresnelPower("Fresnel Power", Float) = 1

        _SkyNoise("Sky Noise", 2D) = "white" {}
        _SkyNoiseDistort("Sky Distortion Texture", 2D) = "black" {}
        _SkyCutoff("Sky Cuttoff", Range(0, 1)) = 0.3
        _SkyCutoffBlend("Sky Cuttoff Blend", Range(0, 1)) = 0.03
        _SkyColor("Sky Color", Color) = (1, 1, 1, 1)
        _SkyDistortStrength("Sky Distortion Strength", Range(0, 1)) = 0.1
        _SkyDistortSpeed("Sky Distortion Speed", Range(0, 1)) = 0.1
        _SkyRotationSpeed("Sky Rotation Speed", Float) = 0.3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM

        #pragma surface surf Standard vertex:vert fullforwardshadows addshadow

        #pragma shader_feature _NORMALMAP
        #pragma shader_feature _OCCLUSIONMAP

        #pragma target 4.0

        half4 _Color;
        sampler2D _MainTex;

        half _Glossiness;
        half _Metallic;

        half _BumpScale;
        sampler2D _BumpMap;

        half _MapScale;

        // Fresnel Stuff
        half4 _FresnelColor;
        half _FresnelBias;
        half _FresnelScale;
        half _FresnelPower;

        // Atmosphere Parameters
        sampler2D _SkyNoise;
        sampler2D _SkyNoiseDistort;
        half _SkyDistortStrength;
        half _SkyDistortSpeed;
        half _SkyCutoff;
        half _SkyCutoffBlend;
        float4 _SkyColor;
        half _SkyRotationSpeed;

        struct Input
        {
            float3 localCoord;
            float3 localNormal;
            float fresnel;
            float2 texcoordScroll;
        };

        void vert(inout appdata_full v, out Input data)
        {

            UNITY_INITIALIZE_OUTPUT(Input, data);
            data.localCoord = v.vertex.xyz;
            data.localNormal = v.normal.xyz;

            // Calculate Fresnel in Vertex Shader
            float3 i = normalize(ObjSpaceViewDir(v.vertex));
            data.fresnel = _FresnelBias + _FresnelScale * pow(1 + dot(i, v.normal), -_FresnelPower);
            float2 scroll = v.texcoord.xy + _Time.y * _SkyDistortSpeed;
            data.texcoordScroll = scroll;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Blending factor of triplanar mapping
            float3 bf = normalize(abs(IN.localNormal));
            bf /= dot(bf, (float3)1);

            // Triplanar mapping
            float2 tx = IN.localCoord.yz * _MapScale;
            float2 ty = IN.localCoord.zx * _MapScale;
            float2 tz = IN.localCoord.xy * _MapScale;

            // Base color
            half4 cx = tex2D(_MainTex, tx) * bf.x;
            half4 cy = tex2D(_MainTex, ty) * bf.y;
            half4 cz = tex2D(_MainTex, tz) * bf.z;
            half4 color = (cx + cy + cz) * _Color;

            // Atmosphere Color
            float2 sky_uv = IN.texcoordScroll;
            half4 sn2 = tex2D(_SkyNoiseDistort, sky_uv);
            half4 acx = tex2D(_SkyNoise, tx + sn2*_SkyDistortStrength + _Time.y * _SkyRotationSpeed) * bf.x;
            half4 acy = tex2D(_SkyNoise, ty + sn2*_SkyDistortStrength + _Time.y * _SkyRotationSpeed) * bf.y;
            half4 acz = tex2D(_SkyNoise, tz + sn2*_SkyDistortStrength + _Time.y * _SkyRotationSpeed) * bf.z;
            half4 atmcolor = (acx + acy + acz);
            half atm = smoothstep(_SkyCutoff, _SkyCutoff + _SkyCutoffBlend, atmcolor.r);
            color = lerp(color, _SkyColor, atm);

            // Fresnel Shading
            color = lerp(color, float4(_FresnelColor.rgb, 1), IN.fresnel * _FresnelColor.a);

            o.Albedo = color.rgb;
            // o.Emission = color.rgb * atm;
            o.Alpha = color.a;

        #ifdef _NORMALMAP
            // Normal map
            half4 nx = tex2D(_BumpMap, tx) * bf.x;
            half4 ny = tex2D(_BumpMap, ty) * bf.y;
            half4 nz = tex2D(_BumpMap, tz) * bf.z;
            half3 final_normal = UnpackScaleNormal(nx + ny + nz, _BumpScale);
            // This is a small brain solution but it fucking works, leave me alone
            half3 sky_normal = UnpackScaleNormal(float4(0.5, 0.5, 1.0, 1.0), _BumpScale);
            final_normal = lerp(final_normal, sky_normal, atm);
            o.Normal = final_normal;
        #endif

            // Misc parameters
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
