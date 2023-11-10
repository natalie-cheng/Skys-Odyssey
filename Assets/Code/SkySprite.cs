using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySprite : MonoBehaviour
{
    // sprite speed
    public float speed = 4;
    // threshhold for double jumping
    public float velThreshhold = 0.01f;

    // fire state vars
    public Sprite fireSprite;
    public RuntimeAnimatorController fireAnimate;
    private float fireRadius = 0.14f;
    private Vector2 fireOffset = new Vector2(-0.01f, -0.03f);

    // water state vars
    public Sprite waterSprite;
    public RuntimeAnimatorController waterAnimate;
    private float waterRadius = 0.12f;
    private Vector2 waterOffset = new Vector2(0, 0.02f);

    // air state vars
    public Sprite airSprite;
    public RuntimeAnimatorController airAnimate;
    private float airRadius = 0.11f;
    private Vector2 airOffset = new Vector2(0, -0.03f);

    // how often player can switch sprites
    private float spriteDelay = 0.3f;
    // 0 is fire, 1 is air, 2 is water
    private float spriteState = 0;

    // water shield
    public GameObject shieldPrefab;
    private GameObject waterShield;

    // fireball
    public GameObject ballPrefab;
    public float ballDelay = 0.4f;
    public float ballSpeed = 5;
    public float numBalls = 8;
    public float angleIncrement;

    // time tracker
    private float spriteTime;
    private float abilityTime;

    // sprite vars
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CircleCollider2D spriteCollider;

    // sprite audio
    private AudioSource sfx;
    public AudioClip scoreSfx;
    public AudioClip dmgSfx;

    // static vars
    public static float health;

    // vorax damage
    public float shotDamage = 10;
    public float voraxDamage = 20;

    // call start
    private void Start()
    {
        // initialize sprite components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteCollider = GetComponent<CircleCollider2D>();
        sfx = GetComponent<AudioSource>();

        // get current time
        spriteTime = Time.time;
        abilityTime = Time.time;

        // initialize health
        health = 100;

        // initialize angle increment for shooting
        angleIncrement = 360f / numBalls;
    }

    // frame update
    private void Update()
    {
        // change state of sprite if necessary
        // implement a sprite switch cooldown
        if (Input.GetButton("Switch") && (Time.time - spriteTime > spriteDelay))
        {
            ChangeSprite();
            spriteTime = Time.time;
        }
        // use ability
        if (Input.GetButton("Fire"))
        {
            UseAbility();
        }
        // destroy shield if necessary
        if (spriteState != 2 || Input.GetButtonUp("Fire"))
        {
            // destroy the shield
            if (waterShield != null)
            {
                Destroy(waterShield);
            }
        }
    }

    // physics update
    private void FixedUpdate()
    {
        // move the sprite
        Move();
    }

    // manoeuver around map management
    private void Move()
    {
        // horizontal input axis
        float horizontal = Input.GetAxis("Horizontal");

        // allow double jumps/flying only if it's the air sprite
        if (Input.GetButton("Vertical") && (spriteState == 1 || Mathf.Abs(rb.velocity.y) < velThreshhold))
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

    // sprite use their ability
    private void UseAbility()
    {
        // fire ability - shoot
        if (spriteState == 0 && ((Time.time - abilityTime) > ballDelay))
        {
            // shoot
            Shoot();

            // ability time cooldown
            abilityTime = Time.time;
        }
        // air ability - jump/double jump
        else if (spriteState == 1)
        {
            // jump
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        // water ability - shield
        else if (spriteState == 2 && waterShield == null)
        {
            //shield
            waterShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }
        
    }

    // fire sprite shoot fireballs in a circle around sprite
    private void Shoot()
    {
        // initial angle 0
        float currentAngle = 0;

        // loop through however many balls needed
        for (int i = 0; i < numBalls; i++)
        {
            // instantiate ball at player position
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            // calculate ball velocity components
            float xVel = ballSpeed * Mathf.Cos(Mathf.Deg2Rad * currentAngle);
            float yVel = ballSpeed * Mathf.Sin(Mathf.Deg2Rad * currentAngle);

            // set initial velocity and rotation of ball
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
            ball.transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);

            // update angle
            currentAngle += angleIncrement;
        }
    }

    // sprite state management
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
            // change the sprite and animation
            spriteRenderer.sprite = fireSprite;
            animator.runtimeAnimatorController = fireAnimate;
            // adjust the collider
            spriteCollider.radius = fireRadius;
            spriteCollider.offset = fireOffset;
        }
        // if it's air sprite
        else if (spriteState == 1)
        {
            // change the sprite and animation
            spriteRenderer.sprite = airSprite;
            animator.runtimeAnimatorController = airAnimate;
            // adjust the collider
            spriteCollider.radius = airRadius;
            spriteCollider.offset = airOffset;
        }
        // if it's water sprite
        else if (spriteState == 2)
        {
            // change the sprite and animation
            spriteRenderer.sprite = waterSprite;
            animator.runtimeAnimatorController = waterAnimate;
            // adjust the collider
            spriteCollider.radius = waterRadius;
            spriteCollider.offset = waterOffset;
        }
    }

    // collision management
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if it collides with a vorax shot, deduct health
        if (collision.collider.name.Contains("VoraxShot"))
        {
            health -= shotDamage;
            UI.ChangeHealth(shotDamage);
            sfx.PlayOneShot(dmgSfx, 1);
        }

        // if it collides with a vorax, deduct health
        else if (collision.collider.name.Contains("Vorax"))
        {
            health -= voraxDamage;
            UI.ChangeHealth(voraxDamage);
            sfx.PlayOneShot(dmgSfx, 1);
        }

        // if it collides with a crystal, play sfx, increase score
        else if (collision.collider.name.Contains("Crystal"))
        {
            UI.IncreaseScore();
            sfx.PlayOneShot(scoreSfx, 0.85f);
        }
    }
}
