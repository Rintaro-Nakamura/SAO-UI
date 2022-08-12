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
        // 理由は不明だがここをEnabledに変えると適切に反応しない

        // 関数名の間違いに気づき、OnEnableに変更すると滞りなく動くようになった
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
