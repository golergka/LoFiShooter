Shader "Pixelated/Diffuse" {

  Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _Rounding("Rounding", Float) = 10.0
  }

  SubShader {

    Tags { "RenderType" = "Opaque" }

    CGPROGRAM

    #pragma surface surf Lambert

    struct Input {
        float2 uv_MainTex;
    };

    sampler2D _MainTex;

    float _Rounding;

    void surf (Input IN, inout SurfaceOutput o) {

      float2 uv_Rounded = IN.uv_MainTex;

      uv_Rounded.x *= _Rounding;
      uv_Rounded.y *= _Rounding;

      uv_Rounded.x = round(uv_Rounded.x);
      uv_Rounded.y = round(uv_Rounded.y);

      uv_Rounded.x /= _Rounding;
      uv_Rounded.y /= _Rounding;

      o.Albedo = tex2D (_MainTex, uv_Rounded).rgb;

      // float3 albedo;

      // albedo.x = uv_Rounded.x;
      // albedo.y = uv_Rounded.y;
      // albedo.z = 0;

      // o.Albedo = albedo;

    }
    
    ENDCG

  }

  Fallback "Diffuse"

}