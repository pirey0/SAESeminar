using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : MonoBehaviour
{
    [SerializeField] float time = 1;
    private void Start()
    {
        Destroy(gameObject, time);
    }
}
