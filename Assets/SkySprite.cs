using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySprite : MonoBehaviour
{
    // sprite speed
    public float speed = 5;
    // threshhold for double jumping
    public float velThreshhold = 0.5f;

    // the sprite states and animations
    public Sprite fireSprite;
    public Sprite waterSprite;
    public Sprite airSprite;
    public RuntimeAnimatorController fireAnimate;
    public RuntimeAnimatorController waterAnimate;
    public RuntimeAnimatorController airAnimate;
    // how often player can switch sprites
    private float spriteDelay = 0.5f;
    // 0 is fire, 1 is water, 2 is air
    private float spriteState = 0;

    // time tracker
    private float currentTime;

    // sprite components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // call start
    private void Start()
    {
        // initialize sprite components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // get current time
        currentTime = Time.time;
    }

    // physics update
    private void FixedUpdate()
    {
        // move the sprite
        Move();
        // change state of sprite if necessary
        // implement a switch cooldown
        if (Input.GetButton("Switch") && (Time.time-currentTime>spriteDelay))
        {
            ChangeSprite();
            currentTime = Time.time;
        }
    }

    private void Move()
    {
        // horizontal input axis
        float horizontal = Input.GetAxis("Horizontal");

        // allow for double jumps when velocity is less than threshhold
        // also can only jump if it's the air sprite
        if (Input.GetButton("Vertical") && Mathf.Abs(rb.velocity.y) < velThreshhold && spriteState == 2)
        {
            rb.velocity = new Vector2(horizontal * speed, speed);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        // fix the orientation of the sprite
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void ChangeSprite()
    {
        // change the sprite state
        if (spriteState == 2)
        {
            spriteState = 0;
        }
        else
        {
            spriteState++;
        }

        // if it's fire sprite
        if (spriteState == 0)
        {
            spriteRenderer.sprite = fireSprite;
            animator.runtimeAnimatorController = fireAnimate;
        }
        // if it's water sprite
        else if (spriteState == 1)
        {
            spriteRenderer.sprite = waterSprite;
            animator.runtimeAnimatorController = waterAnimate;
        }
        // if it's air sprite
        else if (spriteState == 2)
        {
            spriteRenderer.sprite = airSprite;
            animator.runtimeAnimatorController = airAnimate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spriteState != 1)
        {

        }
    }
}
