#include <Windows.h>
#include <ctime>
#include <string>

#define WIN_WIDTH 640
#define WIN_HEIGHT 480
#define BALL_SPEED 6
#define clamp(v, minV, maxV) min(max(v,minV),maxV)

struct Entity
{
	int x;
	int y;
	int width;
	int height;
	int dx;
	int dy;

	RECT rect;

public:
	RECT* GetRect()
	{
		rect.left = x;
		rect.top = y;
		rect.right = x + width;
		rect.bottom = y + height;
		return &rect;
	}

	void Update()
	{
		x += dx;
		x = clamp(x, 0, WIN_WIDTH - width);
		y += dy;
		y = clamp(y, 0, WIN_HEIGHT - height);
	}
};

Entity player1{ 0,WIN_HEIGHT / 2, 30,100 };
Entity player2{ WIN_WIDTH - 30, WIN_HEIGHT / 2, 30,100 };
Entity ball{ WIN_WIDTH / 2, WIN_HEIGHT / 2, 20,20,BALL_SPEED,BALL_SPEED };
RECT textRect{ WIN_WIDTH / 3 ,0, WIN_WIDTH * 2 / 3,100 };
int scoreP1 = 0;
int scoreP2 = 0;
HBRUSH brush = CreateSolidBrush(RGB(255, 255, 255));

//Foreward declaration so that i don't have to declare these functions before the WinMain
void UpdateGameBehaviour();
void FollowerAIUpdate(Entity* e);
void PredictionAIUpdate(Entity* e, bool p1);

LRESULT CALLBACK WndProc(
	HWND hWnd, // handle to window instance
	UINT msg, // message id
	WPARAM wParam, // message information (single information)
	LPARAM lParam // additional informations (multiple informations)
);

int WINAPI WinMain(
	HINSTANCE hInstance, // handle to own application instance
	HINSTANCE hPrevInstance, // deprecated
	LPSTR szCmdLine, // command line
	int iCmdShow // in which state the application should be started (Normal, Minimised, Maximised)
)
{
	// 1. describe window class
	WNDCLASS wc = {};
	wc.hInstance = hInstance; // application handle
	wc.hbrBackground = CreateSolidBrush(RGB(0, 0, 0));
	//wc.hbrBackground = reinterpret_cast<HBRUSH>(COLOR_BACKGROUND + 1); // background color with the default os background color
	wc.hCursor = LoadCursor(nullptr, IDC_ARROW); // arrow cursor
	wc.hIcon = LoadIcon(nullptr, IDI_APPLICATION); // application icon
	wc.lpszClassName = TEXT("Pong");
	wc.style = CS_HREDRAW | CS_VREDRAW | CS_OWNDC;

	wc.lpfnWndProc = WndProc;

	// 2. register window class
	if (!RegisterClass(&wc)) return 10;

	// 3. calculating window size (optional)
	RECT r{ 100, 100, 100 + WIN_WIDTH, 100 + WIN_HEIGHT }; // left-top corner, right-bottom corner
	DWORD style = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU);
	AdjustWindowRect(&r, style, false); // add border width, etc. to window rect

	// 4. create window instance
	HWND hWnd = CreateWindow(
		wc.lpszClassName, // window class name
		wc.lpszClassName, // window title
		style, // visual window style
		r.left, r.top, // window position (origin left-top corner)
		r.right - r.left, r.bottom - r.top, // window size
		nullptr, // handle to parent window
		nullptr, // handle to menu instance
		hInstance, // application handle
		nullptr // optional parameters
	);

	if (!hWnd) return 15;

	// 5. show window
	ShowWindow(hWnd, iCmdShow);
	SetFocus(hWnd);


	// 6. run window (Main Game Loop)
	MSG msg = {};
	while (msg.message != WM_QUIT)
	{
		if (PeekMessage(&msg, nullptr, 0, UINT_MAX, PM_REMOVE))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
		else
		{
			//Update Logic
			PredictionAIUpdate(&player1, true); //Comment this out to control p1 with W/S
			PredictionAIUpdate(&player2, false); //Comment this out to control p2 with UP/DOWN
			player1.Update();
			player2.Update();
			ball.Update();
			UpdateGameBehaviour();
			//Draw
			InvalidateRect(hWnd, NULL, TRUE);
			UpdateWindow(hWnd);
			//Wait
			Sleep(5);
		}
	}

	return msg.wParam;
}

