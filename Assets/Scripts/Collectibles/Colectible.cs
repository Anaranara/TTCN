using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectible : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private AudioSource collectsound;
    [SerializeField] private GameObject g;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (g.CompareTag("Heal") && collision.CompareTag("Player"))
        {
            PlayerHealthsystem heal = collision.GetComponent<PlayerHealthsystem>();
            if (heal != null)
            {
                heal.healthchange(5);
                ani.SetBool("Destroyed", true);
                collectsound.Play();
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                ani.SetBool("Destroyed", true);
                GameManager.Instance.coins++;
                collectsound.Play();
            }
        }
    }
}
