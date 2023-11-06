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

    // track the number of existing crystals
    private float numCrystals;

    // track score
    private float score;

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
    }

    // Update is called once per frame
    private void Update()
    {
        
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

    // change health internal UI
    private void ChangeHealthInternal(float damage)
    {
        healthBar.fillAmount -= (float) damage / 100;
    }
}
