using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public bool isplay;
    [NonSerialized] public int coins;

    [SerializeField] private GameObject Panel;
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        isplay = false;
        if (Instance == null) Instance = this;
        else Debug.Log("Failed");
    }

    #endregion

    

    public void Start()
    {
        coins = 0;
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            Panel.SetActive(false);
            isplay = true;
        }
    }

    public void gameover()
    {
        if (isplay)
        {  
            isplay = false;
            ProgressContain.score = float.Parse(Scorescript.Instance.Roundedscore());
        }
    }

    public void getscore()
    {
        for (int i = 0; i < ProgressContain.HighScore.Count; i++)
        {
            Debug.Log(i + ": " + ProgressContain.HighScore[i]);
            if (ProgressContain.HighScore[i] > 0 && i < ProgressContain.HighScore.Count)
            {
                ProgressContain.HighScore.Insert(i, ProgressContain.score);
                ProgressContain.HighScore.RemoveRange(5, ProgressContain.HighScore.Count - 5);
                break;
            }
            else if (i < ProgressContain.HighScore.Count)
            {
                ProgressContain.HighScore[i] = ProgressContain.score;
                break;
            }
        }
        float[] scored = new float[10];
        scored = ProgressContain.HighScore.ToArray();
        StatsManager.SaveFloatArray("Scores", scored);
    }

    public void getcoin()
    {
        ProgressContain.totalCoins += coins;
        StatsManager.SaveInteger("coin", ProgressContain.totalCoins);
        Debug.Log(ProgressContain.totalCoins);
    }
}