using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class Ui : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject Pausebutton;
    [SerializeField] private float Timer = 0.5f;

    private bool paused = false;
    PlayerHealthsystem phs;
    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void OnGUI()
    {
        score.text = "Score: " + Scorescript.Instance.Roundedscore();
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        Pausebutton.SetActive(false);
        PauseMenu.SetActive(true);
    }
    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        Pausebutton.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void Retry()
    {
        gm.getcoin();
        gm.getscore();
        StartCoroutine(TransisRetry());
        gm.coins = 0;
        Time.timeScale = 1f;
    }

    public void Tomenu()
    {
        Time.timeScale = 1f;
        gm.getscore();
        gm.getcoin();
        StartCoroutine(Transistomenu());
    }

    IEnumerator Transistomenu()
    {
        yield return new WaitForSeconds(Timer);
        SceneManager.LoadScene("Start_Menu");
    }

    IEnumerator TransisRetry()
    {
        yield return new WaitForSeconds(Timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
