#include "GameObject.h"
#include "Vertex.h"
#include "Utils.h"

int GameObject::init(ID3D11Device* pD3DDevice, Mesh* mesh, Material* material)
{
	_material = material;
	_mesh = mesh;
	XMStoreFloat4x4(&_worldMatrix, XMMatrixIdentity());
	return 0;
}

void GameObject::update(float deltaTime)
{
	XMMATRIX translation = XMMatrixTranslation(_position.x, _position.y, _position.z);
	XMMATRIX rotation = XMMatrixRotationRollPitchYaw(_euler.x, _euler.y, _euler.z);
	XMMATRIX scale = XMMatrixScaling(1.0f, 1.0f, 1.0f);

	XMStoreFloat4x4(&_worldMatrix, scale * rotation * translation);
}

void GameObject::render(ID3D11DeviceContext* pD3DDeviceContext, XMFLOAT4X4* viewMatrix, XMFLOAT4X4* projectionMatrix)
{
	_material->render(pD3DDeviceContext, &_worldMatrix, viewMatrix, projectionMatrix);
	_mesh->render(pD3DDeviceContext);
}

void GameObject::SetPosition(float x, float y, float z)
{
	_position = { x, y, z };
}

XMFLOAT3 GameObject::GetPosition()
{
	return _position;
}