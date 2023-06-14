using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    RectTransform rect;     //キャッシュ
    EnemyManager manager;   //マネージャーのインスタンス

    enum State { Move, Attack, CoolDown };

    [Header("アタッチ欄")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    [Header("ステータス")]
    [SerializeField] State state = State.Move;
    [SerializeField] float hp = 10.0f;
    //[SerializeField] float attack = 5.0f;
    [SerializeField] bool isHit = false;
    [Header("移動関係")]
    [SerializeField] Vector2 direction = Vector2.left;
    [SerializeField] float speed = 20.0f;

    public void SetManager(EnemyManager manager_)
    {
        manager = manager_;
    }

    public void Damage(float damage_)
    {
        hp -= damage_;
        if (hp <= 0)
            Destroy(gameObject);
    }

    void Start()
    {
        rect = transform as RectTransform;
    }

    void Update()
    {
        switch (state)
        {
            case State.Move:
                rb.velocity = direction * speed;
                if (isHit)
                    state = State.Attack;
                break;
            case State.Attack:
                animator.SetTrigger("Attack");
                state = State.CoolDown;
                break;
            case State.CoolDown:
                break;
        }
    }

    void OnDestroy()
    {
        manager.EnemysList.Remove(this);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Castle")
        {
            isHit = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Castle")
        {
            isHit = false;
        }
    }
}
