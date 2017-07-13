Shader "Flat Color" {
    Properties{
        _Color("Color",Color) = (0,0,0,0)
    }

    SubShader
    {
        Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            // Pragmas
            #pragma vertex vertex
            #pragma fragment fragment

            uniform float4 _Color;

            // Base Input Structs
            struct vertexInput {
                float4 pos : POSITION;
            };
            struct vertexOutput {
                float4 pos : SV_POSITION;
            };

            // Vertex Function
            vertexOutput vertex(vertexInput v) {
                vertexOutput o;
                o.pos = mul(UNITY_MATRIX_MVP, v.pos);
                return o;
            }

            // Fragment Function
            float4 fragment(vertexOutput i) : COLOR{
                return _Color;
            }
            ENDCG
        }
    }
}