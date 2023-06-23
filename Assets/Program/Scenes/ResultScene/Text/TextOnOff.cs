using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOnOff : MonoBehaviour
{
    [SerializeField] private Text _text1; //点滅させる対象
    [SerializeField] private Text _text2; //点滅させる対象
    [SerializeField] private float cycle = 1; //点滅周期

    private double time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        var repeatValue = Mathf.Repeat((float)time, cycle);

        //点滅させる
        _text1.enabled = repeatValue >= cycle * 0.5f;
        _text2.enabled = repeatValue >= cycle * 0.5f;
    }
}
