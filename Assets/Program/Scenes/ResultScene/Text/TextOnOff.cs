﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOnOff : MonoBehaviour
{
     private Text _text; //点滅させる対象
    [SerializeField] private float cycle = 1; //点滅周期

    private double time;

    private void Start()
    {
        _text = this.gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        var repeatValue = Mathf.Repeat((float)time, cycle);

        //点滅させる
        _text.enabled = repeatValue >= cycle * 0.5f;
    }
}
