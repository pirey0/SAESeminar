#include "Time.h"
#include <Windows.h>
#include <string>

#pragma comment(lib, "Winmm.lib")

using namespace std;

int Time::init()
{
	_lastTimestamp = timeGetTime() * 0.001f; // convert ms into s

	return 0;
}

void Time::update()
{
	float actualTimestamp = timeGetTime() * 0.001f;
	_deltaTime = actualTimestamp - _lastTimestamp;
	_totalTime += _deltaTime;
	_lastTimestamp = actualTimestamp;

#if _DEBUG
#if UNICODE
	wstring output = TEXT("deltaTime: ") + to_wstring(_deltaTime) + TEXT("\n");
#else // UNICODE
	string output = TEXT("deltaTime: ") + to_string(_deltaTime) + TEXT("\n");
#endif // UNICODE
	OutputDebugString(output.c_str());
#endif // _DEBUG

}

void Time::deInit()
{
}
