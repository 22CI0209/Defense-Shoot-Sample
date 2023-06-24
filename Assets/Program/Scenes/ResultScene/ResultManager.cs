using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text _text1; //「あなたのスコアは」
    [SerializeField] private Text _text2; //点！
    [SerializeField] private TextMeshProUGUI scorelabel; //スコア表示用
    [SerializeField] private GameObject RankingObject;
    [SerializeField] private GameObject ResultObject;
    [SerializeField] private Text RankingClicktext; //「左クリックでランキング画面へ」
    [SerializeField] private AudioSource TextSound; //テキストを表示する時の効果音
    [SerializeField] private AudioSource ClickSound; //マウスをクリックした時の音
    private int ResultScore;
    private int timeCount = 1; //時間間隔

    //以下ランキング画面用
    private bool flag1;
    [SerializeField] Text _text;
    [SerializeField] Text[] _rankingScore;
    [SerializeField] Text[] _PrerankingScore; //前回のランキング
    private float cycle = 1; //点滅周期
    [SerializeField] AudioSource RankingSound;
    [SerializeField] private Text TitleClicktext; //「左クリックでタイトル画面へ」

    private void Start()
    {
        //Coroutineを開始する
        StartCoroutine(TextDisplay());
        RankingObject.SetActive(false); //最初はランキングを非表示にする
        flag1 = false;

        _PrerankingScore = _rankingScore; //前回のランキングを保存しておく
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
        RankingClicktext.text = "左クリックでランキング画面へ";
        while(true)
        {
            //左クリックが押されたとき
            if (Input.GetMouseButtonDown(0))
            {
                yield return RankingStart();
                break;
            }
            else
                yield return null;
        }

        // flag1がtrueになるまで中断  
        while (flag1 == false) yield return null;
        ResultObject.SetActive(false); //Resultを非表示にする
        RankingObject.SetActive(true); //Rankingを表示する
        yield return RankingDisplay();
    }

    //ランキング表示をスタートする
    private IEnumerator RankingStart()
    {
        ClickSound.Play();
        //ClickSoundが鳴り終わる前に遷移するのを防ぐ
        yield return new WaitForSeconds(0.5f);
        flag1 = true;

    }

    //ランキング表示のコルーチン
    private IEnumerator RankingDisplay()
    {
        yield return new WaitForSeconds(timeCount);
        _text.text = "ランキング";
        RankingSound.Play();

        yield return new WaitForSeconds(timeCount);
        for (int i = 0; i < Ranking.ranking.Length; ++i)
        {
            _rankingScore[i].text = Ranking.ranking[i] + Ranking.rankingValue[i].ToString();

            if (_PrerankingScore[i] != _rankingScore[i])
            {
                yield return Blink(_rankingScore[i]); //点滅させる
            }
        }
        RankingSound.Play();

        yield return new WaitForSeconds(timeCount);
        TitleClicktext.text = "左クリックでタイトル画面へ";
        while(true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yield return TitleSceneGo();
                break;
            }
            else
                yield return null;
        }
    }

    // 点滅コルーチン
    private IEnumerator Blink(Text text)
    {
        while (true)
        {
            text.enabled = false;
            yield return new WaitForSeconds(cycle);
            text.enabled = true;
        }
    }

    //タイトルシーン遷移
    private IEnumerator TitleSceneGo()
    {
        ClickSound.Play();
        //ClickSoundが鳴り終わる前に遷移するのを防ぐ
        yield return new WaitForSeconds(0.5f);
        GlobalMember.ChangeScene(GlobalMember.NextSceneState.TitleScene);
    }


}
