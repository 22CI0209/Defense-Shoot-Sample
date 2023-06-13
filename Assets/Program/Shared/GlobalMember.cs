/*this script is written in UTF-8*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*プロジェクト間で共有したい変数と関数
  GlobalMember.--- でアクセスできる*/
public class GlobalMember : MonoBehaviour
{
  /*シーン一覧*/
    public enum NextSceneState
    {
        TitleScene   = 0,
        GameScene    = 1,
        ResultScene  = 2
    }

    /*ゲームの進行状況をリセット*/
    public static void ResetProgress()
    {
      UnityEngine.Application.targetFrameRate = 60;
    }

    /*シーン切り替え*/
    public static void ChangeScene(NextSceneState scene_)
    {
        SceneManager.LoadScene(scene_.ToString());
    }
}
