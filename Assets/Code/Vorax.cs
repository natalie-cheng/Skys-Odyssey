using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vorax : MonoBehaviour
{
    // player position
    private Transform player;

    // vorax vars
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float radius = 11; // range of vorax
    private float health = 3;
    private Vector3 voraxColor;
    private float opacity = 1;

    // vorax shot time tracker
    private float shotDelay = 3;
    private float currentTime;
    // vorax shot
    public GameObject shotPrefab;

    // face the player?
    // if is hit three times, dies - more transparent?

    // call start
    void Start()
    {
        // store the player, vorax vars, and current time
        player = FindObjectOfType<SkySprite>().transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentTime = Time.time-shotDelay;
        voraxColor = new Vector3(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b);
    }

    // frame update
    void Update()
    {
        // if the player is within range, shoot shot
        if (WithinRange())
        {
            Shoot();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // return whether the player is within the range of the vorax
    private bool WithinRange()
    {
        // get the distance between the vorax and the player
        float playerDist = Vector3.Distance(transform.position, player.position);

        // if it's less than the radius, it's within range
        if (playerDist < radius)
        {
            return true;
        }

        return false;
    }

    // shoot vorax shot
    private void Shoot()
    {
        // if enough time has passed
        if (Time.time - currentTime > shotDelay)
        {
            // shoot the shot and update
            GameObject shot = Instantiate(shotPrefab, transform.position, transform.rotation);
            currentTime = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if it collides with fireball
        if (collision.collider.name.Contains("Fireball"))
        {
            health--;
            opacity -= 0.15f;
            spriteRenderer.color = new Color(voraxColor.x, voraxColor.y, voraxColor.z, opacity);
        }
    }
}
