using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public static string[] ranking = { "　１位：", "　２位：", "　３位：", "　４位：", "　５位：", "　６位：", "　７位：", "　８位：", "　９位：", "１０位：" };
    public static int[] rankingValue = new int[10];

    // Start is called before the first frame update
    void Start()
    {
        GetRanking();

        SetRanking(ScoreManager.score);
    }

    void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt("SCORE", 0);
        }
    }

    void SetRanking(int _value)
    {
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
}
