using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hitpoint = 10;
    public int maxHitpoint = 10;

    private void Update()
    {
        if(hitpoint == 0)
        {
            Death();
        }
    }

    //All fighters can RecieveDamage / Die
    public void ReceiveDamage(int dmg)
    {
        hitpoint -= dmg;

        if (hitpoint <= 0)
        {
            hitpoint = 0;
            Death();
        }
    }

    protected virtual void Death()
    {

    }
}
