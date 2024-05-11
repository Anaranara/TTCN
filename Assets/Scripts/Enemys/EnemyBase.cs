using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBase : MonoBehaviour
{
    private Rigidbody2D rb;
    private float enemySpeed = 15f;
    public ObjectPool<EnemyBase> refPool;
    private Animator ani;
    private Collider2D colli;
    private bool isDie = false;
    private int milestone;

    private PlayerHealthsystem phs;
    private  void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        colli = GetComponent<Collider2D>();
        Invoke(nameof(ReleaseObj), 30f);
        milestone = Playermove.Speedmilestone;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isDie)
        {
            Behaviour();
        }
        if (Playermove.Speedmilestone > milestone)
        {
            milestone += 1;
            enemySpeed *= Playermove.SpeedMultiplier;
        }
    }

    private void Behaviour()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        //transform.position += enemySpeed * Time.deltaTime * Vector3.left;
        Vector2 speed = rb.velocity;
        speed.x = -enemySpeed;
        rb.velocity = speed;
    }

    void ReleaseObj()
    {
        if (isDie)
        {
            return;
        }
        else
        {
            refPool.Release(this);
            isDie = true;
        }
    }

    public void ResetNewEnemy(Vector3 pos)
    {
        isDie = false;
        GetComponent<Collider2D>().enabled = true;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            phs = other.gameObject.GetComponent<PlayerHealthsystem>();
            if (phs != null && !Playermove.iscrouch)
            {
                phs.healthchange(-8);
            }
            isDie = true;
            rb.AddForce(new Vector2(1, 5), ForceMode2D.Impulse);
            colli.enabled = false;

            ani.SetInteger("State", 4);
            StartCoroutine(ReleaseObjectAfterDelay(1f));
        }
    }

    private IEnumerator ReleaseObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        refPool.Release(this);
    }
}