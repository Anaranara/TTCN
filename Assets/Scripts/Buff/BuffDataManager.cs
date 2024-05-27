using UnityEngine;
public class BuffDataManager
{
    public static void SaveBuffHpData(BuffHp data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("BuffHp", json);
        PlayerPrefs.Save();
    }

    public static BuffHp LoadBuffHpData()
    {
        if (!PlayerPrefs.HasKey("BuffHp"))
        {
            SaveBuffHpData(new());
        }
        string json = PlayerPrefs.GetString("BuffHp");
        BuffHp data = JsonUtility.FromJson<BuffHp>(json);
        return data;
    }

    public static void SaveBuffHealData(BuffHeal data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("BuffHeal", json);
        PlayerPrefs.Save();
    }

    public static BuffHeal LoadBuffHealData()
    {
        if (!PlayerPrefs.HasKey("BuffHeal"))
        {
            SaveBuffHealData(new BuffHeal());
        }
        string json = PlayerPrefs.GetString("BuffHeal");
        BuffHeal data = JsonUtility.FromJson<BuffHeal>(json);
        return data;
    }

    public static void SaveBuffArmorData(BuffArmor data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("BuffArmor", json);
        PlayerPrefs.Save();
    }

    public static BuffArmor LoadBuffArmorData()
    {
        if (!PlayerPrefs.HasKey("BuffArmor"))
        {
            SaveBuffArmorData(new BuffArmor());
        }
        string json = PlayerPrefs.GetString("BuffArmor");
        BuffArmor data = JsonUtility.FromJson<BuffArmor>(json);
        return data;
    }
}