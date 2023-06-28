using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ranking : MonoBehaviour
{
    public static string[] ranking = { "　１位：", "　２位：", "　３位：", "　４位：", "　５位：", "　６位：", "　７位：", "　８位：", "　９位：", "１０位："};
    public static int[] rankingValue = new int[10]; //スコアランキング用の配列
    //public static int[] rankingValue = new int[] { 40, 32, 31, 29, 28, 25, 21, 15, 10, 5};
    public static int? _changePoint = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        GetRanking();
        SetRanking(ScoreManager.score);
    }

    void GetRanking()
    {
        int n = 0;
        n++;
        Debug.Log(n);
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }

    //ランキングに書き込み用
    void SetRanking(int _value)
    {
        for (int i = 0; i < ranking.Length; i++)
        {
            //Debug.Log(i);
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                
                //rankingValue[i]から後ろを一つずらす
                for (int j = ranking.Length - 1; j > i; --j)
                {
                    rankingValue[j] = rankingValue[j - 1];
                }
                int change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                _changePoint = i;
                break;
            }
        }
        
        // 配列を昇順で並び替える
        Array.Sort(rankingValue);
        // 配列の順序を反転させる(降順)
        Array.Reverse(rankingValue);


        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
}
