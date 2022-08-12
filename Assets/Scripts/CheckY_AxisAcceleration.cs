using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckY_AxisAcceleration : MonoBehaviour
{
    // Start is called before the first frame 

    private float acceleration;
    private float[] posY_Array;
    private int activatedFrame, inactivatedFrame;
    public GameObject rightHandPrefab;
    public GameObject UIOpenSound;
    public GameObject UITapSound;
    public GameObject UIPositionSetter;
    public GameObject button;
    public GameObject[] buttons;
    [SerializeField] private float threshold;

    private void Start()
    {
        acceleration = 0f;
        posY_Array = new float[10];
    }

    // Update is called once per frame
    void Update()
    {
        /* HandTrackingが正常に稼働している間は、RightHandAnchorは手の位置を返してくれる
           しかし手をトラッキングできない場合、RightHandAnchorのpositionの値は原点へと返されてしまう
           このプロジェクトでは頭の位置がY座標マイナスの値において実行しているため、HandTrackingをしていない状態としている状態の間で
           加速度が計算されると、必ず絶対値が大きな値になってしまう
           
           ＜ここから示唆されること＞
        　 　OVRCameraRig を使用した場合のHandの座標って相対座標じゃなく絶対座標で表されている
        　   Headの位置座標はまた別のオブジェクトが管理している
            
          上確認したけど正しそうだった。頭の位置座標はCenterEyeAnchorですね
          
          あとは配列の初期状態では必ず0が代入されるので、その値と現在の右手のY座標とで加速度計算すると
          とてつもなく早くなってしまう。この問題に対処する。

        　CheckPosY_Array()で対処完了した
        */


        // 1フレーム前の手のY座標から加速度を計算するのでは感度が高すぎて、
        // ちょっとした手の挙動で音がなってしまう
        // なので10フレーム前のY座標から加速度を計算する
        if(Time.frameCount > posY_Array.Length)
        {
            acceleration = (gameObject.GetComponent<Transform>().position.y - posY_Array[posY_Array.Length - 1]) / (posY_Array.Length * Mathf.Pow(Time.deltaTime, 2));
        }

        if(CheckPosY_Array())
        {
            // 手を振り下ろしたり振り上げた直後を検知しないように、少々の余裕を持たせてある
            // threshold は Inspector 上では現状 -30 に設定してある
            if (acceleration < threshold * 2 && Time.frameCount-inactivatedFrame > 45)
            {
                SetAllButtons(true);
                UIOpenSound.SetActive(true);
                UIPositionSetter.SetActive(true);
                activatedFrame = Time.frameCount;
            }

            if(acceleration > -threshold * 3  && Time.frameCount-activatedFrame > 45)
            {
                SetAllButtons(false);
                UIOpenSound.SetActive(false);
                UIPositionSetter.SetActive(false);
                inactivatedFrame = Time.frameCount;
            }
            
        }

        /*
         * CheckPosY_Array()を追加したので、右手が原点に位置している状態を抜く以下の数行は必要ないかもしれない
         * しかしこれを削除してしまうと画面外から手が消えた後、また画面内に手が表示された時にメニューが呼び出されてしまう
         * という問題が発生した。
         * と思ったが、思い過ごしのようだ。
         * 以下の数行はなくてもいいだろう。ただとりあえずのところ残しておくことにする。
        if (gameObject.GetComponent<Transform>().position.y != 0f)
        {
            for (int i = posY_Array.Length - 1; i > 0; i--)
            {
                posY_Array[i] = posY_Array[i - 1];
            }
            posY_Array[0] = gameObject.GetComponent<Transform>().position.y;
        }
        */

        for (int i = posY_Array.Length - 1; i > 0; i--)
        {
            posY_Array[i] = posY_Array[i - 1];
        }
        posY_Array[0] = gameObject.GetComponent<Transform>().position.y;

    }

    bool CheckPosY_Array()
    {
        // HandTrackingがなされない場合、RightHandAnchorは原点へと移動してしまう
        // その原点座標が posY_Array に保持されてしまっている場合、falseを返すメソッド
        // posY_Arrayの初期状態も同様に0を保持してしまうので、この場合を弾くための関数
        bool notOrigin = true;
        for(int i = 0; i < posY_Array.Length; i++)
        {
            if (posY_Array[i] == 0f) notOrigin = false;
        }
        return notOrigin;
    }

    public void SetAllButtons(bool state)
    {
        if(state == false)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<ChangeButtonColor>().active = false;
            }
            button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
        }
        
    }
}
