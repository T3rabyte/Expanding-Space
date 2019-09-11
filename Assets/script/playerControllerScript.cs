﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playerControllerScript : MonoBehaviour
{
    private Scene scene;

    public Rigidbody2D rb;
    public float maxSpeed = 5f;
    public float maxSpeedInAir = 3.2f;
    public bool facingRight = true;

    public bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.3f;

    public Animator anim;

    public bool busy = false;
    public bool GroundHit;
    public int groundcount;

    float move;
    public float jumpForce = 700f;

    public bool Death;
    public bool isGrounded;
    public bool walinkgparticale;

    public Collider2D bodyCollider;
    public Collider2D groundCheck;
    public Collider2D bukCollider;
    public Collider2D melleeRange;
    public Collider2D blockCollider;

    public ParticleSystem jump;
    public ParticleSystem Shoot;
    public ParticleSystem walk;

    public bool blocking = false;

    public GameObject Orb;

    public bool inRange;

    public bool buk;

    public AudioSource audioSource;

    public AudioClip OrbShot;
    public AudioClip Jump;
    public AudioClip mellee;

    public bool laserInRange;
    public bool laserBuisy = false;

    void Start ()
    {
        anim = GetComponent<Animator>();
        var emission = jump.emission;
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        laserInRange = bodyCollider.IsTouchingLayers(LayerMask.GetMask("laser"));
        Death = bodyCollider.IsTouchingLayers(LayerMask.GetMask("death"));
        if (scene.name == "lvl1" || scene.name == "lvl2")
        {
            if (GameObject.Find("GM").GetComponent<GM>().healthEnemy >= 0)
            {
                input();
            }
            if (Death)
            {
                GameObject.Find("GM").GetComponent<GM>().healthPlayer = 0;
            }
        }

        if (scene.name == "lvl2")
        {
            if (laserInRange && GameObject.Find("Boss").GetComponent<BossSpaceship>().laserActive == true && !laserBuisy)
            {
                laserBuisy = true;
                StartCoroutine(laserDamage());
            }
        }
    }

    public void input()
    {
        anim.SetFloat("vSpeed", rb.velocity.y);

        move = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", Mathf.Abs(move));
        if (Input.GetAxis("Horizontal") != 0 && walinkgparticale == false && scene.name == "lvl1")
        {
            walinkgparticale = true;
            walk.Play();
        }
        else if (Input.GetAxis("Horizontal") == 0 && scene.name == "lvl1")
        {
            walinkgparticale = false;
            walk.Stop();

        }

        //////////////////////////////////////////////////////////////////////////////// Horizontal movement
        
        if (isGrounded && !buk)
        {
            rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
        }
        if (!isGrounded)
        {
            rb.velocity = new Vector2(move * maxSpeedInAir, rb.velocity.y);
            walk.Stop();
            walinkgparticale = false;
        }

        //////////////////////////////////////////////////////////////////////////////// Sprite Flipping

        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }

        //////////////////////////////////////////////////////////////////////////////// grounding & jump

        isGrounded = groundCheck.IsTouchingLayers(LayerMask.GetMask("ground"));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audioSource.clip = Jump;
            audioSource.volume = 0.1f;
            audioSource.Play();
            rb.AddForce(new Vector2(0, jumpForce));
            anim.SetBool("Ground", isGrounded);
        }
        else
        {
            anim.SetBool("Ground", isGrounded);
        }

        //////////////////////////////////////////////////////////////////////////////// bukken

        if (Input.GetKeyDown(KeyCode.LeftShift) && move < 0.01)
        {
            buk = true;
            bukCollider.enabled = false;
            anim.SetBool("bukt", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            buk = false;
            bukCollider.enabled = true;
            anim.SetBool("bukt", false);
        }

        //////////////////////////////////////////////////////////////////////////////// shieten

        if (Input.GetMouseButtonDown(0) && !buk && isGrounded)
        {
            StartCoroutine(shoot());
        }

        //////////////////////////////////////////////////////////////////////////////// blocken

        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded)
        {
            blocking = true;
            blockCollider.enabled = true;
            anim.SetBool("block", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            blocking = false;
            blockCollider.enabled = false;
            anim.SetBool("block", false);
        }

        //////////////////////////////////////////////////////////////////////////////// mellee

        if (Input.GetMouseButtonDown(1) && !attacking)
        {
            attacking = true;
            attackTimer = attackCd;
            anim.SetBool("attack", attacking);

            audioSource.clip = mellee;
            audioSource.volume = 1;
            audioSource.Play();

            inRange = melleeRange.IsTouchingLayers(LayerMask.GetMask("enemy"));

            if (inRange)
            {
                GameObject.Find("GM").GetComponent<GM>().score += 5;
                GameObject.Find("GM").GetComponent<GM>().healthEnemy -= 1;
            }
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                anim.SetBool("attack", attacking);
            }
        }
    }
    
    public void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator shoot()
    {
        if (move <= 0.01)
        {
            anim.Play("playerShoot");
        }
        if (move >= 0.01 && !busy)
        {
            busy = true;
            anim.Play("playerWalkShoot");
            yield return new WaitForSeconds(0.3f);
            busy = false;
        }
        if (move <= -0.01 && !busy)
        {
            busy = true;
            anim.Play("playerWalkShoot");
            yield return new WaitForSeconds(0.3f);
            busy = false;
        }
        audioSource.clip = OrbShot;
        audioSource.volume = 1;
        audioSource.Play();
        Instantiate(Orb, new Vector3(this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y - 0.251f, -3), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(shoot());
    }

    IEnumerator laserDamage()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 144, 144, 255);
        yield return new WaitForSeconds(0.11f);
        GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("GM").GetComponent<GM>().healthPlayer -= 25;
        yield return new WaitForSeconds(1);
        laserBuisy = false;
        StopCoroutine(laserDamage());
    }
}
