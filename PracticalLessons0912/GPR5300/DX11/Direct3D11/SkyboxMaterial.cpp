#include "SkyboxMaterial.h"
#include "DDSTextureLoader.h"


int DDSMaterial::createTextureAndSampler(ID3D11Device* pD3DDevice, LPCWSTR textureName)
{
	HRESULT hr = CreateDDSTextureFromFile(pD3DDevice, textureName, nullptr, &_pMainTexture);
	if (FAILED(hr)) return 48;

	D3D11_SAMPLER_DESC desc = {};
	desc.AddressU = D3D11_TEXTURE_ADDRESS_WRAP;
	desc.AddressV = D3D11_TEXTURE_ADDRESS_WRAP;
	desc.AddressW = D3D11_TEXTURE_ADDRESS_WRAP;
	desc.Filter = D3D11_FILTER_MIN_MAG_MIP_LINEAR;

	hr = pD3DDevice->CreateSamplerState(&desc, &_pMainSampler);
	if (FAILED(hr)) return 49;

	return 0;
}
