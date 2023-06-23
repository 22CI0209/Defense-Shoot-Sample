using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text _text1;
    [SerializeField] private Text _text2;
    [SerializeField] private TextMeshProUGUI scorelabel;
    [SerializeField] private GameObject Ranking;
    [SerializeField] private Text Clicktext; //クリックテキスト
    //[SerializeField] private AudioSource TextSound;
    //[SerializeField] private AudioSource ClickSound;
    private int ResultScore;
    private int timeCount = 1; //時間間隔

    private void Start()
    {
        //Coroutineを開始する
        StartCoroutine(TextDisplay());
        Ranking.SetActive(false);
    }

    private void Update()
    {

    }

    //リザルトテキスト表示のコルーチン
    private IEnumerator TextDisplay()
    {
        yield return new WaitForSeconds(timeCount);
        _text1.text = "あなたのスコアは";
        //TextSound.Play();

        yield return new WaitForSeconds(timeCount);
        ResultScore = ScoreManager.score;
        scorelabel.text = ResultScore.ToString();
        //TextSound.Play();
        _text2.text = "点！";

        yield return new WaitForSeconds(timeCount);
        Clicktext.text = "クリックでランキング画面へ";
    }
}
