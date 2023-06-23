using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public static ScoreManager singleton;
    //[SerializeField] private TextMeshProUGUI scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        //scoreLabel.text = score.ToString();
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 削除時の処理
    void OnDestroy()
    {
        // スコアを保存
        PlayerPrefs.SetInt("SCORE", score);
        PlayerPrefs.Save();
    }

    //scoreを増加させる
    public void AddScore(int amount)
    {
        score += amount;
        //scoreLabel.text = score.ToString();
    }

    //scoreを初期化する
    public void InitializationScore(int zero)
    {
        score *= zero;
        //scoreLabel.text = score.ToString();
    }
}
