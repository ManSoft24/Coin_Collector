using UnityEngine;
using System;
using System.Collections;


public class PlayerAttack : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable enemyScript = other.GetComponent<IDamageable>();
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                enemyScript.KenaDamage(enemy.damageSaatTabrakan);
            }
        }
    }
}

