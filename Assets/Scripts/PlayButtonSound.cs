using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    private int unplayable;
    public GameObject[] otherButtons = new GameObject[4];
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        unplayable = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool button = gameObject.GetComponent<ChangeButtonColor>().active;
        if (button == false)
        {
            GetComponent<AudioSource>().Play();
            gameObject.GetComponent<ChangeButtonColor>().active = true;
            for (int i = 0; i < 4; i++)
            {
                otherButtons[i].GetComponent<ChangeButtonColor>().active = false;
            }
        }
    }

}
