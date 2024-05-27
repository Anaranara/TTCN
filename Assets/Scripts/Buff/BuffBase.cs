
[System.Serializable]
public class BuffBase
{
    public string buffName;
    public int currentLevel;
    public int levelMax;
    public int priceToUpdate;

    public virtual void UpLevel()
    {

    }
}
