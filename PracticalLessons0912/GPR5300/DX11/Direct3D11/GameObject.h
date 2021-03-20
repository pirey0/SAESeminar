#pragma once
#include <d3d11.h>
#include <DirectXMath.h>
#include "Mesh.h"
#include "Material.h"
using namespace DirectX;

class GameObject
{
public:

	int init(ID3D11Device* pD3DDevice, Mesh* mesh, Material* material);
	void update(float deltaTime);
	void render(ID3D11DeviceContext* pD3DDeviceContext, XMFLOAT4X4* viewMatrix, XMFLOAT4X4* projectionMatrix);
	void SetPosition(float x, float y, float z);
	XMFLOAT3 GetPosition();

	XMFLOAT4X4* getWorldMatrix() { return &_worldMatrix; }

private:

	XMFLOAT3 _position;
	XMFLOAT3 _euler;

	Mesh* _mesh = nullptr;
	Material* _material = nullptr;
	XMFLOAT4X4 _worldMatrix = {};
};
