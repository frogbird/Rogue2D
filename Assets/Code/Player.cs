using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    public float maxSpeed = 12f;
    public float jumpForce = 800f;

    public float grav = 1.0f;

    private float DashCooldown;
    private float _lastdash = 0f;
    public bool isDashing = false;

    public float groundRadius = .2f;

    public bool canDoubleJump;
    //public int remainingJumps;

    public bool grounded = true;

    public Transform groundCheck;
    public Transform groundCheckL;
    public Transform groundCheckR;

    public LayerMask groundLayer;
    public LayerMask platformLayer;

    bool facingRight = true;

    //private Gun _gun;
    private PrimaryGun primary;
    private SecondaryGun secondary;

    Animator anim;

    [Header("Combat")]
    public float damageMultiplier = 1;

    public float movementMultiplier;
    public float jumpMultiplier = 1;
    public float DashMultiplier;

    public int baseHealth;
    public Damageable healthComponent;


    public bool canMove;
    public bool knockback;

    private int platformIntValue;

    SpriteRenderer sprite;

    public Inventory inventory;

    Coroutine dashC = null;

    public GameObject platform;
    public PlatformEffector2D platformComponent;



    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();
        //_gun = GetComponent<Gun>();


        primary = GetComponent<PrimaryGun>();
        secondary = GetComponent<SecondaryGun>();



        sprite = GetComponent<SpriteRenderer>();

        healthComponent = GetComponent<Damageable>();

        DashCooldown = 1f;

        //_lastdash = Time.time;

        canMove = true;
        knockback = false;

        platformIntValue = GameObject.Find("Platforms").layer;

        damageMultiplier = 1;

        inventory = GetComponent<Inventory>();

        platform = GameObject.Find("Platforms");

        platformComponent = platform.GetComponent<PlatformEffector2D>();


    }

    // Update is called once per frame
    void Update() {

        grounded = UpdateGrounded();
        if (grounded)
        {
            //print("Grounded");
            canDoubleJump = true;
        }


        if (Mathf.Abs(rb2d.velocity.y) > 20f)
        {
            rb2d.AddForce(-4 * Physics.gravity);
        }


        float move = Input.GetAxis("Horizontal");

        if (canMove)
        {

            if(!knockback && !isDashing){
                rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
            }

            //rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);

            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                Jump();

            }

            if (Input.GetKeyDown(KeyCode.W) && !grounded && canDoubleJump)
            {
                Jump();
                canDoubleJump = false;

            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                dashC = StartCoroutine(Dash(30f, 0.1f));
                
            }


            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && grounded)
            {
                anim.SetInteger("State", 1);
            }
            else if ((anim.GetInteger("State") == 1) && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
            {
                anim.SetInteger("State", 0);
            }
            else if ((anim.GetInteger("State") == 2) && (grounded && canDoubleJump))
            {
                anim.SetInteger("State", 0);
            }



            if (move > 0 && !facingRight && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                Flip();
            else if (move < 0 && facingRight && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
                Flip();

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 aimPos;
                aimPos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

                if ((aimPos.x < 0 && facingRight) || (aimPos.x > 0 && !facingRight))
                {
                    Flip();

                }

                primary.Fire(damageMultiplier, aimPos);
            }


            if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 aimPos;
                aimPos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

                if ((aimPos.x < 0 && facingRight) || (aimPos.x > 0 && !facingRight))
                {
                    Flip();

                }

                secondary.Fire(damageMultiplier, aimPos);
            }

            if(grounded && Input.GetKeyDown(KeyCode.S))
            {
                bool platformOverlap = new List<Transform> { groundCheck, groundCheckL, groundCheckR }
          .Exists(t => Physics2D.OverlapCircle(t.position, groundRadius, platformLayer));

                if (platformOverlap)
                {
                    StartCoroutine(Dropthrough());
                }

                //StartCoroutine(Dropthrough());
            }


        }

    }


    public IEnumerator Knockback(Vector2 direction, int baseDamage)
    {
        
        if (isDashing)
        {
            StopCoroutine(dashC);
            print("stopping");
            isDashing = false;
            rb2d.gravityScale = 4;

        }


        float knockBackTimer = baseDamage / 30f;

        rb2d.velocity = new Vector2(0, 0);

        canMove = false;
        knockback = true;

        float multiplier;

        if (direction.x < 0)
        {
            multiplier = -1;
        }

        else
        {
            multiplier = 1;
        }

        rb2d.AddForce(new Vector2(0, jumpForce));
        rb2d.velocity = new Vector2(8f * multiplier * knockBackTimer, rb2d.velocity.y);

        sprite.color = Color.red;

        for(int i = 0; i < 32; i++)
        {
            yield return new WaitForSeconds(knockBackTimer / 32);

            if (grounded)
            {
                rb2d.velocity = new Vector2(0, 0f);
                sprite.color = Color.white;
                break;
            }

        }

        sprite.color = Color.white;

        if(grounded){
            rb2d.velocity = new Vector2(0, 0); 
        }
        else{
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        canMove = true;
        knockback = false;

        yield return 0;

    }



    private void Jump()
    {
        anim.SetInteger("State", 2);
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
        rb2d.AddForce(new Vector2(0, grav * jumpForce));
    }


    public IEnumerator Dash(float speed, float duration)
    {
        float time = Time.time;
        if (time < _lastdash + DashCooldown) { yield break; }

        _lastdash = time;

        rb2d.gravityScale = 0;
        isDashing = true;

        if (facingRight)
        {
            canMove = false;

            rb2d.velocity = new Vector2(rb2d.velocity.x + speed, 0);
            yield return new WaitForSeconds(duration);

            rb2d.velocity = new Vector2(rb2d.velocity.x - speed, 0);
            canMove = true;

        }
        else
        {
            canMove = false;

            rb2d.velocity = new Vector2(rb2d.velocity.x - speed, 0);
            yield return new WaitForSeconds(duration);

            rb2d.velocity = new Vector2(rb2d.velocity.x + speed, 0);
            canMove = true;
        }

        rb2d.gravityScale = 4;
        isDashing = false;

    }

    public IEnumerator Dropthrough()
    {

        //print("happening");
        //bc2d.isTrigger = true;
        //bc2d.enabled = false;
        //print(platformLayer);

        platformComponent.colliderMask &= ~LayerMask.GetMask("Player");

        //Physics2D.IgnoreLayerCollision(platformIntValue, gameObject.layer, true);

        yield return new WaitForSeconds(0.3f);



        platformComponent.colliderMask |= LayerMask.GetMask("Player");
        //bc2d.isTrigger = false;
        //Physics2D.IgnoreLayerCollision(platformIntValue, gameObject.layer, false);
        //bc2d.enabled = true;





    }



    private bool UpdateGrounded()
    {

        bool groundOverlap = new List<Transform> { groundCheck, groundCheckL, groundCheckR }
          .Exists(t => Physics2D.OverlapCircle(t.position, groundRadius, groundLayer));

        bool platformOverlap = new List<Transform> { groundCheck, groundCheckL, groundCheckR }
          .Exists(t => Physics2D.OverlapCircle(t.position, groundRadius, platformLayer));

        bool anyOverlap = groundOverlap || platformOverlap;

        return anyOverlap && rb2d.velocity.y == 0;

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void HealthUpdate(int amount)
    {
        healthComponent.IncreaseMaxHealth(amount);
        healthComponent.RefillHealth(amount);
    }

    public void damageUpdate(float increase)
    {
        damageMultiplier += increase;
    }

    public void jumpUpdate(float increase)
    {
        jumpMultiplier += increase;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PickupItemIfAvailable(collision);
    }

    private void PickupItemIfAvailable(Collision2D collision)
    {
        GameItem item = collision.gameObject.GetComponent<GameItem>();

        if (item != null)
        {
            inventory.InsertItem(item.consumableItem);
            item.Die();
        }
    }

}
