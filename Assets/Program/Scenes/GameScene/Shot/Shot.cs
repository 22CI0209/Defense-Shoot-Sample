using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("アタッチ欄")]
    [SerializeField] Rigidbody2D rb;
    [Header("スピード調整")]
    [SerializeField] float speed = 20.0f;
    [Header("デバッグ確認用")]
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] float pow = 0;

    public GameObject explosionEffect; //爆発エフェクトのオブジェクト
   
    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void SetShot(Vector2 vec_, float pow_)
    {
        direction = vec_;
        pow = pow_;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Enemyと接触したら消滅
        if (other.CompareTag("Enemy"))
        {
            Instantiate(explosionEffect);
            Destroy(gameObject);
        }
    }
}
