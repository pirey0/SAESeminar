#pragma once
#include <d3d11.h>
#include <DirectXMath.h>
#include "Light.h"
#include "D3D.h"

#pragma comment(lib, "d3dcompiler.lib")

using namespace DirectX;

struct MaterialParameters
{
	XMFLOAT4 Ambient;
	XMFLOAT4 Diffuse;
	XMFLOAT4 Specular;
	XMFLOAT4 Reflect;
};

class Material
{
public:
	int init(D3D* d3d, LPCWSTR textureName, LPCWSTR vertexShaderName, LPCWSTR pixelShaderName, MaterialParameters parameters);
	virtual void render(ID3D11DeviceContext* pD3DDeviceContext, XMFLOAT4X4* worldMatrix, XMFLOAT4X4* viewMatrix, XMFLOAT4X4* projectionMatrix);
	void deInit();

	void setLight(ID3D11DeviceContext* pD3DDeviceContext, Light& lightData);

protected:
	// helper methods
	int createVertexShader(ID3D11Device* pD3DDevice, LPCWSTR name);
	int createPixelShader(ID3D11Device* pD3DDevice, LPCWSTR name);
	int createInputLayout(ID3D11Device* pD3DDevice, ID3DBlob* pBlob);
	int createMatrixBuffer(ID3D11Device* pD3DDevice);
	void setMatrixBuffer(ID3D11DeviceContext* pD3DDeviceContext, XMFLOAT4X4* worldMatrix, XMFLOAT4X4* viewMatrix, XMFLOAT4X4* projectionMatrix);
	virtual int createTextureAndSampler(ID3D11Device* pD3DDevice, LPCWSTR textureName);
	int createPixelShaderBuffer(ID3D11Device* pD3DDevice);
	int createMaterialParameterBuffer(ID3D11Device* pD3DDevice, ID3D11DeviceContext* pD3DDeviceContext, MaterialParameters parameters);

	// shaders
	ID3D11VertexShader* _pVertexShader = nullptr;
	ID3D11PixelShader* _pPixelShader = nullptr;

	// input layout
	ID3D11InputLayout* _pInputLayout = nullptr;

	// textures and samplers
	ID3D11ShaderResourceView* _pMainTexture = nullptr;
	ID3D11SamplerState* _pMainSampler = nullptr;

	// constant buffers
	ID3D11Buffer* _pMatrixBuffer = nullptr;
	struct MatrixBuffer
	{
		XMFLOAT4X4 worldViewProjectionMatrix;
		XMFLOAT4X4 worldMatrix;
		XMFLOAT4 time;
	};

	ID3D11Buffer* _pPixelShaderBuffer = nullptr;
	struct PixelShaderBuffer
	{
		Light lightData;
	};

	ID3D11Buffer* _pMaterialParametersBuffer = nullptr;
	

};
