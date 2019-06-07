using UnityEngine;
using System.Collections;

public class orb : MonoBehaviour
{
    public Collider2D orbCollider;
    public bool inRange;
    public bool destroy;

    public float maxSpeed = 1f;

    public Rigidbody2D rb;

    public Animator anim;

    float orbXposition;
    
    void Start()
    {
        orbXposition = this.gameObject.transform.position.x;
    }
    
    void Update()
    {
        inRange = orbCollider.IsTouchingLayers(LayerMask.GetMask("enemy"));
        destroy = orbCollider.IsTouchingLayers(LayerMask.GetMask("border"));
        
        rb.velocity = new Vector2(maxSpeed, rb.velocity.y);

        if (inRange)
        {
            GameObject.Find("GM").GetComponent<GM>().score += 1;
            GameObject.Find("GM").GetComponent<GM>().healthEnemy -= 1;
            Destroy(this.gameObject);
        }
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }
}
