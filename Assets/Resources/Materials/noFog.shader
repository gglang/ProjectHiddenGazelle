Shader "Unlit/noFog" {
    Properties {
        _Color ("Color Tint", Color) = (1,1,1,1)
        _MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white"
    }
    Category {
       Lighting On
       Fog { Mode Off }
       ZWrite Off
       Blend SrcAlpha OneMinusSrcAlpha
       Cull Back
       Tags {Queue=Opaque}
       SubShader {
            Material {
               Emission [_Color]
            }
            Pass {
               SetTexture [_MainTex] {
                      Combine Texture * Primary, Texture * Primary
                }
            }
        } 
    }
}
