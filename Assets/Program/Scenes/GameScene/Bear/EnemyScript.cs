using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //キャッシュ
    RectTransform rect;
    //マネージャーのインスタンス
    EnemyManager manager;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 direction = Vector2.left;
    [SerializeField] float speed = 20.0f;

    public void SetManager(EnemyManager manager_)
    {
        manager = manager_;
    }

    void Start()
    {
        rect = transform as RectTransform;
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }

    void OnDestroy()
    {
        manager.EnemysList.Remove(this);
    }
}
