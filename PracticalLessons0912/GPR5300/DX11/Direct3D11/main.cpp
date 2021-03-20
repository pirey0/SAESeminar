#include <Windows.h>
#include "Window.h"
#include "D3D.h"
#include "Mesh.h"
#include "Camera.h"
#include "Time.h"
#include "Material.h"
#include "SkyboxMaterial.h"
#include "Light.h"
#include "GameObject.h"
#include "MeshGenerator.h"
#include <string>


int ThrowErrorMSGBox(int code)
{
	MessageBox(NULL, std::to_wstring(code).c_str(), L"Error", 0);
	return code;
}

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR szCmdLine, int nCmdShow)
{
	INT width = 800;
	INT height = 600;
	bool isWindowed = true;

	// 1. create window
	Window window;
	int error = window.init(hInstance, width, height, nCmdShow);
	if (error != 0) return ThrowErrorMSGBox(error);

	// 2. create Direct3D 11 interface
	D3D d3d;
	error = d3d.init(window.getWindowHandle(), width, height, isWindowed);
	if (error != 0) return ThrowErrorMSGBox(error);

	// 3. create mesh
	Mesh sphere;
	MeshData sphereData = MeshGenerator::GenerateSphere(1, 20, 20, false);
	error = sphere.init(d3d.getDevice(), &sphereData);
	if (error != 0) return ThrowErrorMSGBox(error);

	Mesh skyBoxSphere;
	MeshData skysphereData = MeshGenerator::GenerateSphere(100, 30, 30, true);
	error = skyBoxSphere.init(d3d.getDevice(), &skysphereData);
	if (error != 0) return ThrowErrorMSGBox(error);

	// 4. create camera
	Camera camera;
	error = camera.init(width, height);
	if (error != 0) return ThrowErrorMSGBox(error);

	// 5. create time
	Time time;
	error = time.init();
	if (error != 0) return ThrowErrorMSGBox(error);

	// 6. create material
	Material material;
	MaterialParameters parameters1 = {};
	parameters1.Ambient = { 0,0,0,0 };

	error = material.init(&d3d, L"wall.jpg", L"LightVertexShader", L"LightPixelShader", parameters1);
	if (error != 0) return ThrowErrorMSGBox(error);

	DDSMaterial skyboxMaterial;
	error = skyboxMaterial.init(&d3d, L"grasscube1024.dds", L"SkyboxVertexShader", L"SkyboxPixelShader", parameters1);
	if (error != 0) return ThrowErrorMSGBox(error);

	// 7. create light
	Light light = {};
	light.LightDirection = { 0.0f, 0.0f, 1.0f };
	light.AmbientColor = { 0.2f, 0.2f, 0.2f, 1.0f };
	light.DiffuseColor = { 1.0f, 1.0f, 0.0f, 1.0f };
	light.LightIntensity = 1.0f;

	material.setLight(d3d.getDeviceContext(), light);

	//8. GameObject

	GameObject gameObject;
	gameObject.init(d3d.getDevice(), &sphere, &material);


	GameObject skybox;
	skybox.init(d3d.getDevice(), &skyBoxSphere, &skyboxMaterial);

	// 8. run application
	while (true)
	{
		if (!window.run()) break;

		// 8.1. update objects
		time.update();
		gameObject.update(time.getDeltaTime());
		skybox.update(time.getDeltaTime());

		gameObject.SetPosition(0, 0, time.getTotalTime());

		// 8.2. draw objects 
		d3d.beginScene(0.0f, 0.0f, 0.0f);

		// rendering stuff
		gameObject.render(d3d.getDeviceContext(), camera.getViewMatrix(), camera.getProjectionMatrix());
		skybox.render(d3d.getDeviceContext(), camera.getViewMatrix(), camera.getProjectionMatrix());
		//light.render(d3d.getDevice());

		d3d.endScene();
	}

	// 9. tidy up
	material.deInit();
	time.deInit();
	camera.deInit();
	sphere.deInit();
	d3d.deInit();
	window.deInit();

	return 0;
}
