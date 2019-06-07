using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{
    public Collider2D EnemyorbCollider;
    public bool inRange;
    public bool destroy;

    public float maxSpeed = -5f;

    public Rigidbody2D rb;

    float orbXposition;
    

    void Start()
    {
        orbXposition = this.gameObject.transform.position.x;
    }


    void Update()
    {
        inRange = EnemyorbCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        destroy = EnemyorbCollider.IsTouchingLayers(LayerMask.GetMask("border"));


        rb.velocity = new Vector2(maxSpeed, rb.velocity.y);


        if (inRange)
        {
            GameObject.Find("GM").GetComponent<GM>().healthPlayer -= 10;
            Destroy(this.gameObject);
        }
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }

}
