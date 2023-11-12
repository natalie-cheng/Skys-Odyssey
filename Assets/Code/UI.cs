using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class UI : MonoBehaviour
{
    // UI objects
    public static UI Singleton;
    public TextMeshProUGUI scoreText;
    public Image healthBar;
    public GameObject lossScreen;
    public GameObject pauseScreen;
    public GameObject helpScreen;

    // track the number of existing crystals
    public static float numCrystals;

    // track score
    public static float score;

    // game management
    public static bool collected;

    // call start
    private void Start()
    {
        // initialize UI object
        Singleton = this;

        // score is 0 to start
        scoreText.text = "0";
        score = 0;

        // find all the crystals
        numCrystals = FindObjectsOfType<Crystal>().Length;

        // disable ui screens
        lossScreen.SetActive(false);
        pauseScreen.SetActive(false);
        helpScreen.SetActive(false);

        // game management
        collected = false;
    }

    // frame update
    private void Update()
    {
        // check - esc is also pause option
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        // check if all the crystals are collected
        if (numCrystals == score)
        {
            collected = true;
        }
    }

    // static increase score
    public static void IncreaseScore()
    {
        Singleton.IncreaseScoreInternal();
    }

    // increase score interal UI
    private void IncreaseScoreInternal()
    {
        score++;
        // update text
        scoreText.text = score + "";
    }

    // static change health
    public static void ChangeHealth(float damage)
    {
        Singleton.ChangeHealthInternal(damage);
    }

    // change health display UI
    private void ChangeHealthInternal(float damage)
    {
        // change health bar fill
        healthBar.fillAmount -= (float) damage / SkySprite.maxHealth;
        // if skysprites health drops below 0, loss
        if (SkySprite.health <= 0)
        {
            SetGameLossInternal();
        }
    }

    // gameover internal ui
    private void SetGameLossInternal()
    {
        Time.timeScale = 0;
        lossScreen.SetActive(true);
    }

    // pause button/ui
    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    // instructions/help button/ui
    public void Help()
    {
        Time.timeScale = 0;
        helpScreen.SetActive(true);
    }

    // resume button
    public void Resume()
    {
        helpScreen.SetActive(false);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // restart button
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // load menu
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
