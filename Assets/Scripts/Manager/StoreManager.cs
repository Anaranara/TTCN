using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> have = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject nomoney;

    private void Update()
    {
        Maxmin();
        Quantity();
    }
    public void Buy(int Item)
    {
        switch(Item){
            case 0:
                if (ProgressContain.totalCoins - 50 < 0)
                {
                    nomoney.SetActive(true);
                }
                else
                {
                    ProgressContain.Hpincreaseitem += 1;
                    ProgressContain.totalCoins -= 50;
                    StatsManager.SaveInteger("coin",ProgressContain.totalCoins);
                    StatsManager.SaveInteger("item1",ProgressContain.Hpincreaseitem);
                }
                break;
            //case 1:
            //    if (ProgressContain.totalCoins - 50 < 0)
            //    {
            //        nomoney.SetActive(true);
            //    }
            //    else
            //    {
            //        ProgressContain.Hpincreaseitem += 1;
            //        ProgressContain.totalCoins -= 50;
            //    }
            //    break;
            //case 2:
            //    if (ProgressContain.totalCoins - 50 < 0)
            //    {
            //        nomoney.SetActive(true);
            //    }
            //    else
            //    {
            //        ProgressContain.Hpincreaseitem += 1;
            //        ProgressContain.totalCoins -= 50;
            //    }
            //    break;
            //case 3:
            //    if (ProgressContain.totalCoins - 50 < 0)
            //    {
            //        nomoney.SetActive(true);
            //    }
            //    else
            //    {
            //        ProgressContain.Hpincreaseitem += 1;
            //        ProgressContain.totalCoins -= 50;
            //    }
            //    break;
            //case 4:
            //    if (ProgressContain.totalCoins - 50 < 0)
            //    {
            //        nomoney.SetActive(true);
            //    }
            //    else
            //    {
            //        ProgressContain.Hpincreaseitem += 1;
            //        ProgressContain.totalCoins -= 50;
            //    }
            //    break;
            //case 5:
            //    if (ProgressContain.totalCoins - 50 < 0)
            //    {
            //        nomoney.SetActive(true);
            //    }
            //    else
            //    {
            //        ProgressContain.Hpincreaseitem += 1;
            //        ProgressContain.totalCoins -= 50;
            //    }
            //    break;
        }
    }

    private void Maxmin()
    {
        ProgressContain.totalCoins = Mathf.Clamp(ProgressContain.totalCoins,0,10000);
        ProgressContain.Hpincreaseitem = Mathf.Clamp(ProgressContain.Hpincreaseitem,0,99);
    }

    private void Quantity()
    {
        have[0].text = "You have: " + ProgressContain.Hpincreaseitem;
        //have[1].text = "You have: " + ProgressContain.;
        //have[2].text = "You have: " + ProgressContain.;
        //have[3].text = "You have: " + ProgressContain.;
        //have[4].text = "You have: " + ProgressContain.;
        //have[5].text = "You have: " + ProgressContain.;
    }
}
