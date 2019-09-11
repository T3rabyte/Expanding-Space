using System.Collections;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{
    public Collider2D EnemyorbCollider;
    public bool inRange;
    public bool destroy;

    public float maxSpeed = -5f;

    public Rigidbody2D rb;

    float orbXposition;

    bool busy = false;
    

    void Start()
    {
        orbXposition = this.gameObject.transform.position.x;
    }


    void Update()
    {
        inRange = EnemyorbCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        destroy = EnemyorbCollider.IsTouchingLayers(LayerMask.GetMask("border"));
        
        rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        
        if (inRange && !busy)
        {
            busy = true;
            StartCoroutine(damage());
        }
        if (destroy)
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
