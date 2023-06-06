/*this script is written in UTF-8*/
using UnityEngine;
using UnityEngine.UI;

/*明滅対応の画像
  使用方法：
  イ.明滅させたい画像を別途用意します
  ロ.当クラスのインスタンスを作ります 明滅させたい画像をi_引数に指定します
     最初に見えなくするならtrue,そうでなく見えるようにするならばfalseをbe_引数に指定します
     ex)BlinkableImage B = new BlinkableImage(imageEX,true);
  ハ.一旦、imageの操作は宣言した当クラス実体のimageで行います
     ex)B.image.color = new Color(0,0,0,0);
  ニ.imageの変化を反映する時(Update()の終わり際など)は、明滅させたいテキストに当クラス実体のimageを代入します
     ex)imageEX = B.image;
*/
public class BlinkableImage
{
    public Image image { get; set; }             //画像
    public bool becomingInvisible { get; set; }  //透明になろうとしている場合はtrue

    /*今のバージョンではコンストラクタ初期化子は使えないようだ　かなしい*/
    public BlinkableImage(Image i_, bool be_)
    {
        this.image = i_;
        this.becomingInvisible = be_;
    }

    /*画像を明滅させる
      使用方法：Update()に書くとテキストが明滅します*/
    public void Blink()
    {
        if (this.becomingInvisible)  /*透明にする*/
        {
            this.image.color -= new Color(0, 0, 0, 0.01f);
            if (this.image.color.a <= 0.0f) { this.becomingInvisible = false; }
        }
        else                        /*不透明にする*/
        {
            this.image.color += new Color(0, 0, 0, 0.01f);
            if (this.image.color.a >= 1.0f) { this.becomingInvisible = true; }
        }
    }
}
