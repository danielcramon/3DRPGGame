using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text infoText;
    public string[] introduction;
    private int counter = 0;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            PlayText(counter);
        }

        if(counter == 0 && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
        {
            counter = 1;
            isActive = false;
        }
        if(counter == 1 && Input.GetButtonDown("Jump"))
        {
            counter = 2;
            isActive = false;
        }
    }

    public void PlayText(int i)
    {
        infoText.text = introduction[i];
        isActive = true;
    }
}
