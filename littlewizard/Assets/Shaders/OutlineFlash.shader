Shader "Custom/OutlineFlash"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_FlashColor ("Flash Color", Color) = (1,1,1,1)
		_FlashAmount ("Flash Amount", Range (0,1)) = 0
		
		_OutlineColor("OutlineColor", Color) = (1,1,1,1)
		_Brightness("Outline Brightness", Range(0,8)) = 3.2
        _Width("Outline Width", Range(0,0.05)) = 0.003 
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
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;
			fixed4 _FlashColor;
			
			fixed4 _OutlineColor;
			float _Width;
			float _Brightness;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			float _FlashAmount;


			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				
				 // Move sprite in 4 directions according to width, we only care about the alpha
                float spriteLeft = tex2D(_MainTex, IN.texcoord + float2(_Width, 0)).a;
                float spriteRight = tex2D(_MainTex, IN.texcoord - float2(_Width,  0)).a;
                float spriteBottom = tex2D(_MainTex, IN.texcoord + float2( 0 ,_Width)).a;
                float spriteTop = tex2D(_MainTex, IN.texcoord - float2( 0 , _Width)).a;
				
				 // then combine
                float result = (spriteRight + spriteLeft + spriteTop+ spriteBottom);
                // delete original alpha to only leave outline
                result *= (1-c.a);
                // add color and brightness
                float4 outlines = result * _OutlineColor* _Brightness;     
				
				c.rgb = lerp(c.rgb, _FlashColor.rgb, _FlashAmount);
				c.rgb *= c.a;
				
				if (result > 0) {
					c.rgba = c.rgba + fixed4(1, 1, 1, 0.5) * outlines;
				}
				else {
					c.rgb = c.rgb + outlines;//  +outlines;
				}
				
				return c;
			}
		ENDCG
		}
	}
}
