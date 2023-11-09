using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // UI objects
    public static UI Singleton;

    // start button
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    // instructions button
    //public void Instructions()
    //{
    //    SceneManager.LoadScene("Instructions");
    //}

    //public void Menu()
    //{
    //    SceneManager.LoadScene("Menu");
    //}

    // quit button
    public void Quit()
    {
        Application.Quit();
    }
}
