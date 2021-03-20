cbuffer MatrixBuffer
{
    float4x4 WorldViewProjectionMatrix;
    float4x4 WorldMatrix;
    float4 Time;
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
    float4 localPosition : POSITION;
};

VertexOutput main(VertexInput IN)
{
    VertexOutput OUT;
    
    IN.position.w = 1.0f;
    OUT.position = mul(IN.position, WorldViewProjectionMatrix);

    // To make the skybox always be the same, regarles of camera position
    // I do z = w so that z/w = 1 (i.e., skydome always on far plane).
    // Because we dont change the depth function right now z=w would fail the depth test
    //so i use a number very close to 1 (The right solution is to set the depth function to LESS_EQUAL)
    OUT.position.z = OUT.position.w * 0.99999;

    //OUT.position = mul(IN.position, WorldViewProjectionMatrix).xyww;
    OUT.localPosition = IN.position;
    
    return OUT;
}