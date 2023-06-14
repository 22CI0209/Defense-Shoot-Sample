using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    //キャッシュ
    RectTransform rect;

    [Header("アタッチ欄")]
    [SerializeField] GameObject amoPrefab;
    [SerializeField] GameObject bar;
                     BarController barScript;
    [Header("調整欄")]
    [SerializeField] int   hit = 10;                    //体力
    [SerializeField] float inv_Max = 3.0f;              //無敵時間
    [SerializeField] float pow_1Frame = 30f;            //1Fあたりの追加量
    [SerializeField] float pow_ChargeTime = 0.0f;       //カウンタ
    [SerializeField] float pow_ChargeTimeMax = 5.0f;    //最大チャージ
    [Header("デバッグ確認用")]
    [SerializeField] float Pow_Charge = 0;
    [SerializeField] float inv_Now = 0;
    float delta;

    void Awake()
    {
        barScript = bar.GetComponent<BarController>();
        rect = transform as RectTransform;
    }
    void Update()
    {
        delta = Time.deltaTime;

        /*弾を発射*/
        SpawnShot();

        /*無敵時間*/
        if(inv_Now >= 0)
        {
            inv_Now -= delta;
        }
    }

    void SpawnShot()
    {
        /*マウス左を長押ししていると弾の威力がアップ*/
        if (Input.GetMouseButton(0))
        {
            /*チャージ制限*/
            if(pow_ChargeTime < pow_ChargeTimeMax)
            {
                Pow_Charge += pow_1Frame * delta;
                pow_ChargeTime += delta;
            }
            else
            {
                pow_ChargeTime = pow_ChargeTimeMax;
            }
        }
        /*チャージ放出*/
        if (Input.GetMouseButtonUp(0))
        {
            /*威力の最低保証*/
            if (Pow_Charge * delta < pow_1Frame * delta)
                Pow_Charge = pow_1Frame;

            /*クリックした場所に弾を発射
             *アンカーが左下のときのみ正常に動作する*/
            var go = Instantiate(amoPrefab, rect.position, Quaternion.identity, transform.parent);
            var shot = go.GetComponent<ShotScript>();
            Vector2 msPos = Input.mousePosition;
            Vector2 direction = msPos - rect.anchoredPosition;
            shot.SetShot(direction.normalized, Pow_Charge);

            /*リセット*/
            Pow_Charge = 0;
            pow_ChargeTime = 0;
        }

        /*チャージを表示*/
        barScript.ChangeFillAmount(pow_ChargeTime / pow_ChargeTimeMax);
    }

    /*敵と衝突*/
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Enemy") && inv_Now <= 0)
        {
            Damage();
        }
    }

    /*ダメージ処理*/
    void Damage()
    {
        hit--;
        if(hit >= 0)
        {
            inv_Now = inv_Max;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
