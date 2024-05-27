using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthsystem : MonoBehaviour
{
    public int currhealth;
    public int maxhealth;
    private bool immo;
    private float immotime = 2f;
    private float immotimer;
    private float blinktimer;

    public Slider fillhp;
    [SerializeField] private float blink = 0.1f;
    [SerializeField] private TextMeshProUGUI HP;

    private SpriteRenderer sp;

    // buff
    BuffHp buffHp;
    BuffHeal buffHeal;

    void Start()
    {
        buffHp = BuffDataManager.LoadBuffHpData();
        buffHeal = BuffDataManager.LoadBuffHealData(); 
        maxhealth = buffHp.hpMax;
        currhealth = maxhealth;
        fillhp.maxValue = maxhealth;
        fillhp.value = currhealth;
        sp = GetComponent<SpriteRenderer>();

        // healing hp per 5s
        InvokeRepeating(nameof(Healing), 5, 5);
    }
    private void Update()
    {

        HP.text = currhealth + "/" + maxhealth;
        if (immo && !Playergamesystem.Instance.isdead)
        {
            immotimer -= Time.deltaTime;
            blinktimer -= Time.deltaTime;
            if (blinktimer <= 0)
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
        else if (Playergamesystem.Instance.isdead)
        {
            currhealth = 0;
            fillhp.value = 0;
        }
    }

    public void healthchange(int amount)
    {
        if (amount < 0)
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
        if (currhealth == 0)
        {
            Playergamesystem.Instance.isdead = true;
        }
    }

    private void Healing()
    {
        if (currhealth == maxhealth) return;
        currhealth = Mathf.Min(currhealth + buffHeal.healHpPer5s, maxhealth);
        fillhp.value = currhealth;
    }
}
