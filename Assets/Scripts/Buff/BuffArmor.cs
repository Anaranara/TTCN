using UnityEngine;
[System.Serializable]
public class BuffArmor : BuffBase
{
    public float reduceDmg;

    public BuffArmor()
    {
        buffName = "Armor";
        reduceDmg = 0;
        priceToUpdate = 50;
        currentLevel = 1;
        levelMax = 5;
    }

    public override void UpLevel()
    {
        if (currentLevel < levelMax)
        {
            currentLevel += 1;
            reduceDmg += 0.1f;
            priceToUpdate += Mathf.RoundToInt(0.3f * priceToUpdate);
        }
    }
}