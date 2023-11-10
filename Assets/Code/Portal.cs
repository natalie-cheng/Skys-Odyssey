using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // track whether the portal is active
    private bool active;

    private SpriteRenderer spriteRenderer;

    // call start
    private void Start()
    {
        active = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // frame update
    private void Update()
    {
        // if all the crystals are collected, activate portal
        if (UI.collected)
        {
            Activate();
        }
    }

    private void Activate()
    {
        active = true;
        // change color
        spriteRenderer.color = new Color(0.75f, 0.448f, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("SkySprite") && active)
        {
            // end scene
            SceneManager.LoadScene("EndScene");
        }
    }
}
