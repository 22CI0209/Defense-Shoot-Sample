using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnOff : MonoBehaviour
{
    [SerializeField] private Renderer _text; //点滅させる対象
    [SerializeField] private float cycle = 1; //点滅周期

    private double time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        var repeatValue = Mathf.Repeat((float)time, cycle);

        //点滅させる
        _text.enabled = repeatValue >= cycle * 0.5f;
    }
}
