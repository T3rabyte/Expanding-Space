using UnityEngine;
using System.Collections;

public class playerControllerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed = 5f;
    public float maxSpeedInAir = 3.2f;
    public bool facingRight = true;

    public bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.3f;

    public Animator anim;

    public float jumpForce = 700f;

    public bool isGrounded;

    public Collider2D groundCheck;
    public Collider2D bukCollider;
    public Collider2D melleeRange;
    public Collider2D blockCollider;

    public GameObject Orb;

    public bool inRange;

    public bool buk;

    public AudioSource audioSource;

    public AudioClip OrbShot;
    public AudioClip Jump;
    public AudioClip mellee;

    void Start ()
    {
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        if (GameObject.Find("GM").GetComponent<GM>().healthEnemy >= 0)
        {
            anim.SetFloat("vSpeed", rb.velocity.y);

            float move = Input.GetAxis("Horizontal");

            anim.SetFloat("speed", Mathf.Abs(move));

            //////////////////////////////////////////////////////////////////////////////// Horizontal movement
            if (isGrounded && !buk)
            {
                rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
            }
            if (!isGrounded)
            {
                rb.velocity = new Vector2(move * maxSpeedInAir, rb.velocity.y);
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

            if (Input.GetKeyDown(KeyCode.LeftShift))
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

            if (Input.GetMouseButtonDown(1) && !buk && isGrounded)
            {
                StartCoroutine(shoot());
            }

            //////////////////////////////////////////////////////////////////////////////// blocken

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                blockCollider.enabled = true;
                anim.SetBool("block", true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                blockCollider.enabled = false;
                anim.SetBool("block", false);
            }

            //////////////////////////////////////////////////////////////////////////////// mellee

            if (Input.GetMouseButtonDown(0) && !attacking)
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
        anim.Play("playerShoot");
        audioSource.clip = OrbShot;
        audioSource.volume = 1;
        audioSource.Play();
        Instantiate(Orb, new Vector3(this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y - 0.251f, -3), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(shoot());
    }
}