//AI that just follows the balls y
void FollowerAIUpdate(Entity* e)
{
	if (ball.y > e->y)
	{
		e->dy = 1;
	}
	else if (ball.y < e->y)
	{
		e->dy = -1;
	}
}

int PredictNextBallHitHeight(Entity* e, bool p1)
{
	Entity ballClone = Entity(*e);
	int maxTries = 10000;
	while (maxTries-- > 0)
	{
		//Collided with Top
		if (ballClone.y == 0)
		{
			ballClone.dy = BALL_SPEED;
		}
		//Collided with Bottom
		else if (ballClone.y == WIN_HEIGHT - ballClone.height)
		{
			ballClone.dy = -BALL_SPEED;
		}
		//Scored Left
		else if (ballClone.x <= 30)
		{
			if (p1)
				return ballClone.y;
			ballClone.dx = BALL_SPEED;
		}
		//Scored Right
		else if (ballClone.x == WIN_WIDTH - ballClone.width)
		{
			if (!p1)
				return ballClone.y;
			ballClone.dx = -BALL_SPEED;
		}

		ballClone.Update();
	}

	OutputDebugString(L"PredictNextBall: could not predict");
	return 0;
}

//AI that predicts the next landing spot of the ball
void PredictionAIUpdate(Entity* e, bool p1)
{
	int y = PredictNextBallHitHeight(&ball, p1) - e->width / 2;

	if (y > e->y)
	{
		e->dy = 1;
	}
	else if (y < e->y)
	{
		e->dy = -1;
	}
	else
	{
		e->dy = 0;
	}
}

//AABB overlap
bool EntitiesOverlap(Entity* e1, Entity* e2)
{
	RECT RectA = *e1->GetRect();
	RECT RectB = *e2->GetRect();

	bool C1 = RectA.left < RectB.right;
	bool C2 = RectA.right > RectB.left;
	bool C3 = RectA.top < RectB.bottom;
	bool C4 = RectA.bottom > RectB.top;

	bool overlap = (C1 && C2 && C3 && C4);
	return overlap;
}

void UpdateGameBehaviour()
{
	//Collided with Top
	if (ball.y == 0)
	{
		ball.dy = BALL_SPEED;
	}
	//Collided with Bottom
	else if (ball.y == WIN_HEIGHT - ball.height)
	{
		ball.dy = -BALL_SPEED;
	}
	//Scored Left
	else if (ball.x == 0)
	{
		ball.x = WIN_WIDTH / 2;
		ball.y = WIN_HEIGHT / 2;
		ball.dx = -BALL_SPEED;
		scoreP2++;
	}
	//Scored Right
	else if (ball.x == WIN_WIDTH - ball.width)
	{
		ball.x = WIN_WIDTH / 2;
		ball.y = WIN_HEIGHT / 2;
		ball.dx = BALL_SPEED;
		scoreP1++;
	}
	//Bounce off p1
	else if (EntitiesOverlap(&ball, &player1))
	{
		ball.dx = BALL_SPEED;
	}
	//Bounce off p2
	else if (EntitiesOverlap(&ball, &player2))
	{
		ball.dx = -BALL_SPEED;
	}
}

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
		if (wParam == VK_UP) player2.dy = -1;
		if (wParam == VK_DOWN) player2.dy = 1;
		if (wParam == 'W') player1.dy = -1;
		if (wParam == 'S') player1.dy = 1;
		return 0;

	case WM_KEYUP:
		if (wParam == VK_ESCAPE) PostQuitMessage(0);
		if (wParam == VK_UP) player2.dy = 0;
		if (wParam == VK_DOWN) player2.dy = 0;
		if (wParam == 'W') player1.dy = 0;
		if (wParam == 'S') player1.dy = 0;
		return 0;

	case WM_PAINT:
	{
		// old GDI (Graphic Device Interface) painting
		PAINTSTRUCT ps = {};
		
		BeginPaint(hWnd, &ps);

		FillRect(ps.hdc, player1.GetRect(), brush);
		FillRect(ps.hdc, player2.GetRect(), brush);
		FillRect(ps.hdc, ball.GetRect(), brush);

		std::wstring msg = std::to_wstring(scoreP1) + L" : " + std::to_wstring(scoreP2);
		DrawText(ps.hdc, msg.c_str(), msg.length(), &textRect, DT_CENTER);

		EndPaint(hWnd, &ps);

		return 0;
	}

	default:
		return DefWindowProc(hWnd, msg, wParam, lParam);
	}
}
