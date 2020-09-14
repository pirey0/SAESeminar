using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
    [SerializeField] Text _text;

    [SerializeField] float _time;
    [SerializeField] AnimationCurve curve; // use animationcurve instead
 
    private Color _textColor;

    private void Start()
    {
        _textColor = _text.color;
        _textColor.a = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        //x == 5
        var x = Mathf.Lerp(0, 10, 0.9f);
        
        //Color.Lerp
        //Vector3.Lerp
        //Mathf.LerpAngle

        _textColor.a = Mathf.Lerp(1,0, _time);
        //identical
        _textColor.a = -_time + 1;

        //E1 => f(0) = 1
        //E2 => f(1) = 0

        // f(x) = ax + b

        // 1 = a*0 + b
        // 0 = a + b

        // b = 1
        // 0 = a + 1 => a = -1
        // f(x) = -1*x + 1
        _textColor.a = -1*_time + 1;

        _text.color = _textColor;
    }


}
