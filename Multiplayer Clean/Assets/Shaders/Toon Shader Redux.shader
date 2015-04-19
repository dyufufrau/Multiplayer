// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4782,x:32719,y:32712,varname:node_4782,prsc:2|emission-4795-XYZ;n:type:ShaderForge.SFN_VertexColor,id:8767,x:31526,y:32979,varname:node_8767,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:4783,x:31526,y:32795,ptovrint:False,ptlb:ILM Texture,ptin:_ILMTexture,varname:node_4783,prsc:2,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1760,x:31526,y:32597,ptovrint:False,ptlb:SSS Texture,ptin:_SSSTexture,varname:node_1760,prsc:2,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6258,x:31526,y:32413,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_6258,prsc:2,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Vector4Property,id:4795,x:31526,y:33134,ptovrint:False,ptlb:node_4795,ptin:_node_4795,varname:node_4795,prsc:2,glob:False,v1:0,v2:0,v3:0,v4:0;proporder:4783-4795;pass:END;sub:END;*/

Shader "Custom/Toon Shader Redux" {
    Properties {
        _ILMTexture ("ILM Texture", 2D) = "black" {}
        _node_4795 ("node_4795", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _node_4795;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD1;
                #else
                    float3 shLight : TEXCOORD1;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float3 emissive = _node_4795.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
