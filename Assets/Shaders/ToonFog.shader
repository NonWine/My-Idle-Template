Shader "Unlit/ToonFog"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Dark ("Dark", Range(0,1)) = 1
        
        _FogOffset ("Fog Offset", Float) = 1.0
		_FogScale ("Fog Scale", Float) = 0.01
		_FogColor ("Fog Color", COLOR) = (1,1,1,1)
		_FogAxis ("Fog Axis", Vector) = (0,1,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;
            float _Dark;

            float _FogOffset;
            float _FogScale;
            fixed4 _FogColor;
            float3 _FogAxis;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float fong = dot(_WorldSpaceLightPos0.xyz, i.worldNormal);
                float nfong = (fong + 1.0) * 0.5;
                //nfong = clamp(nfong, 0, 1);
                fixed4 computeColor = lerp(_Color * _Dark, _Color, nfong);

                float3 napr = i.worldPos * _FogAxis;

                float fx = -(napr.x + napr.y + napr.z);

            	float f = clamp((fx - _FogOffset) * _FogScale, 0.0, 1.0);
                
                return lerp(computeColor * col, _FogColor, f);
            }
            ENDCG
        }
    }
}