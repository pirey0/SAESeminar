using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private string _name;
    private List<GameObject> _objects;
    public event System.Action<GameObject> ObjectEntered;
    public event System.Action<GameObject> ObjectExited;

    public string Name { get => _name;  }
    public Room(string name)
    {
        _objects = new List<GameObject>();
        _name = name;
    }



    // Destroy all things
    public void DestroyObjects()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            MonoBehaviour.Destroy(_objects[i]);
        }
        _objects.Clear();
    }

    // Change materials
    public void ChangeMaterials(Material newMaterial)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            MeshRenderer renderer = _objects[i].GetComponent<MeshRenderer>();
            renderer.material = newMaterial;
        }
    }

    // Print out objects of the room
    public void LogContent()
    {
        Debug.Log("Objects in :" + _name);
        for (int i = 0; i < _objects.Count; i++)
        {
            Debug.Log(_objects[i].name);
        }
    }

    // Event on room enter/exit
    public void AddObject(GameObject newObject)
    {
        _objects.Add(newObject);
        ObjectEntered?.Invoke(newObject);
    }
   
    public void RemoveObject(GameObject obj)
    {
        _objects.Remove(obj);
        ObjectExited?.Invoke(obj);
    }


}
