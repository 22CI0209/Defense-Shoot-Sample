using UnityEngine;
using UnityEngine.UI;

public class ShotScript : MonoBehaviour
{
    [Header("アタッチ欄")]
    [SerializeField] Image image;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject explosion;
    [SerializeField] Camera mainCamera;

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
            //Vector2 spawnPos = GetComponent<Transform>().position;

            /*編集中*/
            // 弾のローカル座標をワールド座標に変換
            Vector3 bulletPosition = transform.position;

            // カメラのワールド座標上での弾の位置を計算
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 offset = new Vector3(bulletPosition.x, bulletPosition.y, cameraPosition.z);
            Vector3 cameraBulletPosition = cameraPosition + offset;

            // 爆発を生成
            Instantiate(explosion, cameraBulletPosition, Quaternion.identity);

            // 弾を削除
            Destroy(gameObject);
            /*編集中*/
        }
    }

    public Rect GetScreenRect(Graphic self)
    {
        var _corners = new Vector3[4];
        self.rectTransform.GetWorldCorners(_corners);

        if (self.canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            var cam = self.canvas.worldCamera;
            _corners[0] = RectTransformUtility.WorldToScreenPoint(cam, _corners[0]);
            _corners[2] = RectTransformUtility.WorldToScreenPoint(cam, _corners[2]);
        }

        return new Rect(_corners[0].x,
                        _corners[0].y,
                        _corners[2].x - _corners[0].x,
                        _corners[2].y - _corners[0].y);
    }

}
