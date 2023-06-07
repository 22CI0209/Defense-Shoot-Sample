using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [Header("アタッチ欄")]
    [SerializeField] Rigidbody2D rb;
    [Header("スピード調整")]
    [SerializeField] float speed = 20.0f;
    [Header("デバッグ確認用")]
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] float pow = 0;

    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void SetShot(Vector2 vec_, float pow_)
    {
        direction = vec_;
        pow = pow_;
    }

}
