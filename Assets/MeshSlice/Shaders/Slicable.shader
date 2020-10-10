Shader "Custom/Slicable"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _SliceColor ("Slice color", Color) = (1,1,1,1)
        _SliceWidth("Slice line width", Range(0, 0.1)) = 0.05
        _Point1("Point 1", Vector) = (0, 0, 0, 0)
        _Point2("Point 1", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        fixed4 _Color;
        fixed4 _SliceColor;
        fixed _SliceWidth;

        fixed3 _Point1;
        fixed3 _Point2;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed x0 = IN.worldPos.x;
            fixed z0 = IN.worldPos.z;

            fixed x1 = _Point1.x;
            fixed z1 = _Point1.z;

            fixed x2 = _Point2.x;
            fixed z2 = _Point2.z;

            fixed res = abs((z2 - z1) * x0 - (x2 - x1) * z0 + x2 * z1 - z2 * x1);
            res /= sqrt((z2 - z1) * (z2 - z1) + (x2 - x1) * (x2 - x1));

            if(res <= _SliceWidth / 2 && res >= -_SliceWidth / 2)
            {
                o.Albedo = _SliceColor;
            }
            else
            {
                o.Albedo = _Color;
            }
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
