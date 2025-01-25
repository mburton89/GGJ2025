Shader "Unlit/Y2KToonUnlitShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}          // Texture for the object
        _Color ("Base Color", Color) = (1, 1, 1, 1)         // Tint for the texture
        _Ramp ("Color Ramp", 2D) = "white" {}              // Gradient texture for toon shading
        _LightDir ("Light Direction", Vector) = (0, 1, 0)   // Direction of the light
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1) // Color of the outline
        _OutlineWidth ("Outline Width", Range(0.01, 0.1)) = 0.05 // Outline thickness
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "ToonShading"
            Tags { "LightMode"="Always" }
            Cull Back
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _Color;
            sampler2D _Ramp;
            float3 _LightDir;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the main texture
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Calculate light intensity (dot product of normal and light direction)
                float3 lightDir = normalize(_LightDir);
                float intensity = saturate(dot(i.worldNormal, lightDir));

                // Sample the color ramp texture based on light intensity
                float rampValue = tex2D(_Ramp, float2(intensity, 0)).r;

                // Combine the ramp result with the texture and base color
                fixed4 finalColor = texColor * _Color * rampValue;

                return finalColor;
            }
            ENDCG
        }

        Pass
        {
            Name "Outline"
            Tags { "LightMode"="Always" }

            Cull Front
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 outlinePos : POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert(appdata v)
            {
                v2f o;

                // Offset the vertex position along the normal for the outline
                float3 normal = UnityObjectToWorldNormal(v.normal);
                float3 offset = normal * _OutlineWidth;

                o.outlinePos = UnityObjectToClipPos(v.vertex + float4(offset, 0));
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Set the outline color
                return _OutlineColor;
            }
            ENDCG
        }
    }

    FallBack "Unlit"
}
