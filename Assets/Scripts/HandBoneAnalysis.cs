using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBoneAnalysis : MonoBehaviour
{

    // https://qiita.com/divideby_zero/items/4949fadb2c60f810b3aa
    // この記事をもとに手の形を取得しようとしている
    // しかし現状４０行目の Vector3 r で配列のサイズを超えてしまっているというエラーが生じる
    [SerializeField] private OVRSkeleton skeleton;
    // SerializeFieldはInspectorで操作したいけどprivateに
    // したい変数に使用する

    public void Update()
    {
        Debug.Log(isStraight(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip));
    }


    private bool isStraight(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        // params は可変長引数
        if (boneids.Length < 3) return false;
        Vector3? oldVec = null;
        var dot = 1.0f;

        Debug.Log("boneids.Length : "+boneids.Length);

        for(var index = 0; index < boneids.Length-1; index++)
        {

            // Debug.Log("(int)boneids[index + 1] : " + (int)boneids[index + 1]);
            // Debug.Log("(int)boneids[index] : " + (int)boneids[index]);

            // 現状生じているエラーについては、Bones[ x ]という形での値取得が間違っていて、get( x ) のような物があれば
            // そちらを使用したいと考えている
            // boneids[]のエラーでは無いはず
            // https://developer.oculus.com/documentation/unity/unity-handtracking/#:~:text=hands%20as%20input%3A-,Get%20Bone%20ID,-OVR%20Skeleton%20contains
            // https://developer.oculus.com/reference/unity/v41/class_o_v_r_bone
            Debug.Log("now bones : " + skeleton.GetCurrentNumBones());

            Vector3 v = (skeleton.Bones[(int)boneids[index + 1]].Transform.position - skeleton.Bones[(int)boneids[index]].Transform.position).normalized;
            // .normalized はベクトルの正規化のこと
            // 向きは同じで大きさが１のベクトル

            // 確かにベクトルの大きさが１なら、内積は-1～+1の値の範囲に収まるな

            // HasValueはbool型
            // 特定の変数が値を持っているか返すフィールド
            if(oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value);
            }
            oldVec = v; // 一つ前の指ベクトルを格納する変数
        }
        return dot >= threshold; // 指定したBoneIdの内積の相乗が閾値を超えていたら直線とみなす

    }
}
