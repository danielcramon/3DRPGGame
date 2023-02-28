using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCollider : MonoBehaviour
{
    private bool inRange;
    public Text infoText;
    public PlayerController player;
    public Weapon weapon;

    // Start is called before the first frame update
    private void Start()
    {
        inRange = false;
    }

    private void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            infoText.text = "Press 1 to swing weapon";
            inRange = false;
            player.PickUpItem(weapon);
            Destroy(gameObject);
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inRange = true;
            infoText.text = "Press E to pickup the weapon";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inRange = false;
            infoText.text = "";
        }
    }
}
