cbuffer MatrixBuffer
{
    float4x4 WorldViewProjectionMatrix;
    float4x4 WorldMatrix;
};

struct VertexInput
{
    float4 position : POSITION;
    float2 uv : TEXCOORD;
    float4 normal : NORMAL;
};

struct VertexOutput
{
    float4 position : SV_POSITION;
    float2 uv : TEXCOORD;
    float3 normal : NORMAL;
};

VertexOutput main(VertexInput IN)
{
    VertexOutput OUT;
    
    IN.position.w = 1.0f;
    OUT.position = mul(IN.position, WorldViewProjectionMatrix);
    OUT.uv = IN.uv;
    
    IN.normal.w = 0.0f;
    OUT.normal = mul(IN.normal, WorldMatrix).xyz;
    
    return OUT;
}