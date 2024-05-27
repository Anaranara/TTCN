using TMPro;
using UnityEngine;

public class HealingUIInStore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI healText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject noMoneyScr;
    [SerializeField] GameObject button;

    BuffHeal buffHeal;
    private void Start()
    {
        buffHeal = BuffDataManager.LoadBuffHealData();
        UpdateText();
    }

    private void UpdateText()
    {
        levelText.text = "Level: " + buffHeal.currentLevel.ToString();
        healText.text = "Healing: " + buffHeal.healHpPer5s.ToString();
        priceText.text = "Price: " + buffHeal.priceToUpdate.ToString() + " $";
        if (buffHeal.currentLevel == buffHeal.levelMax)
        {
            priceText.enabled = false;
            button.SetActive(false);
        }
    }

    public void BuyBtnClick()
    {
        if (ProgressContain.totalCoins < buffHeal.priceToUpdate)
        {
            noMoneyScr.SetActive(true);
        }
        else
        {
            ProgressContain.totalCoins -= buffHeal.priceToUpdate;
            StatsManager.SaveInteger("coin", ProgressContain.totalCoins);
            buffHeal.UpLevel();
            UpdateText();
            BuffDataManager.SaveBuffHealData(buffHeal);
        }
    }
}