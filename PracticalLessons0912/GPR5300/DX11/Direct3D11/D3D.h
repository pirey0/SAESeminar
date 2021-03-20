#pragma once
#include <d3d11.h>

#pragma comment(lib, "d3d11.lib")
#pragma comment(lib, "dxguid.lib")

class D3D
{
public: 
	int init(HWND hWnd, INT width, INT height, bool isWindowed);
	void beginScene(FLOAT red, FLOAT green, FLOAT blue);
	void endScene();
	void deInit();

	ID3D11Device* getDevice() { return _pD3DDevice; }
	ID3D11DeviceContext* getDeviceContext() { return _pD3DDeviceContext; }

private:
	// COM - Component Object Model
	ID3D11Device* _pD3DDevice = nullptr; // object for creating direct 3d objects
	ID3D11DeviceContext* _pD3DDeviceContext = nullptr; // object for modify renderpipeline
	IDXGISwapChain* _pD3DSwapChain = nullptr; // holds references front & back buffer
	ID3D11RenderTargetView* _pRenderTargetView = nullptr; // target to render on (here back buffer)
	ID3D11DepthStencilView* _pDepthStencilView = nullptr; // reference to depth & stencil buffer
	ID3D11RasterizerState* _pRasterizerState = nullptr; // properties for rasterizer stage
	D3D11_VIEWPORT _viewPort = {};
};
