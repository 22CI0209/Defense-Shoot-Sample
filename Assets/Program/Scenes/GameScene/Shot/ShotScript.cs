using UnityEngine;
using UnityEngine.UI;

public class ShotScript : MonoBehaviour
{
    [Header("アタッチ欄")]
    [SerializeField] Image image;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject explosion;

    [Header("スピード調整")]
    [SerializeField] float speed = 20.0f;
    [Header("デバッグ確認用")]
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] float pow = 0;

    void Update()
    {
        /*移動*/
        rb.velocity = direction * speed;
        transform.Rotate(new Vector3(0,0,-0.6f));
    }

    /*画面外に出たら消去*/

    /*弾のステータス設定
    *他のスクリプトから呼び出して下さい*/
    public void SetShot(Vector2 vec_, float pow_)
    {
        direction = vec_;
        pow = pow_;
        SetParticle(pow_);
    }

    /*一定威力でパーティクル表示
    TODO:パーティクルをCanvas上に表示する方法は?
    */
    void SetParticle(float pow_)
    {
        if(pow_ >= 100.0f)
        {
            ParticleSystem p = Instantiate(particle);
            p.transform.SetParent(gameObject.transform);
            p.transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyScript>();
            //enemy.Damage(pow);
            Vector2 spawnPos = GetComponent<Transform>().position;
            GameObject exp = Instantiate(explosion, spawnPos / 500, Quaternion.identity, transform.parent);
            Debug.Log("スポーン位置：" + spawnPos);
            Destroy(gameObject);
        }
    }


}
