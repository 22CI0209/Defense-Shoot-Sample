/*this script is written in UTF-8*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*爆発パーティクル独自の動き*/
public class Explosion_Script : MonoBehaviour
{
    GameObject sound;
    SoundManager SM;

    /*爆発音を鳴らす*/
    void Awake()
    {
        sound = GameObject.FindWithTag("Sound");
        SM = sound.GetComponent<SoundManager>();
    }
    private void Start()
    {
        try
        {
            SM.PlaySE(1,1.0f);
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
        }
    }
}
