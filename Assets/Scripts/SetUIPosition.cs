using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUIPosition : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject UI;
    // Start is called before the first frame update
    public void Start()
    {/*
        UI.SetActive(true);
        Vector3 handPosition = rightHand.GetComponent<Transform>().position;
        UI.GetComponent<Transform>().position = new Vector3(handPosition.x, handPosition.y, handPosition.z+0.20f);
        // ���R�͕s������������Enabled�ɕς���ƓK�؂ɔ������Ȃ�

        // �֐����̊ԈႢ�ɋC�Â��AOnEnable�ɕύX����Ƒ؂�Ȃ������悤�ɂȂ���
        */
    }

    public void Update()
    {
        // UI.SetActive(true);
        // UI.GetComponent<Transform>().position = rightHand.GetComponent<Transform>().position;
    }
    public void OnEnable()
    {
        UI.SetActive(true);
        Vector3 handPosition = rightHand.GetComponent<Transform>().position;
        UI.GetComponent<Transform>().position = new Vector3(handPosition.x, handPosition.y + 0.25f, handPosition.z + 0.20f);
    }
}
