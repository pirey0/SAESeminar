#pragma once
#include <DirectXMath.h>

using namespace DirectX;

struct Light
{
    XMFLOAT3 LightDirection;
    float LightIntensity;
    XMFLOAT4 AmbientColor;
    XMFLOAT4 DiffuseColor;
};
