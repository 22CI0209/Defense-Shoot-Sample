using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CastleManager : MonoBehaviour
{
    //キャッシュ
    RectTransform rect;

    [Header("アタッチ欄")]
    [SerializeField] Image         image;               //画像
    [SerializeField] GameObject    amoPrefab;           //弾
    [SerializeField] GameObject    explosion;           //爆発
    [SerializeField] GameObject    pow_Bar;             //チャージ進捗
                     BarController pow_BarScript;
    [SerializeField] GameObject    hit_Bar;             //HP表示
                     BarController hit_BarScript;
    [Header("調整欄")]
    [SerializeField] int   hitMax = 5;                  //最大体力
    [SerializeField] float inv_Max = 3.0f;              //無敵時間最大
    [SerializeField] float pow_1Sec = 30f;              //1秒あたりの追加量
    [SerializeField] float pow_ChargeTime = 0.0f;       //カウンタ
    [SerializeField] float pow_ChargeTimeMax = 5.0f;    //最大チャージ秒
    [Header("デバッグ確認用")]
    [SerializeField] float hit = 0;
    [SerializeField] float Pow_Charge = 0;              //チャージ量
    [SerializeField] float inv_Now = 0;                 //無敵時間
    float delta;

    void Awake()
    {
        pow_BarScript = pow_Bar.GetComponent<BarController>();
        hit_BarScript = hit_Bar.GetComponent<BarController>();
        rect = transform as RectTransform;
        hit = hitMax;
    }
    void Update()
    {
        delta = Time.deltaTime;
        /*お社は常にどのオブジェクトより前に表示される
         *https://gametukurikata.com/ui/changethedisplayorder
        */
        //transform.SetAsLastSibling();

        /*弾を発射*/
        SpawnShot();

        /*バー表示*/
        hit_BarScript.ChangeFillAmount(hit / hitMax);
        pow_BarScript.ChangeFillAmount(pow_ChargeTime / pow_ChargeTimeMax);

        /*無敵時間中の処理*/
        if(inv_Now > 0)
        {
            inv_Now -= delta;
            image.color = new Color(1,1,1,0.7f);
        }
        else
        {
            image.color = new Color(1,1,1,1);
        }

        //デバッグ
        if(Input.GetMouseButtonDown(1))
        {
            Damage();
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
                Pow_Charge += pow_1Sec * delta;
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
            if (Pow_Charge < pow_1Sec)
                Pow_Charge = pow_1Sec;

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
    }

    /*ダメージ処理*/
    public void Damage()
    {
        if(inv_Now <= 0)
        {
            hit--;
            GameObject e = Instantiate(explosion);
            e.transform.position = new Vector3(-4,-1);
            e.transform.localScale = new Vector3(4,4,1);

            /*死亡ジャッジ*/
            if(hit > 0)
            {
                inv_Now = inv_Max;
            }
            else
            {
                Die();
            }
        }
    }

    /*死ぬとゲームオーバー*/
    async void Die()
    {
        hit_BarScript.ChangeFillAmount(0);
        Destroy(gameObject);
        await Task.Delay(3000);
        GlobalMember.ChangeScene(GlobalMember.NextSceneState.ResultScene);
    }
}
