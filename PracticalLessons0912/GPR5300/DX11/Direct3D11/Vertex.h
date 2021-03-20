#pragma once
#include <DirectXMath.h>

using namespace DirectX;

struct Vertex
{
	XMFLOAT3 position; // position
	XMFLOAT4 color; // color
	XMFLOAT3 normal; // normal
	XMFLOAT2 uv; // texture coordinates
	
	Vertex() : position(0, 0, 0), color(1.0f, 1.0f, 1.0f, 1.0f), normal(0.0f, 0.0f, 0.0f), uv(0.0f, 0.0f) {}

	Vertex(FLOAT x, FLOAT y, FLOAT z) : position(x, y, z), color(1.0f, 1.0f, 1.0f, 1.0f), normal(0.0f, 0.0f, 0.0f), uv(0.0f, 0.0f) {}
	Vertex(FLOAT x, FLOAT y, FLOAT z, XMFLOAT4 c) : position(x, y, z), color(c), normal(0.0f, 0.0f, 0.0f), uv(0.0f, 0.0f) {}
	Vertex(FLOAT x, FLOAT y, FLOAT z, FLOAT u, FLOAT v) : position(x, y, z), color(1.0f, 1.0f, 1.0f, 1.0f), normal(0.0f, 0.0f, 0.0f), uv(u, v) {}
	Vertex(FLOAT x, FLOAT y, FLOAT z, FLOAT u, FLOAT v, FLOAT nx, FLOAT ny, FLOAT nz) : position(x, y, z), color(1.0f, 1.0f, 1.0f, 1.0f), normal(nx, ny, nz), uv(u, v) {}
};