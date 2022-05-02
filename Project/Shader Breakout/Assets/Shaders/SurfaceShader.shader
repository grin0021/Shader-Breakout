Shader "SurfaceShader" {
	Properties{
		// Properties of the material
		_MainTex("Base (RGB)", 2D) = "white" {}
		_FOVColor("Field Of View Color", Color) = (1, 1, 1)
		_MainColor("MainColor", Color) = (1, 1, 1)
		_BallPosition("BallPosition",  Vector) = (0,0,0)
		_PaddlePosition("PaddlePosition",  Vector) = (0,0,0)
	}
		SubShader{
		Tags{ "RenderType" = "Diffuse" }
		// https://docs.unity3d.com/Manual/SL-SurfaceShaders.html
		CGPROGRAM
#pragma surface surf Lambert

	sampler2D _MainTex;
		//https://docs.unity3d.com/Manual/SL-DataTypesAndPrecision.html
		fixed3 _FOVColor; //Precision
		fixed3 _MainColor;
		float3 _BallPosition;
		float3 _PaddlePosition;
		float _PaddleWidth;
		float _BallRadius;

		// Values that interpolated from vertex data.
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		// Barycentric coordinates
		// http://mathworld.wolfram.com/BarycentricCoordinates.html
		bool isPointInRectangle(float2 basePoint, float2 pointInQuestion)
		{
			if (pointInQuestion.x < basePoint.x - _PaddleWidth || pointInQuestion.x > basePoint.x + _PaddleWidth)
			{
				return false;
			}

			if (pointInQuestion.y < basePoint.y - _PaddleWidth / 10 || pointInQuestion.y > basePoint.y + _PaddleWidth / 10)
			{
				return false;
			}

			return true;
		}

		bool isPointInTheCircle(float2 circleCenterPoint, float2 thisPoint)
		{
			return distance(circleCenterPoint, thisPoint) <= _BallRadius;
		}

		void surf(Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			float3 basePoint = _BallPosition.xyz;
			//basePoint.y = 0;

			float3 pointInQuestion = IN.worldPos;

			c.rgb *= _MainColor;

			if (isPointInTheCircle(basePoint.xy, pointInQuestion.xz) || isPointInRectangle(_PaddlePosition.xy, pointInQuestion.xz))
			{
				o.Albedo = c.rgb * _FOVColor;
			}
			else
			{
				o.Albedo = c.rgb;
			}

			o.Alpha = c.a;
		}
		ENDCG
	}
			FallBack "Diffuse" //If we cannot use the subshader on specific hardware we will fallback to Diffuse shader
}