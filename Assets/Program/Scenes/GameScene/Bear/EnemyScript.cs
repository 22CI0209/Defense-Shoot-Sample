﻿using UnityEngine;
using TMPro;

/// <summary>
/// 敵の行動を処理するクラス
/// </summary>
public class EnemyScript : MonoBehaviour
{
    RectTransform rect;     //キャッシュ
    EnemyManager manager;   //マネージャーのインスタンス

    enum State { Move, Attack, Damage };

    [Header("アタッチ欄")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] AudioSource attackSE;

    [Header("ステータス")]
    [SerializeField] State state = State.Move;
    [SerializeField] int hp = 10;
    [SerializeField] bool isHit = false;
    [SerializeField] CastleManager castleInstance;
    [Header("移動関係")]
    [SerializeField] Vector2 direction = Vector2.left;
    [SerializeField] float speed = 100.0f;

    public void SetManager(EnemyManager manager_)
    {
        manager = manager_;
    }
    public void Damage(int damage_)
    {
        //攻撃を食らった時の何らかの処理～
        hp -= damage_;
        if (hp <= 0)
        {
            EnemyDestroy();
        }
        else
        {
            //被弾したときの処理～
            state = State.Damage;
            StateSet();
            hpText.text = hp.ToString("F0");
        }
    }

    public void Attack()
    {
        //アニメーションイベント中に呼び出される攻撃関数～
        if(castleInstance != null)
        {
            castleInstance.Damage();
            attackSE.Play();
        }
    }
    public void FinishStan()
    {
        state = isHit ? State.Attack : State.Move;
        StateSet();
    }

    void StateAction()
    {
        switch (state)
        {
            case State.Move:
                //移動状態の処理～
                if (isHit)
                {
                    //移動中にお城に当たったら
                    state = State.Attack;
                    StateSet();
                }
                break;
            case State.Attack:
                //攻撃状態の処理～
                if (!isHit)
                {
                    //お城に当たらなくなったら
                    state = State.Move;
                    StateSet();
                }
                break;
            case State.Damage:
                //被弾状態の処理～
                break;
        }
    }
    void StateSet()
    {
        switch (state)
        {
            case State.Move:
                rb.velocity = direction * speed;
                animator.SetBool("Attack", isHit);
                break;
            case State.Attack:
                rb.velocity = Vector2.zero;
                animator.SetBool("Attack", isHit);
                break;
            case State.Damage:
                rb.velocity = Vector2.zero;
                animator.CrossFade("Bear_Damage", 0);
                break;
        }
    }
    void EnemyDestroy()
    {
        //死亡するときの何らかの処理～
        Instantiate(effectPrefab, rect.position, Quaternion.identity, transform.parent);
        manager.EnemysList.Remove(this);
        ScoreManager.score += 1;
        Destroy(gameObject);
    }

    void Start()
    {
        //初期化処理～
        rect = transform as RectTransform;
        state = State.Move;
        StateSet();
        hpText.text = hp.ToString("F0");
    }

    void Update()
    {
        StateAction();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Castle")
        {
            isHit = true;
            castleInstance = collision.GetComponent<CastleManager>();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Castle")
        {
            isHit = false;
            castleInstance = null;
        }
    }
}
