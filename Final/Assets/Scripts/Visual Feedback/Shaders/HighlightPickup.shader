Shader "VGDCustom/HighlightPickup" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _Ramp ("Ramp", 2D) = "white" {}
      _Color ("Highlight Color", Color) = (0, 1, 0, 1)
      _OutlineColor ("Outline Color", Color) = (0, 0, 0, 0)
      _Outline ("Pin Lighting Outline", Range(0,1)) = 0.4
      _Amount ("Vertex Extrusion", Range(0,0.1)) = 0
	  _CrossFade ("Highlight Power", Range(0, 1)) = 1
    }
    SubShader {
    
    	Pass {
	 
		    Cull Front
		    Lighting Off
		    ZWrite On
		 
		    CGPROGRAM
		    #pragma vertex vert
		    #pragma fragment frag
		 
		    #include "UnityCG.cginc"
		    struct a2v
		    {
		        float4 vertex : POSITION;
		        float3 normal : NORMAL;
		    }; 
		 
		    struct v2f
		    {
		        float4 pos : POSITION;
		    };
		 
		 	float _Amount;
		    float _Outline;
		    float4 _OutlineColor;
		 	float _CrossFade;
		 
		    v2f vert (a2v v)
		    {
		         v2f o;
		         //v.vertex.xyz += v.normal * _Amount;
			     float4 pos = mul( UNITY_MATRIX_MV, v.vertex); 
			     float3 normal = mul( (float3x3)UNITY_MATRIX_IT_MV, v.normal);  
			     float nZ = -0.4;
			     normal.z = nZ; //flattening the z values of the normals that are back facing
			     pos = pos + float4(normalize(normal),0) * _Amount * _CrossFade;
			     o.pos = mul(UNITY_MATRIX_P, pos);
			 
			     return o;
		    }
		 
		    float4 frag (v2f IN) : COLOR
		    {
		        return _OutlineColor;
		    }
		 
		    ENDCG
	 
		}
	    
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		CGPROGRAM
		#pragma surface surf Ramp vertex:vert
      
		sampler2D _MainTex;
		sampler2D _Ramp;
		sampler2D _Noise;
		float4 _Color;
		float _Outline;
		float _CrossFade;
		float _NoiseScale;
		float _NoiseOffset;

		half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			half diff = NdotL * 0.5 + 0.5;
			half3 ramp = tex2D (_Ramp, float2(diff)).rgb;
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = s.Alpha;
			return c;
		}
		
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 viewDir;
		};
		
		
		void vert (inout appdata_full v) {
			//float3 random = (tex2Dlod(_Noise, v.texcoord * _NoiseScale + _NoiseOffset));
			//v.vertex.xyz += v.normal * _Amount;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half edge = saturate(dot (o.Normal, normalize(IN.viewDir))); 
			if (edge < _Outline)
				edge = edge / 4;
			else
				edge = 1;
			
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * edge * (1 - _CrossFade) + _Color * _CrossFade;
		}
		ENDCG
		
    }
    Fallback "Diffuse"
  }