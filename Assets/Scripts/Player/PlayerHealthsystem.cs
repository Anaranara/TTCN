using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthsystem : MonoBehaviour
{
    public int currhealth;
    public int maxhealth = 25;
    private bool immo;
    private float immotime = 2f;
    private float immotimer;
    private float blinktimer;

    public Slider fillhp;
    [SerializeField] private float blink = 0.1f;

    private SpriteRenderer sp;

    void Start()
    {
        fillhp.maxValue = maxhealth;
        fillhp.value = maxhealth;
        sp = GetComponent<SpriteRenderer>();
        if (ProgressContain.Hpincreaseitem == 0)
        {
            currhealth = maxhealth;
        }
        else if(ProgressContain.Hpincreaseitem >= 1)
        {
            currhealth = maxhealth + 10;
            ProgressContain.Hpincreaseitem -= 1;
            StatsManager.SaveInteger("item1",ProgressContain.Hpincreaseitem);
        }
        Debug.Log(currhealth);
    }
    private void Update()
    {
        if (immo && !Playergamesystem.Instance.isdead)
        {
            immotimer -= Time.deltaTime;
            blinktimer -= Time.deltaTime;
            if(blinktimer <= 0)
            {
                sp.enabled = !sp.enabled;
                blinktimer = blink;
            }
            if (immotimer <= 0)
            {
                immo = false;
                sp.enabled = true;
            }
        }
        else if(Playergamesystem.Instance.isdead)
        {
            fillhp.value = 0;
        }
    }

    public void healthchange(int amount)
    {
        if(amount < 0)
        {
            if (immo)
                return;
            immo = true;
            immotimer = immotime;
            currhealth = Mathf.Clamp(currhealth + amount, 0, maxhealth);
            fillhp.value = currhealth;
        }
        else
        {
            currhealth = Mathf.Clamp(currhealth + amount, 0, maxhealth);
            fillhp.value = currhealth;
        }
        if(currhealth == 0)
        {
            Playergamesystem.Instance.isdead = true;
        }
    }
}
