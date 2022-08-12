using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBoneAnalysis : MonoBehaviour
{

    // https://qiita.com/divideby_zero/items/4949fadb2c60f810b3aa
    // ���̋L�������ƂɎ�̌`���擾���悤�Ƃ��Ă���
    // ����������S�O�s�ڂ� Vector3 r �Ŕz��̃T�C�Y�𒴂��Ă��܂��Ă���Ƃ����G���[��������
    [SerializeField] private OVRSkeleton skeleton;
    // SerializeField��Inspector�ő��삵��������private��
    // �������ϐ��Ɏg�p����

    public void Update()
    {
        Debug.Log(isStraight(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip));
    }


    private bool isStraight(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        // params �͉ϒ�����
        if (boneids.Length < 3) return false;
        Vector3? oldVec = null;
        var dot = 1.0f;

        Debug.Log("boneids.Length : "+boneids.Length);

        for(var index = 0; index < boneids.Length-1; index++)
        {

            // Debug.Log("(int)boneids[index + 1] : " + (int)boneids[index + 1]);
            // Debug.Log("(int)boneids[index] : " + (int)boneids[index]);

            // ���󐶂��Ă���G���[�ɂ��ẮABones[ x ]�Ƃ����`�ł̒l�擾���Ԉ���Ă��āAget( x ) �̂悤�ȕ��������
            // ��������g�p�������ƍl���Ă���
            // boneids[]�̃G���[�ł͖����͂�
            // https://developer.oculus.com/documentation/unity/unity-handtracking/#:~:text=hands%20as%20input%3A-,Get%20Bone%20ID,-OVR%20Skeleton%20contains
            // https://developer.oculus.com/reference/unity/v41/class_o_v_r_bone
            Debug.Log("now bones : " + skeleton.GetCurrentNumBones());

            Vector3 v = (skeleton.Bones[(int)boneids[index + 1]].Transform.position - skeleton.Bones[(int)boneids[index]].Transform.position).normalized;
            // .normalized �̓x�N�g���̐��K���̂���
            // �����͓����ő傫�����P�̃x�N�g��

            // �m���Ƀx�N�g���̑傫�����P�Ȃ�A���ς�-1�`+1�̒l�͈̔͂Ɏ��܂��

            // HasValue��bool�^
            // ����̕ϐ����l�������Ă��邩�Ԃ��t�B�[���h
            if(oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value);
            }
            oldVec = v; // ��O�̎w�x�N�g�����i�[����ϐ�
        }
        return dot >= threshold; // �w�肵��BoneId�̓��ς̑��悪臒l�𒴂��Ă����璼���Ƃ݂Ȃ�

    }
}
