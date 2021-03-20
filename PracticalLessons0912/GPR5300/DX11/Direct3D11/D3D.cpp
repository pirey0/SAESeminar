#include "D3D.h"
#include "Utils.h"

int D3D::init(HWND hWnd, INT width, INT height, bool isWindowed)
{
    // 1. create device, device context & swap chain
    DXGI_SWAP_CHAIN_DESC desc = {};
    desc.BufferCount = 1;
    desc.BufferDesc.Width = width;
    desc.BufferDesc.Height = height;
    desc.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;
    desc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
    desc.SampleDesc.Count = 1;
    desc.SwapEffect = DXGI_SWAP_EFFECT_DISCARD;
    desc.OutputWindow = hWnd;
    desc.Windowed = isWindowed;

    D3D_FEATURE_LEVEL supportedLevels[] = { 
        D3D_FEATURE_LEVEL_12_1,
        D3D_FEATURE_LEVEL_12_0,
        D3D_FEATURE_LEVEL_11_1,
        D3D_FEATURE_LEVEL_11_0,
        D3D_FEATURE_LEVEL_10_1,
        D3D_FEATURE_LEVEL_10_0
    };

    D3D_FEATURE_LEVEL choosenLevel;

    HRESULT hr = D3D11CreateDeviceAndSwapChain(
        nullptr, // graphic adapter (here primary one)
        D3D_DRIVER_TYPE_HARDWARE, // driver type, hardware or software rendering?
        nullptr, // software renderer if driver type is software
        0, // optional flags
        supportedLevels, 6, // supported direct 3d versions (versions & array size)
        D3D11_SDK_VERSION, // api version the application was build with
        &desc, &_pD3DSwapChain, &_pD3DDevice,
        &choosenLevel, // optional parameter for choosen feature level
        &_pD3DDeviceContext
    );
    if (FAILED(hr)) return 20;

    // 2. create render target view
    ID3D11Texture2D* pBackBuffer = nullptr;
    hr = _pD3DSwapChain->GetBuffer(0, __uuidof(ID3D11Texture2D), reinterpret_cast<void**>(&pBackBuffer));
    if (FAILED(hr)) return 22;

    hr = _pD3DDevice->CreateRenderTargetView(pBackBuffer, nullptr, &_pRenderTargetView);
    if (FAILED(hr)) return 24;

    safeRelease<ID3D11Texture2D>(pBackBuffer);

    // 3. create depth stencil view
    ID3D11Texture2D* pDepthStencilTexture = nullptr;
    D3D11_TEXTURE2D_DESC depthStencilTextureDesc = {};
    depthStencilTextureDesc.BindFlags = D3D11_BIND_DEPTH_STENCIL;
    depthStencilTextureDesc.Width = width;
    depthStencilTextureDesc.Height = height;
    depthStencilTextureDesc.Format = DXGI_FORMAT_D24_UNORM_S8_UINT;
    depthStencilTextureDesc.ArraySize = 1;
    depthStencilTextureDesc.SampleDesc.Count = 1;

    hr = _pD3DDevice->CreateTexture2D(&depthStencilTextureDesc, nullptr, &pDepthStencilTexture);
    if (FAILED(hr)) return 26;

    hr = _pD3DDevice->CreateDepthStencilView(pDepthStencilTexture, nullptr, &_pDepthStencilView);
    if (FAILED(hr)) return 28;

    safeRelease<ID3D11Texture2D>(pDepthStencilTexture);

    // 4. create rasterizer state
    D3D11_RASTERIZER_DESC rsDesc = {};
    rsDesc.FillMode = D3D11_FILL_SOLID;
    rsDesc.CullMode = D3D11_CULL_BACK;

    hr = _pD3DDevice->CreateRasterizerState(&rsDesc, &_pRasterizerState);
    if (FAILED(hr)) return 29;

    // 5. create viewport
    _viewPort.Width = width;
    _viewPort.Height = height;
    _viewPort.TopLeftX = 0.0f;
    _viewPort.TopLeftY = 0.0f;
    _viewPort.MinDepth = 0.0f;
    _viewPort.MaxDepth = 1.0f;

    // 6. prepare the rendering pipeline
    _pD3DDeviceContext->OMSetRenderTargets(1, &_pRenderTargetView, _pDepthStencilView);
    _pD3DDeviceContext->RSSetState(_pRasterizerState);
    _pD3DDeviceContext->RSSetViewports(1, &_viewPort);

    return 0;
}

void D3D::beginScene(FLOAT red, FLOAT green, FLOAT blue)
{
    // clear back buffer with solid color
    const FLOAT color[] = { red, green, blue, 1.0f };
    _pD3DDeviceContext->ClearRenderTargetView(_pRenderTargetView, color);
    _pD3DDeviceContext->ClearDepthStencilView(_pDepthStencilView, D3D11_CLEAR_DEPTH, 1.0f, 0xffffff);
}

void D3D::endScene()
{
    // swap back with front buffer
    _pD3DSwapChain->Present(0, 0);
}

void D3D::deInit()
{
    safeRelease<ID3D11RasterizerState>(_pRasterizerState);
    safeRelease<ID3D11DepthStencilView>(_pDepthStencilView);
    safeRelease<ID3D11RenderTargetView>(_pRenderTargetView);
    safeRelease<ID3D11Device>(_pD3DDevice);
    safeRelease<ID3D11DeviceContext>(_pD3DDeviceContext);
    safeRelease<IDXGISwapChain>(_pD3DSwapChain);
}
