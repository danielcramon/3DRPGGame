using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{

    private bool isInRange;
    public Animator anim;
    public float openLength;
    private float openCounter;
    public DoorHandle doorHandle;

    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E) && !anim.GetBool("isOpen"))
        {
            doorHandle.ChangeHandle(true);
            anim.SetBool("isOpen", true);
            openCounter = openLength;
            infoText.text = "";
        }
        if(openCounter > 0)
        {
            openCounter -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("isOpen", false);
            doorHandle.ChangeHandle(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isInRange = true;
            infoText.text = "Press E to open the door";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isInRange = false;
            infoText.text = "";
        }
    }
}
