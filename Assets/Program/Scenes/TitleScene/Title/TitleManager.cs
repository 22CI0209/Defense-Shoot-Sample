using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GlobalMember.ChangeScene(GlobalMember.NextSceneState.GameScene);
        }
    }
}
