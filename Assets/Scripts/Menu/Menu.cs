using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Menu : MonoBehaviour
{

    public void PLay()
    {
        StartCoroutine(Playbut());
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Deleteprefs()
    {
        PlayerPrefs.DeleteAll();
        float[] a = new float[]
        {
            0, 0, 0, 0, 0
        };
        StatsManager.SaveFloatArray("Scores", a);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Playbut()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameScene");
    }
}
