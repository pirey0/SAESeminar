#include "Engine.h"


Engine::Engine(int size)
{
	m_size = size;
}

void Engine::Move(int dx, int dy)
{
	m_playerPosition.m_x = (m_playerPosition.m_x + dx + m_size) % m_size;
	m_playerPosition.m_y = (m_playerPosition.m_y + dy + m_size) % m_size;
}

std::wstring Engine::Draw() 
{
	std::wstring content = L"";

	for (size_t y = 0; y < m_size; y++)
	{
		for (size_t x = 0; x < m_size; x++)
		{
			content.append((x == m_playerPosition.m_x && y ==m_playerPosition.m_y) ? L"O" : L"_");
		}
		content.append(L"\n");
	}

	return content;
}
