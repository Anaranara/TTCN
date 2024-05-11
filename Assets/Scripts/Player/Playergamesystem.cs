using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Playergamesystem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D p;
    [SerializeField] private GameObject DeadGFX;
    [NonSerialized] public bool isdead;
    [SerializeField] private SpriteRenderer c;
    [SerializeField] private GameObject end,pause,text;
    [SerializeField] private TextMeshProUGUI info;
    [SerializeField] private AudioSource Deadsound;
    [SerializeField] private GameManager gm;

    private PlayerHealthsystem phs;
    #region Singleton

    public static Playergamesystem Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.Log("Failed");
    }

    #endregion

    private void Start()
    {
        phs = gameObject.GetComponent<PlayerHealthsystem>();
        isdead = false;
    }
    private void Update()
    {
        if (isdead)
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bounds"))
        {
            Dead();
        }
        if (collision.CompareTag("Hidden"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(seconds(collision));
        }
    }

    private void Dead()
    {
        Deadsound.Play();
        ProgressContain.score = Scorescript.Instance.score;
        p.simulated = false;
        c.enabled = false;
        DeadGFX.SetActive(true);
        end.SetActive(true);
        gm.gameover();
        pause.SetActive(false);
        text.SetActive(false);
        info.text = "Score: " + Scorescript.Instance.Roundedscore() + "\n" + "\n" + "Coins collected: " + gm.coins;
    }
    IEnumerator seconds(Collider2D collision)
    {
        yield return new WaitForSeconds(2f);
        collision.gameObject.SetActive(true);
    }
}
