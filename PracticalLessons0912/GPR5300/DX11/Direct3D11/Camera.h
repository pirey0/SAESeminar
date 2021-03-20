#pragma once
#include <d3d11.h>
#include <DirectXMath.h>

using namespace DirectX;

class Camera
{
public:
	int init(INT screenWidth, INT screenHeight);
	void deInit();

	XMFLOAT4X4* getViewMatrix() { return &_viewMatrix; }
	XMFLOAT4X4* getProjectionMatrix() { return &_projectionMatrix; }

private:
	XMFLOAT4X4 _viewMatrix = {};
	XMFLOAT4X4 _projectionMatrix = {};
};

