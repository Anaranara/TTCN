using TMPro;
using UnityEngine;

public class HpUIInStore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI hpmaxText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject noMoneyScr;
    [SerializeField] GameObject button;

    BuffHp buffHp;
    private void Start()
    {
        buffHp = BuffDataManager.LoadBuffHpData();
        UpdateText();
        StatsManager.SaveInteger("coin", ProgressContain.totalCoins);
    }

    private void UpdateText()
    {
        levelText.text = "Level: " + buffHp.currentLevel.ToString();
        hpmaxText.text = "Hp: " + buffHp.hpMax.ToString();
        priceText.text = "Price: " + buffHp.priceToUpdate.ToString() + " $";
        if (buffHp.currentLevel == buffHp.levelMax)
        {
            priceText.enabled = false;
            button.SetActive(false);
        }
    }

    public void BuyBtnClick()
    {
        if (ProgressContain.totalCoins < buffHp.priceToUpdate)
        {
            noMoneyScr.SetActive(true);
        }
        else
        {
            ProgressContain.totalCoins -= buffHp.priceToUpdate;
            StatsManager.SaveInteger("coin", ProgressContain.totalCoins);
            buffHp.UpLevel();
            UpdateText();
            BuffDataManager.SaveBuffHpData(buffHp);
        }

    }
}
