Texture2D MainTexture : register(t0);
SamplerState MainSampler;

TextureCube gCubeMap : register(t1);

struct Light
{
    float3 LightDirection;
    float LightIntensity;
    float4 AmbientColor;
    float4 DiffuseColor;
};

struct MaterialParameters
{
    float4 Ambient;
    float4 Diffuse;
    float4 Specular;
    float4 Reflect;
};

cbuffer PerRenderingBuffer: register(b0)
{
    Light LightData;
};

cbuffer PerMaterialBuffer : register(b1)
{
    MaterialParameters Material;
}

struct PixelInput
{
    float4 position : SV_POSITION;
    float2 uv : TEXCOORD;
    float3 normal : NORMAL;
};

float4 main(PixelInput IN) : SV_TARGET
{
    float4 mainTextureColor = MainTexture.Sample(MainSampler, IN.uv);
    
    float3 normalizedLight = normalize(LightData.LightDirection);
    float3 normalizedNormal = normalize(IN.normal);
    
    // diffuse light
    float diffuse = dot(-normalizedLight, normalizedNormal); // calculate light intensity
    diffuse = max(diffuse, 0.0f); // dot product can be negative
    diffuse *= LightData.LightIntensity; // adjust light intensity by multiplicator
    
    float4 color = mainTextureColor * saturate(LightData.AmbientColor + LightData.DiffuseColor * diffuse);

    return color;
}