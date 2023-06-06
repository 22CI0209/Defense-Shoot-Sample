/*this script is written in UTF-8*/
using UnityEngine;
using UnityEngine.UI;

/*明滅対応のテキスト
  使用方法：
  イ.明滅させたいテキストを別途用意します
  ロ.当クラスのインスタンスを作ります 明滅させたいテキストをt_引数に指定します
     最初に見えなくするならtrue,そうでなく見えるようにするならばfalseをbe_引数に指定します
     ex)BlinkableText B = new BlinkableText(textEX,true);
  ハ.一旦、textの操作は宣言した当クラス実体のtextで行います
     ex)B.text.text = "Example";
  ニ.textの変化を反映する時(Update()の終わり際など)は、明滅させたいテキストに当クラス実体のtextを代入します
     ex)textEX = B.text;
*/
public class BlinkableText
{
    public Text text{get; set;}               //テキスト
    public bool becomingInvisible{get; set;}  //透明になろうとしている場合はtrue

    /*今のバージョンではコンストラクタ初期化子は使えないようだ　かなしい*/
    public BlinkableText(Text t_ ,bool be_)
    {
        this.text = t_;
        this.becomingInvisible = be_;
    }

    /*テキストを明滅させる
      使用方法：Update()に書くとテキストが明滅します*/
    public void Blink()
    {
        if(this.becomingInvisible)  /*透明にする*/
        {
            this.text.color -= new Color(0,0,0,0.01f);
            if(this.text.color.a <= 0.0f)  {this.becomingInvisible = false;}
        }
        else                        /*不透明にする*/
        {
            this.text.color += new Color(0,0,0,0.01f);
            if(this.text.color.a >= 1.0f)  {this.becomingInvisible = true;}
        }
    }
}
