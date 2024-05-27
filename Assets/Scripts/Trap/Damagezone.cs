using UnityEngine;

public class Damagezone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealthsystem phs = other.GetComponent<PlayerHealthsystem>();

        if (phs != null)
        {
            int dmg = 10; // mac dinh gay 10 st
            int dmgToPlayer = Mathf.RoundToInt(dmg * (1 - BuffDataManager.LoadBuffArmorData().reduceDmg));
            phs.healthchange(-dmgToPlayer);
            Debug.Log($"{phs.currhealth}/{phs.maxhealth}");
        }
    }
}
