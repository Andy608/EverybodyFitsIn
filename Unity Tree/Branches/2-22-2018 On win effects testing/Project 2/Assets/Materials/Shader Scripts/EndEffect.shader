﻿Shader "Unlit/EndEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//Normalize pixel coords
				float2 uv = -1.0 + 2.0* i.vertex.xy / _ScreenParams.xy;

				float frequency = 3.0;
				float index = _Time.y + uv.x;

				float red = sin(frequency * index + 0.0) * 0.5 + 0.5;
				float green = sin(frequency * index + 2.0) * 0.5 + 0.5;
				float blue = sin(frequency * index + 4.0) * 0.5 + 0.5;

				return float4(red, green, blue, 1.0);

			}
			ENDCG
		}
	}
}