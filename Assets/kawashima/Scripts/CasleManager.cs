using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CasleManager : MonoBehaviour
{
    //キャッシュ
    RectTransform rect;

    [Header("アタッチ欄")]
    [SerializeField] GameObject amoPrefab;
    [Header("調整欄")]
    [SerializeField] float pow_1Frame = 30f;
    [SerializeField] float pow_ChargeTime = 1f;
    [Header("デバッグ確認用")]
    [SerializeField] float Pow_Charge = 0;

    void Start()
    {
        rect = transform as RectTransform;
    }
    void Update()
    {
        var delta = Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Pow_Charge = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Pow_Charge += pow_1Frame * delta;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Pow_Charge * delta > pow_ChargeTime * pow_1Frame * delta)
                Pow_Charge = pow_ChargeTime * pow_1Frame;
            var go = Instantiate(amoPrefab, rect.position, Quaternion.identity, transform.parent);
            var shot = go.GetComponent<ShotScript>();
            Vector2 msPos = Input.mousePosition;
            Vector2 direction = msPos - rect.anchoredPosition;
            shot.SetShot(direction.normalized, Pow_Charge);
        }
    }
}
