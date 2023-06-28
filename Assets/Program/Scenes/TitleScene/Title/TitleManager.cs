using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SoundManager sound;

    private void Start() 
    {
        GlobalMember.ResetProgress();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            sound.PlaySE(2,1.0f);
            Thread.Sleep(500);
            GlobalMember.ChangeScene(GlobalMember.NextSceneState.GameScene);
        }
    }
}
