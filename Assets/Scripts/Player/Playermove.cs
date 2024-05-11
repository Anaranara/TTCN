using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    [SerializeField] private float jump = 8f;
    [SerializeField] private float moveforward = 4.0f;
    [SerializeField] private float slowdown = -3f;
    [SerializeField] private Transform gcheck;
    [SerializeField] private Transform Uppercheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jtime = 0.3f;
    [SerializeField] private float checkdis = 0.25f;
    [SerializeField] private BoxCollider2D p;
    [SerializeField] private AudioSource Jumpsound;
    [SerializeField] private AudioSource SlideSound;

    private float speeduptimer = 20f;
    private float Deathtimer = 2f;
    private float crouchtimer = 2f;
    private int maxmilestone = 4;

    private Animator ani;
    private CharState st;
    private Rigidbody2D rb;
    private bool isgrounded = false;
    private bool upper = false;
    private bool isJump = false;
    private bool isStay = false;
    private float jtimer;
    private float lastpx;

    public static bool iscrouch;
    public static float SpeedMultiplier = 1.15f;
    public static int Speedmilestone = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        lastpx = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isplay)
        {
            #region Move

            Vector2 move = rb.velocity;
            move.x = moveforward;
            rb.velocity = move;
            speeduptimer -= Time.deltaTime;
            speeduptimer = Mathf.Clamp(speeduptimer,0,speeduptimer); 
            if (speeduptimer == 0)
            {
                Speedmilestone += 1;
                if (Speedmilestone < maxmilestone)
                {
                    moveforward = moveforward * SpeedMultiplier;
                    speeduptimer = 20f;
                } 
            }

            #endregion

            #region Jump

            isgrounded = Physics2D.OverlapCircle(gcheck.position, checkdis, ground);

            if (Input.GetButtonDown("Jump") && isgrounded)
            {
                Jumpsound.Play();
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
            if (isJump && Input.GetButton("Jump"))
            {
                Vector2 vel = rb.velocity;
                vel.y -= -9.8f * Time.deltaTime;
                rb.velocity = vel;
                if (jtimer < jtime)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jump);
                    jtimer += Time.deltaTime;
                }
                else
                {
                    isJump = false;
                }
                
            }
            if (Input.GetButtonUp("Jump"))
            {
                isJump = false;
                jtimer = 0;
            }

            #endregion

            #region Crouch
            upper = Physics2D.OverlapCircle(Uppercheck.position, checkdis, ground);
            if (isgrounded && Input.GetButton("Crouch"))
            {            
                if (crouchtimer > 0)
                {
                    iscrouch = true;
                    SlideSound.Play();
                    p.size = new Vector2(1.4f, 0.9f);
                    p.offset = new Vector2(0f, -0.5f);
                    crouchtimer -= Time.deltaTime;
                    rb.velocity += new Vector2(slowdown * Time.deltaTime,rb.velocity.y);
                }
                else
                {
                    SlideSound.Stop();
                    p.size = new Vector2(1.1f, 1.5f);
                    p.offset = new Vector2(0f, -0.25f);
                    rb.simulated = false;
                }
                if (isJump)
                {
                    p.size = new Vector2(1.1f, 1.5f);
                    p.offset = new Vector2(0f, -0.25f);
                }
            }
            if (upper)
            {
                if (Input.GetButtonUp("Crouch"))
                {
                    SlideSound.Stop();
                    Playergamesystem.Instance.isdead = true;
                }
            }
            else
            {
                if (Input.GetButtonUp("Crouch"))
                {
                    iscrouch = false;
                    SlideSound.Stop();
                    rb.simulated = true;
                    p.size = new Vector2(1.1f, 1.4f);
                    p.offset = new Vector2(0f, -0.27f);
                    crouchtimer = 2f;
                }
            }

            #endregion

            #region checkifnotmove
            float currpx = transform.position.x;
            if (currpx == lastpx)
            {
                isStay = true;
                if (Deathtimer > 0f)
                {
                    Deathtimer -= Time.deltaTime;
                }
                else
                {
                    rb.simulated = false;
                    Playergamesystem.Instance.isdead = true;
                }
            }
            else
            {
                isStay = false;
                Deathtimer = 3;
            }
            lastpx = currpx;

            #endregion
            updateani();
        }
    }

    void updateani()
    {
        #region aniupdate

        if (GameManager.Instance.isplay)
        {
            if (!isgrounded && isJump)
            {
                st = CharState.JUMP;
            }
            if (!isgrounded && !isJump)
            {
                st = CharState.FALL;
            }
            if (isgrounded && Input.GetButton("Crouch"))
            {
                st = CharState.SLIDE;
            }
            else if (isgrounded && !Input.GetButton("Crouch"))
            {
                st = CharState.RUN;
            }
            if (isStay && isgrounded)
            {
                st = CharState.IDLE;
            }
            ani.SetInteger("State", (int)st);
        }
        else
        {
            st = CharState.IDLE;
            ani.SetInteger("State", (int)st);
        }

        #endregion
    }

}
