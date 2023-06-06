/*this script is written in UTF-8*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  音楽や効果音を再生する
  使用方法：
  イ.SoundManagerプレハブをヒエラルキーに配置し、音楽や効果音をスクリプトにセット AudioSourceは自分自身のものをセット
  ロ.音を鳴らしたいスクリプトにおいて、何らかの方法でSoundManagerを取得(GameObject.Find(),[SerializeField])
  ハ.public関数を呼び出すとそこで音を鳴らせる
*/
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] SE;

    /*効果音を再生*/
    public void PlaySE(int no_,float vol_)
    {
        aud.PlayOneShot(SE[no_],vol_);
    }
}
