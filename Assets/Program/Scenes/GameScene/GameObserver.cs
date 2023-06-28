using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObserver : MonoBehaviour
{
    [SerializeField] Text scoreNow;

    /*ゲーム画面を観察する*/
    private void Update() 
    {
        ShowScore();
    }

    /*スコア表示*/
    void ShowScore()
    {
        scoreNow.text = "SCORE:" + ScoreManager.score.ToString("D3");
    }
}
