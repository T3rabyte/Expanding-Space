using UnityEngine;
using System.Collections;

public class orb : MonoBehaviour
{
    public Collider2D orbCollider;
    public bool enemy;
    public bool destroy;
    public bool missle;

    public float maxSpeedRechts = 1f;
    public float maxSpeedLinks = -1f;

    public Rigidbody2D rb;

    public Animator anim;

    float orbXposition;

    bool buisy = false;

    bool Rechts;

    void Start()
    {
        orbXposition = this.gameObject.transform.position.x;
        if (GameObject.Find("player").GetComponent<playerControllerScript>().facingRight == true)
        {
            Rechts = true;
        }
        if (GameObject.Find("player").GetComponent<playerControllerScript>().facingRight == false)
        {
            Rechts = false;
        }
    }
    
    void Update()
    {
        enemy = orbCollider.IsTouchingLayers(LayerMask.GetMask("enemy"));
        destroy = orbCollider.IsTouchingLayers(LayerMask.GetMask("border"));
        missle = orbCollider.IsTouchingLayers(LayerMask.GetMask("missle"));

        if (Rechts)
        {
            rb.velocity = new Vector2(maxSpeedRechts, rb.velocity.y);
        }
        if (!Rechts)
        {
            rb.velocity = new Vector2(maxSpeedLinks, rb.velocity.y);
        }

        if (enemy && !buisy)
        {
            buisy = true;
            StartCoroutine(damage());
        }
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator damage()
    {
        GameObject.Find("Boss").GetComponent<SpriteRenderer>().color = new Color32(255, 144, 144, 255);
        yield return new WaitForSeconds(0.11f);
        GameObject.Find("Boss").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("GM").GetComponent<GM>().score += 1;
        GameObject.Find("GM").GetComponent<GM>().healthEnemy -= 1;
        Destroy(this.gameObject);
        StopCoroutine(damage());
    }
}