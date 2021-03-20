#pragma once
#include "Material.h"

class DDSMaterial : public Material
{

protected:

	virtual int createTextureAndSampler(ID3D11Device* pD3DDevice, LPCWSTR textureName) override;

};

