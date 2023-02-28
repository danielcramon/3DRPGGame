using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Vector3 pickupPosition;
    public Vector3 pickUpRotation;
    public int Damage;
    private bool isAttacking;
    public float attackCooldown;
    private float attackCounter;
    private bool equipped = false;
    public Text infoText;
    public Animator anim;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isAttacking == false && equipped == false)
        {
            infoText.text = "You need to equip a weapon to attack";
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && isAttacking == false && equipped == true)
        {
            isAttacking = true;
            attackCounter = attackCooldown;
            infoText.text = "";
            Attack();
        }
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                isAttacking = false;
            }
        }
        anim.SetBool("isAttacking", isAttacking);
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Fighter")
        {
            other.SendMessage("ReceiveDamage", Damage);
        }

    }

    public void SetEquipped(bool value)
    {
        equipped = value;
    }

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().ReceiveDamage(Damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
