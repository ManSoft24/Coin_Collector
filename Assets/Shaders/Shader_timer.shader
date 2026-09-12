Shader "Unlit/Shader_timer"
{
    Properties
    {
        [PerRenderer] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (1,1,1,1)
        _KecepatanDenyut ("Kecepatan Denyut", Float) = 1
        _KecepatanGulir ("Kecepatan Gulir", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" "ignoreProjector"="True" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "Unlit2D"
            Tags { "LightMode"="Universal2D" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Tint;
                float _KecepatanDenyut;
                float _KecepatanGulir;
            CBUFFER_END

            struct appdata
            {
                float4 posisiObjek : POSITION;
                float2 uv : TEXCOORD0;
                float4 warnaVertex : COLOR;
            };

            struct v2f
            {
                float4 posisiClip : SV_POSITION;
                float2 uv : TEXCOORD0;
                half4 warnaVertex : COLOR;
            };


            v2f vert (appdata v)
            {
                v2f keluar;
                keluar.posisiClip = TransformObjectToHClip(v.posisiObjek.xyz);
                keluar.uv = TRANSFORM_TEX(v.uv, _MainTex);
                keluar.warnaVertex = v.warnaVertex;
                return keluar;

            }

            half4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv.x += _Time.y * _KecepatanGulir;

                half4 teks = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                half4 kolom = teks * (half4)_Tint * i.warnaVertex;

                // sin() hasilnya -1..1, kita geser jadi 0.6..1 supaya tidak sampai hitam.
                half denyut = 0.6h + 0.4h * (half)sin(_Time.y * _KecepatanDenyut);
                kolom.rgb *= denyut;

                // sample the texture

                return kolom;
            }
            ENDHLSL
        }
    }
}
