// http://wiki.unity3d.com/index.php?title=Shader_Code

Shader "Pat Shaders/RatioProgressionDiagonalLeft"
{
	Properties
	{
		_CompletedColor ("Completed Tint", Color) = (0,0,0,0)
		_RemainingColor ("Remaining Tint", Color) = (1,1,1,1)
		
		_MainTex ("Texture", 2D) = "white" {}

		_Ratio ("Ratio", Range(0,1)) = 0.5
	}
	
	SubShader
	{
		// http://docs.unity3d.com/460/Documentation/Manual/SL-SubshaderTags.html
		Tags
		{ 
			"Queue"="Transparent-1"
			
		}
	
		// http://docs.unity3d.com/Manual/SL-Blend.html
		Blend One OneMinusSrcAlpha
	
		Pass
		{
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				
				fixed4 _CompletedColor;
				fixed4 _RemainingColor;
				float _Ratio;
				sampler2D _MainTex;

				// Data transferred from application to vertex program
				struct vertexInput
				{
					float4 vertex : POSITION;
					float2 texcoord0 : TEXCOORD0; // Normalized, from 0 to 1
				};
				
				// Data transferred from vertex program to fragment program
				struct fragmentInput
				{
					float4 position : SV_POSITION;
					float2 texcoord0 : TEXCOORD0; // Normalized, from 0 to 1
				};

				// Vertex program
				fragmentInput vert(vertexInput i)
				{
					fragmentInput o;
					
					o.position = mul(UNITY_MATRIX_MVP, i.vertex); // Model*View*Projection matrix
					o.texcoord0 = i.texcoord0;

					return o;
				}

				// Fragment program
				fixed4 frag(fragmentInput i) : COLOR
				{
					float4 color;
				
					if (i.texcoord0.x <= _Ratio && i.texcoord0.y >= 1 - _Ratio)
					{
						color = tex2D(_MainTex, i.texcoord0) * _CompletedColor;
					}
					else
					{
						color = tex2D(_MainTex, i.texcoord0) * _RemainingColor;
					}
					
					color.rgb = color.rgb * color.a;
					
					return color;
				}

			ENDCG
		}
	}
}
