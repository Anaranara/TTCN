using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagezone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealthsystem dmg = other.GetComponent<PlayerHealthsystem>();

        if (dmg != null)
        {
            dmg.healthchange(-5);
        }
    }
}
