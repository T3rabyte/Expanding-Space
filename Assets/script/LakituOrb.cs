using System.Collections;
using UnityEngine;


public class LakituOrb : MonoBehaviour
{
    public Collider2D EnemyorbCollider;
    public bool inRange;
    public bool destroy;
    public bool ground;

    public GameObject target;

    public float maxSpeedRight = 2f;
    public float maxSpeedLeft = -2f;

    public Rigidbody2D rb;

    float orbXposition;

    bool buisy = false;

    bool right;


    void Start()
    {
        target = GameObject.Find("Missle/orb Target");
        orbXposition = this.gameObject.transform.position.x;
        transform.up = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
        if (target.transform.position.x <= this.transform.position.z)
        {
            right = false;
        }
        if (target.transform.position.x >= this.transform.position.z)
        {
            right = true;
        }
    }


    void Update()
    {
        inRange = EnemyorbCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        destroy = EnemyorbCollider.IsTouchingLayers(LayerMask.GetMask("border"));
        ground = EnemyorbCollider.IsTouchingLayers(LayerMask.GetMask("ground"));

        if (right)
        {
            rb.velocity = transform.up * maxSpeedRight;
        }
        if (!right)
        {
            rb.velocity = transform.up * maxSpeedLeft;
        }


        if (inRange && !buisy)
        {
            buisy = true;
            StartCoroutine(damage());
        }
        if (destroy || ground || this.gameObject.transform.position.x > 79.034)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator damage()
    {
        GameObject.Find("player").GetComponent<SpriteRenderer>().color = new Color32(255, 144, 144, 255);
        yield return new WaitForSeconds(0.11f);
        GameObject.Find("player").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("GM").GetComponent<GM>().healthPlayer -= 10;
        Destroy(this.gameObject);
        StopCoroutine(damage());
    }
}