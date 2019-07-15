Shader "Plant/WaterNeed"
{
    Properties
    {
        _Percentage ("Percentage", Range(0, 1)) = 1
        _WaterNeed_Gradient ("Gradient", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "ObjectType" = "Plant" "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface plantSurf Standard fullforwardshadows
        #pragma target 3.0   
        #include "UnityCG.cginc"     
                        
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)
        
        
        sampler2D _WaterNeed_Gradient;
        half _Percentage;
        half _IsPlant;
          
        struct Input
        {
            float2 uv_MainTex;
        };
        
        void plantSurf(Input IN, inout SurfaceOutputStandard o)
        {
            if (!_IsPlant)
                o.Albedo =  fixed3(0.75, 0.75, 0.75);
            else
                o.Albedo = tex2D(_WaterNeed_Gradient, fixed2(_Percentage, 0));
            o.Metallic = 0;
            o.Smoothness = 0;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
