using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を生成するマネージャークラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    //キャッシュ
    RectTransform enemyRect;
    [Header("生成クールダウン")]
    [SerializeField] float spawnTime = 2.0f;
    [Header("生成するプレファブ")]
    [SerializeField] GameObject enemyPrefab;
    [Header("生成する範囲")]
    [SerializeField] Rect spawnArea;

    void Start()
    {
        enemyRect = enemyPrefab.transform as RectTransform;
        StartGenerate();
    }

    IEnumerator Wait()
    {
        while (true)
        {
            //spawnTime秒待つ
            yield return new WaitForSeconds(spawnTime);
            //スポーンエリアをランダムに決める
            //例 x 1920 y 150 w 100 h 500 の場合
            //左下を(0, 0)として右上を(1920, 1080)とするので
            //x軸 1920 ~ 2020 の範囲＋エネミーの横幅分オフセットが働くため
            //実際は敵のwidthとheight分ずれて生成される
            //例としてエネミーのwidth heightが(224, 224)の場合
            //x軸 2144 ~ 2244の範囲に生成
            //yも同様に下からy ~ yからの距離 height の間に生成+エネミーのオフセットが働く
            float x = Random.Range(spawnArea.x + enemyRect.sizeDelta.x * enemyRect.localScale.x,
                spawnArea.x + enemyRect.sizeDelta.x * enemyRect.localScale.x + spawnArea.width),
                y = Random.Range(spawnArea.y + enemyRect.sizeDelta.y * enemyRect.localScale.y,
                spawnArea.y + enemyRect.sizeDelta.y * enemyRect.localScale.y + spawnArea.height);
            //座標確認したい場合下を使ってね
            //Debug.Log("\n生成するx座標：" + x + "  生成するy座標" + y);

            //生成処理
            //第四引数にEnemyManagerの親Canvasを指定している
            //これはimageコンポーネントで描画されるオブジェクトは、
            //Canvasの子オブジェクトじゃないと描画されないから。
            Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity, transform.parent);
        }
    }

    /// <summary>
    /// 外部から開始させる用
    /// </summary>
    public void StartGenerate()
    {
        StartCoroutine(Wait());
    }
    /// <summary>
    /// 外部から停止させる用
    /// </summary>
    public void StopGenerate()
    {
        StopCoroutine(Wait());
    }
}
