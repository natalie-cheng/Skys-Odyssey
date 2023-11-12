using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // position and direction of the mouse
    private Vector3 mousePos;
    private Vector3 mouseDir;

    // ball vars
    private Rigidbody2D rb;
    public float ballSpeed = 8;

    // set the time and lifespan of the fireball
    private float currentTime;
    public float lifespan = 1.2f;

    // call start on a new fireball
    private void Start()
    {
        // get the position and direction of the mouse
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //// mouseDir = (mousePos - transform.position).normalize;
        //mouseDir = (mousePos - transform.position);
        //mouseDir = Vector3.Normalize(mouseDir);
        ////Debug.Log(mouseDir);
        //// initialize ball rigidbody
        //rb = GetComponent<Rigidbody2D>();

        //// set the velocity in the direction of the mouse
        //// velocity is constant throughout ball life
        ////rb.velocity = mouseDir * ballSpeed;
        //rb.velocity = new Vector3(1, 0, 0);
        //rb.velocity *= ballSpeed;

        //// calculate the angle to the mousepos
        //float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

        //// rotate the fireball by angle to face the mousepos
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // set currentTime
        currentTime = Time.time;
    }

    private void Update()
    {
        // if lifespan time has passed, destroy object
        if (Time.time - currentTime > lifespan)
        {
            Destroy(gameObject);
        }
    }

    // if the fireball goes off the screen
    // theoretically not possible because of walls, but in case
    private void OnBecameInvisible()
    {
        // destroy object
        Destroy(gameObject);
    }

    // if it collides with anything except skysprite
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy object
        Destroy(gameObject);
    }
}
