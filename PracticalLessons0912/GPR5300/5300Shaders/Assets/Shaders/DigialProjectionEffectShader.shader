/* Useful links:
https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
https://docs.unity3d.com/Manual/SL-SubShaderTags.html
https://docs.unity3d.com/Manual/SL-Properties.html
https://docs.unity3d.com/Manual/SL-Blend.html
https://developer.download.nvidia.com/cg/sin.html
https://docs.unity3d.com/Manual/SL-ShaderSemantics.html
https://unity3d.com/get-unity/download/archive

*/
Shader "Unlit/DigialProjectionEffectShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Alpha("Alpha", Range(0,1)) = 1
		_Color("Color", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
	}
		SubShader
		{
			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			LOD 100
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha


			Pass
			{
				CGPROGRAM

				#pragma vertex vertexShader
				#pragma fragment pixelShader

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
					float yPos : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Alpha;
				fixed4 _Color;
				fixed4 _Color2;

				v2f vertexShader(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex) + float4(sin((_Time.w * 0.1 + v.vertex.y) % 0.5) * 0.1,0,0,0);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.yPos = v.vertex.y;
					return o;
				}

				fixed4 pixelShader(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = tex2D(_MainTex, i.uv) * _Color;
					
					col.r = i.yPos;
					col.a = _Alpha;
					return col;
				}
				ENDCG
			}
		}
}
