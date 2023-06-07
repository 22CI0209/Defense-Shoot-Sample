using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [Header("�A�^�b�`��")]
    [SerializeField] Rigidbody2D rb;
    [Header("�X�s�[�h����")]
    [SerializeField] float speed = 20.0f;
    [Header("�f�o�b�O�m�F�p")]
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
