using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    // if colliding with the skysprite
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("SkySprite"))
        {
            // increase score
            //UI.IncreaseScore();
            // destroy crystal
            Destroy(gameObject);
        }
    }
}
