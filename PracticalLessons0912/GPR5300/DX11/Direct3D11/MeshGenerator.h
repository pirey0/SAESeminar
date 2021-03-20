#pragma once

#include "Mesh.h"

class MeshGenerator
{

public:
	static MeshData GenerateTestFace();
	static MeshData GenerateSphere(float radius, int stackCount, int sliceCount, bool reverse);
};

