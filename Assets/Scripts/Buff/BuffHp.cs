using UnityEngine;
[System.Serializable]
public class BuffHp : BuffBase
{
    public int hpMax;
    public BuffHp()
    {
        buffName = "hp";
        hpMax = 20;
        priceToUpdate = 10;
        currentLevel = 1;
        levelMax = 20;
    }

    public override void UpLevel()
    {
        if (currentLevel < levelMax)
        {
            currentLevel += 1;
            hpMax += 1;
            priceToUpdate += Mathf.RoundToInt(0.2f * priceToUpdate);
        }
    }
}