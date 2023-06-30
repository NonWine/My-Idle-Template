Shader "Unlit/ToonChangeColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EffectTex ("Effect Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _NewColor ("New Color", Color) = (1,1,1,1)
        _CircleDensity ("Circle Density", Float) = 10
        _Dark ("Dark", Range(0,1)) = 1
        [PerRendererData] _StartPos ("Start Pos", Vector) = (0,0,0,0)
        [PerRendererData]_ChangeProgress ("Progress", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdadd_fullshadows
            
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
                float3 worldNormal : NORMAL;
                float3 worldPos : TEXCOORD2;
                unityShadowCoord4 _ShadowCoord : TEXCOORD3;
                float2 uvWorld : TEXCOORD4;
            };

            sampler2D _MainTex;
            sampler2D _EffectTex;
            float4 _MainTex_ST;

            fixed4 _Color;
            float _Dark;

            float3 _StartPos;
            fixed4 _NewColor;
            float _CircleDensity;
            float _ChangeProgress;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                float3 worldNormal = 1-step(abs(o.worldNormal), 0.1);

                float2 test = float2(o.worldPos.x * worldNormal.y, o.worldPos.z * worldNormal.y);
                float2 testZ = float2(o.worldPos.x * worldNormal.z, o.worldPos.y * worldNormal.z);
                float2 testX = float2(o.worldPos.z * worldNormal.x, o.worldPos.y * worldNormal.x);

                test += testZ + testX;

                o.uvWorld = test;
                
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float fong = dot(_WorldSpaceLightPos0.xyz, i.worldNormal);
                float nfong = (fong + 1.0) * 0.5;

                float dist = _ChangeProgress - length(i.worldPos - _StartPos);
                
                float outVor = tex2D(_EffectTex, i.uvWorld * _CircleDensity) + 0.2;

                fixed4 lerpColor = lerp(_Color, _NewColor, smoothstep(0.8, 0.9, outVor * dist));
                
                fixed4 computeColor = lerp(lerpColor * _Dark, lerpColor, nfong);
                
                //return fixed4(worldNormal, 1);
                return computeColor * col * LIGHT_ATTENUATION(i);
            }
            ENDCG
        }
        
        UsePass "VertexLit/SHADOWCASTER"
    }
}