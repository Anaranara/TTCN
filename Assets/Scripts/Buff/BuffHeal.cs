using UnityEngine;

[System.Serializable]
public class BuffHeal : BuffBase
{
    public int healHpPer5s;
    public BuffHeal()
    {
        buffName = "healing hp per 5s";
        healHpPer5s = 0;
        priceToUpdate = 20;
        currentLevel = 1;
        levelMax = 10;
    }

    public override void UpLevel()
    {
        if (currentLevel < levelMax)
        {
            currentLevel += 1;
            healHpPer5s = currentLevel - 1;
            priceToUpdate += Mathf.RoundToInt(0.5f * priceToUpdate);
        }
    }
}