#pragma once
class Time
{
public:
	int init();
	void update();
	void deInit();

	float getDeltaTime() { return _deltaTime; }
	float getTotalTime() { return _totalTime; }

private:
	float _deltaTime = 0.0f;
	float _lastTimestamp = 0.0f;
	float _totalTime = 0.0f;
};

