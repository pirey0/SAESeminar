#pragma once
#include <d3d11.h>
#include <DirectXMath.h>
#include <vector>
#include "Vertex.h"

using namespace DirectX;

struct MeshData
{
	std::vector<Vertex> Vertices;
	std::vector<WORD> Indices;
};

class Mesh
{
public:
	int init(ID3D11Device* pD3DDevice, MeshData* data);
	void render(ID3D11DeviceContext* pD3DDeviceContext);
	void deInit();

private:
	int initVertexBuffer(ID3D11Device* pD3DDevice);
	int initIndexBuffer(ID3D11Device* pD3DDevice);

	ID3D11Buffer* _pVertexBuffer = nullptr;
	ID3D11Buffer* _pIndexBuffer = nullptr;

	MeshData* _meshData = nullptr;

	UINT _vertexCount = 0; // amount of vertices
	UINT _vertexStride = 0; // size of one vertex instance in bytes
	UINT _indexCount = 0; // amount of indices

};
