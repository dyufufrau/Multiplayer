// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:1,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:10,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:914,x:32719,y:32712,varname:node_914,prsc:2|voffset-2668-OUT;n:type:ShaderForge.SFN_VertexColor,id:7515,x:31854,y:32961,varname:node_7515,prsc:2;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:6067,x:32061,y:33021,varname:node_6067,prsc:2|IN-7515-A,IMIN-4784-OUT,IMAX-8040-OUT,OMIN-7038-OUT,OMAX-7486-OUT;n:type:ShaderForge.SFN_Vector1,id:4784,x:31854,y:33093,varname:node_4784,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:8040,x:31854,y:33159,varname:node_8040,prsc:2,v1:1;n:type:ShaderForge.SFN_NormalVector,id:7762,x:32051,y:32847,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:2668,x:32243,y:32928,varname:node_2668,prsc:2|A-7762-OUT,B-6067-OUT;n:type:ShaderForge.SFN_Slider,id:7038,x:31697,y:33249,ptovrint:False,ptlb:Minimum,ptin:_Minimum,varname:node_7038,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:7486,x:31697,y:33352,ptovrint:False,ptlb:Maximum,ptin:_Maximum,varname:node_7486,prsc:2,min:0,cur:0,max:1;proporder:7038-7486;pass:END;sub:END;*/

Shader "Custom/Toon Shader Redux Outline" {
    Properties {
        _Minimum ("Minimum", Range(0, 1)) = 0
        _Maximum ("Maximum", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "Queue"="Geometry+10"
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Front
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _Minimum;
            uniform float _Maximum;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float3 normalDir : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(_Object2World, float4(-v.normal,0)).xyz;
                float node_4784 = 0.0;
                v.vertex.xyz += (v.normal*(_Minimum + ( (o.vertexColor.a - node_4784) * (_Maximum - _Minimum) ) / (1.0 - node_4784)));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _Minimum;
            uniform float _Maximum;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float3 normalDir : TEXCOORD5;
                float4 vertexColor : COLOR;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD6;
                #else
                    float3 shLight : TEXCOORD6;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(_Object2World, float4(-v.normal,0)).xyz;
                float node_4784 = 0.0;
                v.vertex.xyz += (v.normal*(_Minimum + ( (o.vertexColor.a - node_4784) * (_Maximum - _Minimum) ) / (1.0 - node_4784)));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _Minimum;
            uniform float _Maximum;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(_Object2World, float4(-v.normal,0)).xyz;
                float node_4784 = 0.0;
                v.vertex.xyz += (v.normal*(_Minimum + ( (o.vertexColor.a - node_4784) * (_Maximum - _Minimum) ) / (1.0 - node_4784)));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
