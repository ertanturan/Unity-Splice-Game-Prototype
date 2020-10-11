Shader "HolloBall/Gradient" {
  Properties {
    _ColorTop ("Top Color", Color) = (1,1,1,1)
    _ColorBot ("Bottom Color", Color) = (1,1,1,1)
  }
  
  SubShader {
    Tags {"Queue"="Background" "IgnoreProjector"="True"}
    Cull Off ZWrite Off
    
    Pass {
      CGPROGRAM
      #pragma vertex vert  
      #pragma fragment frag
      #include "UnityCG.cginc"
      
      fixed4 _ColorTop;
      fixed4 _ColorBot;
      
      struct v2f {
        fixed4 pos : SV_POSITION;
        fixed4 texcoord : TEXCOORD0;
      };
      
      v2f vert (appdata_full v) {
        v2f o;
        o.pos = UnityObjectToClipPos (v.vertex);
        o.texcoord = v.texcoord;
        return o;
      }
      
      fixed4 frag (v2f i) : COLOR {
        fixed4 c = lerp(_ColorBot, _ColorTop, i.texcoord.y);
        return c;
      }
      ENDCG
    }
  }
}
