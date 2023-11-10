using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // UI object
    public static UI Singleton;

    // start button
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    // instructions button
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    // menu button
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // quit button
    public void Quit()
    {
        Application.Quit();
    }
}
