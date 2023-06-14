using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*敵を生成する*/
public class EnemyManager : MonoBehaviour
{
    //キャッシュ
    RectTransform rect;

    [SerializeField] float spawnTime = 2.0f;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Rect spawnArea;

    void Start()
    {
        rect = transform as RectTransform;
        StartCoroutine(Wait());
    }

    //敵生成
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(spawnTime);
        float x = Random.Range(spawnArea.x, spawnArea.x + spawnArea.width),
            y = Random.Range(spawnArea.y, spawnArea.y + spawnArea.height);
        Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity, transform.parent);
        StartCoroutine(Wait());
    }
}
