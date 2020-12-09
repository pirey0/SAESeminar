
#include <iostream> //Include to have access to console
#include "Engine.h"

int main() 
{

	/*
	std::cout << "Hello World!"; //Print to console with std::string
	std::wcout << L"Hello World!"; // Print to console with std::wstring

	// Use std::wstring for Strings (As this is the microsoft standard and will be used in DirectX)
	//You can make a wstring by writing strings like this: 
	// L"Text" <- std::wstring or const wchar_t[]
	//instead of this
	//"Text" <- std::string or const char[]
	*/

	Engine* engine = new Engine(10);

	while (true) 
	{
		system("cls"); // To clear the console, there is no nice Console.Clear method

		std::wstring draw = engine->Draw();
		std::wcout << draw;

		char key;
		std::cin.get(key); //Read input from console

		switch (key)
		{
		case 'a':
			engine->Move(-1, 0);
			break;

		case 'd':
			engine->Move(1, 0);
			break;

		case 'w':
			engine->Move(0, -1);
			break;

		case 's':
			engine->Move(0, 1);
			(*engine).Move(0, 1);
			break;
		}
	}

	delete engine;

	return 0;
}