#include "MeshGenerator.h"

MeshData MeshGenerator::GenerateTestFace()
{
	MeshData data;
	
	data.Vertices =
	{
		// quad - trianglestrip or trianglelist with index buffer
		// position with normal & uv
		Vertex(-0.5f, 0.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f),
		Vertex(0.5f, 0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, -1.0f),
		Vertex(-0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, -1.0f),
		Vertex(0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 0.0f, 0.0f, -1.0f)

		//Vertex(-0.5f, 0.5f, 0.0f, XMFLOAT4(1.0f, 0.0f, 0.0f, 1.0f)),
		//Vertex(0.5f, 0.5f, 0.0f, XMFLOAT4(0.0f, 1.0f, 0.0f, 1.0f)),
		//Vertex(-0.5f, -0.5f, 0.0f, XMFLOAT4(0.0f, 0.0f, 1.0f, 1.0f)),
		//Vertex(0.5f, -0.5f, 0.0f, XMFLOAT4(1.0f, 0.0f, 1.0f, 1.0f)) 
	};


	data.Indices =
	{
		0, 1, 2,
		1, 3, 2
	};

	return data;
}

MeshData MeshGenerator::GenerateSphere(float radius, int stackCount, int sliceCount, bool reverse)
{
	MeshData meshData;

	//
	// Compute the vertices stating at the top pole and moving down the stacks.
	//

	// Poles: note that there will be texture coordinate distortion as there is
	// not a unique point on the texture map to assign to the pole when mapping
	// a rectangular texture onto a sphere.
	Vertex topVertex(0.0f, +radius, 0.0f, 0.0f, 0.0f, 0.0f, +1.0f, 0.0f);
	Vertex bottomVertex(0.0f, -radius, 0.0f, 0.0f, 1.0f, 0.0f, -1.0f, 0.0f);

	meshData.Vertices.push_back(topVertex);

	float phiStep = XM_PI / stackCount;
	float thetaStep = 2.0f * XM_PI / sliceCount;

	// Compute vertices for each stack ring (do not count the poles as rings).
	for (UINT i = 1; i <= stackCount - 1; ++i)
	{
		float phi = i * phiStep;

		// Vertices of ring.
		for (UINT j = 0; j <= sliceCount; ++j)
		{
			float theta = j * thetaStep;

			Vertex v;

			// spherical to cartesian
			v.position.x = radius * sinf(phi) * cosf(theta);
			v.position.y = radius * cosf(phi);
			v.position.z = radius * sinf(phi) * sinf(theta);

			// Partial derivative of P with respect to theta
			//v.TangentU.x = -radius * sinf(phi) * sinf(theta);
			//v.TangentU.y = 0.0f;
			//v.TangentU.z = +radius * sinf(phi) * cosf(theta);

			//XMVECTOR T = XMLoadFloat3(&v.TangentU);
			//XMStoreFloat3(&v.TangentU, XMVector3Normalize(T));

			XMVECTOR p = XMLoadFloat3(&v.position);
			XMStoreFloat3(&v.normal, XMVector3Normalize(p));

			v.uv.x = theta / XM_2PI;
			v.uv.y = phi / XM_PI;

			meshData.Vertices.push_back(v);
		}
	}

	meshData.Vertices.push_back(bottomVertex);

	//
	// Compute indices for top stack.  The top stack was written first to the vertex buffer
	// and connects the top pole to the first ring.
	//

	for (UINT i = 1; i <= sliceCount; ++i)
	{
		meshData.Indices.push_back(0);
		meshData.Indices.push_back(i + 1);
		meshData.Indices.push_back(i);
	}

	//
	// Compute indices for inner stacks (not connected to poles).
	//

	// Offset the indices to the index of the first vertex in the first ring.
	// This is just skipping the top pole vertex.
	UINT baseIndex = 1;
	UINT ringVertexCount = sliceCount + 1;
	for (UINT i = 0; i < stackCount - 2; ++i)
	{
		for (UINT j = 0; j < sliceCount; ++j)
		{
			meshData.Indices.push_back(baseIndex + i * ringVertexCount + j);
			meshData.Indices.push_back(baseIndex + i * ringVertexCount + j + 1);
			meshData.Indices.push_back(baseIndex + (i + 1) * ringVertexCount + j);

			meshData.Indices.push_back(baseIndex + (i + 1) * ringVertexCount + j);
			meshData.Indices.push_back(baseIndex + i * ringVertexCount + j + 1);
			meshData.Indices.push_back(baseIndex + (i + 1) * ringVertexCount + j + 1);
		}
	}

	//
	// Compute indices for bottom stack.  The bottom stack was written last to the vertex buffer
	// and connects the bottom pole to the bottom ring.
	//

	// South pole vertex was added last.
	UINT southPoleIndex = (UINT)meshData.Vertices.size() - 1;

	// Offset the indices to the index of the first vertex in the last ring.
	baseIndex = southPoleIndex - ringVertexCount;

	for (UINT i = 0; i < sliceCount; ++i)
	{
		meshData.Indices.push_back(southPoleIndex);
		meshData.Indices.push_back(baseIndex + i);
		meshData.Indices.push_back(baseIndex + i + 1);
	}

	if (reverse)
	{
		std::reverse(meshData.Indices.begin(), meshData.Indices.end());
	}

	return meshData;
}


