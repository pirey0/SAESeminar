using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    [SerializeField] float _timeScale;

    private void Start()
    {
        Time.timeScale = _timeScale;
    }
}
