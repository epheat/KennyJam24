Shader "Custom/WaterSurfaceShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveSpeed ("Wave Speed", Range(0.1, 10.0)) = 1.0
        _WaveHeight ("Wave Height", Range(0.0, 1.0)) = 0.1
        _TileX ("Tile X", Float) = 1.0
        _TileY ("Tile Y", Float) = 1.0
        _DarkColor ("Dark Color", Color) = (0, 0, 0.2, 1)
        _LightColor ("Light Color", Color) = (0.5, 0.5, 1, 1)
        _Daylight ("Daylight", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _WaveSpeed;
            float _WaveHeight;
            float _TileX;
            float _TileY;
            float4 _DarkColor;
            float4 _LightColor;
            float4 _Daylight;

            v2f vert (appdata_t v)
            {
                v2f o;
                float time = _Time.y * _WaveSpeed;
                float wave = sin(v.vertex.x * 0.1 + time) * cos(v.vertex.y * 0.1 + time) * _WaveHeight;

                v.vertex.y += wave;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x *= _TileX;
                o.uv.y *= _TileY;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float luminance = dot(col.rgb, float3(0.299, 0.587, 0.114));
                fixed4 baseColor = lerp(_DarkColor, _LightColor, luminance);

                // Apply daylight
                float4 nightColor = baseColor * 0.5;
                float4 dayColor = baseColor * 1.5;
                fixed4 resultColor = lerp(nightColor, dayColor, _Daylight);

                return resultColor;            
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
