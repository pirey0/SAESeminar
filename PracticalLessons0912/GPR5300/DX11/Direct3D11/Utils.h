#pragma once

template <class T>
void safeRelease(T* &obj)
{
	if (obj != nullptr)
	{
		obj->Release();
		obj = nullptr;
	}
}