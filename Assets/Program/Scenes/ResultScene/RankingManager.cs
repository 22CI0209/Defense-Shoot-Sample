using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{

    [SerializeField] Text _text;
    [SerializeField] Text _rankingScore;
    [SerializeField] AudioSource RankingSound;
    private int timeCount = 1; //時間間隔
    // Start is called before the first frame update
    void Start()
    {
        //Coroutineを開始する
        StartCoroutine(TextDisplay());
    }

    private IEnumerator TextDisplay()
    {
        yield return new WaitForSeconds(timeCount);
        _text.text = "ランキング";
        RankingSound.Play();

        yield return new WaitForSeconds(timeCount);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
