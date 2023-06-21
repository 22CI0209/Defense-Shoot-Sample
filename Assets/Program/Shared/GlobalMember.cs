/*this script is written in UTF-8*/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    /*初回起動*/
    static bool launched;

    /*スコア*/
    static int score;
    static int[] highScore = new int[10];
    
    public static int getScore()
    {
      return score;
    }

    /*ゲームの進行状況をリセット*/
    public static void ResetProgress()
    {
      UnityEngine.Application.targetFrameRate = 60;
      score = 0;

      /*初回起動時*/
      if(!launched)
      {
        RenameTitle_Win("2023年度 制作実習 防衛シューティングゲーム (制作者名)");
        for(int i = 0; i < highScore.Length; ++i)
        {
          highScore[i] = 3 * i;
        }
        launched = true;
      }
    }

    /*タイトルバー変更(Windowsのみ)

     *https://fall-and-fall.hatenablog.com/entry/unity/script/change-window-title-for-standalone-pc

    */
    static void RenameTitle_Win(string t_)

    {
        #if UNITY_STANDALONE_WIN

            string title = t_;

            [DllImport("user32.dll", EntryPoint="FindWindow", CharSet=CharSet.Unicode)]

            static extern System.IntPtr FindWindow(string className, string windowName);

            [DllImport("user32.dll", EntryPoint="SetWindowText", CharSet=CharSet.Unicode)]

            static extern bool SetWindowText(System.IntPtr hwnd, string title);

 

            System.IntPtr hwnd = FindWindow(null, Application.productName);

            SetWindowText(hwnd, title);

        #endif  

    }

    /*シーン切り替え*/
    public static void ChangeScene(NextSceneState scene_)
    {
        SceneManager.LoadScene(scene_.ToString());
    }
}
