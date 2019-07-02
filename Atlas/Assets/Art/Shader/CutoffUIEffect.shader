// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/CutoffUIEffect"
{
    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
        _Color("DisplayColor", Color) = (1,1,1,1)
        _CutoffRange("Cutoff Range", Float) = 1
        _Cutoff("Cutoff", Float) = 0.5
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };
 
            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
            };
         
            sampler2D _MainTex;
            float4 _MainTex_ST;
         
            float4 _Color;
            float _CutoffRange;
            float _Cutoff;

            half GetChannel(half channel, half cutoff, half range)
            {    
                return saturate((channel - ((((range * 2) + 1) * cutoff) - range)) * (1 / range));
            }

            fixed4 oldfrag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                fixed channel = GetChannel(col.r, _Cutoff, _CutoffRange);
            }
            
            v2f vert(appdata IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
#ifdef UNITY_HALF_TEXEL_OFFSET
                OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif
                OUT.color = IN.color * _Color;
                return OUT;
            }
 
            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = tex2D(_MainTex, IN.texcoord);
                fixed channel = GetChannel(color.r, _Cutoff, _CutoffRange);
                clip (channel * color.a);
                return (channel, channel, channel, channel * color.a);
            }
            ENDCG
        }
    }
}
