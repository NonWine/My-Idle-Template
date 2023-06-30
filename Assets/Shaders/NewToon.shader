Shader "LuB/NewCoolToon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ColorFront ("Color Front", Color) = (1,1,1,1)
        _ShadingMult ("Shading Multiple", Float) = 1
        _CircleDensity ("Circle Density", Float) = 1
        _Rotation ("Rotation", Float) = 1
        _Softness ("Softness", Float) = 1
        _FallofTresshold ("Fallof Tresshold", Float) = 1
        _LitTresshold ("Lit Tresshold", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
                float3 worldNormal : NORMAL;
                float4 screenPos : TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _ColorFront;
            
            fixed4 _Color;
            float _ShadingMult;
            float _CircleDensity;
            float _Softness;
            float _Rotation;
            float _FallofTresshold;
            float _LitTresshold;
            
            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed3 Unity_ColorspaceConversion_RGB_HSV_float(float3 In)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
                float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
                float D = Q.x - min(Q.w, Q.y);
                float  E = 1e-10;
                return  float3(abs(Q.z + (Q.w - Q.y)/(6.0 * D + E)), D / (Q.x + E), Q.x);
            }

            fixed3 Unity_ColorspaceConversion_HSV_RGB_float(float3 In)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
                return In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
            }

            inline float2 unity_voronoi_noise_randomVector (float2 UV, float offset)
            {
                float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
                UV = frac(sin(mul(UV, m)) * 46839.32);
                return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
            }

            void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
            {
                float2 g = floor(UV * CellDensity);
                float2 f = frac(UV * CellDensity);
                float t = 8.0;
                float3 res = float3(8.0, 0.0, 0.0);

                for(int y=-1; y<=1; y++)
                {
                    for(int x=-1; x<=1; x++)
                    {
                        float2 lattice = float2(x,y);
                        float2 offset = unity_voronoi_noise_randomVector(lattice + g, AngleOffset);
                        float d = distance(lattice + offset, f);
                        if(d < res.x)
                        {
                            res = float3(d, offset.x, offset.y);
                            Out = res.x;
                            Cells = res.y;
                        }
                    }
                }
            }

            float Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax)
            {
                return  OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                fixed3 col = tex2D(_MainTex, i.uv) * _Color;

                fixed3 hsv = Unity_ColorspaceConversion_RGB_HSV_float(col.rgb);
                hsv.b *= _ShadingMult;
                fixed3 compCol = Unity_ColorspaceConversion_HSV_RGB_float(hsv);

                float2 screenPos = float2(i.screenPos.x, i.screenPos.y / (_ScreenParams.x / _ScreenParams.y)) / i.screenPos.w;
                screenPos *= _CircleDensity;
                screenPos = float2(screenPos.x * cos(_Rotation) - screenPos.y * sin(_Rotation), screenPos.x * sin(_Rotation) + screenPos.y * cos(_Rotation));

                float outVor;
                float CellVor;
                Unity_Voronoi_float(screenPos, 0, 5, outVor, CellVor);
                
                float fong = dot(_WorldSpaceLightPos0.xyz, i.worldNormal);
                fong = -1 * fong;
                
                fong = Unity_Remap_float(fong, float2(-1,1), float2(_LitTresshold - _FallofTresshold, _LitTresshold));
                
                float computeColor = smoothstep(fong, fong + _Softness, outVor);

                col = lerp(compCol, _ColorFront, computeColor);

                //return fixed4(computeColor, computeColor, computeColor, 1);
                return fixed4(col, 1);
            }
            ENDCG
        }
    }
}