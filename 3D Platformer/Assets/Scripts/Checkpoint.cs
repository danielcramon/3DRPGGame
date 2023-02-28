using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public HealthManager theHealthMan;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theHealthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach(Checkpoint checkpoint in checkpoints)
        {
            checkpoint.CheckpointOff();
        }

        anim.SetBool("isActive", true);
    }

    public void CheckpointOff()
    {
        anim.SetBool("isActive", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            theHealthMan.SetSpawnPoint(transform.position);
            CheckPointOn();
        }
    }
}
