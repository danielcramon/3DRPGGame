using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Enemy
{
    public GameObject destroyedVersion;

    protected override void Death()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
