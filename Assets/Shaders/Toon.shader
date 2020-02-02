Shader "Custom/Toon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint Color", Color) = (1,1,1,1)
        _Ambient ("Ambient Amount", Range(0, 1)) = 0.2

        [Specular]
        _SpecularColor ("Specular Color", Color) = (.9,.9,.9,1)
        _Glossiness ("Glossiness", Float) = 32

        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags 
        { 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Tint;
            float _Ambient;

            fixed4 _SpecularColor;
            float _Glossiness;

            float4 _RimColor;
            float _RimAmount;
            float _RimPower;
            float _RimThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                // sample the texture
                float3 normal = normalize(i.worldNormal);
                fixed4 albedo = tex2D(_MainTex, i.uv);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float NdotL = dot(normal, lightDir);

                // Diffuse
                float diffuse = max(NdotL, 0.0f);
                diffuse = (NdotL > 0) ? 1 : 0;

                // Specular
                float3 viewDir = normalize(i.viewDir);
                float3 halfVector = normalize(lightDir + viewDir);
                float NdotH = dot(normal, halfVector);

                float specIntensity = pow(max(NdotH, 0.0f), _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;

                // Rim highlights
                float rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);
                // Dont have rim light on the darkside of the object
                //rimIntensity = lerp(0.0f, rimIntensity, NdotH);
                float4 rim = rimIntensity * _RimColor;

                col = (_Ambient + diffuse + specular + rim) * albedo * _Tint;
                //col = rim * albedo * _Tint;
                return col;
            }
            ENDCG
        }
    }
}
