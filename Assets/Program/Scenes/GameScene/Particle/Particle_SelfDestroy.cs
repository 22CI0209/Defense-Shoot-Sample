/*this script is written in UTF-8*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*再生を終了したパーティクルが自動で消滅する
 *https://your-3d.com/unity-particle-script-1/#toc5
*/
public class Particle_SelfDestroy : MonoBehaviour
{
    [SerializeField] ParticleSystem PS;

    /*パーティクルの生成終了時の処理*/
    void Update()
    {
        if(PS.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
