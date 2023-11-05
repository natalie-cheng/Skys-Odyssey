using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoraxShot : MonoBehaviour
{
    // set the time and lifespan of the shot
    private float currentTime;
    public float lifespan = 4;

    // shot vars
    private Rigidbody2D rb;
    public float shotSpeed = 10;

    // player position and direction
    private Transform player;
    private Vector3 playerDir;
    private float playerAngle;

    // call start
    private void Start()
    {
        // find the player
        player = FindObjectOfType<SkySprite>().transform;
        // initialize shot rigidbody
        rb = GetComponent<Rigidbody2D>();

        // get the direction of the player
        // mouseDir = (mousePos - transform.position).normalize;
        playerDir = (player.position - transform.position);
        playerDir = Vector3.Normalize(playerDir);

        // set the velocity in the direction of the player
        // velocity is constant throughout shot life
        rb.velocity = playerDir * shotSpeed;

        // calculate the angle to the mousepos
        playerAngle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;

        // rotate the fireball by angle to face the mousepos
        transform.rotation = Quaternion.AngleAxis(playerAngle, Vector3.forward);

        // set currentTime
        currentTime = Time.time;
    }

    // frame update
    private void Update()
    {
        // if lifespan time has passed, destroy object
        if (Time.time - currentTime > lifespan)
        {
            Destroy(gameObject);
        }

        // find the direction of the player
        playerDir = (player.position - transform.position);
        playerDir = Vector3.Normalize(playerDir);

        // set the velocity in the direction of the player
        // velocity is constant throughout shot life
        rb.velocity = playerDir * shotSpeed;

        // calculate the angle to the mousepos
        playerAngle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;

        // rotate the fireball by angle to face the mousepos
        transform.rotation = Quaternion.AngleAxis(playerAngle, Vector3.forward);
    }

    // if it collides with anything
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy object
        Destroy(gameObject);
    }
}
