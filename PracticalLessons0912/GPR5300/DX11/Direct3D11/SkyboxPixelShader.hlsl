// Nonnumeric values cannot be added to a cbuffer.
TextureCube gCubeMap : register(t0);

SamplerState samTriLinearSam : register(s1)
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};

struct PixelInput
{
    float4 position : SV_POSITION;
    float4 localPosition : POSITION;
};


float4 main(PixelInput IN) : SV_TARGET
{
   return gCubeMap.Sample(samTriLinearSam, IN.localPosition);
}