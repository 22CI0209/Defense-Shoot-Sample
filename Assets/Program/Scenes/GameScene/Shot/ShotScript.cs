using UnityEngine;
using UnityEngine.UI;

public class ShotScript : MonoBehaviour
{
    [Header("アタッチ欄")]
    [SerializeField] Image image;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject explosion;

    [Header("スピード調整")]
    [SerializeField] float speed = 20.0f;
    [Header("デバッグ確認用")]
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] int pow = 0;

    RectTransform rect;

    private void Awake() 
    {
        rect = transform as RectTransform;
    }

    void Update()
    {
        /*移動*/
        rb.velocity = direction * speed;
        transform.Rotate(new Vector3(0,0,-1.2f));
        OutOfCamera();
    }

    /*画面外に出たら消去*/
    void OutOfCamera()
    {
        if(transform.position.x > 12)
        {
            Destroy(gameObject);
        }
    }

    /*弾のステータス設定
    *他のスクリプトから呼び出して下さい*/
    public void SetShot(Vector2 vec_, int pow_)
    {
        direction = vec_;
        pow = pow_;
        SetParticle(pow_);
    }

    /*一定威力でパーティクル表示*/
    void SetParticle(float pow_)
    {
        if(pow_ >= 3)
        {
            GameObject p = Instantiate(particle,rect.position,Quaternion.identity,gameObject.transform);
        }
    }

    /*敵にダメージを与えて自分は消滅*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyScript>();
            enemy.Damage(pow);
            Vector2 spawnPos = GetComponent<Transform>().position;
            GameObject exp = Instantiate(explosion, rect.position, Quaternion.identity, transform.parent);
            Debug.Log("スポーン位置：" + spawnPos);
            Destroy(gameObject);
        }
    }


}
