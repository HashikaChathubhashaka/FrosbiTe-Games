Shader "IMI Games/Gradient Skybox"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
        _TopColor ("Top Color", Color) = (1, 1, 1, 1)
		_MiddleColor("Middle Color", Color) = (1, 1, 1, 1)
		_BottomColor("Bottom Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
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
			float4 _TopColor;
			float4 _MiddleColor;
			float4 _BottomColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float j = i.uv.y;

				//UV Y range: Top -> 0.25, Bottom -> -0.75

				////Bottom section
				//if (j <= -0.5) {
				//	float v = j < -0.75 ? 0 : ( j + 0.75) * 4;
				//	return lerp(_BottomColor, _MiddleColor, v);
				//}
				////Top section
				//else if (j >= 0) {
				//	return lerp(_MiddleColor, _TopColor, j * 4);
				//}
				////Middle section
				//else {
				//	return _MiddleColor;
				//}

				//return j <= -0.5 ? lerp(_BottomColor, _MiddleColor, j < -0.75 ? 0 : ( j + 0.75) * 4) : j >= 0 ? lerp(_MiddleColor, _TopColor, j * 4) : _MiddleColor;

				//UV Y range: Top -> 0.5, Bottom -> -0.5

				////Bottom section
				//if (j <= -0.25) {
				//	float v = j < -0.5 ? 0 : ( j + 0.5) * 4;
				//	return lerp(_BottomColor, _MiddleColor, v);
				//}
				////Top section
				//else if (j >= 0.25) {
				//	return lerp(_MiddleColor, _TopColor, (j - 0.25) * 4);
				//}
				////Middle section
				//else {
				//	return _MiddleColor;
				//}

				return j <= -0.25 ? lerp(_BottomColor, _MiddleColor, j < -0.5 ? 0 : (j + 0.5) * 4) : j >= 0 ? lerp(_MiddleColor, _TopColor, (j ) * 4) : _MiddleColor;
            }
            ENDCG
        }
    }
}
