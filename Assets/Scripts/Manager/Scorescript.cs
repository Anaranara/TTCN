using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class Scorescript : MonoBehaviour
{
    #region singleton

    public static Scorescript Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.Log("Failed");
    }

    #endregion

    [NonSerialized] public float score;
    [SerializeField] private Transform player;


    void Update()
    {
        if (GameManager.Instance.isplay)
        {
            score = (int)player.position.x;
        }
        score = Mathf.Clamp(score, 0, 999999999999999f);
    }
    public string Roundedscore()
    {
        return Mathf.RoundToInt(score).ToString();
    }
}
