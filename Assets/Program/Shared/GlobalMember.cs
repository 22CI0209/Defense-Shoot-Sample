/*this script is written in UTF-8*/
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

/*プロジェクト間で共有したい変数と関数
  GlobalMember.--- でアクセスできる*/
public class GlobalMember : MonoBehaviour
{
    /*ゲームの進行状況をリセット*/
    public static void ResetProgress()
    {
      UnityEngine.Application.targetFrameRate = 60;
    }

    /*プログラム終了*/
    public static void QuitGame()
    {
      var window = MessageBox.Show
        (
            "ゲームを終了しますか？",
            "ゲーム終了",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );
        if(window == DialogResult.Yes)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            UnityEngine.Application.Quit();
            #endif
        }
    }
}
