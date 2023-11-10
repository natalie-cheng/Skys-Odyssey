using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Instructions : MonoBehaviour
{
    // UI objects
    public static UI Singleton;
    public TextMeshProUGUI textContent;
    public TextMeshProUGUI buttonText;

    // true is state 1, false is state 2
    private bool state;

    private string text1 = "\nSky, the elemental sprite, is lost and can't get " +
        "home! Collect all 25 crystals to reactivate the portal and take her " +
        "home.\n\nBeware, the evil vorax are attracted to the crystals' " +
        "power, and will do anything to stop Sky from collecting them! Avoid " +
        "the vorax bullets, and defeat them to collect the crystals!";

    private string text2 = "1. Use arrows or wasd to move\n2. Press tab to " +
        "toggle between elemental states\n3. Press space to use an ability\n" +
        "Water Ability: Shield\nAir ability: Fly/Double jump\nFire Ability: " +
        "Shoot circle of fireballs\n\nWatch your health! Being hit by a " +
        "vorax bullet or colliding with a vorax will cause damage.";

    // call start
    private void Start()
    {
        textContent.text = text1;
        buttonText.text = "Next";
        state = true;
    }

    // switch the text button
    public void ToggleRead()
    {
        if (state)
        {
            textContent.text = text2;
            buttonText.text = "Back";
        }
        else
        {
            textContent.text = text1;
            buttonText.text = "Next";
        }
        state = !state;
    }

    // menu button
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
