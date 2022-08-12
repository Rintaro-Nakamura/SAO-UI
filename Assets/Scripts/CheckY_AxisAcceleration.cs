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
        /* HandTracking������ɉғ����Ă���Ԃ́ARightHandAnchor�͎�̈ʒu��Ԃ��Ă����
           ����������g���b�L���O�ł��Ȃ��ꍇ�ARightHandAnchor��position�̒l�͌��_�ւƕԂ���Ă��܂�
           ���̃v���W�F�N�g�ł͓��̈ʒu��Y���W�}�C�i�X�̒l�ɂ����Ď��s���Ă��邽�߁AHandTracking�����Ă��Ȃ���ԂƂ��Ă����Ԃ̊Ԃ�
           �����x���v�Z�����ƁA�K����Βl���傫�Ȓl�ɂȂ��Ă��܂�
           
           ���������玦������邱�Ɓ�
        �@ �@OVRCameraRig ���g�p�����ꍇ��Hand�̍��W���đ��΍��W����Ȃ���΍��W�ŕ\����Ă���
        �@   Head�̈ʒu���W�͂܂��ʂ̃I�u�W�F�N�g���Ǘ����Ă���
            
          ��m�F�������ǐ��������������B���̈ʒu���W��CenterEyeAnchor�ł���
          
          ���Ƃ͔z��̏�����Ԃł͕K��0����������̂ŁA���̒l�ƌ��݂̉E���Y���W�Ƃŉ����x�v�Z�����
          �ƂĂ��Ȃ������Ȃ��Ă��܂��B���̖��ɑΏ�����B

        �@CheckPosY_Array()�őΏ���������
        */


        // 1�t���[���O�̎��Y���W��������x���v�Z����̂ł͊��x���������āA
        // ������Ƃ�����̋����ŉ����Ȃ��Ă��܂�
        // �Ȃ̂�10�t���[���O��Y���W��������x���v�Z����
        if(Time.frameCount > posY_Array.Length)
        {
            acceleration = (gameObject.GetComponent<Transform>().position.y - posY_Array[posY_Array.Length - 1]) / (posY_Array.Length * Mathf.Pow(Time.deltaTime, 2));
        }

        if(CheckPosY_Array())
        {
            // ���U�艺�낵����U��グ����������m���Ȃ��悤�ɁA���X�̗]�T���������Ă���
            // threshold �� Inspector ��ł͌��� -30 �ɐݒ肵�Ă���
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
         * CheckPosY_Array()��ǉ������̂ŁA�E�肪���_�Ɉʒu���Ă����Ԃ𔲂��ȉ��̐��s�͕K�v�Ȃ���������Ȃ�
         * ������������폜���Ă��܂��Ɖ�ʊO����肪��������A�܂���ʓ��Ɏ肪�\�����ꂽ���Ƀ��j���[���Ăяo����Ă��܂�
         * �Ƃ�����肪���������B
         * �Ǝv�������A�v���߂����̂悤���B
         * �ȉ��̐��s�͂Ȃ��Ă��������낤�B�����Ƃ肠�����̂Ƃ���c���Ă������Ƃɂ���B
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
        // HandTracking���Ȃ���Ȃ��ꍇ�ARightHandAnchor�͌��_�ւƈړ����Ă��܂�
        // ���̌��_���W�� posY_Array �ɕێ�����Ă��܂��Ă���ꍇ�Afalse��Ԃ����\�b�h
        // posY_Array�̏�����Ԃ����l��0��ێ����Ă��܂��̂ŁA���̏ꍇ��e�����߂̊֐�
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
