using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //ƒLƒƒƒbƒVƒ…
    RectTransform rect;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 direction = Vector2.left;
    [SerializeField] float speed = 20.0f;

    void Start()
    {
        rect = transform as RectTransform;
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }
}
