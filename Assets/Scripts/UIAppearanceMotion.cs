using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAppearanceMotion : MonoBehaviour
{
    private float diameter;
    private float timer;

    public float speed;
    public float buttonDistance;
    public float spreadHeight;
    public GameObject buttons;
    public GameObject[] button = new GameObject[5];
    private GameObject child;


    // Start is called before the first frame update
    void OnEnable()
    {
        /*
        for(int i = 0; i < 5; i++)
        {
            button[i].GetComponent<Transform>().position = new Vector3(0, 0, 0);
        }*/

        button[2].GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
        // increment��Inspector�Œl����������
        timer = 0;
        // transform.GetChild.gameObject �Ŏq�v�f���擾���AGetComponent.sizeDelta.y �ő傫�����擾���Ă���
        child = button[0].transform.GetChild(0).gameObject;
        diameter = child.GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        spread();
        shrink();
        timer += speed;
    }

    void spread()
    {

        if (timer < 1f)
        {
            
            Vector3 center = button[2].GetComponent<Transform>().position;
            button[0].GetComponent<Transform>().position = Vector3.Lerp(center, center + new Vector3(0, spreadHeight, 0), timer);
            button[1].GetComponent<Transform>().position = Vector3.Lerp(center, center + new Vector3(0, spreadHeight/2, 0), timer);
            button[3].GetComponent<Transform>().position = Vector3.Lerp(center, center - new Vector3(0, spreadHeight/2, 0), timer);
            button[4].GetComponent<Transform>().position = Vector3.Lerp(center, center - new Vector3(0, spreadHeight, 0), timer);
            

            /*
            Vector3 center = button[2].GetComponent<Transform>().position;
            button[0].GetComponent<Transform>().position = center;
            button[1].GetComponent<Transform>().position = center;
            button[3].GetComponent<Transform>().position = center;
            button[4].GetComponent<Transform>().position = center;
            */
        }
    }

    void shrink()
    {
        
        if (1 < timer && timer <= 2.0f)
        {
            
            // �ȉ��̐��s�ŁAbutton[2] �� buttons �̈ʒu�������
            // OnEnable �ŏC�����Ă��K�v������B
            for (int i = 3; i >= 0; i--)
            {
                button[i].GetComponent<Transform>().position = button[i + 1].GetComponent<Transform>().position
                                                             + Vector3.Lerp(new Vector3(0, spreadHeight / 2, 0), 
                                                                            new Vector3(0, buttonDistance, 0), 
                                                                            (timer - 1));
            }

            /*
            button[3].GetComponent<Transform>().position = button[4].GetComponent<Transform>().position
                                                         + Vector3.Lerp(new Vector3(0, spreadHeight / 2, 0),
                                                                        new Vector3(0, 0.1f, 0),
                                                                        timer - 1f);
            */

        }
    }
}
