using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OyashiroController : MonoBehaviour
{
    [SerializeField] GameObject shot;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            LaunchShot();
        }

        //または
        // if(Keyboard.current.anyKey.wasPressedThisFrame)
        // {
        //     LaunchShot();
        // }
    }

    void LaunchShot()
    {
        GameObject s = Instantiate(shot);
        s.transform.position = this.transform.position;
    }
}
