Shader "Plant/GradientShader"
{
    Properties
    {
        _Percentage ("Percentage", Range(0, 1)) = 50.0
        _Gradient ("Gradient", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _Gradient;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Percentage;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = tex2D(_Gradient, fixed2(_Percentage, 0));
            o.Metallic = 0;
            o.Smoothness = 0;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
