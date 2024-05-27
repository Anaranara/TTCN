using TMPro;
using UnityEngine;

public class ReduceDmgInStore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI reduceText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject noMoneyScr;
    [SerializeField] GameObject button;

    BuffArmor buffArmor;
    private void Start()
    {
        buffArmor = BuffDataManager.LoadBuffArmorData();
        UpdateText();
    }

    private void UpdateText()
    {
        levelText.text = "Level: " + buffArmor.currentLevel.ToString();
        reduceText.text = "Reduce: " + buffArmor.reduceDmg * 100f + " %";
        priceText.text = "Price: " + buffArmor.priceToUpdate.ToString() + " $";
        if(buffArmor.currentLevel == buffArmor.levelMax)
        {
            priceText.enabled = false;
            button.SetActive(false);
        }
    }

    public void BuyBtnClick()
    {
        if (ProgressContain.totalCoins < buffArmor.priceToUpdate)
        {
            noMoneyScr.SetActive(true);
        }
        else
        {
            ProgressContain.totalCoins -= buffArmor.priceToUpdate;
            StatsManager.SaveInteger("coin", ProgressContain.totalCoins);
            buffArmor.UpLevel();
            UpdateText();
            BuffDataManager.SaveBuffArmorData(buffArmor);
        }

    }
}