using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text _text1; //「あなたのスコアは」
    [SerializeField] private Text _text2; //点！
    [SerializeField] private TextMeshProUGUI scorelabel; //スコア表示用
    [SerializeField] private GameObject Ranking;
    [SerializeField] private GameObject Result;
    [SerializeField] private Text Clicktext; //「左クリックでランキング画面へ」
    [SerializeField] private AudioSource TextSound; //テキストを表示する時の効果音
    [SerializeField] private AudioSource ClickSound; //マウスをクリックした時の音
    private int ResultScore;
    private int timeCount = 1; //時間間隔

    private void Start()
    {
        //Coroutineを開始する
        StartCoroutine(TextDisplay());
        Ranking.SetActive(false); //最初はランキングを非表示にする
    }

    private void Update()
    {
        RankingStart();
    }

    //リザルトテキスト表示のコルーチン
    private IEnumerator TextDisplay()
    {
        yield return new WaitForSeconds(timeCount);
        _text1.text = "あなたのスコアは";
        TextSound.Play();

        yield return new WaitForSeconds(timeCount);
        ResultScore = ScoreManager.score;
        scorelabel.text = ResultScore.ToString();
        TextSound.Play();
        _text2.text = "点！";

        yield return new WaitForSeconds(timeCount);
        Clicktext.text = "左クリックでランキング画面へ";
    }

    //ランキング表示をスタートする
    void RankingStart()
    {
        //左クリックが押されたとき
        if (Input.GetMouseButtonDown(0))
        {
            Result.SetActive(false); //Resultを非表示にする
            Ranking.SetActive(true); //ランキングを表示する
            ClickSound.Play();
        }
    }
}
