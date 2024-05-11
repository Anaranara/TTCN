using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> Scoreboard = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI Coins;
    private float[] scores = new float[5];

    private void Start()
    {

        Initiatescore();
        updateboardfromprefs();
        SaveFloatArray("Scores",scores);
    }

    private void Update()
    {
        updateitemstat();
        ProgressContain.totalCoins = LoadInteger("coin"); 
        Coins.text = "Total coins: " + ProgressContain.totalCoins;
        SaveInteger("coin",ProgressContain.totalCoins);
    }

    private void updateboardfromprefs()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            Scoreboard[i].text = scores[i].ToString();
        }
    }

    private void Initiatescore()
    {
        scores = LoadFloatArray("Scores");
        if (scores.Length > 5)
        {
            float[] nA = new float[5];
            Array.Copy(scores, nA, 5);
            scores = nA;
        }
        ProgressContain.HighScore = new List<float>();
        ProgressContain.HighScore.AddRange(scores);
        ProgressContain.HighScore.Sort((a, b) => b.CompareTo(a));
    }

    //update so luong item tu playerprefs sau khi bat lai game
    private void updateitemstat()
    {
        ProgressContain.Hpincreaseitem = LoadInteger("item1");
        //ProgressContain. = LoadInteger("item2");
        //ProgressContain. = LoadInteger("item3");
        //ProgressContain. = LoadInteger("item4");
        //ProgressContain. = LoadInteger("item5");
        //ProgressContain. = LoadInteger("item6");
    } 




    //prefsmanager
    public static void SaveInteger(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public static int LoadInteger(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return 0;
        }
    }

    // Save a float array into PlayerPrefs
    public static void SaveFloatArray(string key, float[] floatArray)
    {
        // Serialize the array to JSON
        string jsonString = JsonUtility.ToJson(new SerializableArray<float>(floatArray));

        // Save the JSON string into PlayerPrefs
        PlayerPrefs.SetString(key, jsonString);
        PlayerPrefs.Save();
    }

    // Load a float array from PlayerPrefs
    public static float[] LoadFloatArray(string key)
    {
        float[] floatArray = new float[0]; // Default empty array

        // Get the JSON string from PlayerPrefs
        string jsonString = PlayerPrefs.GetString(key);

        if (!string.IsNullOrEmpty(jsonString))
        {
            // Deserialize the JSON string to a SerializableArray object
            SerializableArray<float> serializableArray = JsonUtility.FromJson<SerializableArray<float>>(jsonString);

            if (serializableArray != null && serializableArray.items != null)
            {
                // Convert the SerializableArray to a regular float array
                floatArray = serializableArray.items;
            }
        }

        return floatArray;
    }
}
[System.Serializable]
public class SerializableArray<T>
{
    public T[] items;

    public SerializableArray(T[] array)
    {
        items = array;
    }
}

