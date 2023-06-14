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

    public List<EnemyScript> EnemysList;

    void Start()
    {
        enemyRect = enemyPrefab.transform as RectTransform;
        EnemysList = new List<EnemyScript>();
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
            var go = Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity, transform);
            //エネミーを取得してエネミーの関数を呼び出し
            var enemy = go.GetComponent<EnemyScript>();
            enemy.SetManager(this);
            //リストに生成したエネミーを追加
            EnemysList.Add(enemy);
            //ソート処理
            EnemyPosYSort();
        }
    }

    /// <summary>
    /// 降順ソート
    /// </summary>
    void EnemyPosYSort()
    {
        //ラムダ式でy座標ソート　やり方としてははC言語のソートと同じ
        //obj1, obj2は引数と考えていい　
        EnemysList.Sort((obj1, obj2) => obj2.transform.position.y.CompareTo(obj1.transform.position.y));
        //ソートされた値を実際のオブジェクトの順序に更新
        for (int i = 0; i < EnemysList.Count; ++i)
            EnemysList[i].transform.SetSiblingIndex(i);
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
