#include "Window.h"

LRESULT CALLBACK WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
	case WM_CLOSE:
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;

	case WM_KEYDOWN:
		if (wParam == VK_ESCAPE) PostQuitMessage(0);
		return 0;

	default:
		return DefWindowProc(hWnd, msg, wParam, lParam);
	}
}

int Window::init(HINSTANCE hInstance, INT width, INT height, int nCmdShow)
{
	WNDCLASS wc = {};
	wc.hInstance = hInstance;
	wc.hbrBackground = reinterpret_cast<HBRUSH>(COLOR_BACKGROUND + 1);
	wc.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wc.hIcon = LoadIcon(nullptr, IDI_APPLICATION);
	wc.lpszClassName = TEXT("Direct3D 11");
	wc.style = CS_HREDRAW | CS_VREDRAW | CS_OWNDC;
	wc.lpfnWndProc = WndProc;

	if (!RegisterClass(&wc)) return 10;

	INT halfScreenWidth = GetSystemMetrics(SM_CXSCREEN) / 2;
	INT halfScreenHeight = GetSystemMetrics(SM_CYSCREEN) / 2;
	INT halfWidth = width / 2;
	INT halfHeight = height / 2;
	RECT r{ halfScreenWidth - halfWidth, halfScreenHeight - halfHeight, 
			halfScreenWidth + halfWidth, halfScreenHeight + halfHeight };
	DWORD style = WS_OVERLAPPEDWINDOW;
	AdjustWindowRect(&r, style, false);

	_hWnd = CreateWindow(wc.lpszClassName, wc.lpszClassName, style, 
		r.left, r.top, r.right - r.left, r.bottom - r.top, nullptr, nullptr, hInstance, nullptr);

	if (!_hWnd) return 15;

	ShowWindow(_hWnd, nCmdShow);
	SetFocus(_hWnd); 
	
	return 0;
}

bool Window::run()
{
	MSG msg = {};
	if (PeekMessage(&msg, nullptr, 0, UINT_MAX, PM_REMOVE))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);

		if (msg.message == WM_QUIT) return false;
	}
	
	return true;
}

void Window::deInit()
{
	// TODO: destroy window if it is not destroyed
}
