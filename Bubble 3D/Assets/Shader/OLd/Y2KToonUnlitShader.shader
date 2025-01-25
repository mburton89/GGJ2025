Shader "Unlit/Y2KToonUnlitShader"
{
     Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)
        _LightDir ("Light Direction", Vector) = (0, 1, 0)
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.01, 0.1)) = 0.05
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Name "ToonShading"
            Tags { "LightMode"="UniversalForward" }
            Cull Back
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma target 4.5
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Define the vertex structure
            struct Attributes
            {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 position : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            // Properties
            float4 _BaseColor;
            float3 _LightDir;

            Varyings Vert(Attributes v)
            {
                Varyings o;

                // Transform position from object space to clip space
                o.position = TransformObjectToHClip(v.position);

                // Transform the normal from object space to world space
                o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));

                return o;
            }

            half4 Frag(Varyings i) : SV_Target
            {
                // Normalize the light direction
                float3 lightDir = normalize(_LightDir);

                // Compute lighting intensity using the dot product
                float intensity = saturate(dot(i.worldNormal, lightDir));

                // Apply toon shading with the base color and intensity
                half4 finalColor = _BaseColor * intensity;

                // If the result is black, make sure the base color is visible
                if (finalColor.rgb == float3(0,0,0))
                {
                    finalColor = half4(1, 0, 0, 1); // If black, show red to debug
                }

                return finalColor;
            }
            ENDHLSL
        }

        Pass
        {
            Name "Outline"
            Tags { "LightMode"="Always" }
            Cull Front
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma target 4.5
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Define the vertex structure
            struct Attributes
            {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 position : SV_POSITION;
            };

            // Properties
            float _OutlineWidth;
            float4 _OutlineColor;

            Varyings Vert(Attributes v)
            {
                Varyings o;

                // Transform position and add an offset to create the outline
                float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                float3 offset = worldNormal * _OutlineWidth;

                o.position = TransformObjectToHClip(v.position + float4(offset, 0));

                return o;
            }

            half4 Frag(Varyings i) : SV_Target
            {
                // Return the outline color
                return _OutlineColor;
            }
            ENDHLSL
        }
    }

    FallBack "Unlit/Color"
   
}
