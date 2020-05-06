Shader "Custom/PlanetShader"
{
	Properties
	{
		_CloudsTex("First Clouds Texture", 2D) = "white"{}
		_SecondTex("Second CloudsTexture", 2D) = "white"{}
		_Color1("Color A", Range(0, 1)) = 1
		_Color2("Color B", Range(0, 1)) = 5
		_Color3("Color C", Range(0, 1)) = 10
		_Emission("Emission", Range(0, 1)) = .5 
		_ScrollX("ScrollX Value", Range(-10, 10)) = 1
		_ScrollY("ScrollY Value", Range(-10, 10)) = 5
	}
		SubShader
		{
			CGPROGRAM
#pragma surface surf Lambert
			sampler2D _CloudsTex;
		sampler2D _SecondTex;
		float _Color1;
		float _Color2;
		float _Color3;
		float _Emission;
		float _ScrollX;
		float _ScrollY;

			struct Input
			{
			float2 uv_CloudsTex;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				_ScrollX *= _Time;
				_ScrollY *= _Time;
				float3 firstTex = (tex2D(_CloudsTex, IN.uv_CloudsTex + float2(_ScrollX, _ScrollY))).rgb;
				firstTex.r *= _Color1;
				float3 secondTex = (tex2D(_SecondTex, IN.uv_CloudsTex + float2(_ScrollX / 2.0, _ScrollY / 2.0))).rgb;
				secondTex.g *= _Color2;
				secondTex.b *= _Color3;
				o.Emission = secondTex.g * _Emission;
				o.Albedo = (firstTex + secondTex) / 2.0;
			}
			ENDCG
		}

}