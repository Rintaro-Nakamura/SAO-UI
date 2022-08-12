using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private GameObject[] buttonColor = new GameObject[2];
    public bool active;
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(active == false)
        {
            buttonColor[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            buttonColor[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else if (active == true)
        {
            buttonColor[0].GetComponent<Image>().color = new Color32(255, 186, 0, 255);
            buttonColor[1].GetComponent<Image>().color = new Color32(255, 186, 0, 255);

        }
    }
}
