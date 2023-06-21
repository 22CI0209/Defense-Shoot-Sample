using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    [SerializeField] Image Bar;
    [SerializeField] float FA_First;    //開始時のfillAmount

    private void Start() 
    {
        ChangeFillAmount(FA_First);
    }

    /*バーの表示量を変更*/
    public void ChangeFillAmount(float fill)
    {
        if(fill > 1.0f)fill = 1.0f;
        if(fill < 0.0f)fill = 0.0f;
        Bar.fillAmount = fill;
    }
}