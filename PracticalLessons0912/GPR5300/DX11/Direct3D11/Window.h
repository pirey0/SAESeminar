#pragma once
#include <Windows.h>

class Window
{
public:
	int init(HINSTANCE hInstance, INT width, INT height, int nCmdShow);
	bool run();
	void deInit();

	HWND getWindowHandle() { return _hWnd; }

private:
	HWND _hWnd = nullptr;
};

